using UnityEngine;
using System.Collections;

public class InvokeCamera : MonoBehaviour {
	static AndroidJavaClass mCameraClass;


	public static InvokeCamera _instance;
	public static InvokeCamera instance{
		get{ 
			if (_instance == null) {
				_instance = new InvokeCamera ();
				mCameraClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			}
			return _instance;
		}
	}

	#region Clip

	/// <summary>
	/// 圆形截图（拍照）
	/// </summary>
	/// <param name="go">回调通知的对象</param>
	/// <param name="method">回调通知的方法（方法参数 string）</param>
	/// <returns> 图片的地址
	public void TakeCircleClipPhoto(GameObject go, string method){
		AndroidJavaObject jo = mCameraClass.GetStatic<AndroidJavaObject> ("currentActivity");
		jo.Call ("TakeCircleClipPhoto", go.name, method);
	}


	/// <summary>
	/// 圆形截图（从相册选）
	/// </summary>
	/// <param name="go">回调通知的对象</param>
	/// <param name="method">回调通知的方法（方法参数 string）</param>
	/// <returns> 图片的地址
	public void TakeCircleClipPotolFromAlbum(GameObject go, string method){
		AndroidJavaObject jo = mCameraClass.GetStatic<AndroidJavaObject> ("currentActivity");
		jo.Call ("TakeCircleClipPotolFromAlbum", go.name, method);
	}

	/// <summary>
	/// 矩形截图（拍照）
	/// </summary>
	/// <param name="go">回调通知的对象</param>
	/// <param name="method">回调通知的方法（方法参数 string）</param>
	///  <returns> 图片的地址
	public void TakeRectClipPhoto(GameObject go, string method){
		AndroidJavaObject jo = mCameraClass.GetStatic<AndroidJavaObject> ("currentActivity");
		jo.Call ("TakeRectClipPhoto", go.name, method);
	}


	/// <summary>
	/// 矩形截图（从相册选）
	/// </summary>
	/// <param name="go">回调通知的对象</param>
	/// <param name="method">回调通知的方法（方法参数 string）</param>
	///  <returns> 图片的地址
	public void TakeRectClipPotolFromAlbum(GameObject go, string method){
		AndroidJavaObject jo = mCameraClass.GetStatic<AndroidJavaObject> ("currentActivity");
		jo.Call ("TakeRectClipPotolFromAlbum", go.name, method);
	}

	#endregion
}
