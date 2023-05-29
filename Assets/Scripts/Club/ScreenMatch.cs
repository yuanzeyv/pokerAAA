using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenMatch : BasePopup
{

    public Button resetBtn;
    public Button sureBtn;
    //public Button screenBtn;

    public Toggle[] mangzhuToggle;
    public Toggle[] renshuToggle;
    public Toggle[] baoxianToggle;
    public Toggle[] qianzhuToggle;
    public Toggle[] zhuatouToggle;
    


    void Awake()
    {
        Init();
        resetBtn = transform.Find("ResetBtn").GetComponent<Button>();
		sureBtn = transform.Find("SureBtn").GetComponent<Button>();

        mangzhuToggle = transform.Find("mangzhu").GetComponentsInChildren<Toggle>();
        renshuToggle = transform.Find("renshu").GetComponentsInChildren<Toggle>();
        baoxianToggle = transform.Find("baoxian").GetComponentsInChildren<Toggle>();
        qianzhuToggle = transform.Find("qianzhu").GetComponentsInChildren<Toggle>();
        zhuatouToggle = transform.Find("zhuatou").GetComponentsInChildren<Toggle>();

        resetBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            mangzhuToggle[5].isOn = true;
            renshuToggle[4].isOn = true;
            baoxianToggle[2].isOn = true;
            qianzhuToggle[2].isOn = true;
            zhuatouToggle[2].isOn = true;

        });

        sureBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            SendScreen();
            HideView();
        });

        gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendScreen()
    {
        string mangzhu = "";
        string renshu = "";
        int baoxian = 0;
        int qianzhu = 0;
        int zhuatou = 0;

        for (int i = 0; i < mangzhuToggle.Length; i++)
        {
            if (mangzhuToggle[i].isOn)
            {
                //mangzhu = i + "";
                switch (i) {
                    case 0:
                        mangzhu = "1";
                        break;
                    case 1:
                        mangzhu =  "2";
                        break;
                    case 2:
                        mangzhu = "5";
                        break;
                    case 3:
                        mangzhu = "10";
                        break;
                    case 4:
                        mangzhu =  "100";
                        break;
                    case 5:
                        mangzhu = "0";
                        break;
                }
                break;
            }

        }

        for (int i = 0; i < renshuToggle.Length; i++)
        {
            if (renshuToggle[i].isOn)
            {
                renshu = i + "";
                break;
            }

        }

        for (int i = 0; i < baoxianToggle.Length; i++)
        {
            if (baoxianToggle[i].isOn)
            {
                baoxian = i ;
                break;
            }

        }

        for (int i = 0; i < qianzhuToggle.Length; i++)
        {
            if (qianzhuToggle[i].isOn)
            {
                qianzhu = i ;
                break;
            }

        }
        for (int i = 0; i < zhuatouToggle.Length; i++)
        {
            if (zhuatouToggle[i].isOn)
            {
                zhuatou = i ;
                break;
            }

        }
        HallManager.GetSingleton().hallBottomPanel.currPage = 1;
        NetMngr.GetSingleton().Send(InterfaceMain.ScreenMatch, new object[] { mangzhu, renshu, baoxian,qianzhu,zhuatou,1,20 });
    }
}

