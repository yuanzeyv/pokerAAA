using System;
using UnityEngine;

namespace Constant
{
    public class Constants
    {
        // 服务器是否连接成功
        public static bool socketConnected = false;

        //服务器IP
        public static string IP;

        //端口号
        public static int PORT;

        //用于存放报文中有效信息体长度变量的字节数
        public const int MSGLENTH = 4;
    }

    //-----------------------
    //线程之间交互数据的格式
    //-----------------------
    public struct ThreadContext
    {
        public String MsgString;

        public ThreadContext(bool flag)
        {
            MsgString = null;
        }
    }
}