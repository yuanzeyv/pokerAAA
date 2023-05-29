using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticData : MonoBehaviour {

    public static int selectScene = -1;
    public static int getVerificationCode = 0;
    public static string username = "";
    public static string ID="";
    public static string url = "";
    public static string sex = "";
    public static string signature = "";
    public static int diamond=0;
    public static int gold = 0;  
    public static bool isGuanzhan = true;
    public static bool isInGame = true;
    public static string version = "0.5";
    public static string UpdateURL = "";
    public static bool isDebug = false;
    public static bool isMusic = true;
    public static bool isVibrate = true;
    public static bool isinform = true;
    public static string MyMessage = "0";
    public static string PaiMessage = "0";
	public static string SysMessage = "0";
    public static string imgUrl = "";
    public static string[] reviewData;
    public static bool isReplay = true;
    public static bool gameStart = false;
    public static string isGameOverShowPanel = "";
    public static string clubId="";
    public static string clubName = "";
    public static int isHost = 0;

	public static string[] littlemangs;
	public static string[] noteBeforeTip;

    public static Dictionary<string, int> unionCoins = new Dictionary<string, int>();
    public static string unionId = "";
    public static int unionCoin = 0;  
 

}
