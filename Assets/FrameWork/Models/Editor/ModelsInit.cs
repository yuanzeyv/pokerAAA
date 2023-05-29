using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using UnityEditor;
using Tools = UnityEditor.Tools;

public class ModelsInit : Editor
{

    [MenuItem("FrameWork/Create FrameWork Object")]
    public static void InitModel()
    {
        GameObject framework = new GameObject("Framework");

        //初始化声音模块
        GameObject soundGo = new GameObject("Sound");
        soundGo.transform.parent = framework.transform;
        soundGo.AddComponent<SoundManager>();
        GameObject soundObj = new GameObject("SoundObj");
        soundObj.transform.parent = soundGo.transform;
        for (int i = 0; i < 20; i++)
        {
            AudioSource soundSource = soundObj.AddComponent<AudioSource>();
            soundSource.playOnAwake = false;
        }
        GameObject musicObj = new GameObject("MusicObj");
        musicObj.transform.parent = soundGo.transform;
        AudioSource musicSource = musicObj.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;

        //初始化网络模块
        GameObject serverGo = new GameObject("Server");
        serverGo.transform.parent = framework.transform;
        serverGo.AddComponent<HttpMngr>();
        serverGo.AddComponent<NetMngr>();

        //初始化消息模块
        GameObject messageGo = new GameObject("Message");
        messageGo.transform.parent = framework.transform;
        messageGo.AddComponent<MessageManager>();

        //初始化工具模块
        GameObject toolGo = new GameObject("GameTools");
        toolGo.transform.parent = framework.transform;
        toolGo.AddComponent<GameTools>();
        toolGo.AddComponent<MicroPhoneInput>();
        toolGo.AddComponent<GetGPS>();


        //初始化日志模块
        GameObject logGo = new GameObject("Log");
        logGo.transform.parent = framework.transform;
        logGo.AddComponent<LogManager>();

        framework.transform.SetSiblingIndex(0);

        Debug.Log("Create FrameWork Object Finish");
    }
}
