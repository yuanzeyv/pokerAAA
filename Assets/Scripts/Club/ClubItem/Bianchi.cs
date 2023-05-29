using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bianchi : MonoBehaviour {
    public  Text mtest;
	// Use this for initialization
	void Awake () {
        mtest = transform.Find("value").GetComponent<Text>();
        this.mtest.text = "";
       // this.gameObject.SetActive(false);
    }
	
	public void  showMsg(string msg)
    {
        Debug.Log(this.gameObject.name);
        this.mtest.text = msg;
        this.gameObject.SetActive(true);
    }
    public void hideGo()
    {
        this.mtest.text = "";
        this.gameObject.SetActive(false);
    }
}
