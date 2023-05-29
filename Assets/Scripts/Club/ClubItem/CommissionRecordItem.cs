using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CommissionRecordItem : MonoBehaviour {
   
	public Text txtCreateTime;
    public Text txtFee;
    public Image imFaFang;
  
    public string userid;

    void Awake()
    {

        txtCreateTime = transform.Find("CreateTime").GetComponent<Text>();
        txtFee = transform.Find("Money").GetComponent<Text>();
        imFaFang = transform.Find("Image").GetComponent<Image>();

        imFaFang.gameObject.SetActive(false);
        //btLookRecordBT.onClick.AddListener(()=> {

        //    NetMngr.GetSingleton().Send(InterfaceClub.GetLookRecord, new object[] { userid });
        //});

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

        //state  分给谁的 0-Club   1-给父级的
        //type   游戏类型
    public void SetInfo(string strMoney, string strState, string strTime, string strType)
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
           
        txtCreateTime.text = "时间:" + date_arr[2] + "-" + date_arr[0] + "-" + date_arr[1] + " " + strHour + ":" + time_arr[1] + ":" + time_arr[2].Substring(0,2);
        txtFee.text = "金额:" + strMoney;
        if (strState != "0")
        {
            imFaFang.gameObject.SetActive(true);
        }


    }
 
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
