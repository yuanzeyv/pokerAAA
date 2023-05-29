using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerItem : MonoBehaviour {

	private CircleImage icon;
    private Text playerName;
    private Text playerState;
    private Button noSit;
    private Button noJoinRoom;
    private Button manageBtn;

    private string id;

    private void Awake()
    {
		icon = transform.Find("PlayerHead").GetComponent<CircleImage>();
        playerName = transform.Find("name").GetComponent<Text>();
        playerState = transform.Find("State").GetComponent<Text>();
        noSit = transform.Find("noSit").GetComponent<Button>();
        noJoinRoom = transform.Find("noJoinRoom").GetComponent<Button>();
        manageBtn = transform.Find("manageBtn").GetComponent<Button>();

        manageBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().playerManageListPopup.HideView();
            GameUIManager.GetSingleton().playerManagePopup.ShowView(noSit.interactable, noJoinRoom.interactable,id);
        });
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    public void SetData(Hashtable data)
    {
        id = data["userId"].ToString();
        playerName.text = data["nickName"].ToString();
        playerState.text = data["status"].ToString() == "1" ? "游戏中" : "旁观中";
        noSit.interactable = int.Parse(data["forbidSit"].ToString()) == 1 ? true : false;
        noJoinRoom.interactable = int.Parse(data["forbidEntry"].ToString()) == 1 ? true : false;
        GameTools.GetSingleton().GetTextureFromNet(data["headUrl"].ToString(), SetIcon);
    }
    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
