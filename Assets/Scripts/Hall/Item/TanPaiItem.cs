using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TanPaiItem : MonoBehaviour {

    private Text playerName;
    private RawImage pai1;
    private RawImage pai2;

    private void Awake()
    {
        playerName = transform.Find("Name").GetComponent<Text>();
        pai1 = transform.Find("Name/Pai/1").GetComponent<RawImage>();
        pai2 = transform.Find("Name/Pai/2").GetComponent<RawImage>();
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
        string[] str = data["tanpai"].ToString().Split('|');
        pai1.texture = StaticFunction.Getsingleton().SetPai(str[0]);
        pai2.texture = StaticFunction.Getsingleton().SetPai(str[1]);
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
