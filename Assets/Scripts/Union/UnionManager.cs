using UnityEngine;
using System.Collections;

public class UnionManager : MonoBehaviour {


    private static UnionManager unionManager;
    public static UnionManager GetSingleton()
    {
        return unionManager;
    }
    public PlaneManager planeManager;
    /// <summary>
    /// containerBottom
    /// </summary>

    /// <summary>
    /// containerPopup
    /// </summary>
    /// <summary>

    /// containerTop
    /// </summary>
    public MyUnionPanel myUnionPanel;
    public UnionCreatePanel unionCreatePanel;
    public UnionJoinPanel unionJoinPanel;
    public UnionInfoPanel unionInfoPanel;
    public UnionUpgradePanel unionUpgradePanel;
    public UnionCoinPanel unionCoinPanel;
    public ShopUnionCoinPanel shopUnionCoinPanel;
    public SendUnionCoinPanel sendUnionCoinPanel;
    public RecycleUnionCoinPanel recycleUnionCoinPanel;
    
    
    //通用弹窗
    public PopupCommon commonPopup;

    private Transform containerBottom;
    private Transform containerPopup;
    private Transform containerTop;

    public string clubId = "";
    public string unionId = "";

    private void Awake()
    {
        unionManager = this;

        containerBottom = transform.Find("ContainerBottom");
        containerTop = transform.Find("ContainerTop");
        containerPopup = transform.Find("ContainerPopup");

        myUnionPanel = containerTop.Find("MyUnionPanel").GetComponent<MyUnionPanel>();
        unionCreatePanel = containerTop.Find("UnionCreatePanel").GetComponent<UnionCreatePanel>();
        unionJoinPanel = containerTop.Find("UnionJoinPanel").GetComponent<UnionJoinPanel>();
        unionInfoPanel = containerTop.Find("UnionInfoPanel").GetComponent<UnionInfoPanel>();
        unionUpgradePanel = containerTop.Find("UnionUpgradePanel").GetComponent<UnionUpgradePanel>();
        unionCoinPanel = containerTop.Find("UnionCoinPanel").GetComponent<UnionCoinPanel>();
        shopUnionCoinPanel = containerTop.Find("ShopUnionCoinPanel").GetComponent<ShopUnionCoinPanel>();
        sendUnionCoinPanel = containerTop.Find("SendUnionCoinPanel").GetComponent<SendUnionCoinPanel>();
        recycleUnionCoinPanel = containerTop.Find("RecycleUnionCoinPanel").GetComponent<RecycleUnionCoinPanel>();

        commonPopup= containerPopup.Find("CommonPopup").GetComponent<PopupCommon>();

        planeManager = GetComponent<PlaneManager>();
        planeManager.Init(containerBottom.GetComponent<RectTransform>(), containerTop.GetComponent<RectTransform>());
        planeManager.movePosition = 2f;
        planeManager.topPlaneMoveTime = 0.4f;
        planeManager.sidePlaneMoveTime = 0.4f;
    }

    // Use this for initialization
    void Start () {        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}