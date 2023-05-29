using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemberItem2 : MonoBehaviour {

  
    public RawImage head;
    public GameObject isHost;
    public GameObject isManager;
    public Text memberName;

    public string id;
    void Awake()
    {
        head = transform.Find("headMask/head").GetComponent<RawImage>();
        isHost = transform.Find("IsHost").gameObject;
        isManager = transform.Find("IsManager").gameObject;
        memberName = transform.Find("memberName").GetComponent<Text>();
       
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //成员查找成员时候的点击事件
    public void MemberFindClick() {
        ClubManager.GetSingleton().clubMemberInfoTopPanel.isMine = false;
        ClubManager.GetSingleton().clubMemberInfoTopPanel.id = id;
        ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMemberInfoTopPanel);
    }
    //设置管理员时候的点击事件
    public void SetManager() {
        ClubManager.GetSingleton().addClubManagerTipPopup.ShowView();
        ClubManager.GetSingleton().addClubManagerTipPopup.id = id;
    }
    /*
    public void GetSprtie(Sprite s)
    {
        if (s != null)
        {
            head.sprite = s;
        }
    }*/
    public void GetSprtie(Texture s)
    {
        if (s != null)
        {
            head.texture = s;
        }
    }


    public string SetName(string n)
    {
        string str = "";
        if (n.Length > 8)
        {
            str = n.Substring(0, 6) + "..";

        }
        else
        {
            str = n;
        }
        return str;
    }


    public void SetInfo( string url, string id, string name, string isHost)
    {

       
        GameTools.GetSingleton().GetTextureNet(url, GetSprtie);
        this.id = id;
        memberName.text = SetName(name);


        if (isHost == "1")
        {
            this.isHost.SetActive(true);
            this.isManager.SetActive(false);
        }
        else if (isHost == "2")
        {
            this.isHost.SetActive(false);
            this.isManager.SetActive(true);
        }
        else
        {
            this.isHost.SetActive(false);
            this.isManager.SetActive(false);
        }

        if (ClubManager.GetSingleton().clubMember2TopPanel.panelType == 1)
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                MemberFindClick();
                //SetManager();
            });

        }
        else {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                //MemberFindClick();
                SetManager();
            });


        }

    }

    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
