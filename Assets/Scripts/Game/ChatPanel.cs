using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ChatItem
{
    public string userId;
    public bool isMy;
    public string chat;

    public ChatItem(string _userId, bool _isMy, string _chat)
    {
        this.userId = _userId;
        this.isMy = _isMy;
        this.chat = _chat;
    }

}

public class ChatPanel : BasePlane
{
    public GameObject leftBubblePrefab;
    public GameObject rightBubblePrefab;

    public GameObject leftVociePrefab;
    public GameObject rightVociePrefab;

    private ScrollRect scrollRect;
    private Scrollbar scrollbar;

    private RectTransform content;

    [SerializeField]
    private float stepVertical; //上下两个气泡的垂直间隔
    [SerializeField]
    private float stepHorizontal; //左右两个气泡的水平间隔
    [SerializeField]
    private float maxTextWidth;//文本内容的最大宽度

    private float lastPos; //上一个气泡最下方的位置
    private float halfHeadLength;//头像高度的一半

    public Button sendBtn;
    public InputField sendField;

    public int diamond;

    public List<ChatItem> chatItems = new List<ChatItem>();
    void Awake() {

        lastPos = -60;
        Init();
        sendBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");            
            if (GameManager.GetSingleton().myNetID < 0)
            {
                PopupCommon.GetSingleton().ShowView("请入坐再聊天！", null, false);
                sendField.text = "";
                return;
            }

            if(StaticData.diamond < diamond){
                PopupCommon.GetSingleton().ShowView("钻石不够！", null, false);
                return;
            }

            if (sendField.text != "")
            {
                NetMngr.GetSingleton().Send(InterfaceGame.sendChat, new object[] { 0, sendField.text});
                sendField.text = "";
            }
        });
        gameObject.SetActive(false);
    }

    public void AddChatItem(string _userId, bool _isMy, string _chat)
    {
        ChatItem item = new ChatItem(_userId, _isMy, _chat);
        chatItems.Add(item);

        //if (this.isActiveAndEnabled)
        {
            AddBubble(_userId, _chat, _isMy);
        }
    }

    public void AddBubble(string _userId, string content, bool isMy)
    {
        GameObject newBubble = isMy ? Instantiate(rightBubblePrefab, this.content) : Instantiate(leftBubblePrefab, this.content);
        //halfHeadLength = newBubble.transform.Find("head/Head").GetComponent<RectTransform>().rect.height / 2;
        halfHeadLength = 0;
        //设置气泡内容
        Text text = newBubble.GetComponentInChildren<Text>();
        text.text = content;
        if (text.preferredWidth > maxTextWidth)
        {
            text.GetComponent<LayoutElement>().preferredWidth = maxTextWidth;
        }
        RawImage head = newBubble.gameObject.transform.Find("head/Head").GetComponent<RawImage>();

        for (int j = 0; j < GameManager.GetSingleton().MapLocalSeatPlayer.Count; j++)
        {
            PlayInfo info = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[j]).GetComponent<PlayInfo>();
            if (info != null && info.userID == _userId)
            {
                if (info.cricleImage.overrideSprite != null && info.cricleImage.overrideSprite.texture != null)
                {
                    head.texture = info.cricleImage.overrideSprite.texture;
                }
                break;
            }
        }
            //计算气泡的水平位置
        float hPos = 0;
        if (isMy)
        {
            hPos = this.content.rect.width;
        }
        //float hPos = isMy ? stepHorizontal / 2 : -stepHorizontal / 2;
        //计算气泡的垂直位置
        //float vPos = -stepVertical - halfHeadLength + lastPos;
        newBubble.transform.localPosition = new Vector2(hPos, lastPos);

        //更新lastPos
        Image bubbleImage = newBubble.transform.Find("bubble").GetComponent<Image>();
        float imageLength = GetContentSizeFitterPreferredSize(bubbleImage.GetComponent<RectTransform>(), bubbleImage.GetComponent<ContentSizeFitter>()).y;
        if (imageLength > stepVertical)
        {
            lastPos = lastPos - imageLength;
        }
        else
        {
            lastPos = lastPos - stepVertical;
        }
       
        //lastPos = vPos - imageLength;
        //更新content的长度
        if (-lastPos > this.content.rect.height-300)
        {
            this.content.sizeDelta = new Vector2(this.content.rect.width, -lastPos+300);
        }

        scrollRect.verticalNormalizedPosition = 0;//使滑动条滚轮在最下方
    }   

    public Vector2 GetContentSizeFitterPreferredSize(RectTransform rect, ContentSizeFitter contentSizeFitter)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        return new Vector2(HandleSelfFittingAlongAxis(0, rect, contentSizeFitter),
            HandleSelfFittingAlongAxis(1, rect, contentSizeFitter));
    }

    private float HandleSelfFittingAlongAxis(int axis, RectTransform rect, ContentSizeFitter contentSizeFitter)
    {
        ContentSizeFitter.FitMode fitting =
            (axis == 0 ? contentSizeFitter.horizontalFit : contentSizeFitter.verticalFit);
        if (fitting == ContentSizeFitter.FitMode.MinSize)
        {
            return LayoutUtility.GetMinSize(rect, axis);
        }
        else
        {
            return LayoutUtility.GetPreferredSize(rect, axis);
        }
    }

    public void showChat()
    {
        //for (int i = 0; i < chatItems.Count; i++)
        //{
        //    AddBubble(chatItems[i].chat, chatItems[i].isMy);
        //}
    }

    public void Init()
    {
        chatItems.Clear();
        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollbar = GetComponentInChildren<Scrollbar>();
        content = transform.Find("infoPanel/Viewport/Content").GetComponent<RectTransform>();

        //lastPos = 0;
       // halfHeadLength = leftBubblePrefab.transform.Find("head/Head").GetComponent<RectTransform>().rect.height / 2;
    }

    public void setData(Hashtable h){
        diamond = int.Parse(h["diamond"].ToString());
        transform.Find("Button_Send/num").GetComponent<Text>().text = diamond.ToString();
    }

    // Use this for initialization
    void Start () {
        showChat();
    }
	
	// Update is called once per frame
	void Update () {
	
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
   
    public void GetChatCallBack(Hashtable data) {


        
    }
  
   

}
