using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OtherPanel : BasePlane {
    public Button storeBtn;
    public Button friendBtn;
    public Button guessBtn;
    public Button leaveBtn;
    public Button reviewBtn;
    public Text coin;
    public CircleImage head;

    public int btnClick = 0;
    public bool isManager;

    public Vector3 v1;
    public Vector3 v2;

    public Button guessButton;

    void Awake() {
        head = transform.Find("head/mask").GetComponent<CircleImage>();
       
        friendBtn = transform.Find("friend").GetComponent<Button>();
		storeBtn = transform.Find("store").GetComponent<Button>();
        guessBtn = transform.Find("guessCard").GetComponent<Button>();
		reviewBtn = transform.Find("review").GetComponent<Button>();
        leaveBtn = transform.Find("leaveDesk").GetComponent<Button>();
        coin = transform.Find("mycoin/Text").GetComponent<Text>();

        v1 = storeBtn.GetComponent<RectTransform>().anchoredPosition;
        v2 = friendBtn.GetComponent<RectTransform>().anchoredPosition;

        guessButton= GameObject.Find("Controlbtns/guessBtn").GetComponent<Button>();

        storeBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().planeManager.RemoveSidePlane();
            
            btnClick = 1;
        });
        friendBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().planeManager.RemoveSidePlane();

            btnClick = 2;
        });
        guessBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().planeManager.RemoveSidePlane();
            btnClick = 3;
        });
        leaveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.keepSeat, new object[] { });
            GameUIManager.GetSingleton().planeManager.RemoveSidePlane();
            btnClick = 4;
        });
        reviewBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().planeManager.RemoveSidePlane();
            btnClick = 5;
        });


        //guessButton.onClick.AddListener(()=> {
        //    GameUIManager.GetSingleton().guessHandPopup.gameObject.SetActive(true);
        //   // GameUIManager.GetSingleton().guessRecordPanel.gameObject.SetActive(true);
        //    TouchMove.Instance.isRun = false;
        //});

        //gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
     
    }

    void OnEnable() {
     

    }

	// Update is called once per frame
	void Update () {
        if (isManager)
        {
            friendBtn.gameObject.SetActive(true);
            storeBtn.GetComponent<RectTransform>().anchoredPosition = v1;
        }
        else {
            friendBtn.gameObject.SetActive(false);
            storeBtn.GetComponent<RectTransform>().anchoredPosition = v2;
        }
	}
    public void GetTexture(Sprite s)
    {
        if (s != null)
        {
            head.sprite = s;
        }
    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.getMyInfo, new object[] { });
    }

    public override void OnRemoveComplete()
    {
        if (btnClick == 1)
        {
            GameUIManager.GetSingleton().shopInGame.gameObject.SetActive(true);
           // NetMngr.GetSingleton().Send(InterfaceMain.GetShopInfo, new object[] { "1" });
           // NetMngr.GetSingleton().Send(InterfaceMain.GetShopInfo, new object[] { "2" });
            TouchMove.Instance().isRun = false;
        }
        if (btnClick == 2)
        {
            GameUIManager.GetSingleton().playerManageListPopup.ShowView();

        }
        if (btnClick == 3)
        {
            //GameUIManager.GetSingleton().guessHandPopup.gameObject.SetActive(true);
            GameUIManager.GetSingleton().guessRecordPanel.gameObject.SetActive(true);
            TouchMove.Instance().isRun = false;
            NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { 1, 10 });
        }
        if (btnClick == 5) {
            NetMngr.GetSingleton().Send(InterfaceGame.getRoundReviewMaxPage, new object[] { GameUIManager.GetSingleton().gameReviewPanel.isMine });
         //   GameUIManager.GetSingleton().gameReviewPanel.gameObject.SetActive(true);
            TouchMove.Instance().isRun = false;
         //   NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { 1, GameUIManager.GetSingleton().gameReviewPanel.isMine });
        }
        btnClick = 0;
    }

    public override void OnRemoveStart()
    {

    }

    public void GetMyInfoCallBack(Hashtable data) {
        GameTools.GetSingleton().GetTextureFromNet(data["url"].ToString(), GetTexture);
        coin.text = data["curCoin"].ToString();
        isManager = data["isManager"].ToString() == "1" ? true : false;
    }

}
