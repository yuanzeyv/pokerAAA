using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controlbtns : MonoBehaviour {

    private Button systembtn;//系统设置按钮
    private Button homeMsgBtn;//首页按钮
    private Button record;//记录按钮
    private Button recordZhanji;//记录战绩按钮
    private Button actualZhanji;

	public Button share;
	public Button msg;

	public Button storeBtn;
	public Button guessBtn;
	public Button reviewBtn;

    public Button btnJiFenPai;

    public VoiceButton voiceButton;

    private void Awake()
    {
        systembtn = transform.Find("SystemBtn").GetComponent<Button>();
        homeMsgBtn = transform.Find("homeMsgBtn").GetComponent<Button>();
        record = transform.Find("record").GetComponent<Button>();
        recordZhanji = transform.Find("recordZhanji").GetComponent<Button>();
        actualZhanji = transform.Find("actualZhanji").GetComponent<Button>();

		storeBtn = transform.Find("store").GetComponent<Button>();
		guessBtn = transform.Find("guessCard").GetComponent<Button>();
		reviewBtn = transform.Find("review").GetComponent<Button>();
        voiceButton = transform.Find("voiceBtn").GetComponent<VoiceButton>();
      
		share = transform.Find("shareBtn").GetComponent<Button>();
        
        msg = transform.Find("msgBtn").GetComponent<Button>();
        btnJiFenPai = transform.Find("JiFenPaiBtn").GetComponent<Button>(); //add by yang yang 2021年3月17日 10:55:21
		storeBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			GameUIManager.GetSingleton().shopInGame.gameObject.SetActive(true);
		});

		reviewBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.getRoundReviewMaxPage, new object[] { GameUIManager.GetSingleton().gameReviewPanel.isMine });
        //    GameUIManager.GetSingleton().gameReviewPanel.gameObject.SetActive(true);
		//	NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { 1, GameUIManager.GetSingleton().gameReviewPanel.isMine });
		});

		guessBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			GameUIManager.GetSingleton().guessRecordPanel.gameObject.SetActive(true);
			NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { 1, 10 });
		});

        btnJiFenPai.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton()._mjifenpai.ShowView();
            GameUIManager.GetSingleton()._mjifenpai.showInfo(-1);
        });

        share.onClick.AddListener(()=> 
		{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
		//	GameUIManager.GetSingleton().gameSharePopup.ShowView();
		});

		msg.onClick.AddListener(()=> 
		{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
		    GameUIManager.GetSingleton().planeManager.AddTopPlane(GameUIManager.GetSingleton().paiMsgTopPanel);
        });


        systembtn.onClick.AddListener(()=> 
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().ptopleftpanel.gameObject.SetActive(true);
        });
        homeMsgBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

        });
        record.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

        });
        recordZhanji.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

        });
        actualZhanji.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

        });
        
    }


	void Start ()
    {
	    
	}
	

	void Update ()
    {
	
	}
}
