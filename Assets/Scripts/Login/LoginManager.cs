using UnityEngine;
using System.Collections;

public class LoginManager : MonoBehaviour {

    public SelectLoginPanel selectLoginPanel;
    public PasswordLoginPanel passwordLoginPanel;
    public VerificationLoginPanel verificationLoginPanel;
    //public InputPhonePanel inputPhonePanel;
    //public InputYanZhengPanel inputYanZhengPanel;
    public RegisterPanel registerPanel;
    public SetPasswordPanel setPasswordPanel;
    //public SurePhonePopup surePhonePopup;

    private RectTransform containerBottom;
    private RectTransform containerTop;

    public PlaneManager planeManager;

    private bool checkUpdate;//检测版本更新

    private static LoginManager singleton;
    public static LoginManager GetSingleton()
    {
        return singleton;
    }
    private void Awake()
    {
        singleton = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        selectLoginPanel = transform.Find("ContainerBottom/SelectLoginPanel").gameObject.AddComponent<SelectLoginPanel>();
        passwordLoginPanel = transform.Find("ContainerBottom/PasswordLoginPanel").gameObject.AddComponent<PasswordLoginPanel>();
        verificationLoginPanel = transform.Find("ContainerBottom/VerificationLoginPanel").gameObject.AddComponent<VerificationLoginPanel>();
        //inputPhonePanel = transform.Find("ContainerBottom/InputPhonePanel").gameObject.AddComponent<InputPhonePanel>();
        //inputYanZhengPanel = transform.Find("ContainerBottom/InputYanZhengPanel").gameObject.AddComponent<InputYanZhengPanel>();
        registerPanel = transform.Find("ContainerBottom/RegisterPanel").gameObject.AddComponent<RegisterPanel>();
        setPasswordPanel = transform.Find("ContainerBottom/SetPasswordPanel").gameObject.AddComponent<SetPasswordPanel>();
        //surePhonePopup = transform.Find("SurePhonePopup").gameObject.AddComponent<SurePhonePopup>();

        containerBottom = transform.Find("ContainerBottom").GetComponent<RectTransform>();
        containerTop = transform.Find("ContainerTop").GetComponent<RectTransform>();
        planeManager = GetComponent<PlaneManager>();
        planeManager.Init(containerBottom, containerTop);
        planeManager.movePosition = 1;
        planeManager.topPlaneMoveTime = 0.4f;
        planeManager.sidePlaneMoveTime = 0.4f;
    }
	
	void Start ()
    {
        planeManager.ShowBottomPlane(selectLoginPanel);
        passwordLoginPanel.gameObject.SetActive(false);
        //planeManager.ShowBottomPlane(passwordLoginPanel);
        //selectLoginPanel.gameObject.SetActive(false);
        verificationLoginPanel.gameObject.SetActive(false);
        //inputPhonePanel.gameObject.SetActive(false);
        //inputYanZhengPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(false);
        setPasswordPanel.gameObject.SetActive(false);
        //surePhonePopup.gameObject.SetActive(false);
        PopupCommon.GetSingleton().gameObject.SetActive(false);
        Waitting.getInstance().Hide();

        GetGPS.GetSingleton().StartGPS();
    }
	
	void Update ()
    {
        if (NetMngr.GetSingleton().IsConnect())
        {
            if (!checkUpdate)
            {
                int type =
#if UNITY_ANDROID
                0;
#elif UNITY_IOS
                1;
#elif UNITY_STANDALONE
                2;
#endif
                NetMngr.GetSingleton().Send(InterfaceLogin.CheckVersion, new object[] { StaticData.version, type });
                checkUpdate = true;
            }
        }
        else
        {
            Debug.Log("NetMngr.GetSingleton().IsConnect() 连接失败");
        }
    }
    //版本检测
    public void OnDestroy()
    {
        if (isUpdateQuit)
        {
            isUpdateQuit = false;
            Application.OpenURL(StaticData.UpdateURL);
        }
    }
    public bool isUpdateQuit = false;
    string updateUrl = "";
    public void CheckVersionFinished(Hashtable data)
    {
  //       if (LoadTxt.state != "0")
  //       {
  //           PopupCommon.GetSingleton().ShowView(LoadTxt.tips, null, false, delegate
  //           {
  //               Application.Quit();
  //           });
  //           return;
  //       }
  //       if (data.ContainsKey("url"))
  //       {
  //           StaticData.UpdateURL = data["url"].ToString();
  //       }
  //       if (data.ContainsKey("showLog"))
  //       {
  //           StaticData.isDebug = data["showLog"].ToString() == "0" ? false : true;
  //       }
		// StaticData.isDebug = false;
  //       StaticFunction.Getsingleton().ShowLog(StaticData.isDebug);
        int success = 2; // int.Parse(data["success"].ToString());
  //       if (success == 1)
  //       {
  //           updateUrl = data["url"].ToString();
  //           PopupCommon.GetSingleton().ShowView("请更新版本", null, false, delegate
  //           {
  //               isUpdateQuit = true;
  //               //Application.OpenURL(StaticData.UpdateURL);
  //               Application.Quit();
  //           });
  //       }
        // else if (success == 2)
        if (success == 2)
        {
            // PlayerPrefs.DeleteAll();
            // 版本更新，自动进入
            string tempid = PlayerPrefs.GetString("userid");
            string temppassword = PlayerPrefs.GetString("password");
            string type = PlayerPrefs.GetString("type");
            string longitude = GetGPS.GetSingleton().GetLongitude().ToString();
            string latitude = GetGPS.GetSingleton().GetLatitude().ToString();
            Debug.Log(type);
            if (!string.IsNullOrEmpty(tempid) && !string.IsNullOrEmpty(temppassword))
            {
                NetMngr.GetSingleton().Send(InterfaceLogin.Login, new object[] { tempid, temppassword, type, longitude, latitude });
                return;
            }
        }
    }
}
