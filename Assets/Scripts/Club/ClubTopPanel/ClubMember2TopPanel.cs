using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClubMember2TopPanel : BasePlane
{

    public Button backBtn;
    public GameObject title;
    public GameObject title2;

    public InputField FindMemberInput;
    public GameObject FindImage;

    public Transform memberItemContent;

    public int panelType = 1;


    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back").GetComponent<Button>();
        title = transform.Find("ToggleGroup/Title").gameObject;
        title2 = transform.Find("ToggleGroup/Title2").gameObject;

        FindMemberInput = transform.Find("FindMember").GetComponent<InputField>();
        FindImage = transform.Find("FindMember/Image").gameObject;

        memberItemContent = transform.Find("Info/Viewport/Content");

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        ChangeType();
    }

    public void ChangeType() {
        if (panelType == 1)
        {
            title.gameObject.SetActive(true);
            title2.gameObject.SetActive(false);
        }
        else {
            title.gameObject.SetActive(false);
            title2.gameObject.SetActive(true);
        }

    }


    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.MemberFindAll, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
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

    public void OnEndEdit()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.MemberFind, new object[] { ClubManager.GetSingleton().clubPanel.clubId, FindMemberInput.text });

    }

    public void MemberFindCallBack(Hashtable data)
    {
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
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/MemberItem2")) as GameObject;

            var listv = obj.GetComponent<MemberItem2>();
            listv.transform.parent = memberItemContent;
            listv.transform.localScale = new Vector2(1, 1);

            //赋值
            listv.SetInfo( ht["headUrl"].ToString(), ht["id"].ToString(), ht["memberName"].ToString(), ht["isHost"].ToString());


        }

    }
}
