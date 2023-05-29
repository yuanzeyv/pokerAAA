using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Constant;
using MessageControlSpace;
using System.Runtime.InteropServices;

namespace MySocket
{
    public class CreateSocket
    {
        public CreateSocket()
        {
            CreateReceiveThread();

            CreateListenerThread();
        }
        //网络监听线程的ID
        private Thread m_nListenerThread = null;

        //创建网络监听线程
        private void CreateListenerThread()
        {
            //如果之前存在网络监听线程，那么先将监听线程关闭
            try
            {
                if (m_nListenerThread != null)
                {
                    m_nListenerThread.Abort();

                    m_nListenerThread = null;
                }
            }
            catch (System.Exception ex)
            {
                NetMngr.GetSingleton().ShowLog("关闭心跳线程失败:" + ex.Message);
            }

            m_nListenerThread = new Thread(new ThreadStart(socketListener));

            m_nListenerThread.Start();
        }

        private MessageControlSpace.MessageControl m_MyMessageControl;
        private DataEncrypt m_DataEncrypt;

        public void CreateSocketGetPoint(MessageControlSpace.MessageControl MyMessageControl, DataEncrypt MyDataEncrypt)
        {
            this.m_MyMessageControl = MyMessageControl;
            this.m_DataEncrypt = MyDataEncrypt;
        }

        public Socket MySocket;

        //接收线程的ID
        private Thread m_nReceiveThread = null;

        //可以重连的有效次数
        private int m_nRelineCount = 0;

        public int GetRelineCount()
        {
            return m_nRelineCount;
        }

        //网络是否连接上的标志
        private bool m_nSocketStartFlag;

        public bool GetSocketStartFlag()
        {
            return m_nSocketStartFlag;
        }

        //-----------------------------------
        //网络模块的创建和网络数据的读取
        //-----------------------------------

        //创建网络接收线程
        public void CreateReceiveThread()
        {
            //之前如果存在接收线程，则将接收线程关闭，重新创建新的接收线程
            try
            {
                if (m_nReceiveThread != null)
                {
                    m_nReceiveThread.Abort();
                    m_nReceiveThread = null;
                }
            }
            catch (System.Exception ex)
            {
                NetMngr.GetSingleton().ShowLog("关闭接收消息线程失败：" + ex.Message);
            }

            m_nReceiveThread = new Thread(new ThreadStart(SocketConnect));

            m_nReceiveThread.Start();
        }

        //程序退出时，socket的释放
        public void SocketClose()
        {
            if (MySocket != null)
            {
                MySocket.Close();
                MySocket = null;
            }

            try
            {
                m_nReceiveThread.Abort();
                m_nReceiveThread = null;
            }
            catch (System.Exception ex)
            {
                NetMngr.GetSingleton().ShowLog("关闭接收消息线程失败:" + ex.Message);
            }

            try
            {
                m_nListenerThread.Abort();
                m_nListenerThread = null;
            }
            catch (System.Exception ex)
            {
                NetMngr.GetSingleton().ShowLog("关闭心跳线程失败:" + ex.Message);
            }
        }



        //主调用接口：创建网络模块，用户网络模块的建立
        public void SocketConnect()
        {
            Thread.Sleep(100);

            m_nSocketStartFlag = false;

            m_nSocketStartFlag = _socketStart();

            if (!m_nSocketStartFlag)
            {
                return;
            }

            //网络连接成功向服务器发送公钥，请求本次的AES加密密钥
            //SendPublicKey();
            //连接建立成功，开始进行数据的接收
            _myReceiveControl();
        }

        [DllImport("__Internal")]
        private static extern string getIPv6(string mHost, string mPort);

        public static string GetIPv6(string mHost, string mPort)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
			string mIPv6 = getIPv6(mHost, mPort);
			return mIPv6;
#else
            return mHost + "&&ipv4";
#endif
        }

