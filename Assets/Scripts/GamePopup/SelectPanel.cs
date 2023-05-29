using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectPanel : BasePopup {

	public Button closeBtn;
	public Button okBtn;
	public Text desText;

	public Transform content;

	public Toggle[] addToggles;

	public Image cardBack1;
	public Image cardBack2;
	public Image cardBack3;

	public RawImage[] showcards = new RawImage[3];
	public int maxNum;

	public int[] selectIndexs = new int[3];

	private Dictionary<int, SelectCardItem> cards = new Dictionary<int, SelectCardItem>();

	void Awake() {

		desText = transform.Find("Image/Text").GetComponent<Text>();
		okBtn = transform.Find("Button1").GetComponent<Button>();
		closeBtn = transform.Find("Button2").GetComponent<Button>();
		content = transform.Find("Scroll View/Viewport/Content");

		showcards[0] = transform.Find("card1").GetComponent<RawImage>();
		showcards[1] = transform.Find("card2").GetComponent<RawImage>();
		showcards[2] = transform.Find("card3").GetComponent<RawImage>();


		cardBack1 = transform.Find("Image1").GetComponent<Image>();
		cardBack2 = transform.Find("Image2").GetComponent<Image>();
		cardBack3 = transform.Find("Image3").GetComponent<Image>();

		closeBtn.onClick.AddListener(delegate {
			SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			GameUIManager.GetSingleton().selectPanel.gameObject.SetActive (false);
		});

		okBtn.onClick.AddListener(delegate {
			SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
			bool isOk = true;
			string value = "";
			if(maxNum == 1)
			{
				if(selectIndexs[0] == int.MaxValue)//没选
				{
					isOk = false;
				}
				else
				{
					value = selectIndexs[0].ToString();
				}

			}

			if(maxNum == 3)
			{
				if(selectIndexs[0] == int.MaxValue || selectIndexs[1] == int.MaxValue || selectIndexs[2] == int.MaxValue )//没选
				{
					isOk = false;
				}
				else
				{
					value = string.Format("{0}|{1}|{2}", selectIndexs[0], selectIndexs[1], selectIndexs[2]);
				}
			}
			if(isOk)
			{
				NetMngr.GetSingleton().Send(InterfaceGame.OnSelectCard, new object[] { value });
			}

			GameUIManager.GetSingleton().selectPanel.gameObject.SetActive (false);
		});

		for (int i = 0; i < 3; i++) {
			showcards[i].gameObject.SetActive (false);
		}

		cardBack1.gameObject.SetActive (false);
		cardBack2.gameObject.SetActive (false);
		cardBack3.gameObject.SetActive (false);

	}

	void ClearList()
	{
		int childCount = content.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Destroy(content.GetChild(i).gameObject);
		}

		for (int i = 0; i < maxNum; i++) {
			selectIndexs [i] = int.MaxValue;
		}

		for (int i = 0; i < 3; i++) {
			showcards[i].gameObject.SetActive (false);
		}
	}

	public void showCards(int num, string list)
	{
		maxNum = num;
		ClearList ();
		initShow (num);
		desText.text = "请选择" + num.ToString () + "张牌";
		string[] strs = list.Split('|');
		Object objItem = Resources.Load("HallItem/SelectCardItem");
		for (int i = 0; i < strs.Length; i++)
		{
			GameObject go = Instantiate(objItem) as GameObject;
			go.transform.SetParent(content);
			go.transform.localScale = Vector3.one;
			go.transform.localPosition = Vector3.zero;
			SelectCardItem item = go.AddComponent<SelectCardItem>();
			item.SetData(strs[i]);
			cards[item.cIndex] = item;
		}
	}

	public void initShow(int num)
	{
		if (num == 1) {
			cardBack1.gameObject.SetActive (true);
			cardBack2.gameObject.SetActive (false);
			cardBack3.gameObject.SetActive (false);
		} 
		else 
		{ 
			cardBack1.gameObject.SetActive (true);
			cardBack2.gameObject.SetActive (true);
			cardBack3.gameObject.SetActive (true);
		}
	}

	public void ChooseCard(int cardNum)
	{
		if (cardNum == int.MinValue)
			return;
		
		for (int i = 0; i < maxNum; i++) {
			if (selectIndexs [i] == int.MaxValue) {
				selectIndexs [i] = cardNum;
				FreshShowCard();
				return;
			}
		}

		//已经选择，替换第一个
		int yetCardNum = selectIndexs [0];
		selectIndexs [0] = cardNum;
		cards [yetCardNum].item.isOn = false;
		FreshShowCard ();

	}

	public void DisChooseCard(int cardNum)
	{
		if (cardNum == int.MinValue)
			return;
		
 		for (int i = 0; i < maxNum; i++) {
			if (selectIndexs [i] == cardNum) {
				selectIndexs [i] = int.MaxValue;
				FreshShowCard();
				return;
			}
		}
	}

	public void FreshShowCard()
	{
		for (int i = 0; i < maxNum; i++) {

			if (selectIndexs [i] == int.MaxValue) {
				showcards [i].gameObject.SetActive (false);
			} else {

				showcards[i].gameObject.SetActive (true);
				showcards[i].texture = StaticFunction.Getsingleton().SetPai(selectIndexs [i].ToString());
			}
		}
	}



}
