using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCardItem : MonoBehaviour {

	public Toggle item;
	private RawImage card;
	public int cIndex = int.MinValue;

    private void Awake()
    {
		item = this.GetComponent<Toggle>();
		card = transform.Find("Background").GetComponent<RawImage>();
 		item.onValueChanged.AddListener(
						
			(isOn)=>{
				SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
				if (item.isOn)
				{
					GameUIManager.GetSingleton().selectPanel.ChooseCard(cIndex);
				}
				else
				{
					GameUIManager.GetSingleton().selectPanel.DisChooseCard(cIndex);
				}

			});
    }



    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData(string data)
    {
		cIndex = int.Parse (data);
		card.texture = StaticFunction.Getsingleton().SetPai(data);
    }
}
