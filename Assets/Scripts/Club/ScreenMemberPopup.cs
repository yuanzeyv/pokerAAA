using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenMemberPopup :BasePopup {

    public Button resetBtn;
    public Button sureBtn;
    //public Button screenBtn;

    public Toggle[] recentJoinToggle;
    public Toggle[] memberActiveToggle;

    void Awake()
    {
        Init();
        resetBtn = transform.Find("ResetBtn").GetComponent<Button>();
        sureBtn = transform.Find("SureBtn").GetComponent<Button>();

        recentJoinToggle = transform.Find("RecentJoin").GetComponentsInChildren<Toggle>();
        memberActiveToggle= transform.Find("MemberActive").GetComponentsInChildren<Toggle>();

        resetBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            recentJoinToggle[2].isOn = true;
            memberActiveToggle[5].isOn = true;

        });

        sureBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SendScreen();
            HideView();
        });

        gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SendScreen() {
        string recent="";
        string active="";


        for (int i = 0; i < recentJoinToggle.Length; i++) {
            if (recentJoinToggle[i].isOn) {
                recent = i+"";
                break;
            }

        }

        for (int i = 0; i < memberActiveToggle.Length; i++)
        {
            if (memberActiveToggle[i].isOn)
            {
               active = i + "";
                break;
            }

        }

        NetMngr.GetSingleton().Send(InterfaceClub.SelectFind,new object[] {ClubManager.GetSingleton().clubPanel.clubId,recent,active });
    }
}
