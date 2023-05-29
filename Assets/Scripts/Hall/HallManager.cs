using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System;

public class HallManager : MonoBehaviour
{
    public HallBottomPanel hallBottomPanel;
    public NoticeListBottomPanel noticeListBottomPanel;
    public ScoreBottomPanel scoreBottomPanel;
    public PersonalCenterBottomPanel personalCenterBottomPanel;
    public KuaisupaijuPanel kuaisupaijuPanel;

    public ScoreDetailTopPanel scoreDetailTopPanel;
    public TheGamePaiRecordTopPanel theGamePaiRecordTopPanel;
    public TheGamePaiTopPanel theGamePaiTopPanel;
    public AboutOurTopPanel aboutOurTopPanel;
    //    public BlindPhoneTopPanel blindPhoneTopPanel;
    public FriendDetailTopPanel friendDetailTopPanel;
    public FriendTopPanel friendTopPanel;
    public SettingTopPanel settingTopPanel;
    public ServicesTopPanel servicesTopPanel;
    public MyDataTopPanel myDataTopPanel;
    public ShopGoldTopPanel shopGoldTopPanel;
    public ShopDiamondTopPanel shopDiamondTopPanel;
    public SignatureTopPanel signatureTopPanel;
    public NameTopPanel nameTopPanel;
    public PersonalInfoTopPanel personalInfoTopPanel;
    public MyPaiPuTopPanel myPaiPuTopPanel;
    public AddFriendTopPanel addFriendTopPanel;
    public AddFriendInfoTopPanel addFriendInfoTopPanel;
    public NoticeListContentTopPanel noticeListContentTopPanel;
    public NoticeContentTopPanel noticeContentTopPanel;
    public MyMsgTopPanel myMsgTopPanel;
    public PaiMsgTopPanel paiMsgTopPanel;
    public AgreementTopPanel agreementTopPanel;
    public InsuranceDetailPanel insuranceDetailPanel;
    public FriendRemarkPanel friendRemarkPanel;    

    public CreateGamePopup createGamePopup;
    public SelectPhotoPopup selectPhotoPopup;
    public SelectSexPopup selectSexPopup;
    public OddsPopup oddsPopup;
    public SelectPayPopup selectPayPopup;

    public FixPad fixPadPopup;
    public MatchMainPanel matchMainPanel;

    public RectTransform containerBottom;
    public RectTransform containerTop;


    public Transform containerPopup;

    public PlaneManager planeManager;

    private Toggle[] bottomToggleGroup;
    private Image redImage;

    private Button Btn_msg;

    private static HallManager singleton;
    public static HallManager GetSingleton()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        containerBottom = transform.Find("ContainerBottom").GetComponent<RectTransform>();
        containerTop = transform.Find("ContainerTop").GetComponent<RectTransform>();
        containerPopup = transform.Find("ContainerPopup");

        hallBottomPanel = containerBottom.Find("HallBottomPanel").GetComponent<HallBottomPanel>();
        noticeListBottomPanel = containerTop.Find("NoticeListBottomPanel").GetComponent<NoticeListBottomPanel>();
        personalCenterBottomPanel = containerBottom.Find("PeronalCenterBottomPanel").GetComponent<PersonalCenterBottomPanel>();
        kuaisupaijuPanel = containerBottom.Find("kuaisupaijuPanel").GetComponent<KuaisupaijuPanel>();

