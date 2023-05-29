using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopUnionCoinPanel : BasePlane {

    private Button backBtn;
    private Text residue;
    private Text zhuanshi;
    private Text[] value1=new Text[7];
    private Text[] miaoshu = new Text[7];
    private Text[] value2=new Text[7];
    private Button[] value3 = new Button[7];
    // private string[] id=new string[7];

    ArrayList list = new ArrayList();

    private Text name;
    private RawImage head;

    private int[] prices = new int[7]{
        6, 12, 30, 68, 188, 388, 688,
    };

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
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        for (int i = 0; i < value1.Length; i++)
        {
            value1[i] = transform.Find(i+1 + "/Text").GetComponent<Text>();
        }
        for (int i = 0; i < miaoshu.Length; i++)
        {
            miaoshu[i] = transform.Find(i + 1 + "/miaoshu").GetComponent<Text>();
            miaoshu[i].text = "";
        }

        for (int i = 0; i < value2.Length; i++)
        {
            value2[i] = transform.Find(i+1 + "/Button/Text").GetComponent<Text>();
        }
        for (int i = 0; i < value3.Length; i++)
        {
            value3[i] = transform.Find(i + 1 + "/Button").GetComponent<Button>();
        }
        value3[0].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[0] }); }); 
        });
        value3[1].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[1] }); }); 
        });
        value3[2].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[2] }); }); 
        });
        value3[3].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[3] }); }); 
        });
        value3[4].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[4] }); }); 
        });
        value3[5].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[5] }); }); 
        });
        value3[6].onClick.AddListener(() => { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            PopupCommon.GetSingleton().ShowView("是否购买联盟币", null, true, delegate { NetMngr.GetSingleton().Send(InterfaceUnion.BuyUnionCoin, new object[] { StaticData.unionId, prices[6] }); }); 
        });

        residue.text = StaticData.unionCoin + ""; // GameTools.GetSingleton().GetUnionCoin(StaticData.unionId) + "";
        zhuanshi.text = StaticData.diamond.ToString();

        name.text = StaticData.username.ToString();

        GameTools.GetSingleton().GetTextureNet(StaticData.url, (Texture sprite)=>{
            head.texture = sprite;
        });

        gameObject.SetActive(false);
    }

    public void SetRate(Hashtable data)
    {
        float rate = float.Parse(data["lmCoin"].ToString());
        for (int i = 0; i < prices.Length; i++)
        {
            value1[i].text = (int)(prices[i] * rate) + "联盟币";
            value2[i].text = prices[i].ToString();
        }
        
    }
    public void Refresh()
    {
        int coin = StaticData.unionCoin; // GameTools.GetSingleton().GetUnionCoin(StaticData.unionId);
        residue.text = coin + "";
        zhuanshi.text = StaticData.diamond.ToString();
    
    }

    void Start ()
    {
    
    }
    
    void Update ()
    {
    
    }

    void OnEnable() {
        // NetMngr.GetSingleton().Send(InterfaceMain.GetShopInfo, new object[] { "1" });
    }

    public override void OnAddComplete()
    {
        Refresh();
    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceUnion.GetCreateUnionDiamond, new object[] {} );
    }

    public override void OnRemoveComplete()
    {
        // NetMngr.GetSingleton().Send(InterfaceMain.GetPlayerInfo, new object[] { });
    }

    public override void OnRemoveStart()
    {

    }
}
