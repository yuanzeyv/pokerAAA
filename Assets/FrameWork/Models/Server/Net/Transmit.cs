using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Constant;
using MessageControlSpace;

using UnityEngine;
using UnityEngine.SceneManagement;


namespace MySocket
{
    public class Transmit
    {
        private CreateSocket m_CreateSocket;

        public void TransmitGetPoint(CreateSocket MyCreateSocket)
        {
            m_CreateSocket = MyCreateSocket;
            callbacks = new Dictionary<string, Callback>();
        }

        public void ResetTransmit(CreateSocket MyCreateSocket)
        {
            m_CreateSocket = MyCreateSocket;
        }

        public delegate void Callback(Hashtable message);
        private Dictionary<string, Callback> callbacks;

        public void AddEventListener(string name, Callback call)
        {
            if (callbacks.ContainsKey(name) == false)
                callbacks.Add(name, call);
            else
                callbacks[name] = call;
        }
       
        public void RemoveEventListener(string name)
        {
            if (callbacks.ContainsKey(name))
            {
                callbacks.Remove(name);
            }
        }
        public void PostMsgControl(Message m_MyMessage)
        {
            Hashtable table = m_MyMessage.hashtable;
            string method = table["m"].ToString();

            Hashtable args = new Hashtable();

            args = table["a"] as Hashtable;
            Message myMessage = new Message();
            myMessage.hashtable = args;
            myMessage.data = m_MyMessage.data;
            if (method == "NetDown")
            {
                Constants.socketConnected = false;
                m_CreateSocket.CreateReceiveThread();
                if (StaticData.selectScene == 0)
                {
                    Waitting.getInstance().Show();
                }
                else
                    MessageManagerGame.GetSingleton().SetNetDown = true;
            }
            else if (method == "NetConnected")
            {
                Constants.socketConnected = true;
                if (StaticData.selectScene == 0)
                    Waitting.getInstance().Hide();
            }

            if (callbacks.ContainsKey(method))
            {              
                Hashtable ht = myMessage.hashtable==null?new Hashtable():myMessage.hashtable;
                ht.Add("byteData", myMessage.data);
                callbacks[method](ht);
            }

        }

        /*从后台切回的过滤处理*/
        public void SelectPostMsgControl(Message m_MyMessage)
        {

        }
    }
}