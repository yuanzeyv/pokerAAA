using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPasswordPanel : BasePlane
{
    private Button back;
    private Button getCode;
    private InputField phoneNumber;
    private InputField newpassword;
    private InputField verificationCode;
    private Button sumbit;
   // private Text tip;

    private Text regetcode;

    private bool isStart = false;
    private float timer = 61;

    private void Awake()
    {
        back = transform.Find("Back").GetComponent<Button>();
        phoneNumber = transform.Find("PhoneNumberInput").GetComponent<InputField>();
        getCode = transform.Find("GetYanZhengMaBtn").GetComponent<Button>();
        newpassword = transform.Find("password").GetComponent<InputField>();
        verificationCode = transform.Find("VerificationCodeInput").GetComponent<InputField>();
        sumbit = transform.Find("SubmitBtn").GetComponent<Button>();
       // tip = transform.Find("sendTip").GetComponent<Text>();
        regetcode = transform.Find("Text").GetComponent<Text>();

        newpassword.onValueChanged.AddListener((value) =>
        {
            sumbit.interactable = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(verificationCode.text);
        });
        verificationCode.onValueChanged.AddListener((value) =>
        {
            sumbit.interactable = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(newpassword.text);
        });
        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            LoginManager.GetSingleton().planeManager.RemoveTopPlane();
            ReSetVerificationCode();
        });
        getCode.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StaticData.getVerificationCode = 2;
            OnGetVerificationCode();
            //LoginManager.GetSingleton().surePhonePopup.ShowView();
            //LoginManager.GetSingleton().surePhonePopup.SetMessage(phoneNumber);
        });
        sumbit.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceLogin.ForgetPassword, new object[] { phoneNumber.text, newpassword.text, verificationCode.text });
        });
    }

	void Start ()
    {
	
	}
	

	void Update ()
    {
        if (isStart)
        {
            timer -= Time.deltaTime;
            regetcode.text = "重新获取(" + (int)timer + ")";
            if (timer < 0)
            {
                ReSetVerificationCode();
            }
        }
    }

    public void SendData(Hashtable data)
    {
       // phoneNumber = data["phoneNumber"].ToString();
    }

    public void OnGetVerificationCode()
    {
        //发送验证码
        NetMngr.GetSingleton().Send(InterfaceLogin.GetVerificationCode, new object[] { phoneNumber.text, StaticData.getVerificationCode.ToString() });
        getCode.gameObject.SetActive(false);
        regetcode.gameObject.SetActive(true);
        isStart = true;
    }
    public void ReSetVerificationCode()
    {
        isStart = false;
        regetcode.text = "";
        timer = 61;
        regetcode.gameObject.SetActive(false);
        getCode.gameObject.SetActive(true);
    }
    public void SetForgetPasswordFinished(Hashtable data)
    {
        
    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        //tip.text = "我们已给手机号<color=#ffbf59>" + phoneNumber + "</color>发送了验证码";
        //StaticData.getVerificationCode = 2;
        //OnGetVerificationCode();
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
