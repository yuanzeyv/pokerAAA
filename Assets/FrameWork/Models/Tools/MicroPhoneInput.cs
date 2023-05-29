/**
 * 麦克风工具类
 * 使用方法：
 *     1、编辑界面FrameWork->Create FrameWork Object建立框架对象
 *     2、MicroPhoneInput.GetSingleton()获取单例
 *     3、调用你要使用的方法
 */

using System;  
using System.Collections.Generic;  
using System.IO;  
using UnityEngine;  
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.UI;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;

[RequireComponent (typeof(AudioSource))]  
public class MicroPhoneInput : MonoBehaviour
{
    public double length;//语音bytes数组长度
	//public static MicroPhoneInput m_instance;
    public float startTimer = 0;
    private float talkTimer = 0;
    private static string[] micArray=null;
    public delegate void CallBack(Hashtable tb);
    public CallBack S_AudioTips; 
    const int RECORD_TIME = 10;  
	private AudioClip redioclip;
    private static bool isCanUse = true;
    private AudioSource chatAudioSource;

    private static MicroPhoneInput mng;

    public static MicroPhoneInput GetSingleton()
    {
        return mng;
    }

    public void Awake()
    {
        if (mng == null)
            mng = this;

        Init();
    }
    public void Init()
    {
        Application.RequestUserAuthorization(UserAuthorization.Microphone);
        micArray = Microphone.devices;
        foreach (string deviceStr in Microphone.devices)
        {
           Debug.Log("device name = " + deviceStr);
           isCanUse = true;
        }
        if(micArray.Length==0)  
        {  
           Debug.Log("no mic device");
           isCanUse = false;
        }
        initAllAduioSource();
        //DontDestroyOnLoad(this);
    }

    private void initAllAduioSource()
    {
        chatAudioSource = this.transform.GetComponent<AudioSource>();
    }

	//public static MicroPhoneInput getInstance()  
	//{    
	//	return m_instance;  
	//}  
    /// <summary>
    /// 开始录音
    /// </summary>
	public void StartRecord()  
	{
        startTimer = Time.time;
        if (!isCanUse)
        {
            return;
        }
        chatAudioSource.Stop();  
		if (micArray.Length == 0)
        { 
            Debug.Log("No Record Device!");  
			return;  
		}
        chatAudioSource.mute = true;
        Microphone.End("inputMicro");
        redioclip = Microphone.Start("inputMicro", false, RECORD_TIME, 8000); //22050   //
        //Microphone.End("null");
        //redioclip = Microphone.Start(null, false, RECORD_TIME, 8000);
    }  
    /// <summary>
    /// 结束录音
    /// </summary>
    /// <returns></returns>
	public  byte[] StopRecord()  
	{
        talkTimer = Time.time - startTimer;
        if (!isCanUse)
        {
            return null;
        }
        Debug.Log("StopRecord");  
		if (micArray.Length == 0)  
		{  
			Debug.Log("No Record Device!");  
			return null;  
		}  
		if (!Microphone.IsRecording(null))  
		{  
			return null;  
		}  
		Microphone.End (null);
        chatAudioSource.clip =redioclip;
        Debug.Log("PlayingRecord"+redioclip.length);
	    Byte[] bytes = GetClipData();
	    if (bytes.Length <= 5000)
	    {
            Debug.Log("录音时间太短！");
	        return null;
	    }
        return pressBytesArray(bytes);
    }
    /// <summary>
    /// 二进制压缩方法
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static Byte[] pressBytesArray(Byte[] bytes)
    {
        MemoryStream ms = new MemoryStream();
        GZipOutputStream gzip = new GZipOutputStream(ms);
        gzip.Write(bytes, 0, bytes.Length);
        gzip.Close();
        byte[] press = ms.ToArray();
        return press;
    }
    /// <summary>
    /// 二进制解压方法
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static Byte[] depressBytesArray(Byte[] bytes)
    {
        GZipInputStream gzi = new GZipInputStream(new MemoryStream(bytes));
        MemoryStream re = new MemoryStream();
        int count = 0;
        byte[] data = new byte[4096];
        while ((count = gzi.Read(data, 0, data.Length)) != 0)
        {
            re.Write(data, 0, count);
        }
        byte[] depress = re.ToArray();
        return depress;
    }
    Byte[] GetClipData()  
	{  
		if (GetComponent<AudioSource>().clip == null)  
		{  
			Debug.Log("GetClipData audio.clip is null");  
			return null;   
		}
        //float[] samples = new float[redioclip.samples]; 
        float[] samples = new float[(int)(8000*talkTimer)+1];
		Debug.Log ("samples.Length = "+samples.Length);
        chatAudioSource.clip.GetData(samples, 0);  
		Byte[] outData = new byte[samples.Length * 2];  

		int rescaleFactor = 32767; //to convert float to Int16   
		for (int i = 0; i < samples.Length; i++)  
		{  
			short temshort = (short)(samples[i] * rescaleFactor);  
			Byte[] temdata=System.BitConverter.GetBytes(temshort);  
			outData[i*2]=temdata[0];  
			outData[i*2+1]=temdata[1];  
		}  
		if (outData == null || outData.Length <= 0)  
		{  
			Debug.Log("GetClipData intData is null");  
			return null;   
		}  
		//return intData;   
		return outData;  
	}  
        /// <summary>
        /// 读取语音文件
        /// </summary>
        /// <param name="intArr"></param>
        /// <returns></returns>

	public AudioClip DepressClipData(Int16[] intArr)  
	{  
		if (intArr.Length == 0)  
		{  
			Debug.Log("get intarr clipdata is null");  
			return null;  
		}  
		Debug.Log ("PlayClipData");
		//从Int16[]到float[]   
		float[] samples = new float[intArr.Length];  
		int rescaleFactor = 32767;  
		for (int i = 0; i < intArr.Length; i++)  
		{  
			samples[i] = (float)intArr[i] / rescaleFactor;  
		}
        Debug.Log("语音长度:" + samples.Length);
        AudioClip clip = AudioClip.Create("playRecordClip", samples.Length, 1, 8000, false);
        clip.SetData(samples, 0);
        return clip;
    }
    /// <summary>
    /// 二进制录音数据
    /// </summary>
    /// <param name="audios"></param>
    /// <returns></returns>
	public AudioClip micInputNotice(Byte[] audios){
        //语音
        Debug.Log("Get");
        if (true){
            Debug.Log("micInputNotice"+audios.Length);
            byte[] data = depressBytesArray( audios);
			int i = 0;
			List<short> result = new List<short>();
			while(data.Length - i >= 2)
			{
				result.Add(BitConverter.ToInt16(data,i));
				i += 2;
			}
			Int16[] arr = result.ToArray();
			return DepressClipData(arr);
		}
	}

    void OnDestory()
    {
    }

    public void OnAddGM()
    {
       
    }
}