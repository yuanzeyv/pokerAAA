using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewerItem : MonoBehaviour {

    public RawImage head;
    public Text viewerName;

    void Awake() {
        head = transform.Find("head/Head").GetComponent<RawImage>();
        viewerName = transform.Find("name").GetComponent<Text>();

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GetTexture(Texture t)
    {
        head.texture = t;
    }

    public string SetName(string n)
    {
        string str = "";
        if (n.Length > 4)
        {
            str = n.Substring(0, 4) + "..";

        }
        else
        {
            str = n;
        }
        return str;
    }

    public void SetInfo(string  headUrl,string viewerName) {
        this.viewerName.text = SetName(viewerName);
        GameTools.GetSingleton().GetTextureNet(headUrl, GetTexture);

    }

    public void DelSelf()
    {
        Destroy(gameObject);
    }

}
