using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAdapter : MonoBehaviour
{

    private CanvasScaler canvasScaler;

    private void Awake()
    {
        canvasScaler = gameObject.GetComponent<CanvasScaler>();
    }

    private void Start()
    {
        //宽度大于16:9
        if ((float) Screen.width/Screen.height >= 16f/9)
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }
        //宽度小于16:9
        else
        {
            canvasScaler.matchWidthOrHeight = 0f;
        }
    }
}
