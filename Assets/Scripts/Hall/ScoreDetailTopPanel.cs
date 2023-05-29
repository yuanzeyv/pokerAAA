using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDetailTopPanel : BasePlane {

    private Button back;
    private Toggle[] toggles=new Toggle[2];
    private GameObject[] panels=new GameObject[2];
    private RawImage[] images=new RawImage[3];
    private Text Name;
    private Text Desk;
    private Text Time;
    private Text Mang;
    private Text ShouCount;
    private Text DiChi;
    private Text Buy;
    private Transform parent;
	private Text Insurance;

	private DataIcon ruChi;
	private DataIcon ruchiSheng;
	private DataIcon fanPai;
	private DataIcon tanPai;
	public Dictionary<int, DataIcon> dataicons = new Dictionary<int, DataIcon>();

    private Text jiJin;
    private Text shouShu;
    private Button benJuShouPai;
    private RawImage[] pai1=new RawImage[4];
    private Text score1;
    private RawImage[] pai2=new RawImage[4];
    private Text score2;
    private RawImage[] pai3=new RawImage[4];
    private Text score3;
    private RawImage[] pai4=new RawImage[4];
    private Text score4;
    private Button detail1;
    private Button detail2;
    private Button detail3;
    private Button detail4;
    private string id1;
    private string id2;
    private string id3;
    private string id4;


    public string id;
    private Hashtable data;

    private void Awake()
    {
		back = transform.Find("Top/Back/Share").GetComponent<Button>();
        toggles[0] = transform.Find("ToggleGroup/Toggle1").GetComponent<Toggle>();
        toggles[1] = transform.Find("ToggleGroup/Toggle2").GetComponent<Toggle>();
        panels[0] = transform.Find("1").gameObject;
        panels[1] = transform.Find("2").gameObject;
        images[0] = panels[0].transform.Find("Mvp/Icon/mask/RawImage").GetComponent<RawImage>();
        images[1] = panels[0].transform.Find("Mvp/Icon2/mask/RawImage").GetComponent<RawImage>();
        images[2] = panels[0].transform.Find("Mvp/Icon3/mask/RawImage").GetComponent<RawImage>();
//        images[3] = panels[0].transform.Find("Mvp/Icon4").GetComponent<CircleImage>();
        Name = panels[0].transform.Find("GameInfo/Name").GetComponent<Text>();
        Desk = panels[0].transform.Find("GameInfo/Desk").GetComponent<Text>();
        Time = panels[0].transform.Find("GameInfo/Time").GetComponent<Text>();
        Mang = panels[0].transform.Find("GameInfo/Mang").GetComponent<Text>();
        ShouCount = panels[0].transform.Find("MineInfo/ShouCount").GetComponent<Text>();
        DiChi = panels[0].transform.Find("MineInfo/DiChi").GetComponent<Text>();
        Buy = panels[0].transform.Find("MineInfo/Buy").GetComponent<Text>();
        parent = panels[0].transform.Find("Scroll View/Viewport/Content");

		Insurance = panels[0].transform.Find("MineInfo/insurance").GetComponent<Text>();
        Button insuranceBtn = panels[0].transform.Find("MineInfo/insuranceBtn").GetComponent<Button>();
        insuranceBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().insuranceDetailPanel.id = id;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().insuranceDetailPanel);
        });

        #region 2

		ruChi = panels[1].transform.Find("ThisData/ruchilv").GetComponent<DataIcon>();
		ruchiSheng = panels[1].transform.Find("ThisData/shenglv").GetComponent<DataIcon>();
		fanPai = panels[1].transform.Find("ThisData/fanpailv").GetComponent<DataIcon>();
		tanPai = panels[1].transform.Find("ThisData/tanpailv").GetComponent<DataIcon>();

		dataicons.Add (0, ruChi);
		dataicons.Add (1, ruchiSheng);
		dataicons.Add (2, fanPai);
		dataicons.Add (3, tanPai);


		jiJin = panels[1].transform.Find("ThisData/jijindu").GetComponent<Text>();
		shouShu = panels[1].transform.Find("ThisData/shoushu").GetComponent<Text>();
        benJuShouPai = panels[1].transform.Find("ThisPai/Title/Button").GetComponent<Button>();
		for(int i = 0; i < 4; i++)
		{
			pai1[i] = panels[1].transform.Find("ThisPai/1/Pai/" + (i+1).ToString()).GetComponent<RawImage>();
			pai2[i] = panels[1].transform.Find("ThisPai/2/Pai/" + (i+1).ToString()).GetComponent<RawImage>();
			pai3[i] = panels[1].transform.Find("ThisPai/3/Pai/" + (i+1).ToString()).GetComponent<RawImage>();
			pai4[i] = panels[1].transform.Find("ThisPai/4/Pai/" + (i+1).ToString()).GetComponent<RawImage>();
		}

        score1 = panels[1].transform.Find("ThisPai/1/Score").GetComponent<Text>();
        score2 = panels[1].transform.Find("ThisPai/2/Score").GetComponent<Text>();
        score3 = panels[1].transform.Find("ThisPai/3/Score").GetComponent<Text>();
        score4 = panels[1].transform.Find("ThisPai/4/Score").GetComponent<Text>();
        detail1 = panels[1].transform.Find("ThisPai/1").GetComponent<Button>();
        detail2 = panels[1].transform.Find("ThisPai/2").GetComponent<Button>();
        detail3 = panels[1].transform.Find("ThisPai/3").GetComponent<Button>();
        detail4 = panels[1].transform.Find("ThisPai/4").GetComponent<Button>();

        #endregion

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        toggles[0].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                panels[0].SetActive(true);
                panels[1].SetActive(false);
                if (!isOn)
                {
                    NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordDetail, new object[] { id });
                }
            }
        });
        toggles[1].onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                panels[0].SetActive(false);
                panels[1].SetActive(true);
                NetMngr.GetSingleton().Send(InterfaceMain.GetGameData, new object[] { id });
            }
        });
        benJuShouPai.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().theGamePaiTopPanel.id = this.id;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().theGamePaiTopPanel);
        });
        detail1.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().theGamePaiRecordTopPanel.titlePai = "盈利最多手牌";
            HallManager.GetSingleton().theGamePaiRecordTopPanel.id = id1;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().theGamePaiRecordTopPanel);
        });
        detail2.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().theGamePaiRecordTopPanel.titlePai = "亏损最多手牌";
            HallManager.GetSingleton().theGamePaiRecordTopPanel.id = id2;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().theGamePaiRecordTopPanel);
        });
        detail3.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().theGamePaiRecordTopPanel.titlePai = "所弃最大手牌";
            HallManager.GetSingleton().theGamePaiRecordTopPanel.id = id3;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().theGamePaiRecordTopPanel);
        });
        detail4.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().theGamePaiRecordTopPanel.titlePai = "亏损最大牌型";
            HallManager.GetSingleton().theGamePaiRecordTopPanel.id = id4;
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().theGamePaiRecordTopPanel);
        });
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SetData(Hashtable data)
    {
        GameTools.GetSingleton().GetTextureNet(data["mvpUrl"].ToString(), GetNetSprite1);
        images[0].transform.parent.parent.GetChild(1).GetComponent<Text>().text = data["mvpname"].ToString();
        GameTools.GetSingleton().GetTextureNet(data["fishUrl"].ToString(), GetNetSprite2);
        images[1].transform.parent.parent.GetChild(1).GetComponent<Text>().text = data["fishname"].ToString();
        GameTools.GetSingleton().GetTextureNet(data["richUrl"].ToString(), GetNetSprite3);
        images[2].transform.parent.parent.GetChild(1).GetComponent<Text>().text = data["richname"].ToString();
       // GameTools.GetSingleton().GetTextureFromNet(data["model"].ToString(), GetNetSprite4);
//        images[3].transform.GetChild(2).GetComponent<Text>().text = data["modelname"].ToString();
        Name.text = "创建者:"+data["createrName"].ToString();
        string[] strss = data["produceTime"].ToString().Split(' ');
        string[] strs1 = strss[0].Split('-');
        string[] strs2 = strss[1].Split(':');
        Time.text = "时间:" + strs1[1]+"/"+strs1[2]+"/"+strs2[0]+":"+strs2[1];
        Desk.text = "牌局:" + data["deskName"].ToString();
        Mang.text = "盲注:" + data["mang"].ToString();
        ShouCount.text = data["shoushu"].ToString();
        DiChi.text = data["diChi"].ToString();
        Buy.text = data["buy"].ToString();
		Insurance.text = data["insurance"].ToString();
        this.data = data;
        RefreshList(1);
    }

	public void ResetPai()
	{
		for (int k = 0; k < 4; k++) {
			pai1[k].texture = Resources.Load<Texture>("pai/card_back_2");
			pai2[k].texture = Resources.Load<Texture>("pai/card_back_2");
			pai3[k].texture = Resources.Load<Texture>("pai/card_back_2");
			pai4[k].texture = Resources.Load<Texture>("pai/card_back_2");

			if (k == 0 || k == 3) {
				pai1[k].gameObject.SetActive (false);
				pai2[k].gameObject.SetActive (false);
				pai3[k].gameObject.SetActive (false);
				pai4[k].gameObject.SetActive (false);
			}
		}

		score1.text = "0";
		score2.text = "0";
		score3.text = "0";
		score4.text = "0";

	}

    public void SetPaiData(Hashtable data)
    {
		ResetPai ();
		ruChi.Reset();
		ruchiSheng.Reset();
		fanPai.Reset();
		tanPai.Reset();
		ruChi.SetData(dataicons, float.Parse(data["ruChi"].ToString()),"入池率");
		ruchiSheng.SetData(dataicons, float.Parse(data["ruChiVictory"].ToString()), "入池胜率");
		fanPai.SetData(dataicons, float.Parse(data["fanPai"].ToString()), "翻牌率");
		tanPai.SetData(dataicons, float.Parse(data["tuiPai"].ToString()), "摊牌率");

        jiJin.text = "激进度:" + float.Parse(data["jiJin"].ToString());
        shouShu.text = "手数:" + float.Parse(data["shouShu"].ToString());
        ArrayList list = data["list"] as ArrayList;
        id1 = "";
        id2 = "";
        id3 = "";
        id4 = "";
        for (int i = 0; i < list.Count; i++)
        {
            if (((Hashtable)list[i])["type"].ToString()=="1")
            {
                id1 = ((Hashtable)list[i])["id"].ToString();
                score1.text = ((Hashtable)list[i])["score"].ToString();
                score1.color = StaticFunction.Getsingleton().GetColor(int.Parse(score1.text));
                string[] pais = ((Hashtable)list[i])["pai"].ToString().Split('|');

				if (pais.Length == 2) {

					pai1[1].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai1[2].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
				}

				if (pais.Length == 4) {
					pai1[0].gameObject.SetActive (true);
					pai1[3].gameObject.SetActive (true);

					pai1[0].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai1[1].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
					pai1[2].texture = StaticFunction.Getsingleton().SetPai(pais[2]);
					pai1[3].texture = StaticFunction.Getsingleton().SetPai(pais[3]);

				}

                detail1.interactable = true;
            }
            else if (((Hashtable)list[i])["type"].ToString() == "2")
            {
                id2 = ((Hashtable)list[i])["id"].ToString();
                score2.text = ((Hashtable)list[i])["score"].ToString();
                score2.color = StaticFunction.Getsingleton().GetColor(int.Parse(score2.text));
                string[] pais = ((Hashtable)list[i])["pai"].ToString().Split('|');

				if (pais.Length == 2) {
					pai2[1].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai2[2].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
				}

				if (pais.Length == 4) {
					pai2[0].gameObject.SetActive (true);
					pai2[3].gameObject.SetActive (true);

					pai2[0].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai2[1].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
					pai2[2].texture = StaticFunction.Getsingleton().SetPai(pais[2]);
					pai2[3].texture = StaticFunction.Getsingleton().SetPai(pais[3]);

				}

                detail2.interactable = true;
            }
            else if (((Hashtable)list[i])["type"].ToString() == "3")
            {
                id3 = ((Hashtable)list[i])["id"].ToString();
                score3.text = ((Hashtable)list[i])["score"].ToString();
                score3.color = StaticFunction.Getsingleton().GetColor(int.Parse(score3.text));
                string[] pais = ((Hashtable)list[i])["pai"].ToString().Split('|');
		
				if (pais.Length == 2) {
					pai3[1].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai3[2].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
				}

				if (pais.Length == 4) {
					pai3[0].gameObject.SetActive (true);
					pai3[3].gameObject.SetActive (true);

					pai3[0].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai3[1].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
					pai3[2].texture = StaticFunction.Getsingleton().SetPai(pais[2]);
					pai3[3].texture = StaticFunction.Getsingleton().SetPai(pais[3]);

				}

                detail3.interactable = true;
            }
            else if(((Hashtable)list[i])["type"].ToString() == "4")
            {
                id4 = ((Hashtable)list[i])["id"].ToString();
                score4.text = ((Hashtable)list[i])["score"].ToString();
                score4.color = StaticFunction.Getsingleton().GetColor(int.Parse(score4.text));
                string[] pais = ((Hashtable)list[i])["pai"].ToString().Split('|');

				if (pais.Length == 2) {
					pai4[1].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai4[2].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
				}

				if (pais.Length == 4) {
					pai4[0].gameObject.SetActive (true);
					pai4[3].gameObject.SetActive (true);

					pai4[0].texture = StaticFunction.Getsingleton().SetPai(pais[0]);
					pai4[1].texture = StaticFunction.Getsingleton().SetPai(pais[1]);
					pai4[2].texture = StaticFunction.Getsingleton().SetPai(pais[2]);
					pai4[3].texture = StaticFunction.Getsingleton().SetPai(pais[3]);

				}
                detail4.interactable = true;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (id1=="")
            {
                detail1.interactable = false;
            }
            else if (id2=="")
            {
                detail2.interactable = false;
            }
            else if (id3 == "")
            {
                detail3.interactable = false;
            }
            else if (id4 == "")
            {
                detail4.interactable = false;
            }
        }
        //score1.text = data["VictoryPai"].ToString();
        //score2.text = data["failPai"].ToString();
        //score3.text = data["dropPai"].ToString();
        //score4.text = data["lossPai"].ToString();
        //RefreshList(2);
    }
    private void GetNetSprite1(Texture sprite)
    {
        images[0].texture = sprite;
    }
    private void GetNetSprite2(Texture sprite)
    {
        images[1].texture = sprite;
    }
    private void GetNetSprite3(Texture sprite)
    {
        images[2].texture = sprite;
    }
//    private void GetNetSprite4(Sprite sprite)
//    {
//       images[3].sprite = sprite;
//    }
//
    /// <summary>
    /// 清空列表
    /// </summary>
    private void ClearList()
    {
        if (data == null)
            return;
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// 刷新列表
    /// </summary>
    private void RefreshList(int value)
    {
        if (data == null)
            return;
        ClearList();
        if (value==1)
        {
            ArrayList withList = data["GameRecordDetailLog"] as ArrayList;
            if (withList.Count == 0)
            {
                return;
            }
            Object objItem = Resources.Load("HallItem/PaiZhanJiItem");
            for (int i = 0; i < withList.Count; i++)
            {
                GameObject go = Instantiate(objItem) as GameObject;
                go.transform.SetParent(parent);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
                PaiZhanJiItem paiZhanJiItem = go.AddComponent<PaiZhanJiItem>();
                paiZhanJiItem.SetData(withList[i] as Hashtable);
            }
        }
        else
        {
            //ArrayList withList = data["GameData"] as ArrayList;
            //if (withList.Count == 0)
            //{
            //    return;
            //}
            //Object objItem = Resources.Load("HallItem/ZhuiPaiItem");
            //for (int i = 0; i < withList.Count; i++)
            //{
            //    GameObject go = Instantiate(objItem) as GameObject;
            //    go.transform.SetParent(parent);
            //    go.transform.localScale = Vector3.one;
            //    go.transform.localPosition = Vector3.zero;
            //    ZhuiPaiItem zhuiPaiItem = go.AddComponent<ZhuiPaiItem>();
            //    zhuiPaiItem.SetData(withList[i] as Hashtable);
            //}
        }
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetGameRecordDetail, new object[] { id });
    }

    public override void OnAddStart()
    {
        toggles[0].isOn = true;
        toggles[1].isOn = false;
    }

    public override void OnRemoveComplete()
    {
        
    }

    public override void OnRemoveStart()
    {

    }
}
