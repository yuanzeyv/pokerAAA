using UnityEngine;
using System.Collections;

public class TestClub : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Hashtable  data = new Hashtable();
           
            data["url"] = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1550137424643&di=4975165ce2a2aa87c19d238001d8d19e&imgtype=0&src=http%3A%2F%2Fb-ssl.duitang.com%2Fuploads%2Fitem%2F201512%2F24%2F20151224234958_ERPST.jpeg";
            data["clubId"] = "111000";
            data["clubName"] = "赌神俱乐部";
            data["memberCount"] = "100/500";
            data["tag"] = "傻逼一号|傻逼二号|傻逼三号";
            data["clubJianJie"] = "独到的游戏理解，带你飞";
            data["clubkefu"] = "xxxxxxxxxxx";
            data["isRefuseMessage"] = "1";
            data["isSelf"] = "0";
            data["isMine"] = "1";
            ClubManager.GetSingleton().clubInfoTopPanel.GetClubInfoCallBack(data);
        }
	}
}
