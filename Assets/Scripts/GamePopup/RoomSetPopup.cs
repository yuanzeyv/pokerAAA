using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomSetPopup : BasePopup {

    public Button closeBtn;
    public Button[] deskBtns;
    public Button[] cardStyleBtns;

    public Toggle[] addToggles;
    public Button[] addTypeBtn;

    public Toggle musicToggle;

    public int betType; // 0- 1/4, 1- 1/3 ,2-1/2 ,3-2/3,4-3/4,5-1x,6-1.5x,7-min,8-allin

    public List<string> dichi=new List<string>(); //1/3底池 1/2底池 2/3底池 1倍底池

    public  static string  cardPath="pai/1/poker_";

    void Awake() {

        Init();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        deskBtns = transform.Find("bg1/desk").GetComponentsInChildren<Button>();
        cardStyleBtns = transform.Find("bg3/card").GetComponentsInChildren<Button>();
        addToggles = transform.Find("bg2").GetComponentsInChildren<Toggle>();
        addTypeBtn= transform.Find("bg2/Content").GetComponentsInChildren<Button>();
        musicToggle= transform.Find("musicTog").GetComponent<Toggle>();

        closeBtn.onClick.AddListener(()=> { 
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton()._myController.SetDiRatio();
            HideView(); 
        });

        for(int i = 0; i< 4; i++) {
            int index = i;

            deskBtns[index].onClick.AddListener(()=> {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                ChooseDesk(index);
            });
        }

        for (int i = 0; i < 3; i++)
        {
            int index = i;

            cardStyleBtns[index].onClick.AddListener(() => {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                ChooseCard(index);
            });
        }

        if (PlayerPrefs.HasKey("isMusic"))
        {
            StaticData.isMusic = PlayerPrefs.GetInt("isMusic") == 1 ? true : false;
            musicToggle.isOn = PlayerPrefs.GetInt("isMusic") == 1 ? true : false;
        }
        musicToggle.onValueChanged.AddListener((bool b)=> {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            StaticData.isMusic = musicToggle.isOn;
            PlayerPrefs.SetInt("isMusic", musicToggle.isOn ? 1 : 0); 
            
           
        });

        dichi.Add("1/4");
        dichi.Add("1/3");
        dichi.Add("2/3");
        dichi.Add("1X");


        for (int i = 0; i < 9; i++)
        {
           int index = i;

            addTypeBtn[index].onClick.AddListener(() => {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                betType = index;
                string type="";
                string typeText = "";

                switch (betType) {
                    case 0:
                        type = "1/4";
                        typeText = "1/4\n底池";
                        break;
                    case 1:
                        type = "1/3";
                        typeText = "1/3\n底池";
                        break;
                    case 2:
                        type = "1/2";
                        typeText = "1/2\n底池";
                        break;
                    case 3:
                        type = "2/3";
                        typeText = "2/3\n底池";
                        break;
                    case 4:
                        type = "3/4";
                        typeText = "3/4\n底池";
                        break;
                    case 5:
                        type = "1X";
                        typeText = "1倍\n底池";
                        break;
                    case 6:
                        type = "1.5X";
                        typeText = "1.5倍\n底池";
                        break;
                    case 7:
                        type = "最小加注";
                        typeText = "最小加注";
                        break;
                    case 8:
                        type = "Allin";
                        typeText = "Allin";
                        break;

                }


                if (addToggles[0].isOn) {
                    if (ChangeString(type) == 99)
                    {
                        dichi[0] = type;
                        addToggles[0].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    else {
                        int num = ChangeString(type);
                        dichi[num] = dichi[0];
                        dichi[0] = type;
                        addToggles[num].transform.GetChild(1).GetComponent<Text>().text= addToggles[0].transform.GetChild(1).GetComponent<Text>().text;
                        addToggles[0].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    PlayerPrefs.SetString("AddZhu0",dichi[0]);
                    PlayerPrefs.SetString("typeText0",typeText);
                }
                if (addToggles[1].isOn)
                {
                    if (ChangeString(type) == 99)
                    {
                        dichi[1] = type;
                        addToggles[1].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    else
                    {
                        int num = ChangeString(type);
                        dichi[num] = dichi[1];
                        dichi[1] = type;
                        addToggles[num].transform.GetChild(1).GetComponent<Text>().text = addToggles[1].transform.GetChild(1).GetComponent<Text>().text;
                        addToggles[1].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    PlayerPrefs.SetString("AddZhu1",dichi[1]);
                    PlayerPrefs.SetString("typeText1",typeText);
                }
                if (addToggles[2].isOn)
                {
                    if (ChangeString(type) == 99)
                    {
                        dichi[2] = type;
                        addToggles[2].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    else
                    {
                        int num = ChangeString(type);
                        dichi[num] = dichi[2];
                        dichi[2] = type;
                        addToggles[num].transform.GetChild(1).GetComponent<Text>().text = addToggles[2].transform.GetChild(1).GetComponent<Text>().text;
                        addToggles[2].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    PlayerPrefs.SetString("AddZhu2",dichi[2]);
                    PlayerPrefs.SetString("typeText2",typeText);
                }
                if (addToggles[3].isOn)
                {
                    if (ChangeString(type) == 99)
                    {
                        dichi[3] = type;
                        addToggles[3].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    else
                    {
                        int num = ChangeString(type);
                        dichi[num] = dichi[3];
                        dichi[3] = type;
                        addToggles[num].transform.GetChild(1).GetComponent<Text>().text = addToggles[3].transform.GetChild(1).GetComponent<Text>().text;
                        addToggles[3].transform.GetChild(1).GetComponent<Text>().text = typeText;
                    }
                    PlayerPrefs.SetString("AddZhu3",dichi[3]);
                    PlayerPrefs.SetString("typeText3",typeText);
                }
                Debug.Log(dichi[0]+"  "+ dichi[1] + "  "+dichi[2] + "  "+dichi[3] + "  ");
                GameManager.GetSingleton().betRatio = dichi;
                
            });
        }

        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("AddZhu" + i))
            {
                if (ChangeString(PlayerPrefs.GetString("AddZhu" + i)) == 99)
                {
                    dichi[i] = PlayerPrefs.GetString("AddZhu" + i);
                    addToggles[i].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("typeText" + i);
                }
                else
                {
                    int num = ChangeString(PlayerPrefs.GetString("AddZhu" + i));
                    dichi[num] = dichi[i];
                    dichi[i] = PlayerPrefs.GetString("AddZhu" + i);
                    addToggles[num].transform.GetChild(1).GetComponent<Text>().text = addToggles[i].transform.GetChild(1).GetComponent<Text>().text;
                    addToggles[i].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("typeText" + i);
                }
            }
        }

        ChooseDesk(PlayerPrefs.GetInt("bg",99));
        ChooseCard(PlayerPrefs.GetInt("cardsNum",0));
        

        gameObject.SetActive(false);
    }

    public int  ChangeString(string  type ) {
        int index = 99;
        for (int i = 0; i < dichi.Count; i++) {
            if (dichi[i] == type) {
                index = i;
            }
        }
        return index;
    }


    //chooseDesk
    public void ChooseDesk(int deskNum) {
        if (deskNum == 99) {
            deskNum = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            deskBtns[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        deskBtns[deskNum].transform.GetChild(0).gameObject.SetActive(true);
        if (GameUIManager.GetSingleton() != null)
        {
            GameUIManager.GetSingleton().transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("newimg/paizhuo" + (deskNum + 1));
        }
        else {
            GameObject.Find("BaseCanvas/Canvas/bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("newimg/paizhuo" + (deskNum + 1));
        }

        PlayerPrefs.SetInt("bg",deskNum);
    }

    //chooseDesk
    public void ChooseCard(int cardNum)
    {
        for (int i = 0; i < 3; i++)
        {
            cardStyleBtns[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        cardStyleBtns[cardNum].transform.GetChild(0).gameObject.SetActive(true);

        // GameUIManager.GetSingleton().transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" + (deskNum + 1));
        cardPath = "pai/"+ (cardNum+1)   +"/poker_";
        Debug.Log(cardPath);
        PlayerPrefs.SetInt("cardsNum",cardNum);
    }
}
