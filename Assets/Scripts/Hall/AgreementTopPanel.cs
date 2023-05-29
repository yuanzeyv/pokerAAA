using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AgreementTopPanel : BasePlane {


    private Button back;
    private Button send;
    private Text count;
    private InputField inputfield;

    private void Awake()
    {
        back = transform.Find("Top/Back").GetComponent<Button>();
        send = transform.Find("Send").GetComponent<Button>();
        count = transform.Find("Count").GetComponent<Text>();
        inputfield = transform.Find("InputField").GetComponent<InputField>();

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
            inputfield.text = "";
        });
        send.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.SendAgreement, new object[] {inputfield.text });
        });
        inputfield.onValueChanged.AddListener((value) =>
        {
            count.text = inputfield.text.Length+"/50";
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
