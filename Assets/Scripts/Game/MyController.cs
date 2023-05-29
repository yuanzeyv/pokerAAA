using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyController : MonoBehaviour {

    private Button startGamebtn;
    private Transform tips;
    public Transform mytrunTransform;
    public Transform nomytrunTransform;
    Transform InputjiazhuTransorm;
    Transform InputjiazhuTransorm_btnsTrans;
    Transform mang_diTransfom;
    Text inputjiazhu_text;
    private Text tiptext;
    private Button pass;
    private Button autopass;
    private Button qipai;
    Button quanxiaBtn;
    Button quanxiaBtn_noTurn;
    private Image qipaichild;
    private Text qipaiText;
    private Button genzhubtn;
    private Button genzhubtn_noturn;
    private Button kanpaibtn;
    private Text genzhuText;
    private Text genzhuText_noturn;
    private Button jiazhubtn;
    private Button jingquejiazhubtn;
    private Button bilijiazhubtn;
    private Button jingquecancelbtn;
    private Button mang1;
    private Button mang2;
    private Button mang3;
    private Button mang4;

    public Transform addtransfrom;
    private Text addTopText;
    private Text addSliderText;
    private Button jiazhusurebtn;
    private Button jingquejiazhusurebtn;
    private Slider slider;
    float timeQipaiStep;
    float timeQipai;
    int  CanBetMaxCoin;
    int  CanBetMinCoin;
    int preSomeOneIndex;
    string genzhuMoney;
    bool isNeedConfirm;
    public bool startRunTime;
    bool isCancelmodel;
    Button btnDelayTime;
    AudioSource ads;
    int oneDichi;
    string inputJiazhuType = "";
    
    public int curTurn; // 当前回合

    private void Awake()
    {

        oneDichi = 1;
        curTurn = 0;
        preSomeOneIndex = -1;
        startRunTime = false;
        isCancelmodel = false;
        timeQipai = 0f;
        startGamebtn = transform.Find("startgame").GetComponent<Button>();
        btnDelayTime = transform.Find("myturn/time").GetComponent<Button>();
        quanxiaBtn = transform.Find("myturn/quanxia").GetComponent<Button>();
        ads = transform.Find("ads").GetComponent<AudioSource>();
     
        quanxiaBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2,"-1" });
        });
        quanxiaBtn_noTurn = transform.Find("nomyturn/quanxia").GetComponent<Button>();
   
        btnDelayTime.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.AddColdTime,new object[] { GameManager.GetSingleton().everyDelayTime+"" });
        });
        startGamebtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StartGame();
        });
        InputjiazhuTransorm = transform.Find("myturn/Inputjiazhu");
        InputjiazhuTransorm.gameObject.SetActive(false);
        InputjiazhuTransorm_btnsTrans = InputjiazhuTransorm.Find("btns");
        for(int i = 0; i < InputjiazhuTransorm_btnsTrans.childCount; i++)
        {
            Button btn = InputjiazhuTransorm_btnsTrans.GetChild(i).GetComponent<Button>();
            string temp = btn.name;
            Debug.Log("btn.name = " + btn.name);
            btn.onClick.AddListener(()=> {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                string text = inputjiazhu_text.text;
                if(text == "0")
                {
                    inputjiazhu_text.text = temp;
                }
                else
                    inputjiazhu_text.text = text + temp;

                int num = int.Parse(inputjiazhu_text.text);
                if(inputJiazhuType == "bili"){
                    if(num > 100) num = 100; // 不能超过100
                    num = Mathf.FloorToInt(num * GameManager.GetSingleton().gameDichi / 100);
                }

                if (num < CanBetMinCoin || num > CanBetMaxCoin)
                {
                    jingquecancelbtn.gameObject.SetActive(true);
                    jingquejiazhusurebtn.gameObject.SetActive(false);
                }
                else
                {
                    jingquecancelbtn.gameObject.SetActive(false);
                    jingquejiazhusurebtn.gameObject.SetActive(true);
                }
            });
        }
        InputjiazhuTransorm.Find("close").GetComponent<Button>().onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            InputjiazhuTransorm.gameObject.SetActive(false);
            mang_diTransfom.gameObject.SetActive(true);
            inputjiazhu_text.text = "";
        });
        InputjiazhuTransorm.Find("back").GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            string temp = inputjiazhu_text.text;
            if (temp.Length == 1)
            {
                inputjiazhu_text.text = "0";
                jingquecancelbtn.gameObject.SetActive(true);
                jingquejiazhusurebtn.gameObject.SetActive(false);
            }
            else
            {
                temp = temp.Substring(0,temp.Length-1);
                inputjiazhu_text.text = temp;
            }
            if (int.Parse(inputjiazhu_text.text) < CanBetMinCoin || int.Parse(inputjiazhu_text.text) > CanBetMaxCoin)
            {
                jingquecancelbtn.gameObject.SetActive(true);
                jingquejiazhusurebtn.gameObject.SetActive(false);
            }
            else
            {
                jingquecancelbtn.gameObject.SetActive(false);
                jingquejiazhusurebtn.gameObject.SetActive(true);
            }
        });
        jingquecancelbtn = InputjiazhuTransorm.Find("Cancel").GetComponent<Button>();
        jingquecancelbtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            InputjiazhuTransorm.gameObject.SetActive(false);
            mang_diTransfom.gameObject.SetActive(true);
            inputjiazhu_text.text = "";
        });
        jingquecancelbtn.gameObject.SetActive(false);
        tips = transform.Find("tips");
        tiptext = tips.Find("Text").GetComponent<Text>();
        pass = transform.Find("nomyturn/pass").GetComponent<Button>();
        autopass = transform.Find("nomyturn/autopass").GetComponent<Button>();
        qipai = transform.Find("myturn/qipai").GetComponent<Button>();
        genzhubtn = transform.Find("myturn/genzhu").GetComponent<Button>();
        genzhubtn_noturn = transform.Find("nomyturn/genzhu").GetComponent<Button>();
        kanpaibtn = transform.Find("myturn/kanpai").GetComponent<Button>();
        jiazhubtn = transform.Find("myturn/jiazhu").GetComponent<Button>();
        inputjiazhu_text = InputjiazhuTransorm.Find("Text").GetComponent<Text>();
        jingquejiazhubtn = transform.Find("myturn/jinguqejiazhu").GetComponent<Button>();
        bilijiazhubtn = transform.Find("myturn/bilijiazhu").GetComponent<Button>();
        mang_diTransfom = transform.Find("myturn/mang_di");
        mang1 = transform.Find("myturn/mang_di/1").GetComponent<Button>();
        mang2 = transform.Find("myturn/mang_di/2").GetComponent<Button>();
        mang3 = transform.Find("myturn/mang_di/3").GetComponent<Button>();
        mang4 = transform.Find("myturn/mang_di/4").GetComponent<Button>();
        pass.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (pass.GetComponent<Image>().color == Color.white)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "0" });
                setNoTurnColor("1");
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "-1" });
                pass.GetComponent<Image>().color = Color.white;
            }
  
        });
        autopass.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
           if(autopass.GetComponent<Image>().color == Color.white)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "1" });
                setNoTurnColor("2");
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "-1" });
                autopass.GetComponent<Image>().color = Color.white;
            }
        
        });
        quanxiaBtn_noTurn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            // NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2, "-1" });
            // 8 -7  轮不到操作 点击全下
            if(quanxiaBtn_noTurn.GetComponent<Image>().color == Color.white)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "3" });
                setNoTurnColor("5");
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "-1" });
                quanxiaBtn_noTurn.GetComponent<Image>().color = Color.white;
            }
      
        });
        qipai.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (this.isNeedConfirm)
            {
                PopupCommon.GetSingleton().ShowView("当前可免费看牌，确定弃牌吗？",null,true, () => {
                    NetMngr.GetSingleton().Send(InterfaceGame.OneTurnFold, new object[] { });
                    AlreadyOpera();
                });
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceGame.OneTurnFold, new object[] { });
                AlreadyOpera();
            }

        });
        genzhubtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 1, genzhuMoney });
            AlreadyOpera();
        });
        genzhubtn_noturn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if(genzhubtn_noturn.GetComponent<Image>().color == Color.white)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "2" });
                AlreadyOpera();
                setNoTurnColor("4");
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceGame.KBNotTurnFoldPass, new object[] { "-1" });
                genzhubtn_noturn.GetComponent<Image>().color = Color.white;
            }

        });
        kanpaibtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.Check, new object[] { });
            AlreadyOpera();
        });
        jingquejiazhubtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            inputJiazhuType = "jingque";
            InputjiazhuTransorm.gameObject.SetActive(true);
            jingquecancelbtn.gameObject.SetActive(false);
            jingquejiazhusurebtn.gameObject.SetActive(true);
            mang_diTransfom.gameObject.SetActive(false);
            Debug.Log("inputjiazhu_text.text = " + inputjiazhu_text.text + "CanBetMinCoin = " + CanBetMinCoin);
            // inputjiazhu_text.text = CanBetMinCoin+"";
            inputjiazhu_text.text = 0+"";  //edit by yang yang 2021年3月16日14:52:13
            Debug.Log("---inputjiazhu_text.text = " + inputjiazhu_text.text + "CanBetMinCoin = " + CanBetMinCoin);
        });
        bilijiazhubtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            inputJiazhuType = "bili";
            InputjiazhuTransorm.gameObject.SetActive(true);
            jingquecancelbtn.gameObject.SetActive(false);
            jingquejiazhusurebtn.gameObject.SetActive(true);
            mang_diTransfom.gameObject.SetActive(false);
            // inputjiazhu_text.text = CanBetMinCoin+"";
            inputjiazhu_text.text = 0+"";  //edit by yang yang 2021年3月16日14:52:13
        });
        mang1.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if(mang1.transform.Find("Count").GetComponent<Text>().text==CanBetMaxCoin.ToString())
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2, mang1.transform.Find("Count").GetComponent<Text>().text });
            else
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, mang1.transform.Find("Count").GetComponent<Text>().text});
            AlreadyOpera();
        });
        mang2.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (mang2.transform.Find("Count").GetComponent<Text>().text == CanBetMaxCoin.ToString())
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2, mang2.transform.Find("Count").GetComponent<Text>().text });
            else
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, mang2.transform.Find("Count").GetComponent<Text>().text });
            AlreadyOpera();
        });
        mang3.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (mang3.transform.Find("Count").GetComponent<Text>().text == CanBetMaxCoin.ToString())
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2, mang3.transform.Find("Count").GetComponent<Text>().text });
            else
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, mang3.transform.Find("Count").GetComponent<Text>().text });
            AlreadyOpera();
        });
        mang4.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (mang4.transform.Find("Count").GetComponent<Text>().text == CanBetMaxCoin.ToString())
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2, mang4.transform.Find("Count").GetComponent<Text>().text });
            else
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, mang4.transform.Find("Count").GetComponent<Text>().text });
            AlreadyOpera();
        });
        qipaichild = qipai.transform.Find("qipai").GetComponent<Image>();
        qipaiText = qipai.transform.Find("Text").GetComponent<Text>();
        genzhuText = genzhubtn.transform.Find("Text").GetComponent<Text>();
        genzhuText_noturn= genzhubtn_noturn.transform.Find("Text").GetComponent<Text>();
        addtransfrom = transform.Find("myturn/Add");
        addTopText = addtransfrom.Find("Top/value").GetComponent<Text>();
        addSliderText = addtransfrom.Find("Slider/Handle Slide Area/Handle/value").GetComponent<Text>();
        jiazhusurebtn = addtransfrom.Find("Sure").GetComponent<Button>();
        jingquejiazhusurebtn = InputjiazhuTransorm.Find("Sure").GetComponent<Button>();
        slider = addtransfrom.Find("Slider").GetComponent<Slider>();
        mytrunTransform = transform.Find("myturn");
        nomytrunTransform = transform.Find("nomyturn");
        jiazhubtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //addTopText.text = 200 + "";
            //addSliderText.text = 5 + "";
            //slider.minValue = 5;
            //slider.maxValue = 200;
            slider.value = 0;
            addtransfrom.gameObject.SetActive(true);
            jingquejiazhubtn.gameObject.SetActive(false);
            bilijiazhubtn.gameObject.SetActive(false);
            mang_diTransfom.gameObject.SetActive(false);
          
        });
        jiazhusurebtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //发送加注信息
            Debug.Log(addSliderText.text);
            if(addSliderText.text=="All In")
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu,new object[] {2, CanBetMaxCoin+""});
            else
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, addSliderText.text });
            slider.value = 0;
            addtransfrom.gameObject.SetActive(false);
            jingquejiazhubtn.gameObject.SetActive(true);
            bilijiazhubtn.gameObject.SetActive(true);
            mang_diTransfom.gameObject.SetActive(true);
            AlreadyOpera();

        });
        jingquejiazhusurebtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            InputjiazhuTransorm.gameObject.SetActive(false);
            addtransfrom.gameObject.SetActive(false);
            mang_diTransfom.gameObject.SetActive(false);
            Debug.Log("--1--inputjiazhu_text.text = " + inputjiazhu_text.text + "CanBetMinCoin = " + CanBetMinCoin);

            int num = int.Parse(inputjiazhu_text.text);
            if(inputJiazhuType == "bili"){
                if(num > 100) num = 100; // 不能超过100
                num = Mathf.FloorToInt(num * GameManager.GetSingleton().gameDichi / 100);
            }

            if (int.Parse(inputjiazhu_text.text) == CanBetMaxCoin)
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, num.ToString() }); //edit by yang yang 2021年3月16日 14:33:45
            // NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 2, inputjiazhu_text.text }); 
            else
                NetMngr.GetSingleton().Send(InterfaceGame.JiaZhuGengzhu, new object[] { 0, num.ToString() });
            inputjiazhu_text.text = "";
            Debug.Log("--2--inputjiazhu_text.text = " + inputjiazhu_text.text + "CanBetMinCoin = " + CanBetMinCoin);
            //if (int.Parse(inputjiazhu_text.text) < CanBetMinCoin)
            //{

            //}
            //else
            AlreadyOpera();
        });
        slider.onValueChanged.AddListener((value) =>
        {
            if (value == CanBetMaxCoin)
                addSliderText.text = "All In";
            else
               addSliderText.text = ((int)value).ToString();
        });
        mytrunTransform.gameObject.SetActive(false);
        nomytrunTransform.gameObject.SetActive(false);
        // TEST
        CanBetMinCoin = 8;
    }
    void Start()
    {
        SetDiRatio();
        AudioClip audio = Resources.Load<AudioClip>("Audio/time_warning");
        ads.clip = audio;
    }
    // 废弃！
    void AlreadyOpera()
    {
        //mytrunTransform.gameObject.SetActive(false);
        //nomytrunTransform.gameObject.SetActive(true);
    }
    public void ResetUIButton()
    {
        startRunTime = false;
        mytrunTransform.gameObject.SetActive(false);
        nomytrunTransform.gameObject.SetActive(false);
		if (!StaticData.isGuanzhan) {
			int myid = GameManager.GetSingleton ().myNetID;
			GameUIManager.GetSingleton ().roomNumSitActivePlayerTrans.GetChild (myid).GetComponent<PlayInfo> ().showHeadImage (true);
		}

    }
    //点击后 修改颜色
    //  8  12 缺少全下
    void setNoTurnColor(string type)
    {
        switch (type)
        {
            case "1":
			pass.GetComponent<Image>().color=new Color(150/255f, 150/255f, 150/255f);
                autopass.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                genzhubtn_noturn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
            case "2":
                autopass.GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
                pass.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                genzhubtn_noturn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
                //  重置所有颜色
            case "3":
                pass.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                autopass.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                genzhubtn_noturn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                quanxiaBtn_noTurn.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
            case "4":
                pass.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                autopass.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                genzhubtn_noturn.GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
                break;
            case "5":
                quanxiaBtn_noTurn.GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
                break;
        }
    }
    public void SetDiRatio()
    {
        for(int i = 0; i < 4; i++)
        {
            // 627 全部改成底池
            //if (CanBetMinCoin > (int)(0.3333f * GameManager.GetSingleton().gameDichi))
            //{
            //    mang_diTransfom.GetChild(i).Find("Bei").GetComponent<Text>().text = "X" + (i + 2);
            //    if (GameManager.GetSingleton().isGetGonggongPai)
            //    {
            //        mang_diTransfom.GetChild(i).Find("Info").GetComponent<Text>().text = "底池";
            //        mang_diTransfom.GetChild(i).Find("Count").GetComponent<Text>().text = (GameManager.GetSingleton().gameDichi * (i + 2)).ToString();
            //    }
            //    else
            //    {
            //        mang_diTransfom.GetChild(i).Find("Info").GetComponent<Text>().text = "盲注";
            //        mang_diTransfom.GetChild(i).Find("Count").GetComponent<Text>().text = (GameManager.GetSingleton().roomDaMang * (i + 2)).ToString();
            //    }

            //}
            //else
            //{
            //Debug.Log(GameManager.GetSingleton().betRatio[i]);
            mang_diTransfom.GetChild(i).GetComponent<Button>().interactable = true;
            mang_diTransfom.GetChild(i).Find("Bei").GetComponent<Text>().text = "" + GameManager.GetSingleton().betRatio[i];
                mang_diTransfom.GetChild(i).Find("Info").GetComponent<Text>().text = "底池";
                float money = 0f;
                if (GameManager.GetSingleton().betRatio[i] == "最小加注")
                {
                    mang_diTransfom.GetChild(i).Find("Count").GetComponent<Text>().text = CanBetMinCoin + "";
                mang_diTransfom.GetChild(i).Find("Info").GetComponent<Text>().text = "";
                  }
                if (GameManager.GetSingleton().betRatio[i].Contains("All"))
                    mang_diTransfom.GetChild(i).Find("Count").GetComponent<Text>().text = GameManager.GetSingleton().myDeskLeftmoney + "";
                if (GameManager.GetSingleton().betRatio[i].Contains("X"))
                {
                    money = float.Parse(GameManager.GetSingleton().betRatio[i].Split('X')[0]);
                   // Debug.Log("money:" + money);
                    int temp = (int)(money * this.oneDichi);
                    mang_diTransfom.GetChild(i).Find("Count").GetComponent<Text>().text = temp + "";
                }
                if (GameManager.GetSingleton().betRatio[i].Contains("/"))
                {
                int temp = (int)(this.oneDichi*
                    float.Parse(GameManager.GetSingleton().betRatio[i].Split('/')[0]) / float.Parse(GameManager.GetSingleton().betRatio[i].Split('/')[1]));

                if (temp < CanBetMinCoin)
                    mang_diTransfom.GetChild(i).GetComponent<Button>().interactable = false;
                mang_diTransfom.GetChild(i).Find("Count").GetComponent<Text>().text = temp + "";
                }
          //  }
        }
    }
    public void setTips(bool b)
    {
        Debug.Log("设置。。隐藏。。");
        tips.gameObject.SetActive(b);
    }

    public void setAddCancel()
    {
        addSliderText.text = CanBetMinCoin + "";
        addtransfrom.gameObject.SetActive(false);
        jingquejiazhubtn.gameObject.SetActive(true);
        bilijiazhubtn.gameObject.SetActive(true);
        mang_diTransfom.gameObject.SetActive(true);
    }

    public void OnSomeOneTurn(Hashtable h)
    {
        this.isNeedConfirm = false;
        int temp =GameManager.GetSingleton().netTolocal(int.Parse(h["netID"].ToString()));
        if(preSomeOneIndex>-1)
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(preSomeOneIndex).GetComponent<PlayInfo>().Daojishi_Stop();
        timeQipai = int.Parse(h["qipaiTime"].ToString());
        temponeStep = (int)timeQipai;
        // 轮到我操作 显示操作UI
        if (!StaticData.isGuanzhan)
        {
			if (temp == GameManager.GetSingleton().myNetID)
            {
                SoundManager.GetSingleton().PlaySound("Audio/turn");
                Debug.Log("轮到我操作");
                mytrunTransform.gameObject.SetActive(true);
                nomytrunTransform.gameObject.SetActive(false);
                InputjiazhuTransorm.gameObject.SetActive(false);
                addtransfrom.gameObject.SetActive(false);
                slider.value = 0;
                timeQipaiStep = 0;
                startRunTime = true;
                string[] tempxiazhu = h["jiazhu"].ToString().Split('|');
                CanBetMaxCoin = int.Parse(tempxiazhu[1]);
                CanBetMinCoin = int.Parse(tempxiazhu[0]);
                addTopText.text = CanBetMaxCoin + "";
                addSliderText.text = CanBetMinCoin + "";
                slider.maxValue = CanBetMaxCoin;
                slider.minValue = CanBetMinCoin;
				int myid = GameManager.GetSingleton ().myNetID;
				GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(myid).GetComponent<PlayInfo>().showHeadImage(false);
				GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(myid).GetComponent<PlayInfo>().setModel(false);
                string tempGZ = h["genzhu"].ToString();
                if (tempGZ == "0")
                {
                    this.isNeedConfirm = true;
                    kanpaibtn.gameObject.SetActive(true);
                    genzhubtn.gameObject.SetActive(false);
                }
                else
                {
                    kanpaibtn.gameObject.SetActive(false);
                    genzhubtn.gameObject.SetActive(true);
                    genzhuText.text = tempGZ;
                    genzhuMoney = tempGZ;
                }
                // 只能全下
                if (h["showAllin"].ToString() == "1")
                {
                    kanpaibtn.interactable = false;
                    genzhubtn.interactable = false;
                    quanxiaBtn.gameObject.SetActive(true);
                    mang_diTransfom.gameObject.SetActive(false);
                    jingquejiazhubtn.gameObject.SetActive(false);
                    bilijiazhubtn.gameObject.SetActive(false);
                }
                else
                {
                    kanpaibtn.interactable = true;
                    genzhubtn.interactable = true;
                    quanxiaBtn.gameObject.SetActive(false);
                    mang_diTransfom.gameObject.SetActive(true);
                    jingquejiazhubtn.gameObject.SetActive(true);
                    bilijiazhubtn.gameObject.SetActive(true);
                }
                this.oneDichi = int.Parse(h["onDichi"].ToString());
                SetDiRatio();// 底池不断变化

                curTurn++;
                if(curTurn == 1){
                    Paicontrol.GetInstance().ShowFanPai();
                }
            }
            // 轮到别人操作
            else
            {
                Debug.Log("轮到" + temp + "操作");
                startRunTime = false;
                mytrunTransform.gameObject.SetActive(false);
                //nomytrunTransform.gameObject.SetActive(true);
				int myid = GameManager.GetSingleton ().myNetID;
				GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(myid).GetComponent<PlayInfo>().showHeadImage(true);
                GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().Daojishi_Start(timeQipai);
                GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().setModel(false);
            }
        }
        // 观战不处理
        else
        {
            //mytrunTransform.gameObject.SetActive(false);
            //nomytrunTransform.gameObject.SetActive(false);
            //setNoTurnColor("3");
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(temp).GetComponent<PlayInfo>().Daojishi_Start(timeQipai);
          //  GameUIManager.GetSingleton().rlight.SetNextPosition(GameUIManager.GetSingleton().rlight.vectorss[temp].position*2);
        }
        preSomeOneIndex = temp;

    }
    public void OnNoSomeOneTurn(Hashtable h)
    {
        Debug.Log("轮不到我操作");
//         if(StaticData.isInGame)
//         {

        
        startRunTime = false;
        ads.Stop();
        ads.loop = false;
        mytrunTransform.gameObject.SetActive(false);
        nomytrunTransform.gameObject.SetActive(true);
        setNoTurnColor("3");

        string tempGZ = h["genzhu"].ToString();
        string type = h["type"].ToString();
        switch (type)
        {
            case "1":
                pass.gameObject.SetActive(true);
                autopass.gameObject.SetActive(true);
                quanxiaBtn_noTurn.gameObject.SetActive(false);
                genzhubtn_noturn.gameObject.SetActive(false);
                break;
            case "2":
                pass.gameObject.SetActive(false);
                autopass.gameObject.SetActive(false);
                genzhubtn_noturn.gameObject.SetActive(false);
                quanxiaBtn_noTurn.gameObject.SetActive(true);
                break;
            case "3":
                pass.gameObject.SetActive(true);
                autopass.gameObject.SetActive(false);
                quanxiaBtn_noTurn.gameObject.SetActive(false);
                genzhubtn_noturn.gameObject.SetActive(true);
                genzhuText_noturn.text = tempGZ;
                break;
        }
        // 812 有效的操作
        string Correcttype = h["autoType"].ToString();
        switch (Correcttype)
        {
            case "0": pass.GetComponent<Image>().color = new Color(180 / 255f, 0, 0); break; 
            case "1": autopass.GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 255 / 255f); break;
            case "2": genzhubtn_noturn.GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 255 / 255f); break;
            case "3": quanxiaBtn_noTurn.GetComponent<Image>().color = new Color(150 / 255f, 150 / 255f, 255 / 255f); break;
        }
