/**
 * 对象池
 * 说明：
 *     提供了普通数据结构的对象池管理
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

public class ObjectPool<T> where T : class , new()
{
    private List<T> objectList;
    private Action<T> initAction;
    private Action<T> resetAction; 
    
    public ObjectPool(Action<T> initAction = null, Action<T> resetAction = null)
    {
        objectList = new List<T>();
        this.initAction = initAction;
        this.resetAction = resetAction;
    }


    /// <summary>
    /// 获取对象
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        if (objectList.Count == 0)
        {
            T obj = new T();
            if (initAction != null)
                initAction(obj);
            return obj;
        }
        else
        {
            T obj = objectList[0];
            objectList.RemoveAt(0);
            if (resetAction != null)
                resetAction(obj);
            return obj;
        }
    }

    /// <summary>
    /// 放入对象
    /// </summary>
    /// <param name="obj"></param>
    public void Put(T obj)
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
            objectList.RemoveAt(i);
        }
    }

    /// <summary>
    /// 清空所有对象
    /// </summary>
    public void ClearAll()
    {
        objectList.Clear();
    }
}
