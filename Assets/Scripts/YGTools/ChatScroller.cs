using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatScroller : MonoBehaviour
{

    /// <summary>
    /// 数据长度
    /// </summary>
    public int DataLength
    {
        get { return datas.Count; }
    }

    private RectTransform selfRt;
    private RectTransform canvasRt;
    private RectTransform contentRt;
    private Scrollbar scrollbar;

    public List<Hashtable> datas;
    private List<Vector2> posAndHeightList;
    public static Dictionary<string, Texture2D> headList;

    //用于计算高度和坐标
    private ItemChatScroller tempItem;
    
    private int minShowIndex = 0;
    private int maxShowIndex = 0;
    public Dictionary<int, GameObject> modelList; 

    private GameObjectPool pool;
    private Transform poolTf;
    public List<string> topURL;
    public List<string> AllURL;
    private void Awake()
    {
        topURL = new List<string>();
        AllURL = new List<string>();
        selfRt = gameObject.GetComponent<RectTransform>();
        canvasRt = GameObject.Find("Canvas").GetComponent<RectTransform>();
        scrollbar = transform.Find("Scrollbar").GetComponent<Scrollbar>();

        poolTf = transform.Find("Pool");
        contentRt = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        posAndHeightList = new List<Vector2>();
        modelList = new Dictionary<int, GameObject>();

        datas = new List<Hashtable>();
        if(headList == null)
            headList = new Dictionary<string, Texture2D>();

        pool = new GameObjectPool(Resources.Load<GameObject>("Prefab/Type"));
        tempItem = pool.Get().GetComponent<ItemChatScroller>();
        tempItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
        InvokeRepeating("KBClearHeadTexture", 600, 600);

    }

    private void Update()
    {
        RefreshShow();
    }
    /// <summary>
    /// 清空列表
    /// </summary>
    public void ClearList()
    {
		if (contentRt == null)
			return;
        int childCount = contentRt.childCount;
        if (contentRt.childCount == 0)
            return;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(contentRt.GetChild(i).gameObject);
        }
    }

    public List<Hashtable> GetDatas()
    {
        return datas;
    }
    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="data"></param>
    public void SetDatas(List<Hashtable> data)
    {
        datas = data;
        Refresh();
    }
    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="data"></param>
    public void SetDatas(int index,List<Hashtable> data)
    {
        //for (int i = data.Count-1; i >= 0; i--)
        //{
        //    datas.Insert(index, data[i]);
        //}
        for (int i = 0; i < data.Count; i++)
        {
            datas.Insert(datas.Count, data[i]);
        }
        Refresh();
    }

    /// <summary>
    /// 增加数据
    /// </summary>
    /// <param name="index">下标</param>
    /// <param name="data">数据</param>
    public void AddDataAt(int index, Hashtable data)
    {
        datas.Insert(index, data);
        Debug.Log("当前列表存放的数据个数："+datas.Count);
        if (datas.Count >= 130)
            RemoveDataAt(0);
        Refresh();
    }

    /// <summary>
    /// 移除数据
    /// </summary>
    /// <param name="index">下标</param>
    public void RemoveDataAt(int index)
    {
        datas.RemoveAt(index);
        Refresh();
    }

    /// <summary>
    /// 移动到最后
    /// </summary>
    public void MoveToEnd()
    {
        StartCoroutine(DelayMoveToEnd());
    }
    public void KBClearHeadTexture()
    {
        if (headList == null)
            return;
        Debug.Log("清理多余头像=====");
        if (topURL.Count > 0)
            topURL.Clear();
        if (AllURL.Count > 0)
            AllURL.Clear();
        for(int i = 0; i < datas.Count; i++)
        {
            if (datas[i].Contains("HeadUrl"))
            {
                topURL.Add(datas[i]["HeadUrl"].ToString());
            }
        }
        foreach (KeyValuePair<string,Texture2D> item in headList)
        {
            AllURL.Add(item.Key);
        }
        for(int j = 0; j < AllURL.Count; j++)
        {
            if (!topURL.Contains(AllURL[j]))
            {
                Destroy(headList[AllURL[j]]);
                headList.Remove(AllURL[j]);
            }
        }
       
        Resources.UnloadUnusedAssets();
    }
    /// <summary>
    /// 清理头像
    /// </summary>
    /// <param name="url">头像地址</param>
    public void ClearHeadTexture(string url)
    {
        if(headList == null)
            return;

        if (!headList.ContainsKey(url))
        {
            Destroy(headList[url]);
            headList.Remove(url);
        }

        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 清理头像
    /// </summary>
    /// <param name="url">头像地址</param>
    public void ClearHeadTexture(List<string> url)
    {
        if (headList == null)
            return;
        if(url == null || url.Count == 0)
            return;

        StartCoroutine(ClearHeadTextureCorotine(url));
    }

    private IEnumerator ClearHeadTextureCorotine(List<string> url)
    {
        for (int i = 0; i < url.Count; i++)
        {
            if (headList.ContainsKey(url[i]))
            {
                Destroy(headList[url[i]]);
                headList.Remove(url[i]);
            }
            yield return null;
        }
        Resources.UnloadUnusedAssets();
    }

    private IEnumerator DelayMoveToEnd()
    {
        yield return null;
        scrollbar.value = 0;
    }

    private void Refresh()
    {
		if (!this.gameObject.activeInHierarchy)
			return;
		
        posAndHeightList.Clear();
        float tempPosY = 0;
        for (int i = 0; i < datas.Count; i++)
        {
            tempItem.SetData(datas[i]);
            posAndHeightList.Add(new Vector2(tempPosY, tempItem.GetHeight()));
            tempPosY -= tempItem.GetHeight();
        }
        contentRt.sizeDelta = new Vector2(contentRt.sizeDelta.x, tempPosY * -1);

        StartCoroutine(DelayRefreshShow());
    }

    private IEnumerator DelayRefreshShow()
    {
        yield return null;
        RefreshShow();
    }

    private void RefreshShow()
    {
        float scrollerHeight = canvasRt.sizeDelta.y + selfRt.offsetMax.y - selfRt.offsetMin.y;
        float showStartPosY = contentRt.anchoredPosition.y * -1;
        float showEndPosY = showStartPosY - scrollerHeight;

        int newMinShowIndex = minShowIndex;
        int newMaxShowIndex = maxShowIndex;

        if (newMinShowIndex >= datas.Count)
            newMinShowIndex = datas.Count - 1;
        if (newMaxShowIndex >= datas.Count)
            newMaxShowIndex = datas.Count - 1;

        if (posAndHeightList.Count == 0) return;
        if (posAndHeightList[newMinShowIndex].x < showStartPosY)
        {
            for (int i = newMinShowIndex; i >= 0; i--)
            {
                if (posAndHeightList[i].x < showStartPosY)
                    continue;
                else if (posAndHeightList[i].x >= showStartPosY || i == 0)
                {
                    newMinShowIndex = i;
                    break;
                }
            }
        }
        else
        {
            for (int i = newMinShowIndex; i < datas.Count; i++)
            {
                if (posAndHeightList[i].x > showStartPosY)
                    continue;
                else
                {
                    newMinShowIndex = i - 1;
                    newMinShowIndex = newMinShowIndex < 0 ? 0 : newMinShowIndex;
                    break;
                }
            }
        }

        if (posAndHeightList[newMaxShowIndex].x > showEndPosY)
        {
            for (int i = newMaxShowIndex; i < datas.Count; i++)
            {
                if (posAndHeightList[i].x > showEndPosY)
                {
                    newMaxShowIndex = i;
                    continue;
                }
                else if (posAndHeightList[i].x <= showEndPosY || i == datas.Count - 1)
                {
                    newMaxShowIndex = i;
                    break;
                }
            }
        }
        else
        {
            for (int i = newMaxShowIndex; i >= 0; i--)
            {
                if (posAndHeightList[i].x < showEndPosY)
                    continue;
                else
                {
                    newMaxShowIndex = i + 1;
                    newMaxShowIndex = newMaxShowIndex >= datas.Count ? datas.Count - 1 : newMaxShowIndex;
                    break;
                }
            }
        }

        int minShowIndex1 = newMinShowIndex < minShowIndex ? newMinShowIndex : minShowIndex;
        int maxShowIndex1 = newMaxShowIndex > maxShowIndex ? newMaxShowIndex : maxShowIndex;

        for (int i = minShowIndex1; i <= maxShowIndex1; i++)
        {
            if (modelList.ContainsKey(i))
            {
                if (i < newMinShowIndex || i > newMaxShowIndex)
                {
                    pool.Put(modelList[i]);
                    modelList[i].SetActive(false);
                    modelList[i].transform.SetParent(poolTf);
                    modelList.Remove(i);
                }
                else
                {
                    modelList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, posAndHeightList[i].x);
                    modelList[i].GetComponent<ItemChatScroller>().SetData(datas[i]);
                }
            }
            else
            {
                if (i >= newMinShowIndex && i <= newMaxShowIndex)
                {
                    GameObject go = pool.Get();
                    go.SetActive(true);
                    go.transform.SetParent(contentRt);
                    go.transform.localScale = Vector3.one;
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, posAndHeightList[i].x);
                    go.GetComponent<ItemChatScroller>().SetData(datas[i]);
                    modelList[i] = go;
                }
            }
        }

        minShowIndex = newMinShowIndex;
        maxShowIndex = newMaxShowIndex;
    }

    private void OnDestroy()
    {
        CancelInvoke();
        headList.Clear();
        Resources.UnloadUnusedAssets();
    }

}