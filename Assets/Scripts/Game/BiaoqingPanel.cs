using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BiaoqingPanel : BasePopup
{
    public Button[] mItems;
    void Awake()
    {
        Init();

        gameObject.SetActive(false);

    }

    public override void ShowView(Action onComplete = null)
    {
        base.ShowView(onComplete);
    
    }

    // Use this for initialization
    void Start()
    {
        mItems = transform.Find("Image/Content").GetComponentsInChildren<Button>();
        int x = 1;
        for (int j = 0; j < mItems.Length; j++)
        {
            int y = x;
            mItems[j].onClick.AddListener(() => {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                sendBq(y);
            });
            x++;
        }
    }


    public void sendBq(int index)
    {
        if (GameManager.GetSingleton().myNetID < 0)
        {
            PopupCommon.GetSingleton().ShowView("请入坐再发表情！", null, false);
            HideView();
            return;
        }

        NetMngr.GetSingleton().Send(InterfaceGame.sendChat, new object[] {1, index.ToString() });
        HideView();
    }

    // Update is called once per frame
    void Update()
    {

    }

  
}