        Btn_msg = transform.Find("Button_msg").GetComponent<Button>();
        scoreBottomPanel = containerTop.Find("ScoreBottomPanel").GetComponent<ScoreBottomPanel>();
        scoreDetailTopPanel = containerTop.Find("ScoreDetailTopPanel").GetComponent<ScoreDetailTopPanel>();
        theGamePaiRecordTopPanel = containerTop.Find("TheGamePaiRecordTopPanel").GetComponent<TheGamePaiRecordTopPanel>();
        theGamePaiTopPanel = containerTop.Find("TheGamePaiTopPanel").GetComponent<TheGamePaiTopPanel>();
        aboutOurTopPanel = containerTop.Find("AboutOurTopPanel").GetComponent<AboutOurTopPanel>();
        //blindPhoneTopPanel = containerTop.Find("BlindPhoneTopPanel").GetComponent<BlindPhoneTopPanel>();
        friendDetailTopPanel = containerTop.Find("FriendDetailTopPanel").GetComponent<FriendDetailTopPanel>();
        friendTopPanel = containerTop.Find("FriendTopPanel").GetComponent<FriendTopPanel>();
        settingTopPanel = containerTop.Find("SettingTopPanel").GetComponent<SettingTopPanel>();
        servicesTopPanel = containerTop.Find("ServicesTopPanel").GetComponent<ServicesTopPanel>();
        myDataTopPanel = containerBottom.Find("MyDataTopPanel").GetComponent<MyDataTopPanel>();
        shopGoldTopPanel = containerTop.Find("ShopGoldTopPanel").GetComponent<ShopGoldTopPanel>();
        shopDiamondTopPanel = containerTop.Find("ShopDiamondTopPanel").GetComponent<ShopDiamondTopPanel>();
        signatureTopPanel = containerTop.Find("SignatureTopPanel").GetComponent<SignatureTopPanel>();
        nameTopPanel = containerTop.Find("NameTopPanel").GetComponent<NameTopPanel>();
        personalInfoTopPanel = containerTop.Find("PersonalInfoTopPanel").GetComponent<PersonalInfoTopPanel>();
        myPaiPuTopPanel = containerTop.Find("MyPaiPuTopPanel").GetComponent<MyPaiPuTopPanel>();
        addFriendTopPanel = containerTop.Find("AddFriendTopPanel").GetComponent<AddFriendTopPanel>();
        addFriendInfoTopPanel = containerTop.Find("AddFriendInfoTopPanel").GetComponent<AddFriendInfoTopPanel>();
        noticeListContentTopPanel = containerTop.Find("NoticeListContentTopPanel").GetComponent<NoticeListContentTopPanel>();
        noticeContentTopPanel = containerTop.Find("NoticeContentTopPanel").GetComponent<NoticeContentTopPanel>();
        myMsgTopPanel = containerTop.Find("MyMsgTopPanel").GetComponent<MyMsgTopPanel>();
        paiMsgTopPanel = containerTop.Find("PaiMsgTopPanel").GetComponent<PaiMsgTopPanel>();
        agreementTopPanel = containerTop.Find("AgreementTopPanel").GetComponent<AgreementTopPanel>();
        insuranceDetailPanel = containerTop.Find("InsuranceDetailPanel").GetComponent<InsuranceDetailPanel>();
        friendRemarkPanel = containerTop.Find("FriendRemarkPanel").GetComponent<FriendRemarkPanel>();
        
        createGamePopup = containerPopup.Find("CreateGamePopup").GetComponent<CreateGamePopup>();
        selectPhotoPopup = containerTop.Find("SelectPhotoPopup").GetComponent<SelectPhotoPopup>();
        selectSexPopup = containerTop.Find("SelectSexPopup").GetComponent<SelectSexPopup>();
        oddsPopup = containerPopup.Find("OddsPopup").GetComponent<OddsPopup>();
        selectPayPopup = containerPopup.Find("SelectPayPopup").GetComponent<SelectPayPopup>();
        fixPadPopup = containerPopup.Find("FixPad").GetComponent<FixPad>();
        matchMainPanel = containerTop.Find("MatchMainPanel").GetComponent<MatchMainPanel>();

        planeManager = GetComponent<PlaneManager>();
        planeManager.Init(containerBottom, containerTop);
        planeManager.movePosition = 2f;
        planeManager.topPlaneMoveTime = 0.4f;
        planeManager.sidePlaneMoveTime = 0.4f;

        bottomToggleGroup = transform.Find("BottomToggleGroup").GetComponentsInChildren<Toggle>();
        redImage = transform.Find("Button_msg/Image_red").GetComponent<Image>();

        //设置默认开关
        if (PlayerPrefs.HasKey("isMusic"))
        {
            StaticData.isMusic = PlayerPrefs.GetInt("isMusic") == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey("isVibrate"))
        {
            StaticData.isVibrate = PlayerPrefs.GetInt("isVibrate") == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey("isInform"))
        {
            StaticData.isinform = PlayerPrefs.GetInt("isInform") == 1 ? true : false;
        }
        Waitting.getInstance().Hide();
        //开始时候获取一次上传头像的地址
        NetMngr.GetSingleton().Send(InterfaceMain.RequestUpDataUrl, new object[] { });

