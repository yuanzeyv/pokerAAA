using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoticeContentTopPanel : BasePlane {


    private Button back;
    public  Text title;
    public Text content;

    public string id;
    public string type;

    private void Awake()
    {
        back = transform.Find("Top/Back").GetComponent<Button>();
        title = transform.Find("Title").GetComponent<Text>();
        content = transform.Find("Content").GetComponent<Text>();

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
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
        title.text = data["title"].ToString();
        content.text = data["content"].ToString();
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetNoticeContent, new object[] { type, id });
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
