using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HotClubItem : MonoBehaviour {
    public RawImage clubHead;
    public Text clubName;
    public Text memberCount;
    public Button joinBtn;
	public Text joinBtnText;

    public GameObject clubTag;
    public GameObject clubTag2;
    public GameObject clubTag3;

    public string clubId;

    void Awake()
    {
        clubHead = transform.Find("ClubHead/head").GetComponent<RawImage>();


        clubName = transform.Find("ClubName").GetComponent<Text>();
        memberCount = transform.Find("ClubMemberCount").GetComponent<Text>();
        joinBtn = transform.Find("JoinBtn").GetComponent<Button>();
		joinBtnText = transform.Find("JoinBtn/Text").GetComponent<Text>();
        clubTag = transform.Find("ClubTag").gameObject;
        clubTag2 = transform.Find("ClubTag2").gameObject;
        clubTag3 = transform.Find("ClubTag3").gameObject;
        GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            // NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo,new object[] { clubId});
            //ClubManager.GetSingleton().clubPanel.clubId = clubId;
            //ClubManager.GetSingleton().clubPanel.gameObject.SetActive(true);
            //ClubManager.GetSingleton().clubPanel.clubName.text = clubName.text;
        });

        joinBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceClub.JoinClub, new object[] { clubId});
        });

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    public void GetSprtie(Sprite s)
    {
        clubHead.sprite = s;
    }*/
    public void GetSprtie(Texture s)
    {
        clubHead.texture = s;
    }
    public void SetInfo(string url, string name, string member, string isJoin, string tag, string clubID)
    {
        GameTools.GetSingleton().GetTextureNet(url, GetSprtie);
        clubName.text = name;
        memberCount.text = member;

        if (isJoin == "1")
        {
            joinBtn.interactable = false;
			joinBtnText.text = "已加入";
        }
        else {
            joinBtn.interactable = true;
			joinBtnText.text = "加入";
        }


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

    }
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
