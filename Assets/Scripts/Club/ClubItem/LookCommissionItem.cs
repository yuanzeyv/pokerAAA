using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookCommissionItem : MonoBehaviour {
   
    public Text txtGameName;
    public Text txtYingLi;
	public Text txtCreateTime;
    public Text txtFenCheng;
  
    public string userid;

    void Awake()
    {
        txtGameName = transform.Find("GameName").GetComponent<Text>();
        txtYingLi = transform.Find("YingLi").GetComponent<Text>();
        txtCreateTime = transform.Find("CreateTime").GetComponent<Text>();
        txtFenCheng = transform.Find("FenCheng").GetComponent<Text>();
     

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //public void GetSprtie(Sprite s)
    //{
    //    clubHead.sprite = s;
    //}

    public void SetInfo(string strName, string strYingLi, string strTime, string strFenCheng)
    {
        switch(strName)
        {
            case "0":
                {
                    txtGameName.text = "德州扑克";
                    break;
                }
            case "1":
                {
                    txtGameName.text = "奥马哈";
                    break;
                }
            case "2":
                {
                    txtGameName.text = "短牌";
                    break;
                }
        }
       
       
        txtYingLi.text = "盈利:" + strYingLi;
        txtCreateTime.text = "时间:" + strTime;
        txtFenCheng.text = "分成:" + strFenCheng;

    }
 
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
