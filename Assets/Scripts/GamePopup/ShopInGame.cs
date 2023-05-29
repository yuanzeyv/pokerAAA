using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInGame : MonoBehaviour {
    public Button back1;
    public Button back2;
    
    // Use this for initialization
    void Awake() {

        back1 = transform.Find("ShopDiamondTopPanel/Top/Back").GetComponent<Button>();
        back1.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            gameObject.SetActive(false);
//            TouchMove.Instance().isRun = true;
        });

        back2 = transform.Find("ShopGoldTopPanel/Top/Back").GetComponent<Button>();
        back2.onClick.AddListener(()=> {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            gameObject.SetActive(false);
//            TouchMove.Instance().isRun = true;
        });

    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//        TouchMove.Instance().isRun = false;
    }
}
