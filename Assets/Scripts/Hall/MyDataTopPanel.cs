using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MyDataTopPanel : BasePlane {

    //private Button backBtn;
    private Toggle[] topGroup;
    private Toggle[] middleGroup1;
    private Toggle[] middleGroup2;
    private GameObject myData;
    private GameObject opponent;
    private GameObject allin;
    private GameObject paiZu;

    public DataIcon ruChi;
    public DataIcon victory;
    public DataIcon fanAdd;
    public DataIcon againAdd;
    public DataIcon persistentAdd;
    public DataIcon tanPai;
    public DataIcon tanVictory;
    public DataIcon kanfanVictory;
    public DataIcon fanVictory;
	public Dictionary<int, DataIcon> dataicons = new Dictionary<int, DataIcon>();
    private Text jijin;
    private Text jushu;
    private Text shoushu;
    private Text zongdairu;
    private Text changjundairu;
    private Text changjunzhanji;
    private Text danchangzuigaoyingli;
    private Text danshouzuigaoyingli;
    private Text n;

    private Text iconText;
    private Allin shouShou;
    private Allin win;
    private Allin fail;

    public GameObject[] PaiData;


    private const int showPage = 10;
    private string oppontentType = "1";
    private string myDataType = "1";
    private int currPage = 1;
    private int sumPage = 1;
    private ChatScroller chatScroller;

    Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
        chatScroller = transform.Find("Bottom2/ChatScroller").GetComponent<ChatScroller>();
        //backBtn = transform.Find("Top/Back").GetComponent<Button>();
        topGroup = transform.Find("Top/Gruop").GetComponentsInChildren<Toggle>();
        myData = transform.Find("Bottom1").gameObject;
        opponent = transform.Find("Bottom2").gameObject;
        allin = transform.Find("Bottom3").gameObject;
        paiZu = transform.Find("Bottom4").gameObject;
        middleGroup1 = myData.transform.Find("Middle").GetComponentsInChildren<Toggle>();
        middleGroup2 = opponent.transform.Find("Middle").GetComponentsInChildren<Toggle>();
        ruChi = myData.transform.Find("Scroll View/Viewport/Content/Panel/ruChi").GetComponent<DataIcon>();
        victory = myData.transform.Find("Scroll View/Viewport/Content/Panel/victory").GetComponent<DataIcon>();
        fanAdd = myData.transform.Find("Scroll View/Viewport/Content/Panel/FanAdd").GetComponent<DataIcon>();
        againAdd = myData.transform.Find("Scroll View/Viewport/Content/Panel/againAdd").GetComponent<DataIcon>();
        persistentAdd = myData.transform.Find("Scroll View/Viewport/Content/Panel/persistentAdd").GetComponent<DataIcon>();
        tanPai = myData.transform.Find("Scroll View/Viewport/Content/Panel/tanPai").GetComponent<DataIcon>();
        tanVictory = myData.transform.Find("Scroll View/Viewport/Content/Panel/tanVictory").GetComponent<DataIcon>();
        kanfanVictory = myData.transform.Find("Scroll View/Viewport/Content/Panel/kanfanVictory").GetComponent<DataIcon>();
        fanVictory = myData.transform.Find("Scroll View/Viewport/Content/Panel/fanVictory").GetComponent<DataIcon>();
        jijin = myData.transform.Find("Scroll View/Viewport/Content/Panel/jijin/value").GetComponent<Text>();
        jushu = myData.transform.Find("Scroll View/Viewport/Content/Panel/jushu/value").GetComponent<Text>();
        shoushu = myData.transform.Find("Scroll View/Viewport/Content/Panel/shoushu/value").GetComponent<Text>();
        zongdairu = myData.transform.Find("Scroll View/Viewport/Content/Panel/zongdairu/value").GetComponent<Text>();
        changjundairu = myData.transform.Find("Scroll View/Viewport/Content/Panel/changjundairu/value").GetComponent<Text>();
        changjunzhanji = myData.transform.Find("Scroll View/Viewport/Content/Panel/changjunzhanji/value").GetComponent<Text>();
        danchangzuigaoyingli = myData.transform.Find("Scroll View/Viewport/Content/Panel/danchangzuigaoyingli/value").GetComponent<Text>();
        danshouzuigaoyingli = myData.transform.Find("Scroll View/Viewport/Content/Panel/danshouzuigaoyingli/value").GetComponent<Text>();

        n = opponent.transform.Find("Text").GetComponent<Text>();

		dataicons.Add (0, ruChi);
		dataicons.Add (1, victory);
		dataicons.Add (2, fanAdd);
		dataicons.Add (3, againAdd);
		dataicons.Add (4, persistentAdd);
		dataicons.Add (5, tanPai);

        iconText = allin.transform.Find("Image/Text").GetComponent<Text>();
        shouShou = allin.transform.Find("Allin").GetComponent<Allin>();
        win = allin.transform.Find("Victory").GetComponent<Allin>();
        fail = allin.transform.Find("Fail").GetComponent<Allin>();

        //backBtn.onClick.AddListener(() =>
        //{
        //    HallManager.GetSingleton().planeManager.RemoveTopPlane();
        //});
        topGroup[0].onValueChanged.AddListener((isOn) =>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn){TopShow(0);}
        });
        topGroup[1].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { TopShow(1); } 
        });
        topGroup[2].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { TopShow(2); } 
        });
        topGroup[3].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { TopShow(3); } 
        });
        middleGroup1[0].onValueChanged.AddListener((isOn) =>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn){MiddleShow1(0);}
        });
        middleGroup1[1].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { MiddleShow1(1); } 
        });
        middleGroup1[2].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { MiddleShow1(2); } 
        });
        middleGroup2[0].onValueChanged.AddListener((isOn) =>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn){MiddleShow2(0);}
        });
        middleGroup2[1].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { MiddleShow2(1); } 
        });
        middleGroup2[2].onValueChanged.AddListener((isOn) => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn) { MiddleShow2(2); } 
        });
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {

    }
    public void Reflash()
    {
        //if (currPage<=sumPage)
        //{
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetOpponentInfo, new object[] { oppontentType, currPage, showPage });
        //}
    }

    public void SetMyselfInfo(Hashtable data)
    {
        ruChi.Reset();
        victory.Reset();
        fanAdd.Reset();
        againAdd.Reset();
        persistentAdd.Reset();
        tanPai.Reset();
		ruChi.SetData(dataicons, float.Parse(data["ruChi"].ToString()),"入池率");
		victory.SetData(dataicons, float.Parse(data["victory"].ToString()), "入池胜率");
		fanAdd.SetData(dataicons, float.Parse(data["fanAdd"].ToString()), "翻前加注");
		againAdd.SetData(dataicons, float.Parse(data["againAdd"].ToString()), "再次加注");
		persistentAdd.SetData(dataicons, float.Parse(data["persistentAdd"].ToString()), "持续加注");
		tanPai.SetData(dataicons, float.Parse(data["tanPai"].ToString()), "摊牌率");
        jijin.text = data["excited"].ToString();
        jushu.text = data["gameCount"].ToString();
        shoushu.text = data["shuCount"].ToString();
        zongdairu.text = data["totalDairu"].ToString();
        danchangzuigaoyingli.text = data["theGameScoreMax"].ToString();
        danshouzuigaoyingli.text = data["theShuScoreMax"].ToString();
        
    }
    public void SetGetOpponentInfo(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list= data["opponents"] as ArrayList;
        
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 2;
            datas[i]["URL"] = ((Hashtable)list[i])["playerURL"].ToString();
            datas[i]["Name"] = ((Hashtable)list[i])["playerName"].ToString();
            datas[i]["ShouShu"] = ((Hashtable)list[i])["playerShuCount"].ToString();
            datas[i]["Fail"] = ((Hashtable)list[i])["playerWinCount"].ToString();
            datas[i]["Win"] = ((Hashtable)list[i])["playerFailCount"].ToString();
            datas[i]["Score"] = ((Hashtable)list[i])["playerScore"].ToString();
        }
        if (currPage == 1)
        {
            chatScroller.ClearList();
            chatScroller.datas.Clear();
            chatScroller.modelList.Clear();
            chatScroller.SetDatas(datas);
            if (list.Count == 0)
            {
                n.gameObject.SetActive(true);
            }
            else
            {
                n.gameObject.SetActive(false);
            }
        }
        else
        {
            chatScroller.SetDatas(0, datas);
        }
    }
    public void SetAllinInfo(Hashtable data)
    {
        iconText.text = data["accumulate"].ToString();
        if (int.Parse(data["allinShu"].ToString())==0)
        {
            shouShou.SetData(int.Parse(data["allinShu"].ToString()), int.Parse(data["allinShu"].ToString()), "Allin", "手数");
            win.SetData(int.Parse(data["allinShu"].ToString()), int.Parse(data["allinWin"].ToString()), "0%", "获胜");
            fail.SetData(int.Parse(data["allinShu"].ToString()), int.Parse(data["allinFail"].ToString()), "0%", "失利");
        }
        else
        {
            shouShou.SetData(int.Parse(data["allinShu"].ToString()), int.Parse(data["allinShu"].ToString()), "Allin", "手数");
            win.SetData(int.Parse(data["allinShu"].ToString()), int.Parse(data["allinWin"].ToString()), Mathf.Round((float.Parse(data["allinWin"].ToString()) / float.Parse(data["allinShu"].ToString()))*100) + "%", "获胜");
            fail.SetData(int.Parse(data["allinShu"].ToString()), int.Parse(data["allinFail"].ToString()), Mathf.Round((float.Parse(data["allinFail"].ToString()) / float.Parse(data["allinShu"].ToString()))*100) + "%", "失利");
        }
        
    }
    public void SetPaiData(Hashtable data)
    {
        ArrayList list = data["PaiData"] as ArrayList;
        for (int i = list.Count-1; i >=0; i--)
        {
            PaiData[i].transform.Find("value1").GetComponent<Text>().text = ((Hashtable)list[i])["count"].ToString() + "(获胜" + ((Hashtable)list[i])["win"].ToString() + ")";
            PaiData[i].transform.Find("value2").GetComponent<Text>().text = float.Parse(((Hashtable)list[i])["winCount"].ToString())*100+"%";
            PaiData[i].transform.Find("value3").GetComponent<Text>().color = StaticFunction.Getsingleton().GetColor(int.Parse(((Hashtable)list[i])["score"].ToString()));
            PaiData[i].transform.Find("value3").GetComponent<Text>().text = (int.Parse(((Hashtable)list[i])["score"].ToString())>0?((Hashtable)list[i])["score"].ToString() :((Hashtable)list[i])["score"].ToString());
        }
    }
    public void TopShow(int value)
    {
        switch (value)
        {
            case 0:
                myData.SetActive(true);
                opponent.SetActive(false);
                allin.SetActive(false);
                paiZu.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceMain.GetMyselfInfo, new object[] {myDataType });
                break;
            case 1:
                myData.SetActive(false);
                opponent.SetActive(true);
                allin.SetActive(false);
                paiZu.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceMain.GetOpponentInfo, new object[] { oppontentType,currPage,showPage });
                break;
            case 2:
                myData.SetActive(false);
                opponent.SetActive(false);
                allin.SetActive(true);
                paiZu.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceMain.GetAllinInfo, new object[] { });
                break;
            case 3:
                myData.SetActive(false);
                opponent.SetActive(false);
                allin.SetActive(false);
                paiZu.SetActive(true);
                NetMngr.GetSingleton().Send(InterfaceMain.GetPaiData, new object[] {});
                break;
        }
    }
    public void MiddleShow1(int value)
    {
        switch (value)
        {
            case 0:
                myDataType = "1";
                break;
            case 1:
                myDataType = "7";
                break;
            case 2:
                myDataType = "30";
                break;
        }
        currPage = 1;
        NetMngr.GetSingleton().Send(InterfaceMain.GetMyselfInfo, new object[] { myDataType });
    }
    public void MiddleShow2(int value)
    {
        switch (value)
        {
            case 0:
                oppontentType = "1";
                break;
            case 1:
                oppontentType = "7";
                break;
            case 2:
                oppontentType = "30";
                break;
        }
        currPage = 1;
        NetMngr.GetSingleton().Send(InterfaceMain.GetOpponentInfo, new object[] { oppontentType, currPage, showPage });
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetMyselfInfo, new object[] { myDataType });
    }

    public override void OnAddStart()
    {
        currPage = 1;
        oppontentType = "1";
        myDataType = "1";

    }

    public override void OnRemoveComplete()
    {
        for (int i = 0; i < topGroup.Length; i++)
        {
            if (i == 0)
            {
                topGroup[i].isOn = true;
            }
            else
            {
                topGroup[i].isOn = false;
            }
        }
        for (int i = 0; i < middleGroup1.Length; i++)
        {
            if (i == 0)
            {
                middleGroup1[i].isOn = true;
            }
            else
            {
                middleGroup1[i].isOn = false;
            }
        }
        for (int i = 0; i < middleGroup2.Length; i++)
        {
            if (i == 0)
            {
                middleGroup2[i].isOn = true;
            }
            else
            {
                middleGroup2[i].isOn = false;
            }
        }
    }

    public override void OnRemoveStart()
    {

    }
}
