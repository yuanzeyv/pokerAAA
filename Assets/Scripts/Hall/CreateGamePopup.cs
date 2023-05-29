using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateGamePopup : BasePopup
{

    private InputField roomName;
    private Button back;
    private Text mang;
    private Text little;
    private Slider mangSlider;
    private Text currDiamond;
    private Text costDiamond;
	private Text[] currentTimes = new Text[7]; 
    private Slider timeSlider;
    private Toggle insurance;
    private Toggle authorize;
    private Toggle straddle;
    private Toggle abandon;
    private Toggle GPS;
    private Toggle IP;
    private Toggle DelayKanpai;
    private InputField minJinfenInput;
    private Text MinJifen;

//    private Button advance;
    private Button create;
//    private GameObject advanceMenu;
    private Slider noteBefore;
	private Text[] noteBeforeText = new Text[20]; 
    private Slider positionCount;
	private Text[] positionCountText = new Text[8]; 
    private Slider dairuMin;
    private Slider dairuMax;
	private Text[] dairuText= new Text[8];
    

    public string[] costDiamondStrs;

	public GameObject currDiamondGameObject;
	public GameObject costDiamondGameObject;

	public Transform noteBeforeTipFrom;
	public Transform renshuTipFrom;
	public Transform timeTipFrom;
	public Transform beishuTipFrom;

    public Transform mangForm;
    public Transform noteBeforeForm;
    public Transform mangduanForm;//短牌
   
    private Text mangduanForm_before;//前注
    private Text mangduanForm_dairu;//带入记分牌
    private Slider mangduanForm_slider;//前注
    private Text mangduanForm_currDiamond;//当前钻石
    private Text mangduanForm_costDiamond;//需要钻石

    //服务费  //add by yang yang 2021年3月22日 13:25:30
    public Transform serviceFeeTipFrom;
    private Text[] serviceFeeText = new Text[12];
    private Slider serviceFeeTipFrom_slider;

    public int selectGameType = 0;
    public Toggle[] GameToggles;

	public int createType;


	private Slider playerTimeSlider;
    private string[] playerTimeValues = {"10", "12", "15", "20"};
	private Text[] playerTimeText = new Text[4];
	public Transform playerTimeTipFrom;
	public string[] playeTimeTopTip = {"10s","12s", "15s", "20s"};

    public string[] duan_noteBeforeTip = { "1", "2", "5", "10", "20", "25", "50", "100", "200", "300", "500", "1000" };

    public string[] littlemangs = { "1", "2", "5", "10", "20", "25", "50", "100", "200", "300", "500", "1000"};
	public string[] positionCountTip = {"2", "3", "4", "5", "6", "7", "8", "9" };
	public string[] timeTopTip = { "0.5h", "1h", "2h", "3h", "4h", "5h", "6h"};
    public string[] timeValues = { "30", "60", "120", "180", "240", "300", "360"};
    
	public string[] dairuTip = {"1", "2", "3", "4", "5", "6", "7", "8" };
	public string[] noteBeforeTip0 = {"0", "1", "2"};
    public string[] serviceFeeTip = { "0", "1%", "2%", "3%", "4%", "5%" };  //服务费 add by yang yang 2021年3月22日 12:02:18
    //	public string[] noteBeforeTip;
    public string[] noteBeforeTip = {"0|1|2","0|2|4","0|2|5|10","0|2|5|10|15|20","0|5|10|15|20|25|30|40","0|5|10|15|20|30|40|50",
		"0|10|20|30|40|50|75|100","0|20|40|50|75|100|150|200","0|25|50|100|150|200|300|400","0|25|50|100|200|300|400|600",
		"0|50|100|200|400|600|800|1000", "0|100|200|400|800|1200|1600|2000"};

    private Slider autoStartNumSilder;
    public Transform autoStartNumFrom;
    public string[] autoStartNumTip = {"0", "2"};
    private Text[] autoStartNumText = new Text[9];
    
    private Transform unionForm;
    private Transform unionContentForm;
    private Transform unionCoinForm;

    void Awake()
    {
        Init();
        roomName = transform.Find("BG/Title/RoomName").GetComponent<InputField>();
        back = transform.Find("BG/Top/Back").GetComponent<Button>();


		GameToggles = transform.Find("BG/Top/ToggleGroup").GetComponentsInChildren<Toggle>();

		Transform content = transform.Find ("BG/Scroll View/Viewport/Content");

        mangForm = content.Find("Mangset");
        noteBeforeForm = content.Find("NoteBefore");
        mangduanForm = content.Find("Mangset_duan");

        mangduanForm_before = mangduanForm.Find("Mang").GetComponent<Text>();
        mangduanForm_dairu = mangduanForm.Find("Little").GetComponent<Text>();
        mangduanForm_slider = mangduanForm.Find("Chouma").GetComponent<Slider>();
        mangduanForm_currDiamond = mangduanForm.Find("CurrDiamond/value").GetComponent<Text>();
        mangduanForm_costDiamond = mangduanForm.Find("CostDiamond/value").GetComponent<Text>();

        mang = content.Find("Mangset/Mang").GetComponent<Text>();
		little = content.Find("Mangset/Little").GetComponent<Text>();
		mangSlider = content.Find("Mangset/Chouma").GetComponent<Slider>();
		currDiamond = content.Find("Mangset/CurrDiamond/value").GetComponent<Text>();
		costDiamond = content.Find("Mangset/CostDiamond/value").GetComponent<Text>();

		currDiamondGameObject = content.Find("Mangset/CurrDiamond").gameObject;
		costDiamondGameObject = content.Find("Mangset/CostDiamond").gameObject;

		timeSlider = content.Find("Baseset/GameTime/Silder").GetComponent<Slider>();
//		currentTimes = content.Find("Baseset/GameTime/Tip").GetComponentsInChildren<Text>();
		insurance = content.Find("Common/Panel/Insurance").GetComponent<Toggle>();
		authorize = content.Find("Common/Panel/Authorize").GetComponent<Toggle>();
		straddle = content.Find("Common/Panel/Straddle").GetComponent<Toggle>();
		abandon = content.Find("Common/Panel/Abandon").GetComponent<Toggle>();
		GPS = content.Find("Common/Panel/GPS").GetComponent<Toggle>();
		IP = content.Find("Common/Panel/IP").GetComponent<Toggle>();
        DelayKanpai = content.Find("Common/Panel/DelayKanpai").GetComponent<Toggle>();
        minJinfenInput = content.Find("Common/MinJifen/InputField").GetComponent<InputField>();
        MinJifen = content.Find("Common/MinJifen/InputField/Text").GetComponent<Text>();
        
//        advance = transform.Find("BG/Scroll View/Viewport/Content/Common/Advance").GetComponent<Button>();
		create = transform.Find("BG/Create").GetComponent<Button>();
//        advanceMenu = transform.Find("BG/Scroll View/Viewport/Content/AdvanceMenu").gameObject;
		noteBefore = content.Find("NoteBefore/Silder").GetComponent<Slider>();
		positionCount = content.Find("Baseset/PositionCount/Silder").GetComponent<Slider>();
		
        dairuMin = content.Find("Baseset/Dairu/DoubleSilder/Min").GetComponent<Slider>();
		dairuMax = content.Find("Baseset/Dairu/DoubleSilder/Max").GetComponent<Slider>();
//		noteBeforeText = content.Find("NoteBefore/Tip/0").GetComponent<Text>();
		noteBeforeTipFrom = content.Find("NoteBefore/Tip");

        serviceFeeTipFrom_slider = content.Find("ServiceFee/Silder").GetComponent<Slider>();
        serviceFeeTipFrom = content.Find("ServiceFee/Tip");
        //		positionCountText = content.Find("Baseset/PositionCount/Tip").GetComponentsInChildren<Text>();
        //		dairuText = content.Find("Baseset/Dairu/Tip").GetComponentsInChildren<Text>();

        //思考延时
        playerTimeSlider = content.Find("playerTimeTog/Silder").GetComponent<Slider>();
		playerTimeTipFrom = content.Find("playerTimeTog/Tip");

		renshuTipFrom = content.Find("Baseset/PositionCount/Tip");
		timeTipFrom = content.Find("Baseset/GameTime/Tip");
		beishuTipFrom = content.Find("Baseset/Dairu/Tip");

        autoStartNumSilder = content.Find("autoStartNum/Silder").GetComponent<Slider>();
        autoStartNumFrom = content.Find("autoStartNum/Tip");
        
        mangForm.gameObject.SetActive(true);
        noteBeforeForm.gameObject.SetActive(true);

        unionForm = content.Find("Union");
        unionContentForm = unionForm.Find("info/Viewport/Content");
        unionCoinForm = content.Find("Common/Panel/UnionCoin");
        
        initTip ();

		//ChangeBeforeTip (0);

		mangSlider.GetComponent<SliderBar>().maxCount = littlemangs.Length-1;

		//ChangeBeforeTip (0);

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HideView();
        });


        GameToggles[0].onValueChanged.AddListener(
            (bool s) =>
            {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                if (GameToggles[0].isOn)
                {
                    selectGameType = 0;
                    NetMngr.GetSingleton().Send(InterfaceMain.GetRoundOld, new object[] {selectGameType.ToString()});
                    mangForm.gameObject.SetActive(true);
                    noteBeforeForm.gameObject.SetActive(true);
                    mangduanForm.gameObject.SetActive(false);
                }
            });

        GameToggles[1].onValueChanged.AddListener(
         (bool s) =>
         {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
             if (GameToggles[1].isOn)
             {
                selectGameType = 1;
                NetMngr.GetSingleton().Send(InterfaceMain.GetRoundOld, new object[] {selectGameType.ToString()});
                 mangForm.gameObject.SetActive(true);
                 noteBeforeForm.gameObject.SetActive(true);
                 mangduanForm.gameObject.SetActive(false);
             }
         });


        GameToggles[2].onValueChanged.AddListener(
         (bool s) =>
         {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
             if (GameToggles[2].isOn)
             {
                selectGameType = 2;
                NetMngr.GetSingleton().Send(InterfaceMain.GetRoundOld, new object[] {selectGameType.ToString()});                    
                 mangForm.gameObject.SetActive(true);
                 noteBeforeForm.gameObject.SetActive(true);
                 mangduanForm.gameObject.SetActive(false);
             }
         });



        create.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            CreateRoom();
        });

        mangduanForm_slider.onValueChanged.AddListener((value) =>
        {
            resetDuanForm((int)value);
        });

        mangSlider.onValueChanged.AddListener((value) =>
        {
            //记得通过value的值改变mang和little的值;
            mang.text = littlemangs[(int)value] + "/" + (int.Parse(littlemangs[(int)value]) * 2);
            little.text = (int.Parse(littlemangs[(int)value]) * 200 * (dairuMin.value < dairuMax.value ? dairuMin.value + 1 : dairuMax.value + 1)).ToString();

			if(createType == 2)
			{
				if (timeSlider.value == 0)
				{
					costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value])).ToString();
				}
				else
				{
					costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value]) * 2 * (int)timeSlider.value).ToString();
				}
			}
           

			//修改前注
			ChangeBeforeTip((int)value);

        });
        timeSlider.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < timeSlider.maxValue + 1; i++)
            {
                if (value == i)
                {
                    currentTimes[i].color = timeSlider.GetComponent<SliderBar>().color;
                }
                else
                {
                    currentTimes[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                }
            }
			if(createType == 2)
			{
				if (timeSlider.value == 0)
				{
					costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value])).ToString();
				}
				else
				{
					costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value]) * 2 * (int)timeSlider.value).ToString();
				}
			}
            
        });
        noteBefore.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < noteBefore.maxValue + 1; i++)
            {
                if (value == i)
                {
                    noteBeforeText[i].color = timeSlider.GetComponent<SliderBar>().color;
                }
                else
                {
                    noteBeforeText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                }
            }
        });
        positionCount.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < positionCount.maxValue + 1; i++)
            {
                if (value == i)
                {
                    positionCountText[i].color = timeSlider.GetComponent<SliderBar>().color;
                }
                else
                {
                    positionCountText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                }
            }
            ChangeAutoStartNumTip((int)value);
        });
        dairuMin.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < dairuMin.maxValue + 1; i++)
            {
                if (dairuMin.value < dairuMax.value)
                {
                    if (i < dairuMin.value || i > dairuMax.value)
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                    }
                    else
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().color;
                    }
                }
                else if (dairuMin.value > dairuMax.value)
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                    }
                    else
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().color;
                    }
                }
                else
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                    }
                    else
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().color;
                    }
                }
            }
            little.text = (int.Parse(littlemangs[(int)mangSlider.value]) * 200 * (dairuMin.value < dairuMax.value ? dairuMin.value + 1 : dairuMax.value + 1)).ToString();
        });
        dairuMax.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < dairuMax.maxValue + 1; i++)
            {
                if (dairuMin.value < dairuMax.value)
                {
                    if (i < dairuMin.value || i > dairuMax.value)
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                    }
                    else
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().color;
                    }
                }
                else if (dairuMin.value > dairuMax.value)
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                    }
                    else
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().color;
                    }
                }
                else
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                    }
                    else
                    {
                        dairuText[i].color = timeSlider.GetComponent<SliderBar>().color;
                    }
                }
            }
            little.text = (int.Parse(littlemangs[(int)mangSlider.value]) * 200 * (dairuMin.value < dairuMax.value ? dairuMin.value + 1 : dairuMax.value + 1)).ToString();
        });

        //add by yang yang 2021年3月22日 13:33:07  
        serviceFeeTipFrom_slider.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < serviceFeeTipFrom_slider.maxValue + 1; i++)
            {
                if (value == i)
                {
                    serviceFeeText[i].color = serviceFeeTipFrom_slider.GetComponent<SliderBar>().color;
                }
                else
                {
                    serviceFeeText[i].color = serviceFeeTipFrom_slider.GetComponent<SliderBar>().backGround;
                }
            }
        });
        //end
        playerTimeSlider.onValueChanged.AddListener((value) =>
		{
        	for (int i = 0; i < playerTimeSlider.maxValue + 1; i++)
			{
				if (value == i)
				{
					playerTimeText[i].color = timeSlider.GetComponent<SliderBar>().color;
				}
				else
				{
					playerTimeText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
				}
			}
		});

        autoStartNumSilder.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < autoStartNumSilder.maxValue + 1; i++)
            {
                if (value == i)
                {
                    autoStartNumText[i].color = timeSlider.GetComponent<SliderBar>().color;
                }
                else
                {
                    autoStartNumText[i].color = timeSlider.GetComponent<SliderBar>().backGround;
                }
            }
        });
		
    }

    public void resetDuanForm(int value)
    {
        //记得通过value的值改变mang和little的值;
        mangduanForm_before.text = duan_noteBeforeTip[value];
        mangduanForm_dairu.text = (int.Parse(duan_noteBeforeTip[value]) * 100 * (dairuMin.value < dairuMax.value ? dairuMin.value + 1 : dairuMax.value + 1)).ToString();

        if (createType == 2)
        {
            if (timeSlider.value == 0)
            {
                mangduanForm_currDiamond.text = (int.Parse(costDiamondStrs[(int)mangduanForm_slider.value])).ToString();
            }
            else
            {
                mangduanForm_costDiamond.text = (int.Parse(costDiamondStrs[(int)mangduanForm_slider.value]) * 2 * (int)mangduanForm_slider.value).ToString();
            }
        }
    }

	public void ChangeBeforeTip(int index)
	{
		noteBefore.value = 0;
		string btip = noteBeforeTip [index];
		string[] btips = btip.Split ('|');
		noteBefore.maxValue = btips.Length-1;
		string logs = "";
		for (int i = 0; i < btips.Length; i++) {
			logs = logs + btips [i];
		}

		Debug.Log ("btips:" + logs.ToString ());

		noteBefore.GetComponent<SliderBar> ().resetPoint (btips.Length-1);

		int childCount = noteBeforeTipFrom.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Destroy(noteBeforeTipFrom.GetChild(i).gameObject);
		}

		float lenth = noteBeforeTipFrom.GetComponent<RectTransform>().rect.width;
		float x = lenth / (btips.Length-1);
		float xx = 0;
		Object objItem = Resources.Load("Fix/ContainerPopup/CreateGameTipText");
		for (int i = 0; i < btips.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(noteBeforeTipFrom);
			go.transform.localScale = Vector3.one;
			RectTransform rect = go.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = new Vector3(xx, 0, 0);
			Text btext =  go.GetComponent<Text>();
			btext.text = btips [i].ToString ();
			xx += x;
			noteBeforeText [i] = btext;
		}

	}

    public void ChangeAutoStartNumTip(int index){
        autoStartNumSilder.value = 0;
        int len = int.Parse(positionCountTip[index]);
        autoStartNumSilder.maxValue = len;
        autoStartNumSilder.GetComponent<SliderBar>().resetPoint(len-1);

        int childCount = autoStartNumFrom.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(autoStartNumFrom.GetChild(i).gameObject);
        }

        float lenth = autoStartNumFrom.GetComponent<RectTransform>().rect.width;
        float x = lenth / (len - 1);
        float xx = 0;
        Object objItem = Resources.Load("Fix/ContainerPopup/CreateGameTipText");
        for (int i = 0; i < len; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(autoStartNumFrom);
            go.transform.localScale = Vector3.one;
            RectTransform rect = go.GetComponent<RectTransform> ();
            rect.anchoredPosition3D = new Vector3(xx, 0, 0);
            Text btext =  go.GetComponent<Text>();
            if(i > 0){
                btext.text = (2+i-1).ToString();    
            } else {
                btext.text = "0"; 
            }
            autoStartNumText[i] = btext;
            
            xx += x;
        }
    }

	private void initTip()
	{
		float lenth = renshuTipFrom.GetComponent<RectTransform>().rect.width;
		float x = lenth / (positionCountTip.Length-1);
		float xx = 0;
		Object objItem = Resources.Load("Fix/ContainerPopup/CreateGameTipText");
		for (int i = 0; i < positionCountTip.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(renshuTipFrom);
			go.transform.localScale = Vector3.one;
			RectTransform rect = go.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = new Vector3(xx, 0, 0);
			Text btext =  go.GetComponent<Text>();
			btext.text = positionCountTip [i].ToString ();
			xx += x;
			positionCountText [i] = btext;
		}

		lenth = timeTipFrom.GetComponent<RectTransform>().rect.width;
		x = lenth / (timeTopTip.Length-1);
		xx = 0;
		for (int i = 0; i < timeTopTip.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(timeTipFrom);
			go.transform.localScale = Vector3.one;
			RectTransform rect = go.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = new Vector3(xx, 0, 0);
			Text btext =  go.GetComponent<Text>();
			btext.text = timeTopTip [i].ToString ();
			xx += x;
			currentTimes [i] = btext;
		}

		lenth = beishuTipFrom.GetComponent<RectTransform>().rect.width;
		x = lenth / (dairuTip.Length-1);
		xx = 0;
		for (int i = 0; i < dairuTip.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(beishuTipFrom);
			go.transform.localScale = Vector3.one;
			RectTransform rect = go.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = new Vector3(xx, 0, 0);
			Text btext =  go.GetComponent<Text>();
			btext.text = dairuTip [i].ToString ();
			xx += x;
			dairuText [i] = btext;
		}

		lenth = noteBeforeTipFrom.GetComponent<RectTransform>().rect.width;
		x = lenth / (noteBeforeTip0.Length-1);
		xx = 0;
		for (int i = 0; i < noteBeforeTip0.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(noteBeforeTipFrom);
			go.transform.localScale = Vector3.one;
			RectTransform rect = go.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = new Vector3(xx, 0, 0);
			Text btext =  go.GetComponent<Text>();
			btext.text = noteBeforeTip0 [i].ToString ();
			xx += x;
			noteBeforeText [i] = btext;
		}

        //服务费  //add by yang yang  2021年3月22日 13:24:09
        lenth = serviceFeeTipFrom.GetComponent<RectTransform>().rect.width;
        x = lenth / (serviceFeeTip.Length - 1);
        xx = 0;
        for (int i = 0; i < serviceFeeTip.Length; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(serviceFeeTipFrom);
            go.transform.localScale = Vector3.one;
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3(xx, 0, 0);
            Text btext = go.GetComponent<Text>();
            btext.text = serviceFeeTip[i].ToString();
            xx += x;
            serviceFeeText[i] = btext;
        }
        //end 

        lenth = playerTimeTipFrom.GetComponent<RectTransform>().rect.width;
		x = lenth / (playeTimeTopTip.Length-1);
		xx = 0;
		for (int i = 0; i < playeTimeTopTip.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(playerTimeTipFrom);
			go.transform.localScale = Vector3.one;
			RectTransform rect = go.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = new Vector3(xx, 0, 0);
			Text btext =  go.GetComponent<Text>();
			btext.text = playeTimeTopTip [i].ToString ();
			xx += x;
			playerTimeText [i] = btext;
		}

        // 自动开局人数
        lenth = autoStartNumFrom.GetComponent<RectTransform>().rect.width;
        x = lenth / (autoStartNumTip.Length-1);
        xx = 0;
        for (int i = 0; i < autoStartNumTip.Length; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(autoStartNumFrom);
            go.transform.localScale = Vector3.one;
            RectTransform rect = go.GetComponent<RectTransform> ();
            rect.anchoredPosition3D = new Vector3(xx, 0, 0);
            Text btext =  go.GetComponent<Text>();
            btext.text = autoStartNumTip[i].ToString();
            xx += x;
            autoStartNumText[i] = btext;
        }
		
	}

    private void CreateRoom()
    {
		int gameType = selectGameType;//0 德州 1 奥马哈 2短牌
		// for (int i = 0; i < GameToggles.Length; i++) {
		// 	if (GameToggles [i].isOn == true) {
		// 		gameType = i;
		// 	}
		// }

        int min = 1;
        int max = 1;
        if (dairuMin.value < dairuMax.value)
        {
            min = (int)dairuMin.value + 1;
            max = (int)dairuMax.value + 1;
        }
        else
        {
            max = (int)dairuMin.value + 1;
            min = (int)dairuMax.value + 1;
        }

        string time = timeValues[(int)timeSlider.value];
        // switch ((int)timeSlider.value)
        // {
        //     case 0:
        //         time = "30";
        //         break;
        //     case 1:
        //         time = "60";
        //         break;
        //     case 2:
        //         time = "120";
        //         break;
        //     case 3:
        //         time = "180";
        //         break;
        //     case 4:
        //         time = "240";
        //         break;
        //     case 5:
        //         time = "300";
        //         break;
        //     case 6:
        //         time = "360";
        //         break;
        // }

		int time1 = int.Parse(playerTimeValues[(int)playerTimeSlider.value]);
		// switch ((int)playerTimeSlider.value)
		// {
		// case 0:
		// 	time1 = 10;
		// 	break;
		// case 1:
		// 	time1 = 12;
		// 	break;
		// case 2:
		// 	time1 = 15;
		// 	break;
		// case 3:
		// 	time1 = 20;
		// 	break;
		// }

        string[] str = this.mang.text.Split('/');
        string btip = noteBeforeTip [(int)mangSlider.value];
		string[] btips = btip.Split ('|');
		string txtbefore = btips[(int)noteBefore.value];
        string txtServiceFee = ((int)serviceFeeTipFrom_slider.value).ToString(); // btips[(int)serviceFeeTipFrom_slider.value];
        string autoStartNum = autoStartNumText[(int)autoStartNumSilder.value].text;
        string unionId = "0";
        if(unionForm.gameObject.activeSelf){
            Toggle[] unionToggles = unionContentForm.GetComponentsInChildren<Toggle>();
            for(int i = 0; i < unionToggles.Length; i++){
                if(unionToggles[i].isOn){
                    Transform tr = unionToggles[i].gameObject.transform.Find("id");
                    unionId = tr.GetComponent<Text>().text;
                    break;
                }
            }
        }
        if(unionId == "0" && unionCoinForm.GetComponent<Toggle>().isOn){
            PopupCommon.GetSingleton().ShowView("勾选联盟币选项，但没选择同步联盟，创建失败！");
            return;
        }

        if (gameType == 0) {
			if (createType == 2) {
                NetMngr.GetSingleton().Send(InterfaceMain.CreateGame, new object[] {
                    ClubManager.GetSingleton ().clubPanel.clubId,
                    roomName.text,
                    str [0] + "|" + str [1],
                    currDiamond.text,
                    costDiamond.text,
                    time,
                    insurance.isOn ? "1" : "0",
                    authorize.isOn ? "1" : "0",
                    straddle.isOn ? "1" : "0",
                    abandon.isOn ? "1" : "0",
                    GPS.isOn ? "1" : "0",
                    IP.isOn ? "1" : "0",
                    txtbefore.ToString (),
                    (positionCount.value + 2).ToString (),
                    min.ToString (),
                    max.ToString (),
                    time1.ToString (),
                    txtServiceFee,  //add by yang yang 2021年3月22日 18:03:07
                    DelayKanpai.isOn ? "1" : "0",
                    MinJifen.text == "" ? "0" : MinJifen.text, // 最小带入记分牌
                    autoStartNum, // 自动开局人数
                    unionId,
                    unionCoinForm.GetComponent<Toggle>().isOn ? "1" : "0" ,
				});
			} else {
				NetMngr.GetSingleton ().Send (InterfaceMain.CreateGame, new object[] {
                    "",
                    roomName.text,
					str [0] + "|" + str [1],
					"0",
					"0",
					time,
					insurance.isOn ? "1" : "0",
					authorize.isOn ? "1" : "0",
					straddle.isOn ? "1" : "0",
					abandon.isOn ? "1" : "0",
					GPS.isOn ? "1" : "0",
					IP.isOn ? "1" : "0",
                    txtbefore.ToString (),
					(positionCount.value + 2).ToString (),
					min.ToString (),
					max.ToString (),
					time1.ToString (),
                    txtServiceFee,  //add by yang yang 2021年3月22日 18:03:07
                    DelayKanpai.isOn ? "1" : "0",
                    MinJifen.text == "" ? "0" : MinJifen.text, // 最小带入记分牌
                    autoStartNum, // 自动开局人数
                    unionId,
                    unionCoinForm.GetComponent<Toggle>().isOn ? "1" : "0" ,
                });
			}
		}
        else if (gameType == 1)
        {

			if (createType == 2) {
				NetMngr.GetSingleton ().Send (InterfaceMain.CreateAmhGame, new object[] {
					ClubManager.GetSingleton ().clubPanel.clubId,
					roomName.text,
					str [0] + "|" + str [1],
					currDiamond.text,
					costDiamond.text,
					time,
					insurance.isOn ? "1" : "0",
					authorize.isOn ? "1" : "0",
					straddle.isOn ? "1" : "0",
					abandon.isOn ? "1" : "0",
					GPS.isOn ? "1" : "0",
					IP.isOn ? "1" : "0",
                    txtbefore.ToString (),
					(positionCount.value + 2).ToString (),
					min.ToString (),
					max.ToString (),
					time1.ToString (),
                    txtServiceFee,  //add by yang yang 2021年3月22日 18:03:07
                    DelayKanpai.isOn ? "1" : "0",
                    MinJifen.text == "" ? "0" : MinJifen.text, // 最小带入记分牌
                    autoStartNum, // 自动开局人数
                    unionId,
                    unionCoinForm.GetComponent<Toggle>().isOn ? "1" : "0" ,
				});
			} else {
				NetMngr.GetSingleton ().Send (InterfaceMain.CreateAmhGame, new object[] {
                    "",
                    roomName.text,
					str [0] + "|" + str [1],
					"0",
					"0",
					time,
					insurance.isOn ? "1" : "0",
					authorize.isOn ? "1" : "0",
					straddle.isOn ? "1" : "0",
					abandon.isOn ? "1" : "0",
					GPS.isOn ? "1" : "0",
					IP.isOn ? "1" : "0",
                    txtbefore.ToString (),
					(positionCount.value + 2).ToString (),
					min.ToString (),
					max.ToString (),
					time1.ToString (),
                    txtServiceFee,  //add by yang yang 2021年3月22日 18:03:07
                    DelayKanpai.isOn ? "1" : "0",
                    MinJifen.text == "" ? "0" : MinJifen.text, // 最小带入记分牌
                    autoStartNum, // 自动开局人数
                    unionId,
                    unionCoinForm.GetComponent<Toggle>().isOn ? "1" : "0" ,
				});
			}
			
		}
        else if (gameType == 2)
        {
            if (createType == 2)
            {
                NetMngr.GetSingleton().Send(InterfaceMain.CreateShortGame, new object[] {
                    ClubManager.GetSingleton ().clubPanel.clubId,
                    roomName.text,
                    str [0] + "|" + str [1],
                    currDiamond.text,
                    costDiamond.text,
                    time,
                    insurance.isOn ? "1" : "0",
                    authorize.isOn ? "1" : "0",
                    straddle.isOn ? "1" : "0",
                    abandon.isOn ? "1" : "0",
                    GPS.isOn ? "1" : "0",
                    IP.isOn ? "1" : "0",
                    txtbefore.ToString (),
                    (positionCount.value + 2).ToString (),
                    min.ToString (),
                    max.ToString (),
                    time1.ToString (),
                    txtServiceFee,  //add by yang yang 2021年3月22日 18:03:07
                    DelayKanpai.isOn ? "1" : "0",
                    MinJifen.text == "" ? "0" : MinJifen.text, // 最小带入记分牌
                    autoStartNum, // 自动开局人数
                    unionId,
                    unionCoinForm.GetComponent<Toggle>().isOn ? "1" : "0" ,
                });
            }
            else
            {
                NetMngr.GetSingleton().Send(InterfaceMain.CreateShortGame, new object[] {
                    "",
                    roomName.text,
                    str [0] + "|" + str [1],
                    "0",
                    "0",
                    time,
                    insurance.isOn ? "1" : "0",
                    authorize.isOn ? "1" : "0",
                    straddle.isOn ? "1" : "0",
                    abandon.isOn ? "1" : "0",
                    GPS.isOn ? "1" : "0",
                    IP.isOn ? "1" : "0",
                    txtbefore.ToString (),
                    (positionCount.value + 2).ToString (),
                    min.ToString (),
                    max.ToString (),
                    time1.ToString (),
                    txtServiceFee,  //add by yang yang 2021年3月22日 18:03:07
                    DelayKanpai.isOn ? "1" : "0",
                    MinJifen.text == "" ? "0" : MinJifen.text, // 最小带入记分牌
                    autoStartNum, // 自动开局人数
                    unionId,
                    unionCoinForm.GetComponent<Toggle>().isOn ? "1" : "0" ,
                });
            }

        }


    }

    void Start()
    {

    }

    public void CostDiamondCountFinished(string cost)
    {
        costDiamondStrs = cost.Split('|');
        currDiamond.text = StaticData.diamond.ToString();
        if (timeSlider.value == 0)
        {
            costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value])).ToString();
        }
        else
        {
            costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value]) * 2 * (int)timeSlider.value).ToString();
        }
    }


    void Update()
    {

    }

	//ctype 1普通 2俱乐部
	public void ShowView(int ctype)
    {
        createType = ctype;
       
		if (StaticData.littlemangs.Length == 0) {
			NetMngr.GetSingleton ().Send (InterfaceMain.GetUpdatePar, new object[] { "4" });
		} 
		else {
			littlemangs = StaticData.littlemangs;
			noteBeforeTip = StaticData.noteBeforeTip;
		}

		if (ctype == 2) {
			NetMngr.GetSingleton().Send(InterfaceMain.GetUpdatePar, new object[] { "1" });
            NetMngr.GetSingleton().Send(InterfaceClub.GetClubUnion, new object[] {
             ClubManager.GetSingleton().clubPanel.clubId} );
		}
        base.ShowView();

        roomName.text = "";
        timeSlider.value = timeSlider.minValue;
        mangSlider.value = mangSlider.minValue;
        mangduanForm_slider.value = mangduanForm_slider.minValue;
        insurance.isOn = false;
        authorize.isOn = false;
        straddle.isOn = false;
        abandon.isOn = false;
        GPS.isOn = false;
        IP.isOn = false;
        noteBefore.value = 0;
        positionCount.value = 0;

		if (ctype == 1) {
			authorize.gameObject.SetActive(false);
			currDiamondGameObject.SetActive(false);
			costDiamondGameObject.SetActive(false);
            serviceFeeTipFrom.parent.gameObject.SetActive(false);
            unionForm.gameObject.SetActive(false);

        } else {
			authorize.gameObject.SetActive(true);
			currDiamondGameObject.SetActive(true);
			costDiamondGameObject.SetActive(true);
            serviceFeeTipFrom.parent.gameObject.SetActive(true);
            unionForm.gameObject.SetActive(true);
            GameTools.GetSingleton().HideSv(unionContentForm);
        }
        unionCoinForm.gameObject.SetActive(false);
        DelayKanpai.gameObject.SetActive(false);
        unionCoinForm.GetComponent<Toggle>().isOn = false;
        //resetDuanForm((int)mangduanForm_slider.value);

        NetMngr.GetSingleton().Send(InterfaceMain.GetRoundOld, new object[] {selectGameType.ToString()});

    }

    public void SetClubUnion(Hashtable data){
        if(data["success"].ToString() == "0"){
            return;
        }
        ArrayList info = data["list"] as ArrayList;
        if(info.Count == 0){
            return;
        }
        for(int i = 0; i < info.Count; i++){
            Hashtable h = info[i] as Hashtable;
            GameObject cell = GameTools.GetSingleton().GetSvCell(unionContentForm);
            cell.transform.Find("name").GetComponent<Text>().text = h["lmName"].ToString();
            cell.transform.Find("id").GetComponent<Text>().text = h["lmId"].ToString();
            cell.transform.GetComponent<Toggle>().isOn = false;

            if(h["lmType"].ToString() == "2"){ // 超级联盟
                unionCoinForm.gameObject.SetActive(true);
            }
        }
    }

    public void Setting(Hashtable data){
        if(createType == 1) return; // 私局不需要记忆功能
        // 大小盲
        int mangSliderValue = 0;
        int timeSliderValue = 0;
        int noteBeforeSilderValue = 0;
        int playerTimeSliderValue = 0;
        int positionCountSliderValue = 0;
        if (data["success"].ToString() == "1"){
            for(int i = 0; i < littlemangs.Length; i++){
                if(littlemangs[i] == data["smallBlind"].ToString()){
                    mangSliderValue = i;
                    break;
                }
            }
            for(int i = 0; i < timeValues.Length; i++){
                if(timeValues[i] == data["roundTime"].ToString()){
                    timeSliderValue = i;
                    break;
                }    
            }
            string[] btips = noteBeforeTip[mangSliderValue].Split('|');
            for(int i = 0; i < btips.Length; i++){
                if(btips[i] == data["ante"].ToString()){
                    noteBeforeSilderValue = i;
                    break;
                }
            }
            for(int i = 0; i < playerTimeValues.Length; i++){
                if(playerTimeValues[i] == data["reflectTime"].ToString()){
                    playerTimeSliderValue = i;
                    break;
                }
            }
            for(int i = 0; i < positionCountTip.Length; i++){
                if(positionCountTip[i] == data["deskCount"].ToString()){
                    positionCountSliderValue = i;
                    break;
                }    
            }
        } 
        mang.text = littlemangs[(int)mangSliderValue] + "/" + (int.Parse(littlemangs[(int)mangSliderValue]) * 2);
        little.text = (int.Parse(littlemangs[(int)mangSliderValue]) * 200 * (dairuMin.value < dairuMax.value ? dairuMin.value + 1 : dairuMax.value + 1)).ToString();
        mangSlider.GetComponent<SliderBar>().SetValue(mangSliderValue);
        timeSlider.GetComponent<SliderBar>().SetValue(timeSliderValue);
        if(createType == 2)
        {
            if (timeSlider.value == 0)
            {
                costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value])).ToString();
            }
            else
            {
                costDiamond.text = (int.Parse(costDiamondStrs[(int)mangSlider.value]) * 2 * (int)timeSlider.value).ToString();
            }
        }
        //修改前注
        ChangeBeforeTip((int)mangSliderValue);
        noteBefore.GetComponent<SliderBar>().SetValue(noteBeforeSilderValue);
        playerTimeSlider.GetComponent<SliderBar>().SetValue(playerTimeSliderValue);
        positionCount.GetComponent<SliderBar>().SetValue(positionCountSliderValue);
        ChangeAutoStartNumTip(positionCountSliderValue);

        int autoStartNumSilderValue = 0;
        int dairuMinSliderValue = 0;
        int dairuMaxSliderValue = 0;
        int serviceFeeTipFrom_sliderValue = 0;
        if (data["success"].ToString() == "1"){
            for(int i = 0; i < autoStartNumText.Length; i++){
                if(autoStartNumText[i].text == data["startGameUser"].ToString()){
                    autoStartNumSilderValue = i;
                    break;
                }
            }
            for(int i = 0; i < dairuTip.Length; i++){
                if(dairuTip[i] == data["minBringCoin"].ToString()){
                    dairuMinSliderValue = i;
                    break;
                }
            }
            for(int i = 0; i < dairuTip.Length; i++){
                if(dairuTip[i] == data["maxBringCoin"].ToString()){
                    dairuMaxSliderValue = i;
                    break;
                }
            }
            serviceFeeTipFrom_sliderValue = int.Parse(data["free"].ToString());
        }
        autoStartNumSilder.GetComponent<SliderBar>().SetValue(autoStartNumSilderValue);
        dairuMin.value = dairuMinSliderValue;
        dairuMax.value = dairuMaxSliderValue;
        serviceFeeTipFrom_slider.GetComponent<SliderBar>().SetValue(serviceFeeTipFrom_sliderValue);
        
        if(data["success"].ToString() == "1"){
            minJinfenInput.text = data["minBringCoin2"].ToString();
            insurance.isOn = data["insurance"].ToString() == "1";
            authorize.isOn = data["bringCoinAuthorization"].ToString() == "1";
            straddle.isOn = data["straddle"].ToString() == "1";
            abandon.isOn = data["autoMuck"].ToString() == "1";
            GPS.isOn = data["distanceLimit"].ToString() == "1";
            IP.isOn = data["ipLimit"].ToString() == "1";
            DelayKanpai.isOn = data["delaySeeCard"].ToString() == "1";
            unionCoinForm.GetComponent<Toggle>().isOn = data["lmType"].ToString() == "1";
        } else {
            minJinfenInput.text = "";
            insurance.isOn = false;
            authorize.isOn = false;
            straddle.isOn = false;
            abandon.isOn = false;
            GPS.isOn = false;
            IP.isOn = false;
            DelayKanpai.isOn = false;
            unionCoinForm.GetComponent<Toggle>().isOn = false;
        }

    }

}
