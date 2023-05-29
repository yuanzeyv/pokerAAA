using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPayPopup : BasePopup {

    private Button WeChat;
    private Button ZhiFuBao;
    private Button Back;

    public string id;

    private void Awake()
    {
        Init();
        WeChat = transform.Find("WeChat").GetComponent<Button>();
        ZhiFuBao = transform.Find("ZhiFuBao").GetComponent<Button>();
        Back = transform.Find("CancelBtn").GetComponent<Button>();

        WeChat.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //NetMngr.GetSingleton().Send(InterfaceMain.BuyDiamond, new object[] { "2",id });
            Application.OpenURL(NetMngr.PayUrl + string.Format("/?a={0}&b={1}&c={2}", NetMngr.playerId, id, "0"));
            HideView();
        });
        ZhiFuBao.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //NetMngr.GetSingleton().Send(InterfaceMain.BuyDiamond, new object[] { "1", id });
            Application.OpenURL(NetMngr.PayUrl + string.Format("/?a={0}&b={1}&c={2}", NetMngr.playerId, id, "1"));
            HideView();
        });
        Back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HideView();
        });
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
    public void ShowView(string id)
    {
        this.id = id;
        base.ShowView();
    }
}
