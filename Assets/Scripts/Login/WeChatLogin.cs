using UnityEngine;
using System.Collections;
using cn.sharesdk.unity3d;

public class WeChatLogin : MonoBehaviour {

    //public ShareSDK ssdk;
    private static WeChatLogin _instance;
    public static WeChatLogin GetSingleton()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        //ssdk = gameObject.GetComponent<ShareSDK>();

        //ssdk.authHandler = AuthResultHandler;
        //ssdk.showUserHandler = GetUserInfoResultHandler;
        //ssdk.shareHandler = ShareResultHandler;
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
//    /// <summary>
//    /// 分享微信好友
//    /// </summary>
//    /// <param name="text"></param>文本
//    /// <param name="url"></param>链接
//    /// <param name="imgUrl"></param>图片
//    /// <param name="codeImgUrl"></param>二维码图片
//    public void ShareWechat(string text = "",string title="", string url = "", bool isWebPage = true)
//    {
//        ShareContent content = new ShareContent();
//        content.SetTitle(title);//分享标题文本
//        content.SetText(text);//分享文本
//        if (isWebPage)
//        {
//            content.SetUrl(url);//分享网址
//            content.SetImageUrl("http://106.14.25.203/F/1545982800145.png");//设置icon
//            content.SetShareType(ContentType.Webpage);
//        }
//        else
//        {
//            content.SetImagePath(Application.persistentDataPath + "/shot.png");
//            content.SetShareType(ContentType.Image);
//        }
//        ssdk.ShareContent(PlatformType.WeChat, content);
//    }

//    /// <summary>
//    /// 分享朋友圈
//    /// </summary>
//    /// <param name="text"></param>文本
//    /// <param name="url"></param>链接
//    public void ShareWeChatMoments(string text = "", string title = "", string url = "", bool isWebPage = true)
//    {
//        ShareContent content = new ShareContent();
//        content.SetTitle(title);//分享标题文本
//        content.SetText(text);//QQ空间分享文本
//        if (isWebPage)
//        {
//            content.SetUrl(url);//分享网址
//            content.SetImageUrl("http://106.14.25.203/F/1545982800145.png");//设置icon
//            content.SetShareType(ContentType.Webpage);
//        }
//        else
//        {
//            content.SetImagePath(Application.persistentDataPath + "/shot.png");
//            content.SetShareType(ContentType.Image);
//        }
//        ssdk.ShareContent(PlatformType.WeChatMoments, content);
//    }
//    /// <summary>
//    /// 指定授权的回调
//    /// </summary>
//    /// <param name="reqID"></param>
//    /// <param name="state"></param>
//    /// <param name="type"></param>
//    /// <param name="result"></param>
//    private void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
//    {
//        if (state == ResponseState.Success)
//        {
//            ssdk.GetUserInfo(type);
//            Hashtable ht = ssdk.GetAuthInfo(type);

//            string token = "";
//            string openID = "";
//#if UNITY_ANDROID
//            token = ht["token"].ToString();
//            openID = ht["openID"].ToString();
//#elif UNITY_IOS || UNITY_IPHONE
//        token = ht["uid"].ToString();
//        openID = ht["openid"].ToString();
//#endif

//            //NetMngr.GetSingleton().Send(InterfaceHall.thirdPartyLogin, new object[] { openID, token, type == PlatformType.QQ ? "2" : "1" });
//        }
//        else if (state == ResponseState.Fail)
//        {
//            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
//        }
//        else if (state == ResponseState.Cancel)
//        {
//            print("cancel !");
//        }
//    }
//    /// <summary>
//    /// 获取指定用户的信息回调
//    /// </summary>
//    /// <param name="reqID"></param>
//    /// <param name="state"></param>
//    /// <param name="type"></param>
//    /// <param name="result"></param>
//    void GetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
//    {
//        if (state == ResponseState.Success)
//        {
//            print("get user info result :");
//           // print(MiniJSON.jsonEncode(result));
//        }
//        else if (state == ResponseState.Fail)
//        {
//        //    print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
//        }
//        else if (state == ResponseState.Cancel)
//        {
//            print("cancel !");
//        }
//    }
//    /// <summary>
//    /// 定制分享的回调
//    /// </summary>
//    /// <param name="reqID"></param>
//    /// <param name="state"></param>
//    /// <param name="type"></param>
//    /// <param name="result"></param>
//    private void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
//    {
//        if (state == ResponseState.Success)
//        {
//            print("share result :");
//         //   print(MiniJSON.jsonEncode(result));
//            //NetMngr.GetSingleton().Send(InterfaceHall.getShareInfo, new object[] { });
//        }
//        else if (state == ResponseState.Fail)
//        {
//            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
//        }
//        else if (state == ResponseState.Cancel)
//        {
//            print("cancel !");
//        }
//    }
//    public void CancleAuth()
//    {
//        ssdk.CancelAuthorize(PlatformType.WeChat);
//        ssdk.CancelAuthorize(PlatformType.QQ);
//    }
}
