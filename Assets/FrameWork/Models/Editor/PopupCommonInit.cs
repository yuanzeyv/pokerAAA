using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupCommonInit : Editor
{

    [MenuItem("FrameWork/Init Prefab/UI/PopupCommon")]
    public static void InitPopupCommon()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("FrameWorkPrefab/UI/PopupCommon"));
        go.name = "PopupCommon";
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            go.transform.parent = canvas.gameObject.transform;
            go.transform.localScale = Vector3.one;
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            GameObject canvasGo = new GameObject();
            canvasGo.name = "Canvas";
            canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();

            if (GameObject.FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystemGo = new GameObject();
                eventSystemGo.name = "EventSystem";
                eventSystemGo.AddComponent<EventSystem>();
                eventSystemGo.AddComponent<StandaloneInputModule>();
            }

            go.transform.parent = canvasGo.transform;
            go.transform.localScale = Vector3.one;
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}
