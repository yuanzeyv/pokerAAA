//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class InputYanZhengPanel : BasePlane
//{
//    private Button submit;
//    private Button getCode;
//    private Text regetcode;
//    private Button back;
//    private Text message;

//    private InputField verificationCode;
//    private Text num1;
//    private Text num2;
//    private Text num3;
//    private Text num4;
//    private Text num5;
//    private Text num6;


//    private float timer=61;
//    private bool isStart = false;

//    private string phoneNumber;
//    private string password;

//    private void Awake()
//    {
//        num1 = transform.Find("Number/num1").GetComponent<Text>();
//        num2 = transform.Find("Number/num2").GetComponent<Text>();
//        num3 = transform.Find("Number/num3").GetComponent<Text>();
//        num4 = transform.Find("Number/num4").GetComponent<Text>();
//        num5 = transform.Find("Number/num5").GetComponent<Text>();
//        num6 = transform.Find("Number/num6").GetComponent<Text>();
//        submit = transform.Find("SubmitBtn").GetComponent<Button>();
//        getCode = transform.Find("Number/GetYanZhengMaBtn").GetComponent<Button>();
//        back = transform.Find("Back").GetComponent<Button>();
//        verificationCode = transform.Find("InputField").GetComponent<InputField>();
//        message = transform.Find("message").GetComponent<Text>();
//        regetcode = transform.Find("Number/Text").GetComponent<Text>();

//        verificationCode.onValueChanged.AddListener(delegate {
//            char[] chars = verificationCode.text.ToCharArray();
//            if (verificationCode.text.Length == 0)
//            {
//                num1.text = "";
//                num2.text = "";
//                num3.text = "";
//                num4.text = "";
//                num5.text = "";
//                num6.text = "";
//            }
//            else if (verificationCode.text.Length==1)
//            {
//                num1.text = chars[0].ToString();
//                num2.text = "";
//                num3.text = "";
//                num4.text = "";
//                num5.text = "";
//                num6.text = "";
//            }
//            else if (verificationCode.text.Length==2)
//            {
//                num2.text = chars[1].ToString();
//                num3.text = "";
//                num4.text = "";
//                num5.text = "";
//                num6.text = "";
//            }
//            else if (verificationCode.text.Length==3)
//            {
//                num3.text = chars[2].ToString();
//                num4.text = "";
//                num5.text = "";
//                num6.text = "";
//            }
//            else if(verificationCode.text.Length == 4)
//            {
//                num4.text = chars[3].ToString();
//                num5.text = "";
//                num6.text = "";
//            }
//            else if(verificationCode.text.Length == 5)
//            {
//                num5.text = chars[4].ToString();
//                num6.text = "";
//            }
//            else
//            {
//                num6.text = chars[5].ToString();
//            }
//        });
//        submit.onClick.AddListener(()=>
//        {
//            NetMngr.GetSingleton().Send(InterfaceLogin.Register, new object[] { phoneNumber, password, verificationCode.text });
//        });
//        getCode.onClick.AddListener(() =>
//        {
//            StaticData.getVerificationCode = 1;
//            LoginManager.GetSingleton().surePhonePopup.ShowView();
//            LoginManager.GetSingleton().surePhonePopup.SetMessage(phoneNumber);
//        });
//        back.onClick.AddListener(() =>
//        {
//            LoginManager.GetSingleton().planeManager.RemoveTopPlane();
//        });
//    }

//    void Start ()
//    {
	    
//	}
	

//	void Update ()
//    {
//        if (isStart)
//        {
//            timer -= Time.deltaTime;
//            regetcode.text = "重新获取("+(int)timer+")";
//            if (timer<0)
//            {
//                ReSetVerificationCode();
//            }
//        }
//	}

//    public void SendData(Hashtable data)
//    {
//        phoneNumber = data["phoneNumber"].ToString();
//        password = data["password"].ToString();
//    }

//    public void OnGetVerificationCode()
//    {
//        //发送验证码
//        NetMngr.GetSingleton().Send(InterfaceLogin.GetVerificationCode, new object[] { phoneNumber, StaticData.getVerificationCode.ToString() });
//        getCode.gameObject.SetActive(false);
//        regetcode.gameObject.SetActive(true);
//        isStart = true;
//    }
//    public void ReSetVerificationCode()
//    {
//        isStart = false;
//        regetcode.text = "";
//        timer = 61;
//        regetcode.gameObject.SetActive(false);
//        getCode.gameObject.SetActive(true);
//    }

//    public override void OnAddComplete()
//    {

//    }

//    public override void OnAddStart()
//    {
//        message.text = "我们已给手机号<color=#ffbf59>" + phoneNumber + "</color>发送了验证码";
//        //StaticData.getVerificationCode = 1;
//        //OnGetVerificationCode();
//    }

//    public override void OnRemoveComplete()
//    {
//        ReSetVerificationCode();
//    }

//    public override void OnRemoveStart()
//    {

//    }
//}
