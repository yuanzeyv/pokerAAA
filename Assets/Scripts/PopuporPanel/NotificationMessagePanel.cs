using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public enum MoveStatus{
    Wait,//等待状态
    Ready,//准备移动状态
    Move,//移动状态
    Over,//移动结束状态
}

public class NotificationMessagePanel : BasePlane
{
    [SerializeField]
    public Text m_TextNode;
    [SerializeField]
    public GameObject m_BackGround;
    [SerializeField]
    public float m_MoveSpeed = 500;//每秒中移动五百个像素

    public float m_ForceWaitTime = 0;//需要等待多久
    public float m_NowWaitTime = 0;//当前已经等待了多久

    public MoveStatus IsMoveStatus = MoveStatus.Wait;//当前是否是移动状态

    public Queue<string> m_StringQueue;//内部存在一个 文本池子，用于缓存收到的所有消息，然后慢慢显示
    void Awake() {
        this.m_StringQueue = new Queue<string>();//创建当前的对象
    }
    //插入一条通知
    public void EnqueueNotification(string notifycationText){
        this.m_StringQueue.Enqueue(notifycationText);
    }

	// Use this for initialization
	void Start () {
	}
	
	void Update () {//看样子只可以在update里面写了 
        float deltaTime = Time.deltaTime;//上一帧的延时
        if (this.IsMoveStatus == MoveStatus.Wait){
            this.m_NowWaitTime += deltaTime;//当前时间累加
            if (this.m_NowWaitTime < this.m_ForceWaitTime)//小于强制等待时间不予显示
                return;
            if (this.m_StringQueue.Count == 0)//判断当前是否拥有消息    
                return;
            this.IsMoveStatus = MoveStatus.Ready;//当前拥有了数据了
        }
        if(this.IsMoveStatus == MoveStatus.Ready)
        {
            RectTransform textRectTransform = this.m_TextNode.GetComponent<RectTransform>();
            string noticeText = this.m_StringQueue.Dequeue();//退出一个文本信息
            this.m_TextNode.gameObject.SetActive(true);//设置当前节点状态为激活态
            this.m_BackGround.SetActive(true);
            this.m_TextNode.text = noticeText;//设置当前的文本信息
            textRectTransform.anchoredPosition =  new Vector3(this.GetComponent<RectTransform>().rect.width + 80,0,0);//设置当前的文本信息
            this.IsMoveStatus = MoveStatus.Move;//当前状态变更为移动状态
        }   
        if(this.IsMoveStatus == MoveStatus.Move)
        {
            RectTransform textRectTransform = this.m_TextNode.GetComponent<RectTransform>();
            float xPos = textRectTransform.anchoredPosition.x;
            float offset = deltaTime * this.m_MoveSpeed;
            float newPosX = xPos - offset;
            float textWidth = this.m_TextNode.GetComponent<RectTransform>().rect.width; 
            textRectTransform.anchoredPosition = new Vector3(xPos - offset, 0,0);//更新节点的坐标 
            if ((xPos - offset + textWidth) <  -80){//全部隐藏在了幕后
                this.IsMoveStatus = MoveStatus.Over;
            } 
        }
        if(this.IsMoveStatus == MoveStatus.Over)
        {
            this.m_BackGround.SetActive(false);//设置当前节点为隐藏状态
            this.m_TextNode.gameObject.SetActive(false);//当前节点为隐藏状态
            this.m_NowWaitTime = 0;
            this.m_ForceWaitTime = 2;//强制要求等待3秒钟
            this.IsMoveStatus = MoveStatus.Wait;//设置为等待时间
        }
	}


    public override void OnAddComplete()
    {
    }

    public override void OnAddStart()
    { 
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {
    }
}
