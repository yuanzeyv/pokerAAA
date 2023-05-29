using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanel : BasePlane
{
    private Button back;
    private InputField phoneNumber;
    private InputField password;
    private InputField verificationCode;
    private Button nextOn;
    private Toggle agreementToggle;
    private Button agreementBtn;
    //private Text text;

    private Button getCode;
    private Text regetcode;
    private bool isStart = false;
    private float timer = 61;

    private void Awake()
    {
        back = transform.Find("Back").GetComponent<Button>();
        phoneNumber = transform.Find("PhoneNumberInput").GetComponent<InputField>();
        password = transform.Find("password").GetComponent<InputField>();
        verificationCode = transform.Find("VerificationCodeInput").GetComponent<InputField>();
        nextOn = transform.Find("SubmitBtn").GetComponent<Button>();
        //agreementToggle = transform.Find("AgreementToggle").GetComponent<Toggle>();
        //agreementBtn = transform.Find("AgreementButton").GetComponent<Button>();
        //text = transform.Find("Text").GetComponent<Text>();

        getCode = transform.Find("GetYanZhengMaBtn").GetComponent<Button>();
        regetcode = transform.Find("Text").GetComponent<Text>();

        getCode.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StaticData.getVerificationCode = 1;
            OnGetVerificationCode();
        });

        phoneNumber.onValueChanged.AddListener((str) =>
        {
            nextOn.interactable = !string.IsNullOrEmpty(str)&&!string.IsNullOrEmpty(password.text);
        });
        password.onValueChanged.AddListener((str) =>
        {
            nextOn.interactable = !string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(phoneNumber.text);
        });
        //agreementToggle.onValueChanged.AddListener((isOn) =>
        //{
        //    nextOn.interactable = !string.IsNullOrEmpty(password.text) && !string.IsNullOrEmpty(phoneNumber.text) && isOn;
        //});

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            LoginManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        nextOn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //if (phoneNumber.text.Length!= 11)
            //{
            //    text.text = "手机号输入有误";
            //    return;
            //}
            //else if (password.text.Length<=5||password.text.Length>=13)
            //{
            //    text.text = "密码长度不符,请输入6-12位数字或者字母";
            //    return;
            //}
            //else
            //{
            //    text.text = "";
                //Hashtable data = new Hashtable();
                //data.Add("phoneNumber",phoneNumber.text);
                //data.Add("password", password.text);
                //phoneNumber.text = "";
                //password.text = "";
                StaticData.getVerificationCode = 1;
                NetMngr.GetSingleton().Send(InterfaceLogin.Register, new object[] { phoneNumber.text, password.text, verificationCode.text });

            //LoginManager.GetSingleton().inputYanZhengPanel.SendData(data);
            //LoginManager.GetSingleton().inputYanZhengPanel.OnGetVerificationCode();
            //LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().inputYanZhengPanel);
            //}
        });
        //agreementBtn.onClick.AddListener(() =>
        //{
        //    PopupCommon.GetSingleton().ShowView("服务协议");
        //});
    }

    public void OnGetVerificationCode()
    {
        //发送验证码
        NetMngr.GetSingleton().Send(InterfaceLogin.GetVerificationCode, new object[] { phoneNumber.text, StaticData.getVerificationCode.ToString() });
        getCode.gameObject.SetActive(false);
        regetcode.gameObject.SetActive(true);
        isStart = true;
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

    //重置验证码数据
    public void ReSetVerificationCode()
    {
        isStart = false;
        regetcode.text = "";
        timer = 61;
        regetcode.gameObject.SetActive(false);
        getCode.gameObject.SetActive(true);
    }

    public void SetRegisterFinished(Hashtable data)
    {
       
        LoginManager.GetSingleton().planeManager.RemoveTopPlane();
   //     LoginManager.GetSingleton().planeManager.RemoveTopPlane();
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
