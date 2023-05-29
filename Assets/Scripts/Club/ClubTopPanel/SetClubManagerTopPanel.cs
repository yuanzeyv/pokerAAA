using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SetClubManagerTopPanel : BasePlane
{
    public Button backBtn;

    public Transform infoContent;

    public Button addManagerBtn;

    void Awake()
    {
        backBtn = transform.Find("ToggleGroup/Back").GetComponent<Button>();

        infoContent = transform.Find("Info/Viewport/Content");

		addManagerBtn = transform.Find("addManager/AddBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        addManagerBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().clubMember2TopPanel.panelType = 2;
            ClubManager.GetSingleton().planeManager.AddTopPlane(ClubManager.GetSingleton().clubMember2TopPanel);

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
        NetMngr.GetSingleton().Send(InterfaceClub.GetManager, new object[] { ClubManager.GetSingleton().clubPanel.clubId });
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }

    public void GetManagerCallBack(Hashtable data) {
        BroadcastMessage("DelSelf",SendMessageOptions.DontRequireReceiver);

        ArrayList List = data["ManagerList"] as ArrayList;
        //Debug.LogWarning(List.Count);
        if (List == null)
        {
            return;
        }

        for (int i = 0; i < infoContent.childCount-1; i++) {
            Destroy(infoContent.GetChild(i).gameObject);
        }


            for (int i = 0; i < List.Count; i++)
        {

            Hashtable ht = List[i] as Hashtable;
            GameObject obj = Instantiate(Resources.Load("Club/ClubItem/managerItem")) as GameObject;

            var listv = obj.GetComponent<managerItem>();
         
            listv.transform.parent = infoContent;
            
            listv.transform.localScale = new Vector2(1, 1);
            listv.transform.SetSiblingIndex(infoContent.childCount - 2);
            //赋值
            listv.SetInfo(ht["headUrl"].ToString(), ht["id"].ToString(), ht["name"].ToString());


        }

    }
}
