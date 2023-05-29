using UnityEngine;
using System.Collections;
using System;

public class MessageManagerGame : MonoBehaviour
{
    public Queue queue = new Queue();
    private static MessageManagerGame _instance;

    public static MessageManagerGame GetSingleton()
    {
        return _instance;
    }

    bool isNetDown;
    bool canNetDown;
    float connecttime = 2.5f;

    public bool SetNetDown
    {
        set
        {
            isNetDown = value;
            if (value)
            {
                if (!canNetDown)
                {
                    canNetDown = true;
                    StartCoroutine(GetNetDown());
                }
            }
            else
            {
                StartCoroutine(GetNetDown());
            }
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        MAddListen();
    }

    //0  login  2 main  2 game
    public IEnumerator GetNetDown()
    {
        Debug.Log(isNetDown + "====");
        if (isNetDown)
        {
            Waitting.getInstance().Show();
            AddListen();
            switch (StaticData.selectScene)
            {
                case 0:
                    break;
                case 1:
                    NetMngr.GetSingleton().AddListener(InterfaceLogin.Login, (s) =>
                    {
                        Waitting.getInstance().Hide();
                        this.isNetDown = false;
                        canNetDown = false;
                        if (s["success"].ToString() == "0")
                        {
                            PopupCommon.GetSingleton().ShowView("请重新登录！", null, false,
                                delegate { StaticFunction.Getsingleton().ChangeScene("Login"); });
                            return;
                        }

                        if (s.Contains("userName"))
                        {
                            StaticData.username = s["userName"].ToString();
                        }
                    });
                    break;
                case 2:
                    NetMngr.GetSingleton().AddListener(InterfaceLogin.Login, (s) =>
                    {
                        Waitting.getInstance().Hide();
                        this.isNetDown = false;
                        canNetDown = false;
                        if (s["success"].ToString() == "0")
                        {
                            PopupCommon.GetSingleton().ShowView("请重新登录！", null, false,
                                delegate { StaticFunction.Getsingleton().ChangeScene("Login"); });
                            return;
                        }

                        if (s.Contains("userName"))
                        {
                            StaticData.username = s["userName"].ToString();
                        }

                        NetMngr.GetSingleton().Send(InterfaceGame.sendtotal, new object[] { });
                    });
                    break;
            }
        }

        while (isNetDown)
        {
            connecttime -= Time.deltaTime;
            //  Debug.Log(isNetDown+" "+StaticData.selectScene);
            if (connecttime <= 0 && isNetDown)
            {
                string longitude = GetGPS.GetSingleton().GetLongitude().ToString();
                string latitude = GetGPS.GetSingleton().GetLatitude().ToString();
                NetMngr.GetSingleton().Send(InterfaceLogin.Login, new object[]
                {
                    PlayerPrefs.GetString("userid"), PlayerPrefs.GetString("password"), PlayerPrefs.GetString("type"),
                    longitude, latitude
                });
                connecttime = 2.5f;
            }

            yield return null;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
       // if (StaticData.selectScene == 1)
        //{
            return;
        //}

        Debug.Log(focus + "===========================" + StaticData.selectScene);
        if (StaticData.selectScene <= 0) return;
        if(Paicontrol.GetInstance()!=null)
        Paicontrol.GetInstance().SmallRoundOver();
        //8-20  清空操作区域
		if (GameUIManager.GetSingleton ()._myController != null) {
			GameUIManager.GetSingleton()._myController.mytrunTransform.gameObject.SetActive(false);
			GameUIManager.GetSingleton()._myController.nomytrunTransform.gameObject.SetActive(false);
		}
     

        string a = focus ? "0" : "1";
        NetMngr.GetSingleton().Send(InterfaceGame.outOfApp, new object[] {a});
        if (focus)
        {
            AddListen();
            switch (StaticData.selectScene)
            {
                case 1:
                    NetMngr.GetSingleton().AddListener(InterfaceLogin.Login, (s) =>
                    {
                        if (s["success"].ToString() == "0")
                        {
                            PopupCommon.GetSingleton().ShowView("请重新登录！", null, false,
                                delegate { StaticFunction.Getsingleton().ChangeScene("Login"); });
                            return;
                        }

                        if (s.Contains("userName"))
                        {
                            StaticData.username = s["userName"].ToString();
                        }

                        Waitting.getInstance().Hide();
                        this.isNetDown = false;
                        canNetDown = false;
                    });
                    break;
                case 2:
                    NetMngr.GetSingleton().AddListener(InterfaceLogin.Login, (s) =>
                    {
                        if (s["success"].ToString() == "0")
                        {
                            PopupCommon.GetSingleton().ShowView("请重新登录！", null, false,
                                delegate { StaticFunction.Getsingleton().ChangeScene("Login"); });
                            return;
                        }

                        if (s.Contains("userName"))
                        {
                            StaticData.username = s["userName"].ToString();
                        }

                        Waitting.getInstance().Hide();
                        NetMngr.GetSingleton().Send(InterfaceGame.sendtotal, new object[] { });
                        this.isNetDown = false;
                        canNetDown = false;
                    });
                    break;
            }

            Debug.Log("切回来调用登陆。。。");
            string longitude = GetGPS.GetSingleton().GetLongitude().ToString();
            string latitude = GetGPS.GetSingleton().GetLatitude().ToString();
            NetMngr.GetSingleton().Send(InterfaceLogin.Login, new object[]
            {
                PlayerPrefs.GetString("userid"), PlayerPrefs.GetString("password"), PlayerPrefs.GetString("type"),
                longitude, latitude
            });
        }
    }

    public void AddListen()
    {
        MAddListen();
        MessageManagerClub.GetSingleton().AddListen();
        MessageManagerMain.GetSingleton().AddListen();
        MessageManagerLogin.GetSingleton().AddListen();
    }

    public void MAddListen()
    {
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerEnterRequest, OnDesktopPlayerEnterRequest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerEnterNotify, OnDesktopPlayerEnterNotify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerLeaveRequest, OnDesktopPlayerLeaveRequest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerSitdownRequest, OnDesktopPlayerSitdownRequest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerObRequest, OnDesktopPlayerObRequest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerWaitWhileNotify, OnDesktopPlayerWaitWhileNotify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerReturnRequest, OnDesktopPlayerReturnRequest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerReturnNotify, OnDesktopPlayerReturnNotify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.StartGameRequest, OnStartGameRequest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.StartGameNotify, OnStartGameNotify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerInfo, OnDesktopPlayerInfo);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerInfoToOthers, DesktopPlayerInfoToOthers);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopBankerInfo, OnDesktopBankerInfo);
        NetMngr.GetSingleton().AddListener(InterfaceGame.AddColdTime, OnAddColdTime);
        NetMngr.GetSingleton().AddListener(InterfaceGame.CardInfo, OnCardInfo);
        NetMngr.GetSingleton().AddListener(InterfaceGame.SomeOneTurn, OnSomeOneTurn);
        NetMngr.GetSingleton().AddListener(InterfaceGame.JiaZhuGengzhu, OnJiaZhuGengzhu);
        NetMngr.GetSingleton().AddListener(InterfaceGame.SomeOneXiaZhu, OnSomeOneXiaZhu);
        NetMngr.GetSingleton().AddListener(InterfaceGame.OperateType, OnOperateType);
        NetMngr.GetSingleton().AddListener(InterfaceGame.Winner, OnWinner);
        NetMngr.GetSingleton().AddListener(InterfaceGame.Pot, OnPot);
        NetMngr.GetSingleton().AddListener(InterfaceGame.Insurance, OnInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.InsuranceOP, OnInsuranceOP);
        NetMngr.GetSingleton().AddListener(InterfaceGame.UpdateInsurance, OnUpdateInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.Check, OnCheck);
        NetMngr.GetSingleton().AddListener(InterfaceGame.OneTurnFold, OnFold);
        NetMngr.GetSingleton().AddListener(InterfaceGame.RoomTimeOver, OnRoomTimeOver);
        NetMngr.GetSingleton().AddListener(InterfaceGame.BeOutOfDesk, OnBeOutOfDesk);
        NetMngr.GetSingleton().AddListener(InterfaceGame.BeOutOfRoom, OnBeOutOfRoom);
        NetMngr.GetSingleton().AddListener(InterfaceGame.CurrentZhanji, OnCurrentZhanji);
        NetMngr.GetSingleton().AddListener(InterfaceGame.rinfo, Onrinfo);//房间背景信息
        NetMngr.GetSingleton().AddListener(InterfaceGame.sendtotal, Onsendtotal);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBNotTurnFoldPass, OnKBFoldPass);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBJIFenPai, OnKBJIFenPaiPass);
        NetMngr.GetSingleton().AddListener(InterfaceGame.clickStartGame, OnclickStartGame);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerLeaveNotify, DesktopPlayerLeaveNotifyCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBDesktopPlayerObNotify, DesktopPlayerLeaveNotifyCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.keepSeat, keepSeatCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.tuoGuan, tuoGuanCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.tuoGuanNOtify, tuoGuanNOtify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.addCoin, addCoinCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.inRoom, inRoomCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.showpai, showpaiCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.setRoundDetail, OnsetRoundDetail);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getRoundDetail, OngetRoundDetail);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getRoomUserList, OngetRoomUserList);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getRoomUserDetails, OngetRoomUserDetails);
        NetMngr.GetSingleton().AddListener(InterfaceGame.managerUser, OnmanagerUser);
        NetMngr.GetSingleton().AddListener(InterfaceGame.roundReview, OnroundReview);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getRoundReviewMaxPage, OngetRoundReviewMaxPage); //add by yang yang 2021年4月23日 13:57:32
        NetMngr.GetSingleton().AddListener(InterfaceGame.getInsurancePeiLv, OngetPeilv);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getNowRecord, OngetNowRecord);//实时战报
        NetMngr.GetSingleton().AddListener(InterfaceGame.getMyInfo, OngetMyInfo);
        NetMngr.GetSingleton().AddListener(InterfaceGame.guessHandInfo, OnGuessHandInfo);
        NetMngr.GetSingleton().AddListener(InterfaceGame.guessBet, OnGuessBet);
        NetMngr.GetSingleton().AddListener(InterfaceGame.autoBet, OnAutoBet);
        NetMngr.GetSingleton().AddListener(InterfaceGame.receiveGuess, OnReceiveGuess);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getGuessRecord, OnGetGuessRecord);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getInsuranceData, OnGetInsuranceData);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getInsuranceTimeDiamond, OnGetInsuranceTimeDiamond);
        NetMngr.GetSingleton().AddListener(InterfaceGame.buyInsurance, OnBuyInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.buyInsuranceAddTime, OnBuyInsuranceAddTime);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBSomeOneNoTurn, OnNoSomeOneTurn);
        NetMngr.GetSingleton().AddListener(InterfaceGame.smallRoundEnd, smallRoundEndTurn);
        NetMngr.GetSingleton().AddListener(InterfaceGame.CancelInsurance, OnCancelInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.InsuranceTip, OnInsuranceTip);
        NetMngr.GetSingleton().AddListener(InterfaceGame.ToDealWith, OnToDealWith);
        NetMngr.GetSingleton().AddListener(InterfaceGame.isWaitAuthor, OnisWaitAuthor);
        NetMngr.GetSingleton().AddListener(InterfaceGame.isRootAgree, OnisRootAgree);
        NetMngr.GetSingleton().AddListener(InterfaceGame.GameMessageNotify, GameMessageNotify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.showLeftCards, showLeftCards);
        NetMngr.GetSingleton().AddListener(InterfaceGame.showLeftCardsM, showLeftCardsM);
        NetMngr.GetSingleton().AddListener(InterfaceGame.peekCards, peekCards);
        NetMngr.GetSingleton().AddListener(InterfaceGame.GetBaoBenDengLi, OnGetBaoBenDengLi);
        NetMngr.GetSingleton().AddListener(InterfaceGame.HandsCaresBiggest, HandsCaresBiggest);
        NetMngr.GetSingleton().AddListener(InterfaceGame.GetPaiJuInfo, OnGetPaiJuInfo);
        NetMngr.GetSingleton().AddListener(InterfaceGame.stopNextGame, OnstopNextGame);
        NetMngr.GetSingleton().AddListener(InterfaceGame.stopNextGameH, OnstopNextGameH);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DissolvePaiJu, OnDissolvePaiJu);
        NetMngr.GetSingleton().AddListener(InterfaceGame.DissolvePaiJuH, OnDissolvePaiJuH);
        NetMngr.GetSingleton().AddListener(InterfaceGame.SyncInsurance, OnSyncInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.SyncInsuranceN, OnSyncInsuranceN);
        NetMngr.GetSingleton().AddListener(InterfaceGame.GetInsuranceInitData, OnGetInsuranceInitData);
        NetMngr.GetSingleton().AddListener(InterfaceGame.LookInsurance, OnLookInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.HideLeftCards, HideLeftCards);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getGameReview, On_GameReview);
        NetMngr.GetSingleton().AddListener(InterfaceGame.InsuranceLater, OnInsuranceLater);
        NetMngr.GetSingleton().AddListener(InterfaceGame.cancelInsurance, OncancelInsurance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBAddColdTimeNotify, KBAddColdTimeNotify);
        NetMngr.GetSingleton().AddListener(InterfaceGame.setRoundDetailToAll, setRoundDetailToAll);
        NetMngr.GetSingleton().AddListener(InterfaceGame.setStartGameToAll, setStartGameToAll);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBDesktopPlayerCheck, KBDesktopPlayerCheck);
        NetMngr.GetSingleton().AddListener(InterfaceGame.forceToQuit, forceToQuit);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getGuessInfo, GetGuessInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceGame.NeedToAddmoney, NeedToAddmoneyCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.cancelAddMoney, cancelAddMoneyCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.qianZhu, qianZhuCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.maipai, maipaiCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBSidePot, KBSidePotCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.KBDesktopPlayerCanObRequest, KBDesktopPlayerCanObRequestCallback); 
        NetMngr.GetSingleton().AddListener("<coinFlyTocenter>", coinFlyTocenterBack); 
        NetMngr.GetSingleton().AddListener(InterfaceGame.DesktopPlayerInfoToOthersExpression, KBDesktopPlayerInfoToOthersExpressionCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.SendExperssion, SendExperssionCallback);

        NetMngr.GetSingleton().AddListener(InterfaceGame.sendChat, sendChatCallback);
        NetMngr.GetSingleton().AddListener(InterfaceGame.receiveChat, receiveChat);
        NetMngr.GetSingleton().AddListener(InterfaceGame.queryChatDiamond, queryChatDiamondCallback);

        NetMngr.GetSingleton().AddListener(InterfaceGame.SelectCard, selectBack);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getShowCard, getShowCard);

        NetMngr.GetSingleton().AddListener(InterfaceGame.receiveVoice, notifyReceiveVoice);

        NetMngr.GetSingleton().AddListener(InterfaceGame.PreBalance, OnPreBalance);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getRoundReviewUsers, OnGetRoundReviewUsers);
        NetMngr.GetSingleton().AddListener(InterfaceGame.getRoundReviewByPay, OnGetRoundReviewByPay);
                
    }

	public void selectBack(Hashtable h)
	{
		GameUIManager.GetSingleton ().onSelectCard (h);
	}
    public void getShowCard(Hashtable h)
    {
        Paicontrol.GetInstance().getShowCard( h);
    }
    public void coinFlyTocenterBack(Hashtable h)
    {

        Paicontrol.GetInstance().coinFlyTocenter726();
    }
    public void KBDesktopPlayerCanObRequestCallback(Hashtable h)
    {
        if (h["success"].ToString() == "1")
            PopupCommon.GetSingleton().ShowView("站起将直接弃牌，是否继续？", null, true, () =>
            {
                NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerObRequest, new object[] { });
                GameUIManager.GetSingleton().ptopleftpanel.gameObject.SetActive(false);
            });
        else
        {
            //Tip.Instance.showMsg("游戏未开始");
            NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerObRequest, new object[] { });
            GameUIManager.GetSingleton().ptopleftpanel.gameObject.SetActive(false);
        }
    }

    //表情 
    public void KBDesktopPlayerInfoToOthersExpressionCallback(Hashtable h)
    {
        GameUIManager.GetSingleton().setAnimojiInfo(h);
    }

    public void receiveChat(Hashtable h)
    {
        if(h["state"].ToString() == "0"){
            GameUIManager.GetSingleton().receiveChat(h);
        } else {
            PopupCommon.GetSingleton().ShowView(h["message"].ToString());
        }
    }
    
    public void SendExperssionCallback(Hashtable h)
    {
        GameUIManager.GetSingleton().setSendExperssion(h);
    }

    public void sendChatCallback(Hashtable h)
    {
        if(h["state"].ToString() == "0"){
            if(int.Parse(h["messageType"].ToString()) == 2){ // 语音发送成功,上传语音
                GameUIManager.GetSingleton().controlbtns.voiceButton.UploadVoice();
            }
        }
    }

    public void queryChatDiamondCallback(Hashtable h){
        GameUIManager.GetSingleton().chatPanel.setData(h);
        GameUIManager.GetSingleton().playerInfoPopup.setExpressionDiamond(h);
    }
    

    public void KBSidePotCallback(Hashtable h)
    {
        GameUIManager.GetSingleton().showBianchi(h);
    }

    public void maipaiCallback(Hashtable h)
    {
        GameUIManager.GetSingleton().someOneMaiPai(h);
    }

    public void qianZhuCallback(Hashtable h)
    {
        GameUIManager.GetSingleton().set627Dici(h);
    }

    private void OnInsuranceTip(Hashtable data)
    {
        if (data["type"].ToString() == "2") //无outs
        {
            Tip.Instance.showMsg("风险无需控制，继续发牌");
        }
        else if (data["type"].ToString() == "3") //两人或两人以上领先
        {
            Tip.Instance.showMsg("多名玩家领先，直接发牌");
        }
        else if (data["type"].ToString() == "4") //玩家不够买保险
        {
            Tip.Instance.showMsg("领先玩家选择直接发牌");
        }
        else if (data["type"].ToString() == "5") //保险未买中
        {
            Tip.Instance.showMsg("保险未买中，玩家被反超");
        }
        else if (data["type"].ToString() == "6") //未被反超
        {
            Tip.Instance.showMsg("玩家继续领先，结算时扣除保费");
        }
        else
        {
            GameUIManager.GetSingleton().insuranceInfoPopup.ShowView();
            GameUIManager.GetSingleton().insuranceInfoPopup.setInfo(data);
        }
    }

    public void cancelAddMoneyCallback(Hashtable h)
    {
        if (h["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(h["message"].ToString());
        }
    }

    public void NeedToAddmoneyCallback(Hashtable h)
    {
        GameUIManager.GetSingleton()._mjifenpai.ShowView();
        GameUIManager.GetSingleton()._mjifenpai.showInfo(-1);
    }

    private void GetGuessInfoCallBack(Hashtable data)
    {
        Tip.Instance.showMsg(data["message"].ToString());
    }

    private void forceToQuit(Hashtable data)
    {
        PlayerPrefs.DeleteAll();
        PopupCommon.GetSingleton().ShowView("您被请出游戏！", null, false,
            () => { StaticFunction.Getsingleton().ChangeScene("Login"); });
    }

    private void KBDesktopPlayerCheck(Hashtable data)
    {
        GameUIManager.GetSingleton().OthersWaitForAccessState(data);
    }

    private void setRoundDetailToAll(Hashtable data)
    {
        GameManager.GetSingleton().setNewParam(data);
    }
    private void setStartGameToAll(Hashtable data)
    {
          GameUIManager.GetSingleton().setNewTime(data);  
    }

    private void KBAddColdTimeNotify(Hashtable data)
    {
        GameUIManager.GetSingleton().SomeOneAddTime(data);
    }

    private void OncancelInsurance(Hashtable data)
    {
        GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
        GameUIManager.GetSingleton().insuranceInfoPopup.HideView();
    }

    private void OnInsuranceLater(Hashtable data)
    {
        GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
        GameUIManager.GetSingleton().insuranceInfoPopup.ShowView();
        GameUIManager.GetSingleton().insuranceInfoPopup.openBtn.gameObject.SetActive(false);
        GameUIManager.GetSingleton().insuranceInfoPopup.SetLaterData(data);
    }

    private void On_GameReview(Hashtable data)
    {
        if (Convert.ToInt32(data["success"]) == 1)
        {
            StartCoroutine(DownLoadReviewFile(data["url"].ToString()));
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private IEnumerator DownLoadReviewFile(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            if (!string.IsNullOrEmpty(www.error))
                PopupCommon.GetSingleton().ShowView(www.error);
            else
            {
                Debug.Log(www.text + "===");
                string[] strs = www.text.Split(new char[] {'\n'});
                StaticData.reviewData = strs;
                strs = null;
                Waitting.getInstance().Show();
                StaticData.isReplay = true;
                StaticFunction.Getsingleton().ChangeScene("Game");
            }
        }
    }

    private void HideLeftCards(Hashtable data)
    {
        GameUIManager.GetSingleton().btndashang.gameObject.SetActive(false);
    }


    private void OnLookInsurance(Hashtable data)
    {
        Debug.Log("推送观看保险按钮!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        StaticFunction.Getsingleton().isSync = false;
        GameUIManager.GetSingleton().insuranceInfoPopup.ShowView();
        GameUIManager.GetSingleton().insuranceInfoPopup.openBtn.gameObject.SetActive(true);
        GameUIManager.GetSingleton().insuranceInfoPopup.SetData(data);
    }

    private void OnGetInsuranceInitData(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            Debug.Log("观看其玩家购买保险~~~~~~~~~~~~~~~~~~~~~~~~~~");
            data.Add("isMine", "0");
            GameUIManager.GetSingleton().insurancePanel.SetData(data);
            //if (StaticFunction.Getsingleton().data!=null)
            //{
            //    GameUIManager.GetSingleton().insurancePanel.SyncInsurance(StaticFunction.Getsingleton().data);
            //    StaticFunction.Getsingleton().data = null;
            //}
            StaticFunction.Getsingleton().isSync = true;
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnSyncInsuranceN(Hashtable data)
    {
        Debug.Log("收到同步-------------------");
        StaticFunction.Getsingleton().data = data;
        if (GameUIManager.GetSingleton().insuranceInfoPopup.gameObject.activeSelf)
        {
            GameUIManager.GetSingleton().insuranceInfoPopup.timerConst = float.Parse(data["timer"].ToString());
            GameUIManager.GetSingleton().insuranceInfoPopup.timer = float.Parse(data["timer"].ToString());
        }

        if (GameUIManager.GetSingleton().insurancePanel.gameObject.activeSelf)
        {
            GameUIManager.GetSingleton().insurancePanel.timeconst = float.Parse(data["timer"].ToString());
            GameUIManager.GetSingleton().insurancePanel.timer = float.Parse(data["timer"].ToString());
        }
    }

    private void OnSyncInsurance(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            Debug.Log("同步到其他玩家====================");
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnDissolvePaiJuH(Hashtable data)
    {
        // PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        Tip.Instance.showMsg(data["message"].ToString());
    }

    private void OnDissolvePaiJu(Hashtable data)
    {
        if (data["success"].ToString() == "0")
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        else
            Tip.Instance.showMsg(data["message"].ToString());
    }

    private void OnstopNextGameH(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
    }

    private void OnstopNextGame(Hashtable data)
    {
        GameUIManager.GetSingleton().matchInfoPopup.HideView();
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        GameUIManager.GetSingleton().matchInfoPopup.image1.gameObject
            .SetActive(!GameUIManager.GetSingleton().matchInfoPopup.isStart);
        GameUIManager.GetSingleton().matchInfoPopup.image2.gameObject
            .SetActive(GameUIManager.GetSingleton().matchInfoPopup.isStart);
    }

    private void OnGetPaiJuInfo(Hashtable data)
    {
        GameUIManager.GetSingleton().managerSetPopup.GetFinished(data);
    }

    private void HandsCaresBiggest(Hashtable data)
    {
        GameUIManager.GetSingleton().setPaixingText(data["type"].ToString());
    }

    private void OnGetBaoBenDengLi(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            if (data["baoBen"].ToString() == "-1")
            {
                GameUIManager.GetSingleton().insurancePanel.btn1.gameObject.SetActive(false);
                GameUIManager.GetSingleton().insurancePanel.baoben = 0;
                GameUIManager.GetSingleton().insurancePanel.baobentext.text = "0";
            }
            else
            {
                GameUIManager.GetSingleton().insurancePanel.btn1.gameObject.SetActive(true);
                GameUIManager.GetSingleton().insurancePanel.baoben = int.Parse(data["baoBen"].ToString());
                GameUIManager.GetSingleton().insurancePanel.baobentext.text = data["baoBen"].ToString();
            }

            if (data["dengLi"].ToString() == "-1")
            {
                GameUIManager.GetSingleton().insurancePanel.btn2.gameObject.SetActive(false);
                GameUIManager.GetSingleton().insurancePanel.dengli = 0;
                GameUIManager.GetSingleton().insurancePanel.benlitext.text = "0";
            }
            else
            {
                GameUIManager.GetSingleton().insurancePanel.btn2.gameObject.SetActive(true);
                GameUIManager.GetSingleton().insurancePanel.dengli = int.Parse(data["dengLi"].ToString());
                GameUIManager.GetSingleton().insurancePanel.benlitext.text = data["dengLi"].ToString();
            }

            GameUIManager.GetSingleton().insurancePanel.odds = float.Parse(data["odds"].ToString());
            GameUIManager.GetSingleton().insurancePanel.peilv.text =
                GameUIManager.GetSingleton().insurancePanel.odds.ToString();
            GameUIManager.GetSingleton().insurancePanel.peifu.text = (Mathf.FloorToInt(
                float.Parse(GameUIManager.GetSingleton().insurancePanel.toubao.text) *
                float.Parse(GameUIManager.GetSingleton().insurancePanel.peilv.text))).ToString();

            //滑动条最大值赋值
            if (GameUIManager.GetSingleton().insurancePanel.baoxian.value == float.Parse(data["max"].ToString()))
            {
                GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue = float.Parse(data["max"].ToString());
                GameUIManager.GetSingleton().insurancePanel.baoxian.value = float.Parse(data["max"].ToString());
            }
            else
            {
                GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue = float.Parse(data["max"].ToString());
            }

            //组合牌型发起同步
            GameUIManager.GetSingleton().insurancePanel.pai = "";
            for (int i = 0; i < GameUIManager.GetSingleton().insurancePanel.selectedCardF.Count; i++)
            {
                GameUIManager.GetSingleton().insurancePanel.pai +=
                    GameUIManager.GetSingleton().insurancePanel.selectedCardF[i].id + "|";
            }

            if (GameUIManager.GetSingleton().insurancePanel.pai.Length != 0)
            {
                GameUIManager.GetSingleton().insurancePanel.pai = GameUIManager.GetSingleton().insurancePanel.pai
                    .Substring(0, GameUIManager.GetSingleton().insurancePanel.pai.Length - 1);
            }

            GameUIManager.GetSingleton().insurancePanel.pai += "&";
            for (int i = 0; i < GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Count; i++)
            {
                GameUIManager.GetSingleton().insurancePanel.pai +=
                    GameUIManager.GetSingleton().insurancePanel.selectedCardZ[i].id + "|";
            }

            if (GameUIManager.GetSingleton().insurancePanel.pai.Length != 0)
            {
                GameUIManager.GetSingleton().insurancePanel.pai = GameUIManager.GetSingleton().insurancePanel.pai
                    .Substring(0, GameUIManager.GetSingleton().insurancePanel.pai.Length - 1);
            }

            if (GameUIManager.GetSingleton().insurancePanel.isAllSe == "-1")
            {
                NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance,
                    new object[]
                    {
                        "-1", GameUIManager.GetSingleton().insurancePanel.baoxian.value.ToString(),
                        GameUIManager.GetSingleton().insurancePanel.peifu.text,
                        GameUIManager.GetSingleton().insurancePanel.toubao.text,
                        GameUIManager.GetSingleton().insurancePanel.peilv.text,
                        GameUIManager.GetSingleton().insurancePanel.timer.ToString(),
                        GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue.ToString()
                    });
            }
            else if (GameUIManager.GetSingleton().insurancePanel.isAllSe == "-2")
            {
                NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance,
                    new object[]
                    {
                        "-2", GameUIManager.GetSingleton().insurancePanel.baoxian.value.ToString(),
                        GameUIManager.GetSingleton().insurancePanel.peifu.text,
                        GameUIManager.GetSingleton().insurancePanel.toubao.text,
                        GameUIManager.GetSingleton().insurancePanel.peilv.text,
                        GameUIManager.GetSingleton().insurancePanel.timer.ToString(),
                        GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue.ToString()
                    });
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance,
                    new object[]
                    {
                        GameUIManager.GetSingleton().insurancePanel.pai,
                        GameUIManager.GetSingleton().insurancePanel.baoxian.value.ToString(),
                        GameUIManager.GetSingleton().insurancePanel.peifu.text,
                        GameUIManager.GetSingleton().insurancePanel.toubao.text,
                        GameUIManager.GetSingleton().insurancePanel.peilv.text,
                        GameUIManager.GetSingleton().insurancePanel.timer.ToString(),
                        GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue.ToString()
                    });
            }
        }
        else
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            Tip.Instance.showMsg(data["message"].ToString());
        }
    }

    private void peekCards(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
            Tip.Instance.showMsg(data["message"].ToString());
        }
    }

    private void showLeftCardsM(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
            Tip.Instance.showMsg(data["message"].ToString());
        }
    }

    private void showLeftCards(Hashtable data)
    {
        GameUIManager.GetSingleton().showDashang(data["coin"].ToString());
    }

    private void GameMessageNotify(Hashtable data)
    {
        if (StaticData.selectScene == 2)
            Tip.Instance.showMsg(data["message"].ToString());
    }

    private void OnisRootAgree(Hashtable data)
    {
        GameUIManager.GetSingleton().isStart = false;
        GameUIManager.GetSingleton().timer = 200;
        GameUIManager.GetSingleton().DairuTime.gameObject.SetActive(false);
        //PopupCommon.GetSingleton().ShowView();
        Tip.Instance.showMsg(data["message"].ToString());
        //同意授权带入

        GameUIManager.GetSingleton().WaitForAccess(data, false);
        if (data["agree"].ToString() == "1")
        {
        }
        else
        {
            GameManager.GetSingleton().roomFirstGetIn = true;
        }

        Debug.Log("roomFirstGetIn " + GameManager.GetSingleton().roomFirstGetIn);
    }

    private void OnisWaitAuthor(Hashtable data)
    {
        GameUIManager.GetSingleton().DairuTime.gameObject.SetActive(true);
        GameUIManager.GetSingleton().timerConst = int.Parse(data["keepTime"].ToString());
        GameUIManager.GetSingleton().timer = GameUIManager.GetSingleton().timerConst;
        GameUIManager.GetSingleton().isStart = true;
        Tip.Instance.showMsg(data["message"].ToString());
        //PopupCommon.GetSingleton().ShowView();
        GameUIManager.GetSingleton().WaitForAccess(data, true);
    }

    private void OnToDealWith(Hashtable data)
    {
        queue.Enqueue(data);
        print("收到授权" + "----" + queue.Count + StaticFunction.Getsingleton().isStart);
    }

    private void OnCancelInsurance(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
        }
        else
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            Tip.Instance.showMsg(data["message"].ToString());
        }
    }

    private void smallRoundEndTurn(Hashtable data)
    {
        Paicontrol.GetInstance().SmallRoundOver();
    }

    private void OnGetInsuranceData(Hashtable data)
    {
        data.Add("isMine", "1");
        GameUIManager.GetSingleton().planeManager.AddTopPlane(GameUIManager.GetSingleton().insurancePanel);
        GameUIManager.GetSingleton().insurancePanel.SetData(data);
    }

    private void OnGetInsuranceTimeDiamond(Hashtable data)
    {
        GameUIManager.GetSingleton().insurancePanel.curCoin.text = data["diamond"].ToString();
    }

    private void OnBuyInsurance(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            GameUIManager.GetSingleton().planeManager.RemoveTopPlane();
        }
        else
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            Tip.Instance.showMsg(data["message"].ToString());
        }
    }

    private void OnBuyInsuranceAddTime(Hashtable data)
    {
        //GameUIManager.GetSingleton().insurancePanel.daojishi.text = data["time"].ToString();
        if (data["success"].ToString() == "1")
        {
            GameUIManager.GetSingleton().insurancePanel.timeconst = float.Parse(data["time"].ToString());
            GameUIManager.GetSingleton().insurancePanel.timer = float.Parse(data["time"].ToString());
            GameUIManager.GetSingleton().insurancePanel.curCoin.text = data["delayMoney"].ToString();
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            Tip.Instance.showMsg(data["message"].ToString());
            NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance,
                new object[]
                {
                    GameUIManager.GetSingleton().insurancePanel.pai,
                    GameUIManager.GetSingleton().insurancePanel.baoxian.value.ToString(),
                    GameUIManager.GetSingleton().insurancePanel.peifu.text,
                    GameUIManager.GetSingleton().insurancePanel.toubao.text,
                    GameUIManager.GetSingleton().insurancePanel.peilv.text,
                    GameUIManager.GetSingleton().insurancePanel.timer.ToString(),
                    GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue.ToString()
                });
        }
        else
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            Tip.Instance.showMsg(data["message"].ToString());
        }
    }

    private void OnReceiveGuess(Hashtable data)
    {
        GameUIManager.GetSingleton().guessHandPopup.ReceiveGuessCallBack(data);
    }

    private void OnGetGuessRecord(Hashtable data)
    {
        GameUIManager.GetSingleton().guessRecordPanel.GuessRecordCallBack(data);
    }

    private void OnAutoBet(Hashtable data)
    {
        GameUIManager.GetSingleton().guessHandPopup.AutoBetCallBack(data);
    }

    private void OnGuessBet(Hashtable data)
    {
        GameUIManager.GetSingleton().guessHandPopup.GuessBetCallBack(data);
    }

    private void OnGuessHandInfo(Hashtable data)
    {
        GameUIManager.GetSingleton().guessHandPopup.GuessHandInfoCallBack(data);
    }

    private void OngetPeilv(Hashtable data)
    {
        GameUIManager.GetSingleton().insuranceRulePopup.GetInsurancePeiLvCallBack(data);
    }

    private void OngetMyInfo(Hashtable data)
    {
        GameUIManager.GetSingleton().otherPanel.GetMyInfoCallBack(data);
    }

    private void OngetNowRecord(Hashtable data)
    {
        GameUIManager.GetSingleton().nowRecordPanel.GetNowRecordCallBack(data);
    }

    private void OnroundReview(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            GameUIManager.GetSingleton().gameReviewPanel.ClearItem();
        }
        else
        {
            GameUIManager.GetSingleton().gameReviewPanel.RoundReviewCallBack(data);
        }
    }
    private void OngetRoundReviewMaxPage(Hashtable data)
    {
        GameUIManager.GetSingleton().gameReviewPanel.getRoundReviewMaxPage(data);
    }
    private void OnmanagerUser(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString());
    }

    private void OngetRoomUserDetails(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {
            if (data["success"].ToString() == "1")
            {
                GameUIManager.GetSingleton().playerInfoPopup.SetData(data);
            }
            else
            {
                PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            }
        }
    }

    private void OngetRoomUserList(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {
            if (data["success"].ToString() == "1")
            {
                GameUIManager.GetSingleton().playerManageListPopup.SetData(data);
            }
            else
            {
                PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            }
        }
    }

    private void OngetRoundDetail(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            GameUIManager.GetSingleton().matchInfoPopup.SetData(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void OnsetRoundDetail(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            GameUIManager.GetSingleton().managerSetPopup.Finished(data);
            //GameUIManager.GetSingleton().setNewTime(data);
        }
        else
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
    }

    private void showpaiCallback(Hashtable data)
    {
        Paicontrol.GetInstance().showPai(data);
    }

    private void inRoomCallback(Hashtable data)
    {
        if (StaticData.selectScene != 2)
            NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerEnterRequest,
                new object[] {data["roomID"].ToString()});
    }

    private void addCoinCallback(Hashtable data)
    {
        //PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        Tip.Instance.showMsg(data["message"].ToString());
    }

    private void tuoGuanCallback(Hashtable data)
    {
        if (data["success"].ToString() == "0")
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
    }

    private void tuoGuanNOtify(Hashtable data)
    {
        GameManager.GetSingleton().TuoGuan(data);
    }

    private void keepSeatCallback(Hashtable data)
    {
        //  if(data["success"].ToString()=="0")
       // PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        Tip.Instance.showMsg(data["message"].ToString());
    }

    private void DesktopPlayerLeaveNotifyCallback(Hashtable data)
    {
        GameManager.GetSingleton().ResponseLeave(data);
    }

    private void OnclickStartGame(Hashtable data)
    {

            GameUIManager.GetSingleton().HideBtnAndTips();
    }

    private void OnKBJIFenPaiPass(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
    }

    private void OnKBFoldPass(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
    }

    private void Onsendtotal(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false,
                delegate { StaticFunction.Getsingleton().ChangeScene("Main"); });
        }
        else
        {
            Waitting.getInstance().Hide();
        }
    }

    private void Onrinfo(Hashtable data)
    {
        GameManager.GetSingleton().setRoomInfo(data);
    }

    private void OnCurrentZhanji(Hashtable data)
    {
    }

    private void OnBeOutOfRoom(Hashtable data)
    {
        PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false,
            delegate { StaticFunction.Getsingleton().ChangeScene("Main"); });
    }

    private void OnBeOutOfDesk(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {
            //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
            Tip.Instance.showMsg(data["message"].ToString());
            GameUIManager.GetSingleton().setAllSeatEmpty();

            if (GameUIManager.GetSingleton().backGameTrans.gameObject.activeInHierarchy)
            {
                GameUIManager.GetSingleton().backGameTrans.gameObject.SetActive(false);
            }
        }
    }

    private void OnRoomTimeOver(Hashtable data)
    {
        if (data.Contains("message"))
        {
            if (data.Contains("Roomid"))
            {
                StaticData.isGameOverShowPanel = data["Roomid"].ToString();
            }

            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false,
                delegate { StaticFunction.Getsingleton().ChangeScene("Main"); });
        }
    }

    //  弃牌
    private void OnFold(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
    }

    // 看牌
    private void OnCheck(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
    }

    private void OnUpdateInsurance(Hashtable data)
    {
    }

    private void OnInsuranceOP(Hashtable data)
    {
    }

    private void OnInsurance(Hashtable data)
    {
    }

    private void OnPot(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {

        GameUIManager.GetSingleton().SetDichi(data);

        }
    }

    private void OnWinner(Hashtable data)
    {
        Paicontrol.GetInstance().ShowWinner(data);
    }

    private void OnOperateType(Hashtable data)
    {
    }

    private void OnSomeOneXiaZhu(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {
            GameUIManager.GetSingleton().somneOneXiaZhu(data);
        }
    }

    private void OnJiaZhuGengzhu(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
    }

    private void OnSomeOneTurn(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {
            GameUIManager.GetSingleton()._myController.OnSomeOneTurn(data);
        }
    }

    private void OnNoSomeOneTurn(Hashtable data)
    {
        if (GameUIManager.GetSingleton())
        {
            GameUIManager.GetSingleton()._myController.OnNoSomeOneTurn(data);
        }
    }

    private void OnCardInfo(Hashtable data)
    {
        Paicontrol.GetInstance().FaPaiDeskCenterLeft(data);
    }

    private void OnAddColdTime(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
        else
            GameUIManager.GetSingleton().setDelayTime(data);
    }

    private void OnDesktopBankerInfo(Hashtable data)
    {
        GameManager.GetSingleton().setZhuangInfo(data);
    }

    private void OnDesktopPlayerInfo(Hashtable data)
    {
        GameManager.GetSingleton().setPlayInfo(data);
    }

    private void DesktopPlayerInfoToOthers(Hashtable data)
    {
        GameManager.GetSingleton().setOthersInfo(data);
    }

    private void OnStartGameNotify(Hashtable data)
    {
        if (StaticData.selectScene == 2)
            GameUIManager.GetSingleton().GameStart(data);
    }

    private void OnStartGameRequest(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
        else
            GameUIManager.GetSingleton().HideBtnAndTips();
    }

    private void OnDesktopPlayerReturnNotify(Hashtable data)
    {
        GameManager.GetSingleton().BackToSeat(data);
    }

    private void OnDesktopPlayerReturnRequest(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
        else
        {
            if (GameUIManager.GetSingleton())
            {
                GameUIManager.GetSingleton().backGameTrans.gameObject.SetActive(false);
            }
        }

    }

    private void OnDesktopPlayerWaitWhileNotify(Hashtable data)
    {
        GameManager.GetSingleton().KeeySomeoneSeat(data);
    }

    private void OnDesktopPlayerObRequest(Hashtable data)
    {
        // if (data["success"].ToString() == "0")
        // {
      //  PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        Tip.Instance.showMsg(data["message"].ToString());
        //  GameUIManager.GetSingleton().setAllSeatEmpty();
        //}
        if (data["success"].ToString() == "1")
        {
            GameManager.GetSingleton().mysleftLeave();
        }
    }

    private void OnDesktopPlayerSitdownRequest(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
        else if (data["success"].ToString() == "2")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false, delegate
            {
                GameUIManager.GetSingleton()._mjifenpai.ShowView();
                GameUIManager.GetSingleton()._mjifenpai.showInfo(-1);
            });
        }
        else
        {
            GameManager.GetSingleton().roomFirstGetIn = false;
            if (data.Contains("leftMoney"))
                StaticData.gold = int.Parse(data["leftMoney"].ToString());
        }
    }

    private void OnDesktopPlayerLeaveRequest(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
        else
            StaticFunction.Getsingleton().ChangeScene("Main");
    }

    private void OnDesktopPlayerEnterNotify(Hashtable data)
    {
    }

    private void OnDesktopPlayerEnterRequest(Hashtable data)
    {
        if (data["success"].ToString() != "0")
        {
            StaticFunction.Getsingleton().ChangeScene("Game");
        }
        else
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
    }

    private void notifyReceiveVoice(Hashtable data){
        GameUIManager.GetSingleton().notifyPlayVoice(data);
    }

    private void OnPreBalance(Hashtable data)
    {
        if (data["success"].ToString() == "0")
        {
            PopupCommon.GetSingleton().ShowView(data["message"].ToString(), null, false);
        }
        else
            StaticFunction.Getsingleton().ChangeScene("Main");
    }

    private void OnGetRoundReviewUsers(Hashtable data){
        if (data["success"].ToString() == "1"){
            GameUIManager.GetSingleton().tousuPanel.RefreshUsers(data);
        }
    }

    private void OnGetRoundReviewByPay(Hashtable data){
        if (data["success"].ToString() == "1"){
            Tip.Instance.showMsg("投诉成功！"); 
            GameUIManager.GetSingleton().tousuPanel.HideView();
            GameUIManager.GetSingleton().gameReviewPanel.RoundReviewCallBack(data);
        }
    }


}