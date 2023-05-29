using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticFunction : MonoBehaviour {

    private Transform reporter;
    public bool isStart = true;

    private static StaticFunction _instance;
    public static StaticFunction Getsingleton()
    {
        return _instance;
    }

    public Hashtable data;
    public bool isSync = false;

    public float timer = 0.5f;
    public bool istimer = false;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        reporter = transform.Find("Reporter");
    }

	void Start ()
    {
        //Application.targetFrameRate = 60;
	}
	

	void Update ()
    {
        if (istimer)
        {
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                istimer = false;
                timer = 0.5f;
                isStart = true;
            }
        }
        if (isStart)
        {
            if (MessageManagerGame.GetSingleton() != null && MessageManagerGame.GetSingleton().queue.Count != 0)
            {
                isStart = false;
                Hashtable data = MessageManagerGame.GetSingleton().queue.Dequeue() as Hashtable;
                print("授权弹窗"+"----"+ MessageManagerGame.GetSingleton().queue.Count + isStart);
                PopupCommon.GetSingleton().ShowView("玩家:" + data["playerName"] + "请求授权带入牌局" + data["roundName"].ToString() + "积分" + data["bringCoin"].ToString(), null, true, delegate
                {
//                    PopupCommon.GetSingleton().btnCancel.transform.Find("Text").GetComponent<Text>().text = "取消";
//                    PopupCommon.GetSingleton().btnSure.transform.Find("Text").GetComponent<Text>().text = "确定";
                    NetMngr.GetSingleton().Send(InterfaceMain.IsAgreeJoinPai, new object[] { data["id"].ToString(), "1" });
                    istimer = true;
                    print("取消" + "----" + MessageManagerGame.GetSingleton().queue.Count + isStart);

                }, delegate
                {
//                    PopupCommon.GetSingleton().btnCancel.transform.Find("Text").GetComponent<Text>().text = "取消";
//                    PopupCommon.GetSingleton().btnSure.transform.Find("Text").GetComponent<Text>().text = "确定";
                   // NetMngr.GetSingleton().Send(InterfaceMain.IsAgreeJoinPai, new object[] { data["id"].ToString(), "0" });
                    istimer = true;
                    print("确定" + "----" + MessageManagerGame.GetSingleton().queue.Count + isStart);
                });
//                PopupCommon.GetSingleton().btnCancel.transform.Find("Text").GetComponent<Text>().text = "拒绝";
//                PopupCommon.GetSingleton().btnSure.transform.Find("Text").GetComponent<Text>().text = "同意";
            }
        }
        if (isSync)
        {
            if (data!=null)
            {
                GameUIManager.GetSingleton().insurancePanel.SyncInsurance(data);
                data = null;
            }
        }
    }

    public void ChangeScene(string name)
    {
        Waitting.getInstance().Show();
        switch (name)
        {
            case "Login":
                StaticData.selectScene = 0;
                MessageManagerGame.GetSingleton().AddListen();// 重新监听，要不然登陆不了
                    break;
            case "Main":
                StaticData.selectScene = 1;
                //StaticData.isInRoom = false;
                StaticData.isReplay = false;
                StaticData.gameStart = false;
                break;
            case "Game": StaticData.selectScene = 2; break;
        }
        //Waitting.getInstance().show();
        Debug.Log("跳转到" + name + "场景");
        SceneManager.LoadSceneAsync(name);
    }
    public void ShowLog(bool b)
    {
        reporter.gameObject.SetActive(b);
    }
    public Texture SetPai(string index)
    {
        return Resources.Load<Texture>(RoomSetPopup.cardPath + index);
    }
    public Color GetColor(int score)
    {
        if (score > 0)
        {
            return new Color(1.0f, 99 / 255.0f, 99 / 255.0f); //红色 (255,99,99)
        }
        else
        {
            return new Color(32 / 255.0f, 197 / 255.0f, 48 / 255.0f);//绿色(32,197,48)
        }
    }
}
