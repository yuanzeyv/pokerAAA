using UnityEngine;
using System.Collections;
using System;

public class TouchMove : MonoBehaviour
{
    private static TouchMove _instance;

    public static TouchMove Instance()
    {
        return _instance;
    }
    enum InputDirection { Null, Up, Down, Left, Right };
    public enum ActionType { Left, Right, Up, Down };
    private InputDirection inputDirection = InputDirection.Null;//当前滑动方向
    private bool activeInput = false;
    private Vector3 mousePos = Vector3.zero;

    public float timer = 0;
    public float time = 0.5f;
    public bool isRun = true;


    public Action LeftAction;
    public Action RightAction;
    public Action UpAction;
    public Action DownAction;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (isRun)
        {
            GetInputDirection();
        }
    }

    void GetInputDirection()
    {
        inputDirection = InputDirection.Null;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            timer = 0;
        }
        if (Input.GetMouseButton(0) && activeInput)
        {
            timer += Time.deltaTime;
            if (timer>= time)
            {
                return;
            }
            Vector3 vec = Input.mousePosition - mousePos;
            if (vec.magnitude > 50)
            {
                var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector3.up)) * Mathf.Rad2Deg;
                var angleX = Mathf.Acos(Vector3.Dot(vec.normalized, Vector3.right)) * Mathf.Rad2Deg;
                if (angleY <= 25)
                {
                    inputDirection = InputDirection.Up;
                    //Debug.Log("上");
                    if (UpAction!=null)
                    {
                        UpAction();
                      //  Debug.Log("上执行");
                    }
                }
                else if (angleY >= 155)
                {
                    inputDirection = InputDirection.Down;
                   // Debug.Log("下");
                    if (DownAction!=null)
                    {
                        DownAction();
                    //    Debug.Log("下执行");
                    }
                }
                else if (angleX <= 25)
                {
                    inputDirection = InputDirection.Right;
                    Debug.Log("右");
                    if (RightAction!=null)
                    {
                        RightAction();
                        Debug.Log("右执行");
                    }
                }
                else if (angleX >= 155)
                {
                    inputDirection = InputDirection.Left;
                    Debug.Log("左");
                    if (LeftAction!=null)
                    {
                        LeftAction();
                        Debug.Log("左执行");
                    }
                }
                //if(Mathf.Abs(vec.x)> Mathf.Abs(vec.y)&& vec.x>0)
                //    inputDirection = InputDirection.Right;
                //else if(Mathf.Abs(vec.x) > Mathf.Abs(vec.y) && vec.x< 0)
                //    inputDirection = InputDirection.Left;
                //else if(Mathf.Abs(vec.x) <Mathf.Abs(vec.y) && vec.y >0)
                //    inputDirection = InputDirection.Up;
                //else if (Mathf.Abs(vec.x) < Mathf.Abs(vec.y) && vec.y <0)
                //    inputDirection = InputDirection.Down;
                activeInput = false;
            }
        }
    }
    public void AddFunction(ActionType actionType, Action action)
    {
        switch (actionType)
        {
            case ActionType.Up:
                UpAction = action;
                break;
            case ActionType.Down:
                DownAction = action;
                break;
            case ActionType.Left:
                LeftAction = action;
                break;
            case ActionType.Right:
                RightAction = action;
                break;
        }
    }
    public void RemoveFunction(){
        UpAction = null;
        DownAction = null;
        LeftAction = null;
        RightAction = null;
    }
}
