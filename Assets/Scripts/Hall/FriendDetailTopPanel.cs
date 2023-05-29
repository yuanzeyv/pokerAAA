using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FriendDetailTopPanel : BasePlane {

    private RawImage icon;
    private Text playerName;
    private Text playerid;
    private Text signature;
    private Text totalCount;
    private Text totalShou;
//    private Text ruChiLv;
//    private Text ruChi;
//    private Text tanpai;
    private Image sex0;
    private Image sex1;
    private Button invite;
    private Button delete;
    private Button backBtn;
    public string id;

	public DataIcon ruChiLv;
	public DataIcon ruChi;
	public DataIcon tanpai;
	public Dictionary<int, DataIcon> dataicons = new Dictionary<int, DataIcon>();


    private void Awake()
    {
		icon = transform.Find("Info/Member/MemberHead/mask").GetComponent<RawImage>();
		playerName = transform.Find("Info/Member/MemberName").GetComponent<Text>();
		playerid = transform.Find("Info/Member/Id").GetComponent<Text>();
		signature = transform.Find("Info/Member/myTip").GetComponent<Text>();
		totalCount = transform.Find("Info/Member/zong/GameCount").GetComponent<Text>();
		totalShou = transform.Find("Info/Member/zong/zongshoushu").GetComponent<Text>();
//        ruChiLv = transform.Find("/value").GetComponent<Text>();
//        ruChi = transform.Find("ruChi/value").GetComponent<Text>();
//        tanpai = transform.Find("tanPai/value").GetComponent<Text>();

		ruChiLv = transform.Find("Info/ruchishenglv").GetComponent<DataIcon>();
		ruChi = transform.Find("Info/ruchilv").GetComponent<DataIcon>();
		tanpai = transform.Find("Info/tanpailv").GetComponent<DataIcon>();

		dataicons.Add (0, ruChiLv);
		dataicons.Add (1, ruChi);
		dataicons.Add (2, tanpai);

		delete = transform.Find("Info/Quit/quitBtn").GetComponent<Button>();
		sex0 = transform.Find("Info/Member/Id/girl").GetComponent<Image>();
		sex1 = transform.Find("Info/Member/Id/man").GetComponent<Image>();
		backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        delete.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("确定删除好友吗?",null,true,delegate 
            {
                NetMngr.GetSingleton().Send(InterfaceMain.DeleteFriend, new object[] { id });
            });
        });

    }
    public void DeleteFriendFinished(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        GameTools.GetSingleton().stopGameToolsAllCoroutines();
        HallManager.GetSingleton().planeManager.RemoveTopPlane();
        HallManager.GetSingleton().friendTopPanel.currPage = 1;
        NetMngr.GetSingleton().Send(InterfaceMain.GetFriendList, new object[] { 1, 10 });
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    public void SetFriendData(Hashtable data)
    {
        GameTools.GetSingleton().GetTextureNet(data["playerURL"].ToString(), SetTexture);
        playerName.text = data["playerName"].ToString();
        playerid.text = "ID:"+data["id"].ToString();
        signature.text= data["signature"].ToString();
        if (data["sex"].ToString()=="1")
        {
            sex0.gameObject.SetActive(false);
            sex1.gameObject.SetActive(true);
        }
        else
        {
            sex0.gameObject.SetActive(true);
            sex1.gameObject.SetActive(false);
        }
        totalCount.text = "总局数:"+data["count"].ToString();
        totalShou.text = "总手数:"+data["shoucount"].ToString();

		ruChiLv.Reset();
		ruChi.Reset();
		tanpai.Reset();
		ruChiLv.SetData(dataicons, float.Parse(data["ruchiWin"].ToString()),"入池胜率");
		ruChi.SetData(dataicons, float.Parse(data["ruchi"].ToString()), "入池率");
		tanpai.SetData(dataicons, float.Parse(data["tanPai"].ToString()), "摊牌率");

    }
    private void SetTexture(Texture sprite)
    {
        icon.texture = sprite;
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetFriendDetail, new object[] { id });
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
}
