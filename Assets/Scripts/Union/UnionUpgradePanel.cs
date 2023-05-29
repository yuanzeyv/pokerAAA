using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnionUpgradePanel : BasePlane
{

    public Transform configContent;
    private Button addBtn;
    private Button subBtn;
    private Text upgradeLevelText;
    private Text costDiamondText;
    
    private int unionLevel = 1;
    private int minLevel = 1;
    private int maxLevel = 1;
    private int curLevel = 1;

    private int costDiamond = 0;
    private Dictionary<int, int> levelCost = new Dictionary<int, int>();

    void Awake()
    {
        Button backBtn = transform.Find("Top/Back/Share").GetComponent<Button>();
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        addBtn = transform.Find("Config/diamond/addBtn").GetComponent<Button>();
        addBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            curLevel += 1;
            upgradeLevelText.text = "LV." + curLevel;
            costDiamond += levelCost[curLevel];
            costDiamondText.text = costDiamond + "";
            
            addBtn.interactable = curLevel < maxLevel;
            subBtn.interactable = curLevel > minLevel;
        });

        subBtn = transform.Find("Config/diamond/subBtn").GetComponent<Button>();
        subBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            costDiamond -= levelCost[curLevel];
            curLevel -= 1;
            upgradeLevelText.text = "LV." + curLevel;
            costDiamondText.text = costDiamond + "";
            
            addBtn.interactable = curLevel < maxLevel;
            subBtn.interactable = curLevel > minLevel;
        });

        Button upgradeBtn = transform.Find("upgradeBtn").GetComponent<Button>();
        upgradeBtn.onClick.AddListener(() => {
            if(curLevel == unionLevel){
                return;
            }
            if(costDiamond > StaticData.diamond){
                UnionManager.GetSingleton().commonPopup.ShowView("钻石不足！");
                return;
            }
            NetMngr.GetSingleton().Send(InterfaceUnion.UpgradeUnion, 
                new object[] { UnionManager.GetSingleton().unionId, curLevel.ToString() });
        });

        upgradeLevelText = transform.Find("Config/diamond/level").GetComponent<Text>();
        costDiamondText = transform.Find("Config/diamond/diamond1").GetComponent<Text>();

        configContent = transform.Find("Config/Scroll View/Viewport/Content");

        gameObject.SetActive(false);
    }

    public void SetInfo(Hashtable data){
        transform.Find("Info/unionName").GetComponent<Text>().text = data["name"].ToString();
        string currentClub = data["currentClub"].ToString();
        string currentMaxClub = data["currentMaxClub"].ToString();
        string desc = string.Format("{0}/{1}", currentClub, currentMaxClub);
        transform.Find("Info/memberCount").GetComponent<Text>().text = desc;

        unionLevel = int.Parse(data["level"].ToString());
        minLevel = unionLevel;
        curLevel = unionLevel;

        ArrayList config = data["config"] as ArrayList;
        for(int i = 0; i < config.Count; i++){
            Hashtable h = config[i] as Hashtable;
            GameObject obj = GameTools.GetSingleton().GetSvCell(configContent);
            Transform tr = obj.transform;
            tr.Find("bg").gameObject.SetActive(i % 2 == 0);

            int lv = int.Parse(h["level"].ToString());
            maxLevel = Mathf.Max(maxLevel, lv);
            levelCost.Add(lv, int.Parse(h["diamond"].ToString()));
            tr.Find("level").GetComponent<Text>().text = lv + "";
            tr.Find("count").GetComponent<Text>().text = h["maxClub"].ToString();
        }
        Refresh();

    }

    public void Upgrade(int level){
        unionLevel = level;
        minLevel = unionLevel;
        curLevel = unionLevel;
        Refresh();
        transform.Find("Config/diamond/diamond2").GetComponent<Text>().text = StaticData.diamond + "";
    }

    void Refresh(){
        transform.Find("Info/level").GetComponent<Text>().text = "等级:" + unionLevel;
        upgradeLevelText.text = "LV." + unionLevel;
        addBtn.interactable = curLevel < maxLevel;
        subBtn.interactable = curLevel > minLevel;
        costDiamond = 0;
        costDiamondText.text = costDiamond + "";
    }

    void Start()
    {
        
    }

    public override void OnAddComplete()
    {
        transform.Find("Config/diamond/diamond2").GetComponent<Text>().text = StaticData.diamond + "";
        NetMngr.GetSingleton().Send(InterfaceUnion.UnionUpdateConfig, new object[] { 
            UnionManager.GetSingleton().unionId});
    }

    public override void OnAddStart()
    {
    }

    public override void OnRemoveComplete()
    {
        curLevel = minLevel;
        upgradeLevelText.text = "LV." + curLevel;
        costDiamond = 0;
        levelCost.Clear();
        GameTools.GetSingleton().HideSv(configContent);    
    }

    public override void OnRemoveStart()
    {
    }


}
