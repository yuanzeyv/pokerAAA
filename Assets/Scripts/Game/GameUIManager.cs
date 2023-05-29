using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// using DragonBones;
using DG.Tweening;
using System.Collections.Generic;

public class VoiceMeta {
    public string userID;
    public string voicePath;
    public int vocieTime;
}

public class GameUIManager : MonoBehaviour {

    public static GameUIManager _instance;
    public UnityEngine.Transform playerHeadTrans;
    public UnityEngine.Transform backGameTrans;
    public Button backGamebtn;
    public UnityEngine.Transform roomNumSitActivePlayerTrans;
    public UnityEngine.Transform mineButtomSeatTrans;// 底部自己UI
    public UnityEngine.Transform dichizhuma;
    public UnityEngine.Transform dichizhuma_ani_destination;
    public UnityEngine.Transform loadingTransfrom;
    public MyController _myController;
    public myjifeipai _mjifenpai;
    public Controlbtns controlbtns;
    public PopupTopleftPanel ptopleftpanel;

    public RectTransform containerBottom;
    public RectTransform containerTop;

    public PlaneManager planeManager;

    public ManagerSetPopup managerSetPopup;
    public PlayerManageListPopup playerManageListPopup;
    public PlayerManagePopup playerManagePopup;
    public PlayerInfoPopup playerInfoPopup;
    public BiaoqingPanel biaoqingPanel;
    public MatchInfoPopup matchInfoPopup;
	public GameSharePopup gameSharePopup;
    public ChatPanel chatPanel;

    public PaiMsgTopPanel paiMsgTopPanel;
    public NowRecordPanel nowRecordPanel;
    public GameReviewPanel gameReviewPanel;
    public InsurancePanel insurancePanel;
    public OtherPanel otherPanel;
    public InsuranceRulePopup insuranceRulePopup;
    public CardTypeIntroducePopup cardTypeIntroducePopup;
    public RoomSetPopup roomSetPopup;
    public GameObject shopInGame;
    public GuessHandPopup guessHandPopup;
    public GuessRecordPanel guessRecordPanel;
    public InsuranceInfo insuranceInfoPopup;
    public PopupReplay popupReplay;
    public SelectPayPopup selectPayPopup;
    public RotateLight rlight;
    public TousuPanel tousuPanel;

    public Button btnBiaoqing;
    public Button btnChat;
    public Button guessButton;
    public Button btnNowRecord;
    public Button btndashang; 
    public Text textdashang;
    Button btnBgClose;
	public Text chouma;//房间筹码
	public Text time;//房间剩余时间
	public Text club;//房间名字
	public Text playerName;//ID
    private Text insurance;//保险
    private Text txtServiceFee;  //服务费 //add by yang yang 2021年3月22日 15:22:04

	public string strplayercount;

    private Text Dici;//底池
    private Text dichizhumaText;//底池筹码

    private Text delayMoney;
    private Text paiXing;//牌型

    public UnityEngine.Transform DairuTime;
    public Text dairuTimetext;
    public float timer = 200;
    public float timerConst = 180;
    public bool isStart = false;
    Bianchi[] mbianchi;

	private Button selectBtn;
	public SelectPanel selectPanel;

	public int gameType;
	public Image gameTypeImage;

    public Image tip_face;
    public GameObject tip_chat;
    public PlayVoice playVoice;
    bool voicePlaying = false;
    Queue<VoiceMeta> voiceQueue = new Queue<VoiceMeta>();

    bool isTuoGuan;
    
    private string[] emojiNames = new string[11]{
        "jiguanqiang", "shayu", "bianbian", "pijiu", "dianzan", "zhadan",
        "zhuoji", "xiaoyu", "hongchun", "motou", "guzhang",
    };

    private Vector3 m_vFromPos;
    private Vector3 m_vToPos;

    string strSendID;
    string strReceiveID;

    public bool isAdmin; // 是局头?
    public bool delaySeeCard; // 延迟看牌?


    //end

    public static GameUIManager GetSingleton()
    {
        return _instance;
    }


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
       
