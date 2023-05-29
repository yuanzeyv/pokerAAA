using UnityEngine;
using System.Collections;

public class InterfaceGame : MonoBehaviour {

    //加入房间
    public const string DesktopPlayerEnterRequest = "g/KBDesktopPlayerEnterRequest";
    //通知加入房间
    public const string DesktopPlayerEnterNotify = "<KBDesktopPlayerEnterNotify>";
    //玩家离开桌子
    public const string DesktopPlayerLeaveRequest = "g/KBDesktopPlayerLeaveRequest"; 
    public const string DesktopPlayerLeaveNotify = "<KBDesktopPlayerLeaveNotify>";
    //玩家坐下
    public const string DesktopPlayerSitdownRequest = "g/KBDesktopPlayerSitdownRequest";
    //玩家观战
    public const string DesktopPlayerObRequest = "g/KBDesktopPlayerObRequest";
    //推送玩家暂离
    public const string DesktopPlayerWaitWhileNotify = "<KBDesktopPlayerWaitWhileNotify>";
    //玩家继续
    public const string DesktopPlayerReturnRequest = "g/KBDesktopPlayerReturnRequest";
    //推送某玩家继续
    public const string DesktopPlayerReturnNotify = "<KBDesktopPlayerReturnNotify>";
    //开始游戏
    public const string StartGameRequest = "g/KBStartGameRequest";
    //通知开始游戏
    public const string StartGameNotify = "<KBStartGameNotify>";
    //推送玩家信息
    public const string DesktopPlayerInfo = "<KBDesktopPlayerInfo>";
    public const string DesktopPlayerInfoToOthers = "<KBDesktopPlayerInfoToOthers>";
    //推送庄家信息
    public const string DesktopBankerInfo = "<KBDesktopBankerInfo>";
    //延迟倒计时
    public const string AddColdTime = "g/KBAddColdTime";
    //发牌
    public const string CardInfo = "<KBCardInfo>";
    //轮到某人操作
    public const string SomeOneTurn = "<KBSomeOneTurn>";
    //加注/跟注
    public const string JiaZhuGengzhu = "g/KBJiaZhuGengzhu";
    //推送某人下注量
    public const string SomeOneXiaZhu = "<KBSomeOneXiaZhu>";
    //操作类型推送
    public const string OperateType = "<KBOperateType>";
    //胜者推送
    public const string Winner = "<KBWinner>";
    //底池推送
    public const string Pot = "<KBPot>";
    //保险推送
    public const string Insurance = "<KBInsurance>";
    //保险操作
    public const string InsuranceOP = "g/KBInsurance";
    //刷新保险推送
    public const string UpdateInsurance = "g/KBUpdateInsurance";
    //看牌
    public const string Check = "g/KBCheck";
    //弃牌
    public const string OneTurnFold = "g/KBFold";
    //时间到牌局结束房间解散
    public const string RoomTimeOver = "<RoomTimeOver>";
    //被强行离开座位
    public const string BeOutOfDesk = "<BeOutOfDesk>";
    public const string BeOutOfRoom = "<BeOutOfRoom>";
    //获取实时战绩
    public const string CurrentZhanji = "g/getCurrentZhanji";
    public const string rinfo = "<kbRinfoNotify>";
    public const string sendtotal = "g/kbSendtoal";
    public const string KBNotTurnFoldPass="g/KBFoldPass";
    public const string KBJIFenPai = "g/KBgetJiFenPai";
    public const string clickStartGame = "<clickStartGame>";
    public const string KBDesktopPlayerObNotify = "<KBDesktopPlayerObNotify>";
    public const string keepSeat = "g/startUpKeeySeat";
    public const string tuoGuan = "g/tuoGuan";
    public const string tuoGuanNOtify = "<tuoGuan>";
    public const string addCoin = "g/KBAddCoin";
    public const string inRoom = "<inRoom>";
    public const string showpai = "<ShowPai>";
    public const string outOfApp = "a/outOfApp";
    public const string KBSomeOneNoTurn = "<KBSomeOneNoTurn>";
    public const string smallRoundEnd = "<smallRoundEnd>"; 
    public const string GameMessageNotify = "<GameMessageNotify>";
    public const string showLeftCards = "<showLeftCards>"; 
    public const string peekCards = "g/peekCards";
    public const string showLeftCardsM = "g/showLeftCards";
    public const string HandsCaresBiggest = "<HandsCaresBiggest>";
    public const string HideLeftCards = "<HideLeftCards>";
    public const string KBAddColdTimeNotify = "<KBAddColdTime>";
    public const string getGameReview = "g/gameReview";
    public const string showMyCardsTypes = "<showMyCardsTypes>";
    public const string setRoundDetailToAll = "<setRoundDetailToAll>";
    public const string setStartGameToAll = "<setStartGameToAll>";
    public const string KBDesktopPlayerCheck = "<KBDesktopPlayerCheck>";
    public const string forceToQuit = "<forceToQuit>";
    public const string NeedToAddmoney = "<NeedToAddmoney>"; 
    public const string cancelAddMoney = "g/cancelAddMoney";
    public const string qianZhu = "<qianZhu>";
    public const string maipai = "<maipai>";  
    public const string KBSidePot = "<KBSidePot>";
    public const string KBDesktopPlayerCanObRequest = "g/KBDesktopPlayerCanObRequest";
    //俱乐部管理员功能
    public const string setRoundDetail = "g/setRoundDetail";
    //牌局信息
    public const string getRoundDetail = "g/getRoundDetail";
    //玩家管理列表
    public const string getRoomUserList = "g/getRoomUserList";
    //获取玩家详情
    public const string getRoomUserDetails = "g/getRoomUserDetails";
    //玩家管理
    public const string managerUser = "g/managerUser";
    //牌局回顾
    public const string roundReview = "g/roundReview";

