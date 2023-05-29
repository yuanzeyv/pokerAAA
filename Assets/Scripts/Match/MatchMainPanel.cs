using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MatchMainPanel : BasePlane
{
	Button backBtn;
	Button matchingBtn;
    Toggle[] toggles;

    GameObject statusPanel;
    GameObject playerPanel;
    GameObject rewardPanel;
    GameObject deskPanel;
    GameObject blindPanel;

	int curIndex = -1;

    void Awake()
    {
        backBtn = transform.Find("TopPanel/Back").GetComponent<Button>();
        backBtn.onClick.AddListener(() => {
        	SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        matchingBtn = transform.Find("BottomPanel/MatchingBtn").GetComponent<Button>();
        matchingBtn.onClick.AddListener(() => {
        	SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
        	// 
        });

        toggles = transform.Find("TopPanel/ToggleGroup").GetComponentsInChildren<Toggle>();
        for(int i=0; i < toggles.Length; i++){
        	int idx = i;
        	toggles[i].onValueChanged.AddListener((isOn) => {
        		if(isOn){
        			Select(idx);
        		}
        	});
        }

        statusPanel = transform.Find("MiddlePanel/StatusPanel").gameObject;
        playerPanel = transform.Find("MiddlePanel/PlayerPanel").gameObject;
        rewardPanel = transform.Find("MiddlePanel/RewardPanel").gameObject;
        deskPanel = transform.Find("MiddlePanel/DeskPanel").gameObject;
        blindPanel = transform.Find("MiddlePanel/BlindPanel").gameObject;

    }

    void Start()
    {

    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {
    	Select(0);
    }

    public override void OnAddStart()
    {
    //    NetMngr.GetSingleton().Send(InterfaceClub.GetCoinContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    //    NetMngr.GetSingleton().Send(InterfaceClub.GetDiamondContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
        // NetMngr.GetSingleton().Send(InterfaceClub.GetManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 1});
        // NetMngr.GetSingleton().Send(InterfaceClub.GetManualContent, new object[] { ClubManager.GetSingleton().clubPanel.clubId, 2});
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }

    void Select(int index){
    	if(curIndex == index) return;
    	Debug.Log("Select " + index);
    	curIndex = index;
    	for(int i=0; i < toggles.Length; i++){
    		toggles[i].GetComponent<Toggle>().isOn = (index == i);
    		toggles[i].GetComponent<Toggle>().interactable = (index != i);
    	}
    	statusPanel.SetActive(index == 0);
    	playerPanel.SetActive(index == 1);
    	rewardPanel.SetActive(index == 2);
    	deskPanel.SetActive(index == 3);
    	blindPanel.SetActive(index == 4);
    	
    	// todo 先请求数据 再刷新UI
    	switch(index){
    		case 0:
    			break;

    		case 1:
    			RefreshPlayerPanel();
    			break;
    	}
    }

    void RefreshPlayerPanel()
    {
    	Transform parent = playerPanel.transform.Find("Scroll View/Viewport/Content");
    	HideSv(parent);

    	for(int i=0; i < 10; i++){
    		GameObject cell = GetSvCell(parent);
    		cell.SetActive(true);
    	}
    }

    GameObject GetSvCell(Transform parent)
    {
    	for(int i=0; i < parent.childCount; i++){
    		if(!parent.GetChild(i).gameObject.activeSelf){
    			return parent.GetChild(i).gameObject;
    		}
    	}

    	GameObject cell = Instantiate(parent.GetChild(0).gameObject);
    	cell.transform.SetParent(parent);
    	cell.transform.localScale = Vector3.one;
        cell.transform.localPosition = Vector3.zero;
    	return cell;
    }

    void HideSv(Transform parent)
    {
    	for(int i=0; i < parent.childCount; i++){
    		parent.GetChild(i).gameObject.SetActive(false);
    	}
    }
    

}