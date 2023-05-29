using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Tip : MonoBehaviour {

    private Image tip;
    private Text tipText;

    private float timer = 0;
    private bool isStart = false;


    private static Tip _instance;

    public static Tip Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = new Tip();
                return _instance;
            }
        }
    }
    private void Awake()
    {
        _instance = this;
        tip = GetComponent<Image>();
        tipText = transform.Find("Text").GetComponent<Text>();
        HideTip(0);
    }
	void Start () {
	
	}
	
	void Update ()
    {
        if (isStart)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                HideTip(0.1f);
                timer = 0;
                isStart = false;
            }
        }
    }

    public void HideTip(float time)
    {
        
        tip.DOFade(0, time);
        tipText.DOFade(0, time).OnComplete(()=> { this.gameObject.SetActive(false); });
    }

    public void ShowTip(float time)
    {
        this.gameObject.SetActive(true);
        tip.DOFade(1f, time);
        tipText.DOFade(1f, time).OnComplete(() => isStart = true);
        
      
    }
    public void SetTipText(string str)
    {
        tipText.text = str;
    }
    public void showMsg(string str)
    {
        SetTipText(str);
        ShowTip(0.3f);
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        HideTip(0.1f);
    }
}
