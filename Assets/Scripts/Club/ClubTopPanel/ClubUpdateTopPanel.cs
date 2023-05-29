using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClubUpdateTopPanel : BasePlane
{
    public Button backBtn;

    public RawImage clubHead;

    public Text clubName;

    public Text curMember;

    public Text curManager;

    public Transform infoContent;

    public Button[] updateBtns=new Button[4];

	public int level;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();

        clubHead = transform.Find("ClubHead/mask/head").GetComponent<RawImage>();

        clubName = transform.Find("clubName").GetComponent<Text>();

        curMember = transform.Find("member/Text").GetComponent<Text>();

        curManager = transform.Find("manager/Text").GetComponent<Text>();

        infoContent = transform.Find("Info/Viewport/Content");

        for (int i = 0; i < updateBtns.Length; i++)
        {
            updateBtns[i] = infoContent.GetChild(i).GetComponentInChildren<Button>();
            int index = i;
            updateBtns[i].onClick.AddListener(() => {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                NetMngr.GetSingleton().Send(InterfaceClub.UpdateClub, new object[] { ClubManager.GetSingleton().clubPanel.clubId, (index + 2) + "" });
            });
        }
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetUpdateCost, new object[] { ClubManager.GetSingleton().clubPanel.clubId});
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

	public void UpdateClubCallBack(string msg)
	{
		if (msg == "1") {
			level = level + 1;
			freshBtn ();
		}
	}

	public void freshBtn()
	{
		for (int i = 0; i < 4; i++)
		{
			if (i + 2 > level) {
				updateBtns[i].interactable = true;
			} else {
				updateBtns[i].interactable = false;
			}

		}

	}

    public void GetSprtie(Texture s)
    {
        clubHead.texture = s;
    }

    public void GetUpdateCostCallBack(Hashtable data) {
        GameTools.GetSingleton().GetTextureNet(data["url"].ToString(), GetSprtie);
        clubName.text = data["name"].ToString();
        curMember.text = data["currentPerson"].ToString();
        curManager.text = data["currentManager"].ToString();

		level = int.Parse (data ["level"].ToString ());

		freshBtn ();

        ArrayList List = data["config"] as ArrayList;
      
        if (List == null)
        {
          
            return;
        }

        for (int i = 0; i < List.Count; i++)
        {
            Hashtable ht = List[i] as Hashtable;
            infoContent.GetChild(i).Find("updateBtn/costCoin").GetComponent<Text>().text = ht["cost"].ToString();
            if (i < 3) {
                infoContent.GetChild(i).Find("title").GetComponent<Text>().text = "升级至"+ht["maxPerson"].ToString()+"人";
                infoContent.GetChild(i).Find("updateContent").GetComponent<Text>().text = "提升俱乐部人数总量上限为" + ht["maxPerson"].ToString() + "人\n可设管理员"+ht["manager"].ToString()+"人";

            }


        }

    }

}