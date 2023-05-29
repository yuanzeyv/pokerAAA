using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

using Constant;
using MessageControlSpace;
using MySocket;

namespace MyThread
{
    public class PostMessageThread
    {
        //private bool m_flag;

        private Message m_MyMessage;

        private MessageControlSpace.MessageControl m_MyMessageControl;

        private Transmit m_MyTransmit;

        public PostMessageThread()
        {

        }

        public void PostMessageThreadParaInit()
        {
            //m_flag = false;

            m_MyMessage = new Message();
        }

        public void PostMessageThreadGetPoint(MessageControlSpace.MessageControl MyMessageControl, Transmit MyTransmit)
        {
            this.m_MyMessageControl = MyMessageControl;
            this.m_MyTransmit = MyTransmit;
        }

        public void PostThread()
        {
			int nCount = 1;

            while (nCount > 0 && m_MyMessageControl.PostMessage(ref m_MyMessage))
            {
                //将去除的信息进行解析转发,未解密的数据
                m_MyTransmit.PostMsgControl(m_MyMessage);
                --nCount;
            }
        }

        /*在程序从后台切回时，消除之前的网络消息*/
        public void ClearAllState()
        {
            while (m_MyMessageControl.PostMessage(ref m_MyMessage))
            {
                m_MyTransmit.SelectPostMsgControl(m_MyMessage);
            }
        }
    }
}