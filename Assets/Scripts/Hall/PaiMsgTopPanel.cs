using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PaiMsgTopPanel : BasePlane
{
    private Button back;
    private Toggle[] toggles;

    private ChatScroller chatScroller;

    private const int showPage = 10;
    public int currPage = 1;
    private int sumPage = 1;
    private string type = "1";

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
			if (SceneManager.GetActiveScene().name == "Main")
			{
				HallManager.GetSingleton().planeManager.RemoveTopPlane();
			}
            
			if (SceneManager.GetActiveScene().name == "Game")
			{
				GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
			}
			
        });
        toggles[0].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                type = "1";
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetPaiMessage, new object[] { type,currPage, showPage });
            }
        });
        toggles[1].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                type = "2";
                currPage = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetPaiMessage, new object[] { type, currPage, showPage });
            }
        });
    }

    void Start()
    {

    }

    void Update()
    {

    }
    //下拉刷新
    public void SetReflash()
    {
        //if (currPage <= sumPage)
        //{
        if (type == "1")
        {
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetPaiMessage, new object[] { type, currPage, showPage });
        }
        else if (type == "2")
        {
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetPaiMessage, new object[] { type, currPage, showPage });
        }

        //}
    }

    public void SetData(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["msglist"] as ArrayList;
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 8;
            datas[i]["URL"] = ((Hashtable)list[i])["url"].ToString();
            datas[i]["Title"] = ((Hashtable)list[i])["title"].ToString();
            datas[i]["Name"] = ((Hashtable)list[i])["name"].ToString();
            datas[i]["Chouma"] = ((Hashtable)list[i])["chouma"].ToString();
            datas[i]["ID"] = ((Hashtable)list[i])["id"].ToString();
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
    public void SetData2(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["msglist"] as ArrayList;
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 9;
            datas[i]["URL"] = ((Hashtable)list[i])["url"].ToString();
            datas[i]["Title"] = ((Hashtable)list[i])["title"].ToString();
            datas[i]["Name"] = ((Hashtable)list[i])["name"].ToString();
            datas[i]["Chouma"] = ((Hashtable)list[i])["chouma"].ToString();
            datas[i]["Time"] = ((Hashtable)list[i])["time"].ToString();
            datas[i]["State"] = ((Hashtable)list[i])["state"].ToString();
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
    public void IsAgreeFinished(Hashtable data)
    {
        currPage = 1;
        if(StaticData.selectScene==1&& HallManager.GetSingleton().paiMsgTopPanel.gameObject.activeSelf)
        {
            print("11");
            NetMngr.GetSingleton().Send(InterfaceMain.GetPaiMessage, new object[] { type, currPage, showPage });
        }
    }
    public override void OnAddComplete()
    {
        if (toggles[0].isOn)
        {
            NetMngr.GetSingleton().Send(InterfaceMain.GetPaiMessage, new object[] { type, currPage, showPage });
        }
        else
        {
            toggles[0].isOn = true;
            toggles[1].isOn = false;
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
