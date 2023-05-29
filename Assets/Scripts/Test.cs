using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public TestBottomPlane1 planeBottom1;
    public TestBottomPlane2 planeBottom2;
    public TestTopPlane1 planeTop1;
    public TestTopPlane2 planeTop2;
    public TestSidePlane sidePlane;
    private Button btnToggle1;
    private Button btnToggle2;

    private RectTransform containerBottom;
    private RectTransform containerTop;

    public PlaneManager planeManager;

    private static Test singleton;

    public static Test GetSingleton()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        ShowBar.statusBarState = ShowBar.States.TranslucentOverContent;
        planeBottom1 = transform.Find("ContainerBottom/PlaneBottom1").gameObject.AddComponent<TestBottomPlane1>();
        planeBottom2 = transform.Find("ContainerBottom/PlaneBottom2").gameObject.AddComponent<TestBottomPlane2>();
        planeTop1 = transform.Find("ContainerTop/PlaneTop1").gameObject.AddComponent<TestTopPlane1>();
        planeTop2 = transform.Find("ContainerTop/PlaneTop2").gameObject.AddComponent<TestTopPlane2>();
        sidePlane = transform.Find("SidePlane").gameObject.AddComponent<TestSidePlane>();
        btnToggle1 = transform.Find("BtnToggle1").gameObject.GetComponent<Button>();
        btnToggle2 = transform.Find("BtnToggle2").gameObject.GetComponent<Button>();

        containerBottom = transform.Find("ContainerBottom").gameObject.GetComponent<RectTransform>();
        containerTop = transform.Find("ContainerTop").gameObject.GetComponent<RectTransform>();

        planeManager = GetComponent<PlaneManager>();
        planeManager.Init(containerBottom, containerTop);

        btnToggle1.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            OnBtnToggle1Click();
        });
        btnToggle2.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            OnBtnToggle2Click();
        });
    }

    private void Start()
    {
        planeBottom1.gameObject.SetActive(false);
        planeBottom2.gameObject.SetActive(false);
        planeTop1.gameObject.SetActive(false);
        planeTop2.gameObject.SetActive(false);
        sidePlane.gameObject.SetActive(false);

        //显示初始页面
        planeManager.ShowBottomPlane(planeBottom1);
    }

    private void OnBtnToggle1Click()
    {
        planeManager.ShowBottomPlane(planeBottom1);
    }

    private void OnBtnToggle2Click()
    {
        planeManager.ShowBottomPlane(planeBottom2);
    }

}
