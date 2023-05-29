using UnityEngine;
using System.Collections;

public class ClubManager : MonoBehaviour {


    private static ClubManager clubManager;
    public static ClubManager GetSingleton()
    {
        return clubManager;
    }
    public PlaneManager planeManager;
    /// <summary>
    /// containerBottom
    /// </summary>
    public ClubListPanel clubListPanel;
	public MyClubPanel myClubPanel;
    public ClubPanel clubPanel;

    /// <summary>
    /// containerPopup
    /// </summary>
    public ScreenMemberPopup screenMemberPopup;
    public DissClubPopup dissClubPopup;
    public UpdateClubTipPopup updateClubTipPopup;
    public AddClubManagerTipPopup addClubManagerTipPopup;
    public erweimaSharePopup erweimaPopup;
    public ScreenMatch screenMatch;
    /// <summary>
    /// containerTop
    /// </summary>
    public ClubInfoTopPanel clubInfoTopPanel;
    public ClubMemberInfoTopPanel clubMemberInfoTopPanel;
    public ClubCreateTopPanel clubCreateTopPanel;
    public AddClubTipTopPanel addClubTipTopPanel;
    public ClubUpdateTopPanel clubUpdateTopPanel;
    public WelcomeMemberTopPanel welcomeMemberTopPanel;
    public erweimaTopPanel erweimaPanel;
    public CreateGongGaoTopPanel createGongGaoTopPanel;
    public HotClubTopPanel hotClubTopPanel;
    public SetClubManagerTopPanel setClubManagerTopPanel;
    public ClubMemberTopPanel clubMemberTopPanel;
    public ClubMember2TopPanel clubMember2TopPanel;
    public ClubEditTopPanel clubEditTopPanel;
    public ClubCashOutPanel clubCashOutPanel;
    public FriendCommissionPanel friendCommissionPanel;
    //    public SendCoinTopPanel sendCoinTopPanel;

    public SendMemberDiamondTopPanel sendMemberDiamondTopPanel;
    public SendMemberCoinTopPanel sendMemberCoinTopPanel;
    public ClubModifyLimitPanel clubModifyLimitPanel;

    //通用弹窗
    public PopupCommon commonPopup;

    //邀请码弹窗
    public FriendCode friendCode;
    // public PopupCommon commonPopup;
    private Transform containerBottom;
    private Transform containerPopup;
    private Transform containerTop;

    private void Awake()
    {
        clubManager = this;
        containerBottom = transform.Find("ContainerBottom");
        containerTop = transform.Find("ContainerTop");
        containerPopup = transform.Find("ContainerPopup");

        clubListPanel = containerBottom.Find("ClubBottomPanel/ClubListPanel").GetComponent<ClubListPanel>();
		myClubPanel = containerBottom.Find("ClubBottomPanel/MyClubPanel").GetComponent<MyClubPanel>();
        clubPanel = containerBottom.Find("ClubBottomPanel/ClubPanel").GetComponent<ClubPanel>();

        screenMemberPopup = containerPopup.Find("ScreenMember").GetComponent<ScreenMemberPopup>();
        dissClubPopup= containerPopup.Find("DissClub").GetComponent<DissClubPopup>();
        friendCode = containerPopup.Find("FriendCode").GetComponent<FriendCode>();
        updateClubTipPopup = containerPopup.Find("UpdateClubTip").GetComponent<UpdateClubTipPopup>();
        addClubManagerTipPopup= containerPopup.Find("AddClubManagerTip").GetComponent<AddClubManagerTipPopup>();
        erweimaPopup= containerPopup.Find("erweimaShare").GetComponent<erweimaSharePopup>();
        commonPopup= containerPopup.Find("CommonPopup").GetComponent<PopupCommon>();
        screenMatch= containerPopup.Find("ScreenMatch").GetComponent<ScreenMatch>();

        clubInfoTopPanel = containerTop.Find("ClubInfoTopPanel").GetComponent<ClubInfoTopPanel>();
        clubMemberInfoTopPanel= containerTop.Find("ClubMemberInfoTopPanel").GetComponent<ClubMemberInfoTopPanel>();
        clubCreateTopPanel= containerTop.Find("ClubCreateTopPanel").GetComponent<ClubCreateTopPanel>();
        addClubTipTopPanel= containerTop.Find("AddClubTipTopPanel").GetComponent<AddClubTipTopPanel>();
        clubUpdateTopPanel= containerTop.Find("ClubUpdateTopPanel").GetComponent<ClubUpdateTopPanel>();
        welcomeMemberTopPanel= containerTop.Find("WelcomeMemberTopPanel").GetComponent<WelcomeMemberTopPanel>();
        erweimaPanel= containerTop.Find("erweimaTopPanel").GetComponent<erweimaTopPanel>();
        createGongGaoTopPanel= containerTop.Find("CreateGongGaoTopPanel").GetComponent<CreateGongGaoTopPanel>();
        hotClubTopPanel = containerTop.Find("HotClubTopPanel").GetComponent<HotClubTopPanel>();
        setClubManagerTopPanel = containerTop.Find("SetClubManagerTopPanel").GetComponent<SetClubManagerTopPanel>();
        clubMemberTopPanel= containerTop.Find("ClubMemberTopPanel").GetComponent<ClubMemberTopPanel>();
        clubMember2TopPanel= containerTop.Find("ClubMember2TopPanel").GetComponent<ClubMember2TopPanel>();
        clubEditTopPanel= containerTop.Find("ClubEditTopPanel").GetComponent<ClubEditTopPanel>();
       // sendCoinTopPanel = containerTop.Find("SendCoinTopPanel").GetComponent<SendCoinTopPanel>();
        sendMemberDiamondTopPanel = containerTop.Find("SendMemberDiamondTopPanel").GetComponent<SendMemberDiamondTopPanel>();
        sendMemberCoinTopPanel = containerTop.Find("SendMemberCoinTopPanel").GetComponent<SendMemberCoinTopPanel>();
        clubCashOutPanel = containerTop.Find("CashOutPanel").GetComponent<ClubCashOutPanel>();
        friendCommissionPanel = containerTop.Find("FriendCommissionPanel").GetComponent<FriendCommissionPanel>();
        clubModifyLimitPanel = containerTop.Find("ClubModifyLimitPanel").GetComponent<ClubModifyLimitPanel>();
        

        planeManager = GetComponent<PlaneManager>();
        planeManager.Init(containerBottom.GetComponent<RectTransform>(), containerTop.GetComponent<RectTransform>());
        planeManager.movePosition = 2f;
        planeManager.topPlaneMoveTime = 0.4f;
        planeManager.sidePlaneMoveTime = 0.4f;
    }

    // Use this for initialization
    void Start () {
        commonPopup.gameObject.SetActive(false);
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
