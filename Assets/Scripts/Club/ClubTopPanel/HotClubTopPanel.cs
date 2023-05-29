using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HotClubTopPanel : BasePlane
{

    public Button backBtn;

    public InputField FindClubInput;
    public GameObject FindImage;
    public Button cancelBtn;

    public Transform clubItemContent;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();


        FindClubInput = transform.Find("FindClub").GetComponent<InputField>();
        FindImage = transform.Find("FindClub/Image").gameObject;
        cancelBtn = transform.Find("FindClub/CancelBtn").GetComponent<Button>();

        clubItemContent = transform.Find("Info/Viewport/Content");

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        cancelBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            FindImage.SetActive(true);
            FindClubInput.text = "";
        });


        FindClubInput.onEndEdit.AddListener((string s) => {
            OnEndEdit();
        });
        gameObject.SetActive(false);
    }


    void Start()
    {
        
    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {
       
    }

    public override void OnAddStart()
    {
        FindImage.SetActive(true);
        NetMngr.GetSingleton().Send(InterfaceClub.GetHotClub, new object[] { });
    }

    public override void OnRemoveComplete()
    {
        FindImage.SetActive(true);
        FindClubInput.text = "";
        //顺便调用刷新下已加入俱乐部
		ClubManager.GetSingleton().clubListPanel.GetClubList();
//        ClubManager.GetSingleton().clubListPanel.myClubTog.isOn = true;
//        ClubManager.GetSingleton().clubListPanel.toggleIndex = 2;
    }

    public override void OnRemoveStart()
    {

    }

    public void OnEndEdit()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetClub, new object[] { FindClubInput.text });

    }


    public void GetHotClubCallBack(Hashtable data) {

        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);

        ArrayList List = data["HotClubList"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
            Debug.Log("没俱乐部");
            return;
        }

        for (int i = 0; i < List.Count; i++)
        {

            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/HotClubItem")) as GameObject;

            var listv = obj.GetComponent<HotClubItem>();
            listv.transform.parent = clubItemContent;
            listv.transform.localScale = new Vector2(1, 1);

            //赋值
            listv.SetInfo(ht["url"].ToString(), ht["clubName"].ToString(), ht["memberCount"].ToString(), ht["isJoin"].ToString(), ht["tag"].ToString(), ht["clubId"].ToString());


        }

    }

}
