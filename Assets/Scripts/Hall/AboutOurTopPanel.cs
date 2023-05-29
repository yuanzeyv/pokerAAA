using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AboutOurTopPanel : BasePlane {


    private Button backBtn;
    private Text content;

    private void Awake()
    {
        backBtn = transform.Find("Top/Back").GetComponent<Button>();
        content = transform.Find("Message").GetComponent<Text>();

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            HallManager.GetSingleton().planeManager.RemoveTopPlane();
        });

    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public override void OnAddComplete()
    {

    }

    public override void OnAddStart()
    {

    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
}
