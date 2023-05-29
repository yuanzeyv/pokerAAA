using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestBottomPlane1 : BasePlane
{

    private Button btnOpen;

    private void Awake()
    {
        btnOpen = transform.Find("BtnOpen").gameObject.GetComponent<Button>();
        btnOpen.onClick.AddListener(OnBtnOpenClick);
    }

    private void OnBtnOpenClick()
    {
        Test.GetSingleton().planeManager.AddTopPlane(Test.GetSingleton().planeTop1);
        //Handheld.Vibrate();
    }

    public override void OnAddStart()
    {
        Debug.Log("PlaneBottom1开始加入");
    }

    public override void OnAddComplete()
    {
        Debug.Log("PlaneBottom1完成加入");
    }

    public override void OnRemoveStart()
    {
        Debug.Log("PlaneBottom1开始移除");
    }

    public override void OnRemoveComplete()
    {
        Debug.Log("PlaneBottom1完成移除");
    }

}
