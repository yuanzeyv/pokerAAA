using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddFriendInfoTopPanel : BasePlane {

    private Button back;
    private Button send;
    private InputField sendInfo;
    private Text count;

    public string id;

    private void Awake()
    {
		back = transform.Find("Top/Back").GetComponent<Button>();
        send = transform.Find("Top/Send").GetComponent<Button>();
		sendInfo = transform.Find("Top/InputField").GetComponent<InputField>();
        count = transform.Find("Count").GetComponent<Text>();

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        send.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.AddFriend, new object[] {id,sendInfo.text });
        });
        sendInfo.onValueChanged.AddListener((value) =>
        {
            count.text = sendInfo.text.Length + "/20";
        });
    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public void AddFriendFinished(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        HallManager.GetSingleton().planeManager.RemoveTopPlane();
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
