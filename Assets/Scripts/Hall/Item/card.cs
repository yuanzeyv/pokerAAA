using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    private RawImage cards;
    public string id;
    private Button self;
    public bool isSelected = true;
    public bool isSelectOuts = true;
    public Image selected;
    public int type;

    private void Awake()
    {
        cards = GetComponent<RawImage>();
        self = GetComponent<Button>();
        selected = transform.Find("Image").GetComponent<Image>();

        self.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (this.isSelectOuts)
            {
                if (GameUIManager.GetSingleton().insurancePanel.isMine)
                {
                    GameUIManager.GetSingleton().insurancePanel.toubao.text =
                        GameUIManager.GetSingleton().insurancePanel.baoxian.value.ToString();
                    isSelected = !isSelected;
                    selected.gameObject.SetActive(isSelected);
                    GameUIManager.GetSingleton().insurancePanel.isAllSe = "";
                    if (isSelected) //如果当前牌被选择
                    {
                        if (GameUIManager.GetSingleton().insurancePanel.allCardF.Contains(this)) //如果是当前牌是反超牌
                        {
                            if (!GameUIManager.GetSingleton().insurancePanel.selectedCardF.Contains(this))
                            {
                                GameUIManager.GetSingleton().insurancePanel.selectedCardF.Add(this);
                            }
                        }
                        else //如果当前牌时追平牌
                        {
                            if (!GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Contains(this))
                            {
                                GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Add(this);
                            }
                        }
                    }
                    else //如果当前牌没被选择
                    {
                        if (GameUIManager.GetSingleton().insurancePanel.allCardF.Contains(this))
                        {
                            GameUIManager.GetSingleton().insurancePanel.selectedCardF.Remove(this);
                        }
                        else
                        {
                            GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Remove(this);
                        }
                    }

                    GameUIManager.GetSingleton().insurancePanel.selectPaiCountF =
                        GameUIManager.GetSingleton().insurancePanel.selectedCardF.Count;
                    GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ =
                        GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Count;
                    GameUIManager.GetSingleton().insurancePanel.fanchaopaiText.text =
                        "反超牌" + GameUIManager.GetSingleton().insurancePanel.selectPaiCountF + "/" +
                        GameUIManager.GetSingleton().insurancePanel.fanchaoCount;
                    GameUIManager.GetSingleton().insurancePanel.zhuipingpaiText.text =
                        "追平牌" + GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ + "/" +
                        GameUIManager.GetSingleton().insurancePanel.zhuipingCount;
                    if ((GameUIManager.GetSingleton().insurancePanel.selectedCardF.Count +
                         GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Count) != 0) //如果已选择的牌不为0
                    {
                        GameUIManager.GetSingleton().insurancePanel.buyBtn.interactable = true;
                        NetMngr.GetSingleton().Send(InterfaceGame.GetBaoBenDengLi, new object[]
                        {
                            (GameUIManager.GetSingleton().insurancePanel.selectPaiCountF +
                             GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ).ToString()
                        });
                    }
                    else //如果已选择的牌为0
                    {
                        GameUIManager.GetSingleton().insurancePanel.baoxian.value = GameUIManager.GetSingleton().insurancePanel.baoxian.minValue;
                        GameUIManager.GetSingleton().insurancePanel.toubao.text = "0";
                        GameUIManager.GetSingleton().insurancePanel.peilv.text = "0";
                        GameUIManager.GetSingleton().insurancePanel.peifu.text = "0";
                        GameUIManager.GetSingleton().insurancePanel.buyInsurance.text = "1";
                        GameUIManager.GetSingleton().insurancePanel.buyBtn.interactable = false;
                        GameUIManager.GetSingleton().insurancePanel.btn1.gameObject.SetActive(false);
                        GameUIManager.GetSingleton().insurancePanel.btn2.gameObject.SetActive(false);
                        NetMngr.GetSingleton().Send(InterfaceGame.SyncInsurance,
                            new object[]
                            {
                                "-2", GameUIManager.GetSingleton().insurancePanel.baoxian.value.ToString(),
                                GameUIManager.GetSingleton().insurancePanel.peifu.text,
                                GameUIManager.GetSingleton().insurancePanel.toubao.text,
                                GameUIManager.GetSingleton().insurancePanel.peilv.text,
                                GameUIManager.GetSingleton().insurancePanel.timer.ToString(),
                                GameUIManager.GetSingleton().insurancePanel.baoxian.maxValue.ToString()
                            });
                    }
                }
            }
        });
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void SetData(Hashtable data, int type)
    {
        this.type = type;
        cards.texture = StaticFunction.Getsingleton().SetPai(data["fanChao"].ToString());
        id = data["fanChao"].ToString();
        GameUIManager.GetSingleton().insurancePanel.cardPos.Add(id, this);
        if (type == 1)
        {
            GameUIManager.GetSingleton().insurancePanel.allCardF.Add(this);
            if (!GameUIManager.GetSingleton().insurancePanel.selectedCardF.Contains(this))
            {
                GameUIManager.GetSingleton().insurancePanel.selectedCardF.Add(this);
            }

            GameUIManager.GetSingleton().insurancePanel.selectPaiCountF =
                GameUIManager.GetSingleton().insurancePanel.selectedCardF.Count;
            GameUIManager.GetSingleton().insurancePanel.fanchaopaiText.text =
                "反超牌" + GameUIManager.GetSingleton().insurancePanel.selectPaiCountF + "/" +
                GameUIManager.GetSingleton().insurancePanel.fanchaoCount;
        }
        else
        {
            GameUIManager.GetSingleton().insurancePanel.allCardZ.Add(this);
            if (!GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Contains(this))
            {
                GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Add(this);
            }

            GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ =
                GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Count;
            GameUIManager.GetSingleton().insurancePanel.zhuipingpaiText.text =
                "追平牌" + GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ + "/" +
                GameUIManager.GetSingleton().insurancePanel.zhuipingCount;
        }
    }

    private void OnDestroy()
    {
        if (GameUIManager.GetSingleton().insurancePanel.allCardF.Contains(this))
        {
            GameUIManager.GetSingleton().insurancePanel.allCardF.Remove(this);
            //GameUIManager.GetSingleton().insurancePanel.cardPos.Remove(id);
            if (GameUIManager.GetSingleton().insurancePanel.selectedCardF.Contains(this))
            {
                GameUIManager.GetSingleton().insurancePanel.selectedCardF.Remove(this);
            }

            GameUIManager.GetSingleton().insurancePanel.selectPaiCountF =
                GameUIManager.GetSingleton().insurancePanel.selectedCardF.Count;
            GameUIManager.GetSingleton().insurancePanel.fanchaopaiText.text =
                "反超牌" + GameUIManager.GetSingleton().insurancePanel.selectPaiCountF + "/" +
                GameUIManager.GetSingleton().insurancePanel.fanchaoCount;
        }
        else
        {
            GameUIManager.GetSingleton().insurancePanel.allCardZ.Remove(this);
            //GameUIManager.GetSingleton().insurancePanel.cardPos.Remove(id);
            if (GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Contains(this))
            {
                GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Remove(this);
            }

            GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ =
                GameUIManager.GetSingleton().insurancePanel.selectedCardZ.Count;
            GameUIManager.GetSingleton().insurancePanel.zhuipingpaiText.text =
                "追平牌" + GameUIManager.GetSingleton().insurancePanel.selectPaiCountZ + "/" +
                GameUIManager.GetSingleton().insurancePanel.zhuipingCount;
        }
    }
}