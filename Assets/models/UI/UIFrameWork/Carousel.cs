using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Carousel : MonoBehaviour
{
    public static Carousel _instance;
    public static  Carousel GetInstance()
    {
        return _instance;
    }
    //滑动移动速度
    public float moveSpeed = 5;
    //是否自动滚屏
    public bool isAutoPlay = true;
    //滚屏时间间隔
    public float playTimespan = 5f;
    //触发翻页的滑动时间
    public float canMoveDragTimespan = 0.2f;

    private GameObject baseShowImage;
    private GameObject basePoint;

    private RectTransform imageContent;
    private GridLayoutGroup imageContentGridLayout;
    private Transform pointContent;
    private GridLayoutGroup pointContentGridLayout;
    private Scrollbar scrollbar;

    private CarouselCtrl carouselCtrl;
    
    public List<CarouselObj> carouselList;
    public List<RawImage> showImageList;
    public List<Toggle> pointList; 

    private void Awake()
    {
        _instance = this;
        baseShowImage = transform.Find("View/Content/ShowImage").gameObject;
        basePoint = transform.Find("PointContent/Point").gameObject;
        imageContent = transform.Find("View/Content").GetComponent<RectTransform>();
        imageContentGridLayout = imageContent.GetComponent<GridLayoutGroup>();
        pointContent = transform.Find("PointContent");
        pointContentGridLayout = pointContent.GetComponent<GridLayoutGroup>();
        scrollbar = transform.Find("Scrollbar").GetComponent<Scrollbar>();

        carouselCtrl = transform.Find("View").GetComponent<CarouselCtrl>();
        carouselCtrl.Init(this, scrollbar);

        //满屏显示图片
        RectTransform objRt = gameObject.GetComponent<RectTransform>();
        imageContentGridLayout.cellSize = new Vector2(objRt.sizeDelta.x, objRt.sizeDelta.y);

        baseShowImage.SetActive(false);
        basePoint.SetActive(false);

        carouselList = new List<CarouselObj>();
        showImageList = new List<RawImage>();
        pointList = new List<Toggle>();
        //Texture t1 = Resources.Load<Texture>("lunbo1");
        //Texture t2 = Resources.Load<Texture>("lunbo2");
        //AddTexture(t1, null, true);
        //AddTexture(t2, null, false);
        setINfo("https://www.baidu.com");
    }
    string url = "https://www.baidu.com";
    public void setINfo(string text)
    {
        //if (StaticData.isLoadLunBo) return;
        if (imageContent.transform.childCount >= 2) return;
        this.url = text;
        Texture t1 = Resources.Load<Texture>("舞会1");
        Texture t2 = Resources.Load<Texture>("舞会1");

        AddTexture(t1, null, true);
        AddTexture(t2, null, false);
        AddTexture(t2, null, false);
        AddTexture(t1, null, true);
        AddTexture(t2, null, true);
        // AddTexture(t2, null, false);
        // StaticData.isLoadLunBo = true;
        //修改baseCanvas的canvas

        GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;


    }
    /*
     * 增加图片，texture为显示图片，callback为点击回调，初始化时调用
     */
    public void AddTexture(Texture texture, Action<Texture> callback = null, bool b=false)
    {
      
        carouselList.Add(new CarouselObj(texture, callback));

        GameObject imageGo = Instantiate(baseShowImage);
        imageGo.SetActive(true);
        imageGo.transform.SetParent(imageContent.transform);
        imageGo.transform.localScale = Vector3.one;
        if (b)
        {
          
            imageGo.FindChildByName("Text").SetActive(true);
            imageGo.FindChildByName("Image").SetActive(false);
            imageGo.FindChildByName("copy").SetActive(true);
            imageGo.FindChildByName("copy").GetComponent<Button>().onClick.AddListener(delegate {
               // UniClipboard.SetText(this.url);
               // if (UIManager.GetSingleton() != null)
                 //   UIManager.GetSingleton().popupCommon.ShowView("复制成功！", false, null);
               // if (UIController.GetSingleton() != null)
                  //  UIController.GetSingleton().popupCommon.ShowView("复制成功！", false, null);
            });
        
            imageGo.FindChildByName("Text").GetComponent<Text>().text = this.url;
        }
        else
        {
            imageGo.FindChildByName("Text").SetActive(false);
            imageGo.FindChildByName("Image").SetActive(true);
           // Texture2D t2d = DrawQRCode.DrawCode_Texture2D(url);
            imageGo.FindChildByName("copy").SetActive(false);
           // imageGo.transform.Find("Image/RawImage").GetComponent<RawImage>().texture = t2d;
        }
        RawImage rawImage = imageGo.GetComponent<RawImage>();
        rawImage.texture = texture;
        showImageList.Add(rawImage);
        imageContent.sizeDelta = new Vector2(imageContentGridLayout.cellSize.x * carouselList.Count, imageContentGridLayout.cellSize.y);

        GameObject pointGo = Instantiate(basePoint);
        pointGo.SetActive(true);
        pointGo.transform.SetParent(pointContent);
        pointGo.transform.localScale = Vector3.one;
        Toggle toggle = pointGo.GetComponent<Toggle>();
        toggle.isOn = false;
        pointList.Add(toggle);
        Debug.Log("3333333333");
    }
    /*
     * 移除图片，index为图片的索引
     */
    public void RemoveTexture(int index)
    {
        if(index < 0 || index >= carouselList.Count)
            return;
        Destroy(showImageList[index].gameObject);
        Destroy(pointList[index].gameObject);
        carouselList.RemoveAt(index);
        showImageList.RemoveAt(index);
        pointList.RemoveAt(index);
        imageContent.sizeDelta = new Vector2(imageContentGridLayout.cellSize.x * carouselList.Count, imageContentGridLayout.cellSize.y);
        int newIndex = Mathf.Clamp(index, 0, carouselList.Count - 1);
        pointList[newIndex].isOn = true;
        carouselCtrl.MoveToIndex(newIndex);
    }

}

public class CarouselObj
{
    public Texture texture;
    public Action<Texture> callback;

    public CarouselObj(Texture texture, Action<Texture> callback)
    {
        this.texture = texture;
        this.callback = callback;
    }
}
