using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PlaneManager : MonoBehaviour
{

    private RectTransform bottomPlanesParent;
    private RectTransform topPlanesParent;
    private BasePlane bottomPlane;
    private Stack<BasePlane> planeStack;
    private Transform sidePlaneParent;
    private BasePlane sidePlane;
    private SidePlaneDirection sidePlaneDirection;
    private Button sidePlaneBg;

    public float topPlaneMoveTime = 0.0f;
    public float sidePlaneMoveTime = 0.0f;
    public float movePosition = 1;

    /// <summary>
    /// 顶部面板数量
    /// </summary>
    public int TopPlaneCount
    {
        get
        {
            if (planeStack == null)
                return 0;

            return planeStack.Count;
        }
    }

    private void Awake()
    {
        bottomPlane = null;
        planeStack = new Stack<BasePlane>();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bottomPlanesParent">底部面板容器</param>
    /// <param name="topPlanesParent">顶部面板容器</param>
    public void Init(RectTransform bottomPlanesParent, RectTransform topPlanesParent)
    {
        this.bottomPlanesParent = bottomPlanesParent;
        this.topPlanesParent = topPlanesParent;
    }

    /// <summary>
    /// 显示底部面板
    /// </summary>
    /// <param name="plane">面板</param>
    public void ShowBottomPlane(BasePlane plane)
    {
        if(plane == null)
            return;

        if (bottomPlane != null)
        {
            bottomPlane.OnRemoveStart();
            bottomPlane.gameObject.SetActive(false);
            bottomPlane.OnRemoveComplete();
        }
        plane.gameObject.SetActive(true);
        plane.OnAddStart();

        RectTransform planeRt = plane.gameObject.GetComponent<RectTransform>();
        planeRt.SetParent(bottomPlanesParent);
        planeRt.localScale = Vector3.one;
        planeRt.SetAsLastSibling();
        planeRt.anchorMin = new Vector2(0f, 0f);
        planeRt.anchorMax = new Vector2(1f, 1f); 
        planeRt.offsetMin = new Vector2(0f, 0f);
        planeRt.offsetMax = new Vector2(0f, 0f);

        bottomPlane = plane;

        plane.OnAddComplete();
    }

    /// <summary>
    /// 增加顶部面板
    /// </summary>
    /// <param name="plane">面板</param>
    public void AddTopPlane(BasePlane plane)
    {
        if(plane == null)
            return;
        if(planeStack.Contains(plane))
            return;

        BasePlane topPlane = planeStack.Count == 0 ? null : planeStack.Peek();
        RectTransform topPlaneRt = topPlane == null ? null : topPlane.gameObject.GetComponent<RectTransform>();
        RectTransform newPlaneRt = plane.gameObject.GetComponent<RectTransform>();

        float planeWidth, planeHeight;
        if (topPlane != null)
        {
            topPlaneRt.anchorMin = new Vector2(0f, 0f);
            topPlaneRt.anchorMax = new Vector2(1f, 1f);
            topPlaneRt.offsetMin = new Vector2(0f, 0f);
            topPlaneRt.offsetMax = new Vector2(0f, 0f);
            planeWidth = topPlaneRt.rect.width;
            planeHeight = topPlaneRt.rect.height;
            topPlaneRt.anchorMin = new Vector2(0.5f, 0.5f);
            topPlaneRt.anchorMax = new Vector2(0.5f, 0.5f);
            topPlaneRt.sizeDelta = new Vector2(planeWidth, planeHeight);
        }

        newPlaneRt.SetParent(topPlanesParent);
        newPlaneRt.SetAsLastSibling();
        newPlaneRt.localScale = Vector3.one;
        newPlaneRt.anchorMin = new Vector2(0f, 0f);
        newPlaneRt.anchorMax = new Vector2(1f, 1f);
        newPlaneRt.offsetMin = new Vector2(0f, 0f);
        newPlaneRt.offsetMax = new Vector2(0f, 0f);
        planeWidth = newPlaneRt.rect.width;
        planeHeight = newPlaneRt.rect.height;
        newPlaneRt.anchorMin = new Vector2(0.5f, 0.5f);
        newPlaneRt.anchorMax = new Vector2(0.5f, 0.5f);
        newPlaneRt.sizeDelta = new Vector2(planeWidth, planeHeight);
        newPlaneRt.anchoredPosition = new Vector2(planeWidth, 0);
        if (topPlane==null)
        {
            bottomPlanesParent.DOAnchorPosX(planeWidth / movePosition * -1f, topPlaneMoveTime).OnComplete(() =>
            {
                //bottomPlanesParent.gameObject.SetActive(false);
            });
        }
        if (topPlane != null)
        {
            topPlane.OnRemoveStart();
            topPlaneRt.DOKill();
            topPlaneRt.DOAnchorPosX(planeWidth / movePosition * -1f, topPlaneMoveTime).OnComplete(() =>
            {
                GameTools.GetSingleton().stopGameToolsAllCoroutines();
                topPlane.gameObject.SetActive(false);
               
                topPlane.OnRemoveComplete();
            });
        }
        plane.gameObject.SetActive(true);
        plane.OnAddStart();
        newPlaneRt.DOKill();
        newPlaneRt.DOAnchorPosX(0f, topPlaneMoveTime).OnComplete(() =>
        {
            newPlaneRt.anchorMin = new Vector2(0f, 0f);
            newPlaneRt.anchorMax = new Vector2(1f, 1f);
            newPlaneRt.offsetMin = new Vector2(0f, 0f);
            newPlaneRt.offsetMax = new Vector2(0f, 0f);
            plane.OnAddComplete();
        });
        planeStack.Push(plane);
    }

    /// <summary>
    /// 移除顶部面板
    /// </summary>
    public void RemoveTopPlane()
    {
        if(planeStack.Count == 0)
        {
            if (SceneManager.GetActiveScene().name == "main")
            {
                if (!ClubManager.GetSingleton().clubPanel.isActiveAndEnabled)
                {
                    if(SceneManager.GetActiveScene().name!= "Game")
                        PopupCommon.GetSingleton().ShowView("确定退出游戏吗?",null,true,()=> {
                            Application.Quit();
                        });
                    return;
                }
                else
                {
                    ClubManager.GetSingleton().clubPanel.gameObject.SetActive(false);
                    return;
                }
            }
            else
            {
                if(SceneManager.GetActiveScene().name!= "Game")
                    PopupCommon.GetSingleton().ShowView("确定退出游戏吗?",null,true,()=> {
                        Application.Quit();
                    });
                return;
            }
        }
          

        BasePlane topPlane = planeStack.Pop();
        BasePlane topPrePlane = planeStack.Count == 0 ? null : planeStack.Peek();
        RectTransform topPlaneRt = topPlane.gameObject.GetComponent<RectTransform>();
        RectTransform topPrePlaneRt = topPrePlane == null ? null : topPrePlane.gameObject.GetComponent<RectTransform>();

        float planeWidth, planeHeight;
        if (topPrePlane==null)
        {
            bottomPlanesParent.DOAnchorPosX(0f, topPlaneMoveTime);
        }
        if (topPrePlane != null)
        {
            topPrePlane.gameObject.SetActive(true);
            topPrePlaneRt.anchorMin = new Vector2(0f, 0f);
            topPrePlaneRt.anchorMax = new Vector2(1f, 1f);
            topPrePlaneRt.offsetMin = new Vector2(0f, 0f);
            topPrePlaneRt.offsetMax = new Vector2(0f, 0f);
            planeWidth = topPrePlaneRt.rect.width;
            planeHeight = topPrePlaneRt.rect.height;
            topPrePlaneRt.anchorMin = new Vector2(0.5f, 0.5f);
            topPrePlaneRt.anchorMax = new Vector2(0.5f, 0.5f);
            topPrePlaneRt.sizeDelta = new Vector2(planeWidth, planeHeight);
            topPrePlaneRt.anchoredPosition = new Vector2(planeWidth / movePosition * -1f, 0);
        }

        topPlane.gameObject.SetActive(true);
        topPlaneRt.anchorMin = new Vector2(0f, 0f);
        topPlaneRt.anchorMax = new Vector2(1f, 1f);
        topPlaneRt.offsetMin = new Vector2(0f, 0f);
        topPlaneRt.offsetMax = new Vector2(0f, 0f);
        planeWidth = topPlaneRt.rect.width;
        planeHeight = topPlaneRt.rect.height;
        topPlaneRt.anchorMin = new Vector2(0.5f, 0.5f);
        topPlaneRt.anchorMax = new Vector2(0.5f, 0.5f);
        topPlaneRt.sizeDelta = new Vector2(planeWidth, planeHeight);
        topPlaneRt.anchoredPosition = new Vector2(0f, 0f);

        if (topPrePlane != null)
        {
            topPrePlaneRt.DOKill();
            topPrePlaneRt.DOAnchorPosX(0f, topPlaneMoveTime).OnComplete(() =>
            {
                topPrePlaneRt.anchorMin = new Vector2(0f, 0f);
                topPrePlaneRt.anchorMax = new Vector2(1f, 1f);
                topPrePlaneRt.offsetMin = new Vector2(0f, 0f);
                topPrePlaneRt.offsetMax = new Vector2(0f, 0f);
            });
        }
        topPlane.OnRemoveStart();
        topPlaneRt.DOKill();
        topPlaneRt.DOAnchorPosX(planeWidth, topPlaneMoveTime).OnComplete(() =>
        {
            topPlane.gameObject.SetActive(false);
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            topPlane.OnRemoveComplete();
        });
    }

    /// <summary>
    /// 增加侧边面板
    /// </summary>
    /// <param name="plane">面板</param>
    /// <param name="parent">容器</param>
    /// <param name="direction">方向</param>
    /// <param name="size">尺寸，左右两边为宽度，上下两边为高度</param>
    public void AddSidePlane(BasePlane plane, Transform parent, SidePlaneDirection direction, float size)
    {
        if(plane == null)
            return;
        if(sidePlane != null)
            return;
        sidePlane = plane;
        sidePlaneParent = parent;
        sidePlaneDirection = direction;
        plane.transform.SetParent(parent);
        plane.transform.localScale = Vector3.one;
        plane.transform.SetAsLastSibling();

        sidePlaneBg = new GameObject("Mask").AddComponent<Button>();
        sidePlaneBg.transform.parent = parent;
        sidePlaneBg.transform.localScale = Vector3.one;
        RectTransform sidePlaneBgRt = sidePlaneBg.gameObject.AddComponent<RectTransform>();
        sidePlaneBg.gameObject.AddComponent<CanvasRenderer>();
        Image image = sidePlaneBg.gameObject.AddComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 0f);
        sidePlaneBgRt.SetSiblingIndex(plane.transform.GetSiblingIndex());
        sidePlaneBgRt.anchorMin = new Vector2(0f, 0f);
        sidePlaneBgRt.anchorMax = new Vector2(1f, 1f);
        sidePlaneBgRt.offsetMin = new Vector2(0f, 0f);
        sidePlaneBgRt.offsetMax = new Vector2(0f, 0f);
        sidePlaneBg.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            RemoveSidePlane();
        });

        RectTransform planeRt = plane.gameObject.GetComponent<RectTransform>();
        float width, height;
        bool isPosX = false;
        float endPos = 0f;
        switch (direction)
        {
            case SidePlaneDirection.LEFT:
                planeRt.anchorMin = new Vector2(0f, 0f);
                planeRt.anchorMax = new Vector2(0f, 1f);
                planeRt.offsetMin = new Vector2(size * -1, 0f);
                planeRt.offsetMax = new Vector2(0f, 0f);
                width = planeRt.rect.width;
                height = planeRt.rect.height;
                isPosX = true;
                endPos = size;
                planeRt.anchorMin = new Vector2(0f, 0.5f);
                planeRt.anchorMax = new Vector2(0f, 0.5f);
                planeRt.sizeDelta = new Vector2(width, height);
                planeRt.pivot = new Vector2(1f, 0.5f);
                planeRt.anchoredPosition = new Vector2(0f, 0f);
                break;
            case SidePlaneDirection.RIGHT:
                planeRt.anchorMin = new Vector2(1f, 0f);
                planeRt.anchorMax = new Vector2(1f, 1f);
                planeRt.offsetMin = new Vector2(0f, 0f);
                planeRt.offsetMax = new Vector2(size, 0f);
                width = planeRt.rect.width;
                height = planeRt.rect.height;
                isPosX = true;
                endPos = size * -1;
                planeRt.anchorMin = new Vector2(1f, 0.5f);
                planeRt.anchorMax = new Vector2(1f, 0.5f);
                planeRt.sizeDelta = new Vector2(width, height);
                planeRt.pivot = new Vector2(0f, 0.5f);
                planeRt.anchoredPosition = new Vector2(0f, 0f);
                break;
            case SidePlaneDirection.TOP:
                planeRt.anchorMin = new Vector2(0f, 1f);
                planeRt.anchorMax = new Vector2(1f, 1f);
                planeRt.offsetMin = new Vector2(0f, 0);
                planeRt.offsetMax = new Vector2(0f, size);
                width = planeRt.rect.width;
                height = planeRt.rect.height;
                isPosX = false;
                endPos = size * -1;
                planeRt.anchorMin = new Vector2(0.5f, 1f);
                planeRt.anchorMax = new Vector2(0.5f, 1f);
                planeRt.sizeDelta = new Vector2(width, height);
                planeRt.pivot = new Vector2(0.5f, 0f);
                planeRt.anchoredPosition = new Vector2(0f, 0f);
                break;
            case SidePlaneDirection.BOTTOM:
                planeRt.anchorMin = new Vector2(0f, 0f);
                planeRt.anchorMax = new Vector2(1f, 0f);
                planeRt.offsetMin = new Vector2(0f, size * -1);
                planeRt.offsetMax = new Vector2(1f, 0f);
                width = planeRt.rect.width;
                height = planeRt.rect.height;
                isPosX = false;
                endPos = size;
                planeRt.anchorMin = new Vector2(0.5f, 0f);
                planeRt.anchorMax = new Vector2(0.5f, 0f);
                planeRt.sizeDelta = new Vector2(width, height);
                planeRt.pivot = new Vector2(0.5f, 1f);
                planeRt.anchoredPosition = new Vector2(0f, 0f);
                break;
        }
        plane.gameObject.SetActive(true);
        plane.OnAddStart();
        if (isPosX)
            planeRt.DOAnchorPosX(endPos, sidePlaneMoveTime).OnComplete(() =>
            {
                plane.OnAddComplete();
            });
        else
            planeRt.DOAnchorPosY(endPos, sidePlaneMoveTime).OnComplete(() =>
            {
                plane.OnAddComplete();
            });
    }

    /// <summary>
    /// 移除侧边面板
    /// </summary>
    public void RemoveSidePlane()
    {
        if(sidePlane == null)
            return;

        bool isPosX = false;
        switch (sidePlaneDirection)
        {
            case SidePlaneDirection.LEFT:
                isPosX = true;
                break;
            case SidePlaneDirection.RIGHT:
                isPosX = true;
                break;
            case SidePlaneDirection.TOP:
                isPosX = false;
                break;
            case SidePlaneDirection.BOTTOM:
                isPosX = false;
                break;
        }

        RectTransform planeRt = sidePlane.gameObject.GetComponent<RectTransform>();
        sidePlane.OnRemoveStart();
        if (isPosX)
            planeRt.DOAnchorPosX(0, sidePlaneMoveTime).OnComplete(() =>
            {
                sidePlane.OnRemoveComplete();
                sidePlane.gameObject.SetActive(false);
                sidePlane = null;
                Destroy(sidePlaneBg.gameObject);
                sidePlaneBg = null;
            });
        else
            planeRt.DOAnchorPosY(0, sidePlaneMoveTime).OnComplete(() =>
            {
                sidePlane.OnRemoveComplete();
                sidePlane.gameObject.SetActive(false);
                sidePlane = null;
                Destroy(sidePlaneBg.gameObject);
                sidePlaneBg = null;
            });
    }

    private void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RemoveTopPlane();
        }
#endif
    }

}

/// <summary>
/// 侧边面板方向
/// </summary>
public enum SidePlaneDirection
{
    LEFT,
    RIGHT,
    TOP,
    BOTTOM
}
