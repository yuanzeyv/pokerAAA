using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LHWJ : MonoBehaviour {

    private RawImage[] lHWJ;
    private Text lhwjName;

    private void Awake()
    {
        lHWJ = transform.Find("Pai").GetComponentsInChildren<RawImage>();
        lhwjName = GetComponent<Text>();
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SetData(Hashtable data)
    {
        string[] lhwj = data["failPlayerCards"].ToString().Split('|');
        lHWJ[0].texture = StaticFunction.Getsingleton().SetPai(lhwj[0]);
        lHWJ[1].texture = StaticFunction.Getsingleton().SetPai(lhwj[1]);
        lhwjName.text = "落后玩家:"+data["failPlayer"].ToString();
    }
}
