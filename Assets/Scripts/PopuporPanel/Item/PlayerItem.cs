using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour {

    public Text playerName;
    public Text dairu;
    public Text coin;
    public Text OperationCount;

    void Awake() {
        playerName = transform.Find("name").GetComponent<Text>();
        dairu = transform.Find("dairu").GetComponent<Text>();
        coin = transform.Find("coinBg/coin").GetComponent<Text>();
        OperationCount = transform.Find("OperationCount").GetComponent<Text>();

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string SetName(string n)
    {
        string str = "";
        if (n.Length > 8)
        {
            str = n.Substring(0, 8) + "..";

        }
        else
        {
            str = n;
        }
        return str;
    }


    public void SetInfo(string playerName,string dairu,string coin,string count) {
        this.playerName.text = SetName(playerName);
        this.dairu.text = dairu;
        this.OperationCount.text = count;
        if (int.Parse(coin) > 0)
        {
            this.coin.text = "+" + coin;
            this.coin.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/损失BG");
        }
        else {
            this.coin.text =  coin;
            this.coin.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/盈利BG");
        }
    }

    public void DelSelf() {
        Destroy(gameObject);
    }
}
