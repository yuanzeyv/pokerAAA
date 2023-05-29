using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClubMemberTopPanel : BasePlane
{
    public Button backBtn;
    public Button selectBtn;

    public InputField FindMemberInput;
    public GameObject FindImage;

    public Transform memberItemContent;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        selectBtn= transform.Find("ToggleGroup/SelectBtn").GetComponent<Button>();

        FindMemberInput= transform.Find("FindMember").GetComponent<InputField>();
        FindImage= transform.Find("FindMember/Image").gameObject;

        memberItemContent = transform.Find("Info/Viewport/Content");


        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        selectBtn.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().screenMemberPopup.ShowView();
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
        NetMngr.GetSingleton().Send(InterfaceClub.ManagerFindAll, new object[] { ClubManager.GetSingleton().clubPanel.clubId});
    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        FindImage.SetActive(true);
    }

    public override void OnRemoveStart()
    {

    }

    public void OnEndEdit() {
        NetMngr.GetSingleton().Send(InterfaceClub.ManagerFind, new object[] { ClubManager.GetSingleton().clubPanel.clubId, FindMemberInput.text });

    }

    public void ManagerFindCallBack(Hashtable data) {
        BroadcastMessage("DelSelf", SendMessageOptions.DontRequireReceiver);

        ArrayList List = data["MemberList"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
          
            return;
        }



        for (int i = 0; i < List.Count; i++)
        {





            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/MemberItem")) as GameObject;

            var listv = obj.GetComponent<MemberItem>();
            listv.transform.parent = memberItemContent;
            listv.transform.localScale = new Vector2(1, 1);

            //赋值
            listv.SetInfo(ht["no"].ToString(),ht["headUrl"].ToString(), ht["id"].ToString(), ht["memberName"].ToString(), ht["isHost"].ToString(), ht["totalGame"].ToString(), ht["activeTime"].ToString());


        }

    }
}
