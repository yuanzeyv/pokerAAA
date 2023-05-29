using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateGongGaoTopPanel : BasePlane
{
    public Button backBtn;
    public Button saveBtn;

    public InputField titleInput;
    public Text titleLength;

    public InputField contentInput;
    public Text contentLength;


 

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        saveBtn = transform.Find("saveBtn").GetComponent<Button>();

        titleInput = transform.Find("titleContent").GetComponent<InputField>();
        titleLength = transform.Find("titleContent/count").GetComponent<Text>();

        contentInput = transform.Find("detailContent").GetComponent<InputField>();
        contentLength = transform.Find("detailContent/count").GetComponent<Text>();

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });


        saveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (titleInput.text.Length <= 24 && contentInput.text.Length <= 100)
            {
                NetMngr.GetSingleton().Send(InterfaceClub.SetGongGao, new object[] { ClubManager.GetSingleton().clubPanel.clubId, titleInput.text, contentInput.text });


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
        titleLength.text = titleInput.text.Length + " / 24";
        contentLength.text=contentInput.text.Length + " / 100";

        if (titleInput.text.Length > 24) {
            titleInput.text = titleInput.text.Substring(0, 24);
        }
        if (contentInput.text.Length > 100)
        {
            contentInput.text = contentInput.text.Substring(0, 100);
        }
    }

    public void DelText() {
        titleInput.text = "";
        contentInput.text = "";

    } 


    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        DelText();
    }

    public override void OnRemoveStart()
    {

    }
}
