using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DissClubPopup : BasePopup
{
    public Button setClubInfoBtn;
    public Button dissClubBtn;
    public Button quitClubBtn;
    public Button cancelBtn;

    void Awake()
    {
        Init();

        setClubInfoBtn = transform.Find("SetClubInfoBtn").GetComponent<Button>();
        dissClubBtn = transform.Find("DissClubBtn").GetComponent<Button>();
        quitClubBtn = transform.Find("QuitClubBtn").GetComponent<Button>();
        cancelBtn = transform.Find("CancelBtn/Button").GetComponent<Button>();

        setClubInfoBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().clubEditTopPanel.gameObject.SetActive(true);
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubEditTopPanel);
            HideView();
        });

        quitClubBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().commonPopup.ShowView("确定退出俱乐部？", null, true, () => {

                NetMngr.GetSingleton().Send(InterfaceClub.QuitClub, new object[] { ClubManager.GetSingleton().clubPanel.clubId });

            });


        });


        dissClubBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().commonPopup.ShowView("确定解散俱乐部？",null,true,()=> {

                NetMngr.GetSingleton().Send(InterfaceClub.DissClub,new object[] {ClubManager.GetSingleton().clubPanel.clubId });
                
            });


        });

        cancelBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
        });

        gameObject.SetActive(false);

    }

    public override void ShowView(Action onComplete = null)
    {
        base.ShowView(onComplete);
        if (ClubManager.GetSingleton().clubInfoTopPanel.isMine == 1)
        {
            dissClubBtn.gameObject.SetActive(true);
            setClubInfoBtn.gameObject.SetActive(true);
            quitClubBtn.gameObject.SetActive(false);
        }
        else if (ClubManager.GetSingleton().clubInfoTopPanel.isMine == 0)
        {
            dissClubBtn.gameObject.SetActive(false);
            setClubInfoBtn.gameObject.SetActive(false);
            quitClubBtn.gameObject.SetActive(true);
        }
        else if (ClubManager.GetSingleton().clubInfoTopPanel.isMine == 2) {
            dissClubBtn.gameObject.SetActive(false);
            setClubInfoBtn.gameObject.SetActive(true);
            quitClubBtn.gameObject.SetActive(true);
        }
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
