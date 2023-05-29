using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TheGamePaiTopPanel : BasePlane {

    private Button back;
    private Text count;

    private ChatScroller chatScroller;

    public string id;

    private const int showPage = 10;
    private int currPage = 1;
    private int sumPage = 1;

    private Hashtable data;
    List<Hashtable> datas;

    private void Awake()
    {
        chatScroller = transform.Find("ChatScroller").GetComponent<ChatScroller>();
        back = transform.Find("Top/Back").GetComponent<Button>();
        count = transform.Find("Top/Count").GetComponent<Text>();

        back.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });
    }

    void Start()
    {

    }

    void Update()
    {

    }

    //下拉刷新
    public void SetReflash()
    {
        //if (currPage <= sumPage)
        //{
            currPage++;
            NetMngr.GetSingleton().Send(InterfaceMain.GamePai, new object[] { id, currPage, showPage });
        //}

    }
    public void SetData(Hashtable data)
    {
        sumPage = int.Parse(data["allPage"].ToString());
        ArrayList list = data["GamePaiData"] as ArrayList;
        count.text="共"+data["shoushu"].ToString()+"手";
        datas = new List<Hashtable>();
        for (int i = 0; i < list.Count; i++)
        {
            datas.Add(new Hashtable());
            datas[i]["Type"] = 5;
//            string[] strss = ((Hashtable)list[i])["paiXing"].ToString().Split('|');
			datas[i]["Pai"] = ((Hashtable)list[i])["paiXing"].ToString();
//            datas[i]["Pai1"] = strss[0];
//            datas[i]["Pai2"] = strss[1];
            string str = "第" + ((Hashtable)list[i])["shouIndex"].ToString() + "手";
            switch (((Hashtable)list[i])["sign"].ToString())
            {
                case "1":
                    str += "(盈利最多牌型)";
                    break;
                case "2":
                    str += "(亏损最多牌型)";
                    break;
                case "3":
                    str += "(所弃最大牌型)";
                    break;
                case "4":
                    str += "(亏损最大牌型)";
                    break;
            }
            datas[i]["Title"] = str;
            datas[i]["Chouma"] = ((Hashtable)list[i])["chouma"].ToString();
            string[] strss1 = ((Hashtable)list[i])["time"].ToString().Split(' ');
            string[] strs1 = strss1[0].Split('-');
            string[] strs2 = strss1[1].Split(':');
            datas[i]["Time"] = strs1[1] + "/" + strs1[2] + "/" + strs2[0] + ":" + strs2[1];
            datas[i]["Score"] = ((Hashtable)list[i])["score"].ToString();
            datas[i]["ID"]= ((Hashtable)list[i])["id"].ToString();
        }
        if (currPage == 1)
        {
            chatScroller.ClearList();
            chatScroller.datas.Clear();
            chatScroller.modelList.Clear();
            chatScroller.SetDatas(datas);
        }
        else
        {
            chatScroller.SetDatas(0, datas);
        }
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.GamePai, new object[] { id, currPage, showPage });
    }

    public override void OnAddStart()
    {
        currPage = 1;
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
