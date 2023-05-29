/**
 * 消息管理
 * 说明：
 *     提供在对象间传递消息的能力
 * 使用方式：
 *     1、在需要接受消息的对象中调用RegisterMessageListener方法注册消息侦听
 *     2、调用SendMsg发送消息广播，注册了侦听的对象可以收到消息
 *     3、不需要再接收消息时调用UnRegisterMessageListener移除侦听
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour
{
    private Dictionary<string, List<Action<System.Object>>> listenerDic; 

    private static MessageManager mng;

    public static MessageManager GetSingleton()
    {
        return mng;
    }

    private void Awake()
    {
        if (mng == null)
            mng = this;

        listenerDic = new Dictionary<string, List<Action<object>>>();
    }

    /// <summary>
    /// 注册消息侦听
    /// </summary>
    /// <param name="message">消息内容</param>
    /// <param name="callback">消息回调</param>
    public void RegisterMessageListener(string message, Action<System.Object> callback)
    {
        if (!listenerDic.ContainsKey(message))
            listenerDic.Add(message, new List<Action<System.Object>>());

        if (callback != null)
            listenerDic[message].Add(callback);
    }

    /// <summary>
    /// 移除消息侦听
    /// </summary>
    /// <param name="message">消息内容</param>
    /// <param name="callback">消息回调</param>
    public void UnRegisterMessageListener(string message, Action<System.Object> callback)
    {
        if(!listenerDic.ContainsKey(message))
            return;

        int index = -1;
        for (int i = 0; i < listenerDic[message].Count; i++)
        {
            if (listenerDic[message][i] == callback)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
            listenerDic[message].RemoveAt(index);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="message">消息内容</param>
    /// <param name="param">消息参数</param>
    public void SendMsg(string message, System.Object param = null)
    {
        if(!listenerDic.ContainsKey(message))
            return;

        for (int i = 0; i < listenerDic[message].Count; i++)
        {
            listenerDic[message][i](param);
        }
    }

}
