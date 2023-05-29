using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NowRecordPanel : BasePlane
{

    public Text zongshoushu;
    public Text meishouhaoshi;
    public Text pingjundichi;
    public Text pingjunmairu;
    public Text textCountDown;
    public Transform playerContent;
    public Transform viewersContent;

    public Text pangguanCount;

    public Button backBtn;

    public Text baoxianBuy;
    public Text baoxianScore;
    public Image baoxianCoinBg;
    public Text caishoupaiBuy;
    public Text caishoupaiScore;
    public Image caishoupaiCoinBg;

    void Awake() {

        backBtn = transform.Find("BackBtn").GetComponent<Button>();

        zongshoushu = transform.Find("Title/zongshoushu/Text").GetComponent<Text>();
        meishouhaoshi= transform.Find("Title/meishouhaoshi/Text").GetComponent<Text>();
        pingjundichi= transform.Find("Title/pingjundichi/Text").GetComponent<Text>();
        pingjunmairu= transform.Find("Title/pingjunmairu/Text").GetComponent<Text>();
        textCountDown = transform.Find("Title/Time/Text").GetComponent<Text>();//add  by yang yang 2021年3月16日 15:42:53
        playerContent = transform.Find("infoPanel/Viewport/Content/messageItem/playerItem/info");
        viewersContent= transform.Find("infoPanel/Viewport/Content/messageItem/viewersItem/info");

        pangguanCount= transform.Find("infoPanel/Viewport/Content/messageItem/viewersItem/viewersTitle/pangguan/Text").GetComponent<Text>();

        baoxianBuy = playerContent.Find("baoxianItem").GetChild(2).GetComponent<Text>();
        baoxianScore = playerContent.Find("baoxianItem").GetChild(3).GetChild(0).GetComponent<Text>();
        baoxianCoinBg = playerContent.Find("baoxianItem").GetChild(3).GetComponent<Image>();
        caishoupaiBuy = playerContent.Find("caishoupaiItem").GetChild(2).GetComponent<Text>();
        caishoupaiScore = playerContent.Find("caishoupaiItem").GetChild(3).GetChild(0).GetComponent<Text>();
        caishoupaiCoinBg = playerContent.Find("caishoupaiItem").GetChild(3).GetComponent<Image>();

        backBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameUIManager.GetSingleton().planeManager.RemoveSidePlane();
            //ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            //gameObject.SetActive(false);
        });
      
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override void OnAddComplete()
    {

    } 

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.getNowRecord, new object[] { });
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
   
    public void GetNowRecordCallBack(Hashtable data) {
        Debug.Log("实时战报 GetNowRecordCallBack(Hashtable data) ");
        //playerContent.Find("baoxianItem").gameObject.SetActive(GameUIManager.GetSingleton().isAdmin);//如果当前是房主才会显示
        zongshoushu.text = data["zongshoushu"].ToString();
        meishouhaoshi.text = data["meishouhaoshi"].ToString();
        pingjundichi.text = data["pingjundichi"].ToString();
        pingjunmairu.text = data["pingjunmairu"].ToString();
        pangguanCount.text = "("+data["pangguanCount"].ToString()+")";
        baoxianBuy.text = data["baoxianBuy"].ToString();
        baoxianScore.text = data["baoxianScore"].ToString();
        GameUIManager.GetSingleton().setNewTime(data);
        if (int.Parse(baoxianScore.text) > 0)
        {
            this.baoxianScore.text = "+" + baoxianScore.text;
            this.baoxianCoinBg.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/损失BG");
        }
        else {
            this.baoxianScore.text =  baoxianScore.text;
            this.baoxianCoinBg.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/盈利BG");
        }
        
        caishoupaiBuy.text = data["caishoupaiBuy"].ToString();
        caishoupaiScore.text = data["caishoupaiScore"].ToString();
        if (int.Parse(caishoupaiScore.text) > 0)
        {
            this.caishoupaiScore.text = "+" + caishoupaiScore.text;
            this.caishoupaiCoinBg.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/损失BG");
        }
        else {
            this.caishoupaiScore.text =  caishoupaiScore.text;
            this.caishoupaiCoinBg.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/盈利BG");
        }
        
        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);

        ArrayList playerList = data["playerList"] as ArrayList;

        if (playerList == null)
        {
            return;
        }
        List<object> sortPlayerList = new List<object>();
        for(int i = 0; i < playerList.Count; i++){
            sortPlayerList.Add(playerList[i]);
        }
        sortPlayerList.Sort((object a, object b) => {
            Hashtable ha = a as Hashtable;
            Hashtable hb = b as Hashtable;
            int ia = int.Parse(ha["score"].ToString());
            int ib = int.Parse(hb["score"].ToString());
            return (ib - ia) > 0 ? 1 : -1;//正序
        });
        for (int i = 0; i < sortPlayerList.Count; i++)
        {
            Hashtable ht = sortPlayerList[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("GamePopup/GamePopupItem/item")) as GameObject;
            var listv = obj.GetComponent<PlayerItem>();
            listv.transform.parent = playerContent;
            listv.transform.localScale = new Vector2(1, 1);
            //赋值
            listv.SetInfo(ht["playerName"].ToString(), ht["mairu"].ToString(), ht["score"].ToString(), ht["count"].ToString() );
        }

    
        ArrayList leaveList = data["leaveList"] as ArrayList;
        if (leaveList == null)
        {
            return;
        }
        List<object> sortLeaveList = new List<object>();
        for(int i = 0; i < leaveList.Count; i++){
            sortLeaveList.Add(leaveList[i]);
        }
        sortLeaveList.Sort((object a, object b) => {
            Hashtable ha = a as Hashtable;
            Hashtable hb = b as Hashtable;
            int ia = int.Parse(ha["score"].ToString());
            int ib = int.Parse(hb["score"].ToString());
            return ib - ia;
        });
        for (int i = 0; i < sortLeaveList.Count; i++)
        {
            Hashtable ht = sortLeaveList[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load<GameObject>("GamePopup/GamePopupItem/leaveListItem"));
            var lists = obj.GetComponent<PlayerItem>();
            lists.transform.parent = playerContent;
            lists.transform.localScale = new Vector2(1,1);
            //赋值
            lists.SetInfo(ht["playerName"].ToString(),ht["mairu"].ToString(),ht["score"].ToString(),ht["count"].ToString());
        }

        ArrayList List2 = data["viewerList"] as ArrayList;

        if (List2 == null)
        {
            return;
        }
        for (int i = 0; i < List2.Count; i++)
        {
            Hashtable ht = List2[i] as Hashtable;
            
            GameObject obj = Instantiate(Resources.Load("GamePopup/GamePopupItem/viewerItem")) as GameObject;
            var listv = obj.GetComponent<ViewerItem>();
            listv.transform.parent = viewersContent;
            listv.transform.localScale = new Vector2(1, 1);
            //赋值
            listv.SetInfo(ht["headUrl"].ToString(), ht["name"].ToString());
        }

        var seenList = data["seenList"] as ArrayList;
        if (seenList ==null)
        {
            return;
        }

        //for (var i = 0; i < seenList.Count; i++)
        //{
        //    var ht = seenList[i] as Hashtable;
        //    var obj = Instantiate(Resources.Load<GameObject>("GamePopup/GamePopupItem/seenListItem"));
        //    var lists = obj.GetComponent<ViewerItem>();
        //    lists.transform.SetParent(viewersContent);
        //    lists.transform.localScale = new Vector2(1,1);
        //    //赋值
        //    lists.SetInfo(ht["headUrl"].ToString(),ht["name"].ToString());
        //}
    }
  
   

}
