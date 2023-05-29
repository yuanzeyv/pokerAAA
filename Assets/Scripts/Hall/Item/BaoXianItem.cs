using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaoXianItem : MonoBehaviour {


    private Text XGDC;
    private RawImage[] LXWJ = new RawImage[2];
    private Text PL;
    private Text GMJE;
    private RawImage[] GM;
    private Text PFJE;
    private Transform parent;
    private Transform qbparent;
    private Transform buyParent;
    private Text lxwjName;

    private void Awake()
    {
        parent = transform.Find("GameInfo");
        LXWJ = transform.Find("GameInfo/LXWJ/Pai").GetComponentsInChildren<RawImage>();
        PL = transform.Find("GameInfo/PL/Value").GetComponent<Text>();
        GM = transform.Find("GameInfo/GM/Pai").GetComponentsInChildren<RawImage>();
        PFJE = transform.Find("GameInfo/PFJE/Value").GetComponent<Text>();
        qbparent = transform.Find("GameInfo/QB/Pai");
        buyParent = transform.Find("GameInfo/GM/Pai");
        XGDC = transform.Find("GameInfo/XGDC/Value").GetComponent<Text>();
        GMJE = transform.Find("GameInfo/GMJE/Value").GetComponent<Text>();
        lxwjName = transform.Find("GameInfo/LXWJ").GetComponent<Text>();
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SetData(Hashtable data)
    {
        XGDC.text = data["diChi"].ToString();
        string[] lxwj = data["mvpCards"].ToString().Split('|');
        lxwjName.text = "领先玩家:"+data["mvpPlayer"].ToString();
        LXWJ[0].texture = StaticFunction.Getsingleton().SetPai(lxwj[0]);
        LXWJ[1].texture = StaticFunction.Getsingleton().SetPai(lxwj[1]);
        ArrayList lhwj = data["failPlayers"] as ArrayList;
        if (lhwj.Count == 0)
            return;
        Object objItem = Resources.Load("HallItem/LHWJ");
        for (int i = 0; i < lhwj.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parent);
            go.transform.SetSiblingIndex(2);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            LHWJ lHWJ = go.AddComponent<LHWJ>();
            lHWJ.SetData(lhwj[i] as Hashtable);
        }
        string[] qb = data["outs"].ToString().Split('|');
        Object qbs = Resources.Load("HallItem/qbItem");
        for (int i = 0; i < qb.Length; i++)
        {
            GameObject go = Instantiate(qbs) as GameObject;
            go.transform.SetParent(qbparent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<RawImage>().texture = StaticFunction.Getsingleton().SetPai(qb[i]);
        }
        PL.text = data["odds"].ToString();
        GMJE.text = data["gold"].ToString();
        string[] gm = data["buyOuts"].ToString().Split('|');
        Object gms = Resources.Load("HallItem/qbItem");
        for (int i = 0; i < gm.Length; i++)
        {
            GameObject go = Instantiate(gms) as GameObject;
            go.transform.SetParent(buyParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<RawImage>().texture = StaticFunction.Getsingleton().SetPai(gm[i]);
        }
        PFJE.text = data["oddsGold"].ToString();
    }
}
