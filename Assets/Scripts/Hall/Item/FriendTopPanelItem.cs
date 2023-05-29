using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FriendTopPanelItem : MonoBehaviour {

    private RawImage icon;
    private Text Name;
    private Button self;
    private bool isFriend=false;

    private string id;
    private Hashtable data;

    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<RawImage>();
        Name = transform.Find("Name").GetComponent<Text>();
        self = GetComponent<Button>();
        self.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().friendDetailTopPanel.id = id;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().friendDetailTopPanel);
        });
        
    }

    public void SetData(Hashtable data)
    {
        this.data = data;
        GameTools.GetSingleton().GetTextureNet(data["playerURL"].ToString(), GetNetSprite);
        id = data["playerID"].ToString();
        string name = data["playerName"].ToString();
        // string remark = 
        Name.text = data["playerName"].ToString();
        if (data.ContainsKey("isFriend"))
        {
            isFriend=data["isFriend"].ToString() == "1" ? true : false;
        }
    }

    private void GetNetSprite(Texture sprite)
    {
        icon.texture = sprite;
    }



    void Start()
    {

    }

    void Update()
    {

    }
}
