using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SliderBar : MonoBehaviour {

    public int maxCount = 2;
    public RectTransform handle;
    public Transform parent;
    public Transform hadlele;
    public Color color;
    public Color backGround;
    private RectTransform rectSlider;
    private Slider slider;
	private Image[] lists = new Image[20];
    public int isInit = 0;

    private void Awake()
    {
        rectSlider = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SliderChaned);
    }

	public void resetPoint(int count)
	{
		maxCount = count;
		set ();
	}

	void set()
	{
//		Debug.Log ("set:" + maxCount.ToString ());

		slider.maxValue = maxCount;

		int childCount = parent.childCount;

//		Debug.Log ("set:" + childCount.ToString ());

		if (childCount > 2) {
			for (int i = 1; i < childCount-1; i++)
			{
				//Debug.Log ("Destroy:" + i.ToString());
				Destroy(parent.GetChild(i).gameObject);
			}
		}

		Debug.Log ("RectTransform:" + childCount.ToString ());

		RectTransform form = transform.Find ("Background").GetComponent<RectTransform> ();

		Debug.Log ("form:" + childCount.ToString ());

		float lenth = form.rect.width;

		Debug.Log ("lenth:" + lenth.ToString ());

		float x = (lenth-20) / slider.maxValue;
		float xx = 0;
		handle.gameObject.SetActive(true);
		for (int i = 0; i < slider.maxValue + 1; i++)
		{
			Debug.Log ("RectTransform:" + i.ToString ());

			RectTransform go = Instantiate(handle);
			go.transform.SetParent(parent);
			go.anchoredPosition3D = new Vector3(xx, 0, 0);
			go.localScale = Vector3.one;
			go.GetComponent<Image>().color = backGround;
			xx += x;

			lists [i] = go.GetComponent<Image> ();
		}
		handle.gameObject.SetActive(false);
		hadlele.SetSiblingIndex(parent.childCount - 1);
//		lists = parent.GetComponentsInChildren<Image>();
	}

    void Start()
    {
		set ();   
        if (isInit==1)
        {
            for (int i = 0; i < lists.Length - 1; i++)
            {
                if (i <= 4)
                {
                    lists[i].color = this.color;
                }
                else
                {
                    lists[i].color = backGround;
                }
            }
        }
        else if(isInit==2)
        {
            for (int i = 0; i < lists.Length - 1; i++)
            {
                if (i <= 1)
                {
                    lists[i].color = this.color;
                }
                else
                {
                    lists[i].color = backGround;
                }
            }
        }
    }
    private void SliderChaned(float value)
    {
		for (int i = 0; i < maxCount; i++)
        {
            if (i <= value)
            {
                lists[i].color = this.color;
            }
            else
            {
                lists[i].color = backGround;
            }
        }
    }

    void Update()
    {
	
	}

    public void SetValue(int value){
        slider.value = value;
    }

}
