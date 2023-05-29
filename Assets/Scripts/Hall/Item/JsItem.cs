using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JsItem : MonoBehaviour {

    private Text playerName;
    private Text Score;
    private Text paixing;

    private void Awake()
    {
        playerName = transform.Find("Name").GetComponent<Text>();
        Score = transform.Find("Name/Score").GetComponent<Text>();
        paixing = transform.Find("Name/PaiXing").GetComponent<Text>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData(Hashtable data)
    {
        playerName.text = data["playerName"].ToString();
        Score.text = data["score"].ToString();
        Score.color = StaticFunction.Getsingleton().GetColor(int.Parse(data["score"].ToString()));
        paixing.text = data["paiXing"].ToString();
    }
}
