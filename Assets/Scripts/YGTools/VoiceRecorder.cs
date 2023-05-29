using System;
using UnityEngine;

public class VoiceRecorder : MonoBehaviour
{

    public static VoiceRecorder Instance;

    private AudioClip audioClip;
    private DateTime beginTime;

    const int HEADER_SIZE = 44;
    const int RECORD_TIME = 10;
    const int RECORD_RATE = 44100; //录音采样率

    private static string[] micArray = null; //录音设备列表

    private void Awake()
    {
        Instance = this;
        micArray = Microphone.devices;
    }

    public void StartRecord()
    {
        if (micArray.Length == 0)
        {
            Debug.Log("No Record Device!");
            return;
        }
        Microphone.End(null);
        beginTime = DateTime.Now;
        audioClip = Microphone.Start(null, false, RECORD_TIME, RECORD_RATE);
        while (!(Microphone.GetPosition(null) > 0))
        {
        }
        Debug.Log("StartRecord");
    }

    public void StopRecord()
    {
        if (micArray.Length == 0)
        {
            Debug.Log("No Record Device!");
            return;
        }
        if (!Microphone.IsRecording(null))
        {
            return;
        }
        Microphone.End(null);
    }

    public void PlayRecord()
    {
        PlayRecord(audioClip);
    }

    public void PlayRecord(AudioClip clip)
    {
        PlayRecord(clip, Vector3.zero);
    }

    public void PlayRecord(AudioClip clip, Vector3 pos)
    {
        if(clip)
        {
            // AudioSource.PlayClipAtPoint(clip, pos);
            // Debug.Log("===============PlayRecord--------------");
            GameObject g = new GameObject("MyClip_"+ clip.name);
            AudioSource a = g.AddComponent<AudioSource>();
            a.clip = clip;
            a.volume = SoundManager.GetSingleton().SoundVolume;
            a.Play();
            GameObject.Destroy(g, clip.length);
        }
    }

    public byte[] Save()
    {
        if(audioClip)
        {
            return WavUtility.FromAudioClip(audioClip);
        }
        return null;
    }

    public byte[] Save(out string path, string dirname = "recordings")
    {
        return Save(audioClip, out path, dirname);
    }

    public byte[] Save(AudioClip clip, out string path, string dirname = "recordings")
    {
        if (!clip)
        {
            path = string.Empty;
            return null;
        }
       return WavUtility.FromAudioClip(clip, out path, true, dirname);
    }

    public AudioClip Read(string path)
    {
        return WavUtility.ToAudioClip(path);
    }

    public AudioClip Read(byte[] data)
    {
        return WavUtility.ToAudioClip(data);
    }


}