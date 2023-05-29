using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SendCoinTopPanel : BasePlane
{
    public Button backBtn;
    public Button saveBtn;

    public Text content;
    public Text title;

    public Toggle sendToggle;//赠送积分
    public Toggle sendToggle2;//赠送钻石

    public int type=1; //1-积分界面 2-钻石界面

    public string s1;
    public string s2;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        saveBtn = transform.Find("ToggleGroup/saveBtn").GetComponent<Button>();

        title = transform.Find("ToggleGroup/Title").GetComponent<Text>();

        content = transform.Find("content").GetComponent<Text>();

        sendToggle = transform.Find("sendSetting/Toggle").GetComponent<Toggle>();
        sendToggle2 = transform.Find("sendSetting2/Toggle").GetComponent<Toggle>();

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        saveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (type == 1)
            {
                if (sendToggle.isOn)
                {
                    NetMngr.GetSingleton().Send(InterfaceClub.SetSendCoin, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "1" });

                }
                else
                {
                    NetMngr.GetSingleton().Send(InterfaceClub.SetSendCoin, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "0" });
                }
            }
            else {
                if (sendToggle2.isOn)
                {
                    NetMngr.GetSingleton().Send(InterfaceClub.SetSendDiamond, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "1" });

                }
                else
                {
                    NetMngr.GetSingleton().Send(InterfaceClub.SetSendDiamond, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "0" });
                }
            }


        });

        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public void ChangeType(int index) {
        type = index;
        if (type == 1)
        {
            title.text = "赠送积分";
            sendToggle.transform.parent.gameObject.SetActive(true);
            sendToggle2.transform.parent.gameObject.SetActive(false);
            content.text = s1;
        }
        else {
            title.text = "赠送钻石";
            sendToggle.transform.parent.gameObject.SetActive(false);
            sendToggle2.transform.parent.gameObject.SetActive(true);
            //content.gameObject.SetActive(false);
            content.text = s2;
        }
    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetCoinContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    public override void OnRemoveStart()
    {

    }

    public void GetCoinContentCallBack(Hashtable data) {
       s1= "当前赠送" + data["coinCount"].ToString()+ "积分。此积分为平台提供";

        if (data["canSend"].ToString() == "1")
        {
            ClubManager.GetSingleton().clubInfoTopPanel.canSendCoin = true;
        }
        else {
            ClubManager.GetSingleton().clubInfoTopPanel.canSendCoin = false;
        }

        if (data["isSend"].ToString() == "1")
        {
           sendToggle.isOn = true;
        }
        else
        {
           sendToggle.isOn= false;
        }

    }

    public void GetDiamondContentCallBack(Hashtable data)
    {
        s2= "当前赠送" + data["diamondCount"].ToString() + "钻石。此积分为平台提供";

        if (data["canSend"].ToString() == "1")
        {
            ClubManager.GetSingleton().clubInfoTopPanel.canSendDiamond = true;
        }
        else
        {
            ClubManager.GetSingleton().clubInfoTopPanel.canSendDiamond= false;
        }

        if (data["isSend"].ToString() == "1")
        {
            sendToggle2.isOn = true;
        }
        else
        {
            sendToggle2.isOn = false;
        }


    }
}
