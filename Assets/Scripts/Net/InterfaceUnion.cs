using UnityEngine;
using System.Collections;

public class InterfaceUnion : MonoBehaviour {

	//创建联盟
    public const string CreateUnion = "m/createLM";
    // 创建联盟钻石消耗
    public const string GetCreateUnionDiamond = "m/getCreateDiamond";
    // 获得联盟列表
    public const string GetMyUnionList = "m/getMyAlliance";
    // 查询联盟
    public const string QueryUnion = "m/queryLm";
    // 加入联盟
    public const string JoinUnion = "m/joinAlliance";
    // 是否同意加入联盟
    public const string IsUnionAgree = "m/IsAgree";
    // 获得联盟详情
    public const string GetUnionInfo = "m/getLmInfo";
    // 获得联盟俱乐部
    public const string GetUnionClub = "m/myLmClud";
    // 设置联盟申请开关
    public const string SetUnionApply = "m/updateApply";
    // 解散联盟
    public const string DissUnion = "m/dissAlliance";
	// 踢出联盟
    public const string KickUnionClub = "m/delClub";
    // 升级联盟信息
    public const string UnionUpdateConfig = "m/getUpdateCost";
    // 升级联盟
    public const string UpgradeUnion = "m/updateLm";
    // 退出联盟
    public const string QuitUnion = "m/quitLm";
    // 兑换联盟币
    public const string BuyUnionCoin = "m/getLMCoin";
    // 发放联盟币
    public const string SendUnionCoin = "m/sendLMCoin";
    // 更新联盟币
    public const string UpdateUnionCoin = "<UpdateLmCoin>";
    // 查询联盟币
    public const string QueryUnionCoin = "m/queryLmCoin";
    // 回收联盟币
    public const string RecycleUnionCoin = "m/recycleLMCoin";
    
    
}