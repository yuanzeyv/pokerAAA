using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DragonBones;
using DG.Tweening;

public class PlayerInfoPopup : BasePopup {
    
    public int i;
    private RawImage icon;
    private Button back;
    private Button playerBtn;
    private Text playerName;
    private Text playerState;
    private Text uid; 
    private UnityEngine.Transform parent;
    private Text shoushu;
    private Text ruchilv;
    private Text ruchishenglv;
    private Text jijin;
    private Text fanqianjiazhu;
    private Text againAdd;
    private Text persisentAdd;
    private Text tanpai;
    private Button look;

    //add by yang yang 2021年3月18日 19:15:40
    public UnityDragonBonesData dragonBoneData;
    private Button btAniDaoDanBoom;
    private Button btAniDaoShui;
    private Button btAniDianZan;
    private Button btAniFanQie;
    private Button btAniHongChun;
    private Button btAniJiDan;
    private Button btAniPaiZhao;
    private Button btAniPiJiu;
    private Button btAniShaYu;
    private Button btAniZhaDan;
    private Button btAniZhuoJi;
    //end
    private bool noSit=false;
    private bool noEnter=false;
    private string id;
    private string strChairId;
    private Hashtable data;

    private Vector3 m_vFromPos;
    private Vector3 m_vToPos;

   
    private void Awake()
    {
        Init();
        icon = transform.Find("PlayerHead/head").GetComponent<RawImage>();
        back = transform.Find("CloseBtn").GetComponent<Button>();
        playerBtn = transform.Find("playerManageBtn").GetComponent<Button>();
        playerName = transform.Find("name").GetComponent<Text>();
        playerState = transform.Find("State").GetComponent<Text>();
        uid = transform.Find("uid").GetComponent<Text>(); 
        parent = transform.Find("Scroll View/Viewport/Content");
        shoushu = parent.Find("Item1/shoushu/Text").GetComponent<Text>();
        ruchilv = parent.Find("Item2/ruchilv/Text").GetComponent<Text>();
        ruchishenglv = parent.Find("Item3/ruchishenglv/Text").GetComponent<Text>();
        jijin = parent.Find("Item4/jijinshuliang/Text").GetComponent<Text>();
        fanqianjiazhu = parent.Find("Item5/fanqianjiazhu/Text").GetComponent<Text>();
        againAdd = parent.Find("Item6/zaicijiazhu/Text").GetComponent<Text>();
        persisentAdd = parent.Find("Item7/chixuxiazhu/Text").GetComponent<Text>();
        tanpai = parent.Find("Item8/tanpailv/Text").GetComponent<Text>();
        look = transform.Find("isLook").GetComponent<Button>();

        //add by yang yang 2021年3月17日 18:30:52
        btAniDaoDanBoom = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageDaoDan/daodanboom").GetComponent<Button>();
        btAniDaoShui = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageDaoShui/daoshui").GetComponent<Button>();
        btAniDianZan = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageDianZan/dianzan").GetComponent<Button>();
        btAniFanQie = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageFanQie/fanqie").GetComponent<Button>();
        btAniHongChun = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageHongChun/hongchun").GetComponent<Button>();
        btAniJiDan = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageJiDan/jidan").GetComponent<Button>();
        btAniPaiZhao = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImagePaiZhao/paizhao").GetComponent<Button>();
        btAniPiJiu = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImagePiJiu/pijiu").GetComponent<Button>();
        btAniShaYu = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageShaYu/shayu").GetComponent<Button>();
        btAniZhaDan = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageZhaDan/zhadan").GetComponent<Button>();
        btAniZhuoJi = transform.Find("AnimojiPanel/ScrollView/Viewport/Content/ImageZhuoJI/zhuoji").GetComponent<Button>();

        //end

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
        });
        playerBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().playerManagePopup.ShowView(noSit,noEnter,id);
        });
        look.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.peekCards, new object[] {int.Parse(id) });
            HideView();
        });
        //add by yang yang 2021年3月18日 19:58:17
        btAniDaoDanBoom.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(1);
        
        });
        btAniDaoShui.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(2);
        
        });
        btAniDianZan.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(3);
       
        });
        btAniFanQie.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(4);
         
        });
        btAniHongChun.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(5);
        });
        btAniJiDan.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(6);
        });
        btAniPaiZhao.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(7);
        });
        btAniPiJiu.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(8);
        });
        btAniShaYu.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(9);
        });
        btAniZhaDan.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(10);
        });
        btAniZhuoJi.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            sendAnimoji(11);
          //  GameUIManager.GetSingleton().setPosition(m_vFromPos, m_vToPos, GameUIManager.strZhuoJi);
        });
    }
    public void sendAnimoji(int nAnimojiID)
    {
        if (id == StaticData.ID || StaticData.isGuanzhan)
        {
            PopupCommon.GetSingleton().ShowView("发送表情失败!");
        }
        else
            NetMngr.GetSingleton().Send(InterfaceGame.SendExperssion, new object[] { strChairId, nAnimojiID });
        //   gameObject.transform.localPosition = new Vector3(-5000, -5000, 0);
        HideView();
        //gameObject.SetActive(false);
    }
    public void setPosition(Vector3 fromPos, Vector3 toPos)
    {
        m_vFromPos = fromPos;
        m_vToPos = toPos;
       
        
    }

    private void Start()
    {

    }
    private void Update()
    {

    }
    public void SetData(Hashtable data)
    {
        this.data = data;
        GameTools.GetSingleton().GetTextureNet(data["headUrl"].ToString(),SetIcon);
        playerName.text = data["nickname"].ToString();
        playerState.text = data["State"].ToString() == "1" ? "游戏中" : "旁观中";
        uid.text = "id:" + data["id"].ToString();
        shoushu.text = data["shou"].ToString();
        ruchilv.text = float.Parse(data["inPoolRate"].ToString())*100+"%";
        ruchishenglv.text = float.Parse(data["inPoolWinRate"].ToString())*100+"%";
        jijin.text = data["jijin"].ToString();
        fanqianjiazhu.text = float.Parse(data["fanqian"].ToString())*100+"%";
        againAdd.text = float.Parse(data["again"].ToString())*100 + "%";
        persisentAdd.text = float.Parse(data["continue"].ToString())*100 + "%";
        tanpai.text = float.Parse(data["tanpai"].ToString())*100 + "%";
        id = data["id"].ToString();
        string temp = "";
        if (data.Contains("canShowManageBtn"))
        {
            temp = data["canShowManageBtn"].ToString();
        }
        look.gameObject.SetActive(int.Parse(data["isLook"].ToString()) == 1 ? true : false);
      
        if (id==StaticData.ID || temp=="0")
        {
            playerBtn.gameObject.SetActive(false);
            look.gameObject.SetActive(false);
        }
        else
        {
            look.gameObject.SetActive(true);
            playerBtn.gameObject.SetActive(true);
        }
        noSit = int.Parse(data["forbidSit"].ToString()) == 1 ? true : false;
        noEnter= int.Parse(data["forbidEntry"].ToString()) == 1 ? true : false;
        strChairId = data["chairId"].ToString();

        transform.Find("Residue/value1").GetComponent<Text>().text = data["coin"].ToString();
        transform.Find("Residue/value2").GetComponent<Text>().text = data["diamond"].ToString();
        transform.Find("Residue/value3").GetComponent<Text>().text = data["lmCoin"].ToString();
    }

    public void setExpressionDiamond(Hashtable h){
        UnityEngine.Transform parent = transform.Find("AnimojiPanel/ScrollView/Viewport/Content");

        ArrayList list = h["expression"] as ArrayList;
        for(int i = 0; i < list.Count; i++){
            Hashtable hh = list[i] as Hashtable;
            string id = hh["id"].ToString();
            string amount = hh["icon_spend"].ToString();
            parent.GetChild(i).Find("num").GetComponent<Text>().text = amount;
        }
    }

    /*
    private void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }*/
    private void SetIcon(Texture sprite)
    {
        icon.texture = sprite;
    }
    public void ShowView(string id)
    {
        NetMngr.GetSingleton().Send(InterfaceGame.getRoomUserDetails, new object[] { int.Parse(id)});
     //   gameObject.transform.DOScale(1.0f, 0);

        base.ShowView();
    }
    public void HideView()
    {
        GameTools.GetSingleton().stopGameToolsAllCoroutines();
        base.HideView();
    }

}
