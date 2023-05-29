using UnityEngine;
using System.Collections;

public class InterfaceMain : MonoBehaviour {

    //获取总战绩
    public const string GetGameRecordTotal = "g/YGGetGameRecordTotal";
    //单局战绩详情
    public const string GetGameRecordDetail = "g/YGGetGameRecordDetail";
    //单局战绩数据
    public const string GetGameData = "g/YGGetGameData";
    //本局手牌回顾
    public const string GamePaiReview = "g/YGGamePaiReview";
    //本局手牌
    public const string GamePai = "g/YGGamePai";
    //创建牌局
    public const string CreateGame = "a/YGCreateGame";
	//创建奥马哈牌局
	public const string CreateAmhGame = "a/YGCreateErMaHaGame";
    //创建短牌
    public const string CreateShortGame = "a/YGCreateShortGame";
    //获取大厅牌局信息
    public const string GetGameInfo = "a/YGGetGameInfo";
	//public const string GetGameInfo = "a/YGGetAllGame";
    //获取实时更新的参数
    public const string GetUpdatePar = "g/YGGetUpdatePar";
    //获取个人中心信息
    public const string GetPlayerInfo = "g/YGGetPlayerInfo";
    //修改昵称
    public const string ModifierName = "g/YGModifierName";
    //筛选牌局
    public const string ScreenMatch = "a/YGScreenMatch";
    //修改性别
    public const string ModifierSex = "g/YGModifierSex";
    //修改个性签名
    public const string ModifierSignature = "g/YGModifierSignature";
    //获取文件上传HTTP地址
    public const string RequestUpDataUrl = "g/YGRequestUpDataUrl";
    //修改图像
    public const string UpdateHead = "g/YGUpdateHead";
    //获取商城信息
    public const string GetShopInfo = "g/YGGetShopInfo";
    //购买钻石
    public const string BuyDiamond = "g/YGBuyDiamond";
    //购买积分
    public const string BuyGold = "g/YGBuyGold";
    //更新积分钻石
    public const string UpdateGoldDiamond = "<UpdateGoldDiamond>";
    //个人数据信息获取
    public const string GetMyselfInfo = "g/YGGetMyselfInfo";
    //获取对手数据信息
    public const string GetOpponentInfo = "g/YGGetOpponentInfo";
    //Allin
    public const string GetAllinInfo = "g/YGGetAllinInfo";
    //获取牌组数据
    public const string GetPaiData = "g/YGGetPaiData";
    //绑定手机号
    public const string BlindPhone = "g/YGBlindPhone";
    //我的牌谱
    public const string GetMyPaiData = "g/YGGetMyPaiData";
    //联系客服
    public const string GetServices = "g/YGGetServices";
    //获取好友列表
    public const string GetFriendList = "g/YGGetFriendList";
    //获取好友详情信息
    public const string GetFriendDetail = "g/YGGetFriendDetail";
    //删除好友
    public const string DeleteFriend = "g/YGDeleteFriend";
    //搜索玩家
    public const string SearchPlayer = "g/YGSearchPlayer";
    //添加好友
    public const string AddFriend = "g/YGAddFriend";
    //获取公告信息
    public const string GetNotice = "g/YGGetNotice";
    //获取我的消息
    public const string GetMessage = "g/YGGetMessage";
    //获取公告内容
    public const string GetNoticeContent = "g/YGGetNoticeContent";
    //获取公告列表
    public const string GetNoticelist = "g/YGGetNoticelist";
    //是否同意添加好友
    public const string IsAgree = "g/YGIsAgree";
    //是否同意进入俱乐部
    public const string IsClubAgree = "c/IsAgree";
    //获取俱乐部消息
    public const string GetClubMessage = "c/YGGetClubMessage";
    //获取实时消息通知
    public const string GetRealTimeMessage = "<YGGetRealTimeMessage>";
    //获取牌局消息
    public const string GetPaiMessage = "g/YGGetPaiMessage";
    //牌局加入是否同意
    public const string IsAgreeJoinPai = "g/YGIsAgreeJoinPai";
    //意见反馈
    public const string SendAgreement = "g/YGSendAgreement";
    // 保险明细
    public const string GetInsuranceDetail = "g/getInsurance";
    // 获取上一局牌局设置
    public const string GetRoundOld = "a/getRoundOld";
    // 修改好友备注
    public const string ModifyFriendRemark = "g/addFriendRemark";
    
    
}
