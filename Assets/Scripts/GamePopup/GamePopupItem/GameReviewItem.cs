using System.CodeDom;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameReviewItem : MonoBehaviour
{

	public RawImage Head;//头像
    public Text playerName;//玩家名字
    public Text type;
    public GameObject big;//大盲
    public GameObject little;//小盲
    public GameObject cards;//我的手牌
    public Image[] cardsCard;//第一张牌

    public Text cardsInfo;//操作信息
    public GameObject fanpai;//翻牌
    public Image fanpaiCard1;
    public Image fanpaiCard2;
    public Image fanpaiCard3;
    public Text fanpaiInfo;
    public GameObject zhuanpai;//转牌
    public Image zhuanpaiCard1;
    public Text zhuanpaiInfo;
    public GameObject hepai;//河牌
    public Image hepaiCard1;
    public Text hepaiInfo;
    public Text score;
    public Text baoxian;
    public Text baoxian2;


	public int gametype;
	private int holdcount;

    void Awake()
    {
		Head = transform.Find("PlayerHead/mask/head").GetComponent<RawImage>();
        playerName = transform.Find("playerName").GetComponent<Text>();
        type = transform.Find("type").GetComponent<Text>();
        big = transform.Find("PlayerHead/big").gameObject;
        little = transform.Find("PlayerHead/little").gameObject;
        cards = transform.Find("cards").gameObject;
		cardsCard = transform.Find("cards").GetComponentsInChildren<Image>();
        cardsInfo = transform.Find("cards/info").GetComponent<Text>();
        fanpai = transform.Find("fanpai").gameObject;
        fanpaiCard1 = transform.Find("fanpai/card1").GetComponent<Image>();
        fanpaiCard2 = transform.Find("fanpai/card2").GetComponent<Image>();
        fanpaiCard3 = transform.Find("fanpai/card3").GetComponent<Image>();
        fanpaiInfo = transform.Find("fanpai/info").GetComponent<Text>();
        zhuanpai = transform.Find("zhuanpai").gameObject;
        zhuanpaiCard1 = transform.Find("zhuanpai/card1").GetComponent<Image>();
        zhuanpaiInfo = transform.Find("zhuanpai/info").GetComponent<Text>();
        hepai = transform.Find("hepai").gameObject;
        hepaiCard1 = transform.Find("hepai/card1").GetComponent<Image>();
        hepaiInfo = transform.Find("hepai/info").GetComponent<Text>(); 
        score = transform.Find("winorlose").GetComponent<Text>();
        baoxian = transform.Find("baoxian").GetComponent<Text>();
        baoxian2 = transform.Find("baoxian2").GetComponent<Text>();
    }

	public void GetTexture(Texture t)
    {
		Head.texture = t;
    }

    public void SetInfo(Hashtable data,string str)
    {
        if (!data.ContainsKey("type"))//如果还没坐下后端查不到 大小盲，直接删除自身
        {
            DelSelf();
            return;
        }

		gametype = GameUIManager.GetSingleton().gameType;
        if (gametype == 1)
        {
            this.cardsCard[2].gameObject.SetActive(true);
            this.cardsCard[3].gameObject.SetActive(true);
            holdcount = 4;
        }
        else
        {
            this.cardsCard[2].gameObject.SetActive(false);
            this.cardsCard[3].gameObject.SetActive(false);
            holdcount = 2;
        }
		string[] cards = data ["card"].ToString ().Split ('|');
        
        GameTools.GetSingleton().GetTextureFromNet(data["headUrl"].ToString(), GetTexture); //设置图像
        this.playerName.text = data["name"].ToString();//设置姓名

        if (data.ContainsKey("score"))
        {
            if ( int.Parse(data["score"].ToString())>0)//设置分数颜色
            {
                this.score.color = Color.red;
            }
            else 
            {
                this.score.color = Color.green;
            }
        
            this.score.text = data["score"].ToString();//设置总分
        }
        
        this.type.text = data["cardType"].ToString();//牌型

        
        if ((data["type"].ToString()=="0"))//大小盲
        {
            this.big.gameObject.SetActive(false);
        }
        else if((data["type"].ToString()=="1"))
        {
            this.little.gameObject.SetActive(false);
        }
        else
        {
            this.little.gameObject.SetActive(false);
            this.big.gameObject.SetActive(false);
        }

		for(int i = 0;i<cards.Length;i++)
		{
			if(cards[i] == "99")
				this.cardsCard[i].sprite = Resources.Load<Sprite>("pai/card_back_2");
			else
				this.cardsCard[i].sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + cards[i]);
		}

        if (data["baoxian2"].ToString()!="0"&&data["baoxian2"].ToString()!="")//转牌保险额
        {
            baoxian.text = "投保:" + data["baoxian2"].ToString();
        }
        else
        {
            baoxian.text = "";
        }

        if (data["baoxian"].ToString()!="0"&&data["baoxian"].ToString()!="")//河牌保险额
        {
            baoxian2.text = "投保:" + data["baoxian"].ToString();
        }
        else
        {
            baoxian2.text = "";
        }
        
        //当前玩家的每次操作和牌的显示
        ArrayList lists = data["ops"] as ArrayList;
        
        for (int i = 0; i < lists.Count; i++)
        {
            Hashtable ht = lists[i] as Hashtable;
            if (ht["opType"].ToString()=="1")//翻牌前
            {
                if (ht["type"].ToString()=="0")//如果弃牌
                {
					//for(int k = 0;k < holdcount;k++)
					//{
					//	this.cardsCard[k].sprite = Resources.Load<Sprite>("pai/card_back_2");
					//}
//
//                    this.cardsCard1.sprite = Resources.Load<Sprite>("pai/card_back_2");
//                    this.cardsCard2.sprite = Resources.Load<Sprite>("pai/card_back_2");
                    this.cardsInfo.text = "弃牌"+ht["score"].ToString();
                    this.fanpai.SetActive(false);
                    this.zhuanpai.SetActive(false);
                    this.hepai.SetActive(false);
                    return;
                }
                else
                {
                    //this.cardsCard1.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + data["card"].ToString().Split('|')[0]);
                    //this.cardsCard1.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + data["card"].ToString().Split('|')[1]);
                    if (str.Split('|')[0]=="99")
                    {
                        this.fanpaiCard1.sprite = Resources.Load<Sprite>("pai/card_back_2");
                    }
                    else
                    {
                        this.fanpaiCard1.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + str.Split('|')[0]);
                    }
                    if (str.Split('|')[1]=="99")
                    {
                        this.fanpaiCard2.sprite = Resources.Load<Sprite>("pai/card_back_2");
                    }
                    else
                    {
                        this.fanpaiCard2.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + str.Split('|')[1]);
                    }
                    if (str.Split('|')[2]=="99")
                    {
                        this.fanpaiCard3.sprite = Resources.Load<Sprite>("pai/card_back_2");
                    }
                    else
                    {
                        this.fanpaiCard3.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + str.Split('|')[2]);
                    }
                    
                    //this.fanpai.SetActive(false);
                    this.zhuanpai.SetActive(false);
                    this.hepai.SetActive(false);
                    
                    if (ht.ContainsKey("type"))
                    {
                        switch (ht["type"].ToString())
                        {
                            case "3"://看牌
                                this.cardsInfo.text = "看牌"+ht["score"].ToString();
                                break;
                            case "10"://下注
                                this.cardsInfo.text = "下注"+ht["score"].ToString();
                                break;
                            case "1"://加注
                                this.cardsInfo.text = "加注"+ht["score"].ToString();
                                break;
                            case "2"://跟注
                                this.cardsInfo.text = "跟注"+ht["score"].ToString();
                                break;
                            case "0"://弃牌
                                this.cardsInfo.text = "弃牌"+ht["score"].ToString();
                                break;
                            case "4"://全下
                                this.cardsInfo.text = "全下"+ht["score"].ToString();
                                break;
                        }
                    }
                    else
                    {
                        cardsInfo.text = "";
                    }
                }
            }
            else if (ht["opType"].ToString()=="2")
            {
                if (str.Split('|')[3]=="99")
                {
                    this.zhuanpaiCard1.sprite = Resources.Load<Sprite>("pai/card_back_2");
                }
                else
                {
                    this.zhuanpaiCard1.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + str.Split('|')[3]);
                }
                
                //this.zhuanpai.SetActive(false);
                //this.zhuanpai.SetActive(false);
                this.hepai.SetActive(false);
                this.zhuanpai.SetActive(true);
                if (ht.ContainsKey("type"))
                {
                    switch (ht["type"].ToString())
                    {
                        case "3"://看牌
                            this.fanpaiInfo.text = "看牌"+ht["score"].ToString();
                            break;
                        case "10"://下注
                            this.fanpaiInfo.text = "下注"+ht["score"].ToString();
                            break;
                        case "1"://加注
                            this.fanpaiInfo.text = "加注"+ht["score"].ToString();
                            break;
                        case "2"://跟注
                            this.fanpaiInfo.text = "跟注"+ht["score"].ToString();
                            break;
                        case "0"://弃牌
                            this.fanpaiInfo.text = "弃牌"+ht["score"].ToString();
                            break;
                        case "4"://全下
                            this.fanpaiInfo.text = "全下"+ht["score"].ToString();
                            break;
                    }
                }
                else
                {
                    this.fanpaiInfo.text = "";
                    
                }
            }
            else if(ht["opType"].ToString()=="3")
            {
                if (str.Split('|')[4]=="99")
                {
                    this.hepaiCard1.sprite = Resources.Load<Sprite>("pai/card_back_2");
                }
                else
                {
                    this.hepaiCard1.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + str.Split('|')[4]);
                }
                //this.hepai.SetActive(false);
                //this.hepai.SetActive(false);
                this.hepai.SetActive(true);
                if (ht.ContainsKey("type"))
                {
                    switch (ht["type"].ToString())
                    {
                        case "3"://看牌
                            this.zhuanpaiInfo.text = "看牌"+ht["score"].ToString();
                            break;
                        case "10"://下注
                            this.zhuanpaiInfo.text = "下注"+ht["score"].ToString();
                            break;
                        case "1"://加注
                            this.zhuanpaiInfo.text = "加注"+ht["score"].ToString();
                            break;
                        case "2"://跟注
                            this.zhuanpaiInfo.text = "跟注"+ht["score"].ToString();
                            break;
                        case "0"://弃牌
                            this.zhuanpaiInfo.text = "弃牌"+ht["score"].ToString();
                            break;
                        case "4"://全下
                            this.zhuanpaiInfo.text = "全下"+ht["score"].ToString();
                            break;
                    }
                }
                else
                {
                    
                    this.zhuanpaiInfo.text = "";
                }
            }
            else if (ht["opType"].ToString() == "4")
            {
                //if (str.Split('|')[4] == "99")
                //{
                //    this.hepaiCard1.sprite = Resources.Load<Sprite>("pai/card_back_2");
                //}
                //else
                //{
                //    this.hepaiCard1.sprite = Resources.Load<Sprite>(RoomSetPopup.cardPath + str.Split('|')[4]);
                //}
                //this.hepai.SetActive(false);
                //this.hepai.SetActive(false);
                //this.hepai.SetActive(true);
                if (ht.ContainsKey("type"))
                {
                    switch (ht["type"].ToString())
                    {
                        case "3"://看牌
                            this.hepaiInfo.text = "看牌" + ht["score"].ToString();
                            break;
                        case "10"://下注
                            this.hepaiInfo.text = "下注" + ht["score"].ToString();
                            break;
                        case "1"://加注
                            this.hepaiInfo.text = "加注" + ht["score"].ToString();
                            break;
                        case "2"://跟注
                            this.hepaiInfo.text = "跟注" + ht["score"].ToString();
                            break;
                        case "0"://弃牌
                            this.hepaiInfo.text = "弃牌" + ht["score"].ToString();
                            break;
                        case "4"://全下
                            this.hepaiInfo.text = "全下" + ht["score"].ToString();
                            break;
                    }
                }
                else
                {

                    this.hepaiInfo.text = "";
                }
            }
        }
    }

    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
