using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InsuranceDetailPanel : BasePlane
{
    public string id;
    private Transform infoContent;

    public Color grayColor = new Color(154/255f, 154/255f, 154/255f);
    public Color greenColor = new Color(32/255f, 197/255f, 48/255f);
    public Color redColor = new Color(255/255f, 99/255f, 99/255f);
    
    void Awake()
    {
        Button backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            HallManager.GetSingleton().planeManager.RemoveTopPlane();    
        });

        infoContent = transform.Find("Info/Viewport/Content");

        gameObject.SetActive(false);
    }


    void Start()
    {
        
    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {
    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetInsuranceDetail, new object[] { id });
    }

    public override void OnRemoveComplete()
    {
        GameTools.GetSingleton().HideSv(infoContent);
    }

    public override void OnRemoveStart()
    {
    }


    public void SetInfo(Hashtable data) {
        GameTools.GetSingleton().HideSv(infoContent);
        ArrayList info = data["map"] as ArrayList;
        if(info.Count == 0){
            return;
        }
        for(int i = 0; i < info.Count; i++){
            Hashtable h = info[i] as Hashtable;

            GameObject cell = GameTools.GetSingleton().GetSvCell(infoContent);
            cell.transform.Find("name").GetComponent<Text>().text = h["nickName"].ToString();
            int coin = int.Parse(h["coin"].ToString());
            Text coinText = cell.transform.Find("coin").GetComponent<Text>();
            coinText.text = coin + "";
            if(coin == 0){
                coinText.color = grayColor;
            } else if(coin < 0){
                coinText.color = greenColor;
            } else {
                coinText.color = redColor;
            }
            GameTools.GetSingleton().GetTextureNet(h["url"].ToString(), (Texture s)=>{
                cell.transform.Find("head/mask").GetComponent<RawImage>().texture = s;
            });
        }
    }

}
