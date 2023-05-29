using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MyPaiPuTopPanel : BasePlane {

    private Button backBtn;
    private Transform parent;
    private Toggle toggle;
    public Text bottomTip;
    private Button oddbtn;
    private Text n;

    private ChatScroller chatScroller;


    public int odds = -1;
    public int showPage = 10;
    public int currPage = 1;
    private int sumPage = 1;

    private Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        parent = transform.Find("Scroll View/Viewport/Content");
        toggle = transform.Find("Bottom/Toggle").GetComponent<Toggle>();
        bottomTip = transform.Find("Bottom/Text").GetComponent<Text>();
        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();
        oddbtn = transform.Find("Bottom/Text/Button").GetComponent<Button>();
        n = transform.Find("Text").GetComponent<Text>();

        oddbtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().oddsPopup.ShowView();
        });
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        toggle.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetMyPaiData, new object[] { currPage, showPage, odds });
            }
            else
            {
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetMyPaiData, new object[] { currPage, showPage, -1 });
            }
        });
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {

	}
    //下拉刷新
    public void SetFriendReflash()
    {
        //if (currPage <= sumPage)
        //{
        currPage++;
        NetMngr.GetSingleton().Send(InterfaceMain.GetMyPaiData, new object[] { currPage, showPage, odds });
        //}

    }
    public void SetFriendData(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["PaiData"] as ArrayList;
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 1;
            datas[i]["Title"] = ((Hashtable)list[i])["info"].ToString();
            datas[i]["Chouma"] = ((Hashtable)list[i])["chouma"].ToString();
            datas[i]["Win"] = ((Hashtable)list[i])["score"].ToString();
            datas[i]["Score"] = ((Hashtable)list[i])["myscore"].ToString();

			datas[i]["Pai"] = ((Hashtable)list[i])["pai"].ToString();

//            string[] strss= ((Hashtable)list[i])["pai"].ToString().Split('|');
//            datas[i]["Pai1"] = strss[0];
//            datas[i]["Pai2"] = strss[1];

            datas[i]["ID"] = ((Hashtable)list[i])["id"].ToString();

        }
        if (currPage == 1)
        {
            chatScroller.ClearList();
            chatScroller.datas.Clear();
            chatScroller.modelList.Clear();
            chatScroller.SetDatas(datas);
            if (list.Count == 0)
            {
                n.gameObject.SetActive(true);
            }
            else
            {
                n.gameObject.SetActive(false);
            }
        }
        else
        {
            chatScroller.SetDatas(0, datas);
            
        }
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetMyPaiData, new object[] { currPage, showPage, odds });
    }

    public override void OnAddStart()
    {
        currPage = 1;
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
