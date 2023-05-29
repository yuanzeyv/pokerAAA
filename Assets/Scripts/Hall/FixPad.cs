using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FixPad : BasePopup
{
    public Button cancelBtn;

    public InputField inputField;
    public InputField inputField2;

    void Awake()
    {
        Init();

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        inputField = transform.Find("shurukuang/InputCount").GetComponent<InputField>();
        inputField2 = transform.Find("shurukuang2/InputCount").GetComponent<InputField>();


        cancelBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            Debug.Log("----------------" + inputField);
            if (!string.IsNullOrEmpty(inputField.text) && !string.IsNullOrEmpty(inputField2.text))
            {
              //  Debug.Log(strCode);
                NetMngr.GetSingleton().Send(InterfaceLogin.FixPassword, new object[] { inputField.text,inputField2.text });

                inputField.text = "";
                inputField2.text = "";
            }
            else
            {
                PopupCommon.GetSingleton().ShowView("输入信息不能为空");
            }

            HideView();
        });

        gameObject.SetActive(false);

    }

    public override void ShowView(Action onComplete = null)
    {
        base.ShowView(onComplete);
    
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

  
}
