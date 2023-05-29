using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeadItem : MonoBehaviour {
    public RawImage head;
  

    void Awake() {
        head = transform.Find("Image").GetComponent<RawImage>();
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetHead(string  url) {

    //    GameTools.GetSingleton().GetTextureFromNet(url, GetHead);
    }

/*
    public void GetHead(Sprite s)
    {
        if(s != null)
        {
            head.sprite = s;

        }

            
       
    }
    */
    public void GetHead(Texture s)
    {
        if (s != null)
        {
            head.texture = s;

        }
    }
    public void DelSelf()
    {
        Destroy(gameObject);
    }


}
