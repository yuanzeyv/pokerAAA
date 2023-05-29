using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class MyUnionPanel : BasePlane {

    private Button backBtn;
	private Transform myClubContent;
	private GameObject tip;
    private GameObject addPanel;
    
    private GameObject createSv;
    private GameObject joinSv;
    private Transform createContent;
    private Transform joinContent;

    void Awake() {
		backBtn= transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        myClubContent = transform.Find("Scroll View/Viewport/Content");
		tip = transform.Find("tip").gameObject;
        addPanel = transform.Find("PanelAdd").gameObject;

        createSv = transform.Find("info/createClubs").gameObject;
        joinSv = transform.Find("info/joinClubs").gameObject;
        createContent = transform.Find("info/createClubs/Viewport/Content");
        joinContent = transform.Find("info/joinClubs/Viewport/Content");

		backBtn.onClick.AddListener(()=> {
			SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        Button addBtn = transform.Find("ToggleGroup/ButtonAdd").GetComponent<Button>();
        addBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            addPanel.SetActive(true);
        });

        addPanel.GetComponent<Button>().onClick.AddListener(()=>{
            addPanel.SetActive(false);
        });

        Button createBtn = transform.Find("PanelAdd/ButtonCreate").GetComponent<Button>();
        createBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            addPanel.SetActive(false);
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionCreatePanel);
        });

        Button joinBtn = transform.Find("PanelAdd/ButtonJoin").GetComponent<Button>();
        joinBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            addPanel.SetActive(false);
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionJoinPanel);
        });

        HideSv();
        gameObject.SetActive(false);
    }

    public void Refresh(){
        NetMngr.GetSingleton().Send(InterfaceUnion.GetMyUnionList, new object[] { UnionManager.GetSingleton().clubId});
    }

    void HideSv(){
        GameTools.GetSingleton().HideSv(createContent);
        createSv.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 0);
        GameTools.GetSingleton().HideSv(joinContent);
        joinSv.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 0);
    }

	public void SetInfo(ArrayList info)
	{
        GameTools.GetSingleton().HideSv(createContent);
        createSv.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 0);
        GameTools.GetSingleton().HideSv(joinContent);
        joinSv.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 0);
        if(info.Count == 0){
            tip.SetActive(true);
            return;
        }
        tip.SetActive(false);
        ArrayList createInfo = new ArrayList();
        ArrayList joinInfo = new ArrayList();
        for(int i = 0; i < info.Count; i++){
            Hashtable ht = info[i] as Hashtable;
            if(ht["myLm"].ToString() == "1"){
                createInfo.Add(info[i]);        
            } else {
                joinInfo.Add(info[i]);
            }
        }
        
		for (int i = 0; i < createInfo.Count; i++)
		{
			Hashtable ht = createInfo[i] as Hashtable;
            GameObject cell = GameTools.GetSingleton().GetSvCell(createContent);
            Transform tr = cell.transform;
            string id = ht["id"].ToString();
            tr.Find("name").GetComponent<Text>().text = ht["lmName"].ToString();
            tr.Find("id").GetComponent<Text>().text = "id:" + id;
            cell.GetComponent<Button>().onClick.AddListener(()=>{
                UnionManager.GetSingleton().unionId = id;
                UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionInfoPanel);
            });
		}
        createSv.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 74 * createInfo.Count + 10 * createInfo.Count);

        for (int i = 0; i < joinInfo.Count; i++)
        {
            Hashtable ht = joinInfo[i] as Hashtable;
            GameObject cell = GameTools.GetSingleton().GetSvCell(joinContent);
            Transform tr = cell.transform;
            string id = ht["id"].ToString();
            tr.Find("name").GetComponent<Text>().text = ht["lmName"].ToString();
            tr.Find("id").GetComponent<Text>().text = "id:" + id;
            cell.GetComponent<Button>().onClick.AddListener(()=>{
                UnionManager.GetSingleton().unionId = id;
                UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionInfoPanel);
            });   
        }
        joinSv.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 74 * joinInfo.Count + 10 * joinInfo.Count);
	}


    void Start()
    {
	}

    void Update()
    {

    }

    public override void OnAddComplete()
    {
	   NetMngr.GetSingleton().Send(InterfaceUnion.GetMyUnionList, new object[] { UnionManager.GetSingleton().clubId});	
    }

    public override void OnAddStart()
    {
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {
        HideSv();
    }


}