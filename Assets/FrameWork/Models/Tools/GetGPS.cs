/**
 * 通过GPS获取经纬度类
 * 说明：
 *     提供了获取经纬度和计算距离的功能
 */
using UnityEngine;
using System.Collections;

public class GetGPS : MonoBehaviour {

    private static GetGPS getGPS;

    //设备经度
    private float longitude = -1f;
    //设备纬度
    private float latitude = -1f;

    private Coroutine gpsCoroutine;

    public static GetGPS GetSingleton()
    {
        return getGPS;
    }

    private void Awake()
    {
        if (getGPS == null)
            getGPS = this;
    }

    /// <summary>
    /// 开启GPS
    /// </summary>
    public void StartGPS()
    {
        gpsCoroutine = StartCoroutine(StartGps());
    }

    /// <summary>
    /// 停止GPS
    /// </summary>
    public void StopGPS()
    {
        Input.location.Stop();
        if (gpsCoroutine != null)
            StopCoroutine(gpsCoroutine);
    }

    IEnumerator StartGps()
    {
        // Input.location 用于访问设备的位置属性（手持设备）, 静态的LocationService位置
        // LocationService.isEnabledByUser 用户设置里的定位服务是否启用
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS is On:" + Input.location.isEnabledByUser.ToString());
        }
        else
        {
            // LocationService.Start() 启动位置服务的更新,最后一个位置坐标会被使用
            Input.location.Start(300.0f, 300.0f);

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                // 暂停协同程序的执行(1秒)
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait < 1)
            {
                Debug.Log("Init GPS service time out");
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
            }
            else
            {
                Debug.Log("N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude);
                Debug.Log(" Time:" + Input.location.lastData.timestamp);
                yield return new WaitForSeconds(100);
            }
        }
    }

    /// <summary>
    /// 获取两个经纬度的距离
    /// </summary>
    /// <param name="lng1">经度1</param>
    /// <param name="lat1">纬度1</param>
    /// <param name="lng2">经度2</param>
    /// <param name="lat2">纬度2</param>
    /// <returns>距离，单位米</returns>
    public float Distance(float lng1, float lat1, float lng2, float lat2)
    {
        float a, b, R;
        R = 6378137; //地球半径

        lat1 = lat1 * Mathf.PI / 180.0f;
        lat2 = lat2 * Mathf.PI / 180.0f;


        a = (lat1 - lat2);
        b = (lng1 - lng2) * Mathf.PI / 180.0f;

        float d;
        float sa2, sb2;
        sa2 = Mathf.Sin(a / 2.0f);
        sb2 = Mathf.Sin(b / 2.0f);

        d = 2 * R * Mathf.Asin(Mathf.Sqrt(sa2 * sa2 + Mathf.Cos(lat1) * Mathf.Cos(lat2) * sb2 * sb2));

        return d;
    }

    private float rad(float d)
    {
        return d * Mathf.PI / 180.0f;
    }

    /// <summary>
    /// 获取经度
    /// </summary>
    /// <returns>经度</returns>
    public float GetLongitude()
    {
        return Input.location.lastData.longitude;
    }

    /// <summary>
    /// 获取纬度
    /// </summary>
    /// <returns>纬度</returns>
    public float GetLatitude()
    {
        return Input.location.lastData.latitude;
    }

    public void OnDestroy()
    {
        StopGPS();
    }

}
