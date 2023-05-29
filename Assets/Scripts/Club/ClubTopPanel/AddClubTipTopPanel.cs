using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AddClubTipTopPanel : BasePlane
{
    public Button backBtn;
    public Button saveBtn;
    public InputField clubTagInput;

    public Text title;

    public Text tagLength;

    public enum TagType {
        editTag,
        createTag,
        clubNameEdit,
        kefuEdit,
        jianjieEdit

    }
    public TagType curTagType;

    void Awake()
    {
        title = transform.Find("ToggleGroup/Title").GetComponent<Text>();

        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();

        saveBtn = transform.Find("ToggleGroup/saveBtn").GetComponent<Button>();

        clubTagInput = transform.Find("clubTag").GetComponent<InputField>();

        tagLength = transform.Find("clubTag/count").GetComponent<Text>();

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        saveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (clubTagInput.text.Length <= fontLimit)
            {
                if (curTagType == TagType.editTag)
                {
                    ClubManager.GetSingleton().clubEditTopPanel.AddTag(clubTagInput.text);
                }
                else if (curTagType == TagType.createTag)
                {
                    ClubManager.GetSingleton().clubCreateTopPanel.AddTag(clubTagInput.text);
                }
                else if (curTagType == TagType.clubNameEdit)
                {
                    ClubManager.GetSingleton().clubEditTopPanel.clubNameInput.text = clubTagInput.text;
                }
                else if (curTagType == TagType.kefuEdit)
                {
                    ClubManager.GetSingleton().clubEditTopPanel.kefuWXInput.text = clubTagInput.text;
                }
                else if (curTagType == TagType.jianjieEdit)
                {
                    ClubManager.GetSingleton().clubEditTopPanel.jianJieInput.text = clubTagInput.text;
                }
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
        tagLength.text = clubTagInput.text.Length + " / "+fontLimit;
        SetNowType();

        if (clubTagInput.text.Length > fontLimit)
        {
            clubTagInput.text = clubTagInput.text.Substring(0, fontLimit);
        }
     
    }

    public int fontLimit = 12;

    public void SetNowType() {
        switch (curTagType) {
            case TagType.editTag:
                title.text = "俱乐部标签";
                fontLimit = 5;
                clubTagInput.placeholder.GetComponent<Text>().text = "请输入俱乐部标签";
                break;
            case TagType.createTag:
                title.text = "俱乐部标签";
                fontLimit = 5;
                clubTagInput.placeholder.GetComponent<Text>().text = "请输入俱乐部标签";
                break;
            case TagType.clubNameEdit:
                title.text = "俱乐部名字";
                fontLimit = 12;
                clubTagInput.placeholder.GetComponent<Text>().text = "请输入俱乐部名字";
                break;
            case TagType.kefuEdit:
                title.text = "俱乐部客服";
                fontLimit = 24;
                clubTagInput.placeholder.GetComponent<Text>().text = "请输入俱乐部客服";
                break;
            case TagType.jianjieEdit:
                title.text = "俱乐部简介";
                fontLimit = 56;
                clubTagInput.placeholder.GetComponent<Text>().text = "请输入俱乐部简介";
                break;

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
        clubTagInput.text = "";
    }

    public override void OnRemoveStart()
    {

    }
}
