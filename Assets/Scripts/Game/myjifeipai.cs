using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class myjifeipai : BasePopup {
    public Text mang;
    public Text jifenpai;
    public Text mymoney;
    public Text serviceMoney;
    public Slider mslider;
    Button btn;
    int mseatNum;
    int minCoin;

    private Text coinName;

    private void Awake()
    {
        mang = transform.Find("mang").GetComponent<Text>();
        jifenpai = transform.Find("takemoney").GetComponent<Text>();
        mymoney = transform.Find("mymoney").GetComponent<Text>();
        serviceMoney = transform.Find("serviceMoney").GetComponent<Text>();
        mslider = transform.Find("Slider").GetComponent<Slider>();
        btn = transform.Find("sure").GetComponent<Button>();
        btn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
           // GameManager.GetSingleton().takeMoney = (int)(mslider.value) ;
            if (mseatNum == -1)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.addCoin, new object[] { (int)(mslider.value * minCoin) +"" });
            }
            else
                NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerSitdownRequest, new object[] { mseatNum, (int)(mslider.value*minCoin) });
            HideView();
        });
        mslider.onValueChanged.AddListener((v)=> {
            jifenpai.text = v * minCoin + "";
        });
        coinName = transform.Find("coinName").GetComponent<Text>();

        Init();
    }
    public override void ShowView(Action onComplete = null)
    {
        base.ShowView(onComplete);
        base.hideNeedSendMsg = true;
    }
    void Start () {
        //showInfo();
    }
    public void showInfo(int seatNum)
    {
        mseatNum = seatNum;
        mang.text = GameManager.GetSingleton().roomXiaoMang + "/" + GameManager.GetSingleton().roomDaMang;
        if(GameManager.GetSingleton().minCoin > 0 ){
            minCoin = GameManager.GetSingleton().minCoin;
        } else {
            minCoin = 200 * GameManager.GetSingleton().roomXiaoMang;
        }
        jifenpai.text = GameManager.GetSingleton().roomMinTakeMoneyRatio * minCoin + "";
        
        serviceMoney.text = GameManager.GetSingleton().serviceMoney + "";
        mslider.minValue = GameManager.GetSingleton().roomMinTakeMoneyRatio;
        mslider.maxValue = GameManager.GetSingleton().roomMaxTakeMoneyRatio;
        mslider.value= GameManager.GetSingleton().roomMinTakeMoneyRatio;

        if(GameManager.GetSingleton().isUnionCoinRoom()){ // 联盟币房间
            int coin = StaticData.unionCoin; // GameTools.GetSingleton().GetUnionCoin(GameManager.GetSingleton().unionId); 
            mymoney.text = coin + "";
            coinName.text = "联盟币";
        } else {
            mymoney.text = StaticData.gold + "";
            coinName.text = "游戏币";
        }
        
    }
}
