using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InsuranceRulePopup : BasePopup
{
    
    public Button closeBtn;

    public Transform oddContent;

    public Transform[] peilvItems=new Transform[20];

    public Toggle tipToggle;

    void Awake()
    {
        Init();

       
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        oddContent = transform.Find("rule/ClubContent/Scroll View/Viewport/Content/odds/oddsContent");

        tipToggle = transform.Find("tip").GetComponent<Toggle>();

        tipToggle.onValueChanged.AddListener((bool b) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (b)
            {
                NetMngr.GetSingleton().Send(InterfaceGame.getInsurancePeiLv, new object[] { GameUIManager.GetSingleton().gameType });
            }
        });

        for (int i = 0; i < 20; i++)
        {
            int index = i;
            peilvItems[index] = oddContent.GetChild(index+1);
          
        }
        closeBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
        });
        gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetInsurancePeiLvCallBack(Hashtable data) {
        ArrayList List = data["oddsList"] as ArrayList;
       
        if (List == null)
        {
         
            return;
        }

        for (int i = 0; i < List.Count; i++)
        {
            Hashtable ht = List[i] as Hashtable;
            peilvItems[i].GetChild(0).GetComponent<Text>().text = ht["outs"].ToString();

            peilvItems[i].GetChild(1).GetComponent<Text>().text = ht["odds"].ToString();





        }

    }
}
