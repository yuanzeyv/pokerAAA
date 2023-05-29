using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BatteryAndWiFi : MonoBehaviour {

    public string batteryData;
    public string wifiData;
    public string timeData;

    public Slider batterySlider;
    public Image wifiImage;
    public Text timeText;



    void Awake() {
        batterySlider = transform.Find("Battery").GetComponent<Slider>();
        wifiImage = transform.Find("wifi").GetComponent<Image>();
        timeText = transform.Find("time").GetComponent<Text>();

    }

    // Use this for initialization
    void Start () {
        //StartCoroutine("UpdataTime");//时间
       // StopAllCoroutines();
    }

    //更新时间
    IEnumerator UpdataTime()
    {
        DateTime now = DateTime.Now;
        if (now.Hour < 10)
        {
            if (now.Minute < 10)
            {
                timeText.text = string.Format("{0}:{1}", "0" + now.Hour, "0" + now.Minute);
            }
            else
            {
                timeText.text = string.Format("{0}:{1}", "0" + now.Hour, now.Minute);
            }

         
        }
        else {
            if (now.Minute < 10)
            {
                timeText.text = string.Format("{0}:{1}",  now.Hour, "0" + now.Minute);
            }
            else
            {
                timeText.text = string.Format("{0}:{1}",  now.Hour, now.Minute);
            }

        }
        //  timeText.text = string.Format("{0}:{1}", now.Hour, now.Minute);
        yield return new WaitForSeconds(60f - now.Second);
        //yield return new WaitForSeconds(60f);
        StartCoroutine("UpdataTime");//时间
        Debug.Log("开始新的一分钟");
    }


    // Update is called once per frame
    void Update () {
        // GetBatteryAnWifiData();
        //batterySlider.value =  (float)GetBatteryLevel()/100;
        batterySlider.value = SystemInfo.batteryLevel ;

        GetWifiLevel();


        DateTime now = DateTime.Now;
        if (now.Hour < 10)
        {
            if (now.Minute < 10)
            {
                timeText.text = string.Format("{0}:{1}", "0" + now.Hour, "0" + now.Minute);
            }
            else
            {
                timeText.text = string.Format("{0}:{1}", "0" + now.Hour, now.Minute);
            }


        }
        else
        {
            if (now.Minute < 10)
            {
                timeText.text = string.Format("{0}:{1}", now.Hour, "0" + now.Minute);
            }
            else
            {
                timeText.text = string.Format("{0}:{1}", now.Hour, now.Minute);
            }

        }

        ///充电状态判断
        if (SystemInfo.batteryStatus==BatteryStatus.Charging)
        {
            // log.text += "电池充电中";
            batterySlider.transform.Find("charging").gameObject.SetActive(true);
        }
        else
        {
            // log.text += "电池放电中";
            batterySlider.transform.Find("charging").gameObject.SetActive(false);
        }
    }

    int GetBatteryLevel()
    {
        try
        {
            string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");
            return int.Parse(CapacityString);
        }
        catch (Exception e)
        {
           // Debug.Log("Failed to read battery power; " + e.Message);
        }
        return -1;
    }

    void GetWifiLevel()
    {
        float wifiValue=  NetMngr.GetSingleton().GetLatency();
        // Debug.Log(wifiValue);
        if (wifiValue >0 && wifiValue <= 70) 
        {
            //   log.text += "Wifi信号强度很棒";
            wifiImage.sprite = Resources.Load("wifi/信号3", typeof(Sprite)) as Sprite;
        }
        else if (wifiValue <= 200 && wifiValue > 70)
        {
            // log.text += "Wifi信号强度一般";
            wifiImage.sprite = Resources.Load("wifi/信号2", typeof(Sprite)) as Sprite;

        }
        else if (wifiValue > 200 )
        {
            // log.text += "Wifi信号强度很弱";
            wifiImage.sprite = Resources.Load("wifi/信号1", typeof(Sprite)) as Sprite;
        }
        else if (wifiValue >1000)
        {
            wifiImage.sprite = Resources.Load("wifi/信号0", typeof(Sprite)) as Sprite;
        }

    }



    void GetBatteryAnWifiData()
    {
        batteryData = "";
        wifiData = "";
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        batteryData = jo.Call<string>("MonitorBatteryState");
        Debug.Log(batteryData);
        AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo1 = jc.GetStatic<AndroidJavaObject>("currentActivity");
        wifiData = jo1.Call<string>("ObtainWifiInfo");
        Debug.Log(wifiData);
        OnBatteryDataBack(batteryData);
        OnWifiDataBack(wifiData);
    }
    void OnBatteryDataBack(string batteryData)//level+"|"+scale+"|"+status;
    {
        string[] args = batteryData.Split('|');
        if (args[2] == "2")
        {
            // log.text += "电池充电中";
            batterySlider.transform.Find("charging").gameObject.SetActive(true);
        }
        else
        {
            // log.text += "电池放电中";
            batterySlider.transform.Find("charging").gameObject.SetActive(false);
        }
        float percent = int.Parse(args[0]) / float.Parse(args[1]);
        batterySlider.value = percent;
    }
    void OnWifiDataBack(string wifiData)//strength+"|"+intToIp(ip)+"|"+mac+"|"+ssid;
    {
        //分析wifi信号强度
        //获取RSSI，RSSI就是接受信号强度指示。
        //得到的值是一个0到-100的区间值，是一个int型数据，其中0到-50表示信号最好，
        //-50到-70表示信号偏差，小于-70表示最差，
        //有可能连接不上或者掉线，一般Wifi已断则值为-200。
       // log.text += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 0)
        {
            //   log.text += "Wifi信号强度很棒";
            wifiImage.sprite = Resources.Load("wifi/net_3g_1", typeof(Sprite)) as Sprite;
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            // log.text += "Wifi信号强度一般";
            wifiImage.sprite = Resources.Load("wifi/net_3g_2", typeof(Sprite)) as Sprite;

        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            // log.text += "Wifi信号强度很弱";
            wifiImage.sprite = Resources.Load("wifi/net_3g_3", typeof(Sprite)) as Sprite;
        }
        else if (int.Parse(args[0]) < -200)
        {
           // log.text += "Wifi信号JJ了";
        }
        string ip = "IP：" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
       // log.text += ip;
       // log.text += mac;
      //  log.text += ssid;
    }
}
