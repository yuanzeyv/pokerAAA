/**
 * 通用弹窗
 * 使用说明：
 *     1、默认点击按钮后自动隐藏弹窗
 *     2、如果需要修改隐藏弹窗显示效果，可以取消点击按钮后自动隐藏属性，并在点击回调内自行调用HideView方法
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class PopupCommon : BasePopup
{
    public Text textMessge;
    public Button btnSure;
    public Button btnCancel;

    private Action sureCallback;
    private Action cancelCallback;
    private bool autoHide = true;
    private RectTransform btnSureRt;
    private float btnSureStartPosX;

    private AnimType animType;
    private Ease ease;
    private float animSpeed;

    private static PopupCommon mng;
    public static PopupCommon GetSingleton()
    {
        return mng;
    }

    private void Awake()
    {
        base.Init();
        if (mng == null)
            mng = this;

        btnSureRt = btnSure.GetComponent<RectTransform>();
        btnSureStartPosX = btnSureRt.anchoredPosition.x;

        animType = base.DefaultAnimType;
        ease = base.DefaultEase;
        animSpeed = base.DefaultAnimSpeed;
        this.gameObject.SetActive(false);
    }

    public void OnBtnSureClick()
    {

        if (sureCallback != null)
            sureCallback();
        if (autoHide)
            HideView();
    }

    public void OnBtnCancelClick()
    {
        if (cancelCallback != null)
            cancelCallback();
        if (autoHide)
            HideView();
    }

    public PopupCommon SetAnimType(AnimType type)
    {
        animType = type;
        return this;
    }

    public PopupCommon SetEase(Ease ease)
    {
        this.ease = ease;
        return this;
    }

    public PopupCommon SetSpeed(float speed)
    {
        animSpeed = speed;
        return this;
    }

    /// <summary>
    /// 显示弹窗
    /// </summary>
    /// <param name="msg">弹窗消息</param>
    /// <param name="animCallback">动画完成回调</param>
    /// <param name="showCancel">是否显示取消</param>
    /// <param name="sureCallback">点击确认回调</param>
    /// <param name="cancelCallback">点击取消回调</param>
    /// <param name="autoHide">点击按钮后自动隐藏</param>
    public void ShowView(string msg, Action animCallback = null, bool showCancel = false, Action sureCallback = null, Action cancelCallback = null,bool canClickMask=false, bool autoHide = true)
    {
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>> " + msg + " " + sureCallback);
        if (showCancel)
        {
            btnSureRt.anchoredPosition = new Vector2(btnSureStartPosX, btnSureRt.anchoredPosition.y);
            btnCancel.gameObject.SetActive(true);
        }
        else
        {
            btnSureRt.anchoredPosition = new Vector2(0, btnSureRt.anchoredPosition.y);
            btnCancel.gameObject.SetActive(false);
        }
        textMessge.text = msg;
        this.sureCallback = sureCallback;
        this.cancelCallback = cancelCallback;
        this.autoHide = autoHide;
        base.SetAnimType(animType).SetEase(ease).SetSpeed(animSpeed).ShowView(animCallback);
        base.CanClickMask = false;
        animType = base.DefaultAnimType;
        ease = base.DefaultEase;
        animSpeed = base.DefaultAnimSpeed;
    }

    /// <summary>
    /// 隐藏弹窗
    /// </summary>
    /// <param name="onComplete">完成回调</param>
    public void HideView(Action onComplete = null)
    {
        base.SetAnimType(animType).SetEase(ease).SetSpeed(animSpeed).HideView(onComplete);

        animType = base.DefaultAnimType;
        ease = base.DefaultEase;
        animSpeed = base.DefaultAnimSpeed;
    }

    private void OnDestroy()
    {
        mng = null;
    }

    //public static implicit operator PopupCommon(FriendCode v)
    //{
    //    throw new NotImplementedException();
    //}
}