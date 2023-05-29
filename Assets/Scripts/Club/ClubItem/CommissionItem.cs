using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CommissionItem : MonoBehaviour {
   
    public Text txtUserName;
    public Text txtUserID;
	public Text txtCreateTime;
    public Text txtMoney;
    public Button btLookRecordBT;
    public string userid;

    void Awake()
    {
        txtUserName = transform.Find("UserName").GetComponent<Text>();
        txtUserID = transform.Find("UserID").GetComponent<Text>();
        txtCreateTime = transform.Find("CreateTime").GetComponent<Text>();
        txtMoney = transform.Find("Money").GetComponent<Text>();
        btLookRecordBT = transform.Find("LookRecordBT").GetComponent<Button>();


        btLookRecordBT.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceClub.GetLookRecord, new object[] { userid });
        });

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //public void GetSprtie(Sprite s)
    //{
    //    clubHead.sprite = s;
    //}

    public void SetInfo(string strName, string strID, string strMoney, string strTime)
    {

        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(strTime);
        DateTime dt = startTime.AddMilliseconds(lTime);

        string date = dt.ToShortDateString().ToString();
        string time = dt.ToLongTimeString().ToString();
        string[] date_arr = date.Split('/');
        string[] time_arr = time.Split(':');
        string strTemp = time_arr[2].Substring(time_arr[2].Length - 2, 2);
        string strHour = time_arr[0];

        if (strTemp == "PM")
        {
            int nHour = int.Parse(strHour) + 12;
            strHour = nHour.ToString();
        }

       


        txtUserName.text = strName;
        txtUserID.text = "ID:" + strID;
        txtCreateTime.text = "时间:" + date_arr[2] + "-" + date_arr[0] + "-" + date_arr[1] + " " + strHour + ":" + time_arr[1] + ":" + time_arr[2].Substring(0, 2);
        txtMoney.text = "金额:" + strMoney;
        userid = strID;


    }
 
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
