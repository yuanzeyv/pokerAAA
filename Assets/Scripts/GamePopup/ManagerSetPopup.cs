using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ManagerSetPopup : BasePopup {

    private Toggle Authorize;
    private Toggle Straddle;
	private Text[] dairuText= new Text[8]; 
    private Slider dairuMin;
    private Slider dairuMax;
    private Slider GameTimeTogSlider;
	private Text[] paijuText = new Text[7];
    private SliderBar GameTimeTogDouSlider;
    private Text currCoin;
    private Text costCoin;
    private Slider qianZhuSlider;
	private Text[] qianzhuText = new Text[20]; 
    private Slider playerTimeSlider;
	private Text[] playerTimeText = new Text[4];
    private SliderBar playerTimeDouSlider;
    private Button Back;
    private Button Save;

	public Transform noteBeforeTipFrom;
	public Transform beishuTipFrom;
	public Transform timeTipFrom;
	public Transform playerTimeTipFrom;

    private int little=0;

	public string[] timeTopTip = {"0h","0.5h", "1h", "2h", "3h", "4h", "5h"};
	public string[] dairuTip = {"1", "2", "3", "4", "5", "6", "7", "8" };
	public string[] playeTimeTopTip = {"10s","12s", "15s", "20s"};
	public string[] noteBeforeTip;
	public string[] littlemangs;
    private string strs;

    private void Awake()
    {
        Init();
        Authorize = transform.Find("BG/Authorize").GetComponent<Toggle>();
        Straddle = transform.Find("BG/Straddle").GetComponent<Toggle>();

		Transform content = transform.Find ("BG/Scroll View/Viewport/Content");

		//延时
		GameTimeTogSlider = content.Find("GameTimeTog/Silder").GetComponent<Slider>();
		GameTimeTogDouSlider = content.Find("GameTimeTog/Silder").GetComponent<SliderBar>();
		currCoin = content.Find("GameTimeTog/CurrDiamond/value").GetComponent<Text>();
		costCoin = content.Find("GameTimeTog/CostDiamond/value").GetComponent<Text>();
		timeTipFrom = content.Find("GameTimeTog/Tip");

		//前注
		qianZhuSlider = content.Find("NoteBefore/Silder").GetComponent<Slider>();
		noteBeforeTipFrom = content.Find("NoteBefore/Tip");

		//带入
		dairuMin = content.Find("Dairu/DoubleSilder/Min").GetComponent<Slider>();
		dairuMax = content.Find("Dairu/DoubleSilder/Max").GetComponent<Slider>();
		beishuTipFrom = content.Find("Dairu/Tip");

		//思考延时
		playerTimeSlider = content.Find("playerTimeTog/Silder").GetComponent<Slider>();
		playerTimeTipFrom = content.Find("playerTimeTog/Tip");

        Back = transform.Find("BG/Back").GetComponent<Button>();
        Save = transform.Find("BG/Save").GetComponent<Button>();


        Back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HideView();
        });
        Save.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");            
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
            double time = 30;
            switch ((int)GameTimeTogSlider.value)
            {
                case 0:
                    time = 0;
                    break;
                case 1:
                    time = 0.5f;
                    break;
                case 2:
                    time = 1;
                    break;
                case 3:
                    time = 2;
                    break;
                case 4:
                    time = 3;
                    break;
                case 5:
                    time = 4;
                    break;
                case 6:
                    time = 5;
                    break;
            }
            int time1 = 30;
            switch ((int)playerTimeSlider.value)
            {
                case 0:
                    time1 = 10;
                    break;
                case 1:
                    time1 = 12;
                    break;
                case 2:
                    time1 = 15;
                    break;
                case 3:
                    time1 = 20;
                    break;
            }

			string txtbefore = qianzhuText [(int)qianZhuSlider.value].text;

            //GameManager.GetSingleton().everyDelayTime = time1;
			NetMngr.GetSingleton().Send(InterfaceGame.setRoundDetail, new object[] {min,max,time.ToString(),int.Parse(txtbefore.ToString()),Authorize.isOn?1:0,Straddle.isOn?1:0 , time1 });
        });
        dairuMin.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < dairuMin.maxValue + 1; i++)
            {
                if (dairuMin.value < dairuMax.value)
                {
                    if (i < dairuMin.value || i > dairuMax.value)
                    {
                        dairuText[i].color = GameTimeTogDouSlider.backGround;
                    }
                    else
                    {
                        dairuText[i].color = GameTimeTogDouSlider.color;
                    }
                }
                else if (dairuMin.value > dairuMax.value)
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = GameTimeTogDouSlider.backGround;
                    }
                    else
                    {
                        dairuText[i].color = GameTimeTogDouSlider.color;
                    }
                }
                else
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = GameTimeTogDouSlider.backGround;
                    }
                    else
                    {
                        dairuText[i].color = GameTimeTogDouSlider.color;
                    }
                }
            }
        });
        dairuMax.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < dairuMax.maxValue + 1; i++)
            {
                if (dairuMin.value < dairuMax.value)
                {
                    if (i < dairuMin.value || i > dairuMax.value)
                    {
                        dairuText[i].color = GameTimeTogDouSlider.backGround;
                    }
                    else
                    {
                        dairuText[i].color = GameTimeTogDouSlider.color;
                    }
                }
                else if (dairuMin.value > dairuMax.value)
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = GameTimeTogDouSlider.backGround;
                    }
                    else
                    {
                        dairuText[i].color = GameTimeTogDouSlider.color;
                    }
                }
                else
                {
                    if (i > dairuMin.value || i < dairuMax.value)
                    {
                        dairuText[i].color = GameTimeTogDouSlider.backGround;
                    }
                    else
                    {
                        dairuText[i].color = GameTimeTogDouSlider.color;
                    }
                }
            }
        });
        GameTimeTogSlider.onValueChanged.AddListener((value) =>
        {
            if (GameTimeTogSlider.value == 0)
            {
                costCoin.text = "0";
            }
            else if (GameTimeTogSlider.value == 1)
            {
                costCoin.text = strs;
            }
            else
            {
                costCoin.text = ((GameTimeTogSlider.value - 1) * 2 * float.Parse(strs)).ToString();
            }
            for (int i = 0; i < GameTimeTogSlider.maxValue + 1; i++)
            {
                if (value == i)
                {
                    paijuText[i].color = GameTimeTogDouSlider.color;
                }
                else
                {
                    paijuText[i].color = GameTimeTogDouSlider.backGround;
                }
            }
        });
        qianZhuSlider.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < qianZhuSlider.maxValue + 1; i++)
            {
                if (value == i)
                {
					qianzhuText[i].color = GameTimeTogDouSlider.color;
                }
                else
                {
					qianzhuText[i].color = GameTimeTogDouSlider.backGround;
                }
            }
        });
        playerTimeSlider.onValueChanged.AddListener((value) =>
        {
            for (int i = 0; i < playerTimeSlider.maxValue + 1; i++)
            {
                if (value == i)
                {
					playerTimeText[i].color = GameTimeTogDouSlider.color;
                }
                else
                {
					playerTimeText[i].color = GameTimeTogDouSlider.backGround;
                }
            }
        });
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

	private int getLittleMangsIndex(int xm)
	{
		for(int i = 0;i<littlemangs.Length;i++)
		{
			if (int.Parse (littlemangs [i]) == xm) {
				return i;
			}
		}

		return 0;
	}

	public void initTip()
	{

		if (paijuText [0] != null)
			return;

		float lenth = timeTipFrom.GetComponent<RectTransform>().rect.width;
		float x = lenth / (timeTopTip.Length-1);
		float xx = 0;
		UnityEngine.Object objItem = Resources.Load ("Fix/ContainerPopup/CreateGameTipText");
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
			paijuText [i] = btext;
		}

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

		littlemangs = StaticData.littlemangs;
		noteBeforeTip = StaticData.noteBeforeTip;

		int xiaomang = GameManager.GetSingleton ().roomXiaoMang;

		qianZhuSlider.value = 0;
		int index = getLittleMangsIndex (xiaomang);
		string btip = noteBeforeTip [index];
		string[] btips = btip.Split ('|');
		qianZhuSlider.maxValue = btips.Length-1;
		qianZhuSlider.GetComponent<SliderBar> ().resetPoint (btips.Length-1);

		lenth = noteBeforeTipFrom.GetComponent<RectTransform>().rect.width;
		x = lenth / (btips.Length-1);
		xx = 0;
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
			qianzhuText [i] = btext;
		}

	}

    public void GetFinished(Hashtable data)
    {

		initTip ();

        little = int.Parse(data["little"].ToString());
        dairuMax.value = float.Parse(data["max"].ToString())-1;
        dairuMin.value = float.Parse(data["min"].ToString())-1;
        //qianZhuSlider.value = float.Parse(data["qian"].ToString());
        Authorize.isOn = int.Parse(data["dairu"].ToString()) == 1 ? true : false;
        Straddle.isOn = int.Parse(data["straddle"].ToString()) == 1 ? true : false;
        currCoin.text = StaticData.gold.ToString();
        GameTimeTogSlider.value = GameTimeTogSlider.minValue;
        //costCoin.text = (0.5 * 2 * little).ToString();

		qianZhuSlider.value = 0;
		for(int i=0;i<qianzhuText.Length;i++)
		{
			if (qianzhuText [i].text == data ["qian"].ToString ()) {
				qianZhuSlider.value = i;
				return;
			}
		}
        
    }

    public void Finished(Hashtable data)
    {
        Tip.Instance.showMsg(data["message"].ToString());
        //PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        if (data.Contains("time"))
        {

        }
        HideView();
    }

    public void SetData(string costcoin)
    {
		

        strs = costcoin;
        currCoin.text = StaticData.diamond.ToString();
        if (GameTimeTogSlider.value==0)
        {
            costCoin.text = "0";
        }
        else if (GameTimeTogSlider.value==1)
        {
            costCoin.text = strs;
        }
        else
        {
            costCoin.text = ((GameTimeTogSlider.value-1) * 2 * float.Parse(strs)).ToString();
        }
    }
    public void ShowView()
    {
        NetMngr.GetSingleton().Send(InterfaceGame.GetPaiJuInfo, new object[] { });
        NetMngr.GetSingleton().Send(InterfaceMain.GetUpdatePar, new object[] {"5" });
        base.ShowView();
    }
}
