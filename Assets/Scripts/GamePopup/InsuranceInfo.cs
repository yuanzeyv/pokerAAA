using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsuranceInfo : BasePopup {

    public Button openBtn;
    private CircleImage icon;
    private Text playerName;
    private Text message;

    public bool isStart = false;
    public float timerConst = 0;
    public float timer = 0;

    private void Awake()
    {
        Init();
        openBtn = transform.Find("Image/Open").GetComponent<Button>();
        icon = transform.Find("Image/head").GetComponent<CircleImage>();
        playerName = transform.Find("Image/name").GetComponent<Text>();
        message = transform.Find("Image/message").GetComponent<Text>();

        openBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
            GameUIManager.GetSingleton().planeManager.AddTopPlane(GameUIManager.GetSingleton().insurancePanel);
            NetMngr.GetSingleton().Send(InterfaceGame.GetInsuranceInitData, new object[] { });
        });
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (isStart)
        {
            timer -= Time.deltaTime;
            message.text= "玩家购买保险中<color=#FFCF33FF>" + (int)timer + "s</color>";
            if (timer<=0)
            {
                isStart = false;
                timer = timerConst;
                HideView();
            }
        }
	}

    public void SetData(Hashtable data)
    {
        GameTools.GetSingleton().GetTextureFromNet(data["url"].ToString(), SetIcon);
        playerName.text = data["name"].ToString();
        message.text = "玩家购买保险中<color=#FFCF33FF>" + timer+ "s</color>";
        timerConst = float.Parse(data["time"].ToString());
        timer = timerConst;
        isStart = true;
    }

    public void SetLaterData(Hashtable data)
    {
        isStart = false;
        message.text = "已购买" + data["count"].ToString() + "张OUTS,投保<color=#FFCF33FF>" + data["toubaoe"].ToString() + "</color>,预计赔付<color=#FFCF33FF>" + data["odds"].ToString() + "</color>";
        StartCoroutine(wait(2));
    }

    public void setInfo(Hashtable data)
    {
        isStart = false;
        StopAllCoroutines();
        openBtn.gameObject.SetActive(false);
        if (data["type"].ToString()=="0")//被反超
        {
            GameTools.GetSingleton().GetTextureFromNet(data["url"].ToString(),SetIcon);
            playerName.text = data["name"].ToString();
            message.text = "玩家被反超，获得保险赔付：<color=#FFBC5AFF>"+data["odds"].ToString()+"</color>";
            StartCoroutine(wait(2));
        }
        else if (data["type"].ToString()=="1")//收底池
        {
            GameTools.GetSingleton().GetTextureFromNet(data["url"].ToString(),SetIcon);
            playerName.text = data["name"].ToString();
            message.text = "底池：<color=#FFBC5AFF>"+data["dichi"].ToString()+"</color> \n 扣除保费：<color=#FFBC5AFF>"+data["baofei"].ToString()+"</color>";
            StartCoroutine(wait(2));
        }
        
    }
    
    private IEnumerator wait(int time)
    {
        yield return new WaitForSeconds(time);
        HideView();
    }

    private void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
