using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ClubCashOutPanel : BasePlane
{
    public Button backBtn;
    public Button CashOutBT;

 

    public Toggle ToggleCashOut;
    public Toggle ToggleCashOutRecord;

    public GameObject pagePagCashOut;
    public GameObject PageCashOutRecord;

    private InputField inputCode;
    private Transform parentCashOutRecord;

    private Text txtCanGold;
    void Awake()
    {
        backBtn = transform.Find("Top/Back/Share").GetComponent<Button>();
        pagePagCashOut = transform.Find("PagCashOut").gameObject;
        PageCashOutRecord = transform.Find("PageCashOutRecord").gameObject;
   
        CashOutBT = pagePagCashOut.transform.Find("InputImage/CashOutBT").GetComponent<Button>();
        inputCode = transform.Find("PagCashOut/InputCount").GetComponent<InputField>();

        ToggleCashOut = transform.Find("Top/ToggleGroup/Toggle1").GetComponent<Toggle>();
        ToggleCashOutRecord = transform.Find("Top/ToggleGroup/Toggle2").GetComponent<Toggle>();
        parentCashOutRecord = PageCashOutRecord.transform.Find("ScrollView/Viewport/Content");

        txtCanGold = pagePagCashOut.transform.Find("ImageCanMoney/TextMoney").GetComponent<Text>();
        ToggleCashOut.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                this.pagePagCashOut.SetActive(true);
                this.PageCashOutRecord.SetActive(false);
                NetMngr.GetSingleton().Send(InterfaceClub.GetCashOutInfo, new object[] { });
            }
        });
        ToggleCashOutRecord.onValueChanged.AddListener((isOn) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (isOn)
            {
                this.pagePagCashOut.SetActive(false);
                this.PageCashOutRecord.SetActive(true);          
                NetMngr.GetSingleton().Send(InterfaceClub.GetCashOutRecord, new object[] { });
            }
        });

        backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            ClubManager.GetSingleton().planeManager.RemoveTopPlane();
        });
        CashOutBT.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (!string.IsNullOrEmpty(inputCode.text))
            {
                int nGold = int.Parse(inputCode.text);
                if(nGold < 10)
                {
                    PopupCommon.GetSingleton().ShowView("至少提现10积分！");
                }
                Debug.Log(inputCode.text);
                NetMngr.GetSingleton().Send(InterfaceClub.GetCashOut, new object[] { inputCode.text});

                inputCode.text = "";
            }
            else
            {
                PopupCommon.GetSingleton().ShowView("输入信息不能为空");
            }
        });

        inputCode.onValueChanged.AddListener((strInputInfo) =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");   
            int nGold = int.Parse(inputCode.text);

            inputCode.text = nGold.ToString();
            //Regex reg = new Regex("^[1-9]\d*$");
            //if (reg.IsMatch(strInputInfo))
            //{
            //    txtCanGold.text = strInputInfo;
            //}
            //else
            //{
            //    if (txtCanGold.text == "")
            //    {
            //        txtCanGold.text = "";
            //    }
            //    else
            //    {
            //        txtCanGold.text = strInputInfo.Substring(0, strInputInfo.Length - 1);
            //    }
            //}
        });
        this.pagePagCashOut.SetActive(true);
        this.PageCashOutRecord.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceClub.GetCashOutInfo, new object[] {});
    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {
        ToggleCashOut.isOn = true;
        ToggleCashOutRecord.isOn = false;
    }

    public override void OnRemoveStart()
    {

    }

    private void ClearList()
    {
        int childCount = parentCashOutRecord.childCount;
        if (childCount != 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(parentCashOutRecord.GetChild(i).gameObject);
            }
        }
    }

    public void getCashOutInfoCallBack(Hashtable data)
    {
        string strResult = data["success"].ToString();
        if(strResult == "1")
        {
            txtCanGold.text = data["userBalance"].ToString();
        }
       
        //strURL = data["download_url"].ToString();
        //txtCode.text = strCode;
    }
    public void getCashOutRecordCallBack(Hashtable data)
    {
        ClearList();
        ArrayList withList = data["raisingBeansLogs"] as ArrayList;
        Object objItem = Resources.Load("HallItem/CashOutRecordItem");
        for (int i = 0; i < withList.Count; i++)
        {
            GameObject go = Instantiate(objItem) as GameObject;
            go.transform.SetParent(parentCashOutRecord);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero; //SetInfo

            Hashtable ht = withList[i] as Hashtable;
            CashOutRecordItem cashOutRecordItem = go.AddComponent<CashOutRecordItem>();
            cashOutRecordItem.SetInfo( ht["bean"].ToString(), ht["state"].ToString(),ht["updateTime"].ToString(), "" );
        }
    }
    public void getCashOutCallBack(Hashtable data)
    {
        string strResult = data["success"].ToString();
        if (strResult == "1")
        {
            
            txtCanGold.text = data["userBalance"].ToString();
            PopupCommon.GetSingleton().ShowView(data["message"].ToString());
        }
        
        //strCode = data["code"].ToString();
        //strURL = data["download_url"].ToString();
        //txtCode.text = strCode;
    }

    



}
