using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WelcomeMemberTopPanel : BasePlane
{
    public Button backBtn;
    public Button saveBtn;

    public InputField welcomeInput;
    public Text tagLength;

    public Toggle welcomeToggle;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        saveBtn = transform.Find("ToggleGroup/saveBtn").GetComponent<Button>();

        welcomeInput = transform.Find("welcomeContent").GetComponent<InputField>();
        tagLength = transform.Find("welcomeContent/count").GetComponent<Text>();

        welcomeToggle = transform.Find("welcomeSetting/Toggle").GetComponent<Toggle>();
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });


        saveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (welcomeInput.text.Length <= 50)
            {
                string isAuto;
                if (welcomeToggle.isOn)
                {
                    isAuto = "1";
                }
                else
                {
                    isAuto = "0";
                }
                NetMngr.GetSingleton().Send(InterfaceClub.SetWelcomeWord, new object[] { ClubManager.GetSingleton().clubPanel.clubId, welcomeInput.text, isAuto });


                ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            }
            else
            {
                ClubManager.GetSingleton().commonPopup.ShowView("超出字数限制");
            }
        });

        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        tagLength.text = welcomeInput.text.Length + " / 50";
    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetWelcomeWord, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    public override void OnRemoveComplete()
    {
        //welcomeInput.text = "";
    }

    public override void OnRemoveStart()
    {

    }

    public void GetWelcomeCallBack(Hashtable data) {
        if(data["welcome"] != null)
        {
            welcomeInput.text = data["welcome"].ToString();
        }
       
    }
}
