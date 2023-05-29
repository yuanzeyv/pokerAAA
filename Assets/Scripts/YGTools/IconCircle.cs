using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class IconCircle : MonoBehaviour {

    public float time;
    public bool isShowText=true;
    private float timer = 0;
    private float timerText;
    private RawImage icon;
    private Transform Xing;
    private Image mask;
    private Image mask1;
    private Vector3 dot;
    private Vector3 xingSelf;
    private Text timeText;
    private bool isStart = false;

    private void Awake()
    {
        icon = GetComponent<RawImage>();
        Xing = transform.Find("wei");
        mask = transform.Find("mask").GetComponent<Image>();
        mask1 = transform.Find("mask1").GetComponent<Image>();
        xingSelf = Xing.localPosition;
        timeText = transform.Find("Text").GetComponent<Text>();

    }

	void Start ()
    {
        dot = transform.position;
        timeText.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
        mask1.gameObject.SetActive(false);
    }
	public void startDaojishi(int time_)
    {
        time = time_;
    }
	void FixedUpdate ()
    {
        if (isStart)
        {
            Xing.RotateAround(dot, new Vector3(0, 0, -1f), 360*Time.deltaTime/time);
            mask.fillAmount -= 1 / time*Time.deltaTime ;
            mask1.fillAmount -= 1 / time * Time.deltaTime;
            timer += Time.deltaTime;
            timeText.text = (timerText-=Time.deltaTime).ToString("f0");
            if (timer>=time)
            {
                isStart = false;
                timer = 0;
                timeText.gameObject.SetActive(false);
                mask1.gameObject.SetActive(false);
                Xing.GetComponent<ParticleSystem>().Stop();
            }
        }
	}
    public void Play()
    {
        isStart = false;
        mask.gameObject.SetActive(true);
        mask1.gameObject.SetActive(true);
        timer = 0;
        mask.fillAmount = 1;
        mask1.fillAmount = 1;
        Xing.localPosition = xingSelf;
		dot = transform.position;
        Xing.GetComponent<ParticleSystem>().Play();
        timerText = time;
        timeText.gameObject.SetActive(isShowText);
        isStart = true;
    }
    public void Stop()
    {
        isStart = false;
        timer = 0;
        if (timeText == null) return;
        timeText.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
        mask1.gameObject.SetActive(false);
        Xing.GetComponent<ParticleSystem>().Stop();
    }
}
