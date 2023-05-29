using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheGamePaiRecordTopPanel : BasePlane {

    private Button back;
    private RawImage pai1;
    private RawImage pai2;
    private Text title;
    private Text chouma;
    private Text time;
    private Text score;
    private RawImage[] fanpais;
    private RawImage zhuanpais;
    private RawImage hepais;

    private Transform parent;
    private Transform infoParent;
    private Transform mangParent;
    private Transform fanPaiQianParent;
    private Transform fanPaiParent;
    private Transform zhuanPaiParent;
    private Transform tanPaiParent;
    private Transform hpParent;
    private Transform jsParent;

    private GameObject[] gos=new GameObject[3];

    public string titlePai;
    public string id;


    private void Awake()
    {
        back = transform.Find("Top/Back").GetComponent<Button>();
        pai1 = transform.Find("Yingli/Pai1").GetComponent<RawImage>();
        pai2 = transform.Find("Yingli/Pai2").GetComponent<RawImage>();
        title = transform.Find("Yingli/Title").GetComponent<Text>();
        chouma = transform.Find("Yingli/Chouma/value").GetComponent<Text>();
        time = transform.Find("Yingli/Time/value").GetComponent<Text>();
        score = transform.Find("Yingli/Score").GetComponent<Text>();

        parent = transform.Find("infoPanel/Viewport/Content");
        infoParent = parent.Find("Info");
        mangParent = parent.Find("Mang");
        fanPaiQianParent = parent.Find("FanPaiQian");
        fanPaiParent = parent.Find("FanPai");
        zhuanPaiParent = parent.Find("ZhuanPai");
        tanPaiParent = parent.Find("TanPai");
        hpParent = parent.Find("HP");
        jsParent = parent.Find("JS");

        fanpais = fanPaiParent.Find("Title/Pai").GetComponentsInChildren<RawImage>();
        zhuanpais = zhuanPaiParent.Find("Title/Pai/1").GetComponent<RawImage>();
        hepais = hpParent.Find("Title/Pai/1").GetComponent<RawImage>();


        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData(Hashtable data)
    {
        title.text = titlePai;
        chouma.text = data["chouma"].ToString();
        time.text = data["time"].ToString();
        score.text = data["score"].ToString();
        score.color = StaticFunction.Getsingleton().GetColor(int.Parse(data["score"].ToString()));
        string[] pais = data["currPai"].ToString().Split('|');
        pai1.texture = StaticFunction.Getsingleton().SetPai(pais[0]);
        pai2.texture = StaticFunction.Getsingleton().SetPai(pais[1]);
        string[] pais1 = data["fanPai"].ToString().Split('|');
        if (string.IsNullOrEmpty(data["fanPai"].ToString()))
        {
            for (int i = 0; i < fanpais.Length; i++)
            {
                fanpais[i].gameObject.SetActive(false);
            }
        }
        else
        {
            switch (pais1.Length)
            {
                case 1:
                    fanpais[0].texture = StaticFunction.Getsingleton().SetPai(pais1[0]);
                    for (int i = 1; i < fanpais.Length; i++)
                    {
                        fanpais[i].gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    fanpais[0].texture = StaticFunction.Getsingleton().SetPai(pais1[0]);
                    fanpais[1].texture = StaticFunction.Getsingleton().SetPai(pais1[1]);
                    fanpais[2].gameObject.SetActive(false);
                    break;
                case 3:
                    fanpais[0].texture = StaticFunction.Getsingleton().SetPai(pais1[0]);
                    fanpais[1].texture = StaticFunction.Getsingleton().SetPai(pais1[1]);
                    fanpais[2].texture = StaticFunction.Getsingleton().SetPai(pais1[2]);
                    break;
            }
        }
        zhuanpais.texture = StaticFunction.Getsingleton().SetPai(data["zhuanPai"].ToString());
        hepais.texture = StaticFunction.Getsingleton().SetPai(data["hePai"].ToString());
        string[] str = data["mang"].ToString().Split('|');
        //infoParent.Find("Title").GetComponent<Text>().text = "(盲注:" + str[0] + "/" + str[1] + " 前注:" + str[2] + ")";
        ArrayList list = data["positionInfo"] as ArrayList;
        ClearList(infoParent,1);
        Object objItem = Resources.Load("HallItem/InfoItem");
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(infoParent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            InfoItem infoItem = go.AddComponent<InfoItem>();
            infoItem.SetData(list[i] as Hashtable);
        }
        ClearList(mangParent, 1);
        ClearList(fanPaiQianParent, 1);
        ClearList(fanPaiParent, 1);
        ClearList(zhuanPaiParent, 1);
        ClearList(hpParent, 1);
        ClearList(tanPaiParent, 1);
        ArrayList list1 = data["operation"] as ArrayList;
        for (int i = 0; i < list1.Count; i++)
        {
            switch (((Hashtable)list1[i])["type"].ToString())
            {
                case "1"://盲注
                    Object mangitem = Resources.Load("HallItem/MangItem");
                    GameObject go = Instantiate(mangitem) as GameObject;
                    go.transform.SetParent(mangParent);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    mangItem mangItem = go.AddComponent<mangItem>();
                    mangItem.SetData(list1[i] as Hashtable);
                    break;
                case "2"://翻牌前
                    Object fanpaiqianItem = Resources.Load("HallItem/FanPaiQianItem");
                    GameObject go2 = Instantiate(fanpaiqianItem) as GameObject;
                    go2.transform.SetParent(fanPaiQianParent);
                    go2.transform.localScale = Vector3.one;
                    go2.transform.localPosition = Vector3.zero;
                    FanpaiQianItem FanpaiqianItem = go2.AddComponent<FanpaiQianItem>();
                    FanpaiqianItem.SetData(list1[i] as Hashtable);
                    break;
                case "3"://翻牌
                    Object fanpaiItem = Resources.Load("HallItem/FanPaiItem");
                    GameObject go3 = Instantiate(fanpaiItem) as GameObject;
                    go3.transform.SetParent(fanPaiParent);
                    go3.transform.localScale = Vector3.one;
                    go3.transform.localPosition = Vector3.zero;
                    FanPaiItem FanPaiItem = go3.AddComponent<FanPaiItem>();
                    FanPaiItem.SetData(list1[i] as Hashtable);
                    break;
                case "4"://转牌
                    Object zhuanpaiItem = Resources.Load("HallItem/ZhuanPaiItme");
                    GameObject go4 = Instantiate(zhuanpaiItem) as GameObject;
                    go4.transform.SetParent(zhuanPaiParent);
                    go4.transform.localScale = Vector3.one;
                    go4.transform.localPosition = Vector3.zero;
                    ZhuanPaiItem ZhuanPaiItem = go4.AddComponent<ZhuanPaiItem>();
                    ZhuanPaiItem.SetData(list1[i] as Hashtable);
                    break;
                case "5"://河牌
                    Object hepaiItem = Resources.Load("HallItem/HpItem");
                    GameObject go5 = Instantiate(hepaiItem) as GameObject;
                    go5.transform.SetParent(hpParent);
                    go5.transform.localScale = Vector3.one;
                    go5.transform.localPosition = Vector3.zero;
                    HpItem hePaiItem = go5.AddComponent<HpItem>();
                    hePaiItem.SetData(list1[i] as Hashtable);
                    break;
                case "6"://摊牌
                    Object tanpaiItem = Resources.Load("HallItem/TanPaiItem");
                    GameObject go6 = Instantiate(tanpaiItem) as GameObject;
                    go6.transform.SetParent(tanPaiParent);
                    go6.transform.localScale = Vector3.one;
                    go6.transform.localPosition = Vector3.zero;
                    TanPaiItem TanpaiItem = go6.AddComponent<TanPaiItem>();
                    TanpaiItem.SetData(list1[i] as Hashtable);
                    break;
            }
        }
        ArrayList insurances = data["insurance"] as ArrayList;
        //ClearList(baoXianParent);
        if (gos[0] != null)
        {
            Destroy(gos[0]);
            gos[0] = null;
        }
        if (gos[1] != null)
        {
            Destroy(gos[1]);
            gos[1] = null;
        }
        if (gos[2] != null)
        {
            Destroy(gos[2]);
            gos[2] = null;
        }
        Object baoxian = Resources.Load("HallItem/BaoXian");
        for (int i = 0; i < insurances.Count; i++)
        {
            switch (((Hashtable)insurances[i])["type"].ToString())
            {
                case "1":
                    GameObject go = Instantiate(baoxian) as GameObject;
                    go.transform.SetParent(parent);
                    go.transform.SetSiblingIndex(4);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    BaoXianItem baoXianItem = go.AddComponent<BaoXianItem>();
                    baoXianItem.SetData(insurances[i] as Hashtable);
                    gos[0] = go;
                    break;
                case "2":
                    GameObject go1 = Instantiate(baoxian) as GameObject;
                    go1.transform.SetParent(parent);
                    go1.transform.SetSiblingIndex(6);
                    go1.transform.localScale = Vector3.one;
                    go1.transform.localPosition = Vector3.zero;
                    BaoXianItem baoXianItem1 = go1.AddComponent<BaoXianItem>();
                    baoXianItem1.SetData(insurances[i] as Hashtable);
                    gos[1] = go1;
                    break;
                case "3":
                    GameObject go2 = Instantiate(baoxian) as GameObject;
                    go2.transform.SetParent(parent);
                    go2.transform.SetSiblingIndex(8);
                    go2.transform.localScale = Vector3.one;
                    go2.transform.localPosition = Vector3.zero;
                    BaoXianItem baoXianItem2 = go2.AddComponent<BaoXianItem>();
                    baoXianItem2.SetData(insurances[i] as Hashtable);
                    gos[2] = go2;
                    break;
            }
        }
        ClearList(jsParent, 1);
        ArrayList list9 = data["result"] as ArrayList;
        Object jsItem = Resources.Load("HallItem/JsItem");
        for (int i = 0; i < list9.Count; i++)
        {
            GameObject go9 = Instantiate(jsItem) as GameObject;
            go9.transform.SetParent(jsParent);
            go9.transform.localScale = Vector3.one;
            go9.transform.localPosition = Vector3.zero;
            JsItem jsItemm = go9.AddComponent<JsItem>();
            jsItemm.SetData(list9[i] as Hashtable);
        }
    }
    /// <summary>
    /// 清空列表
    /// </summary>
    private void ClearList(Transform parent,int index)
    {
        int childCount = parent.childCount;
        for (int i = index; i < childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GamePaiReview, new object[] { id });
    }

    public override void OnAddStart()
    {
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {
    }
}
