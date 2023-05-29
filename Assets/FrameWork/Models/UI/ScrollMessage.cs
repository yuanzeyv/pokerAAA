/**
 * 滚动消息类
 * 使用说明：
 *     1、通过调用AddMessage、RemoveFirstMessage、ClearAllMessage方法设置数据
 *     2、当存在数据时显示并滚动，无数据时自动隐藏
 */
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollMessage : MonoBehaviour
{

    public Transform mask;
    public Text textMessage;
    private RectTransform rtMask;
    private RectTransform rtMessage;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed
    {
        set { moveSpeed = value; }
        get { return moveSpeed; }
    }

    /// <summary>
    /// 消息数量
    /// </summary>
    public int MessageCount
    {
        get
        {
            if (listMessage == null)
                return 0;
            else
                return listMessage.Count;
        }
    }

    private float moveSpeed = 50f;

    private List<Hashtable> listMessage;

    private void Awake()
    {
        rtMask = mask.GetComponent<RectTransform>();
        rtMessage = textMessage.GetComponent<RectTransform>();
        listMessage = new List<Hashtable>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 增加消息
    /// </summary>
    /// <param name="msg">消息</param>
    /// <param name="repeatCount">重复次数，0为无限循环</param>
    public void AddMessage(string msg, int repeatCount = 1)
    {
        Hashtable data = new Hashtable();
        data["msg"] = msg;
        data["repeat"] = repeatCount == 0 ? -1 : repeatCount;
        listMessage.Add(data);

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            PlayNext();
        }
    }

    /// <summary>
    /// 移除第一条消息
    /// </summary>
    public void RemoveFirstMessage()
    {
        if(listMessage.Count == 0)
            return;
    
        listMessage.RemoveAt(0);
        PlayNext();
    }

    /// <summary>
    /// 清楚所有数据
    /// </summary>
    public void ClearAllMessage()
    {
        listMessage.Clear();
        PlayNext();
    }

    private void PlayNext()
    {
        if (listMessage.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        Hashtable data = listMessage[0];
        int repeatCount = Convert.ToInt32(data["repeat"]);
        if (repeatCount == 0)
        {
            listMessage.RemoveAt(0);
            PlayNext();
            return;
        }
        if (repeatCount != -1)
        {
            repeatCount--;
            data["repeat"] = repeatCount;
        }
        textMessage.text = data["msg"].ToString();
        rtMessage.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        rtMessage.anchoredPosition = new Vector2(rtMessage.anchoredPosition.x - Time.deltaTime * moveSpeed, rtMessage.anchoredPosition.y);
        if(rtMessage.anchoredPosition.x <= (rtMask.sizeDelta.x + rtMessage.sizeDelta.x) * -1)
            PlayNext();
    }

}
