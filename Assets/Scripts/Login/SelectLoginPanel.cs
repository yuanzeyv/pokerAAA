using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class SelectLoginPanel : BasePlane {

    private Image Icon;
    private Button phoneLogin;
    private Button wechatLogin;

    private void Awake()
    {
        Icon = transform.Find("Icon").GetComponent<Image>();
        //phoneLogin = transform.Find("PhoneLogin").GetComponent<Button>();
        //wechatLogin = transform.Find("ChatLogin").GetComponent<Button>();

        //phoneLogin.onClick.AddListener(()=> {
        //    LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().passwordLoginPanel);
        //});
        //wechatLogin.onClick.AddListener(() => {
        //    WechatLogin.GetSingleton().OnAuthClick();
        //});
    }

    void Start ()
    {
        Invoke("Anim",0.5f);
	}
	
	void Update ()
    {
	
	}

    private void Anim()
    {
        Icon.transform.DOLocalMoveY(280, 2).OnComplete(() => {
            //phoneLogin.gameObject.SetActive(true);
            //wechatLogin.gameObject.SetActive(true);
            //phoneLogin.GetComponent<Image>().DOFade(1, 2);
            //wechatLogin.GetComponent<Image>().DOFade(1, 2);
            LoginManager.GetSingleton().planeManager.AddTopPlane(LoginManager.GetSingleton().passwordLoginPanel);
        });
    }

    public override void OnAddStart()
    {

    }

    public override void OnAddComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }

    public override void OnRemoveComplete()
    {

    }
}
