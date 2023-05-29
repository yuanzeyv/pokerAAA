using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchInfoPopup : BasePopup {

    private Button startBtn;
    private Button endBtn;
    private Button matchInfoBtn;
    private Button matchSetBtn;
    private Text roomName;
    private Text time;
    private Text startTime;
    private Text paiTime;
    private Text ANTE;
    private Text mang;
    private Text dairuMin;
    private Text dairuMax;
    private Text considerTime;
    private Text insurance;
    private Text straddle;
    private Text shouQuanDaiRu;
    private Text GPS;
    private Text IP;
    private Text auto;
    private Text delaySeeCard;

	public Image image1;
	public Image image2;

    private bool isRoot = true;
    public bool isStart = true;
    private float timerh = 0;
    private float timerm = 0;
    private float timers = 0;
    private bool isRun = false;

    private void Awake()
    {
        Init();
        roomName = transform.Find("roomName").GetComponent<Text>();
        time = transform.Find("time").GetComponent<Text>();
        startTime = transform.Find("content/infoItem1/titleText/Text").GetComponent<Text>();
        paiTime = transform.Find("content/infoItem2/titleText/Text").GetComponent<Text>();
        ANTE = transform.Find("content/infoItem3/titleText/Text").GetComponent<Text>();
        mang = transform.Find("content/infoItem4/titleText/Text").GetComponent<Text>();
        dairuMin = transform.Find("content/infoItem5/titleText/Text").GetComponent<Text>();
        dairuMax = transform.Find("content/infoItem6/titleText/Text").GetComponent<Text>();
        considerTime = transform.Find("content/infoItem7/titleText/Text").GetComponent<Text>();
        insurance = transform.Find("content/infoItem8/titleText/Text").GetComponent<Text>();
        straddle = transform.Find("content/infoItem9/titleText/Text").GetComponent<Text>();
        shouQuanDaiRu = transform.Find("content/infoItem10/titleText/Text").GetComponent<Text>();
        GPS = transform.Find("content/infoItem11/titleText/Text").GetComponent<Text>();
        IP = transform.Find("content/infoItem12/titleText/Text").GetComponent<Text>();
        auto = transform.Find("content/infoItem13/titleText/Text").GetComponent<Text>();
        delaySeeCard = transform.Find("content/infoItem14/titleText/Text").GetComponent<Text>();

		image1 = transform.Find("startBtn/Image").GetComponent<Image>();
		image2 = transform.Find("startBtn/Image2").GetComponent<Image>();


        startBtn = transform.Find("startBtn").GetComponent<Button>();
        endBtn = transform.Find("endBtn").GetComponent<Button>();
        matchInfoBtn = transform.Find("matchInfoBtn").GetComponent<Button>();
        matchSetBtn = transform.Find("matchSetBtn").GetComponent<Button>();

        startBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isStart)
            {
                isStart = false;
                NetMngr.GetSingleton().Send(InterfaceGame.stopNextGame, new object[] {"0" });
            }
            else
            {
                isStart = true;
                NetMngr.GetSingleton().Send(InterfaceGame.stopNextGame, new object[] {"1" });
            }
            
        });
        endBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.DissolvePaiJu, new object[] { });
        });
        matchInfoBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
            GameUIManager.GetSingleton().playerManageListPopup.ShowView();
        });
        matchSetBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
            GameUIManager.GetSingleton().managerSetPopup.ShowView();
        });
    }

    public void SetData(Hashtable data)
    {
        if (data.ContainsKey("isPause"))
        {
            if (data["isPause"].ToString() == "1")
            {
                isStart = false;
            }
            else
            {
                isStart = true;
            }
        }
        image1.gameObject.SetActive(!isStart);
        image2.gameObject.SetActive(isStart);
        roomName.text = "房间名字:"+data["roundName"].ToString();
        if (data.ContainsKey("isRoot"))
        {
            isRoot = data["isRoot"].ToString() == "1" ? true : false;
        }
        if (!isRoot)
        {
            startBtn.gameObject.SetActive(false);
            endBtn.gameObject.SetActive(false);
            matchInfoBtn.gameObject.SetActive(false);
            matchSetBtn.gameObject.SetActive(false);
        }
        if (data["leftTime"].ToString()!="暂未开始")
        {
            float m_time = int.Parse(data["leftTime"].ToString());
            timerh = (int)(m_time / 3600);
            timerm = (int)(m_time % 3600 / 60);
            timers = (int)(m_time % 3600 % 60);
            isRun = true;
        }
        time.text = "剩余时间:暂未开始";
        startTime.text = data["startTime"].ToString();
        paiTime.text = int.Parse(data["gameTime"].ToString())/3600+"小时"+ int.Parse(data["gameTime"].ToString()) % 3600/60+"分钟";
        ANTE.text = data["ante"].ToString();
        string[] strs = data["bind"].ToString().Split('|');
        mang.text = strs[0]+"/"+strs[1];
        dairuMin.text = data["minBringCoin"].ToString();
        dairuMax.text = data["maxBringCoin"].ToString();
        if (data.ContainsKey("thinkTime"))
        {
			considerTime.text = data["thinkTime"].ToString() + "s";
        }
        insurance.text = data["safe"].ToString()=="0"?"关闭":"开启";
        straddle.text = data["straddle"].ToString() == "0" ? "关闭" : "开启";
        shouQuanDaiRu.text = data["dragIn"].ToString() == "0" ? "关闭" : "开启";
        GPS.text = data["GPSIP"].ToString() == "0" ? "关闭" : "开启";
        IP.text = data["GPSIP"].ToString() == "0" ? "关闭" : "开启";
        auto.text = data["autoCard"].ToString() == "0" ? "关闭" : "开启";
        delaySeeCard.text = data["delaySeeCard"].ToString() == "0" ? "关闭" : "开启";
    }

    private void SetIcon(Sprite sprite)
    {

    }


    void Start ()
    {
	
	}
	
	void Update ()
    {
        if (isRun)
        {
            timers -= Time.deltaTime;
            time.text = "剩余时间:" + (int)timerh + "时"+(int)timerm+"分"+(int)timers+"秒";
            if (timers <= 0)
            {
                if (timerm==0)
                {
                    if (timerh==0)
                    {
                        isRun = false;
                    }
                    else
                    {
                        timerh -= 1;
                        timerm += 59;
                        timers += 60;
                    }
                }
                else
                {
                    timerm -= 1;
                    timers += 60;
                }
            }
        }
	}

    public void ShowView()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.getRoundDetail, new object[] { });
        base.ShowView();
    }
}
