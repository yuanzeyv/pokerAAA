using Aliyun.OSS;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AliyunOssService : MonoBehaviour {
    
    public const string AccessKeyId = "LTAI5tExtGiF2gpKLTngZ53v"; 
    public const string AccessKeySecret = "L6zDCGyTueiBYeaCGHssaE7GIkV1k1";
    public const string EndPoint = "oss-cn-hangzhou.aliyuncs.com";
    public const string BucketName = "tongzhidezhou";

    private OssClient ossClient;

    public static AliyunOssService Instance;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    private void Start(){
        ossClient = new OssClient(EndPoint, AccessKeyId, AccessKeySecret);
    }

    public void AsyncUploadObject(string path, byte[] bytes, Action<bool> callback = null) {
        Debug.Log("OSS 尝试上传数据 " + path);
        bool isOk = false;
        try {
 
            Stream stream = new MemoryStream(bytes);
            ossClient.PutObject(BucketName, path, stream);
            isOk = true;
            Debug.Log("OSS 数据上传成功 " + path);
        }
        catch (Exception e)
        {
            Debug.Log("OSS 数据上传失败：" + e + " " + path);
        }
        if(callback != null){
            callback(true);
        }     
 
    }

    public void AsyncDownloadObject(string path, Action<bool, byte[]> callback = null){
        Debug.Log("OSS 尝试下载数据 " + path);
        bool isOk = false;
        byte[] buffer = null;
        try
        {
            OssObject obj = ossClient.GetObject(BucketName, path);

            using (var resultStream = obj.Content)
            {
                buffer = new byte[resultStream.Length+1024];
                byte[] buf = new byte[1024];
                int index = 0;
                int len = 0;
                while ((len = resultStream.Read(buf, 0, 1024)) != 0)
                {
                    Array.Copy(buf, 0, buffer, index, buf.Length);
                    index += len;
                }

            }
            isOk = true;
            Debug.Log("OSS 下载数据成功 " + path);

        }
        catch (Exception ex)
        {
            Debug.Log("OSS 下载数据失败 " + path);
        }
        if(callback != null){
            callback(isOk, buffer);
        }
    }

}