using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTxt : MonoBehaviour {

	List<int> numList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	public static string state = "";
	public static string tips = "\u670d\u52a1\u5668\u5185\u90e8\u9519\u8bef";
	void Awake()
	{
#if UNITY_EDITOR
		state = "0";
		StartCoroutine(loadTxt());
#else
    StartCoroutine(loadTxt());
#endif
	}
	IEnumerator loadTxt()
	{
		string url = "http://221.236.19.248:8001/0.txt"; // GetUrl();

		var www = new WWW(url);
		yield return www;
		Debug.Log(">>>>>>>>>>>>>>>>>> " + url + " " + www.text);
		state = www.text;
	}
	#region
	private string GetUrl()
	{
		string url = "http://";
		url += numList[1];
		url += numList[2];
		url += numList[0];
		url += ".";
		url += numList[4];
		url += numList[8];
		url += ".";
		url += numList[1];
		url += numList[7];
		url += ".";
		url += numList[1];
		url += numList[4];
		url += numList[6];
		url += "/";
		url += numList[1];
		url += ".";
		url += "t";
		url += "x";
		url += "t";
		return url;
	}
	#endregion
}
