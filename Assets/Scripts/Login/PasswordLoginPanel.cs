using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PasswordLoginPanel : BasePlane
{

    private Button back;
    private InputField phoneNumber;
    private InputField password;
    private Button loginOn;
    private Button forgetPassword;
    private Button register;
    private Button verificationLogin;
    private Text text;

    

    private void Awake()
    {
        phoneNumber = transform.Find("InputPhoneNumber").GetComponent<InputField>();
        password = transform.Find("InputVerificationCode").GetComponent<InputField>();
        loginOn = transform.Find("BtnLoginOn").GetComponent<Button>();
        forgetPassword = transform.Find("BtnForget").GetComponent<Button>();
        register = transform.Find("BtnRegister").GetComponent<Button>();
        verificationLogin = transform.Find("BtnVerificationLogin").GetComponent<Button>();
        back = transform.Find("Back").GetComponent<Button>();
        text = transform.Find("Text").GetComponent<Text>();

        if (PlayerPrefs.HasKey("userid"))
        {
            phoneNumber.text = PlayerPrefs.GetString("userid");
        }
        

        phoneNumber.onValueChanged.AddListener((value) =>
        {
            //loginOn.interactable = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(password.text);
        });
        password.onValueChanged.AddListener((value) =>
        {
            //loginOn.interactable = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(phoneNumber.text);
        });
        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            LoginManager.GetSingleton().planeManager.RemoveTopPlane();
            phoneNumber.text = PlayerPrefs.HasKey("userid") ? PlayerPrefs.GetString("userid") : "";
            password.text = "";
        });
        loginOn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ////登录
            //if (phoneNumber.text.Length != 11)
            //{
            //    text.text = "手机号输入有误";
            //    return;
            //}
            //else if (password.text.Length <= 5 || password.text.Length >= 13)
            //{
            //    text.text = "密码长度不符,请输入6-12位数字或者字母";
            //    return;
            //}
            //else
            //{
                text.text = "";

                //发送登录请求
                PlayerPrefs.SetString("userid", phoneNumber.text);
                PlayerPrefs.SetString("password", password.text);
                PlayerPrefs.SetString("type", "0");
            string longitude = GetGPS.GetSingleton().GetLongitude().ToString();
            string latitude = GetGPS.GetSingleton().GetLatitude().ToString();
            NetMngr.GetSingleton().Send(InterfaceLogin.Login, new object[] { phoneNumber.text, password.text, "0", longitude, latitude });
           //}

        });
        forgetPassword.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().setPasswordPanel);
            phoneNumber.text = PlayerPrefs.HasKey("userid") ? PlayerPrefs.GetString("userid") : "";
            password.text = "";
        });
        register.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().registerPanel);
            phoneNumber.text = PlayerPrefs.HasKey("userid") ? PlayerPrefs.GetString("userid") : "";
            password.text = "";
        });
        verificationLogin.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().verificationLoginPanel);
            phoneNumber.text = PlayerPrefs.HasKey("userid") ? PlayerPrefs.GetString("userid") : "";
            password.text = "";
        });
    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}
    public void SetLoginFinished(Hashtable data)
    {

    }
    /// <summary>
    /// 第三方获取账号
    /// </summary>
    /// <param name="data"></param>
    public void ThirdPartyLoginFinish(Hashtable data)
    {
        switch (data["success"].ToString())
        {
            case "0":
                PopupCommon.GetSingleton().ShowView(data["message"].ToString());
                break;
            case "1":
                PlayerPrefs.SetString("userid", data["account"].ToString());
                PlayerPrefs.SetString("password", data["pwd"].ToString());
                PlayerPrefs.SetString("type", "1");
                string longitude = GetGPS.GetSingleton().GetLongitude().ToString();
                string latitude = GetGPS.GetSingleton().GetLatitude().ToString();
                NetMngr.GetSingleton().Send(InterfaceLogin.Login, new object[] { data["account"].ToString(), data["pwd"].ToString(), "1", longitude, latitude });
                break;
        }
    }
    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
