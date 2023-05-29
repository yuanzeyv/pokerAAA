using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FriendCode : BasePopup
{
    public Button cancelBtn;

    public InputField inputField;

    void Awake()
    {
        Init();

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        inputField = transform.Find("InputCount").GetComponent<InputField>();


        cancelBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            Debug.Log("----------------" + inputField);
            if (!string.IsNullOrEmpty(inputField.text))
            {
              //  Debug.Log(strCode);
                NetMngr.GetSingleton().Send(InterfaceClub.GetBindingCode, new object[] { inputField.text });

                inputField.text = "";
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
