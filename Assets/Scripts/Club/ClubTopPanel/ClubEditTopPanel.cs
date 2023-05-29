using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class ClubEditTopPanel : BasePlane
{
    public Button backBtn;
    public Button saveBtn;
   
    public GameObject title;
  

//    public GameObject clubJianJie;

	public RawImage clubHead;

    public Button camreaBtn;

//    public Button editBtn1;//clubname
//    public Button editBtn2;//kefu
//    public Button editBtn3;//jianjie

    public InputField clubNameInput;
    public InputField kefuWXInput;
    public InputField jianJieInput;

    public Transform clubTipsContent;
    public Button addBtn;

    public string clubTag;

    public string LoadImageUrl;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        saveBtn = transform.Find("saveBtn").GetComponent<Button>();
      //  saveBtn = transform.Find("ToggleGroup/saveBtn").GetComponent<Button>();
        title = transform.Find("ToggleGroup/Title").gameObject;

//        clubJianJie = transform.Find("jianJie").gameObject;

		clubHead = transform.Find("ClubHead/head").GetComponent<RawImage>();

        camreaBtn = transform.Find("camreaBtn").GetComponent<Button>();

//        editBtn1 = transform.Find("clubName/editBtn").GetComponent<Button>();
//        editBtn2 = transform.Find("kefuWX/editBtn").GetComponent<Button>();
//        editBtn3 = transform.Find("jianJie/editBtn").GetComponent<Button>();

        clubNameInput = transform.Find("clubName").GetComponent<InputField>();
        kefuWXInput = transform.Find("kefuWX").GetComponent<InputField>();
        jianJieInput = transform.Find("jianJie").GetComponent<InputField>();

        clubTipsContent = transform.Find("ClubTips/Scroll View/Viewport/Content");
        addBtn = clubTipsContent.Find("AddTip/addBtn").GetComponent<Button>();

//        editBtn1.onClick.AddListener(() => {
//            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().addClubTipTopPanel);
//            ClubManager.GetSingleton().addClubTipTopPanel.curTagType = AddClubTipTopPanel.TagType.clubNameEdit;
//        });
//
//        editBtn2.onClick.AddListener(() => {
//            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().addClubTipTopPanel);
//            ClubManager.GetSingleton().addClubTipTopPanel.curTagType = AddClubTipTopPanel.TagType.kefuEdit;
//        });
//
//        editBtn3.onClick.AddListener(() => {
//            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().addClubTipTopPanel);
//            ClubManager.GetSingleton().addClubTipTopPanel.curTagType = AddClubTipTopPanel.TagType.jianjieEdit;
//        });

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        addBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().addClubTipTopPanel);
            ClubManager.GetSingleton().addClubTipTopPanel.curTagType = AddClubTipTopPanel.TagType.editTag;
        });

        saveBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //GetTag();
            if (GetTag()) {
                NetMngr.GetSingleton().Send(InterfaceClub.FixClubInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId, LoadImageUrl, clubNameInput.text, kefuWXInput.text, clubTag, jianJieInput.text });

            }

            clubTag = "";
        });


        camreaBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            Debug.Log(StaticData.imgUrl);
            HallManager.GetSingleton().OpenAlbum(StaticData.imgUrl, ClubManager.GetSingleton().clubPanel.clubId, 3, "ClubEditTopPanel");
        });


        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public string GetChinese(string s)
    {
        byte[] buffer1 = Encoding.Default.GetBytes(s);
        byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, buffer1, 0, buffer1.Length);
        string strBuffer = Encoding.UTF8.GetString(buffer2, 0, buffer2.Length);
        return strBuffer;

    }


    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.SetClubTag, new object[] { ClubManager.GetSingleton().clubPanel.clubId, "1" });
        //获取下俱乐部的信息
        clubHead.texture = ClubManager.GetSingleton().clubInfoTopPanel.clubHead.texture;
        clubNameInput.text = ClubManager.GetSingleton().clubInfoTopPanel.clubName.text;
        kefuWXInput.text = ClubManager.GetSingleton().clubInfoTopPanel.kefu;
        jianJieInput.text = ClubManager.GetSingleton().clubInfoTopPanel.clubjianjie.text;

    }

    public override void OnAddStart()
    {
        DelTag();
    }

    public override void OnRemoveComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetClubInfo, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    public override void OnRemoveStart()
    {

    }

    public void SetClubTagCallBack(Hashtable data) {
        string[] tags = data["tag"].ToString().Split('|');

        GameObject go = clubTipsContent.GetChild(0).gameObject;
        go.SetActive(false);
        for (int i = 0; i < tags.Length; i++) {
            GameObject obj = Instantiate(go) as GameObject;
            var listv = obj.GetComponent<Toggle>();
            obj.SetActive(true);
            listv.transform.parent = clubTipsContent;
            listv.transform.localScale = new Vector2(1, 1);
            listv.transform.SetSiblingIndex(clubTipsContent.childCount-2);
            obj.transform.Find("Label").GetComponent<Text>().text = tags[i];
			obj.transform.Find("delTagBtn/Button").GetComponent<Button>().onClick.AddListener(()=> { 
				Destroy(obj); 
				addBtn.gameObject.SetActive (true);
			});
        }

		if (tags.Length > 2) {
			addBtn.gameObject.SetActive (false);

		} else {
			addBtn.gameObject.SetActive (true);
		}
    }


    public void AddTag(string tagContent) {
		if (clubTipsContent.childCount >= 5) {
            ClubManager.GetSingleton().commonPopup.ShowView("标签达到上限！");
            return;
        }


        GameObject go = clubTipsContent.GetChild(0).gameObject;
        GameObject obj = Instantiate(go) as GameObject;
        var listv = obj.GetComponent<Toggle>();
        obj.SetActive(true);
        listv.transform.parent = clubTipsContent;
        listv.transform.localScale = new Vector2(1, 1);
        listv.transform.SetSiblingIndex(clubTipsContent.childCount - 2);
        obj.transform.Find("Label").GetComponent<Text>().text = tagContent;
		obj.transform.Find("delTagBtn/Button").GetComponent<Button>().onClick.AddListener(() => { 
			Destroy(obj); 
			addBtn.gameObject.SetActive (true);
		});
    }

    public bool GetTag()
    {
        int tagCount = 0;
        bool pass = true;
        for (int i = 0; i < clubTipsContent.childCount - 2; i++)
        {
            if (clubTipsContent.GetChild(i + 1).GetComponent<Toggle>().isOn)
            {
                clubTag += clubTipsContent.GetChild(i + 1).Find("Label").GetComponent<Text>().text + "|";
                tagCount++;
            }

        }
        clubTag = clubTag.TrimEnd(new char[] { '|' });

        if (tagCount > 4)
        {
            ClubManager.GetSingleton().commonPopup.ShowView("最多上传四个标签");
            pass = false;
        }
        return pass;
    }



    public void DelTag()
    {

        for (int i = 0; i < clubTipsContent.childCount - 2; i++)
        {
            Destroy(clubTipsContent.GetChild(i + 1).gameObject);
        }

		addBtn.gameObject.SetActive (true);

    }

    string log = "";
    Texture2D texture;

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    public void OnImageLoad(Texture2D tex)
    {
        texture = tex;
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        clubHead.texture = sprite.texture;
        StartCoroutine(UpLoadImage());
    }

	//上传图片到指定服务器
	IEnumerator UpLoadImage()
	{

		byte[] mm = texture.EncodeToPNG();
		WWWForm form = new WWWForm();
		string name = ClubManager.GetSingleton().clubPanel.clubId.ToString();
		form.AddBinaryData("image", mm, name, "image/png");
		WWW www = new WWW(StaticData.imgUrl, form);
		yield return www;
		if (www.error != null)
		{
			Debug.Log("图像获取失败");
		}
		else
		{
			LoadImageUrl = www.text;

		}

	}


    public void SetIcon(Texture sprite)
    {
        PopupCommon.GetSingleton().ShowView("SetIcon(Texture sprite)");
        if(sprite != null)
        {
            clubHead.texture = sprite;
        }
        
    }


    public void AndroidMessage(string imgPath)
    {
       
        HallManager.GetSingleton().AndroidMessage(imgPath);
    }
}
