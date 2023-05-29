using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoItem : MonoBehaviour {

    private Text positionName;
    private Text playerName;
    private Text Score;
    private RawImage pai1;
    private RawImage pai2;

    private void Awake()
    {
        positionName =transform.Find("MP").GetComponent<Text>();
        playerName = transform.Find("MP/Name").GetComponent<Text>();
        Score = transform.Find("MP/Score").GetComponent<Text>();
        pai1 = transform.Find("MP/Pai/1").GetComponent<RawImage>();
        pai2 = transform.Find("MP/Pai/2").GetComponent<RawImage>();
    }

    public void SetData(Hashtable data)
    {
        positionName.text = data["positionID"].ToString();
        playerName.text = data["name"].ToString();
        Score.text = data["scoreRecord"].ToString();
        string[] str = data["paiType"].ToString().Split('|');
        pai1.texture = StaticFunction.Getsingleton().SetPai(str[0]);
        pai2.texture = StaticFunction.Getsingleton().SetPai(str[1]);
    }

	void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}
}
