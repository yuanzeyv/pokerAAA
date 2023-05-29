using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class managerItem : MonoBehaviour {

    public RawImage managerhead;
    public Text managerName;

   
    public string id;


    void Awake()
    {
        managerhead = transform.Find("Mask/head").GetComponent<RawImage>();
        managerName = transform.Find("managerName").GetComponent<Text>();

        GetComponent<Button>().onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            ClubManager.GetSingleton().commonPopup.ShowView("是否取消管理员？",null,true,()=> {
                NetMngr.GetSingleton().Send(InterfaceClub.SetManager, new object[] { ClubManager.GetSingleton().clubPanel.clubId, id, "0" });
            });

        });

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DelSelf()
    {
        Destroy(gameObject);
    }
    /*
    public void GetSprtie(Sprite s)
    {
        managerhead.sprite = s;
    }*/
    public void GetSprtie(Texture s)
    {
        managerhead.texture = s;
    }


    public void SetInfo(string url, string id, string name)
    {


        GameTools.GetSingleton().GetTextureNet(url, GetSprtie);
        this.id = id;
        managerName.text = name;



      

    }

}
