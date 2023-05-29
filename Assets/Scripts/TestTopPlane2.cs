using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestTopPlane2 : BasePlane {

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
        Test.GetSingleton().planeManager.AddSidePlane(Test.GetSingleton().sidePlane, transform, SidePlaneDirection.LEFT, 200);
    }

    public override void OnAddStart()
    {
        Debug.Log("PlaneTop2开始加入");
    }

    public override void OnAddComplete()
    {
        Debug.Log("PlaneTop2完成加入");
    }

    public override void OnRemoveStart()
    {
        Debug.Log("PlaneTop2开始移除");
    }

    public override void OnRemoveComplete()
    {
        Debug.Log("PlaneTop2完成移除");
    }

}
