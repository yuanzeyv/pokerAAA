using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoticeListBottomPanel : BasePlane {

    private Button myMsg;
    private Button paiMsg;
	private Button sysMsg;
    private Transform parent;
    private Text title;
    private Text paiTitile;
	private Text sysTitle;

    private Image myRed;
    private Image sysRed;
    private Image paiRed;

    private string sysId;
	private string sysType;
    private Button backBtn;
    private Hashtable data;

    private void Awake()
    {
        parent = transform.Find("Scroll View/Viewport/Content");
        backBtn = transform.Find("Top/Back/Share").GetComponent<Button>();
        myMsg = parent.Find("MyMsg").GetComponent<Button>();
        title = parent.Find("MyMsg/Title").GetComponent<Text>();
        paiTitile = parent.Find("PaiMsg/Title").GetComponent<Text>();
        paiMsg = parent.Find("PaiMsg").GetComponent<Button>();

		sysTitle = parent.Find("SysMsg/Title").GetComponent<Text>();
		sysMsg = parent.Find("SysMsg").GetComponent<Button>();

        myRed = parent.Find("MyMsg/Red").GetComponent<Image>();
        sysRed = parent.Find("SysMsg/Red").GetComponent<Image>();
        paiRed = parent.Find("PaiMsg/Red").GetComponent<Image>();


        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().showRed();
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        myMsg.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            myRed.gameObject.SetActive(false);
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().myMsgTopPanel);
        });
        paiMsg.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            myRed.gameObject.SetActive(false);
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().paiMsgTopPanel);
        });

		sysMsg.onClick.AddListener(() =>
		{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            myRed.gameObject.SetActive(false);
            HallManager.GetSingleton().noticeListContentTopPanel.id = sysId;
				HallManager.GetSingleton().noticeListContentTopPanel.type = sysType;
				HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().noticeListContentTopPanel);
		});
    }

    void Start ()
    {
        title.text = "我的消息(" + StaticData.MyMessage + ")";
        paiTitile.text = "牌局消息(" + StaticData.PaiMessage + ")";
		sysTitle.text = "系统消息(" + StaticData.SysMessage + ")";

        if (int.Parse(StaticData.MyMessage) > 0)
        {
            myRed.gameObject.SetActive(true);
        }
        if (int.Parse(StaticData.PaiMessage) > 0)
        {
            paiRed.gameObject.SetActive(true);
        }
        if (int.Parse(StaticData.SysMessage) > 0)
        {
            sysRed.gameObject.SetActive(true);
        }
    }

	void Update ()
    {
	
	}

    public void SetData(Hashtable data)
    {
		this.data = data;
		if (gameObject.activeInHierarchy) {
			RefreshList();
		}
    }

    /// <summary>
    /// 清空列表
    /// </summary>
    private void ClearList()
    {
        if (data == null)
            return;
        if (parent != null)
        {
            int childCount = parent.childCount;
            for (int i = 3; i < childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
      
    }
    /// <summary>
    /// 刷新列表
    /// </summary>
    private void RefreshList()
    {
        if (data == null)
            return;
        ClearList();
        ArrayList withList = data["notices"] as ArrayList;
        if (withList.Count == 0)
        {
            return;
        }
		Object objItem = Resources.Load("HallItem/NoticeItem");
        for (int i = 0; i < withList.Count; i++)
        {
			string type =  ((Hashtable)withList[i])["type"].ToString();
			string id =  ((Hashtable)withList[i])["type"].ToString();
			if (type == "1") //系统消息
			{
				sysId = id;
				sysType = type;
				continue;
			}

            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            NoticeItem noticeItem = go.AddComponent<NoticeItem>();
            noticeItem.type2 = "1";
			noticeItem.id = id;
			noticeItem.type = type;
            noticeItem.SetData(withList[i] as Hashtable);
        }
    }
    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetNotice, new object[] { });
    }

    public override void OnAddStart()
    {
        TouchMove.Instance().RemoveFunction();
        title.text = "我的消息("+StaticData.MyMessage+")";
        if (int.Parse(StaticData.MyMessage) > 0)
        {
            myRed.gameObject.SetActive(true);
        }
        paiTitile.text = "牌局消息(" + StaticData.PaiMessage + ")";
        if (int.Parse(StaticData.PaiMessage) > 0)
        {
            paiRed.gameObject.SetActive(true);
        }
        sysTitle.text = "系统消息(" + StaticData.SysMessage + ")";
        if (int.Parse(StaticData.SysMessage) > 0)
        {
            sysRed.gameObject.SetActive(true);
        }

    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
