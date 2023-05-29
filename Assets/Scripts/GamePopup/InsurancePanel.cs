using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InsurancePanel : BasePlane
{
    public Button closeBtn;
    public RawImage[] commonCards = new RawImage[4];

    public Text luohouwanjiaText; //落后玩家
    public Text fanchaopaiText;//反超牌
    public Text zhuipingpaiText;//追平牌

    public Transform luohouwanjiaContent;
    public Transform fanchaopaiContent;
    public Transform zhuipingpaiContent;
    public Toggle allSelectTog;

    public Text dichi;//底池
    public Text daojishi;//倒计时

    public Text peifu;//赔付额 
    public Text toubao;//投保额
    public Text peilv;//赔率

    public Slider baoxian;

    public Button btn1;//
    public Button btn2;//
    public Text baobentext;
    public Text benlitext;

    public Button buyBtn;
    public Button cancelBtn;

    public Button addTimeBtn;

    public Text curCoin;//
    public Text buyInsurance;

    public Text cardType;//牌型

    public RawImage[] myCards;

    public List<card> selectedCardZ = new List<card>();
    public List<card> selectedCardF = new List<card>();
    
    public List<card> allCardF = new List<card>();
    public List<card> allCardZ = new List<card>();

    public Dictionary<string, card> cardPos = new Dictionary<string, card>();
    private card cardItem;

    private bool isSelectOuts=true;

    public int selectPaiCountZ = 0;
    public int selectPaiCountF = 0;
    public float odds = 0;
    public int dengli = 0;
    public int baoben = 0;
    public float timeconst = 400;
    public float timer = 400;
    public bool isStart = false;
    public bool isMine = false;
    public int fanchaoCount = 0;
    public int zhuipingCount = 0;

    public string pai = "";

    public string isAllSe = "1";

    public bool isRun = false;

    void Awake()
    {
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        commonCards = transform.Find("Cards").GetComponentsInChildren<RawImage>();

        luohouwanjiaContent = transform.Find("luohouwanjiaView/Viewport/Content");
        fanchaopaiContent = transform.Find("fanchaopaiView/Viewport/Content/FanChao");
        zhuipingpaiContent = transform.Find("fanchaopaiView/Viewport/Content/ZhuiPing");

        luohouwanjiaText = transform.Find("luohouwanjia").GetComponent<Text>();
        fanchaopaiText = transform.Find("fanchaopaiView/Viewport/Content/FanChaoPai").GetComponent<Text>();
        zhuipingpaiText = transform.Find("fanchaopaiView/Viewport/Content/ZhuiPingPai").GetComponent<Text>();

        allSelectTog = transform.Find("Toggle").GetComponent<Toggle>();

        dichi = transform.Find("dichi/Text").GetComponent<Text>();
        daojishi = transform.Find("daojishi/Text").GetComponent<Text>();

        peifu = transform.Find("peifu/Text").GetComponent<Text>();
        toubao = transform.Find("toubao/Text").GetComponent<Text>();
        peilv = transform.Find("peilv/Text").GetComponent<Text>();

        baoxian = transform.Find("baoxianSlider").GetComponent<Slider>();

        btn1 = transform.Find("btn1").GetComponent<Button>();
        btn2 = transform.Find("btn2").GetComponent<Button>();
        baobentext = transform.Find("btn1/Text").GetComponent<Text>();
        benlitext = transform.Find("btn2/Text").GetComponent<Text>();

        buyBtn = transform.Find("BuyBtn").GetComponent<Button>();
        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>(); 
        addTimeBtn = transform.Find("addTimeBtn").GetComponent<Button>();
        curCoin = transform.Find("mycoin/Text").GetComponent<Text>();

		myCards = transform.Find("myCards").GetComponentsInChildren<RawImage>();

        cardType = transform.Find("myCards/cardType").GetComponent<Text>();
		cardType.gameObject.SetActive (false);
        buyInsurance = transform.Find("BuyBtn/Text").GetComponent<Text>();

        closeBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.CancelInsurance, new object[] { });
            }
            else
            {
                StaticFunction.Getsingleton().isSync = false;
                GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
                GameUIManager.GetSingleton().insuranceInfoPopup.ShowView();
                GameUIManager.GetSingleton().insuranceInfoPopup.timer = timer;
                GameUIManager.GetSingleton().insuranceInfoPopup.timerConst = timer;
            }
            
        });

        baoxian.onValueChanged.AddListener((value) =>
        {
            if (isMine)
            {
                toubao.text = value.ToString();
                peilv.text = odds.ToString();
                peifu.text = (Mathf.FloorToInt(float.Parse(toubao.text) * float.Parse(peilv.text))).ToString();
                buyInsurance.text = toubao.text;
            }
        });
        btn1.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine)
            {
                baoxian.value = baoben;
                toubao.text = baoben.ToString();
                peilv.text = odds.ToString();
                peifu.text = (Mathf.FloorToInt(float.Parse(toubao.text) * float.Parse(peilv.text))).ToString();
                buyInsurance.text = toubao.text;
                NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance, new object[] { pai, baoxian.value.ToString(), peifu.text, toubao.text, peilv.text, timer.ToString(),baoxian.maxValue.ToString() });
            }
        });
        btn2.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine)
            {
                baoxian.value = dengli;
                toubao.text = dengli.ToString();
                peilv.text = odds.ToString();
                peifu.text = (Mathf.FloorToInt(float.Parse(toubao.text) * float.Parse(peilv.text))).ToString();
                buyInsurance.text = toubao.text;
                NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance, new object[] { pai, baoxian.value.ToString(), peifu.text, toubao.text, peilv.text, timer.ToString(),baoxian.maxValue.ToString() });
            }
        });
        addTimeBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine)
            {
                //PopupCommon.GetSingleton().ShowView("是否加时", null, true, delegate
                //{
                    NetMngr.GetSingleton().Send(InterfaceGame.buyInsuranceAddTime, new object[] { });
                //});
            }
        });
        allSelectTog.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine&&this.isSelectOuts)
            {
                if (isOn)
                {
                    selectedCardZ.Clear();
                    selectedCardF.Clear();
                    for (int i = 0; i < allCardZ.Count; i++)
                    {
                        allCardZ[i].isSelected = true;
                        allCardZ[i].selected.gameObject.SetActive(true);
                        selectedCardZ.Add(allCardZ[i]);
                    }
                    for (int i = 0; i < allCardF.Count; i++)
                    {
                        allCardF[i].isSelected = true;
                        allCardF[i].selected.gameObject.SetActive(true);
                        selectedCardF.Add(allCardF[i]);
                    }
                    isAllSe = "-1";
                    buyBtn.interactable = true;
                }
                else
                {
                    for (int i = 0; i < allCardF.Count; i++)
                    {
                        allCardF[i].isSelected = false;
                        allCardF[i].selected.gameObject.SetActive(false);
                        selectedCardF.Clear();
                    }
                    for (int i = 0; i < allCardZ.Count; i++)
                    {
                        allCardZ[i].isSelected = false;
                        allCardZ[i].selected.gameObject.SetActive(false);
                        selectedCardZ.Clear();
                    }
                    isAllSe = "-2";
                    baoxian.value = baoxian.minValue;
                    toubao.text = "0";
                    peilv.text = "0";
                    peifu.text = "0";
                    buyInsurance.text = "1";
                    buyBtn.interactable = false;
                    btn1.gameObject.SetActive(false);
                    btn2.gameObject.SetActive(false);
                    NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance, new object[] { "-2", baoxian.value.ToString(), peifu.text, toubao.text, peilv.text, timer.ToString(),baoxian.maxValue.ToString() });
                }
                selectPaiCountF = selectedCardF.Count;
                selectPaiCountZ = selectedCardZ.Count;
                fanchaopaiText.text = "反超牌"+selectPaiCountF+"/" + fanchaoCount;
                zhuipingpaiText.text = "追平牌" + selectPaiCountZ + "/" + zhuipingCount;
                NetMngr.GetSingleton().Send(InterfaceGame.GetBaoBenDengLi, new object[] { (selectPaiCountF+selectPaiCountZ).ToString() });
            }
        });
        buyBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine)
            {
                string str = "";
                for (int i = 0; i < selectedCardF.Count; i++)
                {
                    str += selectedCardF[i].id + "|";
                }
                for (int i = 0; i < selectedCardZ.Count; i++)
                {
                    str += selectedCardZ[i].id + "|";
                }
                str = str.Substring(0, str.Length - 1);
                NetMngr.GetSingleton().Send(InterfaceGame.buyInsurance, new object[] { str, toubao.text });
            }
            
        });
        gameObject.SetActive(false);

        cancelBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.CancelInsurance, new object[] { });
            }
            else
            {
                StaticFunction.Getsingleton().isSync = false;
                GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
                GameUIManager.GetSingleton().insuranceInfoPopup.ShowView();
                GameUIManager.GetSingleton().insuranceInfoPopup.timer = timer;
                GameUIManager.GetSingleton().insuranceInfoPopup.timerConst = timer;
            }
            
        });

    }

    void Start()
    {

    }

    void Update()
    {
        if (isStart)
        {
            timer -= Time.deltaTime;
            daojishi.text = ((int)timer).ToString() + "s";
            if (timer <= 0)
            {
                isStart = false;
                timer = timeconst;
                if (isMine)
                {
                    GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
                }
            }
        }
        if (isRun)
        {
            TouchMove.Instance().isRun = false;
        }
    }
    public void reSet()
    {
        timer = timeconst;
        isStart = false;
        daojishi.text = ((int)timer).ToString() + "s";
    }

    public void SetData(Hashtable data)
    {
        ReSetInsurancePanel(true);
        if (data["canSelectOuts"].ToString() == "False")
        {
            this.isSelectOuts = false;
        }
        else
        {
            this.isSelectOuts = true;
        }
        if (data.ContainsKey("little"))
        {
            curCoin.text = data["little"].ToString();
        }
        isMine = data["isMine"].ToString() == "1" ? true : false;
        dichi.text = data["diChi"].ToString();
        baoxian.maxValue = float.Parse(data["max"].ToString());
        baoxian.minValue = float.Parse(data["min"].ToString());
        timeconst = float.Parse(data["time"].ToString());
        reSet();
        isStart = true;
        string[] strs = data["publicPai"].ToString().Split('|');
        if (strs.Length == 3)
        {
            commonCards[3].gameObject.SetActive(false);
        }
        else
        {
            commonCards[3].gameObject.SetActive(true);
        }
        for (int i = 0; i < strs.Length; i++)
        {
            commonCards[i].texture = StaticFunction.Getsingleton().SetPai(strs[i]);
        }
        string[] strss = data["myPai"].ToString().Split('|');

		if (strss.Length == 2) {
			myCards[0].texture = StaticFunction.Getsingleton().SetPai(strss[0]);
			myCards[2].texture = StaticFunction.Getsingleton().SetPai(strss[1]);

			myCards [1].gameObject.SetActive (false);
			myCards [3].gameObject.SetActive (false);
		}
		else if (strss.Length == 4) {

			myCards [1].gameObject.SetActive (true);
			myCards [3].gameObject.SetActive (true);

			for (int i = 0; i < strss.Length; i++)
			{
				myCards[i].texture = StaticFunction.Getsingleton().SetPai(strss[i]);
			}
		}

        ClearList(luohouwanjiaContent);
        ArrayList withList = data["failplayers"] as ArrayList;
        luohouwanjiaText.text = "落后玩家(" +withList.Count+ ")";
        UnityEngine.Object objItem = Resources.Load("HallItem/CardItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(luohouwanjiaContent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            CardItem cardItem = go.AddComponent<CardItem>();
            cardItem.SetData(withList[i] as Hashtable);
        }
        ClearList(fanchaopaiContent);
        ArrayList list = data["fanChaos"] as ArrayList;
        fanchaoCount = list.Count;
        fanchaopaiText.text = "反超牌" + "0/" + fanchaoCount;
        UnityEngine.Object objItems = Resources.Load("HallItem/card");
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(objItems) as GameObject;
            go.transform.SetParent(fanchaopaiContent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            card cardd = go.AddComponent<card>();
            cardd.SetData(list[i] as Hashtable,1);
            cardd.isSelectOuts = this.isSelectOuts;
        }
        ClearList(zhuipingpaiContent);
        ArrayList list1 = data["zhuiPings"] as ArrayList;
        if (list1.Count == 0)
        {
            zhuipingpaiText.text = "";
        }
        zhuipingCount = list1.Count;
        zhuipingpaiText.text = "追平牌" + "0/" + zhuipingCount;
        UnityEngine.Object objItems1 = Resources.Load("HallItem/card");
        for (int i = 0; i < list1.Count; i++)
        {
            GameObject go = Instantiate(objItems) as GameObject;
            go.transform.SetParent(zhuipingpaiContent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            card cardd = go.AddComponent<card>();
            cardd.SetData(list1[i] as Hashtable,2);
            cardd.isSelectOuts = this.isSelectOuts;
        }
        //peifu.text = (int.Parse(toubao.text) * int.Parse(peilv.text)).ToString();
        if (isMine)
        {
            baoxian.interactable = true;
            fanchaopaiText.text = "反超牌" + selectPaiCountF + "/" + fanchaoCount;
            zhuipingpaiText.text = "追平牌" + selectPaiCountZ + "/" + zhuipingCount;
            NetMngr.GetSingleton().Send(InterfaceGame.GetBaoBenDengLi, new object[] { (list.Count + list1.Count).ToString() });
        }
        else
        {
            baoxian.interactable = false;
        }
    }

    public void endDrog()
    {
        if (isMine)
        {
            pai = "";
            for (int i = 0; i < selectedCardF.Count; i++)
            {
                pai += selectedCardF[i].id + "|";
            }
            if (pai.Length != 0)
            {
                pai = pai.Substring(0, pai.Length - 1);
            }
            pai += "&";
            for (int i = 0; i < selectedCardZ.Count; i++)
            {
                pai += selectedCardZ[i].id + "|";
            }
            if (pai.Length != 0)
            {
                pai = pai.Substring(0, pai.Length - 1);
            }
            NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance, new object[] { pai, baoxian.value.ToString(), peifu.text, toubao.text, peilv.text, timer.ToString(),baoxian.maxValue.ToString() });
        }
    }

    public void SyncInsurance(Hashtable data)
    {
        //重置保险页面
        ReSetInsurancePanel(false);
        //全选
        if (data["pai"].ToString() == "-1")
        {
            allSelectTog.isOn = true;
            selectedCardF.Clear();
            selectedCardZ.Clear();
            for (int i = 0; i < allCardF.Count; i++)
            {
                allCardF[i].isSelected = true;
                allCardF[i].selected.gameObject.SetActive(true);
                selectedCardF.Add(allCardF[i]);
            }
            for (int i = 0; i < allCardZ.Count; i++)
            {
                allCardZ[i].isSelected = true;
                allCardZ[i].selected.gameObject.SetActive(true);
                selectedCardZ.Add(allCardZ[i]);
            }
        }//不全选
        else if (data["pai"].ToString() == "-2")
        {
            allSelectTog.isOn = false;
            for (int i = 0; i < allCardF.Count; i++)
            {
                allCardF[i].isSelected = false;
                allCardF[i].selected.gameObject.SetActive(false);
                selectedCardF.Clear();
            }
            for (int i = 0; i < allCardZ.Count; i++)
            {
                allCardZ[i].isSelected = false;
                allCardZ[i].selected.gameObject.SetActive(false);
                selectedCardZ.Clear();
            }
        }//单个选择
        else
        {
            allSelectTog.isOn = false;
            string[] s = data["pai"].ToString().Split('&');
            string[] str = s[0].Split('|');
            for (int i = 0; i < cardPos.Count; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    if (cardPos.TryGetValue(str[j], out cardItem))
                    {
                        if (cardItem.id == str[j])
                        {
                            cardItem.isSelected = true;
                            cardItem.selected.gameObject.SetActive(cardItem.isSelected);
                            if (cardItem.isSelected)
                            {
                                if (!selectedCardF.Contains(cardItem))
                                {
                                    GameUIManager.GetSingleton().insurancePanel.selectedCardF.Add(cardItem);
                                }
                            }
                            else
                            {
                                GameUIManager.GetSingleton().insurancePanel.selectedCardF.Remove(cardItem);
                            }
                        }
                    }
                }
            }
            
            if (s.Length==2)
            {
                string[] str1 = s[1].Split('|');
                if (str1.Length!=0)
                {
                    for (int i = 0; i < cardPos.Count; i++)
                    {
                        for (int j = 0; j < str1.Length; j++)
                        {
                            if (cardPos.TryGetValue(str1[j], out cardItem))
                            {
                                if (cardItem.id == str1[j])
                                {
                                    cardItem.isSelected = true;
                                    cardItem.selected.gameObject.SetActive(cardItem.isSelected);
                                    if (cardItem.isSelected)
                                    {
                                        if (!selectedCardZ.Contains(cardItem))
                                        {
                                            GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Add(cardItem);
                                        }
                                    }
                                    else
                                    {
                                        GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Remove(cardItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        selectPaiCountF = selectedCardF.Count;
        selectPaiCountZ = selectedCardZ.Count;
        fanchaopaiText.text = "反超牌" + selectPaiCountF + "/" + fanchaoCount;
        zhuipingpaiText.text = "追平牌" + selectPaiCountZ + "/" + zhuipingCount;
        timeconst = float.Parse(data["timer"].ToString());
        timer = timeconst;
        peifu.text = data["peifu"].ToString();
        toubao.text = data["toubao"].ToString();
        peilv.text = data["odds"].ToString();
        odds = float.Parse(data["odds"].ToString());
        baoxian.value = float.Parse(data["add"].ToString());
        buyBtn.gameObject.SetActive(false);
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        addTimeBtn.gameObject.SetActive(false);
        curCoin.transform.parent.gameObject.SetActive(false);
        print(cardPos.Count + "Other");
    }

    public void ReSetInsurancePanel(bool s)
    {
        if (s)
        {
            selectedCardF.Clear();
            selectedCardZ.Clear();
            cardPos.Clear();
            allCardF.Clear();
            allCardZ.Clear();
        }
        else
        {
            selectedCardF.Clear();
            selectedCardZ.Clear();
            for (int i = 0; i < allCardF.Count; i++)
            {
                allCardF[i].isSelected = false;
                allCardF[i].selected.gameObject.SetActive(false);
            }
            for (int i = 0; i < allCardZ.Count; i++)
            {
                allCardZ[i].isSelected = false;
                allCardZ[i].selected.gameObject.SetActive(false);
            }
        }
        baoxian.interactable = true;
        baoxian.value = baoxian.minValue;
        toubao.text = "1";
        peilv.text = "0";
        peifu.text = "0";
        buyInsurance.text = toubao.text;
        buyBtn.gameObject.SetActive(s);
        btn1.gameObject.SetActive(s);
        btn2.gameObject.SetActive(s);
        addTimeBtn.gameObject.SetActive(s);
        curCoin.transform.parent.gameObject.SetActive(s);
    }

    /// <summary>
    /// 清空列表
    /// </summary>
    private void ClearList(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }


    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        isRun = true;
        TouchMove.Instance().isRun = false;
    }

    public override void OnRemoveComplete()
    {
        
    }

    public override void OnRemoveStart()
    {
        isRun = false;
        TouchMove.Instance().isRun = true;
    }
}