        private void getIPType(String serverIp, String serverPorts, out String newServerIp, out AddressFamily mIPType)
        {
            mIPType = AddressFamily.InterNetwork;
            newServerIp = serverIp;
            try
            {
                string mIPv6 = GetIPv6(serverIp, serverPorts);
                if (!string.IsNullOrEmpty(mIPv6))
                {
                    string[] m_StrTemp = System.Text.RegularExpressions.Regex.Split(mIPv6, "&&");
                    if (m_StrTemp != null && m_StrTemp.Length >= 2)
                    {
                        string IPType = m_StrTemp[1];
                        if (IPType == "ipv6")
                        {
                            newServerIp = m_StrTemp[0];
                            mIPType = AddressFamily.InterNetworkV6;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                NetMngr.GetSingleton().ShowLog("获取IPV6网址失败:" + e);
            }
        }



        //客户端和服务器建立链接
        private bool _socketStart()
        {
            String serverIp = Constants.IP;
            String serverPorts = Constants.PORT.ToString();

            String newServerIp = "";
            AddressFamily newAddressFamily = AddressFamily.InterNetwork;
            getIPType(serverIp, serverPorts, out newServerIp, out newAddressFamily);
            if (!string.IsNullOrEmpty(newServerIp))
            {
                serverIp = newServerIp;
            }

            MySocket = new Socket(newAddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint IPAndPort = new IPEndPoint(IPAddress.Parse(serverIp), Constants.PORT);

            NetMngr.GetSingleton().ShowLog("连接服务器: " + serverIp + ":" + Constants.PORT);

            try
            {
                MySocket.Connect(IPAndPort);

                //建立好连接，将断线重连计数置为0
                m_nRelineCount = 0;

                //建立好连接，开始进行心跳监控和心跳计时
                m_recriveHeartTime = _getCurTime();
                m_sendHeartTime = _getCurTime();

                m_nSocketStartFlag = true;

                _netConnectedControl();
                NetMngr.GetSingleton().ShowLog("连接服务器成功");
                if (NetMngr.GetSingleton().OnConnect != null)
                    NetMngr.GetSingleton().OnConnect();

                return true;
            }
            catch (SocketException e)
            {
                NetMngr.test = e.Message;

                NetMngr.GetSingleton().ShowLog("连接服务器失败:" + e.Message);

                m_nSocketStartFlag = false;

                //网络连接不上，断线处理
                _netDownControl();

                return false;
            }
        }
        public static Int64[] ping = new Int64[2];

        private byte[] head = new byte[4];
        private int messageLenth;

        List<byte> byteList = new List<byte>();

        //在套接字上进行信息的接收处理:数据信息的组包、解包
        private void _myReceiveControl()
        {
            //从socket接收到的数据缓存
            int nRecLenth = 0;
            byte[] Buff;

            while (true)
            {
                Buff = new byte[102400];

                try
                {
                    nRecLenth = MySocket.Receive(Buff);
                }
                catch (System.Exception e)
                {
                    //客户端断网
                    NetMngr.GetSingleton().ShowLog("接收消息失败:" + e.Message);

                    _netDownControl();

                    break;
                }

                if (nRecLenth <= 0)
                {
                    NetMngr.GetSingleton().ShowLog("nRecLenth <= 0  == " + nRecLenth);

                    //服务器程序关闭
                   _netDownControl();

                    break;
                }

                byte[] bytes = new byte[nRecLenth];
                Buffer.BlockCopy(Buff, 0, bytes, 0, nRecLenth);

                byteList.AddRange(bytes);

                while (byteList.Count > 18)
                {
                    head[0] = byteList[3];
                    head[1] = byteList[2];
                    head[2] = byteList[1];
                    head[3] = byteList[0];


                    messageLenth = BitConverter.ToInt32(head, 0);

                    if (byteList.Count < messageLenth)
                    {
                        break;
                    }
                    else if (byteList.Count > messageLenth)
                    {
                        _receiveNetMessage(byteList.GetRange(0, messageLenth).ToArray());
                        byteList.RemoveRange(0, messageLenth);
                    }
                    else if (byteList.Count == messageLenth)
                    {
                        _receiveNetMessage(byteList.ToArray());
                        byteList.Clear();
                        break;
                    }
                }
            }
        }

        StringBuilder jsonStr;

        private void _receiveNetMessage(byte[] msgBytes)
        {
            //  NetMngr.GetSingleton().ShowLog("收到"+BitConverter.ToString(msgBytes));

            byte[] typeBytes = new byte[2]; 
            typeBytes[0] = msgBytes[5];
            typeBytes[1] = msgBytes[4];
            short type= BitConverter.ToInt16(typeBytes, 0);

            byte[] timeBytes = new byte[8];
            timeBytes[0] = msgBytes[13];
            timeBytes[1] = msgBytes[12];
            timeBytes[2] = msgBytes[11];
            timeBytes[3] = msgBytes[10];
            timeBytes[4] = msgBytes[9];
            timeBytes[5] = msgBytes[8];
            timeBytes[6] = msgBytes[7];
            timeBytes[7] = msgBytes[6];
            long time = BitConverter.ToInt64(timeBytes, 0);

            byte[] dataLentBytes = new byte[4];
            dataLentBytes[0] = msgBytes[17];
            dataLentBytes[1] = msgBytes[16];
            dataLentBytes[2] = msgBytes[15];
            dataLentBytes[3] = msgBytes[14];
            int dataLenth = BitConverter.ToInt32(dataLentBytes, 0);
            // 消息实体需要解密
         //   byte[] origndata = new byte[msgBytes.Length - 18 - dataLenth];
            //Buffer.BlockCopy(msgBytes,18, origndata, 0, origndata.Length);
           // byte[] decryptdata = m_DataEncrypt.Decrypt(origndata);
          //  jsonStr = new StringBuilder();
            // jsonStr.Append(Encoding.UTF8.GetString(msgBytes, 18, msgBytes.Length - 18- dataLenth));
           // jsonStr.Append(origndata);
            byte[] data = new byte[dataLenth];
            Buffer.BlockCopy(msgBytes, msgBytes.Length-dataLenth, data, 0, dataLenth);
            switch (type)
            {
                case 99:
                    {
                        jsonStr = new StringBuilder();
                        jsonStr.Append(Encoding.UTF8.GetString(msgBytes, 18, msgBytes.Length - 18 - dataLenth));
                        m_recriveHeartTime = _getCurTime();
                        //NetMngr.GetSingleton().ShowLog("收到心跳: " + jsonStr);
                        if (NetMngr.GetSingleton().OnReceiveTicker != null)
                            NetMngr.GetSingleton().OnReceiveTicker(GetLatency());
                        if (jsonStr.Equals("-"))
                        {
                            _sendHeart("+", new byte[0]);
                        }
                        break;
                    }
                case 0:
                    {
                        if (NetMngr.GetSingleton().OnReceive != null)
                            NetMngr.GetSingleton().OnReceive();

                        byte[] origndata = new byte[msgBytes.Length - 18 - dataLenth];
                        Buffer.BlockCopy(msgBytes,18, origndata, 0, origndata.Length);
                        string temp = System.Text.Encoding.UTF8.GetString(origndata);
                        //base64转码
                        byte[] tempbyte = Convert.FromBase64String(temp);
                        byte[] decryptdata = m_DataEncrypt.Decrypt(tempbyte);

                        jsonStr = new StringBuilder();
                        jsonStr.Append(Encoding.UTF8.GetString(decryptdata));
                        NetMngr.GetSingleton().ShowLog("收到消息:"+jsonStr.ToString()+System.DateTime.Now);
                        AddMessage(jsonStr.ToString(),data);
                        break;
                    }
                case 1:
                    jsonStr = new StringBuilder();
                    jsonStr.Append(Encoding.UTF8.GetString(msgBytes, 18, msgBytes.Length - 18 - dataLenth));
                    NetMngr.GetSingleton().ShowLog("收  :" + jsonStr.ToString() + "   "  + System.DateTime.Now);
                    //设置获取到的AES加密密钥 
                    m_DataEncrypt.DecryptKeyKB(jsonStr.ToString());
                    break;
            }

        }
        
        public void AddMessage(string jsonStr,byte[] data)
        {
            if (jsonStr.StartsWith("{") && jsonStr.EndsWith("}"))
            {
                Hashtable table = new Hashtable();

                table = TinyJSON.jsonDecode(jsonStr) as Hashtable;

                Message myMessage = new Message();
                myMessage.hashtable = table;
                myMessage.data = data;
                m_MyMessageControl.AddMessage(myMessage);
            }
        }

        public void ClearMessage()
        {
            m_MyMessageControl.ClearMessage();
        }

        //----------------------------------------------
        //客户端的网络监控，15秒没有接收到心跳，主动重连
        //---------------------------------------------
        private long m_recriveHeartTime = 0;
        private long m_sendHeartTime = 0;

        private long _getCurTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        public void socketListener()
        {
            Thread.Sleep(5000);

            long lCurTickTime = 0;

            while (true)
            {
                lCurTickTime = _getCurTime();

                //15秒没有接收到网络心跳
                if (lCurTickTime - m_recriveHeartTime > 600000)
                {
                    //将网络接收线程停止
                    try
                    {
                        m_nReceiveThread.Abort();

                        m_nReceiveThread = null;
                    }
                    catch (System.Exception ex)
                    {
                        NetMngr.GetSingleton().ShowLog("关闭接收线程失败:" + ex.Message);
                    }

                    //通知界面进行重连
                    _netDownControl();

                    m_recriveHeartTime = lCurTickTime;

                    continue;
                }

                //每隔5秒主动向服务器发送心跳
                if (lCurTickTime - m_sendHeartTime >= 5000 && MySocket != null)
                {
                    _sendHeart("@",new byte[0]);
                    //NetMngr.GetSingleton().ShowLog("@");
                    m_sendHeartTime = lCurTickTime;
                }

                Thread.Sleep(5000);
            }
        }

        //--------------------------------
        //发送函数的处理
        //--------------------------------

        /*发送的函数接口,最终发送的是密文*/
        public bool _sendMsg(string strMethod, object[] args,byte[] data)
        {
        
            if (data == null) {
                data = new byte[] { };
            }
            Hashtable msgContext = new Hashtable();
            msgContext.Add("m", strMethod);
            msgContext.Add("a", args);

            string s = ReadFileTool.JsonByObject(msgContext);
            NetMngr.GetSingleton().ShowLog("发送消息 :" + s);
            byte[] bufOrign = Encoding.UTF8.GetBytes(s);
            byte[] buf = m_DataEncrypt.Encrypt(bufOrign); // 只对消息体加密
            List<byte> bytes = new List<byte>();
            byte[] messageBodyLenth = BitConverter.GetBytes(18 + buf.Length + data.Length);  // 消息体
            Array.Reverse(messageBodyLenth);
          //  byte[] messageBodyLenth1 = m_DataEncrypt.Encrypt(messageBodyLenth); // 只对消息体加密
            bytes.AddRange(messageBodyLenth);
            
            short messageType = 0;
            byte[] mT = BitConverter.GetBytes(messageType);
            Array.Reverse(mT);
            bytes.AddRange(mT);

            byte[] mTime = BitConverter.GetBytes(_getCurTime());
            Array.Reverse(mTime);
            bytes.AddRange(mTime);

            byte[] dataLenth = BitConverter.GetBytes(data.Length); //语音
            Array.Reverse(dataLenth);
            bytes.AddRange(dataLenth);

            bytes.AddRange(buf);
           
           bytes.AddRange(data);

            //return true;
            //  NetMngr.GetSingleton().ShowLog("发送"+BitConverter.ToString(bytes.ToArray()).Replace("-"," "));
            return _send(bytes.ToArray());
           // return _send(m_DataEncrypt.Encrypt(bytes.ToArray()));
        }

       

        public bool _sendString(string s,byte[] data)
        {
            byte[] buf = Encoding.UTF8.GetBytes(s);

            List<byte> bytes = new List<byte>();
            byte[] messageBodyLenth = BitConverter.GetBytes(18 + buf.Length + data.Length);
            Array.Reverse(messageBodyLenth);
            bytes.AddRange(messageBodyLenth);

            short messageType = 0;
            byte[] mT = BitConverter.GetBytes(messageType);
            Array.Reverse(mT);
            bytes.AddRange(mT);

            byte[] mTime = BitConverter.GetBytes(_getCurTime());
            Array.Reverse(mTime);
            bytes.AddRange(mTime);

            byte[] dataLenth = BitConverter.GetBytes(data.Length);
            Array.Reverse(dataLenth);
            bytes.AddRange(dataLenth);


            bytes.AddRange(buf);
            bytes.AddRange(data);
        
            return _send(bytes.ToArray());
           // return _send(m_DataEncrypt.Encrypt(bytes.ToArray()));
        }

        public bool _sendHeart(string s, byte[] data)
        {
            byte[] heart = Encoding.UTF8.GetBytes(s);

            List<byte> bytes = new List<byte>();
            byte[] messageBodyLenth = BitConverter.GetBytes(18 + heart.Length+ data.Length);
            Array.Reverse(messageBodyLenth);
            bytes.AddRange(messageBodyLenth);

            short messageType = 99;
            byte[] mT = BitConverter.GetBytes(messageType);
            Array.Reverse(mT);
            bytes.AddRange(mT);

            byte[] mTime = BitConverter.GetBytes(_getCurTime());
            Array.Reverse(mTime);
            bytes.AddRange(mTime);

            byte[] dataLenth = BitConverter.GetBytes(data.Length);
            Array.Reverse(dataLenth);
            bytes.AddRange(dataLenth);

            bytes.AddRange(heart);
            bytes.AddRange(data);


            return _send(bytes.ToArray());
            //return _send(m_DataEncrypt.Encrypt(bytes.ToArray()));
        }

        private bool _send(byte[] msg)
        {

            //需要发送的数据
            byte[] byteData = msg;
            byte[] sendData = new byte[byteData.Length];

            try
            {
                int nSendLenth = 0;
                if (MySocket != null)
                {
                    nSendLenth = MySocket.Send(msg);
                    NetMngr.GetSingleton().ShowLog("发送消息了，长度"+ nSendLenth);
                }

                if (nSendLenth == sendData.Length)
                {
                    NetMngr.GetSingleton().ShowLog("发送成功了" + nSendLenth);
                    return true;
                }


                return false;
            }
            catch (Exception e)
            {
                NetMngr.GetSingleton().ShowLog(e.ToString()+" ERROR!!!!!!!!!!!!!!!!");

                return false;
            }
        }

        private void _netConnectedControl()
        {
            Hashtable table = new Hashtable();
            table.Add("m", "NetConnected");
            table.Add("a", new object[] { });
            Message m_MyMessage = new Message();
            m_MyMessage.hashtable = table;

            m_MyMessageControl.AddMessage(m_MyMessage);
        }

        //--------------------------
        //网络模块异常的处理
        //--------------------------

        //-----------------------------------
        //断网之后的处理
        //原则：断网之后，将网络状态全部清除
        //-----------------------------------
        private void _netDownControl()
        {
            //将目前的套接字关闭
            if (MySocket != null)
            {
                MySocket.Close();
                MySocket = null;
            }

            //将加密密钥重置，密钥再次连接之后重新获取
            //m_DataEncrypt.KeyReset();

            //通知界面掉线了
            m_nRelineCount++;
            _tellUINetDown();
        }

        //网络状态不好，断开连接.
        //此处的信息未加密，直接添加到通信队列中
        private void _tellUINetDown()
        {
            Hashtable table = new Hashtable();
            table.Add("m", "NetDown");
            table.Add("a", new object[] { });
            Message m_MyMessage = new Message();
            m_MyMessage.hashtable = table;
            //NetMngr.GetSingleton().MyTransmit.PostMsgControl(m_MyMessage);
            m_MyMessageControl.AddMessage(m_MyMessage);
            if (NetMngr.GetSingleton().OnNetDown != null)
                NetMngr.GetSingleton().OnNetDown();
        }

        //发送RSA的公钥
        private bool SendPublicKey()
        {
            //重新生成RSA的公钥和私钥
            string[] publicKeys = new string[2];
            publicKeys = m_DataEncrypt.NetConnectGetRsaKey();

            String strMethod = "userService/publicKeyForJSON";
            Object[] args = new object[] { publicKeys[0], publicKeys[1] };

            Hashtable table = new Hashtable();
            table.Add("method", strMethod);
            table.Add("args", args);

            //Json进行序列化
            string JsonString = ReadFileTool.JsonByObject(table);

            //发送至网络
            //需要发送的数据
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(JsonString);

            //需要发送数据的长度,此函数得到的数据长度是高低位互换的
            byte[] sizeData = BitConverter.GetBytes(byteData.Length);

            //从socket发送出去的数据
            byte[] sendData = new byte[byteData.Length + Constants.MSGLENTH];

            //反转
            Array.Reverse(sizeData);

            //合并
            System.Buffer.BlockCopy(sizeData, 0, sendData, 0, Constants.MSGLENTH);
            System.Buffer.BlockCopy(byteData, 0, sendData, Constants.MSGLENTH, byteData.Length);

            try
            {
                int nSendLenth = 0;
                nSendLenth = MySocket.Send(sendData, sendData.Length, SocketFlags.None);

                if (nSendLenth == sendData.Length)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                NetMngr.GetSingleton().ShowLog(e.ToString());

                return false;
            }
        }

        public float GetLatency()
        {
            return m_recriveHeartTime - m_sendHeartTime;
        }
    }
}