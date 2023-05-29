using UnityEngine;
using System.Collections;
using System;

public class MessageManagerUnion : MonoBehaviour {
    private static MessageManagerUnion _instance;
    public static MessageManagerUnion GetSingleton()
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
        NetMngr.GetSingleton().AddListener(InterfaceUnion.CreateUnion, CreateUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.GetCreateUnionDiamond, GetCreateUnionDiamondCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.GetMyUnionList, GetMyUnionListCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.QueryUnion, QueryUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.GetUnionInfo, GetUnionInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.JoinUnion, JoinUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.IsUnionAgree, OnIsUnionAgree);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.GetUnionClub, GetUnionClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.SetUnionApply, SetUnionApplyCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.DissUnion, DissUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.UnionUpdateConfig, UnionUpdateConfigCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.KickUnionClub, KickUnionClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.UpgradeUnion, UpgradeUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.QuitUnion, QuitUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.BuyUnionCoin, BuyUnionCoinFinish);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.SendUnionCoin, SendUnionCoinCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.UpdateUnionCoin, OnUpdateUnionCoin);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.QueryUnionCoin, QueryUnionCoinCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceUnion.RecycleUnionCoin, RecycleUnionCoinCallBack);
        
    }

    //创建联盟
    private void CreateUnionCallBack(Hashtable data) {

        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());

        if (data["success"].ToString() == "1")
        {
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
            UnionManager.GetSingleton().myUnionPanel.Refresh();
        }
    }

    // 创建联盟钻石消耗
    private void GetCreateUnionDiamondCallBack(Hashtable data){
        UnionManager.GetSingleton().unionCreatePanel.SetDiamond(data);
        UnionManager.GetSingleton().shopUnionCoinPanel.SetRate(data);
    }

    // 获得联盟列表
    private void GetMyUnionListCallBack(Hashtable data){
        ArrayList list = new ArrayList();
        if(data.ContainsKey("list")){
            list = data["list"] as ArrayList;
        }
        UnionManager.GetSingleton().myUnionPanel.SetInfo(list);
    }

    // 查询联盟
    private void QueryUnionCallBack(Hashtable data){
        if(data["success"].ToString() == "0"){
            UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
            return;
        }
        UnionManager.GetSingleton().unionJoinPanel.QueryUnionCallBack(data);
    }

    // 加入联盟
    private void JoinUnionCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }

    // 联盟详情
    private void GetUnionInfoCallBack(Hashtable data){
        UnionManager.GetSingleton().unionInfoPanel.SetInfo(data);
    }

    // 是否加入联盟
    private void OnIsUnionAgree(Hashtable data)
    {
        if(data["success"].ToString() == "1")
        {
            HallManager.GetSingleton().myMsgTopPanel.IsClubAgreeFinished(data);
        }
        else
        {
            UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        }
    }

    // 获取联盟俱乐部
    private void GetUnionClubCallBack(Hashtable data){
        UnionManager.GetSingleton().unionInfoPanel.SetClubInfo(data);
    }

    // 设置联盟申请开关
    private void SetUnionApplyCallBack(Hashtable data){
        // 
    }

    // 解散联盟
    private void DissUnionCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
            UnionManager.GetSingleton().myUnionPanel.Refresh();
        }
    }

    // 踢出联盟俱乐部
    private void KickUnionClubCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            NetMngr.GetSingleton().Send(InterfaceUnion.GetUnionClub, new object[] {
                UnionManager.GetSingleton().unionId
            });
        }    
    }

    // 获取联盟升级信息
    private void UnionUpdateConfigCallBack(Hashtable data){
        UnionManager.GetSingleton().unionUpgradePanel.SetInfo(data);
    }

    // 升级联盟
    private void UpgradeUnionCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            UnionManager.GetSingleton().unionUpgradePanel.Upgrade(int.Parse(data["level"].ToString()));
        }
    }

    // 退出联盟
    private void QuitUnionCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
            UnionManager.GetSingleton().myUnionPanel.Refresh();
        }    
    }

    // 兑换联盟币
    private void BuyUnionCoinFinish(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            UnionManager.GetSingleton().shopUnionCoinPanel.Refresh();
            UnionManager.GetSingleton().unionCoinPanel.Refresh();
            ClubManager.GetSingleton().clubInfoTopPanel.RefreshUnionCoin();
        }
    }

    // 发放联盟币
    private void SendUnionCoinCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            UnionManager.GetSingleton().sendUnionCoinPanel.Refresh();
            UnionManager.GetSingleton().unionCoinPanel.Refresh();
            ClubManager.GetSingleton().clubInfoTopPanel.RefreshUnionCoin();
        }
    }

    // 更新联盟币
    private void OnUpdateUnionCoin(Hashtable data){
        string unionId = data["lmId"].ToString();
        int coin = int.Parse(data["lmCoin"].ToString());
        StaticData.unionCoin = coin;
        // StaticData.unionCoins[unionId] = coin;
    }

    // 查询联盟币
    private void QueryUnionCoinCallBack(Hashtable data){
        if(data["success"].ToString() == "1"){
            string unionId = data["lmId"].ToString();
            int coin = int.Parse(data["lmCoin"].ToString());
            StaticData.unionCoin = coin;
            // StaticData.unionCoins[unionId] = coin;    
        }
    }

    // 回收联盟币
    private void RecycleUnionCoinCallBack(Hashtable data){
        UnionManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            UnionManager.GetSingleton().recycleUnionCoinPanel.Refresh();
            UnionManager.GetSingleton().unionCoinPanel.Refresh();
            ClubManager.GetSingleton().clubInfoTopPanel.RefreshUnionCoin();
        }
    }

}