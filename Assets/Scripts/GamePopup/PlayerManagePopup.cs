using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManagePopup : BasePopup {

    private Button ClostBtn;
    private Button back;
    private Button sure;
    private Toggle noSit;
    private Toggle noEnter;

    private string id;

    private void Awake()
    {
        Init();
//        ClostBtn = transform.Find("CloseBtn").GetComponent<Button>();
        back = transform.Find("backBtn").GetComponent<Button>();
        sure = transform.Find("sureBtn").GetComponent<Button>();
        noSit = transform.Find("NoSit/Toggle").GetComponent<Toggle>();
        noEnter = transform.Find("NoJoinRoom/Toggle").GetComponent<Toggle>();

//        ClostBtn.onClick.AddListener(() =>
//        {
//            HideView();
//        });
        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HideView();
        });
        sure.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
            NetMngr.GetSingleton().Send(InterfaceGame.managerUser, new object[] { int.Parse(id), noSit.isOn ? 1 : 0, noEnter.isOn ? 1 : 0 });
            GameUIManager.GetSingleton().playerManageListPopup.ShowView();
        });
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ShowView(bool nodSit, bool nodEnter, string id)
    {
        base.ShowView();
        this.id = id;
        noSit.isOn = nodSit;
        noEnter.isOn = nodEnter;
    }
}
