using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardRecordPanel : MonoBehaviour {

    public Button backBtn;
    public Transform itemContent;

    void Awake() {
        backBtn = transform.Find("BackBtn").GetComponent<Button>();

        itemContent = transform.Find("Scroll View/Viewport/Content");

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
