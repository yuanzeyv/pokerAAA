using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClubModifyLimitPanel : BasePlane
{
    public Button backBtn;
    public Button saveBtn;
    public InputField limitInput;

    public Text title;
    public string userId;
    
    private int limit;

    void Awake()
    {
        title = transform.Find("ToggleGroup/Title").GetComponent<Text>();

        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();

        saveBtn = transform.Find("ToggleGroup/saveBtn").GetComponent<Button>();

        limitInput = transform.Find("limit").GetComponent<InputField>();

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        saveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if(limitInput.text == ""){
                Tip.Instance.showMsg("请输入免授权额度");
                return;
            }
            limit = int.Parse(limitInput.text);
            NetMngr.GetSingleton().Send(InterfaceClub.ClubModifyLimit, new object[] { 
                limit, ClubManager.GetSingleton().clubPanel.clubId, userId });
        });


        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
     
    }

    public void Finished(){
        ClubManager.GetSingleton().clubMemberInfoTopPanel.RefreshLimit(limit);
        ClubManager.GetSingleton().planeManager.RemoveTopPlane();    
    }


    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        limitInput.text = "";
    }

    public override void OnRemoveComplete()
    {
    }

    public override void OnRemoveStart()
    {

    }
}
