using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using UnityEngine.SceneManagement;

public class WechatLogin : MonoBehaviour {
    //public ShareSDK shareSdk;
    public Text message;
    // Use this for initialization
    public static WechatLogin wechat;

    public static WechatLogin GetSingleton()
    {
        return wechat;
    }

    private void Awake()
    {
        if (wechat == null)
        {
            wechat = this;
        }
        //shareSdk = GameObject.Find("Main Camera").GetComponent<ShareSDK>();
    }
        void Start()
    {
        //shareSdk = gameObject.GetComponent<ShareSDK>();
        ////授权回调事件
        //shareSdk.authHandler += AuthResultHandler;
        ////分享回调事件
        //shareSdk.shareHandler += ShareResultHandler;
        ////用户信息事件
        //shareSdk.showUserHandler += GetUserInfoResultHandler;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
    }

    //分享
    public void OnShareClick(int type, string text = "", string title = "", string url = "")
    {
        //ShareContent content = new ShareContent();
        //content.SetTitle(title);//分享标题文本
        //content.SetText(text);//分享文本

        //content.SetUrl(url);//分享网址
        //content.SetImageUrl("http://106.14.25.203/F/dz.png");//设置icon
        //content.SetShareType(ContentType.Webpage);

        //if (type == 1)
        //{
        //    shareSdk.ShareContent(PlatformType.WeChat, content);
        //}
        //else {
        //    shareSdk.ShareContent(PlatformType.WeChatMoments, content);
        //}
    }

    /*
    /// <summary>


    // 分享结果回调
    void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        Debug.Log("enter");
        //成功
        if (state == ResponseState.Success)
        {
            Debug.Log("success");
            if (ClubManager.GetSingleton() != null) {
                ClubManager.GetSingleton().erweimaPopup.HideView();
            }
            
        }
        //失败
        else if (state == ResponseState.Fail)
        {
            Debug.Log("fail");
           
        }
        //关闭
        else if (state == ResponseState.Cancel)
        {
            Debug.Log("cancel");
            
        }
        Debug.Log("out");
    }
    //授权
    public void OnAuthClick()
    {
        //请求微信授权//请求这个授权是为了获取用户信息来第三方登录
        shareSdk.Authorize(PlatformType.WeChat);
    }
    //授权结果回调
    void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        Debug.Log("enter");
        if (state == ResponseState.Success)
        {
            Debug.Log("success");
            //message.text = ("authorize success !");

            //授权成功的话，获取用户信息
            shareSdk.GetUserInfo(type);
            //登录场景界面
            Hashtable ht = shareSdk.GetAuthInfo(type);
            string token = "";
            string openID = "";
#if UNITY_ANDROID
            token = ht["token"].ToString();
            openID = ht["openID"].ToString();
#elif UNITY_IOS || UNITY_IPHONE
        token = ht["uid"].ToString();
        openID = ht["openid"].ToString();
#endif

            NetMngr.GetSingleton().Send(InterfaceLogin.thirdPartyLogin, new object[] { openID, token});

        }
        else if (state == ResponseState.Fail)
        {
            Debug.Log("fail");
            //message.text = ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            Debug.Log("cancel");
            // message.text = ("cancel !");
        }
        Debug.Log("out");
    }
    //获取用户信息
    void GetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            //获取成功的话 可以写一个类放不同平台的结构体，用PlatformType来判断，用户的Json转化成结构体，来做第三方登录。
            switch (type)
            {
                case PlatformType.WeChat:
                   // message.text = (MiniJSON.jsonEncode(result));  //Json

                    break;
            }
        }
        else if (state == ResponseState.Fail)
        {
            //message.text = ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            //message.text = ("cancel !");
        }
    }
    */
}
