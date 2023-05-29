
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class TousuPanel : BasePopup
{

    private Button cancelBtn;
    private Button sureBtn;

    public Toggle[] Toggles;
    public int[] users = new int[8];

    public int curPage = 0;
    public int isMine = 0;
    public int diamond = 0;

    private void Awake()
    {
        Init();
        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HideView();
        });
        sureBtn = transform.Find("SureBtn").GetComponent<Button>();
        sureBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            int uid = 0;
            for(int i = 0; i < Toggles.Length; i++){
                if(Toggles[i].gameObject.activeSelf && Toggles[i].isOn){
                    uid = users[i];
                    break;
                }
            }
            if(uid == 0){
                Tip.Instance.showMsg("没有选择要投诉的玩家！");
                return;
            }
            NetMngr.GetSingleton().Send(InterfaceGame.getRoundReviewByPay, new object[] { curPage, isMine, diamond, uid });
        });

        Toggles = transform.Find("UserToggle").GetComponentsInChildren<Toggle>();
        for(int i = 0; i < Toggles.Length; i++){
            Toggles[i].gameObject.SetActive(false);
        }

    }

    public void RefreshUsers(Hashtable data ){
        for(int i = 0; i < Toggles.Length; i++){
            Toggles[i].gameObject.SetActive(false);
        }
        ArrayList list = data["list"] as ArrayList;
        for(int i = 0; i < list.Count; i++){
            Hashtable l = list[i] as Hashtable;
            users[i] = int.Parse(l["id"].ToString());

            Toggle toggle = Toggles[i];
            if(toggle == null) break;
            toggle.gameObject.SetActive(true);
            toggle.gameObject.transform.Find("Label").GetComponent<Text>().text = l["nickName"].ToString();
            toggle.isOn = i == 0;
        }
    }

    public void ShowView(int page, int mine)
    {
        curPage = page;
        isMine = mine;
        diamond = GameManager.GetSingleton().roomDaMang * 10;
        transform.Find("diamond").GetComponent<Text>().text = diamond.ToString();        
        
        NetMngr.GetSingleton().Send(InterfaceGame.getRoundReviewUsers, new object[] { curPage });
        
        base.ShowView();
    }

    public void HideView()
    {
        base.HideView();
    }


}