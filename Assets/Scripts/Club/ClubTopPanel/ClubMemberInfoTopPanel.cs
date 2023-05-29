using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClubMemberInfoTopPanel : BasePlane
{
    public Button backBtn;

    public Transform infoContent;

	public RawImage memberHaed;
    public Text memberName;
    public Text Id;
    public Text gameCount;
    public Text zongshoushu;
    public Text myTip;
    public Text limit;

	public Dictionary<int, DataIcon> dataicons = new Dictionary<int, DataIcon>();

	public DataIcon ruChi;
	public DataIcon victory;
	public DataIcon tanPai;

//    public Text ruchishenglvRate;
//    public Text ruchilvRate;
//    public Text tanpailvRate;

    public Button quitBtn;
    public Button limitModifyBtn;

    public bool isMine;
    public string id;
    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();

        infoContent = transform.Find("Info");

		memberHaed = infoContent.Find("Member/MemberHead/head").GetComponent<RawImage>();
		memberName = infoContent.Find("Member/MemberName").GetComponent<Text>();
		Id = infoContent.Find("Member/Id").GetComponent<Text>();

		gameCount = infoContent.Find("Member/zong/GameCount").GetComponent<Text>();
		zongshoushu = infoContent.Find("Member/zong/zongshoushu").GetComponent<Text>();
		myTip = infoContent.Find("Member/myTip").GetComponent<Text>();
        limit = infoContent.Find("limit/limit").GetComponent<Text>();
        limitModifyBtn = infoContent.Find("limit/modifyBtn").GetComponent<Button>();

		victory = infoContent.Find("ruchishenglv").GetComponent<DataIcon>();
		ruChi = infoContent.Find("ruchilv").GetComponent<DataIcon>();
		tanPai = infoContent.Find("tanpailv").GetComponent<DataIcon>();


		dataicons.Add (0, victory);
		dataicons.Add (1, ruChi);
		dataicons.Add (2, tanPai);

        quitBtn = infoContent.Find("Quit/quitBtn").GetComponent<Button>();

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        quitBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().commonPopup.ShowView("确定踢出俱乐部？", null, true, () => {

                NetMngr.GetSingleton().Send(InterfaceClub.DelMember, new object[] { id, ClubManager.GetSingleton().clubPanel.clubId });

            });
        });

        limitModifyBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().clubModifyLimitPanel.userId = id;
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubModifyLimitPanel);
        });

        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isMine)
        {
            quitBtn.gameObject.SetActive(true);
        }
        else
        {
            quitBtn.gameObject.SetActive(false);
        }
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.MemberInfo,new object[] { id,ClubManager.GetSingleton().clubPanel.clubId });
      

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.ManagerFindAll, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    public override void OnRemoveStart()
    {

    }

    public void GetSprtie(Texture s)
    {
        memberHaed.texture = s;
    }

    //获取成员信息回调
    public void MemberInfoCallBack(Hashtable data) {
        GameTools.GetSingleton().GetTextureNet(data["headUrl"].ToString(), GetSprtie);
        Id.text = "ID:"+data["id"].ToString();
        id = data["id"].ToString();
        if (data.ContainsKey("sex")) {
            if (data["sex"].ToString() == "1")
            {
                Id.transform.GetChild(0).gameObject.SetActive(true);
                Id.transform.GetChild(1).gameObject.SetActive(false);
            }
            else {
                Id.transform.GetChild(1).gameObject.SetActive(true);
                Id.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        memberName.text = "昵称:"+data["memberName"].ToString();
        myTip.text = data["tip"].ToString();
        gameCount.text = "总局数:"+data["totalCount"].ToString();
        zongshoushu.text = "总手数:"+data["zongshoushu"].ToString();
        limit.text = data["limit"].ToString();
        limitModifyBtn.gameObject.SetActive(ClubManager.GetSingleton().clubInfoTopPanel.isMine == 1);

		ruChi.Reset();
		victory.Reset();
		tanPai.Reset();

		ruChi.SetData(dataicons, float.Parse(data["ruchilv"].ToString()),"入池率");
		victory.SetData(dataicons, float.Parse(data["ruchishenglv"].ToString()), "入池胜率");
		tanPai.SetData(dataicons, float.Parse(data["tanpailv"].ToString()), "摊牌率");

//        ruchishenglvRate.text = float.Parse( data["ruchishenglv"].ToString())*100+"%";
//        ruchilvRate.text = float.Parse(data["ruchilv"].ToString()) * 100 + "%";
//        tanpailvRate.text = float.Parse(data["tanpailv"].ToString()) * 100 + "%";

    }

    public void RefreshLimit(int value){
        limit.text = value.ToString();
    }

}
