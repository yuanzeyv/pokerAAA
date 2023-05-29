using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class DataIcon : MonoBehaviour {

    public float transformTime=2;
    public bool isGun = true;
    private Image image;
    private Text text1;
    private Text text2;
    private Image Tip;
    private Text TipText;

    private bool isStart = false;
    private bool isStartEnd = false;
    private float count;
    private float counted=0;
    private float fixedcount=0.1f;
    private float showTime = 0f;

	public Dictionary<int, DataIcon> dataicons;

    public bool isClick = false;

    private void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        text1 = transform.Find("Text1").GetComponent<Text>();
        text2 = transform.Find("Text2").GetComponent<Text>();
        Tip = transform.Find("Tip").GetComponent<Image>();
        TipText = transform.Find("Tip/Text").GetComponent<Text>();
    }
    private void Start()
    {
        Tip.gameObject.SetActive(true);
        HideTip(0);
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isClick)
            {
                HideTip(showTime);
                isClick = false;
            }
            else
            {
                ShowTip(showTime);
                isClick = true;
            }
			if(dataicons != null)
			{
				for (int i = 0; i < dataicons.Count; i++)
	            {
						DataIcon data = dataicons[i];
						if (this != data)
		                {

								data.isClick = false;
								data.HideTip(0);
		                }
		        }
			}
		});
    }

	void Update ()
    {
//        if (Input.GetKeyDown(KeyCode.K))
//        {
//            SetData(0.6f,"胜率");
//        }
//        if (Input.GetKeyDown(KeyCode.I))
//        {
//            Reset();
//        }
//        if (Input.GetKeyDown(KeyCode.J))
//        {
//            HideTip(showTime);
//        }
//        if (Input.GetKeyDown(KeyCode.H))
//        {
//            ShowTip(showTime);
//        }
        if (counted<count*100)
        {
            text1.text = "<size=45>" + counted++ + "</size> %";
        }
        if (isStart)
        {
            image.fillAmount += Time.deltaTime / transformTime;
            
            if (image.fillAmount>=count)
            {
                isStart = false;
                if (count>fixedcount&&isGun)
                {
                    isStartEnd = true;
                }
                else if (count < fixedcount)
                {
                    text1.text = "<size=45>" + count * 100 + "</size> %";
                    image.fillAmount = count;
                }
            }
        }
        if (isStartEnd)
        {
            image.fillAmount -= Time.deltaTime / transformTime;
            if (image.fillAmount<=count- fixedcount)
            {
                fixedcount /= 2;
                if (fixedcount<=0.01f)
                {
                    isStartEnd = false;
                    image.fillAmount = count;
                    text1.text = "<size=45>" + count*100 + "</size> %";
                    return;
                }
                isStartEnd = false;
                isStart = true;
            }
        }
	}

	public void SetData( Dictionary<int, DataIcon> allIcon, float count,string str)
    {
		dataicons = allIcon;
        this.count = count;
        text2.text = str;
        isStart = true;
    }
    public void Reset()
    {
        image.fillAmount = 0;
        text1.text= "<size=45>0</size> %";
        this.count = 0;
        counted = 0;
        fixedcount = 0.1f;
        isStart = false;
    }

    public void HideTip(float time)
    {
        Tip.DOFade(0, time);
        TipText.DOFade(0, time);
    }

    public void ShowTip(float time)
    {
        Tip.DOFade(1, time);
        TipText.DOFade(1, time);
    }
    public void SetTipText(string str)
    {
        TipText.text = str;
        
    }
}
