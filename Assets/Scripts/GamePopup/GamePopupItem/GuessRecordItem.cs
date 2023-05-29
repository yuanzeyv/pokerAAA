using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuessRecordItem : MonoBehaviour
{
    public Text guessText;
    public Text betCoinText;
    public Text winCoinText;

   
    void Awake()
    {
        guessText = transform.Find("guessTitle").GetComponent<Text>();
        betCoinText = transform.Find("betCoin").GetComponent<Text>();
        winCoinText = transform.Find("WinCoin").GetComponent<Text>();

       

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetInfo(string title, string bet, string win)
    {
        guessText.text = title;
        betCoinText.text = bet;
        winCoinText.text = win;




    }

    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
