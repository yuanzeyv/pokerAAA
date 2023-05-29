using UnityEngine;
using System.Collections;
using System;

public class MessageManagerClub : MonoBehaviour {
    private static MessageManagerClub _instance;
    public static MessageManagerClub GetSingleton()
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

    public  void AddListen()
    {
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetMyClub,GetMyClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetClubInfo,GetClubInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.MemberInfo,MemberInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.DelMember,DelMemberCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetClubTag,SetClubTagCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetCoinContent,GetCoinContentCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetSendCoin,SetSendCoinCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetDiamondContent, GetDiamondContentCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetSendDiamond, SetSendDiamondCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.UpdateClub,UpdateClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetUpdateCost,GetUpdateCostCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetWelcomeWord,SetWelcomeWordCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetWelcomeWord, GetWelcomeWordCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetGongGao,SetGongGaoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetHotClub,GetHotClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.JoinClub,JoinClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetManager,SetManagerCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetManager, GetManagerCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.ManagerFind,ManagerFindCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SelectFind,SelectFindCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.MemberFind,MemberFindCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.DelGongGao,DelGongGaoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.DissClub,DissClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetGongGao,GetGongGaoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetClubRecord,GetClubRecordCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetClubMatch,GetClubMatchCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.CreateClub,CreateClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.FixClubInfo,FixClubInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetErWeiMa,GetErWeiMaCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetClub,GetclubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.ManagerFindAll, ManagerFindCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.MemberFindAll, MemberFindCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SetRefuseAndSelf,SetRefuseAndSelfCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.QuitClub, QuitClubCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.SendToPlayerDiamond, SendToPlayerDiamondCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetPlayerDiamondInfo, GetPlayerDiamondInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetPlayerDiamondrecord, GetPlayerDiamondrecordCallBack);

