using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class ScoreBottomPanel : BasePlane {

    private Button backBtn;
    private Toggle[] toggleGroups;
    private Text gameCount;
    private Text profit;
    private Text shouShu;
    private Transform line;
    private Transform parent;
    private ChatScroller chatScroller;
    private Text text;


    private const int showPage = 10;
    private int currPage = 1;
    private int sumPage = 1;
    private string type = "1";

    private Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
		backBtn = transform.Find("Top/Back").GetComponent<Button>();
        toggleGroups = transform.Find("ToggleGroup").GetComponentsInChildren<Toggle>();
        gameCount = transform.Find("Title1/Value").GetComponent<Text>();
        profit = transform.Find("Title2/Value").GetComponent<Text>();
        shouShu = transform.Find("Title3/Value").GetComponent<Text>();
        parent = transform.Find("Scroll View/Viewport/Content");
        line = transform.Find("ToggleGroup/BottomLine");
        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();
        toggleGroups = transform.Find("ToggleGroup").GetComponentsInChildren<Toggle>();
        text = transform.Find("ChatScroller/Text").GetComponent<Text>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        toggleGroups[0].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                currPage = 1;
                type = "1";
                toggleGroups[0].transform.Find("Label").GetComponent<Text>().text = "<color=#FFFFFFFF>100局</color>";
                toggleGroups[1].transform.Find("Label").GetComponent<Text>().text = "<color=#888888FF>7日</color>";
                toggleGroups[2].transform.Find("Label").GetComponent<Text>().text = "<color=#888888FF>30日</color>";
                line.DOScaleX(1.5f,0.15f).OnComplete(()=> { line.DOScaleX(1, 0.15f); });
                line.DOMoveX(toggleGroups[0].transform.position.x,0.3f);
                Refresh(0);
            }
        });
        toggleGroups[1].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                currPage = 1;
                type = "7";
                toggleGroups[1].transform.Find("Label").GetComponent<Text>().text = "<color=#FFFFFFFF>7日</color>";
                toggleGroups[0].transform.Find("Label").GetComponent<Text>().text = "<color=#888888FF>100局</color>";
                toggleGroups[2].transform.Find("Label").GetComponent<Text>().text = "<color=#888888FF>30日</color>";
                line.DOScaleX(1.5f, 0.15f).OnComplete(() => { line.DOScaleX(1, 0.15f); });
                line.DOMoveX(toggleGroups[1].transform.position.x, 0.3f);
                Refresh(1);
            }
        });
        toggleGroups[2].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                currPage = 1;
                type = "30";
                toggleGroups[2].transform.Find("Label").GetComponent<Text>().text = "<color=#FFFFFFFF>30日</color>";
                toggleGroups[1].transform.Find("Label").GetComponent<Text>().text = "<color=#888888FF>7日</color>";
                toggleGroups[0].transform.Find("Label").GetComponent<Text>().text = "<color=#888888FF>100局</color>";
                line.DOScaleX(1.5f, 0.15f).OnComplete(() => { line.DOScaleX(1, 0.15f); });
                line.DOMoveX(toggleGroups[2].transform.position.x, 0.3f);
                Refresh(2);
            }
        });
    }

    void Start ()
    {
	
	}

	void Update ()
    {

	}

    public void SetData(Hashtable data)
    {
        
        sumPage = int.Parse(data["allPage"].ToString());
        gameCount.text = data["gameCount"].ToString();
        profit.text = data["profit"].ToString();
        profit.color = StaticFunction.Getsingleton().GetColor(int.Parse(profit.text));
        shouShu.text = data["shoushu"].ToString();
        ArrayList list = data["GameRecordLog"] as ArrayList;
        
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 4;
            datas[i]["URL"] = ((Hashtable)list[i])["url"].ToString();
            datas[i]["DeskName"] = ((Hashtable)list[i])["deskName"].ToString();
            datas[i]["Chouma"] = ((Hashtable)list[i])["chouma"].ToString();
            datas[i]["Time"] = ((Hashtable)list[i])["time"].ToString();
            string[] strs = ((Hashtable)list[i])["date"].ToString().Split(' ');
            string[] strss1 = strs[0].Split('-');
            string[] strss2= strs[1].Split(':');
            datas[i]["InfoData"] = strss1[1]+"月"+strss1[2]+"日";
            datas[i]["InfoTime"] = strss2[0]+":"+strss2[1];
			datas[i]["InfoName"] = ((Hashtable)list[i])["cldId"].ToString();
            datas[i]["Score"] = ((Hashtable)list[i])["score"].ToString();
            datas[i]["ID"] = ((Hashtable)list[i])["id"].ToString();

			datas[i]["gameType"] = ((Hashtable)list[i])["deskType"].ToString();
			datas[i]["insurance"] = ((Hashtable)list[i])["insurance"].ToString();

        }
        if (currPage == 1)
        {
            chatScroller.ClearList();
            chatScroller.datas.Clear();
            chatScroller.modelList.Clear();
            chatScroller.SetDatas(datas);
            if (list.Count == 0)
            {
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }
            //TouchMove.Instance().AddFunction(TouchMove.ActionType.Left, left);
            //TouchMove.Instance().AddFunction(TouchMove.ActionType.Right, right);
        }
        else
        {
            chatScroller.SetDatas(0, datas);
        }
    }

    //下拉刷新
    public void SetFriendReflash()
    {
        //if (currPage <= sumPage)
        //{
        currPage++;
        NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordTotal, new object[] { type, currPage, showPage });
        //}

    }

    private void Refresh(int number)
    {
        switch (number)
        {
            case 0:
                NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordTotal, new object[] { type, currPage, showPage });
                break;
            case 1:
                NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordTotal, new object[] { type, currPage, showPage });
                break;
            case 2:
                NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordTotal, new object[] { type, currPage, showPage });
                break;
        }
    }
    public void left()
    {
        print(type);
        if (type=="1")
        {
            //toggleGroups[0].isOn = true;
        }
        else if (type=="7")
        {
            toggleGroups[0].isOn = true;
        }
        else
        {
            toggleGroups[1].isOn = true;
        }
    }
    public void right()
    {
        print(type);
        if (type == "1")
        {
            toggleGroups[1].isOn = true;
        }
        else if (type == "7")
        {
            toggleGroups[2].isOn = true;
        }
        else
        {
            //toggleGroups[2].isOn = true;
        }
    }


    public override void OnAddComplete()
    {
        TouchMove.Instance().AddFunction(TouchMove.ActionType.Left, left);
        TouchMove.Instance().AddFunction(TouchMove.ActionType.Right, right);
        NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordTotal, new object[] { type, currPage, showPage });
    }

    public override void OnAddStart()
    {
        TouchMove.Instance().RemoveFunction();
        currPage = 1;
        toggleGroups[0].isOn = true;
        toggleGroups[1].isOn = false;
        toggleGroups[2].isOn = false;
        type = "1";
        
    }

    public override void OnRemoveComplete()
    {
        
    }

    public override void OnRemoveStart()
    {
        
    }
}
