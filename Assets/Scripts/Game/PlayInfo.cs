using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayInfo : MonoBehaviour {

    private Button btnsit;
    private Text gold;
    private Text chouma;
    public Text playerName;
    private IconCircle iconDaojishi;
    private GameObject allin;
    public Transform d;
    public Transform paiDestination;
    public Transform showPaiDestination;
    public Transform othersTrans;
    public Transform choumaTrans;
    public Transform paixingTrans;
    public Transform tuoguanTrans;
    public Text paixingTranstext;
    public Transform winEffectTrans;
    public CircleImage cricleImage;
    public Image imgmodel;
    public Button imageBtn;

	public Transform winTrans;
	public Text winScore_Text;
	public Animation moveup;

    public string userID { get; set; }

	void Awake () {

        //transform.localScale = new Vector2(0.9f, 0.9f);

        btnsit = transform.Find("sit").GetComponent<Button>();
        btnsit.onClick.AddListener(sitCallback);
       // paiDestination = transform.Find("Pai");
        paixingTrans = transform.Find("others/paixing");
        paixingTranstext = transform.Find("others/paixing/Text").GetComponent<Text>();
        tuoguanTrans = transform.Find("others/tuoguan");
        tuoguanTrans.gameObject.SetActive(false);
        //showPaiDestination = transform.Find("showPai");
        othersTrans = transform.Find("others");
        winEffectTrans = othersTrans.Find("ChoumaFX");
        winEffectTrans.gameObject.SetActive(false);

		winTrans = othersTrans.Find("win");
		winScore_Text = othersTrans.Find("win/Text").GetComponent<Text>();
		winTrans.gameObject.SetActive(false);
		moveup = othersTrans.Find("win").GetComponent<Animation>();//找到Animation组件

        imgmodel = othersTrans.Find("model").GetComponent<Image>();
        choumaTrans = othersTrans.Find("Chouma");
        choumaTrans.GetComponent<Image>().enabled = false;
        cricleImage = transform.Find("others/head").GetComponent<CircleImage>() ;
        allin = transform.Find("others/allin").gameObject;

        gold = transform.Find("others/Gold/Text").GetComponent<Text>();
        chouma = transform.Find("others/Text").GetComponent<Text>();
        playerName = transform.Find("others/Name").GetComponent<Text>();
        iconDaojishi = transform.Find("others/IconDaojishi").GetComponent<IconCircle>();
        d = transform.Find("others/D");
        imageBtn = transform.Find("others/Image").GetComponent<Button>();

        imageBtn.onClick.AddListener(() =>
        {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            GameUIManager.GetSingleton().playerInfoPopup.ShowView(userID);
            NetMngr.GetSingleton().Send(InterfaceGame.queryChatDiamond, new object[] {});

      //      GameUIManager.GetSingleton().playerInfoPopup.setPosition( imageBtn.transform.localPosition, imageBtn.transform.position);
        });

        othersTrans.gameObject.SetActive(false);
        imgmodel.gameObject.SetActive(false);
        paixingTrans.gameObject.SetActive(false);
        // KBTEST
        //btnsit.gameObject.SetActive(false);
        //othersTrans.gameObject.SetActive(true);
        setAllin(false);
    }
    private void Start()
    {
        OneGameStartResetUI(false);
    }
    // 播放积分特效
    public void PlayWinEffect()
    {
        winEffectTrans.gameObject.SetActive(true);
        CancelInvoke("hideWinEffect");
        Invoke("hideWinEffect",2f);

		winTrans.gameObject.SetActive (true);
        if(userID == StaticData.ID){
            SoundManager.GetSingleton().PlaySound("Audio/win");            
        }
		moveup.Play();
		CancelInvoke("hideWinAni");
		Invoke("hideWinAni",1f);
    }

	public void SetWinScore(string score)
	{
		winScore_Text.text = score.ToString ();
	}

	public void PlayWinAction()
	{
		winTrans.Translate(Vector3.up * 3 * Time.deltaTime);
	}

    public void setPaixingText(bool b,string tex="")
    {
        setModel(false);
        paixingTrans.gameObject.SetActive(b);
        paixingTranstext.text = tex;
    }
    void hideWinEffect()
    {
        winEffectTrans.gameObject.SetActive(false);
    }

	void hideWinAni()
	{
		winTrans.gameObject.SetActive(false);
	}

    public void oneLeaveSeat()
    {
        this.gameObject.SetActive(true);
        othersTrans.gameObject.SetActive(false);
        btnsit.gameObject.SetActive(true);
        userID = string.Empty;
    }
    // 点击坐下
    private void sitCallback()
    {
        Debug.Log("roomFirstGetIn " + GameManager.GetSingleton().roomFirstGetIn);
        // 通知后端
        if (GameManager.GetSingleton().roomFirstGetIn)
        {
           // GameManager.GetSingleton().roomFirstGetIn = false;
            GameUIManager.GetSingleton()._mjifenpai.ShowView();
            GameUIManager.GetSingleton()._mjifenpai.showInfo(int.Parse(this.gameObject.name));
        }
        else
        {
            NetMngr.GetSingleton().Send(InterfaceGame.DesktopPlayerSitdownRequest, new object[] {int.Parse(this.gameObject.name),-1 });
        }
      
    }
    /// <summary>
    /// 显示Allin
    /// </summary>
    public void setAllin(bool b)
    {
        allin.SetActive(b);
    }
	/// <summary>
    /// 玩家倒计时开启
    /// </summary>
    public void Daojishi_Start(float time)
    {
        iconDaojishi.time = time;
        iconDaojishi.Play();
    }
    /// <summary>
    /// 倒计时关闭
    /// </summary>
    public void Daojishi_Stop()
    {
        iconDaojishi.Stop();
    }
    public void RestUIPlayer()
    {
        btnsit.gameObject.SetActive(true);
        othersTrans.gameObject.SetActive(false);
    }
    public void setLeftMoney(string newstr)
    {
        gold.color = new Color(1, 1, 1, 1f);
        gold.text = newstr;
    }
    //设置抓头
    public void showZhuaTou()
    {

    }
    
    string tempMoney = "";
    Coroutine t;
    //留座
    public void KeepSeat(int time_)
    {
        tempMoney = gold.text;
        t = StartCoroutine(UpdateSeatTime(time_));
    }
    IEnumerator UpdateSeatTime(int t)
    {
        while (t >= 0)
        {
            gold.text = "留座中"+t+"s" ;
            yield return new WaitForSeconds(1f);
            t--;
        }

    }
    // 玩家留座返回游戏
    public void BackToSeat()
    {
        StopCoroutine(t);
        gold.text = tempMoney;
        gold.color = new Color(1, 1, 1, 1f);
    }
    public void KeepWaitForAccess()
    {
       // tempMoney = gold.text;
       // gold.text = "等待中";
    }
    public void BackToAccess()
    {
      //  gold.text = tempMoney;
    }
    public void setTuoGuan(bool h)
    {
        tuoguanTrans.gameObject.SetActive(h);
    }
    public void OneGameStartResetUI(bool b=false)
    {
        setModel(b);
        setAllin(b);
        SetNewChouma("");
        setZhuangInfo(false);
        setPaixingText(false);
        //if()
        Daojishi_Stop();
        // 筹码
        Transform tempStart = transform.Find("others/Chouma");
        while (tempStart.childCount > 0)
        {
            Transform tt = tempStart.GetChild(0);
            tt.SetParent(Paicontrol.GetInstance().zhumaPool);
            tt.localPosition = Vector3.zero;
        }
    }
    public void setModel(bool b)
    {
       // Debug.Log("被彩印厂了");
        imgmodel.gameObject.SetActive(b);
    }
    public void showOperaModel(string img)
    {
       // Debug.Log("===="+img);
        // 只标记前4个状态
        //if (int.Parse(img) < 4)
        //{
            imgmodel.gameObject.SetActive(true);
            imgmodel.overrideSprite = Resources.Load<Sprite>("model/" + img) as Sprite;
        //}
    }
    /// <summary>
    /// 增加筹码
    /// </summary>
    /// <param name="count"></param>
    public void SetNewChouma(string count)
    {
        //chouma.text = (int.Parse(chouma.text) + count).ToString();
        if (count == "0")
        {
            chouma.text = "";
        }
        else
        {
            chouma.text = count;
        }
    }
    /// <summary>
    /// 显示庄家
    /// </summary>
    public void setZhuangInfo(bool b)
    {
        d.gameObject.SetActive(b);
    }
    public  void showHeadImage(bool b)
    {
      //  cricleImage.color = new Color(1, 1, 1, 1);
        cricleImage.gameObject.SetActive(b);
        imageBtn.gameObject.SetActive(b);
    }
    public void setHeadDark(bool b=true)
    {
        if(b)
        {
            cricleImage.color = new Color(1, 1, 1, 0.4f);
            for(int i=0;i< choumaTrans.childCount; i++)
            {
                choumaTrans.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
            }
            gold.color=new Color(1, 1, 1, 0.4f);

        }
        else
        {
            cricleImage.color = new Color(1, 1, 1, 1);
            gold.color = new Color(1, 1, 1, 1f);

        }
    }
    // 设置玩家数据
    public void setPlayinfo(Hashtable h)
    {
        showHeadImage(true);
        gold.color = new Color(1, 1, 1, 1f);
        cricleImage.color = new Color(1, 1, 1, 1);
        btnsit.gameObject.SetActive(false);
        othersTrans.gameObject.SetActive(true);
        d.gameObject.SetActive(false);
        gold.text = h["playCoin"].ToString();
        loadImageFromURL(h["headURL"].ToString(),cricleImage);
        playerName.text = h["playName"].ToString();
        if (!StaticData.isGuanzhan && int.Parse(this.gameObject.name) == GameManager.GetSingleton().myNetID)
        {
            playerName.gameObject.SetActive(false);
        }
        else
            playerName.gameObject.SetActive(true);
        userID = h["id"].ToString();

        int netId = int.Parse(h["netID"].ToString());

        //如果弃牌，头像置灰
        if (netId == GameManager.GetSingleton().myNetID)
        {
            if (int.Parse(h["isInGame"].ToString()) == 0)
            {
                setHeadDark(true);
            }
        }

    }
    public void setAlreadySeatPlayinfo(string t)
    {
        SetNewChouma(t);
    }
    public void loadImageFromURL(string url, CircleImage headImg)
    {

       // Debug.Log(this.gameObject.activeInHierarchy+"  ===   "+headImg.gameObject.activeInHierarchy);
       // StartCoroutine(url, headImg);
      //  Debug.Log(this.gameObject.activeInHierarchy + "  =222==   " + headImg.gameObject.activeInHierarchy);
         GameTools.GetSingleton().GetTextureFromNet(url, T1est);
    }
    public void  T1est(Sprite s)
    {
        cricleImage.overrideSprite = s;
    }
    IEnumerator LoadImage(string url, CircleImage headImg)
    {
        if (url != "")
        {
            Debug.Log(this.gameObject.activeInHierarchy + "  ==2222=   " + headImg.gameObject.activeInHierarchy);
            WWW www = new WWW(url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            }
            headImg.overrideSprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
        }
    }


}
