using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardItem : MonoBehaviour {


    private RawImage[] cards = new RawImage[4];
    private Text playername;
    private Text count;

    private void Awake()
    {
        cards = GetComponentsInChildren<RawImage>();
        playername = transform.Find("name").GetComponent<Text>();
        count = transform.Find("count").GetComponent<Text>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData(Hashtable data)
    {
        string[] strs = data["pai"].ToString().Split('|');
		if (strs.Length == 2) {
			cards[0].texture = StaticFunction.Getsingleton().SetPai(strs[0]);
			cards[2].texture = StaticFunction.Getsingleton().SetPai(strs[1]);

			cards [1].gameObject.SetActive (false);
			cards [3].gameObject.SetActive (false);
		}

		if (strs.Length == 4) {
			cards [1].gameObject.SetActive (true);
			cards [3].gameObject.SetActive (true);
			for (int i = 0; i < strs.Length; i++)
			{
				cards[i].texture = StaticFunction.Getsingleton().SetPai(strs[i]);
			}
		}
      
        playername.text = data["name"].ToString();
        count.text = data["fanChaoPai"].ToString()+"张";
    }
}
