using UnityEngine;
using System.Collections;

/// <summary>
/// 面板基类，所有面板需继承自该类
/// </summary>
public abstract class BasePlane : MonoBehaviour
{

    /// <summary>
    /// 开始增加面板事件
    /// </summary>
    public abstract void OnAddStart();

    /// <summary>
    /// 完成增加面板事件
    /// </summary>
    public abstract void OnAddComplete();

    /// <summary>
    /// 开始移除面板事件
    /// </summary>
    public abstract void OnRemoveStart();

    /// <summary>
    /// 完成移除面板事件
    /// </summary>
    public abstract void OnRemoveComplete();

}
