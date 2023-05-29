using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class erweimaTopPanel : BasePlane
{
    public Button backBtn;
    public Button shareBtn;

	public RawImage clubHead;
    public Text clubName;
    public Text clubAddress;
    public RawImage erweimaImage;


    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        shareBtn = transform.Find("ToggleGroup/ShareBtn").GetComponent<Button>();

		clubHead = transform.Find("ClubHead/head").GetComponent<RawImage>();
        clubName = transform.Find("ClubName").GetComponent<Text>();
        clubAddress = transform.Find("ClubAddress").GetComponent<Text>();
        erweimaImage = transform.Find("erweimaImage").GetComponent<RawImage>();

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        shareBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().erweimaPopup.ShowView();
        });

        gameObject.SetActive(false);
    }

    public void GetSprtie(Texture s)
    {
        erweimaImage.texture = s;
    }


    public void GetErWeiMaCallBack(Hashtable data) {
        GameTools.GetSingleton().GetTextureNet(data["ImgUrl"].ToString(), GetSprtie);

    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetErWeiMa, new object[] {});
        clubHead.texture = ClubManager.GetSingleton().clubInfoTopPanel.clubHead.texture;
        clubName.text = ClubManager.GetSingleton().clubInfoTopPanel.clubName.text;
       
    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
