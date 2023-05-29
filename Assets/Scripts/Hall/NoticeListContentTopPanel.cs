using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoticeListContentTopPanel : BasePlane {

    private Button back;
    private Transform parent;

    public string id;
    public string type;

    private Hashtable data;

    private void Awake()
    {
        back = transform.Find("Top/Back").GetComponent<Button>();
        parent = transform.Find("Scroll View/Viewport/Content");

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });

    }

    void Start()
    {

    }

    void Update()
    {

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
        if (data == null)
            return;
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
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
        ArrayList withList = data["msglist"] as ArrayList;
        if (withList.Count == 0)
        {
            return;
        }
        Object objItem = Resources.Load("HallItem/NoticeItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            NoticeItem noticeItem = go.AddComponent<NoticeItem>();
            noticeItem.type2 = "2";
            noticeItem.type= ((Hashtable)withList[i])["type"].ToString();
            noticeItem.id = ((Hashtable)withList[i])["id"].ToString();
            noticeItem.SetData(withList[i] as Hashtable);
        }
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetNoticelist, new object[] { type, type == "1" ? "" : id });
    }

    public override void OnAddStart()
    {
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
