using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MyMsgTopPanel : BasePlane {

    private Button back;
    private Toggle[] toggles;

    private ChatScroller chatScroller;

    private const int showPage = 10;
    public int currPage = 1;
    private int sumPage = 1;
    private string type="1";

    private Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();
        back = transform.Find("Top/Back").GetComponent<Button>();
        toggles = transform.Find("Top/ToggleGroup").GetComponentsInChildren<Toggle>();

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        toggles[0].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                type = "1";
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetClubMessage, new object[] {currPage, showPage });
            }
        });
        toggles[1].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                type = "2";
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetMessage, new object[] { type, currPage, showPage });
            }
        });
        toggles[2].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                type = "3";
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetMessage, new object[] { type, currPage, showPage });
            }
        });
    }

    //下拉刷新
    public void SetReflash()
    {
        //if (currPage <= sumPage)
        //{
        if (type=="1")
        {
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetClubMessage, new object[] { currPage, showPage });
        }
        else if(type == "2")
        {
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetMessage, new object[] { type, currPage, showPage });
        }
        
        //}

    }
    public void SetMsgData(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["msglist"] as ArrayList;
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 7;
            datas[i]["Title"] = ((Hashtable)list[i])["title"].ToString();
            datas[i]["Time"] = ((Hashtable)list[i])["time"].ToString();
			datas[i]["userIcon"] = ((Hashtable)list[i])["userIcon"].ToString();
        }
        if (currPage == 1)
        {
            chatScroller.ClearList();
            chatScroller.datas.Clear();
            chatScroller.modelList.Clear();
            chatScroller.SetDatas(datas);
        }
        else
        {
            chatScroller.SetDatas(0, datas);
        }
    }

    public void SetClubFriendData(Hashtable data)
    {
        if (type=="1")
        {
            sumPage = int.Parse(data["allPage"].ToString());
            ArrayList list = data["msgList"] as ArrayList;
            datas = new List<Hashtable>();
            for (int i = 0; i < list.Count; i++)
            {
                Hashtable ht = (Hashtable)list[i];
                datas.Add(new Hashtable());
                datas[i]["Type"] = 6;
                datas[i]["Title"] = ht["content"].ToString();
                datas[i]["Msg"] = "";
                datas[i]["ClubID"] = ht["clubId"].ToString();
                datas[i]["Time"] = ht["time"].ToString();
                datas[i]["ID"] = ht["userId"].ToString();
				datas[i]["userIcon"] = ht["userIcon"].ToString();
                datas[i]["Type1"] = "1";
                datas[i]["unionId"] = "";
                if(ht.ContainsKey("allianceId")){
                    datas[i]["unionId"] = ht["allianceId"].ToString();
                }
            }
            if (currPage == 1)
            {
                chatScroller.ClearList();
                chatScroller.datas.Clear();
                chatScroller.modelList.Clear();
                chatScroller.SetDatas(datas);
            }
            else
            {
                chatScroller.SetDatas(0, datas);
            }
        }
        else
        {
            sumPage = int.Parse(data["allPage"].ToString());
            ArrayList list = data["msglist"] as ArrayList;
            datas = new List<Hashtable>();
            for (int i = 0; i < list.Count; i++)
            {
                datas.Add(new Hashtable());
                datas[i]["Type"] = 6;
                datas[i]["Title"] = ((Hashtable)list[i])["title"].ToString();
                datas[i]["Msg"] = ((Hashtable)list[i])["msg"].ToString();
                datas[i]["Time"] = ((Hashtable)list[i])["time"].ToString();
                datas[i]["ID"] = ((Hashtable)list[i])["id"].ToString();
				datas[i]["userIcon"] = ((Hashtable)list[i])["userIcon"].ToString();
                datas[i]["ClubID"] = "";
                datas[i]["Type1"] = "2";
            }
            if (currPage == 1)
            {
                chatScroller.ClearList();
                chatScroller.datas.Clear();
                chatScroller.modelList.Clear();
                chatScroller.SetDatas(datas);
            }
            else
            {
                chatScroller.SetDatas(0, datas);
            }
        }
        
    }

    public void IsAgreeFinished(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        currPage = 1;
        if (StaticData.selectScene == 2)
        {
            return;
        }
        else if (StaticData.selectScene == 1 && HallManager.GetSingleton().myMsgTopPanel.gameObject.activeSelf)
        {
            NetMngr.GetSingleton().Send(InterfaceMain.GetMessage, new object[] { type, currPage, showPage });
        }
    }
    public void IsClubAgreeFinished(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        currPage = 1;
        if (StaticData.selectScene==2)
        {
            return;
        }
        NetMngr.GetSingleton().Send(InterfaceMain.GetClubMessage, new object[] {currPage, showPage });
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public override void OnAddComplete()
    {
        if (toggles[0].isOn)
        {
            NetMngr.GetSingleton().Send(InterfaceMain.GetClubMessage, new object[] { currPage, showPage });
        }
        else
        {
            toggles[0].isOn = true;
            toggles[1].isOn = false;
            toggles[2].isOn = false;
        }
    }

    public override void OnAddStart()
    {
        currPage = 1;
        type = "1";
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
