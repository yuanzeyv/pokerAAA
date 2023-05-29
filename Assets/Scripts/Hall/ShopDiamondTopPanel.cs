using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopDiamondTopPanel : BasePlane {

    private Button backBtn;
    private Text residue;
	private Text zhuanshi;

    private Text[] value1 = new Text[7];
    private Text[] miaoshu = new Text[7];
    private Text[] value2 = new Text[7];
    private Button[] value3 = new Button[7];
    private string[] id = new string[7];

	private Text name;
	private RawImage head;

    ArrayList list = new ArrayList();

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
		residue = transform.Find("Residue/value1").GetComponent<Text>();
		zhuanshi = transform.Find("Residue/value2").GetComponent<Text>();
		head =  transform.Find("head/mask").GetComponent<RawImage>();
		name = transform.Find("head/Text").GetComponent<Text>();



        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
			if( HallManager.GetSingleton() != null){
				HallManager.GetSingleton().planeManager.RemoveTopPlane();}
			//else{
			//	this.gameObject.SetActive(false);
   //             this.transform.parent.gameObject.SetActive(false);
			//}
        });
        for (int i = 0; i < value1.Length; i++)
        {
            value1[i] = transform.Find(i + 1 + "/Text").GetComponent<Text>();
        }
        for (int i = 0; i < miaoshu.Length; i++)
        {
            miaoshu[i] = transform.Find(i + 1 + "/miaoshu").GetComponent<Text>();
            miaoshu[i].text = "";
        }
        
        for (int i = 0; i < value2.Length; i++)
        {
            value2[i] = transform.Find(i + 1 + "/Button/Text").GetComponent<Text>();
        }
        for (int i = 0; i < value3.Length; i++)
        {
            value3[i] = transform.Find(i + 1 + "/Button").GetComponent<Button>();
        }
        value3[0].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[0]);
            }
            else {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[0]);
            }
          
        }
        );
        value3[1].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[1]);
            }
            else
            {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[1]);
            }
        });
        value3[2].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[2]);
            }
            else
            {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[2]);
            }
        });
        value3[3].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[3]);
            }
            else
            {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[3]);
            }

        });
        value3[4].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[4]);
            }
            else
            {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[4]);
            }
        });
        value3[5].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[5]);
            }
            else
            {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[5]);
            }
        });
        value3[6].onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (HallManager.GetSingleton() != null)
            {
                HallManager.GetSingleton().selectPayPopup.ShowView(id[6]);
            }
            else
            {
                GameUIManager.GetSingleton().selectPayPopup.ShowView(id[6]);
            }

        });

		residue.text = StaticData.gold.ToString();
		zhuanshi.text = StaticData.diamond.ToString ();
		name.text = StaticData.username.ToString();
		head.texture = HallManager.GetSingleton().personalCenterBottomPanel.icon.texture;
    }

    public void DiamondFinished(Hashtable data)
    {
        list = data["list"] as ArrayList;
        for (int i = 0; i < list.Count; i++)
        {
            id[i] = ((Hashtable)list[i])["id"].ToString();
            value1[i].text = ((Hashtable)list[i])["count"].ToString()+"钻石";
			value2[i].text = "¥ " + ((Hashtable)list[i])["costGold"].ToString();
        }
		residue.text = StaticData.gold.ToString();
		zhuanshi.text = StaticData.diamond.ToString ();
    }

    public void BuyDiamondFinished(Hashtable data)
    {
        Application.OpenURL(data["url"].ToString());
    }

    void Start()
    {

    }

    void OnEnable()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetShopInfo, new object[] { "2" });
    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetShopInfo, new object[] { "2" });
    }

    public override void OnRemoveComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetPlayerInfo, new object[] { });
    }

    public override void OnRemoveStart()
    {

    }
}
