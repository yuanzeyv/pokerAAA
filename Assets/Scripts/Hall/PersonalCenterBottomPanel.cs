using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersonalCenterBottomPanel : BasePlane
{
	public RawImage icon;
    private Text playerName;
    private Text id;
    private Text signature;
    private Button modificationBtn;
    private Text diamond;
    private Button diamondBtn;
    private Text gold;
    private Button goldBtn;
    private Text unionCoin;
    private Button unionCoinBtn;
    private Button myData;
    private Button myPai;
    //private Text myPhoneInfo;
    private Button myScore;
    private Button friend;
    private Button share;
    private Button serives;
    private Button setting;
    private Button CommissionBtn;
    private Image sex0;
    private Image sex1;
    private RawImage bgImgae;
    private RectTransform bgimage;
    private RectTransform content;

    public bool isStart = false;

    public string fixedDiamond="30";

    private Button Btn_msg;
    private Image redImage;

    
    private void Awake()
    {
        icon = transform.Find("Scroll View/Viewport/Content/Top/Title/Icon/icon").GetComponent<RawImage>();
        playerName = transform.Find("Scroll View/Viewport/Content/Top/Title/Name").GetComponent<Text>();
        id = transform.Find("Scroll View/Viewport/Content/Top/Title/ID").GetComponent<Text>();
        signature = transform.Find("Scroll View/Viewport/Content/Top/Title/Signature").GetComponent<Text>();
		modificationBtn = transform.Find("Scroll View/Viewport/Content/Top/Title/bianji").GetComponent<Button>();
		diamond = transform.Find("Scroll View/Viewport/Content/Diamond/Count").GetComponent<Text>();
		diamondBtn = transform.Find("Scroll View/Viewport/Content/Diamond").GetComponent<Button>();
		gold = transform.Find("Scroll View/Viewport/Content/Gold/Count").GetComponent<Text>();
		goldBtn = transform.Find("Scroll View/Viewport/Content/Gold").GetComponent<Button>();
        unionCoin = transform.Find("Scroll View/Viewport/Content/UnionCoin/Count").GetComponent<Text>();
        unionCoinBtn = transform.Find("Scroll View/Viewport/Content/UnionCoin").GetComponent<Button>();
        myData = transform.Find("Scroll View/Viewport/Content/MyData").GetComponent<Button>();
        myPai = transform.Find("Scroll View/Viewport/Content/MyPai").GetComponent<Button>();
        //myPhone = transform.Find("Scroll View/Viewport/Content/MyPhone").GetComponent<Button>();
        //myPhoneInfo = transform.Find("Scroll View/Viewport/Content/MyPhone/Tip").GetComponent<Text>();
		myScore = transform.Find("Scroll View/Viewport/Content/zhanji").GetComponent<Button>();
        friend = transform.Find("Scroll View/Viewport/Content/Friend").GetComponent<Button>();
        share = transform.Find("Scroll View/Viewport/Content/Share").GetComponent<Button>();
        serives = transform.Find("Scroll View/Viewport/Content/Serives").GetComponent<Button>();
        setting = transform.Find("Scroll View/Viewport/Content/Setting").GetComponent<Button>();
        sex0 = transform.Find("Scroll View/Viewport/Content/Top/Title/Sex0").GetComponent<Image>();
        sex1 = transform.Find("Scroll View/Viewport/Content/Top/Title/Sex1").GetComponent<Image>();
        bgImgae = transform.Find("Scroll View/Viewport/Content/Top/BG").GetComponent<RawImage>();
        content = transform.Find("Scroll View/Viewport/Content").GetComponent<RectTransform>();
        bgimage = transform.Find("Scroll View/Viewport/Content/Top/BG").GetComponent<RectTransform>();
        // CommissionBtn = transform.Find("Scroll View/Viewport/Content/Commission").GetComponent<Button>();
        modificationBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().personalInfoTopPanel);
        });
        diamondBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().shopDiamondTopPanel);
        });
        goldBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().shopGoldTopPanel);
        });
        unionCoinBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if(StaticData.unionId == ""){
                Tip.Instance.showMsg("请先加入联盟！");
                return;                
            }
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionCoinPanel);
            // HallManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().shopUnionCoinPanel);
        });
        myData.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            // HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().myDataTopPanel);
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().scoreBottomPanel);
        });
		myScore.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().scoreBottomPanel);
        });
        myPai.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().myPaiPuTopPanel);
        });
        friend.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().friendTopPanel);
        });
        share.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //add by yang yang 2021年4月23日 10:57:47  分享界面跳转更改
          //  ClubManager.GetSingleton().friendCommissionPanel.nClubID = int.Parse();
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().friendCommissionPanel);
         //   ClubManager.GetSingleton().erweimaPopup.ShowView();
        });
        serives.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().servicesTopPanel);
        });
        setting.onClick.AddListener(() => 
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().settingTopPanel);
        });
        // CommissionBtn.onClick.AddListener(() =>
        // {
        //  //   ClubManager.GetSingleton().friendCommissionPanel.bShowShare = false;
        //     HallManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().friendCommissionPanel);
        // });

        Btn_msg = transform.Find("Button_msg").GetComponent<Button>();
        Btn_msg.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().noticeListBottomPanel);
        });
        redImage = transform.Find("Button_msg/Image_red").GetComponent<Image>();
        showRed();
    }

    public void SetData(Hashtable data)
    {
        StaticData.url = data["playerURL"].ToString();
        StaticData.ID= data["playerID"].ToString();
        StaticData.username= data["playerName"].ToString();
        StaticData.sex = data["playerSex"].ToString();
        StaticData.signature = data["playerSignature"].ToString();
        StaticData.diamond = int.Parse(data["playerDiamond"].ToString());
        StaticData.gold = int.Parse(data["playerGold"].ToString());
        if(data.ContainsKey("clubId")){
            StaticData.clubId = data["clubId"].ToString();    
        }
        if(data.ContainsKey("lmId")){
            StaticData.unionId = data["lmId"].ToString();    
        }
        if(data.ContainsKey("lmCoin")){
            StaticData.unionCoin = int.Parse(data["lmCoin"].ToString());            
        }
        
        playerName.text = data["playerName"].ToString();
        GameTools.GetSingleton().GetTextureNet(data["playerURL"].ToString(), GetNetSprite);
        id.text = "ID:" + data["playerID"].ToString();
        signature.text = data["playerSignature"].ToString();
        NetMngr.playerId = data["playerID"].ToString();
        if (data["playerSex"].ToString() == "1")
        {
            sex1.gameObject.SetActive(true);
            sex0.gameObject.SetActive(false);
        }
        else
        {
            sex0.gameObject.SetActive(true);
            sex1.gameObject.SetActive(false);
        }
        diamond.text = data["playerDiamond"].ToString();
        gold.text = data["playerGold"].ToString();
        unionCoin.text = data["lmCoin"].ToString();
//        if (string.IsNullOrEmpty(data["playerBlindPhone"].ToString()))
//        {
//            myPhoneInfo.text = "绑定手机号,可以获得" + fixedDiamond + "钻石";
//        }
//        else
//        {
//            myPhoneInfo.text = data["playerBlindPhone"].ToString();
//            myPhone.interactable = false;
//            myPhone.transform.Find("Btn").gameObject.SetActive(false);
//        }
       // GameTools.GetSingleton().GetTextureFromNet(data["playerURL"].ToString(), GetNetTexture);
    }
    private void GetNetTexture(Texture texture)
    {
//        bgImgae.texture = texture;
    }

    private void GetNetSprite(Texture sprite)
    {
        icon.texture = sprite;
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

    void Start ()
    {
	
	}
	

	void Update ()
    {
        
	}
    public void SetDropPro()
    {
        if (isStart)
        {
            if (content.rect.y < -0.1f)
            {
                print("sssssss");
                bgimage.sizeDelta = new Vector2(750 - content.anchoredPosition3D.y, 750 - content.rect.y);
                bgimage.anchoredPosition3D = new Vector3(0, bgimage.rect.height / 2, 0);
            }
        }
    }

    public void SetStartDrag()
    {
        //isStart = true;
    }

    public void SetEndDrag()
    {
        //isStart = false;
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetPlayerInfo, new object[] { });
    }

    public override void OnAddStart()
    {
        TouchMove.Instance().RemoveFunction();
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
