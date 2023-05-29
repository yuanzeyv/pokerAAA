using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSidePlane : BasePlane {

    private void Awake()
    {
        
    }

    public override void OnAddStart()
    {
        Debug.Log("SidePlane开始加入");
    }

    public override void OnAddComplete()
    {
        Debug.Log("SidePlane完成加入");
    }

    public override void OnRemoveStart()
    {
        Debug.Log("SidePlane开始移除");
    }

    public override void OnRemoveComplete()
    {
        Debug.Log("SidePlane完成移除");
    }
}
