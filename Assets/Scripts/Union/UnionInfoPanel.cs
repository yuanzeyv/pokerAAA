using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System.IO;

public class UnionInfoPanel : BasePlane
{
	public Transform info;
	public Transform infoContent;

	public Button dissBtn;
	public Button sjBtn;
	public Toggle toggle;

	private int isMine = 0;
    private string unionName = "";

	void Awake(){
		Button backBtn = transform.Find("Top/Back/Share").GetComponent<Button>();
		backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
     		GameTools.GetSingleton().stopGameToolsAllCoroutines();
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
            UnionManager.GetSingleton().myUnionPanel.Refresh();       
        });

        sjBtn = transform.Find("Top/shengji").GetComponent<Button>();
		sjBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UnionManager.GetSingleton().planeManager.AddTopPlane(UnionManager.GetSingleton().unionUpgradePanel);
     	});

        dissBtn = transform.Find("Info/dissBtn").GetComponent<Button>();
		dissBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UnionManager.GetSingleton().commonPopup.ShowView("确定解散联盟？",null,true,()=> {
				NetMngr.GetSingleton().Send(InterfaceUnion.DissUnion,new object[] {
					UnionManager.GetSingleton().unionId });
			});
     	});        

        info = transform.Find("Info");
        infoContent = transform.Find("ClubInfo/Viewport/Content");

        Button copyBtn = infoContent.Find("item2/CopyBtn").GetComponent<Button>();
        copyBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            UniClipboard.SetText(UnionManager.GetSingleton().unionId);
            ClubManager.GetSingleton().commonPopup.ShowView("复制成功");
        });

        toggle = infoContent.Find("item3/Toggle").GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(
            (bool s) =>
            {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                if(toggle.isOn){
                	NetMngr.GetSingleton().Send(InterfaceUnion.SetUnionApply, new object[] {
			        	UnionManager.GetSingleton().unionId, 1
			        });
                } else {
                	NetMngr.GetSingleton().Send(InterfaceUnion.SetUnionApply, new object[] {
			        	UnionManager.GetSingleton().unionId, 0
			        });
                }
        	});

		gameObject.SetActive(false);
	}

	public void SetInfo(Hashtable data){
        unionName = data["lmName"].ToString();
		info.Find("unionName").GetComponent<Text>().text = unionName;
		info.Find("adminName").GetComponent<Text>().text = data["userMame"].ToString();
		string memberCount = string.Format("{0}/{1}", data["memberCount"].ToString(), data["maxCount"].ToString());
		info.Find("memberCount").GetComponent<Text>().text = memberCount;
		isMine = int.Parse(data["isMine"].ToString());
		dissBtn.gameObject.SetActive(isMine == 1);
		sjBtn.gameObject.SetActive(isMine == 1);

		infoContent.Find("item2/id").GetComponent<Text>().text = UnionManager.GetSingleton().unionId;
		toggle.isOn = int.Parse(data["apply"].ToString()) == 1;
		infoContent.Find("item3").gameObject.SetActive(isMine == 1);
	}

	public void SetClubInfo(Hashtable data){
		HideSv();
		ArrayList clubList = data["clubList"] as ArrayList;
		for(int i = 0; i < clubList.Count; i++){
			Hashtable h = clubList[i] as Hashtable;
			GameObject cell = GetSvCell();
			Transform tr = cell.transform;
			string clubName = h["clubName"].ToString();
			tr.Find("ClubName").GetComponent<Text>().text = clubName;
            tr.Find("userName").GetComponent<Text>().text = h["userName"].ToString();
			string memberCount = string.Format("{0}/{1}", h["memberCount"].ToString(), h["maxCount"].ToString());
			tr.Find("ClubMemberCount").GetComponent<Text>().text = memberCount;

			RawImage head = tr.Find("ClubHead/head").GetComponent<RawImage>();
			GameTools.GetSingleton().GetTextureNet(h["url"].ToString(), (Texture s)=>{
				head.texture = s;
			});

			string clubId = h["clubId"].ToString();
			Button kickBtn = tr.Find("KickBtn").GetComponent<Button>();
			kickBtn.gameObject.SetActive(isMine == 1 && clubId != UnionManager.GetSingleton().clubId);
			kickBtn.onClick.AddListener(()=>{
				SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
				string tip = string.Format("确定要将俱乐部{0}踢出联盟", clubName);	
        		UnionManager.GetSingleton().commonPopup.ShowView(tip,null,true,()=> {
					NetMngr.GetSingleton().Send(InterfaceUnion.KickUnionClub,new object[] {
						clubId, UnionManager.GetSingleton().unionId });
					});
				});

            Button exitBtn = tr.Find("ExitBtn").GetComponent<Button>();
            exitBtn.gameObject.SetActive(isMine != 1 && h["userId"].ToString() == StaticData.ID);
            exitBtn.onClick.AddListener(()=>{
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                string tip = string.Format("确定要退出联盟{0}", unionName); 
                UnionManager.GetSingleton().commonPopup.ShowView(tip,null,true,()=> {
                    NetMngr.GetSingleton().Send(InterfaceUnion.QuitUnion, new object[] {
                        UnionManager.GetSingleton().unionId, clubId
                    });
                });
            });
		}
	}

	public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceUnion.GetUnionInfo, new object[] {
        	UnionManager.GetSingleton().unionId
        });
        NetMngr.GetSingleton().Send(InterfaceUnion.GetUnionClub, new object[] {
        	UnionManager.GetSingleton().unionId
        });
    }

    public override void OnAddStart()
    {
    }

    public override void OnRemoveComplete()
    {
        HideSv();
    }

    public override void OnRemoveStart()
    {

    }

    private GameObject GetSvCell()
    {
        for(int i=0; i < infoContent.childCount-2; i++){
            GameObject obj = infoContent.GetChild(i).gameObject;
            if(!obj.activeSelf){
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject cell = Instantiate(infoContent.GetChild(0).gameObject);
        cell.transform.SetParent(infoContent);
        cell.transform.localScale = Vector3.one;
        cell.transform.localPosition = Vector3.zero;
        cell.transform.SetSiblingIndex(infoContent.childCount-3);
        cell.SetActive(true);
        return cell;
    }

    private void HideSv(){
    	for(int i=0; i < infoContent.childCount-2; i++){
            infoContent.GetChild(i).gameObject.SetActive(false);
        }
    }

}