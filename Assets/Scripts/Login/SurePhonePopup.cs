//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class SurePhonePopup : BasePopup
//{
//    private Text message;
//    private Button cancel;
//    private Button sure;

//    private void Awake()
//    {
//        Init();
//        SetAnimType(AnimType.Alpha);
//        SetDefaultAnimType(AnimType.Alpha);
//        message = transform.Find("message").GetComponent<Text>();
//        cancel = transform.Find("cancelBtn").GetComponent<Button>();
//        sure = transform.Find("sureBtn").GetComponent<Button>();

//        cancel.onClick.AddListener(() =>
//        {
//            LoginManager.GetSingleton().surePhonePopup.HideView();
//        });
//        sure.onClick.AddListener(() =>
//        {
//            if (StaticData.getVerificationCode==0)
//            {
//                LoginManager.GetSingleton().verificationLoginPanel.OnGetVerificationCode();
//                LoginManager.GetSingleton().surePhonePopup.HideView();
//            }
//            else if (StaticData.getVerificationCode == 1)
//            {
//                //LoginManager.GetSingleton().inputYanZhengPanel.OnGetVerificationCode();
//                LoginManager.GetSingleton().surePhonePopup.HideView();
//            }
//            else if (StaticData.getVerificationCode == 2)
//            {
//                LoginManager.GetSingleton().setPasswordPanel.OnGetVerificationCode();
//                LoginManager.GetSingleton().surePhonePopup.HideView();
//            }
//        });
//    }

//    void Start ()
//    {
	
//	}
	
//	void Update ()
//    {
	
//	}

//    public void SetMessage(string str)
//    {
//        message.text = "将发送验证码短信到这个号码"+str;
//    }
//}
