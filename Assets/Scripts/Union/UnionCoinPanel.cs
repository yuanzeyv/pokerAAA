using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnionCoinPanel : BasePlane
{

    private Text coinText;

    void Awake()
    {
        Button backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        coinText = transform.Find("info/coin").GetComponent<Text>();
        
        Button consumeBtn = transform.Find("info/consumeBtn").GetComponent<Button>();
        consumeBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().shopUnionCoinPanel);    
        });

        Button sendBtn = transform.Find("info/sendBtn").GetComponent<Button>();
        sendBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().sendUnionCoinPanel);    
        });

        Button recycleBtn = transform.Find("info/recycleBtn").GetComponent<Button>();
        recycleBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().recycleUnionCoinPanel);    
        });
        
        gameObject.SetActive(false);
    }


    void Start()
    {
        
    }

    public void Refresh(){
        int coin = StaticData.unionCoin; // GameTools.GetSingleton().GetUnionCoin(UnionManager.GetSingleton().unionId);
        coinText.text = coin + "";
    }

    public override void OnAddComplete()
    {
        Refresh();

        // GameObject consume = transform.Find("info/consumeBtn").gameObject;
        // consume.SetActive(ClubManager.GetSingleton().clubInfoTopPanel.isMine == 1);
    }

    public override void OnAddStart()
    {
    }

    public override void OnRemoveComplete()
    {
    }

    public override void OnRemoveStart()
    {
    }


}