        //add by yang yang 2021年3月24日 17:16:38
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetUserShare, GetUserShareCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetFriendComission, GetFriendComissionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetComissionRecord, GetComissionRecordCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetBindingCode, GetBindingCodeCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetLookRecord, GetLookRecordCallBack);

        NetMngr.GetSingleton().AddListener(InterfaceClub.GetCashOutInfo, GetCashOutInfoCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetCashOutRecord, GetCashOutRecordCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.GetCashOut, GetCashOutCallBack);

        NetMngr.GetSingleton().AddListener(InterfaceClub.GetManualContent, GetManualContentCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.UpdateManualContent, UpdateManualContentCallBack);

        NetMngr.GetSingleton().AddListener(InterfaceClub.GetClubUnion, GetClubUnionCallBack);
        NetMngr.GetSingleton().AddListener(InterfaceClub.ClubModifyLimit, ClubModifyLimitCallBack);
        
    }

    private void GetPlayerDiamondrecordCallBack(Hashtable message)
    {
        if (ClubManager.GetSingleton() != null)
        {
            if(message["type"].ToString() == "1"){ // 积分
                ClubManager.GetSingleton().sendMemberCoinTopPanel.GetRedcordInfoCallback(message);
            } else if(message["type"].ToString() == "2") { // 钻石
                ClubManager.GetSingleton().sendMemberDiamondTopPanel.GetRedcordInfoCallback(message);
            } else if(message["type"].ToString() == "3") { // 发放联盟币
                UnionManager.GetSingleton().sendUnionCoinPanel.GetRedcordInfoCallback(message);
            } else { // 回收联盟币
                UnionManager.GetSingleton().recycleUnionCoinPanel.GetRedcordInfoCallback(message);
            }
        }
    }

    private void GetPlayerDiamondInfoCallBack(Hashtable message)
    {
        if (ClubManager.GetSingleton() != null)
        {
            if(message["type"].ToString() == "1"){ // 积分
                ClubManager.GetSingleton().sendMemberCoinTopPanel.GetMemberCoinTopCallBack(message);        
            } else if(message["type"].ToString() == "2") { // 钻石
                ClubManager.GetSingleton().sendMemberDiamondTopPanel.GetMeberDiamondTopCallBack(message);
            } else if(message["type"].ToString() == "3") { // 联盟币
                UnionManager.GetSingleton().sendUnionCoinPanel.GetUnionCoinTopCallBack(message);
                UnionManager.GetSingleton().recycleUnionCoinPanel.GetUnionCoinTopCallBack(message);
            }
        }
    }

    private void SendToPlayerDiamondCallBack(Hashtable message)
    {
        if (ClubManager.GetSingleton() != null)
        {
            if(message["type"].ToString() == "1"){ // 积分
                ClubManager.GetSingleton().sendMemberCoinTopPanel.SendToCallBack(message);
            } else { // 钻石
                ClubManager.GetSingleton().sendMemberDiamondTopPanel.SendToCallBack(message);
            }
        }
    }

    //获取欢迎词
    public void GetWelcomeWordCallBack(Hashtable data)
    {
        if (ClubManager.GetSingleton() != null)
            ClubManager.GetSingleton().welcomeMemberTopPanel.GetWelcomeCallBack(data);
    }

    //获取我的俱乐部
    public void GetMyClubCallBack(Hashtable data) {
		if (ClubManager.GetSingleton () != null) {
			ClubManager.GetSingleton ().clubListPanel.GetMyClubCallBack (data);
		}
    }

    //获取俱乐部信息
    public void GetClubInfoCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubInfoTopPanel.GetClubInfoCallBack(data);
    }


    //俱乐部成员信息
    public void MemberInfoCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubMemberInfoTopPanel.MemberInfoCallBack(data);
    }


    //踢出俱乐部
    public void DelMemberCallBack(Hashtable data) {

        StartCoroutine(Wait(data["message"].ToString()));
        if (data["success"].ToString() == "1") {
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        }

        NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }


    //设置俱乐部初始标签
    public void SetClubTagCallBack(Hashtable data) {
        if (data["tagType"].ToString() == "0")
        {
            ClubManager.GetSingleton().clubCreateTopPanel.SetClubTagCallBack(data);
        }
        else {
            ClubManager.GetSingleton().clubEditTopPanel.SetClubTagCallBack(data);
        }
    }

    //获取赠送钻石内容
    public void GetDiamondContentCallBack(Hashtable data)
    {
		ClubManager.GetSingleton().clubInfoTopPanel.GetDiamondContentCallBack(data);
    }

    //设置是否赠送钻石
    public void SetSendDiamondCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }

    //获取赠送积分内容
    public void GetCoinContentCallBack(Hashtable data) {
		ClubManager.GetSingleton().clubInfoTopPanel.GetCoinContentCallBack(data);
    }


    //设置是否赠送积分
    public void SetSendCoinCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }

    //升级俱乐部
    public void UpdateClubCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
		ClubManager.GetSingleton ().clubUpdateTopPanel.UpdateClubCallBack (data ["success"].ToString ());
    }

    //获取升级俱乐部的金额
    public void GetUpdateCostCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubUpdateTopPanel.GetUpdateCostCallBack(data);
    }


    //设置新人欢迎词
    public void SetWelcomeWordCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }


    //新建公告
    public void SetGongGaoCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        NetMngr.GetSingleton().Send(InterfaceClub.GetGongGao, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    //热门俱乐部
    public void GetHotClubCallBack(Hashtable data) {
        ClubManager.GetSingleton().hotClubTopPanel.GetHotClubCallBack(data);
    }

    //加入俱乐部
    public void JoinClubCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }

    //设置管理员
    public void SetManagerCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        NetMngr.GetSingleton().Send(InterfaceClub.GetManager, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    //获取管理员
    public void GetManagerCallBack(Hashtable data) {
        ClubManager.GetSingleton().setClubManagerTopPanel.GetManagerCallBack(data);
    }

    //管理员 查找成员
    public void ManagerFindCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubMemberTopPanel.ManagerFindCallBack(data);
    }

    //筛选 查找成员
    public void SelectFindCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubMemberTopPanel.ManagerFindCallBack(data);
    }

    //成员查找成员
    public void MemberFindCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubMember2TopPanel.MemberFindCallBack(data);
    }

    //清除公告
    public void DelGongGaoCallBack(Hashtable data) {
        StartCoroutine(Wait(data["message"].ToString()));
        NetMngr.GetSingleton().Send(InterfaceClub.GetGongGao, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    IEnumerator Wait(string  s) {
        yield return new  WaitForSeconds(1);
        ClubManager.GetSingleton().commonPopup.ShowView(s);
    }
    

    //解散俱乐部
    public void DissClubCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if (data["success"].ToString() == "1") {
            ClubManager.GetSingleton().dissClubPopup.HideView();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
			ClubManager.GetSingleton().myClubPanel.gameObject.SetActive(true);
            ClubManager.GetSingleton().clubPanel.gameObject.SetActive(false);
			ClubManager.GetSingleton().myClubPanel.transform.parent.SetAsLastSibling();
			ClubManager.GetSingleton().clubListPanel.GetClubList();

        }


    }

    //获取俱乐部公告
    public void GetGongGaoCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubPanel.GetGongGaoCallBack(data);
    }

    //获取俱乐部牌局记录
    public void GetClubRecordCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubPanel.GetClubRecordCallBack(data);
    }

    //获取俱乐部牌局
    public void GetClubMatchCallBack(Hashtable data) {
       
        ClubManager.GetSingleton().clubPanel.GetMatchCallBack(data);

    }


    //创建俱乐部
    public void CreateClubCallBack(Hashtable data) {

        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());

        if (data["success"].ToString() == "1")
        {
           
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            ClubManager.GetSingleton().clubListPanel.gameObject.SetActive(true);
            ClubManager.GetSingleton().clubPanel.gameObject.SetActive(false);
            ClubManager.GetSingleton().clubListPanel.transform.parent.SetAsLastSibling();

			ClubManager.GetSingleton().clubListPanel.GetClubList();

        }
    }

    //编辑俱乐部信息
    public void FixClubInfoCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }

    //获取二维码图片
    public void GetErWeiMaCallBack(Hashtable data) {
        ClubManager.GetSingleton().erweimaPanel.GetErWeiMaCallBack(data);
    }

    //搜索俱乐部
    public void GetclubCallBack(Hashtable data) {
        ClubManager.GetSingleton().hotClubTopPanel.GetHotClubCallBack(data);
    }

    //设置免打扰和私密
    public void SetRefuseAndSelfCallBack(Hashtable data)
    {
       // ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }
    //退出俱乐部
    public void QuitClubCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if (data["success"].ToString() == "1")
        {
            ClubManager.GetSingleton().dissClubPopup.HideView();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            ClubManager.GetSingleton().clubListPanel.gameObject.SetActive(true);
            ClubManager.GetSingleton().clubPanel.gameObject.SetActive(false);
            ClubManager.GetSingleton().clubListPanel.transform.parent.SetAsLastSibling();
			ClubManager.GetSingleton().clubListPanel.GetClubList();

        }

    }

    public void GetUserShareCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().friendCommissionPanel.getUserShareCallBack(data);
    }

    public void GetFriendComissionCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().friendCommissionPanel.getFriendComissionCallBack(data);
    }
    public void GetComissionRecordCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().friendCommissionPanel.getComissionRecordCallBack(data);
    }
    public void GetBindingCodeCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().clubListPanel.getBindingCodeCallBack(data);
        ClubManager.GetSingleton().friendCommissionPanel.getBindingCodeCallBack(data);
    }
    public void GetLookRecordCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().friendCommissionPanel.getLookRecordCallBack(data);
    }

    //NetMngr.GetSingleton().AddListener(InterfaceClub.GetCashOutInfo, GetCashOutInfoCallBack);
    //NetMngr.GetSingleton().AddListener(InterfaceClub.GetCashOutRecord, GetCashOutRecordCallBack);
    //NetMngr.GetSingleton().AddListener(InterfaceClub.GetCashOut, GetCashOutCallBack);
    public void GetCashOutInfoCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().clubCashOutPanel.getCashOutInfoCallBack(data);
    }
    public void GetCashOutRecordCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().clubCashOutPanel.getCashOutRecordCallBack(data);
    }
    public void GetCashOutCallBack(Hashtable data)
    {
        ClubManager.GetSingleton().clubCashOutPanel.getCashOutCallBack(data);
    }

    //获取赠送钻石积分开关状态
    public void GetManualContentCallBack(Hashtable data) {
        ClubManager.GetSingleton().clubInfoTopPanel.GetManualContentCallBack(data);
    }

    //设置是否赠送积分
    public void UpdateManualContentCallBack(Hashtable data) {
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
    }

    // 获取俱乐部所在联盟
    public void GetClubUnionCallBack(Hashtable data){
        if(data["success"].ToString() == "1"){
            ArrayList info = data["list"] as ArrayList;
            for(int i = 0; i < info.Count; i++){
                Hashtable h = info[i] as Hashtable;
                if(h["lmType"].ToString() == "2"){
                    string unionId = h["lmId"].ToString();
                    int coin = int.Parse(h["lmCoin"].ToString());
                    // StaticData.unionCoins[unionId] = coin;
                    StaticData.unionId = unionId;
                    StaticData.unionCoin = coin;                    
                }
            }
        }
        ClubManager.GetSingleton().clubInfoTopPanel.SetUnionInfo(data);
        HallManager.GetSingleton().createGamePopup.SetClubUnion(data);            
    }

    // 修改免授权额度
    public void ClubModifyLimitCallBack(Hashtable data){
        ClubManager.GetSingleton().commonPopup.ShowView(data["message"].ToString());
        if(data["success"].ToString() == "1"){
            ClubManager.GetSingleton().clubModifyLimitPanel.Finished();
        }
    }

}