//         }
//         else
//         {
//             mytrunTransform.gameObject.SetActive(false);
//             nomytrunTransform.gameObject.SetActive(false);
//         }
     }
    public void startGameShow(bool b)
    {
        startGamebtn.gameObject.SetActive(b);
    }
    public void StartGame()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.StartGameRequest,new object[] { });
    }
    public  void AddDelayTime(int time_)
    {
        timeQipai = time_;
        temponeStep = (int)timeQipai;
        timeQipaiStep = 0f;
    }

    float timeQipaiOneStep=0f;
    int temponeStep = 0;
    void Update ()
    {
        if (startRunTime)
        {
            timeQipaiStep += Time.deltaTime;
            timeQipaiOneStep +=Time.deltaTime;
            qipaiText.text = temponeStep + "";
            if (timeQipaiOneStep >= 1)
            {
                timeQipaiOneStep = 0;
                temponeStep--;
                if (temponeStep < 6)
                {
                    if(StaticData.isVibrate)
                        Handheld.Vibrate();
                    if (StaticData.isMusic)
                    {
                        ads.Play();
                    }
                    if (temponeStep <= 0)
                    {
                        startRunTime = false;
                    }
                }
            }
            qipaichild.fillAmount = (timeQipai - timeQipaiStep) / timeQipai;
           
         
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            timeQipai = 10;
            timeQipaiStep = 0;
            startRunTime = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddDelayTime(30);
        }
    }
}
