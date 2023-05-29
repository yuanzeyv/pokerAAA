using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Waitting : MonoBehaviour 
{
    public static Waitting _instance;
    public static Waitting getInstance()
    {
        return _instance;
    }
    Transform waite;
	// Use this for initialization
	void Awake () 
    {
        _instance = this;
        Hide();
        waite = transform.Find("Image");
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    public void Show()
    {
        if (StaticData.selectScene == 2)
        {
            waite.gameObject.SetActive(true);
        }
        else
        {
            waite.gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
    }
}
