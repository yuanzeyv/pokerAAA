using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zengsongItem : MonoBehaviour {

    private Text title;
    private Text time;

    private void Awake()
    {
        title = transform.Find("title").GetComponent<Text>();
        time = transform.Find("time").GetComponent<Text>();
    }

    public void SetData(Hashtable data, int type)
    {
        string name = "";
        switch(type){
            case 1:
                name = "积分";
                break;

            case 2:
                name = "钻石";
                break;

            case 3:
            case 4:
                name = "联盟币";
                break;
        }

        string action = " 赠送给 ";
        if(type == 4){
            action = " 回收 ";
        }

        title.text = data["name"].ToString() + action + data["name2"].ToString() +" "+ data["count"].ToString() + "个" + name;
        time.text = data["time"].ToString();
    }
}
