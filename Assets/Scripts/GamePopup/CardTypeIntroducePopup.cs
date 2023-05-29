using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CardTypeIntroducePopup : BasePopup
{
    public Button backBtn;
    public Button closeBtn;

    void Awake() {
        Init();

        backBtn = transform.Find("BackBtn").GetComponent<Button>();
//        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();

        backBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HideView();
        });

//        closeBtn.onClick.AddListener(() => {
//            HideView();
//        });
//
        gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