    public const string getRoundReviewMaxPage = "g/getRoundReviewMaxPage";
    //获取保险赔率
    public const string getInsurancePeiLv = "g/getInsurancePeiLv";
    //实施战报
    public const string getNowRecord = "g/getNowRecord";
    //获取我的信息
    public const string getMyInfo = "g/getMyInfo";
    //获取猜手牌信息
    public const string guessHandInfo = "g/guessHandInfo";
    //下注猜手牌
    public const string guessBet = "g/guessBet";
    //自动追
    public const string autoBet = "g/autoBet";
    //玩家最新记分牌推送
    public const string receiveGuess = "<PlayerGuess>";
    //获取猜手牌记录
    public const string getGuessRecord = "g/getGuessRecord";
    //获取猜手牌信息
    public const string getGuessInfo = "<getGuessInfo>";

   

    //获取保险数据
    public const string getInsuranceData = "<Insurance>";
    //获取保险加时需要的钻石
    public const string getInsuranceTimeDiamond = "g/YGGetInsuranceTimeDiamond";
    //购买保险
    public const string buyInsurance = "g/YGBuyInsurance";
    //后买保险时间加时
    public const string buyInsuranceAddTime = "g/YGBuyInsuranceAddTime";
    //取消保险
    public const string CancelInsurance = "g/YGCancelInsurance";
    //保险提示
    public const string InsuranceTip = "<InsuranceTip>";
    //推送处理授权带入给管理员
    public const string ToDealWith = "<ToDealWith>";

    //给玩家等待授权时间推送
    public const string isWaitAuthor = "<isWaitAuthor>";
    //管理员是否同意 （推送给请求的玩家）
    public const string isRootAgree = "<isRootAgree>";

    //推送保本等利
    public const string GetBaoBenDengLi = "g/YGGetBaoBenDengLi";
    //暂停比赛
    public const string stopNextGame = "g/stopNextGame";
    //暂停比赛
    public const string stopNextGameH = "<stopNextGame>";
    //解散当前牌局
    public const string DissolvePaiJu = "g/YGDissolvePaiJu";
    //解散当前牌局
    public const string DissolvePaiJuH = "<DissolvePaiJu>";
    //获取牌局信息
    public const string GetPaiJuInfo = "g/YGGetPaiJuInfo";
    //保险同步
    public const string SyncInsurance = "g/YGSyncInsurance";
    //保险同步
    public const string SyncInsuranceN = "<SyncInsurance>";
    //其他玩家打开保险页面初始化保险数据
    public const string GetInsuranceInitData = "g/YGGetInsuranceInitData";
    //推送观看买保险
    public const string LookInsurance = "<LookInsurance>";
    //推送其他玩家保险购买完成
    public const string InsuranceLater = "<InsuranceLater>";
    //通知其他玩家取消保险
    public const string cancelInsurance = "<cancelInsurance>";  

    public const string coinFlyTocenter = "<coinFlyTocenter>﻿";

	//名单查询
	public const string SelectCard = "g/KBWait";
	public const string OnSelectCard = "g/KBsetPublicCard";

    //结束亮牌
    public const string getShowCard = "g/getShowCard";
    //add by yang yang 2021年4月2日 18:21:32 
    public const string SendExperssion = "g/sendExpression";
    public const string DesktopPlayerInfoToOthersExpression = "<KBDesktopPlayerInfoToOthersExpression>";

    public const string sendChat = "g/sendChat";//发送聊天
    public const string receiveChat = "<KBReceiveChat>";//收到聊天
    
    public const string queryChatDiamond = "g/sendChatDiamond";//查询聊天消耗

    public const string playVoice = "g/playVoice";//播放语音
    public const string receiveVoice = "<KBPlayVoice>";//收到语音通知

    public const string PreBalance = "g/closeCoin"; // 提前结算
    public const string getRoundReviewUsers = "g/getRoundReviewUserList"; // 获取当前回合玩家
    public const string getRoundReviewByPay = "g/roundReviewByPay"; // 付费查看玩家底牌
    

}
