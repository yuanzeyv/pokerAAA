using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SettingTopPanel : BasePlane {

    private Button backBtn;
    public Toggle Audio;
    public Toggle Shake;
    public Toggle Inform;
    private Button CheckVersion;
    private Text checkText;
    private Button AboutOur;
    private Button quit;
    private Button agreement;
    private Button fixpas;


    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        Audio = transform.Find("Audio/Toggle").GetComponent<Toggle>();
        Shake = transform.Find("Shake/Toggle").GetComponent<Toggle>();
        Inform = transform.Find("Inform/Toggle").GetComponent<Toggle>();
        CheckVersion = transform.Find("CheckVersion").GetComponent<Button>();
        checkText = CheckVersion.transform.Find("value").GetComponent<Text>();
        AboutOur = transform.Find("AboutOur").GetComponent<Button>();
        quit = transform.Find("QuitLogin").GetComponent<Button>();
        agreement = transform.Find("Agreement").GetComponent<Button>();
        fixpas = transform.Find("fixpas").GetComponent<Button>();

        checkText.text = "v "+StaticData.version;
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        agreement.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().agreementTopPanel);
        });
        quit.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceLogin.quitLogin, new object[] { });
        });
        CheckVersion.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            int type =
#if UNITY_ANDROID
                0;
#elif UNITY_IOS
                1;
#elif UNITY_STANDALONE
                2;
#endif
            NetMngr.GetSingleton().Send(InterfaceLogin.CheckVersion, new object[] { StaticData.version, type });
            //Tip.Instance.ShowTip(0.5f);
        });
        AboutOur.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().aboutOurTopPanel);
        });
        fixpas.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().fixPadPopup.ShowView();
        });
        Audio.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StaticData.isMusic = isOn;
            PlayerPrefs.SetInt("isMusic", isOn ? 1 : 0);
        });
        Shake.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StaticData.isVibrate = isOn;
            PlayerPrefs.SetInt("isVibrate", isOn ? 1 : 0);
            //Handheld.Vibrate();
        });
        Inform.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StaticData.isinform = isOn;
            PlayerPrefs.SetInt("isInform", isOn?1:0);
        });
        //设置默认开关
        if (PlayerPrefs.HasKey("isMusic"))
        {
            StaticData.isMusic = PlayerPrefs.GetInt("isMusic") == 1 ? true : false;
            Audio.isOn = PlayerPrefs.GetInt("isMusic") == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey("isVibrate"))
        {
            StaticData.isVibrate = PlayerPrefs.GetInt("isVibrate") == 1 ? true : false;
            Shake.isOn = PlayerPrefs.GetInt("isVibrate") == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey("isInform"))
        {
            StaticData.isinform = PlayerPrefs.GetInt("isInform") == 1 ? true : false;
            Inform.isOn = PlayerPrefs.GetInt("isInform") == 1 ? true : false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void CheckVersionFinished(Hashtable data)
    {
        if (data["success"].ToString()=="1")
        {
            Tip.Instance.showMsg("不是最新版本!");
            Invoke("Quit", 1);
        }
        else if (data["success"].ToString() == "2") 
        {
            Tip.Instance.showMsg("当前版本是最新版本!");
        }
        else
        {
            Tip.Instance.showMsg("检查版本失败!");
        }
    }
    void Start ()
    {
	
	}
	
	void Update ()
    {
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
