using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HallBottomPanel : BasePlane {

    private Button filtrate;

    private ChatScroller chatScroller;

    private const int showPage = 10;
    public int currPage = 1;
    private int sumPage = 1;

    private Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();
		filtrate = transform.Find("Top/Button").GetComponent<Button>();

        filtrate.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //筛选
            ClubManager.GetSingleton().screenMatch.ShowView();
           // NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerEnterRequest, new object[] { "25"});
        });

    }

    //下拉刷新
    public void SetFriendReflash()
    {
        //if (currPage <= sumPage)
        //{
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetGameInfo, new object[] { currPage, showPage });
        //}

    }
   
    public void SetHallData(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["gameInfo"] as ArrayList;
        datas = new List<Hashtable>();
        if(list.Count == 0)
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
         //   return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 0;
			datas[i]["URL"] = ((Hashtable)list[i])["url"].ToString();
            datas[i]["DeskName"] = ((Hashtable)list[i])["deskName"].ToString();
            datas[i]["Name"] = ((Hashtable)list[i])["name"].ToString();
            datas[i]["Chouma"] = ((Hashtable)list[i])["chouma"].ToString();
            datas[i]["Persion"] = ((Hashtable)list[i])["roomPeopleCount"].ToString();
            datas[i]["Time"] = ((Hashtable)list[i])["time"].ToString();
            datas[i]["isStarting"] = ((Hashtable)list[i])["state"].ToString();
            datas[i]["ID"] = ((Hashtable)list[i])["roomId"].ToString();
			datas[i]["clubName"] = ((Hashtable)list[i])["clubName"].ToString();
			datas[i]["insurance"] = ((Hashtable)list[i])["insurance"].ToString();
			datas[i]["gameType"] = ((Hashtable)list[i])["gameType"].ToString();

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

    void Start ()
    {
		
        //Hashtable data = new Hashtable();
        //data = new Hashtable();
        //data["Type"] = 0;
        //data["URL"] = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1550137424643&di=4975165ce2a2aa87c19d238001d8d19e&imgtype=0&src=http%3A%2F%2Fb-ssl.duitang.com%2Fuploads%2Fitem%2F201512%2F24%2F20151224234958_ERPST.jpeg";
        //data["DeskName"] = "10/20/40桌D1";
        //data["Name"] = "天狼一号  天狼星";
        //data["Chouma"] = "10/20(10)";
        //data["Persion"] = "0/9";
        //data["Time"] = "1小时59分钟";
        //data["isStarting"] = "进行中";
        //chatScroller.AddDataAt(chatScroller.DataLength, data);

        //List<Hashtable> datas = new System.Collections.Generic.List<Hashtable>();
        //for (int i = 0; i < 200; i++)
        //{
        //    datas.Add(new Hashtable());
        //    datas[i]["Type"] = 0;
        //    datas[i]["URL"] = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1550137424643&di=4975165ce2a2aa87c19d238001d8d19e&imgtype=0&src=http%3A%2F%2Fb-ssl.duitang.com%2Fuploads%2Fitem%2F201512%2F24%2F20151224234958_ERPST.jpeg";
        //    datas[i]["DeskName"] = "10/20/40桌D5";
        //    datas[i]["Name"] = "天狼五号  天狼星";
        //    datas[i]["Chouma"] = "10/20(10)";
        //    datas[i]["Persion"] = "1/9";
        //    datas[i]["Time"] = "4小时59分钟";
        //    datas[i]["isStarting"] = "已关闭";
        //}
        //chatScroller.SetDatas(datas);
    }

    void Update ()
    {
	
	}

	public void GetGameInfo()
	{
		NetMngr.GetSingleton().Send(InterfaceMain.GetGameInfo, new object[] { currPage, showPage });
	}

    public override void OnAddComplete()
    {
		
    }

    public override void OnAddStart()
    {
        TouchMove.Instance().RemoveFunction();
        currPage = 1;
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
