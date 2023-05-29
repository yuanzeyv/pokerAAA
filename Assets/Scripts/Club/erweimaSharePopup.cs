using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class erweimaSharePopup : BasePopup
{
    public Button cancelBtn;
    public Button wechatBtn;
    public Button wechatCircleBtn;
    public Button qqBtn;

    void Awake()
    {
        Init();

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        wechatBtn = transform.Find("WeChatBtn").GetComponent<Button>();
        wechatCircleBtn = transform.Find("WeChatCircleBtn").GetComponent<Button>();
        qqBtn = transform.Find("QQBtn").GetComponent<Button>();

        cancelBtn.onClick.AddListener(()=> { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView(); 
        });
        wechatBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
           WechatLogin.GetSingleton().OnShareClick(1, "您的好友邀请您加入今年最火的精英德扑游戏!赶快来玩吧！", "精英德扑","www.baidu.com");
           // WechatLogin.GetSingleton().OnShareClick(1, "123", "123456", "www.baidu.com");
        });
        wechatCircleBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            WechatLogin.GetSingleton().OnShareClick(2, "您的好友邀请您加入今年最火的精英德扑游戏!赶快来玩吧！", "精英德扑","www.baidu.com");
            //WechatLogin.GetSingleton().OnShareClick(2, "123", "123456","www.baidu.com");
        });
        qqBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
        });

        gameObject.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}