using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Allin : MonoBehaviour {

    public int sumCount;
    public float speed;
    
    public Color color;
    public Color backColor;

    private Slider slider;
    private Text count;
    private Text title;
    private Text tip;

    private bool isStart = false;
    private int currentCount;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        count = transform.Find("Count").GetComponent<Text>();
        title = transform.Find("Title").GetComponent<Text>();
        tip = transform.Find("Tip").GetComponent<Text>();
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SetData(12, 5, "Allin", "手数");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetData(17, 3, "41%", "获胜");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetData(18, 10, "68%", "失利");
        }
        if (isStart)
        {
            slider.value += currentCount/speed*Time.deltaTime ;
            if (slider.value>=currentCount)
            {
                isStart = false;
            }
        }
    }

    public void SetData(int sumcount,int currcount,string tip,string title)
    {
        currentCount = currcount;
        slider.maxValue = sumcount;
        slider.value = 0;
        this.tip.text = tip;
        this.title.text = title;
        this.count.text = currcount.ToString();
        slider.transform.Find("Background").GetComponent<Image>().color = backColor;
        slider.transform.Find("Fill Area/Fill").GetComponent<Image>().color = color;
        isStart = true;
    }
}
