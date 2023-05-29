using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnionJoinPanel : BasePlane
{

    public InputField findUnionInput;  
    public Transform infoContent;

    void Awake()
    {
        Button backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            GameTools.GetSingleton().stopGameToolsAllCoroutines();
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        findUnionInput = transform.Find("FindClub").GetComponent<InputField>();
        infoContent = transform.Find("Info/Viewport/Content");

        Button cancelBtn = transform.Find("FindClub/CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            findUnionInput.text = "";
            GameTools.GetSingleton().HideSv(infoContent);
        });

        findUnionInput.onEndEdit.AddListener((string s) => {
            NetMngr.GetSingleton().Send(InterfaceUnion.QueryUnion, new object[] { 
                findUnionInput.text, UnionManager.GetSingleton().clubId });
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
    }

    public override void OnRemoveComplete()
    {
        findUnionInput.text = "";
        GameTools.GetSingleton().HideSv(infoContent);
    }

    public override void OnRemoveStart()
    {
    }


    public void QueryUnionCallBack(Hashtable data) {
        GameTools.GetSingleton().HideSv(infoContent);

        GameObject cell = GameTools.GetSingleton().GetSvCell(infoContent);
        Transform tr = cell.transform;
        tr.Find("name").GetComponent<Text>().text = data["lmName"].ToString();

        Button joinBtn = tr.Find("JoinBtn").GetComponent<Button>();
        bool isJoin = int.Parse(data["join"].ToString()) == 1;
        if(isJoin){ // 已加入
            joinBtn.interactable = false;
            tr.Find("JoinBtn/Text").GetComponent<Text>().text = "已加入";
        } else { // 未加入
            joinBtn.interactable = true;
            tr.Find("JoinBtn/Text").GetComponent<Text>().text = "加入";
        }

        joinBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceUnion.JoinUnion, new object[] { 
                data["id"].ToString(), UnionManager.GetSingleton().clubId });
        });

    }

}
