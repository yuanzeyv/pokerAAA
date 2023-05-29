using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MessageManagerLogin : MonoBehaviour {

    private static MessageManagerLogin _instance;
    public static MessageManagerLogin GetSingleton()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

	void Start ()
    {
        AddListen();
	}

    public void AddListen()
    {
        NetMngr.GetSingleton().AddListener(InterfaceLogin.CheckVersion, OnCheckVersion);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.Login, OnLogin);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.Register, OnRegister);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.ForgetPassword, OnForgetPassword);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.GetVerificationCode, OnGetVerificationCode);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.quitToLogin, OnquitToLogin);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.thirdPartyLogin, OnthirdPartyLogin);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.outOfApp, OnoutOfApp);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.quitLogin, OnquitLogin);
        NetMngr.GetSingleton().AddListener(InterfaceLogin.FixPassword, onFixPassword);

    }

    private void OnquitLogin(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            PlayerPrefs.DeleteAll();
            StaticFunction.Getsingleton().ChangeScene("Login");
        }
        else
        {

        }
    }

    private void onFixPassword(Hashtable data)
    {
        if (data["message"] != null)
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
    }


    private void OnoutOfApp(Hashtable data)
    {
       
    }

    private void OnthirdPartyLogin(Hashtable data)
    {
        if (data["success"].ToString() == "1") {
            string longitude = GetGPS.GetSingleton().GetLongitude().ToString();
            string latitude = GetGPS.GetSingleton().GetLatitude().ToString();
            NetMngr.GetSingleton().Send(InterfaceLogin.Login, new object[] { data["account"].ToString(), data["pwd"].ToString(),"1", longitude, latitude });
        }
       
    }

    private void OnquitToLogin(Hashtable data)
    {
        if (data["type"].ToString() == "0")
        {
            PlayerPrefs.DeleteKey("userid");
            PlayerPrefs.DeleteKey("password");
            PlayerPrefs.DeleteKey("type");
            PopupCommon.GetSingleton().ShowView("异地登录",null,false,delegate { StaticFunction.Getsingleton().ChangeScene("Login"); });
        }
    }

    private void OnGetVerificationCode(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            switch (StaticData.getVerificationCode)
            {
                case 0:
                    PopupCommon.GetSingleton().ShowView(data["message"].ToString());
                    break;
                case 1:
                    //LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().inputYanZhengPanel);
                    PopupCommon.GetSingleton().ShowView(data["message"].ToString());
                    break;
                case 2:
                    LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().setPasswordPanel);
                    PopupCommon.GetSingleton().ShowView(data["message"].ToString());
                    break;
            }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            switch (StaticData.getVerificationCode)
            {
                case 0:
                    LoginManager.GetSingleton().verificationLoginPanel.ReSetVerificationCode();
                    break;
                case 1:
                    LoginManager.GetSingleton().verificationLoginPanel.ReSetVerificationCode();
                    //LoginManager.GetSingleton().planeManager.RemoveTopPlane();
                    break;
                case 2:
                    LoginManager.GetSingleton().setPasswordPanel.ReSetVerificationCode();
                    break;
            }
            
        }
    }

    private void OnForgetPassword(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(),null,false,delegate { LoginManager.GetSingleton().planeManager.RemoveTopPlane(); LoginManager.GetSingleton().planeManager.RemoveTopPlane(); });
            LoginManager.GetSingleton().setPasswordPanel.SetForgetPasswordFinished(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnRegister(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            LoginManager.GetSingleton().registerPanel.SetRegisterFinished(data);

        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnLogin(Hashtable data)
    {
        Debug.Log(data);
        switch (data["success"].ToString())
        {
            case "0":
                PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false, delegate {  });
                PlayerPrefs.DeleteKey("userid");
                PlayerPrefs.DeleteKey("password");
                PlayerPrefs.DeleteKey("type");
                break;
            case "1":
                PlayerPrefs.SetString("password", data["password"].ToString());
                PlayerPrefs.SetString("type", "0");
                if (data.ContainsKey("url"))
                {
                    StaticData.url = data["url"].ToString();
                    StaticData.ID = data["id"].ToString();
                    StaticData.diamond = int.Parse(data["diamond"].ToString());
                    StaticData.gold = int.Parse(data["coin"].ToString());
                    StaticData.unionCoin = int.Parse(data["lmCoin"].ToString());
                    StaticData.username = data["userName"].ToString();
                }
                LoginManager.GetSingleton().passwordLoginPanel.SetLoginFinished(data);
                StaticFunction.Getsingleton().ChangeScene("Main");
                break;
        }
    }

    private void OnCheckVersion(Hashtable data)
    {
        if (SceneManager.GetActiveScene().name == "Login")
        {
            LoginManager.GetSingleton().CheckVersionFinished(data);
        }
        else
        {
            HallManager.GetSingleton().settingTopPanel.CheckVersionFinished(data);
        }
    }

    void Update ()
    {
	
	}
}
