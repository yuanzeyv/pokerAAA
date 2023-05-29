using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManageListPopup : BasePopup
{

    private Button back;
    private Transform parent;

    private Hashtable data;

    private void Awake()
    {
        Init();
        back = transform.Find("CloseBtn").GetComponent<Button>();
        parent = transform.Find("Scroll View/Viewport/Content");

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HideView();
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
        ArrayList withList = data["userList"] as ArrayList;
        Object objItem = Resources.Load("GamePopup/GamePopupItem/playerItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            playerItem playerItem = go.AddComponent<playerItem>();
            playerItem.SetData(withList[i] as Hashtable);
        }
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void ShowView()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.getRoomUserList, new object[] { });
        base.ShowView();
    }
}
