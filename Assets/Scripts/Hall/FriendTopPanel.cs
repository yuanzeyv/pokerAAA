using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FriendTopPanel : BasePlane {


    private Button backBtn;
    private Button addBtn;
    private GameObject image;
    private Text ss;
    private InputField search;
    private ChatScroller chatScroller;
    private Transform parent;
    private Text n;
    private Text ns;

    private const int showPage = 10;
    public int currPage = 1;
    private int sumPage = 1;

    private Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        image = transform.Find("Search/Placeholder/Image").gameObject;
        search = transform.Find("Search").GetComponent<InputField>();
        ss = transform.Find("Search/Placeholder").GetComponent<Text>();
        addBtn = transform.Find("Top/Save").GetComponent<Button>();
        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();
//        parent = transform.Find("Scroll View/Viewport/Content");
//        n = transform.Find("Scroll View/Text").GetComponent<Text>();
        ns = transform.Find("ChatScroller/Text").GetComponent<Text>();

        addBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().addFriendTopPanel);
        });

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        search.onEndEdit.AddListener(delegate
        {
            if (!string.IsNullOrEmpty(search.text))
            {
//                parent.parent.parent.gameObject.SetActive(true);
                chatScroller.gameObject.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceMain.SearchPlayer, new object[] { "1", search.text });
            }
            else
            {
                chatScroller.gameObject.SetActive(true);
//                parent.parent.parent.gameObject.SetActive(false);
                
            }
        });
    }
    //下拉刷新
    public void SetFriendReflash()
    {
        //if (currPage <= sumPage)
        //{
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GetFriendList, new object[] { currPage, showPage });
        //}
        
    }
    public void SetFriendData(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["friends"] as ArrayList;
        
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 3;
            datas[i]["URL"] = ((Hashtable)list[i])["playerURL"].ToString();
            datas[i]["Name"] = ((Hashtable)list[i])["playerName"].ToString();
            datas[i]["ID"] = ((Hashtable)list[i])["playerID"].ToString();
        }
        if (currPage==1)
        {
            chatScroller.ClearList();
            chatScroller.datas.Clear();
            chatScroller.modelList.Clear();
            chatScroller.SetDatas(datas);
            if (list.Count == 0)
            {
                ns.gameObject.SetActive(true);
            }
            else
            {
                ns.gameObject.SetActive(false);
            }
        }
        else
        {
            chatScroller.SetDatas(0,datas);
        }
    }
    public void SetData(Hashtable data)
    {
        this.data = data;
        RefreshList();
    }
    /// <summary>
    /// 清空列表
    /// </summary>
    private void ClearList()
    {
//        if (data == null)
//            return;
//        int childCount = parent.childCount;
//        for (int i = 0; i < childCount; i++)
//        {
//            Destroy(parent.GetChild(i).gameObject);
//        }
    }
    /// <summary>
    /// 刷新列表
    /// </summary>
    private void RefreshList()
    {
//        if (data == null)
//            return;
//        ClearList();
//        ArrayList withList = data["friends"] as ArrayList;
//        if (withList.Count == 0)
//        {
//            n.gameObject.SetActive(true);
//        }
//        else
//        {
//            n.gameObject.SetActive(false);
//        }
//        Object objItem = Resources.Load("HallItem/FriendTopPanelItem");
//        for (int i = 0; i < withList.Count; i++)
//        {
//            GameObject go = Instantiate(objItem) as GameObject;
//            go.transform.SetParent(parent);
//            go.transform.localScale = Vector3.one;
//            go.transform.localPosition = Vector3.zero;
//            FriendTopPanelItem friendTopPanelItem = go.AddComponent<FriendTopPanelItem>();
//            friendTopPanelItem.SetData(withList[i] as Hashtable);
//        }
    }

    void Start()
    {

    }

    void Update()
    {
        image.SetActive(ss.IsActive());
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetFriendList, new object[] { currPage, showPage });
    }

    public override void OnAddStart()
    {
        currPage = 1;
        
    }

    public override void OnRemoveComplete()
    {
        search.text = "";
        chatScroller.gameObject.SetActive(true);
//        parent.parent.parent.gameObject.SetActive(false);
    }

    public override void OnRemoveStart()
    {
        
    }
}
