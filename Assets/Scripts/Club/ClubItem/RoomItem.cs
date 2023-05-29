using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomItem : MonoBehaviour {

    public RawImage roomHead;
    public Text roomName;
    public Text roomName2;
    public Text clubName;
    public Text roomState;
    public Text blinds;
    public Text playerCount;
    public Text time;

//	public Text txtroomid;

	public Image imageBaoxian;
	public Image imageClub;

	private Image imagetype;

    public string roomId;

    void Awake()
    {
        roomHead = transform.Find("RoomHead/head").GetComponent<RawImage>();
		roomName = transform.Find("Panel/DeskName").GetComponent<Text>();
        roomName2 = transform.Find("RoomName2").GetComponent<Text>();
//        clubName = transform.Find("ClubName").GetComponent<Text>();
        roomState = transform.Find("RoomState").GetComponent<Text>();
        blinds = transform.Find("Blinds").GetComponent<Text>();
        playerCount = transform.Find("RoomPlayerCount").GetComponent<Text>();
        time = transform.Find("Time").GetComponent<Text>();

		imageBaoxian = transform.Find("Panel/baoxian").GetComponent<Image>();
		imageClub = transform.Find("Panel/julebu").GetComponent<Image>();

		imagetype = transform.Find("Panel/gametype").GetComponent<Image>();

        GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //加入牌局
            NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerEnterRequest, new object[] { roomId });
            //记录一下俱乐部id 用于 下次进入大厅 
            StaticData.clubId = ClubManager.GetSingleton().clubPanel.clubId;
            StaticData.clubName = ClubManager.GetSingleton().clubPanel.clubName.text;

        });

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetSprtie(Texture s)
    {
        if (s != null && roomHead != null)
            roomHead.texture = s;
    }

	public void SetInfo(string roomId,  string url, string deskName, string hostName, string blinds, string playerCount, string time,string state,string clubName, int insurance, int gameType)
    {
        this.roomId = roomId;
		//txtroomid.text = "ID: " + roomId;
//		txtroomid.text = "";
        GameTools.GetSingleton().GetTextureNet(url, GetSprtie);
        roomName.text = deskName;
		roomName2.text = "房主: " + hostName + " | " + "俱乐部: " + clubName;

		string[] persion = playerCount.Split ('|');
//        this.clubName.text = hostName;
        string[] b = blinds.Split('|');

        this.blinds.text = b[0] + "/" + b[1];
        if (b[3] != "0")
        {
            this.blinds.text = this.blinds.text + "/" + b[3];
        }

        if (b[2] != "0")
        {
            this.blinds.text = this.blinds.text + "(" + b[2] + ")";
        }
		this.playerCount.text = persion[0]+"/"+persion[1];
        this.time.text = time;
		imageClub.gameObject.SetActive (true);
		imageBaoxian.gameObject.SetActive (insurance == 1);

		imagetype.sprite = Resources.Load<Sprite> ("img/gtype_" + gameType);

        //int timeValue = int.Parse(time);
        //if (timeValue < 60)
        //{
        //    this.time.text = timeValue + "分钟";
        //}
        //else
        //{
        //    if (timeValue % 60 == 0)
        //    {
        //        this.time.text = timeValue / 60 + "小时";
        //    }
        //    else
        //    {
        //        this.time.text = timeValue / 60 + "小时" + timeValue % 60 + "分钟";
        //    }
        //}

        if (state == "1")
        {
            this.roomState.text = "<color=orange>未开始</color>";
        }
        else if (state == "2"){
            this.roomState.text = "<color=green>进行中</color>";
        }
        else if (state == "3")
        {
            this.roomState.text = "<color=red>已暂停</color>";
        }


    }

    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
