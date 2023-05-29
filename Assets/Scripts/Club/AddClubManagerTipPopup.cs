using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AddClubManagerTipPopup : BasePopup
{
    public Button cancelBtn;
    public Button sureBtn;

    public string id;
    void Awake()
    {
        Init();

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        sureBtn = transform.Find("SureBtn").GetComponent<Button>();

        cancelBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
        });

        sureBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");   
            NetMngr.GetSingleton().Send(InterfaceClub.SetManager, new object[] { ClubManager.GetSingleton().clubPanel.clubId, id ,"1"});
            HideView();
        });

        gameObject.SetActive(false);
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
