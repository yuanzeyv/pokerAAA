using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
public class Paicontrol : MonoBehaviour
{
    public static Paicontrol _instance;
    Transform paiPool;
    Transform paiPool_big;
    public Transform zhumaPool;
    Transform paiDeskCenter;
    Transform moveDestination;
    Transform qipaiPos;
    RawImage[] MyArea_Cards;
    float MyArea_Cards_y;
    Image MyArea_TwoCards;
    Transform[,] firstPaiPos;
    Transform[] TanPaitrans;
    IEnumerator fanpaiCor;
    AudioClip fapai_clip;
    AudioClip fanpai_clip;

    bool[] bBtnSend = new bool[4];
    Image[] imEye;
    Button[] eyeCardBtn = new Button[4];
    int paiCount;
    int gameType;
    int pai_index;

    bool fapaiing = false; // 正在发牌

    string[] strCards = new string[4];
    public AudioSource ads;
    public static Paicontrol GetInstance()
    {
        return _instance;
    }
    void Awake()
    {
        _instance = this;
        ads = this.GetComponent<AudioSource>();
        paiPool = transform.Find("PaiPool");
        paiPool_big = transform.Find("PaiPool_Big");
        zhumaPool = transform.Find("ZhumaPool");
        qipaiPos = transform.Find("qipaipos");
        paiPool.gameObject.SetActive(false);
        paiPool_big.gameObject.SetActive(false);
        zhumaPool.gameObject.SetActive(false);
        paiDeskCenter = transform.Find("Center");
        MyArea_Cards = new RawImage[] { transform.Find("MyArea/eyeBtn1/1").GetComponent<RawImage>(), transform.Find("MyArea/eyeBtn2/2").GetComponent<RawImage>() };
        MyArea_Cards[0].gameObject.SetActive(false);
        MyArea_Cards[1].gameObject.SetActive(false);

        imEye = new Image[] { transform.Find("MyArea/eyeBtn1/Image").GetComponent<Image>(), transform.Find("MyArea/eyeBtn2/Image").GetComponent<Image>(), transform.Find("MyArea/eyeBtn3/Image").GetComponent<Image>(), transform.Find("MyArea/eyeBtn4/Image").GetComponent<Image>() };
        for (int i = 0; i < 4; i++)
        {
            bBtnSend[i] = false;
            imEye[i].gameObject.SetActive(false);
        }
        MyArea_Cards_y = MyArea_Cards[0].transform.localPosition.y;
        paiCount = 2;

        eyeCardBtn[0] = transform.Find("MyArea/eyeBtn1").GetComponent<Button>();
        eyeCardBtn[0].onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            //if(bBtnSend[0])
            //{
            //    bBtnSend[0] = false;
            //}
            if (strCards[0] != null)
            {
                Debug.Log(strCards[0]);
                NetMngr.GetSingleton().Send(InterfaceGame.getShowCard, new object[] { int.Parse(strCards[0]) });
            }

        });
        eyeCardBtn[1] = transform.Find("MyArea/eyeBtn2").GetComponent<Button>();
        eyeCardBtn[1].onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (strCards[1] != null)
            {
                Debug.Log(strCards[1]);
                NetMngr.GetSingleton().Send(InterfaceGame.getShowCard, new object[] { int.Parse(strCards[1]) });
            }


        });
        eyeCardBtn[2] = transform.Find("MyArea/eyeBtn3").GetComponent<Button>();
        eyeCardBtn[2].onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (strCards[2] != null)
            {
                Debug.Log(strCards[2]);
                NetMngr.GetSingleton().Send(InterfaceGame.getShowCard, new object[] { int.Parse(strCards[2]) });
            }

        });
        eyeCardBtn[3] = transform.Find("MyArea/eyeBtn4").GetComponent<Button>();
        eyeCardBtn[3].onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            if (strCards[3] != null)
            {
                Debug.Log(strCards[3]);
                NetMngr.GetSingleton().Send(InterfaceGame.getShowCard, new object[] { int.Parse(strCards[3]) });
            }

        });




    }

    public void setMyArea(int gametype)
    {
        gameType = gametype;
        pai_index = gameType == 1 ? 1 : 0;

        if (gametype == 1)
        {
            MyArea_Cards = new RawImage[] {  transform.Find("MyArea/eyeBtn1/1").GetComponent<RawImage>(),
                transform.Find("MyArea/eyeBtn2/2").GetComponent<RawImage>(), transform.Find("MyArea/eyeBtn3/3").GetComponent<RawImage>(),transform.Find("MyArea/eyeBtn4/4").GetComponent<RawImage>() };

            MyArea_Cards[0].gameObject.SetActive(false);
            MyArea_Cards[1].gameObject.SetActive(false);
            MyArea_Cards[2].gameObject.SetActive(false);
            MyArea_Cards[3].gameObject.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                eyeCardBtn[i].gameObject.SetActive(false);
            }

            paiCount = 4;

        }
    }

    private void Start()
    {
        fapai_clip = Resources.Load<AudioClip>("Sound/fapai");
        fanpai_clip = Resources.Load<AudioClip>("Sound/fanpai");
    }
    // 弃牌
    //add by yang yang 2021年3月16日 14:21:30
    public void QiPaiDark(int localid, bool bDark)
    {
        if (bDark && localid == GameManager.GetSingleton().myNetID && !StaticData.isGuanzhan)
        {
            for (int i = 0; i < paiCount; i++)
            {
                MyArea_Cards[i].color = new Color(1, 1, 1, 0.4f);
              //  MyArea_Cards[1].color = new Color(1, 1, 1, 0.4f);
            }
        //    StaticData.isInGame = false;
        }
        else
        {
            for (int i = 0; i < paiCount; i++)
            {

                Transform des = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).Find("others/Pai_" + pai_index).GetChild(i).transform;
                Debug.Log("弃牌-----------des.childCount = " + des.childCount);
                
                if (des.childCount == 0)
                    return;
                for(int j = 0; j< des.childCount; j++)
                {
                    Transform temp = des.GetChild(j);
                    if (temp != null)
                    {
                        temp.GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
                    }
                }
            //    des.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>().Daojishi_Stop();
        }
    }
    //end
    public void QiPaiAni(int localid, string tem)
    {
        if (localid == GameManager.GetSingleton().myNetID && !StaticData.isGuanzhan)
        //        if (localid==0 && !StaticData.isGuanzhan)
        {
            Debug.Log("弃牌");
            for (int i = 0; i < MyArea_Cards.Length; i++)
            {
                MyArea_Cards[i].transform.DOLocalMoveY(MyArea_Cards_y + 400, 1f).OnComplete(delegate
                {
                    MyArea_Cards[i].transform.localPosition = new Vector2(MyArea_Cards[i].transform.localPosition.x, MyArea_Cards_y);
                });
                MyArea_Cards[i].DOFade(0, 1f).OnComplete(delegate
                {
                    if (tem == "1")
                    {

                        eyeCardBtn[i].gameObject.SetActive(true);

                        MyArea_Cards[i].transform.gameObject.SetActive(true);
                        MyArea_Cards[i].color = new Color(1, 1, 1, 0.6f);
                    }
                    else
                    {
                        eyeCardBtn[i].gameObject.SetActive(false);
                        MyArea_Cards[i].transform.gameObject.SetActive(false);
                        MyArea_Cards[i].color = new Color(1, 1, 1, 1f);
                    }

                });

            }

        }
        else
        {
            // 回收小牌
            //  选取一个做动画，剩下的直接删除
            for (int i = 0; i < paiCount; i++)
            {

                Transform des = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).Find("others/Pai_" + pai_index).GetChild(i).transform;
                if (des.childCount == 0)
                    return;
                Transform temp = des.GetChild(0);
                if (temp != null)
                {
                    temp.SetParent(qipaiPos);
                    temp.DOLocalMove(Vector2.zero, 0.8f).OnComplete(delegate
                        {
                            temp.SetParent(paiPool);
                            temp.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            temp.localPosition = Vector3.zero;
                        });
                    temp.GetComponent<Image>().DOFade(0, 0.8f);
                }
                while (des.childCount > 0)
                {
                    Transform t = des.GetChild(0);
                    t.SetParent(paiPool);
                    t.localPosition = Vector3.zero;
                }

            }
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>().Daojishi_Stop();

        }

    }
    public void XiaZhuAni(PlayInfo p)
    {
        if (zhumaPool.childCount > 0)
        {
            Transform go = zhumaPool.GetChild(0);
            go.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            go.SetParent(p.othersTrans);
            go.localScale = Vector3.one;
            go.localPosition = Vector3.zero;
            go.SetParent(p.choumaTrans);
            go.DOLocalMove(Vector3.zero, 0.15f);
        }
    }
    //  小局结束 
    public void SmallRoundOver()
    {
        GameUIManager.GetSingleton().hideBianchi();
        // 牌重置
        RecoverDeskCenterPai();
        StartCoroutine(recoveryZhuMa());
        RecoveryPerson_Big_Small_Pai();
        // 头像特效重置
        for (int i = 0; i < GameManager.GetSingleton().MapLocalSeatPlayer.Count; i++)
        {
            //  Debug.Log(GameManager.GetSingleton().MapLocalSeatPlayer[i]);
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[i]).GetComponent<PlayInfo>().OneGameStartResetUI(false);
        }
        //桌面UI重置
        GameUIManager.GetSingleton().gameStartResetData();
        StaticData.gameStart = false;
    }
    public void PlayerAlreadyOnDeskFapai(Transform trans)
    {

        for (int i = 0; i < paiCount; i++)
        {
            if (paiPool.childCount > 0)
            {
                Transform t = paiPool.GetChild(0);
                t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                Transform temp = trans.Find("others/Pai_" + pai_index).GetChild(i);
                t.SetParent(temp);
                t.localPosition = Vector3.zero;
                t.localScale = Vector3.one;
            }
        }
    }

    public void getShowCard(Hashtable h)
    {
        string mCode = h["code"].ToString();
        if (mCode == "success")
        {
            for (int i = 0; i < paiCount; i++)
                if (h["cardIndex"].ToString() == strCards[i])
                {
                    if (!bBtnSend[i])
                    {
                        imEye[i].gameObject.SetActive(true);
                        bBtnSend[i] = true;
                    }
                    else
                    {
                        imEye[i].gameObject.SetActive(false);
                        bBtnSend[i] = false;
                    }
                }
        }
        else
        {
            PopupCommon.GetSingleton().ShowView("获取信息失败");
        }

    }
    public void MfaPai(Hashtable h)
    {
        // RecoveryPerson_Big_Small_Pai();
  //      StaticData.isInGame = true;
        fapaiing = true;
        StartCoroutine(FaPai(h));
    }
    // 发牌动画  
    IEnumerator FaPai(Hashtable h)
    {
        string[] mcards = h["cards"].ToString().Split('|');
        string[] netIDs = h["netID"].ToString().Split('|');
        int nQipai = int.Parse(h["qipai"].ToString()); 
        List<int> tempallcanPeople = new List<int>();
        for (int i = 0; i < netIDs.Length; i++)
        {
            tempallcanPeople.Add(GameManager.GetSingleton().netTolocal(int.Parse(netIDs[i])));
            //8-7 新需求，把玩家头像透明度设置1
            int localid = GameManager.GetSingleton().netTolocal(int.Parse(netIDs[i]));
            GameUIManager.GetSingleton().RecoerHeadImage(localid);
        }

        if (paiPool.childCount > 0)
        {
            for (int i = 0; i < paiCount; i++)
            {
                for (int j = 0; j < GameManager.GetSingleton().MapLocalSeatPlayer.Count; j++)
                {
                    //Debug.Log("GameManager.GetSingleton().MapLocalSeatPlayer.Count:"+ GameManager.GetSingleton().MapLocalSeatPlayer.Count);
                    // 自己的牌直接显示 
                    //if (!StaticData.isGuanzhan && (GameManager.GetSingleton().MapLocalSeatPlayer[j]==0))
                    if (!StaticData.isGuanzhan && (GameManager.GetSingleton().MapLocalSeatPlayer[j] == GameManager.GetSingleton().myNetID))
                    {
                        // 留座离桌 托管 
                        if (h["cards"].ToString() != "")
                        {
                            if (paiPool_big.childCount > 0)
                            {
                                //Debug.Log("显示自己的牌");
                                Transform t = MyArea_Cards[i].transform;
                                t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                                eyeCardBtn[i].gameObject.SetActive(true);
                                MyArea_Cards[i].gameObject.SetActive(true);
                                MyArea_Cards[i].transform.DOPause();
                                MyArea_Cards[i].DOPause();
                                MyArea_Cards[i].color = new Color(1, 1, 1, 1);
                                MyArea_Cards[i].transform.localPosition = new Vector2(MyArea_Cards[i].transform.localPosition.x, MyArea_Cards_y);

                                //                                 Debug.Log("StaticData.isInGame = " + StaticData.isInGame);
                                //                                 if(!StaticData.isInGame)//弃牌状态
                                //                                 {
                                //                                     MyArea_Cards[i].color = new Color(1, 1, 1, 0.4f);
                                //                                 }

                                if (nQipai == 0)
                                {
                                    MyArea_Cards[i].color = new Color(1, 1, 1, 0.4f);
                                }
                                strCards[i] = mcards[i];
                                // 没有勾选“延迟看牌”
                                if(!GameUIManager.GetSingleton().delaySeeCard){
                                    t.DOScaleX(0, 0.15f);
                                    yield return new WaitForSeconds(0.15f);
                                    t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[i]);
                                    t.DOScaleX(1f, 0.15f);
                                    t.GetComponent<RawImage>().gameObject.SetActive(true);
                                    t.DOScale(Vector3.one, 0.125f);    
                                }
                                
                            }
                        }
                        else
                        {
                            Debug.Log("留座离桌 托管中。。。");
                        }

                    }
                    //发给其他人
                    else
                    {
                        if (paiPool.childCount > 0 && tempallcanPeople.Contains(GameManager.GetSingleton().MapLocalSeatPlayer[j]))
                        {
                            Transform t = paiPool.GetChild(0);
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[j]).Find("others/Pai_" + pai_index).GetChild(i);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                            t.DOScale(Vector3.one, 0.125f);
                            t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/fashoupai");
                            });
                        }
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }


        if (h.Contains("otherCards"))
        {
            if (h["otherCards"].ToString() != "")
            {
                ArrayList othercards = h["otherCards"] as ArrayList;
                for (int i = 0; i < othercards.Count; i++)
                {

                    Hashtable data = othercards[i] as Hashtable;
                    showPai(data);
                }

            }
        }
        fapaiing = false;

    }

     public void ShowFanPai()
    {
        StartCoroutine(MShowFanPai());
    }

    IEnumerator MShowFanPai(){
        if(fapaiing){ // 正在发牌 需要等牌发完再翻牌（因为发牌有wait）
            yield return new WaitForSeconds(1f);
        }
        if(GameUIManager.GetSingleton().delaySeeCard){ // 勾选延迟看牌
            for (int i = 0; i < paiCount; i++)
            {
                for (int j = 0; j < GameManager.GetSingleton().MapLocalSeatPlayer.Count; j++)
                {
                    if (!StaticData.isGuanzhan && (GameManager.GetSingleton().MapLocalSeatPlayer[j] == GameManager.GetSingleton().myNetID))
                    {
                        Transform tr = MyArea_Cards[i].transform;
                        tr.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        tr.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + strCards[i]);
                        tr.DOScaleX(1f, 0.15f);
                        tr.GetComponent<RawImage>().gameObject.SetActive(true);
                        tr.DOScale(Vector3.one, 0.125f);
                        break;
                    }
                }
            }    
        }
    }


    public void FaPaiDeskCenterLeft(Hashtable h)
    {
        //1 表示发翻牌
        //2 转牌
        //3 和牌


        switch (h["type"].ToString())
        {
            case "0": break;
            case "1":
                StartCoroutine(FaPai_left(h));
                break;
            case "2":
                StartCoroutine(FaPai_left2(h));
                break;
            case "3":
                StartCoroutine(FaPai_left3(h));
                break;
        }


        // StartCoroutine(FaPai_left(h));
        // 隐藏提示
        for (int i = 0; i < GameManager.GetSingleton().MapLocalSeatPlayer.Count; i++)
        {
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[i]).GetComponent<PlayInfo>().setModel(false);
        }
    }
    // 清空桌面显示区的牌+底部手牌
    public void RecoverDeskCenterPai()
    {
        Debug.Log("清空桌面显示区的牌+底部手牌");
        GameManager.GetSingleton().isGetGonggongPai = false;
        for (int i = 0; i < paiCount; i++)
        {
            MyArea_Cards[i].transform.localPosition = new Vector2(MyArea_Cards[i].transform.localPosition.x, MyArea_Cards_y);
            MyArea_Cards[i].gameObject.SetActive(false);
            eyeCardBtn[i].gameObject.SetActive(false);
            bBtnSend[i] = false;
            imEye[i].gameObject.SetActive(false);
        }

        for (int jtemp = 0; jtemp < paiDeskCenter.childCount; jtemp++)
        {
            Debug.Log(jtemp + " 子物体个数 " + paiDeskCenter.GetChild(jtemp).childCount);
            while (paiDeskCenter.GetChild(jtemp).childCount > 0)
            {

                Transform ttemp = paiDeskCenter.GetChild(jtemp).GetChild(0);
                ttemp.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ttemp.SetParent(paiPool_big);
                ttemp.localScale = Vector3.zero;
                Debug.Log(jtemp + " 移除子物体，剩余个数 " + paiDeskCenter.GetChild(jtemp).childCount);
            }
        }
    }
    public void showPai(Hashtable h)
    {
        if (GameManager.GetSingleton().myNetID != int.Parse(h["netID"].ToString()))
            StartCoroutine(SmallRoundshowPai(h));
    }
    IEnumerator SmallRoundshowPai(Hashtable h)
    {
        //Debug.Log("显示牌！");
        int localid = GameManager.GetSingleton().netTolocal(int.Parse(h["netID"].ToString()));
        string[] mcards = h["cards"].ToString().Split('|');
        string[] showcards = { };
        if (h.ContainsKey("showCard"))
        {
            showcards = h["showCard"].ToString().Split('|');
        }

        string mtypes = "";
        if (h.Contains("cardsType"))
        {
            mtypes = h["cardsType"].ToString();
            if (mtypes != "")
                GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>().setPaixingText(true, mtypes);
        }
        if (h.Contains("showAnimation"))
        {
            if (h["showAnimation"].ToString() == "0")
            {

                for (int j = 0; j < paiCount; j++)
                {


                    Transform temp1 = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).Find("others/showPai_" + pai_index).GetChild(j);
                    if (temp1 != null && temp1.childCount > 0)
                    {
                        Transform t1 = temp1.GetChild(0);
                        if (t1 != null)
                        {

                            //奥马哈凸显用到的牌
                            if (gameType == 1 && showcards.Length > 0)
                            {

                                bool blight = false;
                                for (int k = 0; k < showcards.Length; k++)
                                {

                                    if (mcards[j] == showcards[k])
                                    {
                                        blight = true;
                                    }
                                }
                                if (!blight)
                                {
                                    t1.GetComponent<RawImage>().color = new Color(.5f, .5f, .5f, 1f);
                                }
                                else
                                {
                                    t1.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
                                }

                            }
                        }

                    }

                }


                yield break;
            }

            else
            {

                // 回收小牌
                for (int i = 0; i < paiCount; i++)
                {
                    Transform t = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).Find("others/Pai_" + pai_index).GetChild(i).transform;
                    while (t.childCount > 0)
                    {
                        Transform temp = t.GetChild(0);
                        temp.SetParent(paiPool);
                        temp.localScale = Vector3.one;
                        temp.localPosition = Vector3.zero;
                    }
                    //  Debug.Log("回收小牌！"+i);
                }
                // 展示大牌
                for (int j = 0; j < paiCount; j++)
                {
                    if (h["anim"].ToString() == "1")
                    {
                        if (paiPool_big.childCount > 0 && int.Parse(mcards[j]) >= 0)
                        {
                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).Find("others/showPai_" + pai_index).GetChild(j);
                            t.SetParent(temp);
                            t.localPosition = Vector3.zero;
                            t.localScale = Vector3.one;
                            t.DOScaleX(0, 0.15f);
                            yield return new WaitForSeconds(0.15f);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            t.DOScale(0.8f, 0.15f).OnStart(delegate
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                            });
                            t.GetComponent<RawImage>().gameObject.SetActive(true);
                            //奥马哈凸显用到的牌
                            if (gameType == 1 && showcards.Length > 0)
                            {

                                bool blight = false;
                                for (int k = 0; k < showcards.Length; k++)
                                {

                                    if (mcards[j] == showcards[k])
                                    {
                                        blight = true;
                                    }
                                }
                                if (!blight)
                                {
                                    t.GetComponent<RawImage>().color = new Color(.5f, .5f, .5f, 1f);
                                }
                                else
                                {
                                    t.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
                                }

                            }
                            //                Debug.Log(" 展示大牌！"+j);
                        }
                    }
                    else
                    {
                        if (paiPool_big.childCount > 0)
                        {
                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).Find("others/showPai_" + pai_index).GetChild(j);
                            t.SetParent(temp);
                            t.localPosition = Vector3.zero;
                            t.localScale = Vector3.one;
                            t.DOScaleX(0, 0.0f);
                          
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            t.DOScale(0.8f, 0.15f);
                            t.GetComponent<RawImage>().gameObject.SetActive(true);
                            //奥马哈凸显用到的牌
                            if (gameType == 1 && showcards.Length > 0)
                            {

                                bool blight = false;
                                for (int k = 0; k < showcards.Length; k++)
                                {

                                    if (mcards[j] == showcards[k])
                                    {
                                        blight = true;
                                    }
                                }
                                if (!blight)
                                {
                                    t.GetComponent<RawImage>().color = new Color(.5f, .5f, .5f, 1f);
                                }
                                else
                                {
                                    t.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
                                }

                            }
                            //                Debug.Log(" 展示大牌！"+j);
                        }
                    }
                }
            }

        }
    }
    // 结算
    public void ShowWinner(Hashtable hh)
    {
        ArrayList ar = hh["winnerList"] as ArrayList;
        for (int i = 0; i < ar.Count; i++)
        {
            Hashtable h = ar[i] as Hashtable;
            int localid = GameManager.GetSingleton().netTolocal(int.Parse(h["netID"].ToString()));
            string coin = h["winCoin"].ToString();
            PlayInfo pinfo = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(localid).GetComponent<PlayInfo>();
            pinfo.SetWinScore(coin);
            GameUIManager.GetSingleton()._myController.ResetUIButton();
            // 筹码飞
            StartCoroutine(winnerCoinFly(pinfo.transform.Find("wincoinpos"), pinfo));
        }
        //  清空玩家面牌  7 8 修改
        // RecoveryPerson_Big_Small_Pai();
    }
    IEnumerator winnerCoinFly(Transform parent, PlayInfo p)
    {
        List<Transform> templistTransfrom = new List<Transform>();
        SoundManager.GetSingleton().PlaySound("Audio/chiptoplayers");
        for (int j = 0; j < 10; j++)
        {
            if (zhumaPool.childCount > 0)
            {
                Transform go = zhumaPool.GetChild(0);
                go.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                go.SetParent(GameUIManager.GetSingleton().dichizhuma_ani_destination);
                go.localScale = Vector3.one;
                go.localPosition = Vector3.zero;
                go.SetParent(parent);
                templistTransfrom.Add(go);
                go.DOLocalMove(Vector3.zero, 0.15f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        // yield return new WaitForSeconds(0.6f);
        p.PlayWinEffect();
        for (int i = 0; i < templistTransfrom.Count; i++)
        {
            templistTransfrom[i].SetParent(zhumaPool);
            templistTransfrom[i].localScale = Vector3.one;
            templistTransfrom[i].localPosition = Vector3.zero;
        }
    }
    public void RecoveryPersonPai(Transform t)
    {
        for (int i = 0; i < paiCount; i++)
        {
            Transform big = t.Find("others/showPai_" + pai_index + "/" + (i + 1).ToString());
            while (big.childCount > 0)
            {
                Transform tt = big.GetChild(0);
                tt.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
                tt.SetParent(paiPool_big);
                tt.localPosition = Vector3.zero;
                tt.localScale = Vector3.one;
            }

            Transform small = t.Find("others/Pai_" + pai_index + "/" + (i + 1).ToString());
            while (small.childCount > 0)
            {
                Transform tt = small.GetChild(0);
                tt.SetParent(paiPool);
                tt.localPosition = Vector3.zero;
                tt.localScale = Vector3.one;
            }
        }
    }
    public void RecoveryPerson_Big_Small_Pai()
    {
        for (int i = 0; i < GameManager.GetSingleton().MapLocalSeatPlayer.Count; i++)
        {

            //  Debug.Log(GameManager.GetSingleton().MapLocalSeatPlayer[i]+"===  "+ GameManager.GetSingleton().MapLocalSeatPlayer.Count);
            Transform temptransfrom_old = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[i]);
            RecoveryPersonPai(temptransfrom_old);
            temptransfrom_old.GetComponent<PlayInfo>().setPaixingText(false);
        }
    }
    public void coinFlyTocenter726()
    {

        StartCoroutine(recoveryZhuMa());
    }
    IEnumerator FaPai_left(Hashtable h)
    {
        string[] mcards = h["cards"].ToString().Split('|');
        switch (h["type"].ToString())
        {
            case "0": break;
            case "1":
                GameManager.GetSingleton().isGetGonggongPai = true;
                Debug.Log("发公共牌");
                // 发牌的同时需要积分飞往底池 
                // StartCoroutine(recoveryZhuMa());
                for (int j = 0; j < 3; j++)
                {
                    Debug.Log("发牌  if (h[].ToString() == )" + j);
                    if (h["anim"].ToString() == "1")
                    {
                        Debug.Log("发牌" + j);
                        if (paiPool_big.childCount > 0)
                        {
                            Debug.Log("发牌------0---------" + j);
                            if (GameManager.GetSingleton().isGetGonggongPai == false)
                                break;
                       //     Debug.Log("发牌------1---------" + j);
                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = paiDeskCenter.GetChild(j);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                        //    Debug.Log("发牌------2---------" + j);
                            t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/fapai");
                            });
                       //     Debug.Log("发牌------3---------" + j);
                            yield return new WaitForSeconds(0.125f);
                        //    Debug.Log("发牌------4---------" + j);
                            t.DOScaleX(0, 0.15f);
                         //   Debug.Log("发牌------5---------" + j);
                            yield return new WaitForSeconds(0.15f);
                       //     Debug.Log("发牌------6---------" + j);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                       //     Debug.Log("发牌------7---------" + j);
                            t.DOScaleX(1f, 0.15f).OnStart(delegate
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                            });
                        //    Debug.Log("发牌------8---------" + j);

                            t.GetComponent<RawImage>().gameObject.SetActive(true);



                        }
                        yield return new WaitForSeconds(0.1f);
                    }
                    else
                    {
                        Debug.Log("发牌" + j);
                        if (paiPool_big.childCount > 0)
                        {
                            if (GameManager.GetSingleton().isGetGonggongPai == false)
                                break;

                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = paiDeskCenter.GetChild(j);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                            t.DOLocalMove(Vector3.zero, 0.0f);

                            //          t.DOScaleX(0, 0.0f);

                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            //    t.DOScaleX(1f, 0.0f);
                            t.GetComponent<RawImage>().gameObject.SetActive(true);

                        }

                    }
                }

                break;
            case "2":
                // StartCoroutine(recoveryZhuMa());
                if (h["anim"].ToString() == "1")
                {
                    Debug.Log("发公共牌2");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(3);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        // t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/fapai");
                        });
                        yield return new WaitForSeconds(0.125f);
                        t.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.15f).OnStart(delegate
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                        });
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("发公共牌2");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(3);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        // t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.0f);

                        //    t.DOScaleX(0, 0.15f);

                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                        //    t.DOScaleX(1f, 0.15f);
                    }
                }
                break;
            case "3":
                //  StartCoroutine(recoveryZhuMa());
                if (h["anim"].ToString() == "1")
                {
                    Debug.Log("发公共牌3");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(4);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        //   t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        // t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/fapai");
                        });
                        yield return new WaitForSeconds(0.125f);
                        t.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.15f).OnStart(delegate
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                        });
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("发公共牌3");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(4);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        //   t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        // t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.0f);

                        //    t.DOScaleX(0, 0.0f);

                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.0f);
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                break;
        }
        yield return null;
    }
    IEnumerator FaPai_left2(Hashtable h)
    {
        string[] mcards = h["cards"].ToString().Split('|');
        switch (h["type"].ToString())
        {
            case "0": break;
            case "1":
                GameManager.GetSingleton().isGetGonggongPai = true;
                Debug.Log("发公共牌");
                // 发牌的同时需要积分飞往底池 
                // StartCoroutine(recoveryZhuMa());
                for (int j = 0; j < 3; j++)
                {
                    if (h["anim"].ToString() == "1")
                    {
                        Debug.Log("发牌" + j);
                        if (paiPool_big.childCount > 0)
                        {
                            if (GameManager.GetSingleton().isGetGonggongPai == false)
                                break;

                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = paiDeskCenter.GetChild(j);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                            t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/fapai");
                            });
                            yield return new WaitForSeconds(0.125f);
                            t.DOScaleX(0, 0.15f);
                            yield return new WaitForSeconds(0.15f);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            t.DOScaleX(1f, 0.15f).OnStart(delegate
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                            });
                            t.GetComponent<RawImage>().gameObject.SetActive(true);
                        }
                        yield return new WaitForSeconds(0.1f);
                    }
                    else
                    {
                        Debug.Log("发牌" + j);
                        if (paiPool_big.childCount > 0)
                        {
                            if (GameManager.GetSingleton().isGetGonggongPai == false)
                                break;

                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = paiDeskCenter.GetChild(j);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                            t.DOLocalMove(Vector3.zero, 0.0f);

                            //  t.DOScaleX(0, 0.15f);

                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            //  t.DOScaleX(1f, 0.15f);
                            t.GetComponent<RawImage>().gameObject.SetActive(true);
                        }

                    }

                }

                break;
            case "2":
                // StartCoroutine(recoveryZhuMa());
                if (h["anim"].ToString() == "1")
                {

                    Debug.Log("发公共牌2");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(3);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        // t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/fapai");
                        });
                        yield return new WaitForSeconds(0.125f);
                        t.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.15f).OnStart(delegate
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                        });
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("发公共牌2");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(3);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        // t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.0f);

                        //  t.DOScaleX(0, 0.15f);

                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //     t.DOScaleX(1f, 0.15f);
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }

                }
                break;
            case "3":
                //  StartCoroutine(recoveryZhuMa());
                if (h["anim"].ToString() == "1")
                {

                    Debug.Log("发公共牌3");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(4);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        //   t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        // t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/fapai");
                        });
                        yield return new WaitForSeconds(0.125f);
                        t.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.15f).OnStart(delegate
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                        });
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("发公共牌3");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(4);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        //   t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        // t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.0f);

                        //  t.DOScaleX(0, 0.15f);

                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScaleX(1f, 0.15f);
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }

                }
                break;
        }
        yield return null;
    }
    // left
    IEnumerator FaPai_left3(Hashtable h)
    {
        string[] mcards = h["cards"].ToString().Split('|');
        switch (h["type"].ToString())
        {
            case "0": break;
            case "1":
                GameManager.GetSingleton().isGetGonggongPai = true;
                Debug.Log("发公共牌");
                // 发牌的同时需要积分飞往底池 
                // StartCoroutine(recoveryZhuMa());
                for (int j = 0; j < 3; j++)
                {
                    if (h["anim"].ToString() == "1")
                    {

                        Debug.Log("发牌" + j);
                        if (paiPool_big.childCount > 0)
                        {
                            if (GameManager.GetSingleton().isGetGonggongPai == false)
                                break;

                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = paiDeskCenter.GetChild(j);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                            t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/fapai");
                            });
                            yield return new WaitForSeconds(0.125f);
                            t.DOScaleX(0, 0.15f);
                            yield return new WaitForSeconds(0.15f);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            t.DOScaleX(1f, 0.15f).OnStart(delegate
                            {
                                SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                            });
                            t.GetComponent<RawImage>().gameObject.SetActive(true);
                        }
                        yield return new WaitForSeconds(0.1f);
                    }
                    else
                    {

                        Debug.Log("发牌" + j);
                        if (paiPool_big.childCount > 0)
                        {
                            if (GameManager.GetSingleton().isGetGonggongPai == false)
                                break;

                            Transform t = paiPool_big.GetChild(0);
                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                            t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                            Transform temp = paiDeskCenter.GetChild(j);
                            t.SetParent(temp);
                            t.localScale = Vector3.one;
                            t.DOLocalMove(Vector3.zero, 0.0f);

                            //  t.DOScaleX(0, 0.15f);

                            t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[j]);
                            //    t.DOScaleX(1f, 0.15f);
                            t.GetComponent<RawImage>().gameObject.SetActive(true);
                        }

                    }
                }


                break;
            case "2":
                // StartCoroutine(recoveryZhuMa());
                if (h["anim"].ToString() == "1")
                {
                    Debug.Log("发公共牌2");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(3);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        // t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/fapai");
                        });
                        yield return new WaitForSeconds(0.125f);
                        t.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.15f).OnStart(delegate
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                        });
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("发公共牌2");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(3);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        // t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.0f);

                        //  t.DOScaleX(0, 0.15f);

                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScaleX(1f, 0.15f);
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                break;
            case "3":
                //  StartCoroutine(recoveryZhuMa());
                if (h["anim"].ToString() == "1")
                {

                    Debug.Log("发公共牌3");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(4);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        //   t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        // t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.125f).OnStart(() =>
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/fapai");
                        });
                        yield return new WaitForSeconds(0.125f);
                        t.DOScaleX(0, 0.15f);
                        yield return new WaitForSeconds(0.15f);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        t.DOScaleX(1f, 0.15f).OnStart(delegate
                        {
                            SoundManager.GetSingleton().PlaySound("Audio/kanpai");
                        });
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("发公共牌3");
                    if (paiPool_big.childCount > 0)
                    {
                        Transform t = paiPool_big.GetChild(0);
                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>("pai/card_back_2");
                        t.GetComponent<RectTransform>().anchoredPosition = Vector2.one;
                        Transform temp = paiDeskCenter.GetChild(4);
                        t.SetParent(temp);
                        t.localScale = Vector3.one;
                        //   t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        // t.DOScale(Vector3.one, 0.125f);
                        t.DOLocalMove(Vector3.zero, 0.0f);

                        //  t.DOScaleX(0, 0.15f);

                        t.GetComponent<RawImage>().texture = Resources.Load<Texture>(RoomSetPopup.cardPath + mcards[0]);
                        //   t.DOScaleX(1f, 0.15f);
                        t.GetComponent<RawImage>().gameObject.SetActive(true);
                    }
                }
                break;
        }
        yield return null;
    }
    IEnumerator recoveryZhuMa()
    {
        // SoundManager.GetSingleton().PlaySound("Audio/chiptopool");
        Transform tempEnd = GameUIManager.GetSingleton().dichizhuma_ani_destination;
        for (int j = 0; j < GameManager.GetSingleton().MapLocalSeatPlayer.Count; j++)
        {
            Transform tempStart = GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[j]).Find("others/Chouma");

            while (tempStart.childCount > 0)
            {
                Transform tt = tempStart.GetChild(0);
                tt.SetParent(tempEnd);
                tt.DOLocalMove(Vector3.zero, 0.5f).OnStart(() =>
                {
                    //MusicManage.Single.AddMuicPlay(Resources.Load<AudioClip>("Sound/fapai"));
                });
            }
            GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[j]).GetComponent<PlayInfo>().SetNewChouma("");
        }
        GameUIManager.GetSingleton().setZhuamaDichi();
        // 注意等待时间同步
        yield return new WaitForSeconds(0.5f);
        while (tempEnd.childCount > 0)
        {
            Transform t = tempEnd.GetChild(0);
            t.SetParent(zhumaPool);
            t.localScale = Vector3.one;
            t.localPosition = Vector3.zero;
        }
    }

    public void KBStopAllCouritn()
    {
        ads.Stop();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Hashtable h = new Hashtable();
        //    h.Add("i", "1");
        //    h.Add("cards", "406|206|111|206|206|111|206|206|206|111|206|206|111|");
        //    MfaPai(h);
        //}
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    // RecoveryPerson_Big_Small_Pai();
        //    for(int i=0;i< GameManager.GetSingleton().MapLocalSeatPlayer.Count;i++)
        //    XiaZhuAni(GameUIManager.GetSingleton().roomNumSitActivePlayerTrans.GetChild(GameManager.GetSingleton().MapLocalSeatPlayer[i]).GetComponent<PlayInfo>());
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Hashtable h = new Hashtable();
        //    h.Add("type", "1");
        //    h.Add("cards", "406|206|111|206|206|111|206|206|206|111|206|206|111|");
        //    FaPaiDeskCenterLeft(h);
        //}
    }
}
