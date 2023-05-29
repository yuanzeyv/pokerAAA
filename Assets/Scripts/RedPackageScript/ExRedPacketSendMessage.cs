using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
/// <summary>
/// 红包示例发送消息
/// </summary>
public class ExRedPacketSendMessage : MonoBehaviour
{

    private ChatScroller chatScroller;

	// Use this for initialization
	void Start ()
	{

        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();

        //List<Hashtable> datas = new System.Collections.Generic.List<Hashtable>();
        ////for (int i = 0; i < 200; i++)
        ////{
        ////       datas.Add(new Hashtable());
        ////    datas[i]["Type"] = Random.Range(0, 3);
        ////    datas[i]["peopleDirection"] = Random.Range(0, 2);
        ////    datas[i]["Nickname"] = "爱仕达多";
        ////    datas[i]["HeadUrl"] = Convert.ToInt32(datas[i]["peopleDirection"]) == 0 ? "http://b.hiphotos.baidu.com/image/pic/item/0ff41bd5ad6eddc4802878ba34dbb6fd536633a0.jpg" : "http://b.hiphotos.baidu.com/image/h%3D300/sign=291caeb2923df8dcb93d8991fd1072bf/aec379310a55b319b21722304ea98226cefc178c.jpg";
        ////    datas[i]["Message"] = "撒旦法师打发阿斯";
        ////    datas[i]["ChatMessage"] = "阿斯顿发斯蒂芬阿萨德发送到发送打对方啊啊发是的发生发送对方阿道夫啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
        ////    datas[i]["Title"] = "啊啊";
        ////   }
        //chatScroller.SetDatas(datas);


        Hashtable data = new Hashtable();

        /////-----------------------------------
        /////Type: 0 系统提示 1 正常消息 2 红包消息
        /////-----------------------------------
        /////0：
        /////  Message 为显示内容
        /////-----------------------------------
        /////1：
        /////  HeadUrl 为头像地址
        /////  Nickname 为玩家昵称
        /////  ChatMessage 为聊天信息
        /////  peopleDirection 为显示方向
        /////-----------------------------------
        /////2：
        /////  HeadUrl 为头像地址
        /////  Nickname 为玩家昵称
        /////  Title 为红包信息内容
        /////  peopleDirection 为显示方向
        /////-----------------------------------

        //data = new Hashtable();
        //data["Type"] = 0;
        //data["Message"] = "早上14:25";

        //data["peopleDirection"] = 1;
        //data["Nickname"] = "";
        //data["HeadUrl"] = "";
        //data["ChatMessage"] = "";
        //data["Title"] = "";

        //chatScroller.AddDataAt(chatScroller.DataLength, data);

        //data = new Hashtable();
        //data["Type"] = 1;
        //data["peopleDirection"] = 0;
        //data["HeadUrl"] = "http://www.9shadow.com/new_img/2-3.png";
        //data["Nickname"] = "黑猫";
        //data["ChatMessage"] = "HM1这是一条很长很长的聊天信息。....................................................................";

        //data["Title"] = "";
        //data["Message"] = "";

        //chatScroller.AddDataAt(chatScroller.DataLength, data);

        //data = new Hashtable();
        //data["Type"] = 1;
        //data["peopleDirection"] = 1;
        //data["HeadUrl"] = "http://www.9shadow.com/new_img/2-3.png";
        //data["Nickname"] = "黑猫";
        //data["ChatMessage"] = "HM2这是一条很长很长的聊天信息。....................................................................";

        //data["Title"] = "";
        //data["Message"] = "";

        //chatScroller.AddDataAt(chatScroller.DataLength, data);

        //data = new Hashtable();
        //data["Type"] = 2;
        //data["peopleDirection"] = 0;
        //data["HeadUrl"] = "http://www.9shadow.com/new_img/2-3.png";
        //data["Nickname"] = "黑猫";
        //data["Title"] = "黑猫1发送了一个红包";

        //data["ChatMessage"] = "";
        //data["Message"] = "";
        //chatScroller.AddDataAt(chatScroller.DataLength, data);

        //data = new Hashtable();
        //data["Type"] = 2;
        //data["peopleDirection"] = 1;
        //data["HeadUrl"] = "http://www.9shadow.com/new_img/2-3.png";
        //data["Nickname"] = "黑猫";
        //data["Title"] = "黑猫2发送了一个红包";

        //data["ChatMessage"] = "";
        //data["Message"] = "";
        //chatScroller.AddDataAt(chatScroller.DataLength, data);

        ////data["Type"] = 0;
        ////data["peopleDirection"] = 1;
        ////data["Nickname"] = "测试";
        ////data["HeadUrl"] = "https://lh3.googleusercontent.com/57mz1RFFaR5k_AdKZQEnLyY7cSu3Mw6fXBTEGKnGHWOzoWPPpF-cHINmDoIk6X4lDg-X=s64";
        ////data["Message"] = "HM这是一条系统信息";
        ////data["ChatMessage"] = inputMessage.text;
        ////data["Title"] = "";
        ////chatScroller.AddDataAt(chatScroller.DataLength, data);

        //chatScroller.MoveToEnd();
    }
    
}
