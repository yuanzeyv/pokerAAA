using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClubItem : MonoBehaviour {
    public RawImage clubHead;
    public Text clubName;
	public Text txtclubId;
    public Text memberCount;
    public Text deskCount;
    public GameObject clubTag;
    public GameObject clubTag2;
    public GameObject clubTag3;

    public string clubId;

    public int isHost; //0-成员 1-群主 2-管理员

    void Awake()
    {
        clubHead = transform.Find("mask/ClubHead").GetComponent<RawImage>();
       
       
        clubName = transform.Find("ClubName").GetComponent<Text>();
		txtclubId = transform.Find("ClubId").GetComponent<Text>();
        memberCount = transform.Find("ClubMemberCount").GetComponent<Text>();
        deskCount = transform.Find("ClubDeskCount").GetComponent<Text>();
        clubTag = transform.Find("ClubTag").gameObject;
        clubTag2 = transform.Find("ClubTag2").gameObject;
        clubTag3 = transform.Find("ClubTag3").gameObject;

        GetComponent<Button>().onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            // NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo,new object[] { clubId});
            ClubManager.GetSingleton().clubPanel.clubId = clubId;
            StaticData.clubId = clubId;
           
            ClubManager.GetSingleton().clubPanel.clubName.text = clubName.text;

            ClubManager.GetSingleton().clubPanel.GongGaoTog.isOn = false;
            ClubManager.GetSingleton().clubPanel.MatchTog.isOn = true;
            ClubManager.GetSingleton().clubPanel.RecordTog.isOn = false;
            ClubManager.GetSingleton().clubPanel.ClubInfTog.isOn = false;
            ClubManager.GetSingleton().clubPanel.toggleIndex = 1;
            //NetMngr.GetSingleton().Send(InterfaceClub.GetGongGao, new object[] { clubId });
            NetMngr.GetSingleton().Send(InterfaceClub.GetClubMatch, new object[] { clubId, 1, 100 });
            
            StaticData.isHost = isHost;
            if (isHost == 1||isHost==2)
            {
                ClubManager.GetSingleton().clubPanel.createGongGaoBtn.gameObject.SetActive(true);
                ClubManager.GetSingleton().clubPanel.createMatchBtn.gameObject.SetActive(true);
                
            }
            else {
                ClubManager.GetSingleton().clubPanel.createGongGaoBtn.gameObject.SetActive(false);
                ClubManager.GetSingleton().clubPanel.createMatchBtn.gameObject.SetActive(false);
                
            }
            ClubManager.GetSingleton().clubPanel.gameObject.SetActive(true);
        });
    }
    /*
    public void GetSprtie(Sprite s)
    {
		if(clubHead != null)
        	clubHead.sprite = s;
    }*/
    public void GetSprtie(Texture s)
    {
        if (clubHead != null)
            clubHead.texture = s;
    }

    public void SetInfo(string url,string name,string member,string desk,string tag,string clubID,string isHost ) {
        GameTools.GetSingleton().GetTextureNet(url, GetSprtie);
        clubName.text = name;
        memberCount.text = member;
        deskCount.text = desk;
		txtclubId.text = "ID:" + clubID;
        clubId = clubID;
        string[] tags = tag.Split('|');
        if (tags.Length < 3)
        {
            clubTag.gameObject.SetActive(false);
            clubTag2.gameObject.SetActive(false);
            clubTag3.gameObject.SetActive(false);
            if (tags.Length == 1)
            {
                clubTag.gameObject.SetActive(true);
                clubTag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
            }
            if (tags.Length == 2)
            {
                clubTag.gameObject.SetActive(true);
                clubTag2.gameObject.SetActive(true);
                clubTag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
                clubTag2.transform.GetChild(0).GetComponent<Text>().text = tags[1];
            }
        }
        else 
        {
            clubTag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
            clubTag2.transform.GetChild(0).GetComponent<Text>().text = tags[1];
            clubTag3.transform.GetChild(0).GetComponent<Text>().text = tags[2];
            clubTag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
            clubTag2.transform.GetChild(0).GetComponent<Text>().text = tags[1];
            clubTag3.transform.GetChild(0).GetComponent<Text>().text = tags[2];
        }
        this.isHost = int.Parse(isHost);
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
