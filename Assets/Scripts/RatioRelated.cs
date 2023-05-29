using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RatioRelated : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float ratio = (float)Screen.width / (float)Screen.height;
        // 4:3的处理
        if (ratio < 1.7f)
        {
            //float nowHeight = 1920.0f / Screen.width * Screen.height;
            //float height = (nowHeight - 1080.0f) / 2.0f;
            //this.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, -height);
            //this.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, height);
        }
        // 18:9以及18.5:9的处理
        else if (ratio > 1.8f)
        {
            //float nowHeight = 1920.0f/Screen.width*Screen.height;
            //float width = 16.0f*nowHeight/9.0f;
            //width = (1920.0f - width)/2.0f;
            //this.GetComponent<RectTransform>().offsetMax = new Vector2(-width, 0.0f);
            //this.GetComponent<RectTransform>().offsetMin = new Vector2(width, 0.0f);

            CanvasScaler canvasScaler = GameObject.Find("BaseCanvas").GetComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = 1;
        }

	}
}
