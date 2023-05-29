/**
 * 游戏对象池
 * 说明：
 *     提供了动态生成Unity预设的对象池管理
 * 使用方法：
 *     1、实例化对象池，可传入创建对象时初始化方法，重用对象时初始化方法
 *     2、使用Get()方法从对象池取对象，使用Put()把对象放回对象池
 *     3、若对象池内无对象则创建新对象并调用初始化方法
 *     4、若对象池有对象则从对象池内取出对象并重置方法
 *     5、使用Clear(int retainCount)方法可清理对象池内对象并保留部分对象
 *     6、使用ClearAll()方法可清理所有对象
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool
{
    private GameObject prefab;
    private List<GameObject> objectList;
    private Action<GameObject> initAction;
    private Action<GameObject> resetAction;

    /// <summary>
    /// 对象池长度
    /// </summary>
    public int Count
    {
        get
        {
            if (objectList != null)
                return objectList.Count;
            else
                return -1;
        }
    }

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="prefab">游戏对象的预设</param>
    /// <param name="initAction">初始化方法</param>
    /// <param name="resetAction">重置方法</param>
    public GameObjectPool(GameObject prefab, Action<GameObject> initAction = null, Action<GameObject> resetAction = null)
    {
        this.prefab = prefab;
        this.initAction = initAction;
        this.resetAction = resetAction;
        objectList = new List<GameObject>();
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <returns></returns>
    public GameObject Get()
    {
        if (objectList.Count == 0)
        {
            GameObject go = MonoBehaviour.Instantiate(prefab);
            if (initAction != null)
                initAction(go);
            return go;
        }
        else
        {
            GameObject go = objectList[0];
            objectList.RemoveAt(0);
            if (resetAction != null)
                resetAction(go);
            return go;
        }
    }

    /// <summary>
    /// 放入对象
    /// </summary>
    /// <param name="obj"></param>
    public void Put(GameObject obj)
    {
        if(obj != null)
            objectList.Add(obj);
    }

    /// <summary>
    /// 清理对象
    /// </summary>
    /// <param name="retainCount">保留对象数</param>
    public void Clear(int retainCount)
    {
        for (int i = objectList.Count - 1; i >= retainCount; i--)
        {
            MonoBehaviour.Destroy(objectList[i]);
            objectList.RemoveAt(i);
        }
    }

    /// <summary>
    /// 清空所有对象
    /// </summary>
    public void ClearAll()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            MonoBehaviour.Destroy(objectList[i]);
        }
        objectList.Clear();
    }
}
