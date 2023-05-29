using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSharePopup: BasePopup {

    private Button saveBtn;
    private Button shareBtn;

    private Text roomName;
    private Text time;
    private Text id;
    private Text pepole;
    private Text chouma;
   

    private void Awake()
    {
        Init();
		roomName = transform.Find("Info/Text_name").GetComponent<Text>();
		time = transform.Find("Info/Text_time").GetComponent<Text>();
		id = transform.Find("Info/Text_Id").GetComponent<Text>();
		pepole = transform.Find("Info/Text_renshu").GetComponent<Text>();
		chouma = transform.Find("Info/Text_chouma").GetComponent<Text>();
       
		saveBtn = transform.Find("SaveBtn").GetComponent<Button>();
		shareBtn = transform.Find("ShareBtn").GetComponent<Button>();

		saveBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			GameTools.GetSingleton().CaptureScreenshot();
    
        });
		shareBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			GameTools.GetSingleton().CaptureScreenshot();
        });
      
    }

    public void SetData(Hashtable data)
    {
       
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
      
	}

    public void ShowView()
    {
        base.ShowView();
        this.IsShowMask = false;

		roomName.text = GameUIManager.GetSingleton().club.text;
		time.text = GameUIManager.GetSingleton().time.text;
		id.text = GameUIManager.GetSingleton().playerName.text;
		pepole.text = GameUIManager.GetSingleton().strplayercount;
		chouma.text = GameUIManager.GetSingleton().chouma.text;
		base.ShowView();

    }
}
