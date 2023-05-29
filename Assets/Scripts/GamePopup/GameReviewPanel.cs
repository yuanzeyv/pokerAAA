using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameReviewPanel : BasePlane
{
    public GameObject[] commonCards = new GameObject[5];
   
    public Text baoxian;

    public Transform playerContent;

    public Button cancelBtn;

    public Button backBtn;
    public Button tousuBtn; 

    public Toggle allTog;
    public Toggle myTog;

//    public Slider s;//牌局进度条

    public Button firstPageBtn;
    public Button prevPageBtn;
    public Button nextPageBtn;
    public Button lastPageBtn;

    public Text page;

    public int curPage=1;
    public int maxPage;

    public int isMine;

    void Awake()
    {

        backBtn = transform.Find("BackBtn").GetComponent<Button>();
        cancelBtn= transform.Find("cancelBtn").GetComponent<Button>();
        tousuBtn= transform.Find("TousuBtn").GetComponent<Button>();

        for (int i = 0; i < 5; i++)
        {
            int index = i;
            commonCards[i] = transform.Find("commonCardItem").GetChild(index+1).gameObject;

        }

        playerContent = transform.Find("Scroll View/Viewport/Content");
      
        baoxian = transform.Find("baoxian/Text").GetComponent<Text>();

        allTog = transform.Find("AllToggle").GetComponent<Toggle>();
        myTog = transform.Find("MyToggle").GetComponent<Toggle>();

//        s= transform.Find("Slider").GetComponent<Slider>();

        firstPageBtn = transform.Find("firstPage").GetComponent<Button>();
        prevPageBtn = transform.Find("prevPage").GetComponent<Button>();
        nextPageBtn = transform.Find("nextPage").GetComponent<Button>();
        lastPageBtn = transform.Find("lastPage").GetComponent<Button>();

        page = transform.Find("page").GetComponent<Text>();



        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            // ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            gameObject.SetActive(false);
            TouchMove.Instance().isRun = true;
        });

        cancelBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            // ClubManager.GetSingleton().planeManager.RemoveTopPlane();
            gameObject.SetActive(false);
            TouchMove.Instance().isRun = true;
        });

        tousuBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if(curPage > 0){
                GameUIManager.GetSingleton().tousuPanel.ShowView(curPage, isMine);
            } else {
                Tip.Instance.showMsg("暂无数据");
            }
        });

        firstPageBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            curPage = 1;
//            s.value = 1;
            NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { 1,isMine });

        });
        prevPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (curPage > 1)
            {
                curPage--;
//                s.value = curPage;
                NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { curPage ,isMine});
            }
        });
        nextPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (curPage < maxPage)
            {
                curPage++;
//                s.value = curPage;
                NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { curPage ,isMine});
            }
        });
        lastPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            curPage = maxPage;
//            s.value = maxPage;
            NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { maxPage,isMine });
        });


        allTog.onValueChanged.AddListener((bool b)=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { curPage, 0 });
        });

        myTog.onValueChanged.AddListener((bool b) => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { curPage, 1});
        });

        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        page.text = curPage + "/" + maxPage;
        if (allTog.isOn)
        {
            isMine = 0;
        }
        else {
            isMine = 1;
        }
    }


    public override void OnAddComplete()
    {
    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { 1,isMine});
    }

    public override void OnRemoveComplete()
    {
        //去除下载头像 协成
        GameTools.GetSingleton().stopGameToolsAllCoroutines();
    }

    public override void OnRemoveStart()
    {

    }

    public void ClearItem()
    {
        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);
        for (int i = 0; i < 5; i++)
        {
            commonCards[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("pai/card_back_2");
            commonCards[i].SetActive(true);
        }
    }
    public void getRoundReviewMaxPage(Hashtable data)
    {
        //transform.Find("baoxian").gameObject.SetActive(GameUIManager.GetSingleton().isAdmin);
        string strTemp = data["maxPage"].ToString();
        this.gameObject.SetActive(true);
        curPage = int.Parse(strTemp);
        NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { int.Parse(strTemp), GameUIManager.GetSingleton().gameReviewPanel.isMine });
    }
    public void RoundReviewCallBack(Hashtable data) {

        for (int i = 0; i < 5; i++)
        {
            commonCards[i].SetActive(false);
        }
        if (data["success"].ToString() == "0") {
            return;
        }
        maxPage = int.Parse(data["maxPage"].ToString());
        if (maxPage == 0) {
            maxPage = 1;
        }
 //       
//        s.maxValue = maxPage;
        baoxian.text = data["totalBaoXian"].ToString();
        if ( int.Parse(baoxian.text)>0)
        {
            this.baoxian.color = Color.red;
        }
        else 
        {
            this.baoxian.color = Color.green;
        }
        string[] cardobjs = data["commonCard"].ToString().Split('|');



        for (int i = 0; i < cardobjs.Length; i++) {

            if (cardobjs[i] == "99")
            {
                //commonCards[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("pai/card_back_2");
                //commonCards[i].SetActive(true);
                commonCards[i].SetActive(false);
            }
            else {
                commonCards[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + cardobjs[i]);
                commonCards[i].SetActive(true);
            }
        }

        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);
    
        ArrayList List = data["playerList"] as ArrayList;
        
        if (List == null)
        {
            return;
        }
        for (int i = 0; i < List.Count; i++)
        {
            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("GamePopup/GamePopupItem/gameReviewItem")) as GameObject;
            var listv = obj.GetComponent<GameReviewItem>(); 
            listv.transform.parent = playerContent;         
            listv.transform.localScale = new Vector2(1, 1);
            //赋值
            listv.SetInfo(ht,data["commonCard"].ToString());
        }

    }

    public void EndDrag() {
        Debug.Log("拖拽结束");
//        curPage = (int)s.value;
        NetMngr.GetSingleton().Send(InterfaceGame.roundReview, new object[] { curPage,isMine});
    }

    public void DuringDrag() {
        //Debug.Log("拖拽中");
//        curPage=(int)s.value;
    }

    public void BeginDrag()
    {
        Debug.Log("拖拽开始");
//        curPage = (int)s.value;
    }
}
