using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OddsPopup : BasePopup {

    private Text tipText;
    private Slider Odds;
    private Button cancel;
    private Button sure;

    private void Awake()
    {
        Init();
        tipText = transform.Find("tipText").GetComponent<Text>();
        Odds = transform.Find("Odds").GetComponent<Slider>();
        cancel = transform.Find("CancelBtn").GetComponent<Button>();
        sure = transform.Find("SureBtn").GetComponent<Button>();

        Odds.onValueChanged.AddListener((value) =>
        {
            if ((int)value==0)
            {
                tipText.text = "仅显示大于<color=#F3C37FFF> " + 1 + " </color>倍盲注";
            }
            else
            {
                tipText.text = "仅显示大于<color=#F3C37FFF> " + (int)value * 10 + " </color>倍盲注";
            }
        });
        cancel.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
        });
        sure.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
            if ((int)Odds.value==0)
            {
                HallManager.GetSingleton().myPaiPuTopPanel.odds = 1;
                HallManager.GetSingleton().myPaiPuTopPanel.bottomTip.text = "仅显示大于<color=#F3C37FFF> " + 1 + " </color>倍盲注";
            }
            else
            {
                HallManager.GetSingleton().myPaiPuTopPanel.odds = 10 * (int)Odds.value;
                HallManager.GetSingleton().myPaiPuTopPanel.bottomTip.text = "仅显示大于<color=#F3C37FFF> " + 10 * (int)Odds.value + " </color>倍盲注";
            }
            HallManager.GetSingleton().myPaiPuTopPanel.currPage = 1;
            NetMngr.GetSingleton().Send(InterfaceMain.GetMyPaiData, new object[] { HallManager.GetSingleton().myPaiPuTopPanel.currPage, HallManager.GetSingleton().myPaiPuTopPanel.showPage, 10 * (int)Odds.value });
        });
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
