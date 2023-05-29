using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRecordItem : MonoBehaviour {

    private RawImage icon;
    private Text deskName;
    private Text chouma;
    private Text time;
    private Text Info;
    private Text score;
    private Text type;

    private void Awake()
    {
        icon = transform.Find("RawImage/Image").GetComponent<RawImage>();
        deskName = transform.Find("DeskName").GetComponent<Text>();
        chouma = transform.Find("Chouma/value").GetComponent<Text>();
        time = transform.Find("Time/value").GetComponent<Text>();
        Info = transform.Find("Info").GetComponent<Text>();
        score = transform.Find("Score").GetComponent<Text>();
        type = transform.Find("Type").GetComponent<Text>();
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SetData(Hashtable data)
    {
        GameTools.GetSingleton().GetTextureNet(data["url"].ToString(), GetNetSprite);
        //数据获取
    }
    private void GetNetSprite(Texture sprite)
    {
        icon.texture = sprite;
    }
}
