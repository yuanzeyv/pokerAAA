using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersonalInfoTopPanel : BasePlane {

	public  RawImage rawImage;
    private Button iconBtn;
    private Text nameText;
    private Button nameBtn;
    private Text idText;
    private Button sexBtn;
    private Text sexText;
    private Button signatureBtn;
    private Text signatureText;
    private Button backBtn;

    private void Awake()
    {
		rawImage = transform.Find("Icon/Icon/mask").GetComponent<RawImage>();
        iconBtn = transform.Find("Icon/Button").GetComponent<Button>();
        nameText = transform.Find("Name/value").GetComponent<Text>();
        nameBtn = transform.Find("Name/Button").GetComponent<Button>();
        idText = transform.Find("ID/value").GetComponent<Text>();
        sexBtn = transform.Find("Sex/Button").GetComponent<Button>();
        sexText = transform.Find("Sex/value").GetComponent<Text>();
        signatureBtn = transform.Find("Signature/Button").GetComponent<Button>();
        signatureText = transform.Find("Signature/value").GetComponent<Text>();
        backBtn = transform.Find("Top/Back").GetComponent<Button>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        iconBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddSidePlane(HallManager.GetSingleton().selectPhotoPopup, HallManager.GetSingleton().containerTop, SidePlaneDirection.BOTTOM, 350);
        });
        nameBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().nameTopPanel);
        });
        sexBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddSidePlane(HallManager.GetSingleton().selectSexPopup,HallManager.GetSingleton().containerTop,SidePlaneDirection.BOTTOM,350);
        });
        signatureBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.AddTopPlane(HallManager.GetSingleton().signatureTopPanel);
        });
    }

    public void SetData(string url,string name,string id,string sex,string signature)
    {
        GameTools.GetSingleton().GetTextureNet(url, SetIcon);
        nameText.text = name;
        idText.text = id;
        sexText.text = sex == "1" ? "男" : "女";
        signatureText.text = signature.Length>=16?signature.Substring(0,16)+"......": signature;
    }

	public void SetIcon(Texture sprite)
    {
		rawImage.texture = sprite;
    }

    public void SetSex(string sex)
    {
        sexText.text = sex == "1" ? "男" : "女";
    }
    public void SetSignature(string signature)
    {
        signatureText.text = signature.Length >= 16 ? signature.Substring(0, 16) + "......" : signature;
    }
    public void SetName(string name)
    {
        nameText.text = name;
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
        SetData(StaticData.url,StaticData.username,StaticData.ID,StaticData.sex,StaticData.signature);
    }

    public override void OnRemoveComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GetPlayerInfo, new object[] { });
    }

    public override void OnRemoveStart()
    {

    }
}
