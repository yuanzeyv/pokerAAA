using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using Constant;
using MySocket;
using MyThread;
using MessageControlSpace;
using System.Net;

public class NetMngr : MonoBehaviour
{
    public static string test;

    public Dictionary<string, int> serverMessage;

    public static NetMngr G_NetMngr = null;
    public static NetMngr GetSingleton()
    {
        return G_NetMngr;
    }

    public CreateSocket MyCreateSocket;
    public Transmit MyTransmit;
    public MessageControlSpace.MessageControl MyMessageControl;
    public PostMessageThread MyPostMessageThread;
    private DataEncrypt MyDataEncrypt;
    private Thread _ConnectThread = null;

    public bool isURL = true;
    //private string URL = "dezhou.xuxiangvictor.com";
    // private string URL = "192.168.101.106";//"47.99.215.214";
    // 220.167.106.133
    // 221.236.19.248
   private string URL = "heise.sootu.top";
   private string payURL = "http://jingzhou.sootu.top:8001/UpLoad/pay_url.txt";

    public string IP;
    public int PORT;

    //接收到心跳事件
    public Action<float> OnReceiveTicker;
    //网络连接成功事件
    public Action OnConnect; 
    //网络断开事件
    public Action OnNetDown; 
    //发送消息事件
    public Action OnSend;
    //收到消息事件
    public Action OnReceive; 

    void Awake()
    {
        if (G_NetMngr == null)
        {
            G_NetMngr = this;
        }

        DontDestroyOnLoad(gameObject);
        Constants.IP = IP;
        Constants.PORT = PORT;

        if (isURL)
        {
            IPHostEntry hostinfo = Dns.GetHostByName(URL);
            IPAddress[] aryIP = hostinfo.AddressList;
            Constants.IP = aryIP[0].ToString();
            Debug.Log(URL+"域名链接" + Constants.IP);
        }
        Init();
        StartCoroutine(ReadPayUrl());
    }
    public static string PayUrl;
    public static string playerId = "";

    /// <summary>
    /// 获取支付链接
    /// </summary>
    /// <returns></returns>
    IEnumerator ReadPayUrl()
    {
        string url = string.Format("http://jingzhou.sootu.top:8001/UpLoad/pay_url.txt");
        WWW www = new WWW(url);
        yield return www;
        PayUrl = www.text;

    }
    public void Init() {

        MyCreateSocket = new CreateSocket();
        MyTransmit = new Transmit();
        MyMessageControl = new MessageControl();
        MyPostMessageThread = new PostMessageThread();
        MyDataEncrypt = new DataEncrypt();

        MyMessageControl.MessageControlParaInit();
        MyPostMessageThread.PostMessageThreadParaInit();

        MyCreateSocket.CreateSocketGetPoint(MyMessageControl, MyDataEncrypt);
        MyTransmit.TransmitGetPoint(MyCreateSocket);
        MyPostMessageThread.PostMessageThreadGetPoint(MyMessageControl, MyTransmit);
    }

    public void ReInit()
    {
        MyCreateSocket.SocketClose();
        MyCreateSocket = new CreateSocket();
        MyCreateSocket.CreateSocketGetPoint(MyMessageControl, MyDataEncrypt);
        MyTransmit.ResetTransmit(MyCreateSocket);
    }


    public void ShowLog(string s)
    {
        Debug.Log(s);
    }

    public void ShowLogcat(string titleString, string content)
    {
        if (content.Length > 500)
        {
            double partCountF = content.Length / 500;
            int partCountI = (int)Math.Ceiling(partCountF);
            for (int i = 0; i <= partCountI; i++)
            {
                string stemp = "";
                if (i * 500 + 500 < content.Length)
                {
                    stemp = content.Substring(i * 500, 500);
                }
                else
                {
                    int remain = content.Length - i * 500;
                    stemp = content.Substring(i * 500, remain);
                }

                NetMngr.GetSingleton().ShowLog(titleString + " part " + i + " : " + stemp);
            }
        }
    }

    public Hashtable GetHashTable(object obj)
    {
        Dictionary<string, object> objs = (Dictionary<string, object>)obj as Dictionary<string, object>;

        Hashtable h = new Hashtable();

        foreach (KeyValuePair<string, object> kvp in objs)
        {
            NetMngr.GetSingleton().ShowLog(kvp.Key + " : " + kvp.Value);
            h.Add(kvp.Key, kvp.Value);
        }
        return h;
    }

    /*
     * 发送消息
     */
    public bool Send(string strMethod, object[] args,byte[] data=null)
    {
        if (OnSend != null)
            OnSend();

        return MyCreateSocket._sendMsg(strMethod, args, data);
    }

    public bool SendString(string s, byte[] data)
    {
        if (OnSend != null)
            OnSend();

        return MyCreateSocket._sendString(s, data);
    }
    
    void FixedUpdate()
    {
        //解析线程
        if (MyPostMessageThread != null)
        {
            MyPostMessageThread.PostThread();
        }
    }

    public void AddListener(string name, Transmit.Callback call)
    {
        
        MyTransmit.AddEventListener(name, call);
    }
    public void RemoveListener(string name)
    {

        MyTransmit.RemoveEventListener(name);
    }


    void OnDestroy()
    {
        Debug.Log("关闭连接");
        if (MyCreateSocket != null)
        {
            MyCreateSocket.SocketClose();
        }
    }

    public void AddReviewMessage(string jsonStr,byte[] data)
    {
        MyCreateSocket.AddMessage(jsonStr, data);
    }

    public void ClearMessage()
    {
        MyCreateSocket.ClearMessage();
    }

    /*
     * 获取网络延迟，为心跳发送返回的时间，单位为毫秒
     */
    public float GetLatency()
    {
        return MyCreateSocket.GetLatency();
    }

    /*
     * 网络是否连接
     */
    public bool IsConnect()
    {
        return Constants.socketConnected;
    }

    /*
     * 获取网络类别，0为无网络连接，1为WIFI连接，2为流量连接，-1为获取失败
     */
    public int GetNetType()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return 0;
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            return 1;
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            return 2;
        else
            return -1;
    }
    // kb 注册回放
    public void regidReview(Message h)
    {
        this.MyMessageControl.AddMessage(h);
    }
}