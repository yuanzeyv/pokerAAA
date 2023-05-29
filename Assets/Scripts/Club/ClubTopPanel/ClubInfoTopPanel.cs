using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ClubInfoTopPanel : BasePlane
{
    public Button backBtn;
    public Button settingBtn;

	public Button exitBtn;
	public Button disBtn;

    public Transform infoContent;

    public RawImage clubHead;
    public Text clubName;
    public Text clubId;
    public string clubIdString;
    public GameObject tag;
    public GameObject tag2;
    public GameObject tag3;

    public Text clubMemberCount;
    public Transform clubTagContent;

    public Text clubjianjie;
    public Text kefuWX;
    public string kefu;
    public Button copyBtn;

    public Transform memberHeadContent;
    public Text memberCount;
    public Button seeClubChengYuanBtn;
    public Button seeClubChengYuanConentBtn;
    public Button seeClubSettingBtn;

    public Button seeClubUpdateBtn;

	public Toggle seeSendCoinToggle;

	public Toggle seeSendDiamondToggle;

    public Button seeClubWelcomeBtn;

    public Button seeClubShareBtn;

    public Button seeCashOutBtn;

    public Button seeClubMemberDiamonBtn;
    public Button seeClubMemberCoinBtn;
    
    public Toggle clubRefuseMsg;

    public Toggle clubSelf;

    public Button delGongGaoBtn;
    //需要区分显示的内容
    public GameObject setting;
    public GameObject update;
    public GameObject welcome;
    public GameObject refuse;
    public GameObject self;
    public GameObject delgonggao;
    public GameObject sendCoin;
    public GameObject sendDiamond;
    public GameObject sendDiamondMember;
    public GameObject sendCoinMember;

	public GameObject sendCoinTip;
	public GameObject sendDiamondip;

	public Button sendCoinTipBtn;
	public Button sendDiamondipBtn;

	public GameObject clubdis;

	public bool initialSendCoin;
	public bool initialSendDiamond;


    public int isMine; //0-成员 1-群主 2-管理员

    public bool canSendCoin=false;
    public bool canSendDiamond = false;

    private GameObject clubMyUnion;
    private GameObject clubUnionCoin;
    
    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        settingBtn = transform.Find("ToggleGroup/SettingBtn").GetComponent<Button>();

        infoContent = transform.Find("Info/Viewport/Content");

        clubHead = infoContent.Find("ClubBG/ClubTitle/ClubHead/mask/head").GetComponent<RawImage>();
        clubName = infoContent.Find("ClubBG/ClubTitle/ClubName").GetComponent<Text>();
        clubId = infoContent.Find("ClubBG/ClubTitle/ClubId").GetComponent<Text>();
        clubMemberCount = infoContent.Find("ClubBG/ClubTitle/ClubMemberCount").GetComponent<Text>();
        clubTagContent = infoContent.Find("ClubBG/ClubTitle/ClubTip/Scroll View/Viewport/Content");
        tag = clubTagContent.Find("tip").gameObject;
        tag2 = clubTagContent.Find("tip2").gameObject;
        tag3 = clubTagContent.Find("tip3").gameObject;

        

		clubjianjie = infoContent.Find("ClubBG/ClubTitle/Jieshao/JieshaoText").GetComponent<Text>();
		kefuWX = infoContent.Find("ClubBG/ClubFuWX/KeFuWX").GetComponent<Text>();
		copyBtn = infoContent.Find("ClubBG/ClubFuWX/copyBtn").GetComponent<Button>();

        memberHeadContent = infoContent.Find("ClubChengYuan/Scroll View/Viewport/Content");
        seeClubChengYuanConentBtn = infoContent.Find("ClubChengYuan/Scroll View/Viewport/Content").GetComponent<Button>();
        memberCount = infoContent.Find("ClubChengYuan/memberCount").GetComponent<Text>();
        seeClubChengYuanBtn = infoContent.Find("ClubChengYuan/moreBtn").GetComponent<Button>();

        seeClubSettingBtn = infoContent.Find("ClubSetting/moreBtn").GetComponent<Button>();

        seeClubUpdateBtn = infoContent.Find("ClubUpdate/moreBtn").GetComponent<Button>();

        //edit by yang yang 2021年3月22日 14:38:46
        seeSendCoinToggle = infoContent.Find("ClubSendCoin/Toggle").GetComponent<Toggle>();
        seeSendDiamondToggle = infoContent.Find("ClubSendDiamond/Toggle").GetComponent<Toggle>();

        //		seeSendCoinToggle = infoContent.Find("ClubSendCoin/Toggle").GetComponent<Button>();
        //        seeSendDiamondToggle = infoContent.Find("ClubSendDiamond/moreBtn").GetComponent<Button>();

        seeClubWelcomeBtn = infoContent.Find("ClubWelcome/moreBtn").GetComponent<Button>();

        seeClubShareBtn = infoContent.Find("ClubShare/moreBtn").GetComponent<Button>();

        seeCashOutBtn = infoContent.Find("ClubCashOut/moreBtn").GetComponent<Button>();

        seeClubMemberDiamonBtn = infoContent.Find("ClubSendMemberDiamond/moreBtn").GetComponent<Button>();
        seeClubMemberCoinBtn = infoContent.Find("ClubSendMemberCoin/moreBtn").GetComponent<Button>();

        clubRefuseMsg = infoContent.Find("ClubRefuseMsg/Toggle").GetComponent<Toggle>();

        clubSelf = infoContent.Find("ClubSelf/Toggle").GetComponent<Toggle>();

		delGongGaoBtn = infoContent.Find("ClubDelGongGao/moreBtn").GetComponent<Button>();

		exitBtn = infoContent.Find("Clubdis/exit").GetComponent<Button>();
		disBtn = infoContent.Find("Clubdis/dis").GetComponent<Button>();

		sendCoinTip = infoContent.Find("ClubSendCoin/Cointips").gameObject;
		sendDiamondip = infoContent.Find("ClubSendDiamond/Diamondtips").gameObject;

		sendCoinTipBtn = infoContent.Find("ClubSendCoin/Wenhao").GetComponent<Button>();
		sendDiamondipBtn = infoContent.Find("ClubSendCoin/Wenhao").GetComponent<Button>();


		sendCoinTipBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			sendCoinTip.SetActive(true);
		});

		sendDiamondipBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			sendDiamondip.SetActive(true);
		});

        //需要区分显示的内容
        setting = infoContent.Find("ClubSetting").gameObject;
        update = infoContent.Find("ClubUpdate").gameObject;
        welcome = infoContent.Find("ClubWelcome").gameObject;
        refuse = infoContent.Find("ClubRefuseMsg").gameObject;
        delgonggao = infoContent.Find("ClubDelGongGao").gameObject;
        sendCoin = infoContent.Find("ClubSendCoin").gameObject;
        sendDiamond = infoContent.Find("ClubSendDiamond").gameObject;
        sendDiamondMember = infoContent.Find("ClubSendMemberDiamond").gameObject;
		sendCoinMember = infoContent.Find("ClubSendMemberCoin").gameObject;
        clubdis = infoContent.Find("Clubdis").gameObject;
        self = infoContent.Find("ClubSelf").gameObject;

        backBtn.onClick.AddListener(() => {
            
			if (isMine==1||isMine==2)
			{
				if(seeSendCoinToggle.isOn != initialSendCoin)
				{
					if (seeSendCoinToggle.isOn)
					{
						// NetMngr.GetSingleton().Send(InterfaceClub.SetSendCoin, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "1" });
                        NetMngr.GetSingleton().Send(InterfaceClub.UpdateManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1, 1 });
					}
					else
					{
						// NetMngr.GetSingleton().Send(InterfaceClub.SetSendCoin, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "0" });
                        NetMngr.GetSingleton().Send(InterfaceClub.UpdateManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1, 0 });
					}
				}
				if(seeSendDiamondToggle.isOn != initialSendDiamond){
					if (seeSendDiamondToggle.isOn)
					{
						// NetMngr.GetSingleton().Send(InterfaceClub.SetSendDiamond, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "1" });
                        NetMngr.GetSingleton().Send(InterfaceClub.UpdateManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 2, 1 });
					}
					else
					{
						// NetMngr.GetSingleton().Send(InterfaceClub.SetSendDiamond, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "0" });
                        NetMngr.GetSingleton().Send(InterfaceClub.UpdateManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 2, 0 });
					}
				}
			}

            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();

        });

        copyBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UniClipboard.SetText(kefu);
            ClubManager.GetSingleton().commonPopup.ShowView("复制成功");
        });

        settingBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().dissClubPopup.ShowView();
        });

        seeClubChengYuanConentBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine == 1 || isMine == 2)
            {
                ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMemberTopPanel);
            }
            else
            {
                ClubManager.GetSingleton().clubMember2TopPanel.panelType = 1;
                ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMember2TopPanel);

            }

        });
        seeClubChengYuanBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isMine==1||isMine==2)
            {
                ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMemberTopPanel);
            }
            else
            {
                ClubManager.GetSingleton().clubMember2TopPanel.panelType = 1;
                ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMember2TopPanel);

            }

        });

        seeClubSettingBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().setClubManagerTopPanel);
        });

        seeClubShareBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().friendCommissionPanel.nClubID = int.Parse(clubIdString.ToString());
         /* ClubManager.GetSingleton().friendCommissionPanel.bShowShare = true;
            ClubManager.GetSingleton().friendCommissionPanel.ToggleShare.gameObject.SetActive(false);
            ClubManager.GetSingleton().friendCommissionPanel.pageShare.SetActive(false);*/
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().friendCommissionPanel);
        });
        //add by yang yang 2021年3月23日 15:28:21
        seeCashOutBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubCashOutPanel);
        });
        seeClubMemberDiamonBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().sendMemberDiamondTopPanel);
        });

        seeClubMemberCoinBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().sendMemberCoinTopPanel);
        });

        seeClubUpdateBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubUpdateTopPanel);
        });

       seeSendCoinToggle.onValueChanged.AddListener((isOn) => {
           if (isOn)
           {
                sendCoinMember.SetActive(true);
               // ClubManager.GetSingleton().sendCoinTopPanel.ChangeType(1);
               // ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().sendCoinTopPanel);
           }
           else
           {
                sendCoinMember.SetActive(false);
               // ClubManager.GetSingleton().commonPopup.ShowView("当前没有赠送积分权限");
           }
       });

       seeSendDiamondToggle.onValueChanged.AddListener((isOn) => {
           if (isOn)
           {
                if(isMine == 1 || isMine == 2){
                    sendDiamondMember.SetActive(true);                    
                }
               // ClubManager.GetSingleton().sendCoinTopPanel.ChangeType(2);
               // ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().sendCoinTopPanel);
           }
           else
           {
                sendDiamondMember.SetActive(false);
               // ClubManager.GetSingleton().commonPopup.ShowView("当前没有赠送钻石权限");
           }
       });


        seeClubWelcomeBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().welcomeMemberTopPanel);
        });

      

        delGongGaoBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().commonPopup.ShowView("确定清除俱乐部公告？", null, true, () => {

                NetMngr.GetSingleton().Send(InterfaceClub.DelGongGao, new object[] { ClubManager.GetSingleton().clubPanel.clubId });

            });

        });

		exitBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			ClubManager.GetSingleton().commonPopup.ShowView("确定退出俱乐部？", null, true, () => {

				NetMngr.GetSingleton().Send(InterfaceClub.QuitClub, new object[] { ClubManager.GetSingleton().clubPanel.clubId });

			});


		});


		disBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			ClubManager.GetSingleton().commonPopup.ShowView("确定解散俱乐部？",null,true,()=> {

				NetMngr.GetSingleton().Send(InterfaceClub.DissClub,new object[] {ClubManager.GetSingleton().clubPanel.clubId });

			});


		});



        clubRefuseMsg.onValueChanged.AddListener((bool b) => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            int jujue, simi;
            jujue = clubRefuseMsg.isOn ? 1 : 0;
            simi = clubSelf.isOn ? 1 : 0;
            NetMngr.GetSingleton().Send(InterfaceClub.SetRefuseAndSelf, new object[] { ClubManager.GetSingleton().clubPanel.clubId, jujue, simi });

        });

        clubSelf.onValueChanged.AddListener((bool b) => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            int jujue, simi;
            jujue = clubRefuseMsg.isOn ? 1 : 0;
            simi = clubSelf.isOn ? 1 : 0;
            NetMngr.GetSingleton().Send(InterfaceClub.SetRefuseAndSelf, new object[] { ClubManager.GetSingleton().clubPanel.clubId, jujue, simi });

        });

        clubMyUnion = infoContent.Find("ClubMyUnion").gameObject;
        Button myUnionBtn = clubMyUnion.transform.Find("moreBtn").GetComponent<Button>();
        myUnionBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            // GameTools.GetSingleton().stopGameToolsAllCoroutines();
            // ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            UnionManager.GetSingleton().clubId = clubIdString;
            // UnionManager.GetSingleton().myUnionPanel.Show(clubIdString);
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().myUnionPanel);
        });

        clubUnionCoin = infoContent.Find("ClubUnionCoin").gameObject;
        Button unionCoinBtn = clubUnionCoin.transform.Find("moreBtn").GetComponent<Button>();
        clubUnionCoin.SetActive(false);
        unionCoinBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionCoinPanel);
        });

        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
    public void SetInfo(Hashtable data) {

        GameTools.GetSingleton().GetTextureNet(data["url"].ToString(), GetSprtie);

        clubName.text = data["clubName"].ToString();
        clubId.text = "ID:" + data["clubId"].ToString();
        clubIdString = data["clubId"].ToString();
        
        clubMemberCount.text = data["memberCount"].ToString();

        memberCount.text = data["memberCount"].ToString().Split('/')[0] + "人";

        clubjianjie.text = data["clubJianJie"].ToString();
        kefuWX.text = "客服:" + data["clubkefu"].ToString();
        kefu = data["clubkefu"].ToString();

        clubRefuseMsg.isOn = data["isRefuseMessage"].ToString() == "1" ? true : false;
        clubSelf.isOn = data["isSelf"].ToString() == "1" ? true : false;

        string[] tags = data["tag"].ToString().Split('|');
        if (tags.Length < 3)
        {
            tag.gameObject.SetActive(false);
            tag2.gameObject.SetActive(false);
            tag3.gameObject.SetActive(false);
            if (tags.Length == 1)
            {
                tag.gameObject.SetActive(true);
                tag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
            }
            if (tags.Length == 2)
            {
                tag.gameObject.SetActive(true);
                tag2.gameObject.SetActive(true);
                tag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
                tag2.transform.GetChild(0).GetComponent<Text>().text = tags[1];
            }
        }
        else
        {
            tag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
            tag2.transform.GetChild(0).GetComponent<Text>().text = tags[1];
            tag3.transform.GetChild(0).GetComponent<Text>().text = tags[2];
            tag.transform.GetChild(0).GetComponent<Text>().text = tags[0];
            tag2.transform.GetChild(0).GetComponent<Text>().text = tags[1];
            tag3.transform.GetChild(0).GetComponent<Text>().text = tags[2];
        }

        if (data["isMine"].ToString() == "1")
        {
            setting.SetActive(true);
            update.SetActive(true);
            welcome.SetActive(true);
            refuse.SetActive(true);
            delgonggao.SetActive(true);

            //add by yang yang 2021年3月22日 14:40:58  取消赠送
            // sendCoin.SetActive(false);
            // sendDiamond.SetActive(false);
            // sendDiamondMember.SetActive(false);
            sendCoin.SetActive(true);
            sendDiamond.SetActive(true);
            //end

           

            self.SetActive(true);
       //     sendDiamondMember.SetActive(true);
//			clubdis.SetActive(true);
			exitBtn.gameObject.SetActive(false);
			disBtn.gameObject.SetActive(true);
        
            isMine = 1;

        }
        else if (data["isMine"].ToString() == "2")
        {
            setting.SetActive(true);
            update.SetActive(true);
            welcome.SetActive(true);
            refuse.SetActive(true);
            delgonggao.SetActive(true);
            //add by yang yang 2021年3月22日 14:40:58  取消赠送
            sendCoin.SetActive(true);
            sendDiamond.SetActive(true);
            // sendDiamondMember.SetActive(false);
            //sendCoin.SetActive(true);
            //sendDiamond.SetActive(true);
            //end
            self.SetActive(true);
       //     sendDiamondMember.SetActive(true);
			exitBtn.gameObject.SetActive(true);
			disBtn.gameObject.SetActive(false);
            
            isMine = 2;

        }
        else
        {
            setting.SetActive(false);
            update.SetActive(false);
            welcome.SetActive(false);
            refuse.SetActive(false);
            delgonggao.SetActive(false);
            sendCoin.SetActive(false);
            sendDiamond.SetActive(false);
            self.SetActive(false);
            sendDiamondMember.SetActive(false);
			exitBtn.gameObject.SetActive(true);
			disBtn.gameObject.SetActive(false);
            
            isMine = 0;
        }


        for (int i = 0; i < memberHeadContent.childCount; i++)
        {
            memberHeadContent.GetChild(i).gameObject.SetActive(false);

        }

        ArrayList List = data["clubMemberList"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
            Debug.Log("俱乐部没人");
            return;
        }


        for (int i = 0; i < List.Count; i++)
        {
            if (i > 7)
            {
                break;
            }

            Hashtable ht = List[i] as Hashtable;
            memberHeadContent.GetChild(i).gameObject.SetActive(true);
            GameTools.GetSingleton().GetTextureNet(ht["headUrl"].ToString(), memberHeadContent.GetChild(i).GetComponent<HeadItem>().GetHead);

        }
    }
    public Hashtable ht=new Hashtable();
    public void GetClubInfoCallBack(Hashtable data) {
        ht = data;
        ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubInfoTopPanel);
        
        SetInfo(ht);

    }

    public void GetSprtie(Texture s) {
        clubHead.texture = s;
    }
    

    public override void OnAddComplete()
    {
     //   Waitting.getInstance().Show();
 //       SetInfo(ht);
    }

    public override void OnAddStart()
    {
    //    NetMngr.GetSingleton().Send(InterfaceClub.GetCoinContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    //    NetMngr.GetSingleton().Send(InterfaceClub.GetDiamondContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
        NetMngr.GetSingleton().Send(InterfaceClub.GetManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1});
        NetMngr.GetSingleton().Send(InterfaceClub.GetManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 2});
        NetMngr.GetSingleton().Send(InterfaceClub.GetClubUnion, new object[] { 
            ClubManager.GetSingleton().clubPanel.clubId});
    }

	public void GetCoinContentCallBack(Hashtable data) {

		if (data["isSend"].ToString() == "1")
		{
			seeSendCoinToggle.isOn = true;
            sendCoinMember.SetActive(true);
		}
		else
		{
			seeSendCoinToggle.isOn= false;
            sendCoinMember.SetActive(false);
		}

		initialSendCoin = seeSendCoinToggle.isOn;

	}

	public void GetDiamondContentCallBack(Hashtable data)
	{

		if (data["isSend"].ToString() == "1")
		{
			seeSendDiamondToggle.isOn = true;

            if(isMine == 1 || isMine == 2){
                sendDiamondMember.SetActive(true);
            }
		}
		else
		{
			seeSendDiamondToggle.isOn = false;
            sendDiamondMember.SetActive(false);
		}

		initialSendDiamond = seeSendDiamondToggle.isOn;


	}

    public void GetManualContentCallBack(Hashtable data){
        if(data["type"].ToString() == "1"){
            if(data["canSend"].ToString() == "1"){
                seeSendCoinToggle.isOn = true;
                sendCoinMember.SetActive(true);
            } else {
                seeSendCoinToggle.isOn = false;
                sendCoinMember.SetActive(false);   
            }
            initialSendCoin = seeSendCoinToggle.isOn;

        } else if(data["type"].ToString() == "2"){
            if(data["canSend"].ToString() == "1"){
                seeSendDiamondToggle.isOn = true;

                if(isMine == 1 || isMine == 2){
                    sendDiamondMember.SetActive(true);
                }
            } else {
                seeSendDiamondToggle.isOn = false;
                sendDiamondMember.SetActive(false);   
            }
            initialSendDiamond = seeSendDiamondToggle.isOn;
        }
    }

    public void SetUnionInfo(Hashtable data){
        if(data["success"].ToString() == "0"){
            clubMyUnion.transform.Find("moreBtn/Text").GetComponent<Text>().text = "";
            clubUnionCoin.SetActive(false);
            return;
        }
        ArrayList info = data["list"] as ArrayList;
        if(info.Count == 0){
            clubMyUnion.transform.Find("moreBtn/Text").GetComponent<Text>().text = "";
            clubUnionCoin.SetActive(false);
            return;
        }
        
        Hashtable h = info[0] as Hashtable;
        string unionId = h["lmId"].ToString();
        // UnionManager.GetSingleton().unionId = unionId;
        // StaticData.unionId = unionId;
        clubMyUnion.SetActive(true);
        clubMyUnion.transform.Find("moreBtn/Text").GetComponent<Text>().text = h["lmName"].ToString();
        if(h["lmType"].ToString() == "2"){
            clubUnionCoin.SetActive(true);
            int unionCoin = StaticData.unionCoin; // GameTools.GetSingleton().GetUnionCoin(unionId);
            clubUnionCoin.transform.Find("moreBtn/Text").GetComponent<Text>().text = unionCoin.ToString();
        }

    }

    public void RefreshUnionCoin(){
        int unionCoin = StaticData.unionCoin; // GameTools.GetSingleton().GetUnionCoin(UnionManager.GetSingleton().unionId);
        clubUnionCoin.transform.Find("moreBtn/Text").GetComponent<Text>().text = unionCoin.ToString();        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {
		ClubManager.GetSingleton().clubListPanel.GetClubList();
//        ClubManager.GetSingleton().clubPanel.GongGaoTog.isOn = true;
//        ClubManager.GetSingleton().clubPanel.toggleIndex = 1;
    }
}