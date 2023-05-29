//---------------------------------------------------
//用于socket线程和其它线程共享的信息数据。使用Dequeue
//---------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using Constant;

namespace MessageControlSpace
{
    public class MessageControl
    {
        //共享资源的互斥锁
        private Mutex m_MessageControlMutex;

        //共享的数据队列
        private Queue m_MsgQueue;


        //本类中数据的初始化
        public void MessageControlParaInit()
        {
            m_MessageControlMutex = new Mutex();

            m_MsgQueue = new Queue();
        }

        //添加数据
        public void AddMessage(Message MyMessage)
        {
            m_MessageControlMutex.WaitOne();

            m_MsgQueue.Enqueue(MyMessage);

            m_MessageControlMutex.ReleaseMutex();
        }

        //读取数据
        public bool PostMessage(ref Message MyMessage)
        {
            m_MessageControlMutex.WaitOne();

            if (m_MsgQueue.Count <= 0)
            {
                m_MessageControlMutex.ReleaseMutex();

                return false;
            }

            MyMessage = (Message)m_MsgQueue.Dequeue();

            m_MessageControlMutex.ReleaseMutex();

            return true;
        }

        public void ClearMessage()
        {
            m_MsgQueue.Clear();
        }
    }
}
