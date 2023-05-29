using UnityEngine;
using System.Collections;

public class First : MonoBehaviour {



    private void Awake()
    {
        Input.multiTouchEnabled = false;
        ShowBar.statusBarState = ShowBar.States.TranslucentOverContent;
    }
	void Start ()
    {
        StaticFunction.Getsingleton().ChangeScene("Login");
	}
	
	void Update ()
    {
	
	}
}
