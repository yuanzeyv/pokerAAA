using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecordItem : MonoBehaviour {

    public Image card1;
    public Image card2;
    public Text message;
    public Text winorlose;
    public Text daxiaomangzhu;
    public Text deskWin;

    void Awake() {
        card1 = transform.Find("card1").GetComponent<Image>();
        card2 = transform.Find("card2").GetComponent<Image>();
        message = transform.Find("message").GetComponent<Text>();
        winorlose= transform.Find("winorlose").GetComponent<Text>();
        daxiaomangzhu= transform.Find("daxiaomangzhu").GetComponent<Text>();
        deskWin= transform.Find("deskWin/Text").GetComponent<Text>();


    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInfo(string card,string winnerName,string winType,string daxiaomangzhu,string winorlose,string deskWin) {
        //card
        //winType
        if (winType == "")
        {
            message.text = "对手弃牌，" + winnerName + "获得胜利";
        }
        else {
            message.text =   winnerName + " 以 <color=#FFFFFF>"+winType+"</color>"+"获得胜利";
        }

        this.winorlose.text = winorlose;
        this.daxiaomangzhu.text = daxiaomangzhu ;
        this.deskWin.text = deskWin;


    }

}
