using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputPhonePanel : BasePlane
{

    private Button back;
    private InputField phoneNumber;
    private Button next;
    private Image tip;
    private Text text;


    private void Awake()
    {
        back = transform.Find("Back").GetComponent<Button>();
        phoneNumber = transform.Find("phoneNumber/InputField").GetComponent<InputField>();
        next = transform.Find("NextBtn").GetComponent<Button>();
        tip = transform.Find("sendTip").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        phoneNumber.onValueChanged.AddListener((value)=> {
            next.interactable = !string.IsNullOrEmpty(phoneNumber.text);
        });

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            LoginManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        next.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (phoneNumber.text.Length != 11)
            {
                text.text = "手机号输入有误";
                return;
            }
            else
            {
                text.text = "";
                Hashtable data = new Hashtable();
                data.Add("phoneNumber", phoneNumber.text);
                phoneNumber.text = "";
                LoginManager.GetSingleton().setPasswordPanel.SendData(data);
                StaticData.getVerificationCode = 2;
                LoginManager.GetSingleton().setPasswordPanel.OnGetVerificationCode();
            }
        });
    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
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
