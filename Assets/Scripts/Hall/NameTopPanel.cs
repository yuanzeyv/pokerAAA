using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameTopPanel : BasePlane {

    private Button backBtn;
    private Button saveBtn;
    private InputField nameInputField;
    private Text textCount;
    private Text costDiamond;
    private Text myDiamond;
    private Text tipText;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        saveBtn = transform.Find("Top/Save").GetComponent<Button>();
        nameInputField = transform.Find("InputField").GetComponent<InputField>();
        textCount = transform.Find("Count").GetComponent<Text>();
        costDiamond = transform.Find("Cost/value").GetComponent<Text>();
        myDiamond = transform.Find("Residue/value").GetComponent<Text>();
        tipText = transform.Find("TipText").GetComponent<Text>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        saveBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.ModifierName,new object[] {nameInputField.text });
        });
        nameInputField.onValueChanged.AddListener((value) =>
        {
            textCount.text = nameInputField.text.Length + "/6";
        });
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    public void SetData(string cost,string my)
    {
        costDiamond.text = cost;
        myDiamond.text = my;
        tipText.text = "提示 : 首次更改昵称免费, 之后每次修改需要" + cost + "钻石。";
        if (cost=="0")
        {
            tipText.gameObject.SetActive(false);
            myDiamond.gameObject.SetActive(false);
            costDiamond.gameObject.SetActive(false);
        }
        else
        {
            tipText.gameObject.SetActive(true);
            myDiamond.gameObject.SetActive(true);
            costDiamond.gameObject.SetActive(true);
        }
    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetUpdatePar, new object[] { "3" });
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
