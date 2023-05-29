/**
 * 常用游戏工具类
 * 使用方法：
 *     1、编辑界面FrameWork->Create FrameWork Object建立框架对象
 *     2、GameTools.GetSingleton()获取单例
 *     3、调用你要使用的方法
 */

using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using UnityEngine.UI;

public class GameTools : MonoBehaviour {

    private static GameTools mng;

    public static GameTools GetSingleton()
    {
        return mng;
    }

    private void Awake()
    {
        if (mng == null)
            mng = this;
    }
   
    /// <summary>
    /// 保存截屏图片，并且刷新相册（Android和iOS）
    /// </summary>
    /// <param name="name">若空就按照时间命名</param>
    public void CaptureScreenshot(string name = "")
	{
		string _name = "";
		if (string.IsNullOrEmpty(name))
		{
			_name = "Screenshot_" + GetCurTime() + ".png";
		}
		//编辑器下
		//string path = Application.persistentDataPath + "/" + _name;
		//Application.CaptureScreenshot(path, 0);
		//EDebug.Log("图片保存地址" + path);


		//Android版本
		StartCoroutine(CutImage(_name));
	}

	//截屏并保存
	IEnumerator CutImage(string name)
	{

		//图片大小  
		string destination = "";
		if (Application.platform == RuntimePlatform.Android)
		{
			destination = "/mnt/sdcard/DCIM/Screenshots";
			if (!Directory.Exists(destination))
			{
				Directory.CreateDirectory(destination);
			}
			destination = destination + "/" + name;
		}
		Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
		yield return new WaitForEndOfFrame();
		tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
		tex.Apply();
		yield return tex;
		byte[] byt = tex.EncodeToPNG();
		string path = Application.persistentDataPath.Substring(0, Application.persistentDataPath.IndexOf("Android"));
		File.WriteAllBytes(destination, byt);
		string[] paths = new string[1];
		paths[0] = destination;
		ScanFile(paths);

		Tip.Instance.showMsg("保存到相册成功");
	}
	//刷新图片，显示到相册中
	void ScanFile(string[] path)
	{
		using (AndroidJavaClass PlayerActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			AndroidJavaObject playerActivity = PlayerActivity.GetStatic<AndroidJavaObject>("currentActivity");
			using (AndroidJavaObject Conn = new AndroidJavaObject("android.media.MediaScannerConnection", playerActivity, null))
			{
				Conn.CallStatic("scanFile", playerActivity, path, null, null);
			}
		}
	}
	/// <summary>
	/// 获取当前年月日时分秒，如201803081916
	/// </summary>
	/// <returns></returns>
	string GetCurTime()
	{
		return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
			+ DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
	}

    /// <summary>
    /// 截图
    /// </summary>
    /// <param name="x">开始x坐标</param>
    /// <param name="y">开始y坐标</param>
    /// <param name="width">图片宽度</param>
    /// <param name="height">图片高度</param>
    /// <param name="callback">回调</param>
    public void PrintScreenTexture(int x, int y, int width, int height, Action<Texture2D> callback)
    {
        StartCoroutine(PrintScreenTextureCoroutine(x, y, width, height, callback));
    }

