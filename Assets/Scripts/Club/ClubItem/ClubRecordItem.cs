using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClubRecordItem : MonoBehaviour {


   
	public RawImage head;
    public Text myName;
  
    public Text blinds;
   
    public Text playTime;
    public Text endTime;

    public Text win;
    public Text lose;

    public string paijuId;

    void Awake()
    {
		head = transform.Find("Headmask/head").GetComponent<RawImage>();
        myName = transform.Find("myName").GetComponent<Text>();
       
		blinds = transform.Find("Blinds/value").GetComponent<Text>();
      
        playTime = transform.Find("Time").GetComponent<Text>();
        endTime = transform.Find("endTime").GetComponent<Text>();

        win= transform.Find("Win").GetComponent<Text>();
        lose= transform.Find("Lose").GetComponent<Text>();

        GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().scoreDetailTopPanel.id = paijuId;
            Debug.Log("点击");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().scoreDetailTopPanel);

        });
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    public void GetSprtie(Sprite s)
    {

        if (s != null)
        {
            head.sprite = s;
        }
    }*/
    public void GetSprtie(Texture s)
    {

        if (s != null)
        {
            head.texture = s;
        }
    }

    public void SetInfo(string paijuId, string url, string deskname, string strblinds, string playTime, string endTime, string score,string type)
    {
        this.paijuId = paijuId;

        GameTools.GetSingleton().GetTextureNet(url, GetSprtie);
        myName.text = deskname;
		this.blinds.text = strblinds;
        this.playTime.text = playTime;
        this.endTime.text = endTime;

        if (int.Parse(score) >= 0)
        {
           // win.text = "+"+score;
            win.text =  score;
            win.gameObject.SetActive(true);
            lose.gameObject.SetActive(false);
        }
        else {

            lose.text =  score;
            win.gameObject.SetActive(false);
            lose.gameObject.SetActive(true);
        }
    }
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
