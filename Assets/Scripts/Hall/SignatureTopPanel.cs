using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SignatureTopPanel : BasePlane {

    private Button backBtn;
    private Button saveBtn;
    private InputField signatureInputField;
    private Text textCount;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        saveBtn = transform.Find("Top/Save").GetComponent<Button>();
        signatureInputField = transform.Find("InputField").GetComponent<InputField>();
        textCount = transform.Find("Count").GetComponent<Text>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        saveBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.ModifierSignature, new object[] { signatureInputField.text });
        });
        signatureInputField.onValueChanged.AddListener((value) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            textCount.text = signatureInputField.text.Length+"/56";
            
        });
    }
    public void ModifierSignatureFinished()
    {
        signatureInputField.text = "";
        HallManager.GetSingleton().planeManager.RemoveTopPlane();
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