    private IEnumerator PrintScreenTextureCoroutine(int x, int y, int width, int height, Action<Texture2D> callback)
    {
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(x, y, width, height), 0, 0);
        tex.Apply();
        if (callback != null)
            callback(tex);
    }

    /// <summary>
    /// 截图
    /// </summary>
    /// <param name="x">起始x坐标</param>
    /// <param name="y">起始y坐标</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    /// <returns></returns>
    public void PrintScreenTexture(int x, int y, int width, int height, Action<Sprite> callback)
    {
        StartCoroutine(PrintScreenTextureCoroutine(x, y, width, height, callback));
    }

    private IEnumerator PrintScreenTextureCoroutine(int x, int y, int width, int height, Action<Sprite> callback)
    {
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(x, y, width, height), 0, 0);
        tex.Apply();
        Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        if (callback != null)
            callback(s);
    }

    /// <summary>
    /// 修改图片宽高
    /// </summary>
    /// <param name="source">原图</param>
    /// <param name="targetWidth">目标宽度</param>
    /// <param name="targetHeight">目标高度</param>
    /// <returns></returns>
    public Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                result.SetPixel(j, i, source.GetPixelBilinear(j / (float)result.width, i / (float)result.height));
            }
        }
        result.Apply();
        return result;
    }

    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="path">保存文件的完整地址</param>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public bool SaveFileToLocal(string path, byte[] file)
    {
        try
        {
            string filePath = path.Substring(0, path.LastIndexOf('/') + 1);
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            File.WriteAllBytes(path, file);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 加载文件
    /// </summary>
    /// <param name="path">文件的完整路径</param>
    /// <returns></returns>
    public byte[] LoadFileFromLocal(string path)
    {
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int) fs.Length);
            fs.Close();
            fs.Dispose();
            return bytes;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 加载网络图片
    /// </summary>
    /// <param name="url">图片地址</param>
    /// <param name="callback">加载完成回调</param>
    public void GetTextureFromNet(string url, System.Action<Texture2D> callback)
    {
        if(callback == null)
            return;
        StartCoroutine(LoadTexture(url, callback));
    }

    private IEnumerator LoadTexture(string url, System.Action<Texture2D> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("加载图片失败：" + url);
        }
        else
        {
            callback(Instantiate(www.texture));
        }
        www.Dispose();
    }

    /// <summary>
    /// 加载网络图片
    /// </summary>
    /// <param name="url">图片地址</param>
    /// <param name="callback">加载完成回调</param>
    public void GetTextureFromNet(string url, System.Action<Sprite> callback)
    {
        if (callback == null)
            return;
        StartCoroutine(LoadTexture(url, callback));
    }

    private IEnumerator LoadTexture(string url, System.Action<Sprite> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("加载图片失败：" + url);
        }
        else
        {
          //  Debug.Log("加载图片失败：" + url);
            Sprite s = Sprite.Create(Instantiate(www.texture), new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            yield return s;
            if (callback != null)
            {
                callback(s);
            }
            else
            {
                Debug.Log("回调函数为空：" + url);
            }
            
        }
        www.Dispose();
    }

    public void GetTextureNet(string url, System.Action<Texture> callback)
    {
        if (callback == null)
            return;
        StartCoroutine(LoadTexture(url, callback));
    }

    private IEnumerator LoadTexture(string url, System.Action<Texture> callback)
    {
        url = url.Trim();
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("加载图片失败：" + url + " " + www.error);
        }
        else
        {
            Texture s = www.texture;
            callback(s);
        }
        www.Dispose();
    }

    /// <summary>
    /// 加载JSON文件
    /// </summary>
    /// <param name="url">文件地址</param>
    /// <param name="callback">加载完成回调</param>
    /// <param name="fromLocal">是否从本地加载</param>
    public void LoadJson(string url, Action<Hashtable> callback, bool fromLocal = false)
    {
        if(string.IsNullOrEmpty(url)|| callback == null)
            return;
        if (fromLocal && !(url.Contains("file://") || url.Contains("file:///")))
            url = "file:///" + url;
        StartCoroutine(LoadJsonCoroutine(url, callback));
    }

    private IEnumerator LoadJsonCoroutine(string url, Action<Hashtable> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
            Debug.Log("加载JSON失败：" + www.error);
        else
        {
            try
            {
                Hashtable data = TinyJSON.jsonDecode(www.text) as Hashtable;
                callback(data);
            }
            catch
            {
                Debug.Log("JSON格式错误");
            }
        }
        www.Dispose();
    }

    /// <summary>
    /// 加载JSON文件
    /// </summary>
    /// <param name="url">文件地址</param>
    /// <param name="callback">加载完成回调</param>
    /// <param name="fromLocal">是否从本地加载</param>
    public void LoadJson(string url, Action<string> callback, bool fromLocal = false)
    {
        if (string.IsNullOrEmpty(url) || callback == null)
            return;
        if (fromLocal && !(url.Contains("file://") || url.Contains("file:///")))
            url = "file:///" + url;
        StartCoroutine(LoadJsonCoroutine(url, callback));
    }

    private IEnumerator LoadJsonCoroutine(string url, Action<string> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
            Debug.Log("加载JSON失败：" + www.error);
        else
        {
            try
            {
                string s = www.text;
                callback(s);
            }
            catch
            {
                Debug.Log("JSON格式错误");
            }
        }
        www.Dispose();
    }

    /// <summary>
    /// 保存json文件到本地
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="json">json数据</param>
    /// <returns></returns>
    public bool SaveJsonToLocal(string path, string json)
    {
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(json);
        bool result = SaveFileToLocal(path, bytes);
        return result;
    }

    /// <summary>
    /// 保存json文件到本地
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="jsonData">json数据</param>
    /// <returns></returns>
    public bool SaveJsonToLocal(string path, Hashtable jsonData)
    {
        string s = TinyJSON.jsonEncode(jsonData);
        bool result = SaveJsonToLocal(path, s);
        return result;
    }

	public System.DateTime ConvertLongToDateTime( long timeStamp )
	{
		System.DateTime dtStart = System.TimeZone.CurrentTimeZone.ToLocalTime( new System.DateTime( 1970, 1, 1 ) );
		long lTime = long.Parse( timeStamp + "0000000" );
		System.TimeSpan toNow = new System.TimeSpan( lTime );
		return dtStart.Add( toNow );
	}

	public long ConvertDateTimeToLong( System.DateTime time )
	{
		System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime( new System.DateTime( 1970, 1, 1 ) );
		return (long)( time - startTime ).TotalSeconds;
	}
    public void stopGameToolsAllCoroutines()
    {
        StopAllCoroutines();
    }

    float GetSafearea(){
        if(Application.platform == RuntimePlatform.IPhonePlayer){ // ios
            return Screen.safeArea.y;
        } else if(Application.platform == RuntimePlatform.Android){ // android
            return 0f;
        } else {
            return 0f;
        }
    }

    public void AdaptSafearea(Transform tr){
        float safeArea = GetSafearea();
        tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y - safeArea, tr.localPosition.z);
    }

    public GameObject GetSvCell(Transform parent)
    {
        for(int i=0; i < parent.childCount; i++){
            GameObject obj = parent.GetChild(i).gameObject;
            if(!obj.activeSelf){
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject cell = Instantiate(parent.GetChild(0).gameObject);
        cell.transform.SetParent(parent);
        cell.transform.localScale = Vector3.one;
        cell.transform.localPosition = Vector3.zero;
        cell.SetActive(true);
        return cell;
    }

    public void HideSv(Transform parent)
    {
        for(int i=0; i < parent.childCount; i++){
            parent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public int GetUnionCoin(string unionId){
        int coin = 0;
        if(StaticData.unionCoins.ContainsKey(unionId)){
            coin = StaticData.unionCoins[unionId];
        }
        return coin;
    }

}
