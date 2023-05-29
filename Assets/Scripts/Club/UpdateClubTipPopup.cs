using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateClubTipPopup : BasePopup
{
    public Button cancelBtn;
    public Button buyBtn;

    void Awake()
    {
        Init();

        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();
        buyBtn = transform.Find("BuyBtn").GetComponent<Button>();

        gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
