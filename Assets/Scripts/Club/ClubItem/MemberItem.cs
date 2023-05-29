using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemberItem : MonoBehaviour {

    //public Text no;
    public RawImage head;
    public GameObject isHost;
    public GameObject isManager;
    public Text memberName;
    public Text totalGame;
    public Text activeTime;

    public string id;
    void Awake()
    {
        head = transform.Find("headMask/head").GetComponent<RawImage>();
        isHost = transform.Find("IsHost").gameObject;
        isManager = transform.Find("IsManager").gameObject;
        memberName = transform.Find("memberName").GetComponent<Text>();
        //no= transform.Find("no").GetComponent<Text>();
        totalGame= transform.Find("totalGame").GetComponent<Text>();
        activeTime= transform.Find("activeTime").GetComponent<Text>();

        GetComponent<Button>().onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().clubMemberInfoTopPanel.isMine = true;
            ClubManager.GetSingleton().clubMemberInfoTopPanel.id = id;
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMemberInfoTopPanel);

        });

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*

     public void GetSprite(Sprite s)
     {
         if (s != null)
         {
             head.sprite = s;
         }
     }*/
    public void GetSprite(Texture s)
    {
        if (s != null)
        {
            head.texture = s;
        }
    }

    public void SetInfo(string no,  string url,string  id, string name, string isHost, string totalCount, string active)
    {

        //this.no.text = no;
        GameTools.GetSingleton().GetTextureNet(url, GetSprite);
        this.id= id;
        memberName.text =SetName(name);


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
        else {
            this.isHost.SetActive(false);
            this.isManager.SetActive(false);
        }
        totalGame.text = totalCount;
        activeTime.text = active;


    }

    public string SetName(string  n) {
        string str="";
        if (n.Length > 8)
        {
             str = n.Substring(0, 6) + "..";
           
        }
        else {
            str = n;
        }
        return str;
    }

    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
