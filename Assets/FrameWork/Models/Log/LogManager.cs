/**
 * 日志管理类
 * 说明：
 *     1、提供了日志写入本地的功能，默认不开启
 *     2、如需开启日志写入功能，需要调用初始化方法
 *     3、初始化方式：LogManager.GetSingleton().Init();
 *     4、日志内容为：报错日志前的最大30条日志 + 出现报错的日志
 *     5、日志保存3天
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.EventSystems;

public class LogManager : MonoBehaviour {

    private LogWriter logWriter;
    private int mainThreadID;
    private int initCount = 0;
    private bool isInit = true;

    private static LogManager mng;

    public static LogManager GetSingleton()
    {
        return mng;
    }

    private void Awake()
    {
        if (mng == null)
            mng = this;
    }

    /// <summary>
    /// 初始化方法
    /// </summary>
    public void Init()
    {
        if(isInit)
            return;
        logWriter = new LogWriter();
        logWriter.StartWrite();
        mainThreadID = Thread.CurrentThread.ManagedThreadId;
        Application.logMessageReceived += LogCallback;
        Application.logMessageReceivedThreaded += LogMultiThreadCallback;
        isInit = true;
    }

    private void LogCallback(string log, string track, LogType type)
    {
        if (this.mainThreadID == Thread.CurrentThread.ManagedThreadId)
            Output(log, track, type);
    }

    private void LogMultiThreadCallback(string log, string track, LogType type)
    {
        if (this.mainThreadID != Thread.CurrentThread.ManagedThreadId)
            Output(log, track, type);
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="log">日志内容</param>
    /// <param name="track">日志目标</param>
    /// <param name="type">日志类型</param>
    private void Output(string log, string track, LogType type)
    {
        LogData logData = new LogData
        {
            Log = log,
            Track = track,
            Type = type,
            time =  DateTime.Now.ToString()
        };
        logWriter.Log(logData);
    }

    /// <summary>
    /// 获取所有日志文件路径
    /// </summary>
    /// <returns>文件路径列表</returns>
    public List<string> GetAllLogPath()
    {
        return logWriter.GetAllLogPath();
    }

    private void OnDestroy()
    {
        if (!isInit)
            return;

        if (logWriter != null)
            logWriter.EndWrite();

        Application.logMessageReceived -= LogCallback;
        Application.logMessageReceivedThreaded -= LogMultiThreadCallback;
        isInit = false;
    }

    private void OnApplicationQuit()
    {
        if (!isInit)
            return;

        if (logWriter != null)
            logWriter.EndWrite();

        Application.logMessageReceived -= LogCallback;
        Application.logMessageReceivedThreaded -= LogMultiThreadCallback;
        isInit = false;
    }
}

/// <summary>
/// 日志数据类
/// </summary>
public class LogData
{
    public string Log { get; set; }
    public string Track { get; set; }
    public LogType Type { get; set; }
    public string time;
}
