using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ItemPai : MonoBehaviour {
     Sprite[] paiSprite;
    RectTransform paiRT;
    Image paiimag;
	// Use this for initialization
	void Awake () {
        if (paiSprite == null)
            paiSprite = new Sprite[55];
        paiRT = this.GetComponent<RectTransform>();
        paiimag = this.GetComponent<Image>();
	}
	public void setPai(int index)
    {
        if(index<0 || index > 55)
        {
            return;
        }
        if (paiSprite[index] == null)
        {
            paiSprite[index] = Resources.Load<Sprite>("pai/Pai_"+index);
        }
        paiimag.sprite = paiSprite[index];
    }
}