        backGameTrans = transform.Find("outofGame");
        backGamebtn = backGameTrans.Find("back").GetComponent<Button>();
        backGamebtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            if(!isTuoGuan)
                 NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerReturnRequest,new object[] { });
            else
                NetMngr.GetSingleton().Send(InterfaceGame.tuoGuan, new object[] { false });
        });
        btndashang = transform.Find("dashang").GetComponent<Button>();
        btndashang.gameObject.SetActive(false);
        btndashang.onClick.AddListener(delegate {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.showLeftCardsM,new object[] { });
            btndashang.gameObject.SetActive(false);
        });
        textdashang= transform.Find("dashang/Text").GetComponent<Text>();
        rlight = transform.Find("Light").GetComponent<RotateLight>();
        backGameTrans.gameObject.SetActive(false);
        containerBottom = transform.Find("ContainerBottom").GetComponent<RectTransform>();
        containerTop = transform.Find("ContainerTop").GetComponent<RectTransform>();
        planeManager = GetComponent<PlaneManager>();
        planeManager.Init(containerBottom, containerTop);
        planeManager.movePosition = 2f;
        planeManager.topPlaneMoveTime = 0.4f;
        planeManager.sidePlaneMoveTime = 0.4f;
        nowRecordPanel = containerTop.Find("NowRecordPanel").GetComponent<NowRecordPanel>();
        gameReviewPanel = containerTop.Find("GameReviewPanel").GetComponent<GameReviewPanel>();
        insurancePanel = transform.Find("InsurancePanel").GetComponent<InsurancePanel>();
        otherPanel = containerTop.Find("OtherPanel").GetComponent<OtherPanel>();
        insuranceRulePopup= transform.Find("InsuranceRulePopup").GetComponent<InsuranceRulePopup>();
        cardTypeIntroducePopup =transform.Find("CardTypeIntroducePopup").GetComponent<CardTypeIntroducePopup>();
        roomSetPopup= transform.Find("RoomSetPopup").GetComponent<RoomSetPopup>();
        shopInGame = transform.Find("ShopInGame").gameObject;
        guessHandPopup = transform.Find("GuessHandPopup").GetComponent<GuessHandPopup>();
        guessRecordPanel= transform.Find("GuessRecordPanel").GetComponent<GuessRecordPanel>();
        selectPayPopup = transform.Find("SelectPayPopup").GetComponent<SelectPayPopup>();
        guessButton = transform.Find("Controlbtns/guessBtn").GetComponent<Button>();
        btnNowRecord = transform.Find("Controlbtns/BtnNowRecord").GetComponent<Button>();
        btnChat = transform.Find("Controlbtns/audio").GetComponent<Button>();
        btnBiaoqing = transform.Find("Controlbtns/chat").GetComponent<Button>();
        tousuPanel = transform.Find("TousuPanel").GetComponent<TousuPanel>();
        

        tip_face = transform.Find("Tip_face").GetComponent<Image>();
        tip_chat = transform.Find("Tip_chat").gameObject;
        playVoice = transform.Find("playVoice").GetComponent<PlayVoice>();
        playVoice.Hide();

        loadingTransfrom = transform.Find("Loading");
        playerHeadTrans = transform.Find("playerHead");
        mineButtomSeatTrans = playerHeadTrans.Find("0");
        btnBgClose = transform.Find("bg1").GetComponent<Button>();
      
        _myController = transform.Find("mycontrol").GetComponent<MyController>();
        controlbtns = transform.Find("Controlbtns").GetComponent<Controlbtns>();
        ptopleftpanel = transform.Find("LeftTopPopupup").GetComponent<PopupTopleftPanel>();
        managerSetPopup = transform.Find("ManagerSetPopup").GetComponent<ManagerSetPopup>();
        playerManageListPopup = transform.Find("PlayerManageListPopup").GetComponent<PlayerManageListPopup>();
        playerManagePopup = transform.Find("PlayerManagePopup").GetComponent<PlayerManagePopup>();
        playerInfoPopup = transform.Find("PlayerInfoPopup").GetComponent<PlayerInfoPopup>();
        biaoqingPanel = transform.Find("BiaoqingPanel").GetComponent<BiaoqingPanel>();
        chatPanel = transform.Find("ChatPanel").GetComponent<ChatPanel>();
        matchInfoPopup = transform.Find("MatchInfoPopup").GetComponent<MatchInfoPopup>();
		gameSharePopup = transform.Find("GameSharePopup").GetComponent<GameSharePopup>();
		paiMsgTopPanel = transform.Find("PaiMsgTopPanel").GetComponent<PaiMsgTopPanel>();
        insuranceInfoPopup = transform.Find("InsuranceInfo").GetComponent<InsuranceInfo>();
        insuranceInfoPopup.gameObject.SetActive(false);
        managerSetPopup.gameObject.SetActive(false);
        playerManageListPopup.gameObject.SetActive(false);
        playerManagePopup.gameObject.SetActive(false);
        playerInfoPopup.gameObject.SetActive(false);
        //biaoqingPanel.gameObject.SetActive(false);
        //chatPanel.gameObject.SetActive(false);
        matchInfoPopup.gameObject.SetActive(false);
        selectPayPopup.gameObject.SetActive(false);
		gameSharePopup.gameObject.SetActive (false);
		paiMsgTopPanel.gameObject.SetActive (false);
        tousuPanel.gameObject.SetActive(false);
  //      shopInGame.gameObject.transform.GetChild(1).gameObject.SetActive(true);  //edit by yang yang 2021年4月16日 15:30:32
        
        ptopleftpanel.gameObject.SetActive(false);
        chouma = transform.Find("BgInfo/BG/Chouma/Text").GetComponent<Text>();
   //     time = transform.Find("BgInfo/BG/Time/Text").GetComponent<Text>();
        club = transform.Find("BgInfo/Club").GetComponent<Text>();
        playerName = transform.Find("BgInfo/Name").GetComponent<Text>();
        insurance = transform.Find("BgInfo/Insurance").GetComponent<Text>();
        txtServiceFee = transform.Find("BgInfo/ServiceFee").GetComponent<Text>();
        Dici = transform.Find("Dichi/count").GetComponent<Text>();
        delayMoney = transform.Find("mycontrol/myturn/Text").GetComponent<Text>();
        paiXing = transform.Find("Paixing").GetComponent<Text>();
        paiXing.gameObject.SetActive(false);
        dichizhuma = transform.Find("Dichi/dichizhuma");
        dichizhuma_ani_destination = dichizhuma.Find("value");
        dichizhumaText = dichizhuma.Find("value").GetComponent<Text>();
        _mjifenpai = transform.Find("jifeipai").GetComponent<myjifeipai>();
        DairuTime = transform.Find("DaiRuTime");
        dairuTimetext = DairuTime.transform.Find("Text").GetComponent<Text>();
        DairuTime.gameObject.SetActive(false);
        _mjifenpai.gameObject.SetActive(false);
        Dici.gameObject.SetActive(false);
        dichizhuma.gameObject.SetActive(false);
        nowRecordPanel.gameObject.SetActive(false);
        shopInGame.gameObject.SetActive(false);
        otherPanel.gameObject.SetActive(false);
        btnBgClose.onClick.AddListener(() => {
            // SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (ptopleftpanel.gameObject.activeInHierarchy)
                ptopleftpanel.gameObject.SetActive(false);
            if (_myController.addtransfrom.gameObject.activeInHierarchy)
            {
                _myController.setAddCancel();
            }
        });
        popupReplay = transform.Find("PopupReplay").GetComponent<PopupReplay>();
        popupReplay.gameObject.SetActive(false);
        // Test 
        // roomNumSitActivePlayerTrans = playerHeadTrans.Find("9Persion");
        guessButton.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            guessHandPopup.gameObject.SetActive(true);
            // GameUIManager.GetSingleton().guessRecordPanel.gameObject.SetActive(true);
            TouchMove.Instance().isRun = false;
        });
        //add by yang yang 2021年3月16日 19:49:24
        btnNowRecord.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            planeManager.AddSidePlane(nowRecordPanel, containerTop, SidePlaneDirection.LEFT, 508);
         //   nowRecordPanel.gameObject.SetActive(true);
         //   TouchMove.Instance().isRun = false;
        });

        btnChat.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            planeManager.AddSidePlane(chatPanel, containerTop, SidePlaneDirection.LEFT, 517);
            NetMngr.GetSingleton().Send(InterfaceGame.queryChatDiamond, new object[] {});
        });

        btnBiaoqing.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            biaoqingPanel.ShowView();
        });
        //end
        //cheat
        selectBtn = transform.Find("SelectButton").GetComponent<Button>();
		selectBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			NetMngr.GetSingleton().Send(InterfaceGame.SelectCard, new object[] { });
		});

		selectPanel = transform.Find("SelectPanel").GetComponent<SelectPanel>();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

		gameTypeImage = transform.Find("bg/Image").GetComponent<Image>();


    }

    void Start() {
        mbianchi = transform.Find("Dichi").GetComponentsInChildren<Bianchi>();
        for (int j = 0; j < mbianchi.Length; j++)
        {
         //   Debug.Log(mbianchi[j].gameObject.name + "===");
            mbianchi[j].hideGo();
        }
        if (StaticData.isReplay)
        {
            Waitting.getInstance().Hide();
            popupReplay.gameObject.SetActive(true);
            return;
        }
        Waitting.getInstance().Show();
        NetMngr.GetSingleton().Send(InterfaceGame.sendtotal, new object[]{});
        TouchMove.Instance().AddFunction(TouchMove.ActionType.Right, LeftPanel);
        TouchMove.Instance().AddFunction(TouchMove.ActionType.Left, RightPanel);
        ShowBar.statusBarState = ShowBar.States.Hidden;

        
    }
    public  void hideBianchi()
    {
        for (int j = 0; j < mbianchi.Length; j++)
        {
          //  Debug.Log(mbianchi[j].gameObject.name+"===");
            mbianchi[j].hideGo();
        }
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            //StartCoroutine(showTimeOnDesk("5"));
            NetMngr.GetSingleton().Send(InterfaceGame.getGameReview,new object[] { "5444" });
        }
        if (isStart)
        {
            timer -= Time.deltaTime;
            dairuTimetext.text = ((int)timer).ToString();
            if (timer<=0)
            {
                isStart = false;
                timer = 180;
            }
        }
        if(!voicePlaying && voiceQueue.Count > 0){ // 播放声音
            VoiceMeta data = (VoiceMeta)voiceQueue.Dequeue();
            string userID = data.userID;
            string voicePath = data.voicePath;
            int voiceTime = data.vocieTime;
            StartCoroutine(doPlayVoice(userID, voicePath, voiceTime));
        }
    }
    //  b  是否等待授权
    public void WaitForAccess(Hashtable h,bool b)
    {
        if (h["netID"].ToString() == "") return;
        int temp = GameManager.GetSingleton().netTolocal(int.Parse(h["netID"].ToString()));
        if(b)
        roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().KeepWaitForAccess();
        else
        {
            roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().BackToAccess();
        }
    }
    // 其他玩家授权状态
    public void OthersWaitForAccessState(Hashtable h)
    {
        if (h.Contains("netID"))
        {
            if (h["netID"].ToString() == "") return;
            int temp = GameManager.GetSingleton().netTolocal(int.Parse(h["netID"].ToString()));
            string type = h["type"].ToString();
            if (type == "0")// 等待授权
                roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().KeepWaitForAccess();
            else
            {
                roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().BackToAccess();
            }
        }
       
    }
    //8-7 显示还原头像
    public void RecoerHeadImage(int temp)
    {
        roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().setHeadDark(false);
    }
    public void showBianchi(Hashtable h)
    {
        Debug.Log("Fuck.................................=========");
        ArrayList al = h["potCoin"] as ArrayList;
        this.hideBianchi();
        for(int i = 0; i < al.Count; i++)
        {
            mbianchi[i].showMsg(al[i].ToString());
        }
    }
    public void showDashang(string msg)
    {
        btndashang.gameObject.SetActive(true);
        textdashang.text = msg;
    }
    public void LeftPanel()
    {
        planeManager.AddSidePlane(nowRecordPanel, containerTop, SidePlaneDirection.LEFT, 508);
    }
    public void RightPanel()
    {
       // planeManager.AddSidePlane(otherPanel, containerTop, SidePlaneDirection.RIGHT, 174);
    }
    public void setPaixingText(string s)
    {
        paiXing.gameObject.SetActive(true);
        paiXing.text = s;
    }
    public void gameStartResetData()
    {
        Dici.gameObject.SetActive(false);
        dichizhuma.gameObject.SetActive(false);
        btndashang.gameObject.SetActive(false);
        paiXing.gameObject.SetActive(false);
        SetDelayMoney(orignDelayMoney);
        _myController.curTurn = 0;
    }
    public void setMyCardsType(Hashtable h)
    {

    }
    public void RoomOwnerclickStartBtn()
    {
        _myController.setTips(false);
    }
    public void GameStart(Hashtable h)
    {
        GameManager.GetSingleton().isGetGonggongPai = false;
        Paicontrol.GetInstance().MfaPai(h);
    }
    /// <summary>
    /// 设置桌布信息
    /// </summary>
    /// <param name="data"></param>
    public void SetBgInfo(Hashtable data)
    {
    }
    /// <summary>
    /// 设置底池
    /// </summary>
    /// <param name="data"></param>
    public void SetDichi(Hashtable data)
    {
        //Debug.Log(data["potCoin"].ToString()+"===");
        Dici.gameObject.SetActive(true);
        Dici.text = "底池:"+data["potCoin"].ToString();
        GameManager.GetSingleton().gameDichi = int.Parse(data["potCoin"].ToString());
        // 6-26 需求
       // this.setZhuamaDichi();
    }
    public void set627Dici(Hashtable h)
    {
        dichizhuma.gameObject.SetActive(true);
        dichizhumaText.text = h["money"].ToString();
    }
    public void setZhuamaDichi()
    {
        dichizhuma.gameObject.SetActive(true);
        dichizhumaText.text = Dici.text.Split(':')[1];
    }
    public void setDelayTime(Hashtable h)
    {
        SetDelayMoney(h["delayMoney"].ToString());
        _myController.AddDelayTime(int.Parse(h["time"].ToString()));
    }
    public void SetDelayMoney(string value)
    {
        delayMoney.text = value;
    }
    
    public void setPlayinfo(int i,Hashtable h)
    {
        Debug.Log("设置头像");
        roomNumSitActivePlayerTrans.GetChild(i).gameObject.SetActive(true);
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().setPlayinfo(h);
    }
    public void setAlreadySeatPlayinfo(int i, string h)
    {
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().setAlreadySeatPlayinfo(h);
    }
    public void setAlreadySeatPlayinfoPai(int i, string h)
    {
		if (i == GameManager.GetSingleton().myNetID)
            return;
        Paicontrol.GetInstance().PlayerAlreadyOnDeskFapai(roomNumSitActivePlayerTrans.GetChild(i));
    }
    // 某人延长时间
    public void SomeOneAddTime(Hashtable hh)
    {
        int temp = GameManager.GetSingleton().netTolocal(int.Parse(hh["netID"].ToString()));
        float time = float.Parse(hh["time"].ToString());
        roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().Daojishi_Start(time);
    }


    public void ResetAllPlayerInfo()
    {
        for(int j=0;j< roomNumSitActivePlayerTrans.childCount; j++)
        {
            roomNumSitActivePlayerTrans.GetChild(j).GetComponent<PlayInfo>().RestUIPlayer();
        }
    }
    public void setAllSeatEmpty()
    {
        for(int i=0;i< roomNumSitActivePlayerTrans.childCount; i++)
        {
            roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().oneLeaveSeat();
            roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().OneGameStartResetUI(false);
            Paicontrol.GetInstance().RecoveryPersonPai(roomNumSitActivePlayerTrans.GetChild(i));
        }
    }
    //某人离开（站起围观）除我之外
    public void setOneSeatEmpty(int i, Hashtable h=null)
    {
        // 如果我是观战，仍然需要显示“入座”
        if (StaticData.isGuanzhan)
            roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().oneLeaveSeat();
        //  我是入座玩家，直接隐藏位置即可
        else
            roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().gameObject.SetActive(false);
        // 清空牌
        Paicontrol.GetInstance().RecoveryPersonPai(roomNumSitActivePlayerTrans.GetChild(i));
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().OneGameStartResetUI(false);
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().userID = string.Empty;
    }
    // 玩家暂离(托管处理方式类似)
    public void setOneSeatKeey(int i, int h)
    {
        isTuoGuan = false;
		if (i == GameManager.GetSingleton().myNetID && !StaticData.isGuanzhan)
        {
            // 自己显示“返回”按钮
            backGameTrans.gameObject.SetActive(true);
        }
        // 显示倒计时
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().KeepSeat(h);
    }
    public void BackToSeat(int i)
    {
		if (i == GameManager.GetSingleton().myNetID && !StaticData.isGuanzhan)
        {
            // 自己显示“返回”按钮
            backGameTrans.gameObject.SetActive(false);
        }
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().BackToSeat();
    }
    public void TuoGuan(int i,bool b)
    {
        isTuoGuan = true;
		if (i == GameManager.GetSingleton().myNetID && !StaticData.isGuanzhan)
        {
            // 自己显示“返回”按钮
            backGameTrans.gameObject.SetActive(b);
           // GameUIManager.GetSingleton()._myController.ResetUIButton();
        }
        //else
        //{
        //    backGameTrans.gameObject.SetActive(false);
        //}
        // 托管
        roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>().setTuoGuan(b);
    }
    public  void  setZhuanginfo(int i)
    {
        for(int j=0;j< roomNumSitActivePlayerTrans.childCount; j++)
        {
            if(i==j)
                roomNumSitActivePlayerTrans.GetChild(j).GetComponent<PlayInfo>().setZhuangInfo(true);
            else
                roomNumSitActivePlayerTrans.GetChild(j).GetComponent<PlayInfo>().setZhuangInfo(false);
        }
    }
    public void hideAllSeatHeadImg()
    {
        for (int j = 0; j < roomNumSitActivePlayerTrans.childCount; j++)
        {
            roomNumSitActivePlayerTrans.GetChild(j).gameObject.SetActive(false);
        }
    }
    // rinfo
    string orignDelayMoney = "";
    public void setRoomInfo(Hashtable data)
    {
        gameType = int.Parse(data["gameType"].ToString());
        if (gameType == 2)
        {
            insuranceRulePopup = transform.Find("InsuranceRulePopup2").GetComponent<InsuranceRulePopup>();
        }
        else
        {
            insuranceRulePopup = transform.Find("InsuranceRulePopup").GetComponent<InsuranceRulePopup>();
        }
        gameTypeImage.sprite = Resources.Load<Sprite> ("img/gametype_" + gameType.ToString());

		Paicontrol.GetInstance ().setMyArea (gameType);
        isAdmin = data["isAdmin"].ToString() == "1" ? true : false;

        if (data.Contains("delayMoney"))
        {
            orignDelayMoney = data["delayMoney"].ToString();
            SetDelayMoney(data["delayMoney"].ToString());
        }
		strplayercount = data ["roomPlaycount"].ToString ();
        roomNumSitActivePlayerTrans = playerHeadTrans.Find(data["roomPlaycount"].ToString()+"Persion");
        
        for(int i=0;i< playerHeadTrans.childCount; i++)
        {
            if (i == (int.Parse(data["roomPlaycount"].ToString()) - 2))
            {
                playerHeadTrans.GetChild(i).gameObject.SetActive(true);
            }else
                playerHeadTrans.GetChild(i).gameObject.SetActive(false);
        }
        for(int j = 0; j < roomNumSitActivePlayerTrans.childCount; j++)
        {
            rlight.vectorss.Add(roomNumSitActivePlayerTrans.GetChild(j));
        }
        chouma.text = GameManager.GetSingleton().roomXiaoMang+"/"+GameManager.GetSingleton().roomDaMang;
        if (GameManager.GetSingleton().straddle > 0)
        {
            chouma.text = chouma.text + "/" + GameManager.GetSingleton().straddle;
        }

        if (GameManager.GetSingleton().ante > 0)
        {
            chouma.text = chouma.text + "(" + GameManager.GetSingleton().ante + ")";
        }
        int temp = int.Parse(data["time"].ToString());
        float h = Mathf.FloorToInt(temp / 3600f);
        float m = Mathf.FloorToInt(temp / 60f - h * 60f);
        float s = Mathf.FloorToInt(temp - m * 60f - h * 3600f);
    //    time.text = h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
        club.text = "[" + data["roomName"].ToString() + "]";


		if (data ["clubName"].ToString () == "") {
			//显示分享按钮
			controlbtns.share.gameObject.SetActive(false);
			controlbtns.msg.gameObject.SetActive(false);
			playerName.text = "(" + data ["roomId"].ToString () + ")";
		} else {
			//显示消息按钮
			controlbtns.share.gameObject.SetActive(false);
			controlbtns.msg.gameObject.SetActive(true);
			playerName.text = "(" + data["clubName"].ToString() + ")";
		}
        insurance.text = data["isInsurance"].ToString() == "1" ? "保险:开启" : "保险:关闭";
        if(int.Parse(data["fee"].ToString()) > 0 )
        {
            txtServiceFee.text = txtServiceFee.text + data["fee"].ToString() + "%"; //txtServiceFee.text + "3%"; //add by yang yang 2021年3月22日 15:23:49
        }
        else
        {
            txtServiceFee.gameObject.SetActive(false);
        }
        //edit by yang yang 2021年3月16日 16:41:20
        if(cor!=null)
        StopCoroutine(cor);
        bool bDaojishi = data["roomState"].ToString() == "0" ? false : true;
        cor = StartCoroutine(showTimeOnDesk(data["time"].ToString(), bDaojishi));
    
        Debug.Log("cor..."+ data["showBtnOrTips"].ToString());
        //end


        switch (data["showBtnOrTips"].ToString())
        {
            case "0":
                _myController.startGameShow(true);
                _myController.setTips(false);
                break;
            case "1":
                _myController.startGameShow(false);
                _myController.setTips(true);
                break;
            case "2":
                _myController.startGameShow(false);
                _myController.setTips(false);
                break;
        }

        delaySeeCard = data["delaySeeCard"].ToString() == "1" ? true : false;
    }
    Coroutine cor;
    public void setNewTime(Hashtable data)
    {
        Debug.Log("设置定时器UIManager---setNewTime(Hashtable data)");
        if (data.Contains("delayTime"))
            GameManager.GetSingleton().everyDelayTime = int.Parse(data["delayTime"].ToString());

        if (cor != null)
            StopCoroutine(cor);

        bool bDaojishi = data["roomState"].ToString() == "0" ? false : true;
        cor = StartCoroutine(showTimeOnDesk(data["time"].ToString(), bDaojishi));
    }
    IEnumerator showTimeOnDesk(string time1, bool bDaojishi)
    {
        int temo = int.Parse(time1);
        if (!bDaojishi)//倒计时用
        {
            float h = Mathf.FloorToInt(temo / 3600f);
            float m = Mathf.FloorToInt(temo / 60f - h * 60f);
            float s = Mathf.FloorToInt(temo - m * 60f - h * 3600f);

         //   time.text = h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
         //   add by yang yang 2021年3月16日 17:00:20
             if (nowRecordPanel.gameObject.activeSelf)
             {
                nowRecordPanel.textCountDown.text = h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
            }
         //   end
        }
        else
        {
            while (temo > 0)
            {
                yield return new WaitForSeconds(1);
                temo--;
                float h = Mathf.FloorToInt(temo / 3600f);
                float m = Mathf.FloorToInt(temo / 60f - h * 60f);
                float s = Mathf.FloorToInt(temo - m * 60f - h * 3600f);

          //      time.text = h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
                if (nowRecordPanel.gameObject.activeSelf)
                {
                    nowRecordPanel.textCountDown.text = h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
               }
            }
        }

       
    }
    public void HideBtnAndTips()
    {
        _myController.startGameShow(false);
        _myController.setTips(false);
        
    }
    public void loadImageFromURL(string url, Image headImg)
    {
        StartCoroutine(url,headImg);
    }
    public void OnSomeOneTurn(Hashtable h)
    {

    }
    public void someOneMaiPai(Hashtable h) // netid
    {
        int netid = int.Parse(h["netid"].ToString());
        int localid = GameManager.GetSingleton().netTolocal(netid);
        PlayInfo pinfo = roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>();
        pinfo.showOperaModel("maipai");
    }
    // 某人下注（我操作的时候，别人“弃牌”也推给我！！！这个时候不能隐藏操作界面）
    public void somneOneXiaZhu(Hashtable h) {
        string coin = h["leftMoney"].ToString();
        string zhunma = h["zhuma"].ToString();
        int netid = int.Parse(h["netID"].ToString());
        string tempmode = h["opratorMode"].ToString();
        int localid = GameManager.GetSingleton().netTolocal(netid);
        if (!StaticData.isGuanzhan)
        {
			if (netid == GameManager.GetSingleton().myNetID)
            {
				GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>().showHeadImage(true);
                GameManager.GetSingleton().myDeskLeftmoney = int.Parse(coin);
                _myController.mytrunTransform.gameObject.SetActive(false);
                _myController.nomytrunTransform.gameObject.SetActive(false);
                _myController.startRunTime = false;
                // 我弃牌
                //    if (tempmode == "0")
                //    {
                //        _myController.mytrunTransform.gameObject.SetActive(false);
                //        _myController.nomytrunTransform.gameObject.SetActive(false);
                //    }
            }
        }
        if (h.Contains("isGameOver"))
        {
            if (h["isGameOver"].ToString() == "1")
            {
                _myController.mytrunTransform.gameObject.SetActive(false);
                _myController.nomytrunTransform.gameObject.SetActive(false);
            }
        }
    
        PlayInfo pinfo = roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>();
        pinfo.setLeftMoney(coin);
        pinfo.SetNewChouma(zhunma);
        pinfo.Daojishi_Stop();

        // 我（其他人）弃牌动画
        if (tempmode == "0")
        {
            string tem = h["isQiPaiAndShowPai"].ToString();
            //  Paicontrol.GetInstance().QiPaiAni(localid, tem);   //edit by yang yang 2021年3月16日 13:22:11
            Debug.Log("弃牌，暂停动画，置灰！！！！");
            Paicontrol.GetInstance().QiPaiDark(localid, true);
            //end

            // 头像设置灰色
            pinfo.setHeadDark();
            SoundManager.GetSingleton().PlaySound("Audio/qipai");
            pinfo.SetNewChouma("");
        }
        if (h.Contains("dizhu"))
           Dici.text = h["dizhu"].ToString();
        if (h["zhuatou"].ToString() == "1")
        {
            pinfo.showZhuaTou();
        }
        pinfo.showOperaModel(tempmode);
        // "积分飞"动画
        if (tempmode!="0" && tempmode !="3")
        Paicontrol.GetInstance().XiaZhuAni(pinfo);
        if (tempmode == "4")
        {
            SoundManager.GetSingleton().PlaySound("Audio/allin");
            pinfo.setAllin(true);
        }
        // 
        switch (tempmode)
        {
            // jiazhu
            case "1": SoundManager.GetSingleton().PlaySound("Audio/jiazhu"); break;
            // genzhu
            case "2": SoundManager.GetSingleton().PlaySound("Audio/genzhu"); break;
            // genzhu
            case "3": SoundManager.GetSingleton().PlaySound("Audio/rangpai"); break;
            // qianzhu
            case "5": SoundManager.GetSingleton().PlaySound("Audio/qianzhu"); break;
            case "6": SoundManager.GetSingleton().PlaySound("Audio/daxiaomang"); break;
        }
           
    }
    IEnumerator LoadImage(string url,Image headImg)
    {
        if (url != "")
        {
            WWW www = new WWW(url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            }
            headImg.overrideSprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

	public void onSelectCard(Hashtable h)
	{

		if (!h.ContainsKey ("state"))
			return;

		if (h ["state"].ToString () == "1") {
			selectPanel.gameObject.SetActive (true);
			//几张牌
			int count = int.Parse (h ["impose"].ToString());
			//剩余牌显示
			selectPanel.showCards (count, h ["cards"].ToString());

		} else {
			selectPanel.gameObject.SetActive (false);
		}
	}
    private IEnumerator playChat(string strContent)
    {
        yield return new WaitForEndOfFrame();
        string sendName = "";
        for (int i = 0; i < roomNumSitActivePlayerTrans.childCount; i++)
        {
            PlayInfo pInfo = roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>();
            if (pInfo == null)
            {
                continue;
            }
            if (pInfo.userID == strSendID)
            {
                sendName = pInfo.playerName.text + ":";
                break;
            }
        }
        tip_chat.SetActive(true);
        Text text = tip_chat.GetComponentInChildren<Text>();
        text.text = sendName + strContent;
        float y = Random.Range(-300, 300);
        tip_chat.transform.localPosition = new Vector3(750, y, 0);
        tip_chat.transform.DOLocalMoveX(-1000, 6f).OnComplete(() => { tip_chat.gameObject.SetActive(false); });
    }

    private IEnumerator playFace(string strContent)
    {
        yield return new WaitForEndOfFrame();
        bool isAtPos = false;
        for (int i = 0; i < roomNumSitActivePlayerTrans.childCount; i++)
        {
            PlayInfo pInfo = roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>();
            if (pInfo == null)
            {
                continue;
            }
            if (pInfo.userID == strSendID)
            {
                m_vFromPos = pInfo.transform.localPosition;
                isAtPos = true;
            }
        }
        if (isAtPos)
        {
            tip_face.sprite = Resources.Load<Sprite>("biaoqing/" + strContent); ;
            tip_face.gameObject.SetActive(true);
            tip_face.transform.localPosition = m_vFromPos;
            yield return new WaitForSeconds(3);
            tip_face.gameObject.SetActive(false);
        }

    }


    //播放表情动画
    public void gamePlayAnim(string strType){
        StartCoroutine(MoveAction(strType));
    }

    private IEnumerator MoveAction(string strType){
        yield return new WaitForEndOfFrame();
        string name = emojiNames[int.Parse(strType)-1];

        for (int i = 0; i < roomNumSitActivePlayerTrans.childCount; i++)
        {
            PlayInfo pInfo = roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>();
            if(pInfo == null)
            {
                continue;
            }
            if (i == int.Parse(strSendID))
            {
                m_vFromPos = pInfo.transform.localPosition;
            }

            if (i == int.Parse(strReceiveID))
                m_vToPos = pInfo.transform.localPosition;
        }
        float angle = Vector3.Angle(m_vToPos - m_vFromPos, transform.right);
        string dir = m_vFromPos.x < m_vToPos.x ? "right" : "left";

        SoundManager.GetSingleton().PlaySound("Audio/Sound/Animoji/" + name);
        
        switch(name){
            case "jiguanqiang":
            {
                GameObject fromobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/from", name))) as GameObject;
                fromobj.transform.SetParent(transform);
                fromobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                fromobj.SetActive(false);
                GameObject pointobj = fromobj.transform.Find("point").gameObject;
                
                GameObject toobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/to", name))) as GameObject;
                toobj.transform.SetParent(transform);
                toobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                toobj.SetActive(false);

                GameObject flyobj1 = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj1.transform.SetParent(transform);
                flyobj1.transform.localScale = new Vector3(1f, 1f, 1);
                flyobj1.transform.rotation = Quaternion.Euler(0, 0, angle);
                flyobj1.SetActive(false);

                GameObject flyobj2 = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj2.transform.SetParent(transform);
                flyobj2.transform.localScale = new Vector3(1f, 1f, 1);
                flyobj2.transform.rotation = Quaternion.Euler(0, 0, angle);
                flyobj2.SetActive(false);   

                GameObject flyobj3 = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj3.transform.SetParent(transform);
                flyobj3.transform.localScale = new Vector3(1f, 1f, 1);
                flyobj3.transform.rotation = Quaternion.Euler(0, 0, angle);
                flyobj3.SetActive(false);
                
                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(()=>{
                    fromobj.SetActive(true);
                    fromobj.transform.localPosition = m_vFromPos;
                    fromobj.transform.rotation = Quaternion.Euler(0, 0, angle+180);
                    pointobj.transform.SetParent(playerHeadTrans);
                    Vector3 startPos = pointobj.transform.localPosition;

                    float x = startPos.x + 400 * Mathf.Cos(Mathf.Deg2Rad * angle);
                    float y = startPos.y + 400 * Mathf.Sin(Mathf.Deg2Rad * angle);
                    flyobj1.transform.localPosition = new Vector3(x, y, startPos.z);
                    flyobj2.transform.localPosition = new Vector3(x, y, startPos.z);
                    flyobj3.transform.localPosition = new Vector3(x, y, startPos.z);
                    
                });
                seq.AppendInterval(0.25f);
                seq.AppendCallback(()=>{
                    flyobj1.SetActive(true);
                    flyobj1.transform.DOLocalMove(m_vToPos, 0.5f);
                });
                seq.AppendInterval(0.25f);
                seq.AppendCallback(()=>{
                    flyobj2.SetActive(true);
                    flyobj2.transform.DOLocalMove(m_vToPos, 0.5f);
                });
                seq.AppendInterval(0.25f);
                seq.AppendCallback(()=>{
                    flyobj1.SetActive(false);
                    flyobj3.SetActive(true);
                    flyobj3.transform.DOLocalMove(m_vToPos, 0.5f);
                    toobj.SetActive(true);
                    toobj.transform.localPosition = m_vToPos;
                });
                seq.AppendInterval(0.5f); // (toobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
                seq.AppendCallback(()=>{
                    pointobj.transform.SetParent(fromobj.transform);
                    fromobj.SetActive(false);
                    toobj.SetActive(false);
                    flyobj1.SetActive(false);
                    flyobj2.SetActive(false);
                    flyobj3.SetActive(false);
                    GameObject.Destroy(fromobj);
                    GameObject.Destroy(toobj);
                    GameObject.Destroy(flyobj1);
                    GameObject.Destroy(flyobj2);
                    GameObject.Destroy(flyobj3);   
                });

                break;
            }

            case "shayu":
            {
                GameObject fromobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/from", name))) as GameObject;
                fromobj.transform.SetParent(transform);
                fromobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                fromobj.SetActive(false);
                
                GameObject toobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/to", name))) as GameObject;
                toobj.transform.SetParent(transform);
                toobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                toobj.SetActive(false);
                
                GameObject flyobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj.transform.SetParent(transform);
                flyobj.transform.localScale = new Vector3(1f, 1f, 1);
                flyobj.SetActive(false);

                GameObject killobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/kill", name))) as GameObject;
                killobj.transform.SetParent(transform);
                killobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                killobj.SetActive(false);
                                  
                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(()=>{
                    fromobj.SetActive(true);
                    fromobj.transform.localPosition = m_vFromPos;
                    toobj.SetActive(true);
                    toobj.transform.localPosition = m_vToPos;
                });
                seq.AppendInterval(fromobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
                seq.AppendCallback(()=>{
                    fromobj.SetActive(false);
                    toobj.SetActive(false);
                    flyobj.SetActive(true);
                    flyobj.transform.localPosition = m_vFromPos;
                    flyobj.transform.rotation = Quaternion.Euler(0, 0, angle);
                });
                seq.Append(flyobj.transform.DOLocalMove(m_vToPos, 0.5f));
                seq.AppendCallback(()=>{
                    flyobj.SetActive(false);
                    killobj.SetActive(true);
                    killobj.transform.localPosition = m_vToPos;   
                });
                seq.AppendInterval(killobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
                seq.AppendCallback(()=>{
                    killobj.SetActive(false);
                    GameObject.Destroy(fromobj);
                    GameObject.Destroy(toobj);
                    GameObject.Destroy(flyobj);
                    GameObject.Destroy(killobj);
                });

                break;
            }

            case "bianbian":
            case "pijiu":
            case "dianzan":
            case "zhadan":
            case "zhuoji":
            case "hongchun":
            case "motou":
            {
                GameObject flyobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj.transform.SetParent(transform);
                flyobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                flyobj.SetActive(false);
                
                GameObject toobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/to", name))) as GameObject;
                toobj.transform.SetParent(transform);
                toobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                toobj.SetActive(false);

                if(name == "motou"){
                    m_vToPos = new Vector3(m_vToPos.x, m_vToPos.y+50, m_vToPos.z);
                }

                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(()=>{
                    flyobj.SetActive(true);
                    flyobj.transform.localPosition = m_vFromPos;
                });
                seq.Append(flyobj.transform.DOLocalMove(m_vToPos, 1f));
                seq.AppendCallback(()=>{
                    flyobj.SetActive(false);
                    toobj.SetActive(true);
                    toobj.transform.localPosition = m_vToPos;
                    // if(name == "motou"){
                    //     toobj
                    // }   
                });
                seq.AppendInterval(toobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
                seq.AppendCallback(()=>{
                    toobj.SetActive(false);
                    GameObject.Destroy(flyobj);
                    GameObject.Destroy(toobj);
                });
                
                break;
            }

            case "xiaoyu":
            {   
                GameObject fromobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/from", name))) as GameObject;
                fromobj.transform.SetParent(transform);
                fromobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                fromobj.SetActive(false);
                GameObject pointobj = fromobj.transform.Find("line").gameObject;
                
                GameObject toobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/to", name))) as GameObject;
                toobj.transform.SetParent(transform);
                toobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                toobj.SetActive(false);
                
                GameObject flyobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj.transform.SetParent(transform);
                flyobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                flyobj.SetActive(false);

                GameObject lineobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/line", name))) as GameObject;
                lineobj.transform.SetParent(transform);
                lineobj.transform.localScale = new Vector3(1f, 2f, 1);
                lineobj.SetActive(false);

                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(()=>{
                    fromobj.SetActive(true);
                    fromobj.transform.localPosition = m_vFromPos;
                });
                seq.Append(fromobj.transform.DORotate(new Vector3(0, 0, 45), 0.25f));
                seq.Append(fromobj.transform.DORotate(new Vector3(0, 0, 0), 0.25f));
                seq.AppendCallback(()=>{
                    pointobj.transform.SetParent(playerHeadTrans);
                    Vector3 startPos = pointobj.transform.localPosition;
                    lineobj.SetActive(true);
                    lineobj.transform.localPosition = startPos;
                    lineobj.transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(m_vToPos - startPos, transform.right));
                    float scaleX = Vector3.Distance(startPos, m_vToPos) / 130;    
                    lineobj.transform.DOScaleX(scaleX, 0.5f);
                });
                seq.AppendInterval(0.5f);
                seq.AppendCallback(()=>{
                    pointobj.transform.SetParent(fromobj.transform);
                    fromobj.SetActive(false);
                    lineobj.SetActive(false);
                    toobj.SetActive(true);
                    toobj.transform.localPosition = m_vToPos;
                    toobj.transform.rotation = Quaternion.Euler(0, dir=="left"?180:0, 0);                 
                });
                seq.AppendInterval(toobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
                seq.AppendCallback(()=>{
                    toobj.SetActive(false);
                    flyobj.SetActive(true);
                    flyobj.transform.localPosition = m_vToPos;
                    flyobj.transform.rotation = Quaternion.Euler(0, dir=="left"?180:0, 0);                 
                });
                seq.Append(flyobj.transform.DOLocalMove(m_vFromPos, 0.5f));
                seq.AppendCallback(()=>{
                    flyobj.SetActive(false);
                    GameObject.Destroy(fromobj);
                    GameObject.Destroy(toobj);
                    GameObject.Destroy(flyobj);
                    GameObject.Destroy(lineobj);
                });

                break;
            }

            case "guzhang":
            {
                GameObject fromobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/from", name))) as GameObject;
                fromobj.transform.SetParent(transform);
                fromobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                fromobj.SetActive(false);
                
                GameObject toobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/to", name))) as GameObject;
                toobj.transform.SetParent(transform);
                toobj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                toobj.SetActive(false);
                
                GameObject flyobj = Instantiate(Resources.Load(string.Format("NewAniEmoji/{0}/fly", name))) as GameObject;
                flyobj.transform.SetParent(transform);
                flyobj.transform.localScale = new Vector3(1f, 1f, 1);
                flyobj.SetActive(false);

                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(()=>{
                    fromobj.SetActive(true);
                    fromobj.transform.localPosition = m_vFromPos;
                    flyobj.SetActive(true);
                    flyobj.transform.localPosition = m_vFromPos;
                });
                seq.Append(flyobj.transform.DOLocalMove(m_vToPos, 1f));
                seq.AppendCallback(()=>{
                    flyobj.SetActive(false);
                    toobj.SetActive(true);
                    toobj.transform.localPosition = m_vToPos;   
                });
                seq.AppendInterval(toobj.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
                seq.AppendCallback(()=>{
                    fromobj.SetActive(false);
                    toobj.SetActive(false);
                    GameObject.Destroy(fromobj);
                    GameObject.Destroy(toobj);
                    GameObject.Destroy(flyobj);
                });
                
                
                break;
            }
        }
    }


    public void setPosition(Vector3 fromPos, Vector3 toPos,string strType)
    {
        m_vFromPos = fromPos;
        m_vToPos = toPos;

        gamePlayAnim(strType);
    }

    public void setSendExperssion(Hashtable h)
    {
      /*  string strState = h["state"].ToString();
        if (strState == "0")
        {
            strSendID = h["sendId"].ToString();
            strReceiveID = h["receiveId"].ToString();
        }
        else
        {
            PopupCommon.GetSingleton().ShowView("发送表情失败!");
        }*/
    }
    public void setAnimojiInfo(Hashtable h)
    {
        string strState = h["state"].ToString();
        if(strState == "0")
        {
            strSendID = h["sendId"].ToString();
            strReceiveID = h["receiveId"].ToString();
        }
        else
        {
            PopupCommon.GetSingleton().ShowView("发送表情失败!");
            return;
        }

        gamePlayAnim(h["iconId"].ToString());

    }

    public void receiveChat(Hashtable h)
    {
        strSendID = h["sendId"].ToString();
        string strType = h["messageType"].ToString();
        if (strType == "1")//表情
        {
            StartCoroutine(playFace(h["message"].ToString()));
        }
        else if (strType == "0")//文字
        {
            StartCoroutine(playChat(h["message"].ToString()));

            bool isMy = false;
            for (int i = 0; i < roomNumSitActivePlayerTrans.childCount; i++)
            {
                PlayInfo pInfo = roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>();
                if (pInfo == null)
                {
                    continue;
                }
                if (pInfo.userID == strSendID && GameManager.GetSingleton().myNetID == i)
                {
                    isMy = true;
                    break;
                }
            }
            chatPanel.AddChatItem(strSendID, isMy, h["message"].ToString());
        } else if (strType == "2"){ // 语音
            // todo
        }

    }

    public void notifyPlayVoice(Hashtable h){
        string userID = h["userID"].ToString();
        string voicePath = h["path"].ToString();
        int voiceTime = int.Parse(h["time"].ToString());
        if(userID == StaticData.ID){ // 不需要播放自己声音
            return;
        }

        VoiceMeta data = new VoiceMeta();
        data.userID = userID;
        data.voicePath = voicePath;
        data.vocieTime = voiceTime;

        voiceQueue.Enqueue(data); // 入队列
    }

    IEnumerator doPlayVoice(string userID, string voicePath, int vocieTime){
        PlayInfo pInfo = null;
        for (int i = 0; i < roomNumSitActivePlayerTrans.childCount; i++)
        {
            pInfo = roomNumSitActivePlayerTrans.GetChild(i).GetComponent<PlayInfo>();
            if (pInfo != null && pInfo.userID == strSendID)
            {
                break;
            }
        }
        if(pInfo == null){
            yield return null;
        }

        voicePlaying = true;
        AliyunOssService.Instance.AsyncDownloadObject(voicePath, (bool isOk, byte[] data) =>{
            if(!isOk){
                // Tip.Instance.showMsg("播放失败");
                voicePlaying = false;
                return;
            }
            AudioClip clip = VoiceRecorder.Instance.Read(data);
            if(clip == null){
                voicePlaying = false;
                return;
            }
            VoiceRecorder.Instance.PlayRecord(clip);
            Vector3 pos = pInfo.transform.localPosition;
            playVoice.Show(new Vector3(pos.x, pos.y+80, pos.z), vocieTime);
        });
        yield return new WaitForSeconds(vocieTime+1);
        playVoice.Hide();
        voicePlaying = false;
    }
    
}