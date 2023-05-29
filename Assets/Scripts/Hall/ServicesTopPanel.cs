using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServicesTopPanel : BasePlane {

    private Button backBtn;
    private Button copyChat;
    private Button copyQQ;
    private Text weChatText;
    private Text qqText;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        copyChat = transform.Find("weChatServices/copy").GetComponent<Button>();
        copyQQ = transform.Find("qqServices/copy").GetComponent<Button>();
        weChatText = transform.Find("weChatServices/value").GetComponent<Text>();
        qqText = transform.Find("qqServices/value").GetComponent<Text>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        copyChat.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UniClipboard.SetText(weChatText.text);
            PopupCommon.GetSingleton().ShowView("复制成功!");
        });
        copyQQ.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UniClipboard.SetText(qqText.text);
            PopupCommon.GetSingleton().ShowView("复制成功!");
        });
    }
    public void SetServicesFinished(Hashtable data)
    {
        weChatText.text = data["wechat"].ToString();
        qqText.text = data["qq"].ToString();
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetServices, new object[] { });
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
