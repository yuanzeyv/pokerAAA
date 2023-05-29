using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public delegate void VoidDelegate();
public delegate void FloatDelegate(float delta);
public delegate void StringDelegate(string str);
public delegate void IntArrayDelegate(params int[] a);
public delegate void ObjectDelegate(object o);
public static class GlobalTool
{
    public static GameObject FindChildByName(this GameObject go, string name) {
        Transform tran = go.transform;
        for (int i = 0; i < tran.childCount; i++)
        {
            if (tran.GetChild(i).name.Equals(name)) {
                return tran.GetChild(i).gameObject;
            }
        } return null;
    }
    public static Transform FindGameByName(this Transform tran, string name)
    {
        for (int i = 0; i < tran.childCount; i++)
        {
            if (tran.GetChild(i).name.Equals(name))
            {
                return tran.GetChild(i);
            }
        }
        return null;
    }
    const string UIPath = "Prefab/UI/";
    public static T LoadResouce<T>(GameObject parent = null)
    {
        string Path = UIPath + typeof(T);
        GameObject res = Resources.Load(Path) as GameObject;
        GameObject temp = UnityEngine.Object.Instantiate(res) as GameObject;
        if (parent != null)
        {

            temp.transform.SetParent(parent.transform);
        }

        return temp.GetComponent<T>();

    }
    // 三公
    public static T LoadResouce<T>(GameObject parent, string path = null)
    {
        string prefabPath;
        if (path == null)
        {
            prefabPath = UIPath + typeof(T);
        }
        else
            prefabPath = path;
        GameObject res = Resources.Load(prefabPath) as GameObject;
        GameObject temp = UnityEngine.Object.Instantiate(res) as GameObject;
        if (parent != null)
        {

            temp.transform.SetParent(parent.transform, false);
        }
        return temp.GetComponent<T>();
    }

    public static bool FileIsExist(string path,int type=0)
    {
        if (type == 0)
        {
            return Directory.Exists(path);
        }
        else if(type==1){
             return File.Exists(path);
        }
        return false;
    }
    /// <summary>
    /// 文件删除
    /// </summary>
    /// <param name="path"></param>
    public static void FileDelete(string path)
    {
        if (FileIsExist(path,1))
        {
            File.Delete(path);
        }
    }
    public static void CreateFolder(string path)
    {
        Directory.CreateDirectory(path);
    }
    //public static bool WriteJsonFile(string path,string content) {
    //    try
    //    {
    //        File.WriteAllText(path, content);
    //    }
    //    catch {
    //        Debug.Log("Cannt Write");
    //        return false;
    //    }
    //    return true;
    //}
    public static bool WriteJsonFile(string path, byte[] content)
    {
        try
        {
            FileStream sw = File.Create(path);
            sw.Write(content,0,content.Length);
            sw.Close();
        }
        catch
        {
            Debug.Log("Cannt Write");
            return false;
        }
        return true;
    }
    public static bool JudgeFileIsHave(string path) {
        return Directory.Exists(path);
    }

}