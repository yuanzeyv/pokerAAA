using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoubleSliderBar : MonoBehaviour {

    public int maxCount=2;
    public RectTransform dot;
    public Transform parent;
    public float fillHight=1;
    public Color color;
    public Color backColor;
    private Transform hadlele;
    private RectTransform rectSlider;
    private Slider slider;
    private Slider minHandle;
    private Slider maxHandle;
    public Image[] lists;
    private RectTransform fill;
    private RectTransform length;
    //private LineRenderer line;

    private void Awake()
    {
        rectSlider = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
        minHandle = transform.Find("Min").GetComponent<Slider>();
        maxHandle = transform.Find("Max").GetComponent<Slider>();

        hadlele = transform.Find("Handle Slide Area/Handle");

        fill = transform.Find("Fill Area/Fill").GetComponent<RectTransform>();
        length = transform.Find("Background").GetComponent<RectTransform>();
        //line = transform.Find("Line").GetComponent<LineRenderer>();
        minHandle.onValueChanged.AddListener(SliderChaned);
        maxHandle.onValueChanged.AddListener(SliderChaned);
    }

    void Start()
    {
        slider.maxValue = maxCount;
        minHandle.maxValue = maxCount;
        maxHandle.maxValue = maxCount;
        float lenth = length.rect.width;
        float x = (lenth-20) / slider.maxValue;
        float xx = 0;
        for (int i = 0; i < slider.maxValue + 1; i++)
        {
            RectTransform go = Instantiate(dot);
            go.transform.SetParent(parent);
			go.anchoredPosition3D = new Vector3(xx, 0, 0);
            go.localScale = Vector3.one;
            go.GetComponent<Image>().color = backColor;
            xx += x;
        }
        dot.gameObject.SetActive(false);
        hadlele.SetSiblingIndex(parent.childCount - 1);
        lists = parent.GetComponentsInChildren<Image>();
    }
    private void SliderChaned(float value)
    {
        Vector3 min = minHandle.transform.Find("Handle Slide Area/Handle").position;
        Vector3 max = maxHandle.transform.Find("Handle Slide Area/Handle").position;

        Vector3 minpre = fill.parent.InverseTransformPoint(min);
        Vector3 maxpre = fill.parent.InverseTransformPoint(max);
        float distance = (maxpre - minpre).magnitude;
        
        //line.SetPosition(0, min);
        //line.SetPosition(1, max);
        //line.SetWidth(0.02f, 0.02f);
        //Debug.Log(min + "   " + max + "   " + distance + " , " + Vector3.Distance(min, max));
        //fill.transform.TransformPoint(min);
        //fill.transform.TransformPoint(max);
        fill.sizeDelta = new Vector2(distance, fillHight);

        if (min.x < max.x)
        {
            fill.localPosition = new Vector3(minpre.x + distance / 2, fill.localPosition.y, fill.localPosition.z);
        }
        else
        {
            fill.localPosition = new Vector3(maxpre.x + distance / 2, fill.localPosition.y, fill.localPosition.z);
        }
        
        if (minHandle.value<maxHandle.value)
        {
            for (int i = 0; i < lists.Length - 1; i++)
            {
                if (i<minHandle.value)
                {
                    lists[i].color = this.backColor;
                }
                else if (i>minHandle.value&&i<maxHandle.value)
                {
                    lists[i].color = this.color;
                }
                else
                {
                    lists[i].color = this.backColor;
                }
            }
        }
        else if(minHandle.value > maxHandle.value)
        {
            for (int i = 0; i < lists.Length - 1; i++)
            {
                if (i <= maxHandle.value)
                {
                    lists[i].color = this.backColor;
                }
                else if (i > maxHandle.value && i < minHandle.value)
                {
                    lists[i].color = this.color;
                }
                else
                {
                    lists[i].color = this.backColor;
                }
            }
        }

    }

    void Update()
    {

    }
}
