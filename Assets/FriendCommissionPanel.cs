using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendCommissionPanel : BasePlane
{


    public Toggle ToggleShare;
    public Toggle ToggleFriend;
    public Toggle ToggleRecord;

    public GameObject pageShare;
    public GameObject pageFriend;
    public GameObject pageRecord;

 
    private InputField inputCode;
    private Button btBinding;
    private Button backBtn;
    private Button LookRecord;

    private Button btCopy;
    private Button btSave;

    private Transform parentFriend;
    private Transform parentRecord;
    private GameObject shareBG;

    public Text txtCode;
    public Text txtCommissionCount;
    public Text txtPlayerCount;
    private string strCode;
    private string strURL;
    public int nClubID;
    public bool bShowShare =  false;
    private void Awake()
    {
        pageShare = transform.Find("PagShare").gameObject;
        pageFriend = transform.Find("PageFriend").gameObject;
        pageRecord = transform.Find("PageRecord").gameObject;

        //sendText = pageShare.transform.Find("send/shuliang").GetComponent<Text>();
        //aleardyText = pageShare.transform.Find("aleardy/shuliang").GetComponent<Text>();
        //inputName = pageShare.transform.Find("InputName").GetComponent<InputField>();
        //inputCount = pageShare.transform.Find("InputCount").GetComponent<InputField>();
        //sendBtn = pageShare.transform.Find("sendTo").GetComponent<Button>();
        backBtn = transform.Find("Top/Back/Share").GetComponent<Button>();
        btBinding = transform.Find("InputBG/BindingBT").GetComponent<Button>();
        btCopy = transform.Find("CopyBT").GetComponent<Button>();
        btSave = transform.Find("SaveBT").GetComponent<Button>();

        inputCode = transform.Find("InputBG/InputCount").GetComponent<InputField>();

        ToggleShare = transform.Find("Top/ToggleGroup/Toggle1").GetComponent<Toggle>();
        ToggleFriend = transform.Find("Top/ToggleGroup/Toggle2").GetComponent<Toggle>();
        ToggleRecord = transform.Find("Top/ToggleGroup/Toggle3").GetComponent<Toggle>();
        parentFriend = pageFriend.transform.Find("ScrollView/Viewport/Content");
        parentRecord = pageRecord.transform.Find("ScrollView/Viewport/Content");
        txtPlayerCount = pageFriend.transform.Find("txtPlayerCount").GetComponent<Text>();
        txtCommissionCount = pageFriend.transform.Find("txtCommissionCount").GetComponent<Text>();
        txtCode = pageShare.transform.Find("BG/TextCode").GetComponent<Text>();
        shareBG = transform.Find("InputBG").gameObject;

        ToggleShare.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                this.pageShare.SetActive(true);
                this.pageFriend.SetActive(false);
                this.pageRecord.SetActive(false);
                //  ClubManager.GetSingleton()
                this.btCopy.gameObject.SetActive(false);
                this.btSave.gameObject.SetActive(false);
                this.shareBG.SetActive(true);
                NetMngr.GetSingleton().Send(InterfaceClub.GetUserShare, new object[] { nClubID });
            }
        });
        ToggleFriend.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                this.pageShare.SetActive(false);
                this.pageFriend.SetActive(true);
                this.pageRecord.SetActive(false);
                this.btCopy.gameObject.SetActive(false);
                this.btSave.gameObject.SetActive(false);
                this.shareBG.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceClub.GetFriendComission, new object[] { });
               
            }
        });
        ToggleRecord.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                this.pageShare.SetActive(false);
                this.pageFriend.SetActive(false);
                this.pageRecord.SetActive(true);
                this.shareBG.SetActive(false);
                this.btCopy.gameObject.SetActive(false);
                this.btSave.gameObject.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceClub.GetComissionRecord, new object[] { });
            }
        });
        backBtn.onClick.AddListener(() =>
        {
            this.nClubID = 0;
            txtCode.text = "";
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        btBinding.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(inputCode.text))
            {
                Debug.Log(strCode);
                NetMngr.GetSingleton().Send(InterfaceClub.GetBindingCode, new object[] { inputCode.text });
                
                inputCode.text = "";
            }
            else
            {
                PopupCommon.GetSingleton().ShowView("输入信息不能为空");
            }
        });
        btCopy.onClick.AddListener(() =>
        {
            UniClipboard.SetText(strURL);
            ClubManager.GetSingleton().commonPopup.ShowView("复制成功");
        });
        btSave.onClick.AddListener(() =>
        {
            GameTools.GetSingleton().CaptureScreenshot();
        });

        this.nClubID = 0;

     //   Init();


    }
    private void Init()
    {
        if (!bShowShare)
        {
            ToggleShare.isOn = false;
            ToggleFriend.isOn = true;
            ToggleRecord.isOn = false;

            this.pageShare.SetActive(false);
            this.pageFriend.SetActive(true);
            this.pageRecord.SetActive(false);
            this.btCopy.gameObject.SetActive(false);
            this.btSave.gameObject.SetActive(false);
            this.shareBG.SetActive(false);
            this.ToggleShare.gameObject.SetActive(false);
        }
        else
        {
            this.pageShare.SetActive(true);
            this.pageFriend.SetActive(false);
            this.pageRecord.SetActive(true);
            this.btCopy.gameObject.SetActive(false);
            this.btSave.gameObject.SetActive(false);
            this.shareBG.SetActive(true);
            this.ToggleShare.gameObject.SetActive(true);
            ToggleShare.isOn = true;
            ToggleFriend.isOn = false;
            ToggleRecord.isOn = false;
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {
        /*
        if (!bShowShare)
        {
            ToggleShare.isOn = false;
            ToggleFriend.isOn = true;
            ToggleRecord.isOn = false;

            this.pageShare.SetActive(false);
            this.pageFriend.SetActive(true);
            this.pageRecord.SetActive(false);
            this.btCopy.gameObject.SetActive(false);
            this.btSave.gameObject.SetActive(false);
            this.shareBG.SetActive(false);
            this.ToggleShare.gameObject.SetActive(false);
        }
        else
        {
            this.pageShare.SetActive(true);
            this.pageFriend.SetActive(false);
            this.pageRecord.SetActive(true);
            this.btCopy.gameObject.SetActive(true);
            this.btSave.gameObject.SetActive(true);
            this.shareBG.SetActive(true);
            this.ToggleShare.gameObject.SetActive(true);
            ToggleShare.isOn = true;
            ToggleFriend.isOn = false;
            ToggleRecord.isOn = false;
            NetMngr.GetSingleton().Send(InterfaceClub.GetUserShare, new object[] { nClubID });
        }
        */
        NetMngr.GetSingleton().Send(InterfaceClub.GetUserShare, new object[] { nClubID });
    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        ToggleShare.isOn = true;
        ToggleFriend.isOn = false;
        ToggleRecord.isOn = false;
    }

    public override void OnRemoveStart()
    {

    }
    
    
    private void ClearListFriend()
    {
        int childCount = parentFriend.childCount;
        if (childCount != 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(parentFriend.GetChild(i).gameObject);
            }
        }
    }
    private void ClearListRecord()
    {
        int childCount = parentRecord.childCount;
        if (childCount != 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(parentRecord.GetChild(i).gameObject);
            }
        }
    }


    public void getUserShareCallBack(Hashtable data)
    {
        strCode = data["code"].ToString();
        if (strCode == "-1")
        {
            txtCode.text = "请先加入俱乐部";
        }
        else
        {
            strCode = data["code"].ToString();
            txtCode.text = strCode;
        }
        
        strURL = data["download_url"].ToString();
       
        if(data["superior_user_id"].ToString() == "1")
        {
            shareBG.gameObject.SetActive(false);
        //    btSave.gameObject.transform.localPosition = new Vector3(200.0f, -600.0f, btSave.gameObject.transform.localPosition.z);
        //    btCopy.gameObject.transform.localPosition = new Vector3(-200.0f, -600.0f, btCopy.gameObject.transform.localPosition.z);
          
        }
        else
        {
            shareBG.gameObject.SetActive(true);
         //   btSave.transform.position = new Vector2(200, 250);
        //    btSave.gameObject.transform.localPosition = new Vector3(200.0f, -450.0f, btSave.gameObject.transform.localPosition.z);
       //     btCopy.gameObject.transform.localPosition = new Vector3(-200.0f, -450.0f, btCopy.gameObject.transform.localPosition.z);
        }

        this.btCopy.gameObject.SetActive(true);
        this.btSave.gameObject.SetActive(true);
    }
    public void getFriendComissionCallBack(Hashtable data)
    {
        ClearListFriend();
        ArrayList withList = data["records"] as ArrayList;
        txtCommissionCount.text = "佣金总和:" + data["contribution"].ToString();
        txtPlayerCount.text = "人数:" + data["count"].ToString();
        Object objItem = Resources.Load("HallItem/CommissionItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parentFriend);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero; //SetInfo

            Hashtable ht = withList[i] as Hashtable;
            CommissionItem commissionItem = go.AddComponent<CommissionItem>(); 
            commissionItem.SetInfo(ht["userName"].ToString(), ht["userId"].ToString(), ht["contribution"].ToString(), ht["updateTime"].ToString());
        }

    }
    public void getComissionRecordCallBack(Hashtable data)
    {
       // state   发放类型  0没法放  
       //type   游戏类型
        ClearListRecord();
        ArrayList withList = data["records"] as ArrayList;
        Object objItem = Resources.Load("HallItem/CommissionRecordItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parentRecord);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero; //SetInfo

            Hashtable ht = withList[i] as Hashtable;
            CommissionRecordItem commissionRecordItem = go.AddComponent<CommissionRecordItem>();
            commissionRecordItem.SetInfo(ht["fee"].ToString(), ht["state"].ToString(), ht["createTime"].ToString(), ht["type"].ToString());
        }

    }
    public void getBindingCodeCallBack(Hashtable data)
    {

        string strMessage = data["success"].ToString();
        if (data["success"].ToString() == "1")
        {
            shareBG.gameObject.SetActive(false);
            //   btSave.gameObject.transform.localPosition = new Vector3(200.0f, -600.0f, btSave.gameObject.transform.localPosition.z);
            //    btCopy.gameObject.transform.localPosition = new Vector3(-200.0f, -600.0f, btCopy.gameObject.transform.localPosition.z);
            ClubManager.GetSingleton().commonPopup.ShowView("绑定成功");
        }
        else
        {
            shareBG.gameObject.SetActive(true);
            //   btSave.transform.position = new Vector2(200, 250);
        //    btSave.gameObject.transform.localPosition = new Vector3(200.0f, -450.0f, btSave.gameObject.transform.localPosition.z);
         //   btCopy.gameObject.transform.localPosition = new Vector3(-200.0f, -450.0f, btCopy.gameObject.transform.localPosition.z);
        }
      
        
    }
    public void getLookRecordCallBack(Hashtable data)
    {
        ClearListFriend();
        ArrayList withList = data["records"] as ArrayList;
        Object objItem = Resources.Load("HallItem/LookCommissionItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parentFriend);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero; //SetInfo

            Hashtable ht = withList[i] as Hashtable;
            LookCommissionItem lookCommissionItem = go.AddComponent<LookCommissionItem>();
            lookCommissionItem.SetInfo(ht["gameType"].ToString(), ht["earn"].ToString(), ht["createTime"].ToString(), ht["contribution"].ToString());
        }

    }
}
