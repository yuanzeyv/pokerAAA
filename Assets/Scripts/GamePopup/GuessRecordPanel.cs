using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GuessRecordPanel : BasePlane
{

    public Transform playerContent;

    public Button backBtn;
    public Button cancelBtn;


    public Slider s;//牌局进度条

    public Button firstPageBtn;
    public Button prevPageBtn;
    public Button nextPageBtn;
    public Button lastPageBtn;

    public Text page;

    public int curPage = 1;
    public int maxPage;


    void Awake()
    {

        backBtn = transform.Find("BackBtn").GetComponent<Button>();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button>();



        playerContent = transform.Find("Scroll View/Viewport/Content");


        s = transform.Find("Slider").GetComponent<Slider>();

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

        firstPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            curPage = 1;
            s.value = 1;
            NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { 1 ,10});

        });
        prevPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (curPage > 1)
            {
                curPage--;
                s.value = curPage;
                NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { curPage,10});
            }
        });
        nextPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (curPage < maxPage)
            {
                curPage++;
                s.value = curPage;
                NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { curPage,10});
            }
        });
        lastPageBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            curPage = maxPage;
            s.value = maxPage;
            NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { maxPage,10});
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
        
    }


    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { 1 ,10});
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }

    public void GuessRecordCallBack(Hashtable data)
    {

      
        if (data["success"].ToString() == "0")
        {
            return;

        }
        maxPage = int.Parse(data["allPage"].ToString());
        if (maxPage == 0) {
            maxPage = 1;
        }

        s.maxValue = maxPage;
    



    

        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);

        ArrayList List = data["recordList"] as ArrayList;

        if (List == null)
        {
            return;
        }
        for (int i = 0; i < List.Count; i++)
        {
            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("GamePopup/GamePopupItem/guessItem")) as GameObject;
            var listv = obj.GetComponent<GuessRecordItem>();
            listv.transform.parent = playerContent;
            listv.transform.localScale = new Vector2(1, 1);
            //赋值
            listv.SetInfo(ht["title"].ToString(), ht["betCoin"].ToString(), ht["winCoin"].ToString());
        }

    }


    


    public void EndDrag()
    {
        Debug.Log("拖拽结束");
        curPage = (int)s.value;
        NetMngr.GetSingleton().Send(InterfaceGame.getGuessRecord, new object[] { curPage ,10});
    }

    public void DuringDrag()
    {
        //Debug.Log("拖拽中");
        curPage = (int)s.value;
    }

    public void BeginDrag()
    {
        Debug.Log("拖拽开始");
        curPage = (int)s.value;
    }
}

