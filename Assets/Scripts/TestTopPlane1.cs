using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestTopPlane1 : BasePlane
{

    private Button btnBack;
    private Button btnOpen;

    private void Awake()
    {
        btnBack = transform.Find("BtnBack").gameObject.GetComponent<Button>();
        btnOpen = transform.Find("BtnOpen").gameObject.GetComponent<Button>();

        btnBack.onClick.AddListener(OnBtnBackClick);
        btnOpen.onClick.AddListener(OnBtnOpenClick);
    }

    private void OnBtnBackClick()
    {
        Test.GetSingleton().planeManager.RemoveTopPlane();
    }

    private void OnBtnOpenClick()
    {
        Test.GetSingleton().planeManager.AddTopPlane(Test.GetSingleton().planeTop2);
    }

    public override void OnAddStart()
    {
        Debug.Log("PlaneTop1开始加入");
    }

    public override void OnAddComplete()
    {
        Debug.Log("PlaneTop1完成加入");
    }

    public override void OnRemoveStart()
    {
        Debug.Log("PlaneTop1开始移除");
    }

    public override void OnRemoveComplete()
    {
        Debug.Log("PlaneTop1完成移除");
    }

}