        NetMngr.GetSingleton().Send(InterfaceMain.GetUpdatePar, new object[] { "4" });
    }

    void Start()
    {

        planeManager.ShowBottomPlane(hallBottomPanel);

        noticeListBottomPanel.gameObject.SetActive(false);

        hallBottomPanel.gameObject.SetActive(false);
        personalCenterBottomPanel.gameObject.SetActive(false);
        ClubManager.GetSingleton().clubListPanel.gameObject.SetActive(false);

        scoreBottomPanel.gameObject.SetActive(false);
        scoreDetailTopPanel.gameObject.SetActive(false);
        theGamePaiRecordTopPanel.gameObject.SetActive(false);
        theGamePaiTopPanel.gameObject.SetActive(false);
        aboutOurTopPanel.gameObject.SetActive(false);
        //        blindPhoneTopPanel.gameObject.SetActive(false);
        friendDetailTopPanel.gameObject.SetActive(false);
        friendTopPanel.gameObject.SetActive(false);
        settingTopPanel.gameObject.SetActive(false);
        servicesTopPanel.gameObject.SetActive(false);
        myDataTopPanel.gameObject.SetActive(false);
        shopGoldTopPanel.gameObject.SetActive(false);
        shopDiamondTopPanel.gameObject.SetActive(false);
        signatureTopPanel.gameObject.SetActive(false);
        nameTopPanel.gameObject.SetActive(false);
        personalInfoTopPanel.gameObject.SetActive(false);
        myPaiPuTopPanel.gameObject.SetActive(false);
        addFriendTopPanel.gameObject.SetActive(false);
        addFriendInfoTopPanel.gameObject.SetActive(false);
        myMsgTopPanel.gameObject.SetActive(false);
        paiMsgTopPanel.gameObject.SetActive(false);
        agreementTopPanel.gameObject.SetActive(false);
        friendRemarkPanel.gameObject.SetActive(false);

        createGamePopup.gameObject.SetActive(false);
        selectPhotoPopup.gameObject.SetActive(false);
        selectSexPopup.gameObject.SetActive(false);
        oddsPopup.gameObject.SetActive(false);
        selectPayPopup.gameObject.SetActive(false);
        PopupCommon.GetSingleton().gameObject.SetActive(false);

        Btn_msg.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().noticeListBottomPanel);
        });

        bottomToggleGroup[0].onValueChanged.AddListener(delegate {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (bottomToggleGroup[0].isOn)
            {
                planeManager.ShowBottomPlane(hallBottomPanel);
                hallBottomPanel.GetGameInfo();
                //createGamePopup.ShowView();
            }
        });
        bottomToggleGroup[1].onValueChanged.AddListener(delegate {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (bottomToggleGroup[1].isOn)
            {
                TouchMove.Instance().RemoveFunction();
                // planeManager.ShowBottomPlane(ClubManager.GetSingleton().clubListPanel);
                ClubManager.GetSingleton().clubListPanel.gameObject.SetActive(true);
                ClubManager.GetSingleton().clubPanel.gameObject.SetActive(false);
                ClubManager.GetSingleton().myClubPanel.gameObject.SetActive(false);
                ClubManager.GetSingleton().clubListPanel.transform.parent.SetAsLastSibling();
                ClubManager.GetSingleton().clubListPanel.GetClubList();

            }
        });
        bottomToggleGroup[2].onValueChanged.AddListener(delegate {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (bottomToggleGroup[2].isOn)
            {
                planeManager.ShowBottomPlane(kuaisupaijuPanel);
            }
        });
        bottomToggleGroup[3].onValueChanged.AddListener(delegate {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (bottomToggleGroup[3].isOn)
            {
                planeManager.ShowBottomPlane(myDataTopPanel);
            }
        });
        bottomToggleGroup[4].onValueChanged.AddListener(delegate {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (bottomToggleGroup[4].isOn)
            {
                planeManager.ShowBottomPlane(personalCenterBottomPanel);
            }
        });
        if (StaticData.isGameOverShowPanel != "")
        {
            scoreDetailTopPanel.id = StaticData.isGameOverShowPanel;
            planeManager.AddTopPlane(scoreDetailTopPanel);
        }

        //判断是否在俱乐部中
        if (StaticData.clubId != "")
        {
            TouchMove.Instance().RemoveFunction();
            ClubManager.GetSingleton().clubPanel.clubId = StaticData.clubId;

            ClubManager.GetSingleton().clubPanel.clubName.text = StaticData.clubName;

            ClubManager.GetSingleton().clubPanel.GongGaoTog.isOn = false;
            ClubManager.GetSingleton().clubPanel.MatchTog.isOn = true;
            ClubManager.GetSingleton().clubPanel.RecordTog.isOn = false;
            ClubManager.GetSingleton().clubPanel.toggleIndex = 2;
            NetMngr.GetSingleton().Send(InterfaceClub.GetClubMatch, new object[] { StaticData.clubId, 1, 100 });
            if (StaticData.isHost == 1 || StaticData.isHost == 2)
            {
                ClubManager.GetSingleton().clubPanel.createGongGaoBtn.gameObject.SetActive(true);
                ClubManager.GetSingleton().clubPanel.createMatchBtn.gameObject.SetActive(true);

            }
            else
            {
                ClubManager.GetSingleton().clubPanel.createGongGaoBtn.gameObject.SetActive(false);
                ClubManager.GetSingleton().clubPanel.createMatchBtn.gameObject.SetActive(false);

            }
            ClubManager.GetSingleton().clubPanel.gameObject.SetActive(true);
            ClubManager.GetSingleton().clubListPanel.transform.parent.SetAsLastSibling();
            StaticData.clubId = "";
        }
        showRed();

    }

    public void showRed()
    {
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

    void Update()
    {

    }

    [DllImport("__Internal")]
    private static extern void IOS_OpenCamera();
    [DllImport("__Internal")]
    private static extern void IOS_OpenAlbum();


    public int OpenFromType;//1个人信息2创建俱乐部3修改俱乐部信息
    public string upUrl;
    public string upName;

    public void OpenCamera(string url, string name, int otype, string strClassName)
    {
        OpenFromType = otype;
        upUrl = url;
        upName = name;
#if UNITY_ANDROID
        InvokeCamera.instance.TakeCircleClipPhoto(gameObject, "AndroidMessage");
        //AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject current = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //current.Call("Takephoto", strClassName);
#elif UNITY_IPHONE
        IOS_OpenCamera();
#endif
    }

    public void OpenAlbum(string url, string name, int otype, string strClassName)
    {
        OpenFromType = otype;
        upUrl = url;
        upName = name;
#if UNITY_ANDROID
        //InvokeCamera.instance.TakeCircleClipPotolFromAlbum(gameObject, "TakeCircleClipPotolFromAlbum");
        InvokeCamera.instance.TakeCircleClipPotolFromAlbum(gameObject, "AndroidMessage");

        //AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject current = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //current.Call("OpenGallery", strClassName);
#elif UNITY_IPHONE
        IOS_OpenAlbum();
#endif
    }

    void Message(string filenName)
    {
        string filePath = Application.persistentDataPath + "/" + filenName;
        var temp = ReadLocalFile(filePath);
        Texture2D tex = new Texture2D(256, 256, TextureFormat.RGB24, false);
        tex.LoadImage(temp);
        if (OpenFromType == 1)
        {
            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            HallManager.GetSingleton().personalInfoTopPanel.SetIcon(sprite.texture);
            StartCoroutine(SendTexture(0, "image", temp));
        }
        else if (OpenFromType == 2)
        {
            ClubManager.GetSingleton().clubCreateTopPanel.OnImageLoad(tex);
        }
        else if (OpenFromType == 3)
        {
            ClubManager.GetSingleton().clubEditTopPanel.OnImageLoad(tex);
        }


    }

    public string getUrl()
    {
        return upUrl;
    }

    IEnumerator _CircleTexture(string imageName)
    {
        string path = "file://" + imageName;
        WWW www = new WWW(path);
        while (!www.isDone)
        {

        }
        yield return www;
        //为贴图赋值
        var newTexture = www.texture;
        if (OpenFromType == 1)
        {
            var sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);

            HallManager.GetSingleton().personalInfoTopPanel.SetIcon(sprite.texture);
            StartCoroutine(SendTexture(0, "image", newTexture.EncodeToPNG()));
        }
        else if (OpenFromType == 2)
        {
            ClubManager.GetSingleton().clubCreateTopPanel.OnImageLoad(newTexture);
        }
        else if (OpenFromType == 3)
        {
            ClubManager.GetSingleton().clubEditTopPanel.OnImageLoad(newTexture);
        }


    }
    public void AndroidMessage(string imgPath)
    {
        StartCoroutine(_CircleTexture(imgPath));

        // var temp = ReadLocalFile(imgPath);
        // Texture2D tex = new Texture2D(256, 256, TextureFormat.RGB24, false);

        // tex.LoadImage(temp);
        //var newTexture =  ResetTexture(tex, 64, 64);
        // if (OpenFromType == 1) 
        // {
        //     var sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);

        //     HallManager.GetSingleton().personalInfoTopPanel.SetIcon(sprite.texture);
        //     StartCoroutine(SendTexture(0, "image", newTexture.EncodeToPNG()));
        // } 
        // else if (OpenFromType == 2)
        // {

        //     ClubManager.GetSingleton().clubCreateTopPanel.OnImageLoad(tex);
        // }
        // else if (OpenFromType == 3)
        // {

        //    /* var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        //     ClubManager.GetSingleton().clubEditTopPanel.SetIcon(sprite.texture);
        //     StartCoroutine(SendTexture(3, "image", temp));*/
        //     ClubManager.GetSingleton().clubEditTopPanel.OnImageLoad(newTexture);
        // }
    }

    IEnumerator SendTexture(int type, string fileName, byte[] b)
    {
        WWWForm form = new WWWForm();
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        upName += ("_" + Convert.ToInt64(ts.TotalSeconds).ToString());
        form.AddBinaryData(fileName, b, upName, "image/png");
        //   PopupCommon.GetSingleton().ShowView("开始上传" + upUrl);
        UnityWebRequest webRequest = UnityWebRequest.Post(upUrl, form);
        webRequest.timeout = 30;
        yield return webRequest.Send();
        if (webRequest.error != null)
        {
            Debug.Log(webRequest.error);
            //    PopupCommon.GetSingleton().ShowView("上传失败");
            if (OpenFromType == 1)
            {
                HallManager.GetSingleton().planeManager.RemoveSidePlane();
            }
            yield return null;
        }
        else
        {

            upUrl = webRequest.downloadHandler.text;
            //   PopupCommon.GetSingleton().ShowView("上传成功" + upUrl);

            if (OpenFromType == 1)
            {
                NetMngr.GetSingleton().Send(InterfaceMain.UpdateHead, new object[] { upUrl });
                HallManager.GetSingleton().planeManager.RemoveSidePlane();
            }

            yield return null;
        }
    }

    public byte[] ReadLocalFile(string filePath)
    {
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        fs.Seek(0, SeekOrigin.Begin);
        var binary = new byte[fs.Length];
        fs.Read(binary, 0, binary.Length);
        fs.Close();
        return binary;
    }
    public static Texture2D ResetTexture(Texture2D orginalTexture, int resetWidth, int resetHeight)
    {
        Texture2D orginalTextureReset = new Texture2D(orginalTexture.height, orginalTexture.height, TextureFormat.RGB24, false);
        Color color;
        int cutValue = 420;
        for (int i = 0; i < orginalTextureReset.width; i++)
        {
            for (int j = 0; j < orginalTextureReset.height; j++)
            {
                color = orginalTexture.GetPixel(i + cutValue, j);
                orginalTextureReset.SetPixel(i, j, color);
            }
        }
        Texture2D newTexture = new Texture2D(resetWidth, resetHeight, TextureFormat.RGB24, false);
        for (int i = 0; i < newTexture.height; i++)
        {
            for (int j = 0; j < newTexture.width; j++)
            {
                color = orginalTextureReset.GetPixel((int)(i * 8.4375f), (int)(j * 8.4375f));
                newTexture.SetPixel(i, j, color);
            }
        }
        newTexture.Apply();
        return newTexture;
    }


}
