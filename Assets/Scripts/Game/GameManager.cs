using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public int myNetID;
    public List<int> MapLocalSeatPlayer;// 自己+其他人
    public int roomNum { get; set; }
    public int gameDichi { get; set; }
    public int myDeskLeftmoney { get; set; }
    public int roomXiaoMang { get; set; }
    public int straddle { get; set; }
    public int ante { get; set; }
    public int roomDaMang { get; set; }
    public int roomMinTakeMoneyRatio { get; set; }
    public int roomMaxTakeMoneyRatio { get; set; }  //最大比例
    public float serviceMoney { get; set; } // 服务费
    public int fee { get; set; } // 服务费 //add by yang yang 2021年3月23日 13:53:23
    public int takeMoney { get; set; } // 带入金额
    public int everyDelayTime { get; set; }
    public bool roomFirstGetIn { get; set; }
    public bool isGetGonggongPai { get; set; }
    public List<string> betRatio;//1/4 1/3 1/3 2/3 3/4 1 1.5 最小加注  Allin
    public int minCoin {get; set;} // 最小带入积分(创房设置)

    public string unionId = ""; // 联盟Id
    public string unionType = ""; // 联盟币结算？0否1是
    public int roomId;

    public static   GameManager GetSingleton()
    {
        return _instance;
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        myNetID = -1;
        everyDelayTime = -1;
        betRatio = new List<string>();
        betRatio.Add("1/3");
        betRatio.Add("3/4");
        betRatio.Add("1X");
        betRatio.Add("1.5X");



        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("AddZhu" + i))
            {
                if (ChangeString(PlayerPrefs.GetString("AddZhu" + i)) == 99)
                {
                    betRatio[i] = PlayerPrefs.GetString("AddZhu" + i);
                }
                else
                {
                    int num = ChangeString(PlayerPrefs.GetString("AddZhu" + i));
                    betRatio[num] = betRatio[i];
                    betRatio[i] = PlayerPrefs.GetString("AddZhu" + i);
                }
            }
        }
        //TEST  
        //MapLocalSeatPlayer.Add(0);
        //MapLocalSeatPlayer.Add(2);
        //MapLocalSeatPlayer.Add(1);
        //MapLocalSeatPlayer.Add(4);
        //MapLocalSeatPlayer.Add(3);
        //roomXiaoMang = 1;
        //roomDaMang = 3;
        //roomMinTakeMoneyRatio = 100;
        //roomMaxTakeMoneyRatio = 400;
        //gameDichi = 10;

    } 
       public int  ChangeString(string  type ) {
        int index = 99;
        for (int i = 0; i < betRatio.Count; i++) {
            if (betRatio[i] == type) {
                index = i;
            }
        }
        return index;
    }

    //有座玩家连续几把没下注，提示下把离座
    public void beOutOfDesk(Hashtable h)
    {

    }
    public void setNewParam(Hashtable h)
    {


        everyDelayTime = int.Parse(h["delayTime"].ToString());
        GameUIManager.GetSingleton().setNewTime(h);
        roomMaxTakeMoneyRatio = int.Parse(h["max"].ToString());
        roomMinTakeMoneyRatio = int.Parse(h["min"].ToString());
    }
    public void ResponseLeave(Hashtable hh)
    {
        int temp = netTolocal(int.Parse(hh["netID"].ToString()));
        MapLocalSeatPlayer.Remove(temp);
        GameUIManager.GetSingleton().setOneSeatEmpty(temp,hh);
    }
    //8 -7 自己站起围观
    public void mysleftLeave()
    {
		SoundManager.GetSingleton().PlaySound("Audio/situp");
        return;
//        Debug.Log("移除自己。。。");
//        MapLocalSeatPlayer.Remove(0);
//        GameUIManager.GetSingleton().setOneSeatEmpty(0);
    } 
    // 玩家暂离
    public void KeeySomeoneSeat(Hashtable hh)
    {
        int temp = netTolocal(int.Parse(hh["netid"].ToString()));
        GameUIManager.GetSingleton().setOneSeatKeey(temp, int.Parse(hh["keepTime"].ToString()));
    }
    public void BackToSeat(Hashtable hh)
    {
        int temp = netTolocal(int.Parse(hh["netID"].ToString()));
        GameUIManager.GetSingleton().BackToSeat(temp);
    }
  
    public void ResponseStandup(Hashtable h)
    {

    }
    public void getGameReusltTips(Hashtable h)
    {

    }
    public void setRoomInfo(Hashtable data)
    {  
        roomId = int.Parse(data["roomId"].ToString());
        roomNum = int.Parse(data["roomPlaycount"].ToString());
        roomXiaoMang= int.Parse(data["xiaomang"].ToString());
        roomDaMang = int.Parse(data["damang"].ToString());
        straddle = int.Parse(data["straddle"].ToString());
        ante = int.Parse(data["ante"].ToString());
        roomMaxTakeMoneyRatio = int.Parse(data["MaxCoin"].ToString());
        roomMinTakeMoneyRatio = int.Parse(data["MinCoin"].ToString());
        minCoin = int.Parse(data["MinCoin2"].ToString());

        if(data["serviceMoney"].ToString()!="")
        serviceMoney= float.Parse(data["serviceMoney"].ToString());
        roomFirstGetIn = data["needShowJiFenPai"].ToString()=="0"?false:true;

        if(data.ContainsKey("lmId")){
            unionId = data["lmId"].ToString();
        }
        if(data.ContainsKey("lmType")){
            unionType = data["lmType"].ToString();
        }
        if(isUnionCoinRoom()){
            NetMngr.GetSingleton().Send(InterfaceUnion.QueryUnionCoin, new object[] { unionId });
        }

        GameUIManager.GetSingleton().setRoomInfo(data);
    }

    public void ResetAllPlayerInfo()
    {
        GameUIManager.GetSingleton().ResetAllPlayerInfo();
    }
    public void setPlayInfo(Hashtable h)
    {
        ResetAllPlayerInfo();
        if (MapLocalSeatPlayer.Count > 0)
            MapLocalSeatPlayer.Clear();
        Debug.Log(" ---------------setPlayInfo  h = " + h);

        GameUIManager.GetSingleton().setAllSeatEmpty();
        myNetID = int.Parse(h["MyOwnnetID"].ToString());
        StaticData.isGuanzhan = myNetID >=0  ? false : true;
		if (StaticData.isGuanzhan) {
			playseatAnimation (0);
		} else {
			playseatAnimation (myNetID);
		}

        if(!StaticData.isGuanzhan)
            GameUIManager.GetSingleton().hideAllSeatHeadImg();

        ArrayList ar = h["playinfoList"] as ArrayList;
        for(int i = 0; i < ar.Count; i++)
        {
            Hashtable hh = ar[i] as Hashtable;
			int netId = int.Parse (hh ["netID"].ToString ());
			int temp = netTolocal(netId);

            //自己坐下了，转动座位
            //			if (netId == myNetID) {
            //				playseatAnimation (myNetID);
            //			}

//             if (netId == myNetID)
//             {
//                 if (hh.Contains("isInGame"))
//                 {
//                     Debug.Log(" ---------------setPlayInfo int.Parse(hh[isInGame].ToString()) = " + int.Parse(hh["isInGame"].ToString()));
//                     if (int.Parse(hh["isInGame"].ToString()) > 0)
//                     {
//                         StaticData.isInGame = true;
//                     }
//                     else
//                     {
//                         StaticData.isInGame = false;
//                     }
//                 }
//             }
//                


            if (!MapLocalSeatPlayer.Contains(temp))
            	MapLocalSeatPlayer.Add(temp);
            GameUIManager.GetSingleton().setPlayinfo(temp,hh);
            // 如果我在座位上
            if (hh.Contains("betMoney"))
            {
                if(int.Parse(hh["betMoney"].ToString())>0)
                GameUIManager.GetSingleton().setAlreadySeatPlayinfo(temp,hh["betMoney"].ToString());
            }
            if (hh.Contains("isInGame"))
            {
                if (int.Parse(hh["isInGame"].ToString()) > 0)
                    GameUIManager.GetSingleton().setAlreadySeatPlayinfoPai(temp, hh["betMoney"].ToString());
            }

            Debug.Log("更新头像了");
        }
        //MapLocalSeatPlayer.Sort((a,b)=>{ return a.CompareTo(b); });
    }

	void playseatAnimation(int posId)
	{
		//myNetID
		Animator ator1 =  GameUIManager.GetSingleton ().roomNumSitActivePlayerTrans.GetComponentInChildren<Animator>();
		ator1.SetInteger("InitPosition", posId);

		if (posId != 0) {
			SoundManager.GetSingleton().PlaySound("Audio/sit");
		}

	}

    public void setOthersInfo(Hashtable hh)
    {
        int netId = int.Parse(hh["netID"].ToString());
        string strName = hh["playName"].ToString();
        Debug.Log(" -----------------setOthersInfo  hh.Contains(isInGame) = " + hh.Contains("isInGame") + "netId = " + netId + "myNetID = " + myNetID + "playName = " + strName);
    //    if (netId == myNetID)
//         {
//            
//             if (hh.Contains("isInGame"))
//             {
//                 if (int.Parse(hh["isInGame"].ToString()) > 0)
//                 {
//                     StaticData.isInGame = true;
//                 }
//                 else
//                 {
//                     StaticData.isInGame = false;
//                 }
//             }
//         }

        int temp = netTolocal(netId);
        if (!MapLocalSeatPlayer.Contains(temp))
            MapLocalSeatPlayer.Add(temp);
        GameUIManager.GetSingleton().setPlayinfo(temp, hh);
    }
    public  void setZhuangInfo(Hashtable h)
    {
        int temp = netTolocal(int.Parse(h["netID"].ToString()));
        GameUIManager.GetSingleton().setZhuanginfo(temp);
    }
    public void TuoGuan(Hashtable h)
    {
        int temp = netTolocal(int.Parse(h["netID"].ToString()));
        bool b = bool.Parse(h["deposit"].ToString());
        GameUIManager.GetSingleton().TuoGuan(temp,b);
    }
    public int netTolocal(int i)
    {
		return i;
        // Debug.Log(i+"   "+myNetID+" ");
//        if (StaticData.isGuanzhan)
//        {
//            return i;
//        }
//        int temp = i - myNetID;
//            if (temp < 0)
//                return temp + roomNum;
//            return temp;
    }
    private void Update()
    {
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            //MapLocalSeatPlayer.Sort((a,b)=> {return b.CompareTo(a); });
//            Hashtable a= new Hashtable();
//            a.Add("netID",0);
//            ResponseLeave(a);
//        }
    }

    // 是否是联盟币房间
    public bool isUnionCoinRoom(){
        return unionId != "" && unionId != "0" && unionType == "1";
    }
    
}
