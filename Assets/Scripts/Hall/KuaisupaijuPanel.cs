using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class KuaisupaijuPanel : BasePlane {

	private Text[] showTextGroup;
	private InputField inputField;
	private Button createPaiJu;
	private Button JoinPaiJu;

	private int curIndex = 0;//当前输入到第几个


    private void Awake()
    {
    	// GameTools.GetSingleton().AdaptSafearea(transform.Find("Top"));
		//showTextGroup = transform.Find("shuru").GetComponentsInChildren<Text>();
		inputField = transform.Find("InputField").GetComponent<InputField>();
		createPaiJu = transform.Find("Button2").GetComponent<Button>();
		JoinPaiJu = transform.Find("Button1").GetComponent<Button>();

		createPaiJu.onClick.AddListener(() =>
		{
			SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			HallManager.GetSingleton().createGamePopup.ShowView(1);
		});

		JoinPaiJu.onClick.AddListener(() =>
		{
			SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
				onJoinPaiJu();
				initText();

		});
    }

	void initText()
	{
		curIndex = 0;
//		for (int i = 0; i < 6; i++) {
//			showTextGroup[i].text = "";
//		}
	}

    void Start ()
    {
		initText ();
    }

    void Update ()
    {

//		foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
//		{
//			
//			if (Input.GetKeyUp(keyCode))
//			{
//				Debug.Log ("输入:" + keyCode.ToString());
//
//				if (keyCode == KeyCode.Backspace) {
//					if (curIndex > 0) {
//						showTextGroup [curIndex - 1].text = "";
//						curIndex = curIndex - 1;
//					}
//				} 
//				else if(keyCode >= KeyCode.Alpha0 & keyCode <= KeyCode.Alpha9){
//					if (curIndex < 6) {
//						showTextGroup [curIndex].text = (keyCode-KeyCode.Alpha0).ToString();
//						curIndex = curIndex + 1;
////						if (curIndex == 6) {
////							//进去游戏
////							onJoinPaiJu();
////						}
//					}
//					
//				}
//			}
//		}


//		if (Input.GetKeyUp(KeyCode.Backspace))
//		{
//			showTextGroup[curIndex-1].text = "";
//			curIndex = curIndex - 1;
//			if (curIndex < 0) {
//				curIndex = 0;
//			}
//		}
//		else if (Input.GetKeyUp(KeyCode.Keypad0))
//		{
//			curIndex = curIndex + 1;
//			showTextGroup[curIndex-1].text = "0";
//			if (curIndex > 5) {
//				//加入牌局
//				initText();
//			}
//		}
//	

	
	}

	void onJoinPaiJu()
	{
		String id = "";
//		for (int i = 0; i < 6; i++) {
//			id = id + showTextGroup[i].text;
//		}
		id = inputField.text;
		NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerEnterRequest, new object[] {id });
	}


    public override void OnAddComplete()
    {
		
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
