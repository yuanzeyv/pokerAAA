/**
 * 日志写入
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class LogWriter
{
    private string mDevicePersistentPath = Application.persistentDataPath;
    private Queue<LogData> mWritingLogQueue = null;
    private Queue<LogData> mWaitingLogQueue = null;
    private object mLogLock = null;
    private Thread mFileLogThread = null;
    private bool mIsRunning = false;
    private StreamWriter mLogWriter = null;
    private static string LogPath = "Log";
    private bool containError = false;

    public LogWriter()
    {
#if UNITY_EDITOR
        Debug.Log("日志文件目录：" + mDevicePersistentPath);
#endif

        this.mWritingLogQueue = new Queue<LogData>();
        this.mWaitingLogQueue = new Queue<LogData>();
        this.mLogLock = new object();
        System.DateTime now = System.DateTime.Now;
        string logName = string.Format("Log{0}{1}{2}",
          now.Year, now.Month, now.Day);
        string logPath = string.Format("{0}/{1}/{2}.txt", mDevicePersistentPath, LogPath, logName);
        string logDir = Path.GetDirectoryName(logPath);
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);
        DeleteOldLog();
        this.mLogWriter = new StreamWriter(logPath, true);
        this.mLogWriter.AutoFlush = true;
        this.mFileLogThread = new Thread(new ThreadStart(WriteLog));
    }

    /// <summary>
    /// 删除3天前的日志
    /// </summary>
    private void DeleteOldLog()
    {
        DirectoryInfo di = new DirectoryInfo(mDevicePersistentPath + "/" + LogPath);
        DateTime date = DateTime.Today;
        date.AddDays(-3);
        foreach (FileInfo fi in di.GetFiles())
        {
            if(fi.CreationTime < date)
                fi.Delete();
        }
    }

    /// <summary>
    /// 获取所有配置文件路径
    /// </summary>
    /// <returns>文件路径列表</returns>
    public List<string> GetAllLogPath()
    {
        List<string> pathList = new List<string>();
        DirectoryInfo di = new DirectoryInfo(mDevicePersistentPath + "/" + LogPath);
        foreach (FileInfo fi in di.GetFiles())
        {
            pathList.Add(fi.FullName);
        }
        return pathList;
    } 

    /// <summary>
    /// 开始写入
    /// </summary>
    public void StartWrite()
    {
        this.mIsRunning = true;
        this.mFileLogThread.Start();
    }

    /// <summary>
    /// 结束写入
    /// </summary>
    public void EndWrite()
    {
        this.mIsRunning = false;
        this.mLogWriter.Close();
    }

    /// <summary>
    /// 日志写入线程
    /// </summary>
    private void WriteLog()
    {
        while (this.mIsRunning)
        {
            if (this.mWritingLogQueue.Count == 0)
            {
                lock (this.mLogLock)
                {
                    while(!containError && mWaitingLogQueue.Count <= 30)
                        Monitor.Wait(this.mLogLock);
                    if (containError)
                    {
                        Queue<LogData> tmpQueue = this.mWritingLogQueue;
                        this.mWritingLogQueue = this.mWaitingLogQueue;
                        this.mWaitingLogQueue = tmpQueue;
                        containError = false;
                    }
                    else
                    {
                        this.mWaitingLogQueue.Dequeue();
                    }
                }
            }
            else
            {
                this.mLogWriter.WriteLine("Has Error:");
                while (this.mWritingLogQueue.Count > 0)
                {
                    LogData log = this.mWritingLogQueue.Dequeue();
                    if (log.Type == LogType.Error || log.Type == LogType.Exception || log.Type == LogType.Assert)
                    {
                        this.mLogWriter.WriteLine(log.time + "\t" + log.Log + "\n");
                        this.mLogWriter.WriteLine(log.Track);
                    }
                    else
                    {
                        this.mLogWriter.WriteLine(log.time + "\t" + log.Log);
                    }
                }
                this.mLogWriter.WriteLine("=====================================================================================");
            }
        }
    }

    /// <summary>
    /// 增加日志
    /// </summary>
    /// <param name="logData">日志数据</param>
    public void Log(LogData logData)
    {
        lock (this.mLogLock)
        {
            this.mWaitingLogQueue.Enqueue(logData);
            if (logData.Type == LogType.Error || logData.Type == LogType.Exception || logData.Type == LogType.Assert)
                containError = true;
            Monitor.Pulse(this.mLogLock);
        }
    }
}
