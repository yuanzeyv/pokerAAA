using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddFriendTopPanelItem : MonoBehaviour {

    private RawImage icon;
    private Text Name;
    private Button add;
    private Button self;
    private bool isFriend = false;
    private string id;
    private Hashtable data;

    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<RawImage>();
        Name = transform.Find("Name").GetComponent<Text>();
        add = transform.Find("Add").GetComponent<Button>();
        self = GetComponent<Button>();

        add.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isFriend)
            {
                PopupCommon.GetSingleton().ShowView("该玩家已经是好友,请勿重复添加!");
            }
            else
            {
                HallManager.GetSingleton().addFriendInfoTopPanel.id = id;
                HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().addFriendInfoTopPanel);
            }
            
        });
        self.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
        });
    }

    public void SetData(Hashtable data)
    {
        this.data = data;
        GameTools.GetSingleton().GetTextureNet(data["playerURL"].ToString(), GetNetSprite);
        id = data["playerID"].ToString();
        Name.text = data["playerName"].ToString();
        if (data.ContainsKey("isFriend"))
        {
            isFriend = data["isFriend"].ToString() == "1" ? true : false;
        }
    }
    private void GetNetSprite(Texture sprite)
    {
        icon.texture = sprite;
    }


    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
