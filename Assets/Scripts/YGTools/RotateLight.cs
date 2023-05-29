using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RotateLight : MonoBehaviour {

    public int persionCount=6;
    public float speed=5;
    public List<Transform> vectorss;

    private Vector3 currentPos=Vector3.zero;
    private Vector3 nextPos= Vector3.zero;

    private LineRenderer line;

    private bool isStart = false;
    public int i = 1;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        vectorss = new List<Transform>();
    }

	void Start ()
    {
        line.SetPosition(0,Vector3.zero);
    }
	
	void Update ()
    {
        if (isStart)
        {
            Vector3 temp = Vector3.Lerp(currentPos,nextPos,speed*Time.deltaTime);
            line.SetPosition(1, temp);
            currentPos = temp;
            if ((currentPos-nextPos).sqrMagnitude<0.001f)
            {
                isStart = false;
                line.SetPosition(1, nextPos);
                currentPos = nextPos;
                nextPos = Vector3.zero;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetNextPosition(new Vector3(vectorss[i % persionCount].position.x, vectorss[i % persionCount].position.y, 0) * 2);
            i++;
            if (i%persionCount==1)
            {
                i = 1;
            }
        }
	}

    public void SetNextPosition(Vector3 vector3)
    {
        nextPos = vector3;
        isStart = true;
    }
}
