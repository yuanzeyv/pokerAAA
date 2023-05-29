using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMemberCoinTopPanel : BasePlane {

    public Toggle sendToggle;
    public Toggle sendToggle2;

    public GameObject page1;
    public GameObject page2;

    private Text sendText;
    private Text aleardyText;
    private InputField inputName;
    private InputField inputCount;
    private Button sendBtn;
    private Button backBtn;

    private Transform parent;

    private void Awake()
    {
        page1 = transform.Find("Page1").gameObject;
        page2 = transform.Find("Page2").gameObject;

		sendText = page1.transform.Find("send/shuliang").GetComponent<Text>();
		aleardyText = page1.transform.Find("aleardy/shuliang").GetComponent<Text>();
        inputName = page1.transform.Find("InputName").GetComponent<InputField>();
        inputCount = page1.transform.Find("InputCount").GetComponent<InputField>();
        sendBtn = page1.transform.Find("sendTo").GetComponent<Button>();
		backBtn = transform.Find("Top/Back/Share").GetComponent<Button>();

        sendToggle = transform.Find("Top/ToggleGroup/Toggle1").GetComponent<Toggle>();
        sendToggle2 = transform.Find("Top/ToggleGroup/Toggle2").GetComponent<Toggle>();
        parent = page2.transform.Find("ScrollView/Viewport/Content");
        sendToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                this.page1.SetActive(true);
                this.page2.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceClub.GetPlayerDiamondInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1 });
            }
        });
        sendToggle2.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                this.page1.SetActive(false);
                this.page2.SetActive(true);
                getrecord();
            }
        });
        sendBtn.onClick.AddListener(SendTo);
        backBtn.onClick.AddListener(() =>
        {
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

		this.page1.SetActive(true);
		this.page2.SetActive(false);
    }

    void Start () {
		
	}

	void Update () {
		
	}

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetPlayerDiamondInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1 });
    }

    public override void OnAddStart()
    {
    }

    public override void OnRemoveComplete()
    {
        sendToggle.isOn = true;
        sendToggle2.isOn = false;
    }

    public override void OnRemoveStart()
    {
        
    }

    public void GetMemberCoinTopCallBack(Hashtable data)
    {
        this.sendText.text = data["canSendCoin"].ToString();
        this.aleardyText.text = data["aleardySendCoin"].ToString();
    }

    public void SendTo()
    {
        if (!string.IsNullOrEmpty(inputName.text)&&!string.IsNullOrEmpty(inputCount.text))
        {
            NetMngr.GetSingleton().Send(InterfaceClub.SendToPlayerDiamond, new object[] { ClubManager.GetSingleton().clubPanel.clubId, inputName.text, inputCount.text, 1});
            inputName.text = "";
            inputCount.text = "";
        }
        else
        {
            PopupCommon.GetSingleton().ShowView("输入信息不能为空");
        }
    }

    public void SendToCallBack(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            NetMngr.GetSingleton().Send(InterfaceClub.GetPlayerDiamondInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1});
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    public void getrecord()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetPlayerDiamondrecord, new object[] {ClubManager.GetSingleton().clubPanel.clubId, 1});
    }

    public void GetRedcordInfoCallback(Hashtable data)
    {
        ClearList();
        ArrayList withList = data["records"] as ArrayList;
        Object objItem = Resources.Load("HallItem/zengsongItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            zengsongItem zengsongItemd = go.AddComponent<zengsongItem>();
            zengsongItemd.SetData(withList[i] as Hashtable, 1);
        }
    }

    private void ClearList()
    {
        int childCount = parent.childCount;
        if (childCount!=0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}
