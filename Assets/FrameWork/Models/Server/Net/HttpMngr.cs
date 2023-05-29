using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class HttpMngr : MonoBehaviour
{
    private static HttpMngr mng;

    public static HttpMngr GetSingleton()
    {
        return mng;
    }

    public string url;

    private void Awake()
    {
        if (mng == null)
            mng = this;

        DontDestroyOnLoad(gameObject);
    }

    /*
     * Get请求，使用默认url，interfaceName为接口名,getData为参数列表，callBack为请求回调
     */
    public void Get(string interfaceName, Hashtable getData, Action<Hashtable> callBack = null)
    {
        StartCoroutine(GetCortonine(url + interfaceName, getData, callBack));
    }

    /*
     * Get请求，自定义url，url为接口名，getData为参数列表，callBack为请求回调
     */
    public void GetWithUrl(string url, Hashtable getData, Action<Hashtable> callBack = null)
    {
        StartCoroutine(GetCortonine(url, getData, callBack));
    }

    private IEnumerator GetCortonine(string url, Hashtable getData, Action<Hashtable> callBack = null)
    {
        url += "?";
        foreach (DictionaryEntry getArg in getData)
        {
            url += getArg.Key + "=" + getArg.Value + "&";
        }
        url = url.Substring(0, url.Length - 1);

        Debug.Log("发送Get请求:" + url);
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Http Get错误:" + www.error);
        }
        else
        {
            Debug.Log("收到Get请求返回:" + www.text);

            if (callBack != null)
            {
                Hashtable data = TinyJSON.jsonDecode(www.text) as Hashtable;
                callBack(data);
            }
        }
    }

    /*
     * Post请求，使用默认url，interfaceName为接口名,postData为参数列表，callBack为请求回调
     */
    public void Post(string interfaceName, Hashtable postData, Action<Hashtable> callBack = null)
    {
        StartCoroutine(PostCortonine(url + interfaceName, postData, callBack));
    }

    /*
     * Post请求，自定义url，url为接口名，postData为参数列表，callBack为请求回调
     */
    public void PostWithUrl(string url, Hashtable postData, Action<Hashtable> callBack = null)
    {
        StartCoroutine(PostCortonine(url, postData, callBack));
    }

    /*
     * Post二进制请求，使用默认url，interfaceName为接口名,postData为参数列表，callBack为请求回调
     */
    public void PostBytes(string interfaceName, byte[] postData, Action<Hashtable> callBack = null)
    {
        StartCoroutine(PostByteCortonine(url + interfaceName, postData, callBack));
    }

    /*
     * Post二进制请求，自定义url，url为接口名，postData为参数列表，callBack为请求回调
     */
    public void PostBytesWithUrl(string url, byte[] postData, Action<Hashtable> callBack = null)
    {
        StartCoroutine(PostByteCortonine(url, postData, callBack));
    }

    private IEnumerator PostCortonine(string url, Hashtable postData, Action<Hashtable> callBack = null)
    {
        WWWForm form = new WWWForm();
        foreach (DictionaryEntry postArg in postData)
        {
            form.AddField(postArg.Key.ToString(), postArg.Value.ToString());
        }

        WWW www = new WWW(url, form);
        yield return www;

        Debug.Log("发送Post请求:" + TinyJSON.jsonEncode(postData));
        if (www.error != null)
        {
            Debug.Log("Http Post错误:" + www.error);
        }
        else
        {
            Debug.Log("收到Post请求返回:" + www.text);

            if (callBack != null)
            {
                Hashtable data = TinyJSON.jsonDecode(www.text) as Hashtable;
                callBack(data);
            }
        }
    }

    private IEnumerator PostByteCortonine(string url, byte[] postData, Action<Hashtable> callBack = null)
    {
        WWW www = new WWW(url, postData);
        yield return www;

        Debug.Log("发送二进制Post请求，长度:" + postData.Length);
        if (www.error != null)
        {
            Debug.Log("Http Post错误:" + www.error);
        }
        else
        {
            Debug.Log("收到Post请求返回:" + www.text);

            if (callBack != null)
            {
                Hashtable data = TinyJSON.jsonDecode(www.text) as Hashtable;
                callBack(data);
            }
        }    
    }
}
