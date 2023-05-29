using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuessHandPopup : BasePopup {

    public Button cancelBtn;
    public Button sureBtn;
    public Toggle autoTog;

    public BetItem[] betItems;
    public BetItem[] chooseCoinItems;

    public int curCoin;
    public int betNum;
    public int last1 = -1;
    public int last2 = -1;

    void Awake() {
        Init();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button>();
        sureBtn = transform.Find("sureBtn").GetComponent<Button>();
        autoTog = transform.Find("autoToggle").GetComponent<Toggle>();

        betItems = transform.Find("betItemContent").GetComponentsInChildren <BetItem>();
        chooseCoinItems = transform.Find("betCoinContent").GetComponentsInChildren<BetItem>();

        cancelBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            gameObject.SetActive(false);
            TouchMove.Instance().isRun = true;
        });

        sureBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GetBet();
            if (betNum == -99)
            {
                Tip.Instance.showMsg("还没选择猜牌选项");
                //PopupCommon.GetSingleton().ShowView("还没选择猜牌选项");
            }
            else {
                if (curCoin == -99)
                {
                    Tip.Instance.showMsg("还没选择下注额");
                    //PopupCommon.GetSingleton().ShowView("还没选择下注额");
                }
                else {
                    NetMngr.GetSingleton().Send(InterfaceGame.guessBet, new object[] { betNum, curCoin*GameManager.GetSingleton().roomXiaoMang});
                }
            }
          
        });

        autoTog.onValueChanged.AddListener((bool b)=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (autoTog.isOn)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.autoBet, new object[] { 1 });
            }
            else {
                NetMngr.GetSingleton().Send(InterfaceGame.autoBet, new object[] { 0});
            }
        });

        betItems[0].btn.onClick.AddListener(()=> { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(betItems,0); 
        });
        betItems[1].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(betItems, 1); 
        });
        betItems[2].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(betItems, 2); 
        });
        betItems[3].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(betItems, 3); 
        });
        betItems[4].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(betItems, 4); 
        });

        chooseCoinItems[0].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(chooseCoinItems, 0); 
        });
        chooseCoinItems[1].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(chooseCoinItems, 1); 
        });
        chooseCoinItems[2].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(chooseCoinItems, 2); 
        });
        chooseCoinItems[3].btn.onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SelectBtnFun(chooseCoinItems, 3); 
        });

        NetMngr.GetSingleton().Send(InterfaceGame.guessHandInfo, new object[] { });

        //autoTog.isOn = false;

        gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetBet()
    {
        betNum = -99;
        curCoin = -99;

        for (int i = 0; i < betItems.Length; i++)
        {
            if (betItems[i].select.activeSelf)
            {
                betNum = i;
            }
        }
        for (int i = 0; i <chooseCoinItems.Length; i++)
        {
            if (chooseCoinItems[i].select.activeSelf)
            {
                curCoin = int.Parse(chooseCoinItems[i].t.text);
            }
        }


    }

    public void SelectBtnFun(BetItem[] b,int index) {
        for (int i = 0; i < b.Length; i++) {
            if (i == index)
            {
                b[i].select.SetActive(!b[i].select.activeSelf);
                if (b.Length == 5)
                {
                    this.last1 = index;
                    if (b[i].select.activeSelf)
                    {
                        b[i].btn.transform.Find("Text").GetComponent<Text>().color = Color.black;
                    }
                    else {
                        b[i].btn.transform.Find("Text").GetComponent<Text>().color = new Color(1, (float)218 / (float)255, (float)154 / (float)255);
                    }
                }
                else {
                    this.last2 = index;
                    if (b[i].select.activeSelf)
                    {
                        b[i].t.color = Color.black;
                    }
                    else {
                        b[i].t.color = new Color(1, (float)218 / (float)255, (float)154 / (float)255);
                    }
                       
                }
                
            }
            else {
                b[i].select.SetActive(false);
                if (b.Length == 5)
                {
                    b[i].btn.transform.Find("Text").GetComponent<Text>().color = new Color(1, (float)218 / (float)255, (float)154 / (float)255);
                }
                else
                {
                    b[i].t.color = new Color(1, (float)218 / (float)255, (float)154 / (float)255);
                }
               
            }
         
        }
    }

    public void GuessHandInfoCallBack(Hashtable data) {
        string[] s = data["peilv"].ToString().Split('|');
        for (int i = 0; i < 5; i++) {
            betItems[i].t.text = s[i];

        }
        string [] s2 = data["betCoin"].ToString().Split('|');

        for (int i = 0; i < 4; i++)
        {
            chooseCoinItems[i].t.text = s2[i];

        }

    }
    public void GuessBetCallBack(Hashtable data)
    {
        TouchMove.Instance().isRun = true;
        //HideView();
        gameObject.SetActive(false);
        Tip.Instance.showMsg(data["message"].ToString());
        if (data["success"].ToString() == "0")
        {
            if (this.last1 != -1)
            {
                SelectBtnFun(betItems, this.last1);
            }
            if (this.last2 != -1)
            {
                SelectBtnFun(chooseCoinItems, this.last2);
            }
        }
        //      PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false,()=> {
        //            TouchMove.Instance().isRun = true;
        //            //HideView();
        //            gameObject.SetActive(false);
        //        });

    }
    public void AutoBetCallBack(Hashtable data)
    {
        Tip.Instance.showMsg(data["message"].ToString());
//        PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        if (data["success"].ToString() == "0") {
            autoTog.isOn = false;
        }

    }

    public void ReceiveGuessCallBack(Hashtable h)
    {

        string coin = h["coin"].ToString();
      
        int netid = int.Parse(h["deskId"].ToString());
   
        int localid = GameManager.GetSingleton().netTolocal(netid);
        PlayInfo pinfo = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>();
        pinfo.setLeftMoney(coin);

        if (this.last1 != -1)
        {
            SelectBtnFun(betItems, this.last1);
        }
        if (this.last2 != -1)
        {
            SelectBtnFun(chooseCoinItems, this.last2);
        }
    }

}
