using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemChatScroller : MonoBehaviour
{

    private RectTransform selfRt;
    private GameObject GameItem;
    private GameObject MyPaiPuTopPanelItem;
    private GameObject OppontentItem;
    private GameObject FriendTopPanelItem;
    private GameObject GameRecordItem;
    private GameObject BenJuPaiItem;
    private GameObject MyMsgItem;
    private GameObject MyMsgItem2;
    private GameObject PaiMsgItem;


    //GameItem
	private CircleImage imageGameItemHead;
    private Text textGameItemDeskName;
    private Text textGameItemName;
    private Text textGameItemChouma;
    private Text textGameItemPersion;
    private Text textGameItemTime;
    private Text textGameItemIsStarting;
    private Button GameItemSelf;
    private string gameItemID;
	private Image imageBaoxian;
	private Image imageClub;
	private Image imagetype;
//	private Text txtroomid;

    //MyPaiPuTopPanelItem
    private RawImage[] imageMyPaiPuTopPanelItempai;
//    private RawImage imageMyPaiPuTopPanelItempai2;
    private Text textMyPaiPuTopPanelItemtitle;
    private Text textMyPaiPuTopPanelItemscore;
    private Text textMyPaiPuTopPanelItemchouma;
    private Text textMyPaiPuTopPanelItemwin;
    //OppontentItem
	private CircleImage imageOppontentItemHead;
    private Text textOppontentItemName;
    private Text textOppontentItemShouShu;
    private Text textOppontentItemWin;
    private Text textOppontentItemFail;
    private Text textOppontentItemScore;
    //FriendTopPanelItem
    private Button btnFriendTopPanelItemSelf;
	private CircleImage imageFriendTopPanelItemHead;
    private Text textFriendTopPanelItemName;
    public string friendid;
    private Button friendEditBtn;
    //GameRecordItem
//    private RawImage imageGameRecordItemHead;
    private Text textGameRecordItemDeskName;
    private Text textGameRecordItemChouma;
    private Text textGameRecordItemTime;
    private Text textGameRecordItemInfoData;
    private Text textGameRecordItemInfoTime;
    private Text textGameRecordItemInfoName;
    private Text textGameRecordItemScore;
    private Button GameRecordItemSelf;

	private Image imageGameRecordBaoxian;
	private Image imageGameRecordClub;
	private Image imageGameRecordtype;

    public string id;
    public string clubid;
    public string unionId = "";
    //BenJuPaiItem
    private Button BenJuPaiItemSelf;
    private RawImage[] imageBenJuPaiItemPai;
//    private RawImage imageBenJuPaiItemPai2;
    private Text textBenJuPaiItemTitle;
    private Text textBenJuPaiItemChouma;
    private Text textBenJuPaiItemTime;
    private Text textBenJuPaiItemScore;
    public string id5;
    //MyMsgItem
    private Button MyMsgItemSelf;
    private Text textMyMsgItemTitle;
    private Text textMyMsgItemMsg;
    private Text textMyMsgItemTime;
    private Button btnMyMsgItemAgree;
    private Button btnMyMsgItemRefuse;
	private CircleImage iconMyMsgItem;
    public string type;
    //MyMsgItem2
    private Text textMyMsgItem2Title;
    private Text textMyMsgItem2Time;
	private CircleImage iconMyMsgItem2;
    //PaiMsgItem
	private CircleImage imagePaiMsgItemHead;
    private Text textPaiMsgItemTitle;
    private Text textPaiMsgItemName;
    private Text textPaiMsgItemChouma;
    private Text textPaiMsgItemTime;
    private Text textPaiMsgItemType;
    private Button btnPaiMsgItemAdd;
    private Button btnPaiMsgItemRefuse;
    public string id8;

    private void Awake()
    {
        selfRt = gameObject.GetComponent<RectTransform>();
        
        //GameItem
        GameItem = transform.Find("GameItem").gameObject;
		imageGameItemHead = transform.Find("GameItem/Icon").GetComponent<CircleImage>();
		textGameItemDeskName = transform.Find("GameItem/Panel/DeskName").GetComponent<Text>();
		imageBaoxian = transform.Find("GameItem/Panel/baoxian").GetComponent<Image>();
		imageClub = transform.Find("GameItem/Panel/julebu").GetComponent<Image>();
		imagetype = transform.Find("GameItem/Panel/gametype").GetComponent<Image>();
        textGameItemName = transform.Find("GameItem/Name").GetComponent<Text>();
        textGameItemChouma = transform.Find("GameItem/Chouma/value").GetComponent<Text>();
        textGameItemPersion = transform.Find("GameItem/Persion/value").GetComponent<Text>();
        textGameItemTime = transform.Find("GameItem/Time/value").GetComponent<Text>();
        textGameItemIsStarting = transform.Find("GameItem/isStarting").GetComponent<Text>();
//		txtroomid = transform.Find("GameItem/Panel/Id").GetComponent<Text>();

        GameItemSelf = GameItem.GetComponent<Button>();
        //MyPaiPuTopPanelItem
        MyPaiPuTopPanelItem = transform.Find("MyPaiPuTopPanelItem").gameObject;
		imageMyPaiPuTopPanelItempai = MyPaiPuTopPanelItem.transform.Find("Pai").GetComponentsInChildren<RawImage>();
//        imageMyPaiPuTopPanelItempai2 = MyPaiPuTopPanelItem.transform.Find("Icon/pai2").GetComponent<RawImage>();
        textMyPaiPuTopPanelItemtitle = MyPaiPuTopPanelItem.transform.Find("Text").GetComponent<Text>();
        textMyPaiPuTopPanelItemscore = MyPaiPuTopPanelItem.transform.Find("value").GetComponent<Text>();
        textMyPaiPuTopPanelItemchouma = MyPaiPuTopPanelItem.transform.Find("Chouma/value").GetComponent<Text>();
        textMyPaiPuTopPanelItemwin = MyPaiPuTopPanelItem.transform.Find("Win/value").GetComponent<Text>();
        //OppontentItem
        OppontentItem = transform.Find("OppontentItem").gameObject;
		imageOppontentItemHead = OppontentItem.transform.Find("Icon").GetComponent<CircleImage>();
        textOppontentItemName = OppontentItem.transform.Find("Name").GetComponent<Text>();
        textOppontentItemShouShu = OppontentItem.transform.Find("value1").GetComponent<Text>();
        textOppontentItemWin = OppontentItem.transform.Find("value2").GetComponent<Text>();
        textOppontentItemFail = OppontentItem.transform.Find("value3").GetComponent<Text>();
        textOppontentItemScore = OppontentItem.transform.Find("value4").GetComponent<Text>();
        
        //FriendTopPanelItem
        FriendTopPanelItem = transform.Find("FriendTopPanelItem").gameObject;
		imageFriendTopPanelItemHead = FriendTopPanelItem.transform.Find("Icon").GetComponent<CircleImage>();
        textFriendTopPanelItemName = FriendTopPanelItem.transform.Find("Name").GetComponent<Text>();
        btnFriendTopPanelItemSelf = FriendTopPanelItem.GetComponent<Button>();
        friendEditBtn = FriendTopPanelItem.transform.Find("EditBtn").GetComponent<Button>();
        
        //GameRecordItem
        GameRecordItem = transform.Find("GameRecordItem").gameObject;
        GameRecordItemSelf = GameRecordItem.GetComponent<Button>();
//        imageGameRecordItemHead = GameRecordItem.transform.Find("Icon/mask/Image").GetComponent<RawImage>();
		textGameRecordItemDeskName = GameRecordItem.transform.Find("Panel/DeskName").GetComponent<Text>();
		imageGameRecordBaoxian = GameRecordItem.transform.Find("Panel/baoxian").GetComponent<Image>();
		imageGameRecordClub = GameRecordItem.transform.Find("Panel/julebu").GetComponent<Image>();
		imageGameRecordtype = GameRecordItem.transform.Find("Panel/gametype").GetComponent<Image>();
        textGameRecordItemChouma = GameRecordItem.transform.Find("Chouma/value").GetComponent<Text>();
        textGameRecordItemTime = GameRecordItem.transform.Find("Time/value").GetComponent<Text>();
		textGameRecordItemInfoData = GameRecordItem.transform.Find("shijianxian/Text").GetComponent<Text>();
        textGameRecordItemInfoTime = GameRecordItem.transform.Find("InfoTime").GetComponent<Text>();
//        textGameRecordItemInfoName = GameRecordItem.transform.Find("InfoName").GetComponent<Text>();
        textGameRecordItemScore = GameRecordItem.transform.Find("Score").GetComponent<Text>();



        //BenJuPaiItem
        BenJuPaiItem = transform.Find("BenJuPaiItem").gameObject;
        BenJuPaiItemSelf = BenJuPaiItem.GetComponent<Button>();
		imageBenJuPaiItemPai = BenJuPaiItem.transform.Find("Pai").GetComponentsInChildren<RawImage>();
//        imageBenJuPaiItemPai2 = BenJuPaiItem.transform.Find("Pai/2").GetComponent<RawImage>();
        textBenJuPaiItemChouma = BenJuPaiItem.transform.Find("Chouma/value").GetComponent<Text>();
        textBenJuPaiItemTime = BenJuPaiItem.transform.Find("Time/value").GetComponent<Text>();
        textBenJuPaiItemScore = BenJuPaiItem.transform.Find("Score").GetComponent<Text>();
        textBenJuPaiItemTitle = BenJuPaiItem.transform.Find("Title").GetComponent<Text>();
        //MyMsgItem
        MyMsgItem = transform.Find("MyMsgItem").gameObject;
        textMyMsgItemTitle = MyMsgItem.transform.Find("Title").GetComponent<Text>();
        textMyMsgItemMsg = MyMsgItem.transform.Find("Msg").GetComponent<Text>();
        textMyMsgItemTime = MyMsgItem.transform.Find("Time").GetComponent<Text>();
        btnMyMsgItemAgree = MyMsgItem.transform.Find("Add").GetComponent<Button>();
        btnMyMsgItemRefuse = MyMsgItem.transform.Find("Refuse").GetComponent<Button>();
		iconMyMsgItem = MyMsgItem.transform.Find("Head").GetComponent<CircleImage>();
        //MyMsgItem2
        MyMsgItem2 = transform.Find("MyMsgItem2").gameObject;
        textMyMsgItem2Title = MyMsgItem2.transform.Find("Title").GetComponent<Text>();
        textMyMsgItem2Time = MyMsgItem2.transform.Find("Time").GetComponent<Text>();
		iconMyMsgItem2 = MyMsgItem2.transform.Find("Head").GetComponent<CircleImage>();
        //PaiMsgItem
        PaiMsgItem = transform.Find("PaiMsgItem").gameObject;
		imagePaiMsgItemHead = PaiMsgItem.transform.Find("Icon").GetComponent<CircleImage>();
        textPaiMsgItemTitle = PaiMsgItem.transform.Find("Title").GetComponent<Text>();
        textPaiMsgItemName = PaiMsgItem.transform.Find("Name").GetComponent<Text>();
        textPaiMsgItemChouma = PaiMsgItem.transform.Find("Chouma").GetComponent<Text>();
        textPaiMsgItemTime = PaiMsgItem.transform.Find("Time").GetComponent<Text>();
        textPaiMsgItemType = PaiMsgItem.transform.Find("Type").GetComponent<Text>();
        btnPaiMsgItemAdd = PaiMsgItem.transform.Find("Add").GetComponent<Button>();
        btnPaiMsgItemRefuse = PaiMsgItem.transform.Find("Refuse").GetComponent<Button>();

        AddButtonListen();
    }


    public void SetData(Hashtable data)
    {
        int type = Convert.ToInt32(data["Type"]);
        switch (type)
        {
		case 0:
			string headurl1 = data ["URL"].ToString ();
			string deskname = data ["DeskName"].ToString ();
			string name = data ["Name"].ToString ();
			string[] chouma = data ["Chouma"].ToString ().Split ('|');
			string[] persion = data ["Persion"].ToString ().Split ('|');
			string time = data ["Time"].ToString ();
			string isstarting = data ["isStarting"].ToString ();
			string clubName = data ["clubName"].ToString ();
			int insurance = int.Parse (data ["insurance"].ToString ());
			int gameType = int.Parse (data ["gameType"].ToString ());
			gameItemID = data ["ID"].ToString ();
			GameItem.SetActive (true);
			MyPaiPuTopPanelItem.SetActive (false);
			OppontentItem.SetActive (false);
			FriendTopPanelItem.SetActive (false);
			GameRecordItem.SetActive (false);
			BenJuPaiItem.SetActive (false);
			MyMsgItem.SetActive (false);
			MyMsgItem2.SetActive (false);
			PaiMsgItem.SetActive (false);

			LoadHead (headurl1, imageGameItemHead);

			textGameItemDeskName.text = deskname;
//			txtroomid.text = "";
			//textGameItemChouma.text = chouma [0] + "/" + chouma [1] + "(" + chouma [2] + ")";

            textGameItemChouma.text = chouma[0] + "/" + chouma[1];
            if (chouma[3] != "0")
            {
                textGameItemChouma.text = textGameItemChouma.text + "/" + chouma[3];
            }

            if (chouma[2] != "0")
            {
                textGameItemChouma.text = textGameItemChouma.text + "(" + chouma[2] + ")";
            }

            textGameItemPersion.text = persion [0] + "/" + persion [1];
			textGameItemTime.text = time;
			imageBaoxian.gameObject.SetActive (false);
			if (insurance == 1) {
				imageBaoxian.gameObject.SetActive (true);
			}
			imageClub.gameObject.SetActive (true);
			textGameItemName.text = "房主: " + name + " | " + "俱乐部: " + clubName;
			if (clubName == "") {
				imageClub.gameObject.SetActive (false);
				textGameItemName.text = "房主: " + name + " | " + "ID: " + gameItemID;
			}

			imagetype.sprite = Resources.Load<Sprite> ("img/gtype_" + gameType);

                switch (isstarting)
                {
                    case "1":
                        textGameItemIsStarting.text = "<color=orange>未开始</color>";
                        break;
                    case "2":
                        textGameItemIsStarting.text = "<color=green>进行中</color>";
                        break;
                    case "3":
                        textGameItemIsStarting.text = "<color=red>已暂停</color>";
                        break;
                }
                
                break;
            case 1:
				string[] pai1 = data ["Pai"].ToString ().Split ('|');
//                string pai11 = data["Pai1"].ToString();
//                string pai12 = data["Pai2"].ToString();
                string title = data["Title"].ToString();
                string chouma1 = data["Chouma"].ToString();
                string win = data["Win"].ToString();
                string score = data["Score"].ToString();
                MyPaiPuTopPanelItem.SetActive(true);
                GameItem.SetActive(false);
                OppontentItem.SetActive(false);
                FriendTopPanelItem.SetActive(false);
                GameRecordItem.SetActive(false);
                BenJuPaiItem.SetActive(false);
                MyMsgItem.SetActive(false);
                MyMsgItem2.SetActive(false);
                PaiMsgItem.SetActive(false);
                textMyPaiPuTopPanelItemtitle.text = title;
                textMyPaiPuTopPanelItemchouma.text = chouma1;
                textMyPaiPuTopPanelItemwin.text = win;
                textMyPaiPuTopPanelItemscore.text = score;
                textMyPaiPuTopPanelItemscore.color = StaticFunction.Getsingleton().GetColor(int.Parse(score));

				if (pai1.Length == 2) {
					imageMyPaiPuTopPanelItempai[0].gameObject.SetActive (false);
					imageMyPaiPuTopPanelItempai[3].gameObject.SetActive (false);
					imageMyPaiPuTopPanelItempai[1].texture = StaticFunction.Getsingleton().SetPai(pai1[0]);
					imageMyPaiPuTopPanelItempai[2].texture = StaticFunction.Getsingleton().SetPai(pai1[1]);
				}

				if (pai1.Length == 4) {
					imageMyPaiPuTopPanelItempai[0].gameObject.SetActive (true);
					imageMyPaiPuTopPanelItempai[3].gameObject.SetActive (true);
					imageMyPaiPuTopPanelItempai[0].texture = StaticFunction.Getsingleton().SetPai(pai1[0]);
					imageMyPaiPuTopPanelItempai[1].texture = StaticFunction.Getsingleton().SetPai(pai1[1]);
					imageMyPaiPuTopPanelItempai[2].texture = StaticFunction.Getsingleton().SetPai(pai1[2]);
					imageMyPaiPuTopPanelItempai[3].texture = StaticFunction.Getsingleton().SetPai(pai1[3]);
				}

                break;
            case 2:
                string head = data["URL"].ToString();
                string name2 = data["Name"].ToString();
                string shouShu = data["ShouShu"].ToString();
                string win2 = data["Win"].ToString();
                string fail2 = data["Fail"].ToString();
                string score2 = data["Score"].ToString();
                OppontentItem.SetActive(true);
                MyPaiPuTopPanelItem.SetActive(false);
                GameItem.SetActive(false);
                FriendTopPanelItem.SetActive(false);
                GameRecordItem.SetActive(false);
                BenJuPaiItem.SetActive(false);
                MyMsgItem.SetActive(false);
                MyMsgItem2.SetActive(false);
                PaiMsgItem.SetActive(false);
                LoadHead(head, imageOppontentItemHead);
                textOppontentItemName.text = name2;
                textOppontentItemShouShu.text = shouShu;
                textOppontentItemWin.text = win2;
                textOppontentItemFail.text = fail2;
                textOppontentItemScore.text = score2;
                textOppontentItemScore.color = StaticFunction.Getsingleton().GetColor(int.Parse(score2));
                break;
            case 3:
                string head3 = data["URL"].ToString();
                string name3 = data["Name"].ToString();
                friendid = data["ID"].ToString();
                FriendTopPanelItem.SetActive(true);
                OppontentItem.SetActive(false);
                MyPaiPuTopPanelItem.SetActive(false);
                GameItem.SetActive(false);
                GameRecordItem.SetActive(false);
                BenJuPaiItem.SetActive(false);
                MyMsgItem.SetActive(false);
                MyMsgItem2.SetActive(false);
                PaiMsgItem.SetActive(false);
                textFriendTopPanelItemName.text = name3;
                LoadHead(head3, imageFriendTopPanelItemHead);
                break;
		case 4:
				string head4 = data ["URL"].ToString ();
				string ID = data ["ID"].ToString ();
				this.id = ID;
				string DeskName = data ["DeskName"].ToString ();
				string Chouma = data ["Chouma"].ToString ();
				string Time = data ["Time"].ToString ();
				string InfoData = data ["InfoData"].ToString ();
				string InfoTime = data ["InfoTime"].ToString ();
				string InfoName = data ["InfoName"].ToString ();
				string Score = data ["Score"].ToString ();
					
				string isinsurance = data ["insurance"].ToString ();  
					
				GameRecordItem.SetActive (true);
				FriendTopPanelItem.SetActive (false);
				OppontentItem.SetActive (false);
				MyPaiPuTopPanelItem.SetActive (false);
				GameItem.SetActive (false);
				BenJuPaiItem.SetActive (false);
				MyMsgItem.SetActive (false);
				MyMsgItem2.SetActive (false);
				PaiMsgItem.SetActive (false);
		//                LoadHead(head4, imageGameRecordItemHead);
				textGameRecordItemDeskName.text = DeskName;
				textGameRecordItemChouma.text = Chouma;
				textGameRecordItemTime.text = Time;
				textGameRecordItemInfoData.text = InfoData;
				textGameRecordItemInfoTime.text = InfoTime;
				if (InfoName != "0") {
					imageGameRecordClub.gameObject.SetActive (true);
				} else {
					imageGameRecordClub.gameObject.SetActive (false);
				}
					

				imageGameRecordBaoxian.gameObject.SetActive (false);
				if (isinsurance == "1") {
					imageGameRecordBaoxian.gameObject.SetActive (true);
				}

				int GameRecordType = int.Parse (data ["gameType"].ToString ());
				imageGameRecordtype.sprite = Resources.Load<Sprite> ("img/gtype_" + GameRecordType);
//                textGameRecordItemInfoName.text = InfoName;
                textGameRecordItemScore.text = Score;
                textGameRecordItemScore.color= StaticFunction.Getsingleton().GetColor(int.Parse(data["Score"].ToString()));
                break;
		case 5:
			
			string[] pai5 = data ["Pai"].ToString ().Split ('|');
//                string pai51 = data["Pai1"].ToString();
//                string pai52 = data["Pai2"].ToString();
			string Title = data ["Title"].ToString ();
			string Chouma5 = data ["Chouma"].ToString ();
			string Time5 = data ["Time"].ToString ();
			string Score5 = data ["Score"].ToString ();
			textBenJuPaiItemTitle.text = Title;
			BenJuPaiItem.SetActive (true);
			string id5 = data ["ID"].ToString ();
			this.id5 = id5;
			GameRecordItem.SetActive (false);
			FriendTopPanelItem.SetActive (false);
			OppontentItem.SetActive (false);
			MyPaiPuTopPanelItem.SetActive (false);
			GameItem.SetActive (false);
			MyMsgItem.SetActive (false);
			MyMsgItem2.SetActive (false);
			PaiMsgItem.SetActive (false);
			textBenJuPaiItemChouma.text = Chouma5;
			textBenJuPaiItemTime.text = Time5;
			textBenJuPaiItemScore.text = Score5;
			textBenJuPaiItemScore.color = StaticFunction.Getsingleton ().GetColor (int.Parse (Score5));

			if (pai5.Length == 2) {
				imageBenJuPaiItemPai [0].gameObject.SetActive (false);
				imageBenJuPaiItemPai [3].gameObject.SetActive (false);
				imageBenJuPaiItemPai[1].texture = StaticFunction.Getsingleton().SetPai(pai5[0]);
				imageBenJuPaiItemPai[2].texture = StaticFunction.Getsingleton().SetPai(pai5[1]);
			}

			if (pai5.Length == 4) {
				imageBenJuPaiItemPai [0].gameObject.SetActive (true);
				imageBenJuPaiItemPai [3].gameObject.SetActive (true);
				imageBenJuPaiItemPai[0].texture = StaticFunction.Getsingleton().SetPai(pai5[0]);
				imageBenJuPaiItemPai[1].texture = StaticFunction.Getsingleton().SetPai(pai5[1]);
				imageBenJuPaiItemPai[2].texture = StaticFunction.Getsingleton().SetPai(pai5[2]);
				imageBenJuPaiItemPai[3].texture = StaticFunction.Getsingleton().SetPai(pai5[3]);
			}


                break;
            case 6:
                string title6 = data["Title"].ToString();
                string msg6 = data["Msg"].ToString();
                string time6 = data["Time"].ToString();
                string id6 = data["ID"].ToString();
                string clubid6 = data["ClubID"].ToString();
				string url6 = data["userIcon"].ToString();
                MyMsgItem.SetActive(true);
                BenJuPaiItem.SetActive(false);
                GameRecordItem.SetActive(false);
                FriendTopPanelItem.SetActive(false);
                OppontentItem.SetActive(false);
                MyPaiPuTopPanelItem.SetActive(false);
                GameItem.SetActive(false);
                MyMsgItem2.SetActive(false);
                PaiMsgItem.SetActive(false);
                id = id6;
                clubid = clubid6;
                if(data.ContainsKey("unionId")){
                    unionId = data["unionId"].ToString();
                } else {
                    unionId = "0";
                }
                this.type = data["Type1"].ToString();
                textMyMsgItemTitle.text = title6;
                textMyMsgItemMsg.text = msg6;
                textMyMsgItemTime.text = time6;

				LoadHead(url6, iconMyMsgItem);
                break;
            case 7:
                string title7 = data["Title"].ToString();
                string time7 = data["Time"].ToString();
				string url7 = data["userIcon"].ToString();
                MyMsgItem2.SetActive(true);
                MyMsgItem.SetActive(false);
                BenJuPaiItem.SetActive(false);
                GameRecordItem.SetActive(false);
                FriendTopPanelItem.SetActive(false);
                OppontentItem.SetActive(false);
                MyPaiPuTopPanelItem.SetActive(false);
                GameItem.SetActive(false);
                PaiMsgItem.SetActive(false);
                textMyMsgItem2Title.text = title7;
                textMyMsgItem2Time.text = time7;
				LoadHead(url7, iconMyMsgItem2);
                break;
            case 8:
                string url8 = data["URL"].ToString();
                string title8 = data["Title"].ToString();
                string name8 = data["Name"].ToString();
                string chouma8 = data["Chouma"].ToString();
                //string time8 = data["Time"].ToString();
                //string state = data["State"].ToString();
                string id8 = data["ID"].ToString();
                PaiMsgItem.SetActive(true);
                MyMsgItem2.SetActive(false);
                MyMsgItem.SetActive(false);
                BenJuPaiItem.SetActive(false);
                GameRecordItem.SetActive(false);
                FriendTopPanelItem.SetActive(false);
                OppontentItem.SetActive(false);
                MyPaiPuTopPanelItem.SetActive(false);
                GameItem.SetActive(false);
                LoadHead(url8,imagePaiMsgItemHead);
                textPaiMsgItemTitle.text = title8;
                textPaiMsgItemName.text = name8;
                textPaiMsgItemChouma.text = chouma8;
                //textPaiMsgItemTime.text = time8;
                //textPaiMsgItemType.text = state;
                this.id8 = id8;
                textPaiMsgItemTime.gameObject.SetActive(false);
                textPaiMsgItemType.gameObject.SetActive(false);
                btnPaiMsgItemAdd.gameObject.SetActive(true);
                btnPaiMsgItemRefuse.gameObject.SetActive(true);
                break;
            case 9:
                string url9 = data["URL"].ToString();
                string title9 = data["Title"].ToString();
                string name9 = data["Name"].ToString();
                string chouma9 = data["Chouma"].ToString();
                string time9 = data["Time"].ToString();
                string state = data["State"].ToString();
                //string id9 = data["ID"].ToString();
                PaiMsgItem.SetActive(true);
                MyMsgItem2.SetActive(false);
                MyMsgItem.SetActive(false);
                BenJuPaiItem.SetActive(false);
                GameRecordItem.SetActive(false);
                FriendTopPanelItem.SetActive(false);
                OppontentItem.SetActive(false);
                MyPaiPuTopPanelItem.SetActive(false);
                GameItem.SetActive(false);
                LoadHead(url9, imagePaiMsgItemHead);
                textPaiMsgItemTitle.text = title9;
                textPaiMsgItemName.text = name9;
                textPaiMsgItemChouma.text = chouma9;
                textPaiMsgItemTime.text = time9;
                textPaiMsgItemType.text = state;
                //this.id8 = id9;
                btnPaiMsgItemAdd.gameObject.SetActive(false);
                btnPaiMsgItemRefuse.gameObject.SetActive(false);
                break;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(selfRt);
    }

//	private void LoadHead(string url, RawImage image)
//	{
//		if(ChatScroller.headList == null)
//			ChatScroller.headList = new Dictionary<string, Texture2D>();
//		if (!ChatScroller.headList.ContainsKey(url))
//		{
//			ChatScroller.headList[url] = new Texture2D(150, 150);
//			StartCoroutine(LoadHeadImage(url));
//		}

//		image.texture = ChatScroller.headList[url];
//	}

	private void LoadHead(string url, CircleImage image)
    {
        if(ChatScroller.headList == null)
            ChatScroller.headList = new Dictionary<string, Texture2D>();
        if (!ChatScroller.headList.ContainsKey(url))
        {
            ChatScroller.headList[url] = new Texture2D(150, 150);
            StartCoroutine(LoadHeadImage(url));
        }

		Texture2D result = ChatScroller.headList[url];
		Sprite sp = Sprite.Create(result, new Rect(0, 0, result.width, result.height), Vector2.zero);
		image.sprite = sp;
    }

    private IEnumerator LoadHeadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Load Head Error:" + www.error);
        }
        else
        {
            if (ChatScroller.headList.ContainsKey(url))
            {
                Texture2D result = ScaleTexture(www.texture, 100, 100);
                yield return null;
                byte[] bytes = result.EncodeToPNG();
                yield return null;
                ChatScroller.headList[url].LoadImage(bytes);
            }
        }
    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                result.SetPixel(j, i, source.GetPixelBilinear(j / (float)result.width, i / (float)result.height));
            }
        }
        result.Apply();
        return result;
    }

    public float GetHeight()
    {
        return selfRt.sizeDelta.y;
    }

    public void AddButtonListen()
    {
        GameRecordItemSelf.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().scoreDetailTopPanel.id = this.id;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().scoreDetailTopPanel);
        });
        GameItemSelf.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerEnterRequest, new object[] {gameItemID });
        });
        btnMyMsgItemAgree.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (type=="2")
            {
                NetMngr.GetSingleton().Send(InterfaceMain.IsAgree, new object[] { id, "1" });
            }
            else
            {
                if(unionId != "" && unionId != "0"){ // 加入联盟
                    NetMngr.GetSingleton().Send(InterfaceUnion.IsUnionAgree, new object[] { 
                        int.Parse(id), int.Parse(unionId),1, int.Parse(clubid) });
                } else {
                    NetMngr.GetSingleton().Send(InterfaceMain.IsClubAgree, new object[] { int.Parse(id), int.Parse(clubid),1 });
                }
            }
        });
        btnMyMsgItemRefuse.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (type == "2")
            {
                NetMngr.GetSingleton().Send(InterfaceMain.IsAgree, new object[] { id, "0" });
            }
            else
            {
                if(unionId != "" && unionId != "0"){ // 加入联盟
                    NetMngr.GetSingleton().Send(InterfaceUnion.IsUnionAgree, new object[] { 
                        int.Parse(id), int.Parse(unionId), 0, int.Parse(clubid) });
                } else {
                    NetMngr.GetSingleton().Send(InterfaceMain.IsClubAgree, new object[] { int.Parse(id), int.Parse(clubid) ,0});
                }
            }
        });
        btnPaiMsgItemAdd.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.IsAgreeJoinPai, new object[] { id8, "1" });
        });
        btnPaiMsgItemRefuse.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.IsAgreeJoinPai, new object[] { id8, "0" });
        });
        btnFriendTopPanelItemSelf.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().friendDetailTopPanel.id = friendid;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().friendDetailTopPanel);
        });
        friendEditBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().friendRemarkPanel.friendId = friendid;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().friendRemarkPanel);
        });

        BenJuPaiItemSelf.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.getGameReview, new object[] { id5 });
        });

    }
}
