using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System.IO;

public class ClubCreateTopPanel : BasePlane
{
    public Button backBtn;
   
    public Button completeBtn;
    public GameObject title;

    public RawImage clubHead;

    public Button camreaBtn;

    public Text curCoin;
    public Text coinTip;

    public InputField clubNameInput;
    public InputField kefuWXInput;

    public Transform clubTipsContent;
    public Button addBtn;

    public string LoadImageUrl;

    public string clubTag;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
       
	//	completeBtn= transform.Find("ToggleGroup/saveBtn").GetComponent<Button>();  //edit by yang yang 2021年3月17日 13:59:51
        completeBtn = transform.Find("saveBtn").GetComponent<Button>();
        title = transform.Find("ToggleGroup/Title").gameObject;
      

        clubHead = transform.Find("ClubHead/mask/head").GetComponent<RawImage>();

        camreaBtn= transform.Find("camreaBtn").GetComponent<Button>();

        curCoin= transform.Find("Coin/Text").GetComponent<Text>();
		coinTip= transform.Find("Coin/Text (1)").GetComponent<Text>();

        clubNameInput = transform.Find("clubName").GetComponent<InputField>();
        kefuWXInput= transform.Find("kefuWX").GetComponent<InputField>();

        clubTipsContent= transform.Find("ClubTips/Scroll View/Viewport/Content");
        addBtn = clubTipsContent.Find("AddTip/addBtn").GetComponent<Button>();


        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            StopAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });


        addBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().addClubTipTopPanel);
            ClubManager.GetSingleton().addClubTipTopPanel.curTagType = AddClubTipTopPanel.TagType.createTag;
        });

        completeBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //GetTag();
            if (GetTag()) {
                NetMngr.GetSingleton().Send(InterfaceClub.CreateClub, new object[] { LoadImageUrl, clubNameInput.text, kefuWXInput.text, clubTag });
            }
           
            clubTag = "";

        });

        camreaBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().OpenAlbum("", "", 2, "");

        });

        NetMngr.GetSingleton().AddListener("c/getDiamondCost",GetDiamondCost);

        gameObject.SetActive(false);
    }


    public void GetDiamondCost(Hashtable data) {
        curCoin.text =data["DiamondCost"].ToString();
        
    }

    void Start()
    {

    }

    void Update()
    {
        if (clubNameInput.text.Length >12)
        {
            clubNameInput.text = clubNameInput.text.Substring(0, 12);
        }
    }

    public string GetChinese(string s) {
        byte[] buffer1 = Encoding.Default.GetBytes(s);
        byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, buffer1, 0, buffer1.Length);
        string strBuffer = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
        return strBuffer;
       
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.SetClubTag, new object[] {  ClubManager.GetSingleton().clubPanel.clubId,"0" });
        //获取个人中心的积分
        coinTip.text = StaticData.diamond+"";
    }

    public override void OnAddStart()
    {
        DelTag();
        NetMngr.GetSingleton().Send("c/getDiamondCost", new object[] {  });
    }

    public override void OnRemoveComplete()
    {
        clubNameInput.text = "";
        kefuWXInput.text = "";
    }

    public override void OnRemoveStart()
    {

    }

    public void SetClubTagCallBack(Hashtable data)
    {
        string[] tags = data["tag"].ToString().Split('|');


        GameObject go = clubTipsContent.GetChild(0).gameObject;
        go.SetActive(false);
        for (int i = 0; i < tags.Length; i++)
        {
            GameObject obj = Instantiate(go) as GameObject;
            var listv = obj.GetComponent<Toggle>();
            obj.SetActive(true);
            listv.transform.parent = clubTipsContent;
            listv.transform.localScale = new Vector2(1, 1);
            listv.transform.SetSiblingIndex(clubTipsContent.childCount - 2);
            obj.transform.Find("Label").GetComponent<Text>().text = tags[i];
        }

//		if (tags.Length > 2) {
//			addBtn.gameObject.SetActive (false);
//				
//		} else {
//			addBtn.gameObject.SetActive (true);
//		}
//
    }

    public void AddTag(string tagContent)
    {
        if (clubTipsContent.childCount >= 4)
        {
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
       
    }


    public bool GetTag() {
        int tagCount = 0;
        bool pass = true;
        for (int i = 0; i < clubTipsContent.childCount-2; i++)
        {
            if (clubTipsContent.GetChild(i + 1).GetComponent<Toggle>().isOn)
            {
                clubTag += clubTipsContent.GetChild(i + 1).Find("Label").GetComponent<Text>().text + "|";
                tagCount++;
            }

        }
        clubTag= clubTag.TrimEnd(new char[] { '|'});

        if (tagCount > 4) {
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
     /*   texture = tex;
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        clubHead.sprite = sprite;
        StartCoroutine(UpLoadImage());*/
    }

    public void SetIcon(Texture sprite)
    {
        clubHead.texture = sprite;
    }

    IEnumerator UpLoadImage()
	{
		byte[] mm = texture.EncodeToPNG();
		WWWForm form = new WWWForm();
		string name = GameTools.GetSingleton ().ConvertDateTimeToLong (System.DateTime.Now).ToString();
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
}