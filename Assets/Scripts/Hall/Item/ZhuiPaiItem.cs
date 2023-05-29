using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZhuiPaiItem : MonoBehaviour {

    private Text value1;
    private Text value2;
    private Text value3;
    private Text value4;

    private Hashtable data;

    private void Awake()
    {
        value1 = transform.Find("value1").GetComponent<Text>();
        value2 = transform.Find("value2").GetComponent<Text>();
        value3 = transform.Find("value3").GetComponent<Text>();
        value4 = transform.Find("value4").GetComponent<Text>();

    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SetData(Hashtable data)
    {
        this.data = data;
        value1.text = data["playerName"].ToString();
        value2.text = data["playerBuy"].ToString();
        value3.text = data["playerShouShu"].ToString();
        value4.text = data["playerScore"].ToString();
    }

}
