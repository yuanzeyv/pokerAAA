using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddFriendTopPanel : BasePlane {

    private Button backBtn;
    private GameObject image;
    private Text ss;
    private InputField search;
    private Transform parent;
    private Text n;

    private Hashtable data;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        image = transform.Find("Search/Placeholder/Image").gameObject;
        search = transform.Find("Search").GetComponent<InputField>();
        ss = transform.Find("Search/Placeholder").GetComponent<Text>();
        parent = transform.Find("Scroll View/Viewport/Content");
		n = transform.Find("Scroll View/Text").GetComponent<Text>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        search.onEndEdit.AddListener(delegate 
        {
			if(search.text != "")
			{
        		NetMngr.GetSingleton().Send(InterfaceMain.SearchPlayer, new object[] {"2",search.text });
			}
        });
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
        ArrayList withList = data["friends"] as ArrayList;
        if (withList.Count==0)
        {
            if (withList.Count == 0)
            {
                n.gameObject.SetActive(true);
            }
            else
            {
                n.gameObject.SetActive(false);
            }
        }
        Object objItem = Resources.Load("HallItem/AddFriendTopPanelItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            AddFriendTopPanelItem addFriendTopPanelItem = go.AddComponent<AddFriendTopPanelItem>();
            addFriendTopPanelItem.SetData(withList[i] as Hashtable);
        }
    }

    void Start ()
    {
	
	}

	void Update ()
    {
        image.SetActive(ss.IsActive());
	}

    public override void OnAddComplete()
    {

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
