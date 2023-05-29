/**
 * 弹窗基类
 * 注意：子类需要调用base.Init()进行初始化
 * 包含属性：
 *      1、动画类型
 *      2、动画Ease
 *      3、动画速度
 *      4、默认动画类型
 *      5、默认动画Ease
 *      6、默认动画速度
 *      6、是否显示遮罩
 *      7、遮罩颜色
 *  包含方法：
 *      1、ShowView
 *      2、HideView
 *  使用说明：
 *      可在显示和隐藏前设置动画效果，如：SetAnimType(AnimType.Alpha).SetEase(Ease.InBounce).SetSpeed(0.5f).ShowView();
 */

using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public abstract class BasePopup : MonoBehaviour {

    /// <summary>
    /// 动画类型
    /// </summary>
    public enum AnimType
    {
        None,             //无动画
        Scale,            //缩放动画
        Alpha,            //淡入淡出动画
        ScaleAndAlpha     //缩放加淡入淡出动画
    }

    /// <summary>
    /// 默认动画类型
    /// </summary>
    public AnimType DefaultAnimType
    {
        set { defaultAnimType = value; }
        get { return defaultAnimType; }
    }

    /// <summary>
    /// 默认动画速度
    /// </summary>
    public float DefaultAnimSpeed
    {
        set { defaultAnimSpeed = value; }
        get { return defaultAnimSpeed; }
    }

    /// <summary>
    /// 默认动画Ease
    /// </summary>
    public Ease DefaultEase
    {
        set { defaultEase = value; }
        get { return defaultEase; }
    }

    /// <summary>
    /// 遮罩颜色
    /// </summary>
    public Color MaskColor
    {
        set { maskColor = value; }
        get { return maskColor; }
    }

    /// <summary>
    /// 是否显示遮罩
    /// </summary>
    public bool IsShowMask
    {
        set { isShowMask = value; }
        get { return isShowMask; }
    }

    /// <summary>
    /// 是否显示在顶层
    /// </summary>
    public bool IsShowTop
    {
        set { isShowTop = value; }
        get { return isShowTop; }
    }
    public bool CanClickMask { get; set; }
    public bool hideNeedSendMsg { get; set; }
    private AnimType defaultAnimType = AnimType.None;
    private float defaultAnimSpeed = 0.3f;
    private Ease defaultEase = Ease.Unset;
    private Color maskColor = new Color(0, 0, 0, 0.3f);
    private bool isShowMask = true;
    private bool isShowTop = true;
    private CanvasGroup group;
    private GameObject mask;
    private bool isAnim = false;

    private AnimType onceAnimType = AnimType.None;
    private Ease onceEase = Ease.Unset;
    private float onceAnimSpeed = 0.3f;

    /// <summary>
    /// 设置默认动画类型
    /// </summary>
    /// <param name="type">动画类型</param>
    public void SetDefaultAnimType(AnimType type)
    {
        defaultAnimType = type;
    }

    /// <summary>
    /// 设置默认动画速度
    /// </summary>
    /// <param name="speed">动画速度</param>
    public void SetDefaultAnimSpeed(float speed)
    {
        defaultAnimSpeed = speed;
    }

    /// <summary>
    /// 设置默认显示动画Ease
    /// </summary>
    /// <param name="ease">动画Ease</param>
    public void SetDefaultShowEase(Ease ease)
    {
        defaultEase = ease;
    }

    /// <summary>
    /// 设置遮罩颜色
    /// </summary>
    /// <param name="color">遮罩颜色</param>
    public void SetDefaultMaskColor(Color color)
    {
        maskColor = color;
    }

    /// <summary>
    /// 设置是否显示在顶层
    /// </summary>
    /// <param name="isShowTop">是否显示在顶层</param>
    public void SetDefaultIsShowTop(bool isShowTop)
    {
        this.isShowTop = isShowTop;
    }

    /// <summary>
    /// 设置本次动画类型
    /// </summary>
    /// <param name="type">动画类型</param>
    /// <returns></returns>
    public BasePopup SetAnimType(AnimType type)
    {
        onceAnimType = type;
        return this;
    }

    /// <summary>
    /// 设置本次动画Ease
    /// </summary>
    /// <param name="ease">动画Ease</param>
    /// <returns></returns>
    public BasePopup SetEase(Ease ease)
    {
        onceEase = ease;
        return this;
    }

    /// <summary>
    /// 设置本次速度
    /// </summary>
    /// <param name="speed">动画速度</param>
    /// <returns></returns>
    public BasePopup SetSpeed(float speed)
    {
        onceAnimSpeed = speed;
        return this;
    }

    /// <summary>
    /// 初始化方法，继承后必调
    /// </summary>
    public void Init()
    {
        group = gameObject.GetComponent<CanvasGroup>();
        if (group == null)
            group = gameObject.AddComponent<CanvasGroup>();
        CanClickMask = true;
        hideNeedSendMsg = false;
    }

    /// <summary>
    /// 显示弹窗
    /// </summary>
    /// <param name="onComplete">完成回调</param>
    public virtual void ShowView(Action onComplete = null)
    {
        TouchMove.Instance().isRun = false;
        if(isAnim)
            return;
        gameObject.SetActive(true);
        if (isShowTop)
            transform.SetSiblingIndex(transform.parent.childCount);
        isAnim = true;
        if (isShowMask)
            CreateMask();
        switch (onceAnimType)
        {
            case AnimType.None:
                gameObject.transform.localScale = Vector3.one;
                group.alpha = 1;
                group.interactable = true;
                if (onComplete != null)
                    onComplete();
                isAnim = false;
                break;
            case AnimType.Alpha:
                gameObject.transform.localScale = Vector3.one;
                group.alpha = 0;
                group.interactable = false;
                group.DOFade(1, onceAnimSpeed * 2).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase).OnComplete(() =>
                {
                    group.interactable = true;
                    if (onComplete != null)
                        onComplete();
                    isAnim = false;
                });
                break;
            case AnimType.Scale:
                gameObject.transform.localScale = Vector3.zero;
                group.alpha = 1;
                group.interactable = false;
                group.transform.DOScale(Vector3.one, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase).OnComplete(() =>
                {
                    group.interactable = true;
                    if (onComplete != null)
                        onComplete();
                    isAnim = false;
                });
                break;
            case AnimType.ScaleAndAlpha:
                gameObject.transform.localScale = Vector3.zero;
                group.alpha = 0;
                group.interactable = false;
                group.transform.DOScale(Vector3.one, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase);
                group.DOFade(1, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase).OnComplete(() =>
                {
                    group.interactable = true;
                    if (onComplete != null)
                        onComplete();
                    isAnim = false;
                });
                break;
        }
        onceAnimType = defaultAnimType;
        onceEase = defaultEase;
        onceAnimSpeed = defaultAnimSpeed;
    }

    /// <summary>
    /// 隐藏弹窗
    /// </summary>
    /// <param name="onComplete">完成回调</param>
    public virtual void HideView(Action onComplete = null)
    {
     
        TouchMove.Instance().isRun = true;
        if (isAnim)
            return;
        isAnim = true;
        if (isShowMask)
        {
            Destroy(mask);
            mask = null;
        }
        switch (onceAnimType)
        {
            case AnimType.None:
                if (onComplete != null)
                    onComplete();
                isAnim = false;
                gameObject.SetActive(false);
                break;
            case AnimType.Alpha:
                group.interactable = false;
                group.DOFade(0, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase).OnComplete(() =>
                {
                    group.interactable = true;
                    if (onComplete != null)
                        onComplete();
                    isAnim = false;
                    gameObject.SetActive(false);
                });
                break;
            case AnimType.Scale:
                group.interactable = false;
                group.transform.DOScale(Vector3.zero, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase).OnComplete(
                () =>
                {
                    group.interactable = true;
                    if (onComplete != null)
                        onComplete();
                    isAnim = false;
                    gameObject.SetActive(false);
                });
                break;
            case AnimType.ScaleAndAlpha:
                group.interactable = false;
                group.transform.DOScale(Vector3.zero, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase);
                group.DOFade(0, onceAnimSpeed).SetEase(onceEase == Ease.Unset ? defaultEase : onceEase).OnComplete(
                () =>
                {
                    group.interactable = true;
                    if (onComplete != null)
                        onComplete();
                    isAnim = false;
                    gameObject.SetActive(false);
                });
                break;
        }
        onceAnimType = defaultAnimType;
        onceEase = defaultEase;
        onceAnimSpeed = defaultAnimSpeed;
    }

    public virtual void CreateMask()
    {
        if(mask != null)
            return;
        RectTransform rect = transform.root.GetComponent<RectTransform>();
        mask = new GameObject();
        mask.name = transform.name + "_Mask";
        mask.transform.SetParent(transform.parent, false);
        mask.transform.SetSiblingIndex(transform.GetSiblingIndex());
        Image img = mask.GetComponent<Image>();
        if (img == null)
            img = mask.AddComponent<Image>();

        Button btn = mask.GetComponent<Button>();
        if (btn == null)
            btn = mask.AddComponent<Button>();
        btn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");            
            if (CanClickMask)
            {
                HideView();
                if (hideNeedSendMsg)
                {
                    NetMngr.GetSingleton().Send(InterfaceGame.cancelAddMoney,new object[] { });
                }
            }
             
            // 授权大厅可以关闭
            if (!CanClickMask && StaticData.selectScene == 1)
            {
                if (transform.Find("SureBtn/Text") != null)
                {
                    if (transform.Find("SureBtn/Text").GetComponent<Text>().text == "同意")
                    {
                        StaticFunction.Getsingleton().istimer = true;
                        HideView();
                    }
                
                }
            }
               
         
           
        });
        img.rectTransform.sizeDelta = rect.sizeDelta;
        img.color = maskColor;
    }

}
