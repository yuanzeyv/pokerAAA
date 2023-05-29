using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class ClubPanel : BasePlane
{
    public Text clubName;
    public Button clubInfoBtn;

    public Button backBtn;

    public Transform announcementContent;
    public Transform MatchContent;
    public Transform RecordContent;

    public Button createGongGaoBtn;
    public Button createMatchBtn;
    public Button refreshMatchBtn;
    
    public GameObject tip1;
    public GameObject tip2;
    public GameObject tip3;

    public string clubId;


    public Toggle GongGaoTog;
    public Toggle MatchTog;
    public Toggle RecordTog;
    public Toggle ClubInfTog;

    public int toggleIndex = 1;

    public int curPage=1;
    public int sumPage=1;
    public static int  pageCount=10;

    private Transform line;

    private Button Btn_msg;
    private Image redImage;


    void Awake()
    {
		clubName = transform.Find("Panel/clubName").GetComponent<Text>();
		clubInfoBtn= transform.Find("Panel/ClubInfo").GetComponent<Button>();
        backBtn = transform.Find("Back/Share").GetComponent<Button>();

        announcementContent= transform.Find("Announcement/ClubContent/Scroll View/Viewport/Content");
        MatchContent= transform.Find("Match/ClubContent/Scroll View/Viewport/Content");
        RecordContent= transform.Find("Record/ClubContent/Scroll View/Viewport/Content");

        createGongGaoBtn= transform.Find("Announcement/ClubContent/CreateGongGaoBtn").GetComponent<Button>();
        createMatchBtn= transform.Find("Match/ClubContent/CreateRoomBtn").GetComponent<Button>();
        refreshMatchBtn= transform.Find("Match/ClubContent/RefreshRoomBtn").GetComponent<Button>();
        
        tip1 = transform.Find("Announcement/ClubContent/tip").gameObject;
        tip2 = transform.Find("Match/ClubContent/tip").gameObject;
        tip3 = transform.Find("Record/ClubContent/tip").gameObject;

        GongGaoTog = transform.Find("Announcement").GetComponent<Toggle>();
        MatchTog= transform.Find("Match").GetComponent<Toggle>();
        RecordTog= transform.Find("Record").GetComponent<Toggle>();
        ClubInfTog = transform.Find("ClubInfo").GetComponent<Toggle>();
        line = transform.Find("BottomLine");

        backBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            gameObject.SetActive(false);
			ClubManager.GetSingleton().clubListPanel.GetClubList();
        });

        clubInfoBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo,new object[] { clubId});
        });
        createMatchBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().createGamePopup.ShowView(2);
        });
        refreshMatchBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceClub.GetClubMatch, new object[] { clubId, 1, 100 });
        });
        createGongGaoBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().createGongGaoTopPanel);
        });

        Btn_msg = transform.Find("Button_msg").GetComponent<Button>();
        Btn_msg.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().noticeListBottomPanel);
        });
        redImage = transform.Find("Button_msg/Image_red").GetComponent<Image>();
        showRed();
      

        GongGaoTog.onValueChanged.AddListener(
            (bool s)=>{
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");    
                if (s)
                {
                    line.DOScaleX(1.5f, 0.15f).OnComplete(() => { line.DOScaleX(1, 0.15f); });
                    line.DOMoveX(GongGaoTog.transform.position.x, 0.3f);
                }
                if (toggleIndex == 1)
                {

                }
                else
                {
                    if (GongGaoTog.isOn)
                    {
                        GameTools.GetSingleton().stopGameToolsAllCoroutines();
                        NetMngr.GetSingleton().Send(InterfaceClub.GetGongGao, new object[] { clubId });
                    }

                }
                toggleIndex = 1;

               
        });


        MatchTog.onValueChanged.AddListener(
            (bool s) => {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                if (s)
                {
                    line.DOScaleX(1.5f, 0.15f).OnComplete(() => { line.DOScaleX(1, 0.15f); });
                    line.DOMoveX(MatchTog.transform.position.x, 0.3f);
                }
                if (toggleIndex == 2)
                {

                }
                else
                {
                    if (MatchTog.isOn)
                    {
                        GameTools.GetSingleton().stopGameToolsAllCoroutines();
                        NetMngr.GetSingleton().Send(InterfaceClub.GetClubMatch, new object[] { clubId, 1, 100 });
                    }
                }
                toggleIndex = 2;           

            });

        RecordTog.onValueChanged.AddListener(
          (bool s) => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
              if (s)
              {
                  line.DOScaleX(1.5f, 0.15f).OnComplete(() => { line.DOScaleX(1, 0.15f); });
                  line.DOMoveX(RecordTog.transform.position.x, 0.3f);
              }
              if (toggleIndex == 3)
              {

              }
              else
              {
                  if (RecordTog.isOn)
                  {
                      GameTools.GetSingleton().stopGameToolsAllCoroutines();
                      curPage = 1;
                      NetMngr.GetSingleton().Send(InterfaceClub.GetClubRecord, new object[] { clubId ,curPage,pageCount});
                  }
              }
              toggleIndex = 3;

             

          });

        ClubInfTog.onValueChanged.AddListener(
          (bool s) =>
          {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
              if (s)
              {
                  line.DOScaleX(1.5f, 0.15f).OnComplete(() => { line.DOScaleX(1, 0.15f); });
                  line.DOMoveX(ClubInfTog.transform.position.x, 0.3f);
              }
              if (toggleIndex == 4)
              {

              }
              else
              {
                  if (ClubInfTog.isOn)
                  {
                      GameTools.GetSingleton().stopGameToolsAllCoroutines();
                      //   curPage = 1;
                      NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo, new object[] { clubId });
                  //    NetMngr.GetSingleton().Send(InterfaceClub.GetClubRecord, new object[] { clubId, curPage, pageCount });
                  }
              }
              toggleIndex = 4;



          });


        tip1.SetActive(false);
        tip2.SetActive(false);
        tip3.SetActive(false);

        gameObject.SetActive(false);
        
        
    }

    void Start()
    {
        GetComponent<ClubTouchMove>().AddFunction(ClubTouchMove.ActionType.Left, left);
        GetComponent<ClubTouchMove>().AddFunction(ClubTouchMove.ActionType.Right, right);
    }

    void Update()
    {
        GetComponent<ClubTouchMove>().isRun = true;
        for (int i = 0; i < HallManager.GetSingleton().containerTop.childCount; i++)
        {
            if (HallManager.GetSingleton().containerTop.GetChild(i).gameObject.activeSelf)
            {
                GetComponent<ClubTouchMove>().isRun = false;
                break;
            }
        }
        for (int i = 0; i < HallManager.GetSingleton().containerPopup.childCount; i++)
        {
            if (HallManager.GetSingleton().containerPopup.GetChild(i).gameObject.activeSelf)
            {
                GetComponent<ClubTouchMove>().isRun = false;
                break;
            }
        }
    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }

    public void left()
    {

        if (toggleIndex == 1)
        {
         //   toggleGroups[0].isOn = true;
        }
        else if (toggleIndex == 2)
        {
            GongGaoTog.isOn = true;
            
        }
        else if (toggleIndex == 3)
        {
            MatchTog.isOn = true;
        }
        else if (toggleIndex == 4)
        {
            ClubInfTog.isOn = true;
        }

    }
    public void right()
    {
        if (toggleIndex == 1)
        {
            MatchTog.isOn = true;
        }
        else if (toggleIndex == 2)
        {
            RecordTog.isOn = true;

        }
        else if (toggleIndex == 3)
        {
            MatchTog.isOn = true;
        }
        else if (toggleIndex == 4)
        {
           ClubInfTog.isOn = true;
        }

    }

    //下拉刷新
    public void SetRecordReflash()
    {
        if (curPage <= sumPage)
        {
        curPage++;
        NetMngr.GetSingleton().Send(InterfaceClub.GetClubRecord, new object[] {clubId, curPage, pageCount });
        }

    }

    public void SetGongGaoReflash()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetGongGao, new object[] { clubId });
    }
    public void SetMatchReflash()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetClubMatch, new object[] { clubId, 1, 100 });
    }

    public void GetGongGaoCallBack(Hashtable data) {
        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);
        tip1.SetActive(false);
        
        ArrayList List = data["GongGaoList"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
            tip1.SetActive(true);
            return;
        }
        for (int i = 0; i < List.Count; i++)
        {

            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/AnnouncementItem")) as GameObject;

            var listv = obj.GetComponent<AnnouncementItem>();
            listv.transform.parent =announcementContent;
            listv.transform.localScale = new Vector2(1, 1);

			string url = "";

			if (ht.ContainsKey ("iconUrl")) {
				url = ht ["iconUrl"].ToString ();
			}

            //赋值
			listv.SetInfo(ht["title"].ToString(), ht["time"].ToString(), ht["content"].ToString(), ht["id"].ToString(), url);


        }


    }

    public void GetMatchCallBack(Hashtable data) {
        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);

        tip2.SetActive(false);

        ArrayList List = data["gameInfo"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
            tip2.SetActive(true);
            return;
        }
        for (int i = 0; i < List.Count; i++)
        {

            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/RoomItem")) as GameObject;

            var listv = obj.GetComponent<RoomItem>();
            listv.transform.SetParent(MatchContent);
            listv.transform.localScale = new Vector2(1, 1);

            //赋值
			listv.SetInfo(ht["roomId"].ToString(), ht["url"].ToString(), ht["deskName"].ToString(), ht["name"].ToString(), 
				ht["chouma"].ToString(), ht["roomPeopleCount"].ToString(), ht["time"].ToString(), 
				ht["state"].ToString(),ht.ContainsKey("clubName")?ht["clubName"].ToString():"", int.Parse(ht["insurance"].ToString())
				, int.Parse(ht["gameType"].ToString()));


        }
    }

    public void GetClubRecordCallBack(Hashtable data) {

        if (data["message"].ToString() == "暂无数据") {
            return;
        }

        sumPage = int.Parse(data["allPage"].ToString());
        if (curPage == 1)
        {
            BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
          
        }

       

        tip3.SetActive(false);

        ArrayList List = data["GameRecordLog"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
            tip3.SetActive(true);
            return;
        }
        for (int i = 0; i < List.Count; i++)
        {

            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/ClubRecordItem")) as GameObject;

            var listv = obj.GetComponent<ClubRecordItem>();
            listv.transform.SetParent(RecordContent) ;
            listv.transform.localScale = new Vector2(1, 1);

            //赋值
            listv.SetInfo(ht["id"].ToString(), ht["url"].ToString(), ht["deskName"].ToString(), ht["chouma"].ToString(), ht["time"].ToString(), ht["date"].ToString(), ht["score"].ToString(), "");


        }

    }

    public void showRed()
    {
        if(redImage == null) return;   
        redImage.gameObject.SetActive(false);
        if (int.Parse(StaticData.MyMessage) > 0)
        {
            redImage.gameObject.SetActive(true);
        }
        if (int.Parse(StaticData.PaiMessage) > 0)
        {
            redImage.gameObject.SetActive(true);
        }
        if (int.Parse(StaticData.SysMessage) > 0)
        {
            redImage.gameObject.SetActive(true);
        }
    }

}