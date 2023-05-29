using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class MessageManagerMain : MonoBehaviour {

    private static MessageManagerMain _instance;
    public static MessageManagerMain GetSingleton()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        AddListen();
    }

    public void AddListen()
    {
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetGameRecordTotal, OnGetGameRecordTotal);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetGameRecordDetail, OnGetGameRecordDetail);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetGameData, OnGetGameData);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GamePaiReview, OnGamePaiReview);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GamePai, OnGamePai);
        NetMngr.GetSingleton().AddListener(InterfaceMain.CreateGame, OnCreateGame);
		NetMngr.GetSingleton().AddListener(InterfaceMain.CreateAmhGame, OnCreateGame);
        NetMngr.GetSingleton().AddListener(InterfaceMain.CreateShortGame, OnCreateGame);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetGameInfo, OnGetGameInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.ScreenMatch, OnGetGameInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetUpdatePar, OnGetUpdatePar);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetPlayerInfo, OnGetPlayerInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.ModifierName, OnModifierName);
        NetMngr.GetSingleton().AddListener(InterfaceMain.ModifierSex, OnModifierSex);
        NetMngr.GetSingleton().AddListener(InterfaceMain.ModifierSignature, OnModifierSignature);
        NetMngr.GetSingleton().AddListener(InterfaceMain.RequestUpDataUrl, OnRequestUpDataUrl);
        NetMngr.GetSingleton().AddListener(InterfaceMain.UpdateHead, OnUpdateHead);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetShopInfo, OnGetShopInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.BuyDiamond, OnBuyDiamond);
        NetMngr.GetSingleton().AddListener(InterfaceMain.BuyGold, OnBuyGold);
        NetMngr.GetSingleton().AddListener(InterfaceMain.UpdateGoldDiamond, OnUpdateGoldDiamond);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetMyselfInfo, OnGetMyselfInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetOpponentInfo, OnGetOpponentInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetAllinInfo, OnGetAllinInfo);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetPaiData, OnGetPaiData);
        NetMngr.GetSingleton().AddListener(InterfaceMain.BlindPhone, OnBlindPhone);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetMyPaiData, OnGetMyPaiData);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetServices, OnGetServices);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetFriendList, OnGetFriendList);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetFriendDetail, OnGetFriendDetail);
        NetMngr.GetSingleton().AddListener(InterfaceMain.DeleteFriend, OnDeleteFriend);
        NetMngr.GetSingleton().AddListener(InterfaceMain.SearchPlayer, OnSearchPlayer);
        NetMngr.GetSingleton().AddListener(InterfaceMain.AddFriend, OnAddFriend);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetNotice, OnGetNotice);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetMessage, OnGetMessage);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetNoticeContent, OnGetNoticeContent);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetNoticelist, OnGetNoticelist);
        NetMngr.GetSingleton().AddListener(InterfaceMain.IsAgree, OnIsAgree);
        NetMngr.GetSingleton().AddListener(InterfaceMain.IsClubAgree, OnIsClubAgree);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetClubMessage, OnGetClubMessage);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetRealTimeMessage, OnGetRealTimeMessage);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetPaiMessage, OnGetPaiMessage);
        NetMngr.GetSingleton().AddListener(InterfaceMain.IsAgreeJoinPai, OnIsAgreeJoinPai);
        NetMngr.GetSingleton().AddListener(InterfaceMain.SendAgreement, OnSendAgreement);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetInsuranceDetail, OnGetInsuranceDetail);
        NetMngr.GetSingleton().AddListener(InterfaceMain.GetRoundOld, OnGetRoundOld);
        NetMngr.GetSingleton().AddListener(InterfaceMain.ModifyFriendRemark, OnModifyFriendRemark);

    }

    private void OnSendAgreement(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(),null,false,delegate { HallManager.GetSingleton().planeManager.RemoveTopPlane(); });
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnIsAgreeJoinPai(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
			if (SceneManager.GetActiveScene().name == "Main")
			{
				HallManager.GetSingleton().paiMsgTopPanel.IsAgreeFinished(data);
			}

			if (SceneManager.GetActiveScene().name == "Game")
			{
				GameUIManager.GetSingleton().paiMsgTopPanel.IsAgreeFinished(data);
			}
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetPaiMessage(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            if (data["type"].ToString()=="1")
            {
				if (SceneManager.GetActiveScene().name == "Main")
				{
					HallManager.GetSingleton().paiMsgTopPanel.SetData(data);
				}

				if (SceneManager.GetActiveScene().name == "Game")
				{
					GameUIManager.GetSingleton().paiMsgTopPanel.SetData(data);
				}
               
            }
            else
            {
				if (SceneManager.GetActiveScene().name == "Main")
				{
					HallManager.GetSingleton().paiMsgTopPanel.SetData2(data);
				}

				if (SceneManager.GetActiveScene().name == "Game")
				{
					GameUIManager.GetSingleton().paiMsgTopPanel.SetData2(data);
				}

            }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetRealTimeMessage(Hashtable data)
    {
        StaticData.MyMessage = data["count"].ToString();
        if (data.ContainsKey("paiCount"))
        {
            StaticData.PaiMessage = data["paiCount"].ToString();
        }
        switch (data["type"].ToString())
        {
            case "1":
                if (StaticData.isinform)
                {
                    SoundManager.GetSingleton().PlaySound("Audio/notice");
                }
                break;
            case "2":
                if (StaticData.isinform)
                {
                    SoundManager.GetSingleton().PlaySound("Audio/notice");
                }
                break;
            case "3":
                if (StaticData.isinform)
                {
                    SoundManager.GetSingleton().PlaySound("Audio/notice");
                }
                break;
            case "4":
                if (StaticData.isinform)
                {
                    SoundManager.GetSingleton().PlaySound("Audio/notice");
                }
                break;
            case "5":
                if (StaticData.isinform)
                {
                    SoundManager.GetSingleton().PlaySound("Audio/notice");
                }
                break;
        }
        if (HallManager.GetSingleton() != null)
        {
            HallManager.GetSingleton().showRed();
            HallManager.GetSingleton().personalCenterBottomPanel.showRed();        
        }
        if(ClubManager.GetSingleton() != null){
            ClubManager.GetSingleton().clubListPanel.showRed();    
            ClubManager.GetSingleton().clubPanel.showRed();    
            ClubManager.GetSingleton().myClubPanel.showRed();    
        }
       
    }

    private void OnGetClubMessage(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myMsgTopPanel.SetClubFriendData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnIsClubAgree(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myMsgTopPanel.IsClubAgreeFinished(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnIsAgree(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myMsgTopPanel.IsAgreeFinished(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetNoticelist(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().noticeListContentTopPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetNoticeContent(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().noticeContentTopPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetMessage(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            if (data["type"].ToString()=="1"||data["type"].ToString()=="2")
            {
                HallManager.GetSingleton().myMsgTopPanel.SetClubFriendData(data);
            }
            else
            {
                HallManager.GetSingleton().myMsgTopPanel.SetMsgData(data);
            }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetNotice(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().noticeListBottomPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnAddFriend(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().addFriendInfoTopPanel.AddFriendFinished(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnSearchPlayer(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            if (data["type"].ToString()=="2")
            {
                HallManager.GetSingleton().addFriendTopPanel.SetData(data);
            }
            else
            {
                HallManager.GetSingleton().friendTopPanel.SetData(data);
            }
        }
        else
        {

        }
        
    }

    private void OnDeleteFriend(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().friendDetailTopPanel.DeleteFriendFinished(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetFriendDetail(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().friendDetailTopPanel.SetFriendData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetFriendList(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().friendTopPanel.SetFriendData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetServices(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().servicesTopPanel.SetServicesFinished(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetMyPaiData(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myPaiPuTopPanel.SetFriendData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnBlindPhone(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(),null,false,delegate { HallManager.GetSingleton().planeManager.RemoveTopPlane(); NetMngr.GetSingleton().Send(InterfaceMain.GetPlayerInfo, new object[] { }); });
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetPaiData(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myDataTopPanel.SetPaiData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetAllinInfo(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myDataTopPanel.SetAllinInfo(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetOpponentInfo(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myDataTopPanel.SetGetOpponentInfo(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetMyselfInfo(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            HallManager.GetSingleton().myDataTopPanel.SetMyselfInfo(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnUpdateGoldDiamond(Hashtable data)
    {
        StaticData.diamond = int.Parse(data["diamond"].ToString());
        StaticData.gold = int.Parse(data["gold"].ToString());
    }

    private void OnBuyGold(Hashtable data)
    {
        
        if (data["success"].ToString() == "1")
        {
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().shopGoldTopPanel.BuyGoldFinished();
                Tip.Instance.showMsg(data["message"].ToString());
            }
            else {
                GameUIManager.GetSingleton().shopInGame.transform.GetChild(1).GetComponent<ShopGoldTopPanel>().BuyGoldFinished();
                Tip.Instance.showMsg(data["message"].ToString());
            }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnBuyDiamond(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().shopDiamondTopPanel.BuyDiamondFinished(data);
            }
            else {
                GameUIManager.GetSingleton().shopInGame.transform.GetChild(0).GetComponent<ShopDiamondTopPanel>().BuyDiamondFinished(data);
            }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetShopInfo(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            if (data["type"].ToString()=="1")
            {
                if (HallManager.GetSingleton() != null)
                {
                    HallManager.GetSingleton().shopGoldTopPanel.GoldFinished(data);
                }
                else {
                    GameUIManager.GetSingleton().shopInGame.transform.GetChild(1).GetComponent<ShopGoldTopPanel>().GoldFinished(data);    
                }
            }
            else
            {
                if (HallManager.GetSingleton() != null)
                {
                    HallManager.GetSingleton().shopDiamondTopPanel.DiamondFinished(data);
                }
                else
                {
                    GameUIManager.GetSingleton().shopInGame.transform.GetChild(0).GetComponent<ShopDiamondTopPanel>().DiamondFinished(data);
                }
            }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnUpdateHead(Hashtable data)
    {

    }

    private void OnRequestUpDataUrl(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().selectPhotoPopup.SetRequestUrl(data);
            StaticData.imgUrl = data["url"].ToString();
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnModifierSignature(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            HallManager.GetSingleton().signatureTopPanel.ModifierSignatureFinished();
            HallManager.GetSingleton().personalInfoTopPanel.SetSignature(data["str"].ToString());
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnModifierSex(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            HallManager.GetSingleton().planeManager.RemoveSidePlane();
            HallManager.GetSingleton().personalInfoTopPanel.SetSex(data["sex"].ToString());
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnModifierName(Hashtable data)
    {
        //会有BUG
        if (data["success"].ToString()=="1")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(),null,false,delegate 
            {
                HallManager.GetSingleton().planeManager.RemoveTopPlane();
            });
            HallManager.GetSingleton().personalInfoTopPanel.SetName(data["name"].ToString());
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetPlayerInfo(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().personalCenterBottomPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetGameRecordTotal(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().scoreBottomPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnGetGameRecordDetail(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().scoreDetailTopPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
        StaticData.isGameOverShowPanel = "";
    }
    private void OnGetGameData(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().scoreDetailTopPanel.SetPaiData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }
    private void OnGamePaiReview(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().theGamePaiRecordTopPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }
    private void OnGamePai(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().theGamePaiTopPanel.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }
    private void OnCreateGame(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerEnterRequest,new object[] { data["roomId"].ToString()});

            //记录一下俱乐部id 用于 下次进入大厅 
//            StaticData.clubId = ClubManager.GetSingleton().clubPanel.clubId;
//            StaticData.clubName = ClubManager.GetSingleton().clubPanel.clubName.text;

        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }
    private void OnGetGameInfo(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().hallBottomPanel.SetHallData(data);
        }
        else
        {
            if (data["message"].ToString()=="暂无更多数据")
            {
                if (HallManager.GetSingleton().hallBottomPanel.currPage==1)
                {
                    PopupCommon.GetSingleton().ShowView("暂无牌局");
                }
                else
                {
                    PopupCommon.GetSingleton().ShowView("暂无更多牌局");
                }
            }
            else
            {
                PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            }
        }
    }
    private void OnGetUpdatePar(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            string type = data["type"].ToString();
            switch (type)
            {
                case "1":
                    HallManager.GetSingleton().createGamePopup.CostDiamondCountFinished(data["par"].ToString());
                    break;
                case "2":
                    break;
                case "3":
                    HallManager.GetSingleton().nameTopPanel.SetData(data["par"].ToString(),StaticData.diamond.ToString());
                    break;
			case "4":
					string[] strs = data ["par"].ToString ().Split ('|');
					string[] strs2 = data ["before"].ToString ().Split (',');

					StaticData.littlemangs = strs;
					StaticData.noteBeforeTip = strs2;
//					HallManager.GetSingleton ().createGamePopup.littlemangs = strs;
//					HallManager.GetSingleton ().createGamePopup.noteBeforeTip = strs2;
                    break;
                case "5":
                    GameUIManager.GetSingleton().managerSetPopup.SetData(data["par"].ToString());
                    break;
            }
        }
        else
        {

        }
    }

    // 获取保险明细
    private void OnGetInsuranceDetail(Hashtable data){
        HallManager.GetSingleton().insuranceDetailPanel.SetInfo(data);
    }

    // 获得上局牌局设置
    private void OnGetRoundOld(Hashtable data){
        HallManager.GetSingleton().createGamePopup.Setting(data);            
    }

    // 修改好友备注
    private void OnModifyFriendRemark(Hashtable data){
        Tip.Instance.showMsg(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        }
    }

}
