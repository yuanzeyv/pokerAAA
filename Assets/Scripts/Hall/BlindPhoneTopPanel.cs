using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlindPhoneTopPanel : BasePlane {

    private Button back;
    private InputField phoneNumber;
    private InputField verificationCode;
    private Button getCode;
    private Button blindBtn;
    private Text regetcode;

    private bool isStart = false;
    private float timer = 61;

    private void Awake()
    {
        back = transform.Find("Top/Back").GetComponent<Button>();
        phoneNumber = transform.Find("PhoneNumberInput").GetComponent<InputField>();
        verificationCode = transform.Find("VerificationCodeInput").GetComponent<InputField>();
        getCode = transform.Find("GetVerCode").GetComponent<Button>();
        blindBtn = transform.Find("BlindPhoneBtn").GetComponent<Button>();
        regetcode = transform.Find("Text").GetComponent<Text>();
        phoneNumber.onValueChanged.AddListener((value) =>
        {
            blindBtn.interactable = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(verificationCode.text);
        });
        verificationCode.onValueChanged.AddListener((value) =>
        {
            blindBtn.interactable = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(phoneNumber.text);
        });
        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        getCode.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (string.IsNullOrEmpty(phoneNumber.text))
            {
                return;
            }
            OnGetVerificationCode();
        });
        blindBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.BlindPhone, new object[] {phoneNumber.text,verificationCode.text });
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

    //获取验证码调用的方法
    public void OnGetVerificationCode()
    {
        StaticData.getVerificationCode = 3;
        NetMngr.GetSingleton().Send(InterfaceLogin.GetVerificationCode, new object[] { phoneNumber.text, StaticData.getVerificationCode.ToString() });
        getCode.gameObject.SetActive(false);
        regetcode.gameObject.SetActive(true);
        isStart = true;
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

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        ReSetVerificationCode();
        phoneNumber.text = "";
        verificationCode.text = "";
    }

    public override void OnRemoveStart()
    {

    }
}
