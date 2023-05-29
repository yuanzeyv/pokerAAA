using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpItem : MonoBehaviour {

    private Text playerName;
    private Text Op;
    private Text Score;

    private void Awake()
    {
        playerName = transform.Find("Name").GetComponent<Text>();
        Op = transform.Find("Name/Op").GetComponent<Text>();
        Score = transform.Find("Name/Score").GetComponent<Text>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData(Hashtable data)
    {
        playerName.text = data["name"].ToString();
        Op.text = GetOp(data["op"].ToString());
        Score.text = data["chouma"].ToString();
    }

    public string GetOp(string str)
    {
        string ss = "";
        switch (str)
        {
            case "1":
                ss = "弃牌";
                break;
            case "2":
                ss = "跟注";
                break;
            case "3":
                ss = "看牌";
                break;
            case "4":
                ss = "全下";
                break;
            case "5":
                ss = "加注";
                break;
        }
        return ss;
    }
}
