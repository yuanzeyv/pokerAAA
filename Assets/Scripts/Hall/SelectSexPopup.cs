using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectSexPopup : BasePlane {

    private Button man;
    private Button woman;
    private Button cancel;
    private Button mask;
    

    private void Awake()
    {
        man = transform.Find("Man").GetComponent<Button>();
        woman = transform.Find("Woman").GetComponent<Button>();
        cancel = transform.Find("Cancel").GetComponent<Button>();
        mask = transform.Find("Mask").GetComponent<Button>();

        mask.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveSidePlane();
        });

        man.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.ModifierSex, new object[] {"1" });
        });
        woman.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            NetMngr.GetSingleton().Send(InterfaceMain.ModifierSex, new object[] {"0" });
        });
        cancel.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveSidePlane();
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
