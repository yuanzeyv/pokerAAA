/**
 * 声音管理类
 * 使用方法：
 *     1、编辑界面FrameWork->Create FrameWork Object建立框架对象
 *     2、使用SoundManager.GetSingleton()获取单例
 *     3、调用你要使用的方法
 */

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private static SoundManager mng;

    private Transform soundObj;
    private Transform musicObj;
    private AudioSource[] soundAudios;
    private AudioSource musicAudio;

    /// <summary>
    /// 音乐音量
    /// </summary>
    public float MusicVolume
    {
        get { return musicVolume; }
    }

    /// <summary>
    /// 音效音量
    /// </summary>
    public float SoundVolume
    {
        get { return allSoundVolume; }
    }

    private float musicVolume = 1f;
    private float allSoundVolume = 1f;

    public static SoundManager GetSingleton()
    {
        return mng;
    }

    private void Awake()
    {
        if (mng == null)
            mng = this;

        soundObj = transform.Find("SoundObj");
        musicObj = transform.Find("MusicObj");
        soundAudios = soundObj.GetComponents<AudioSource>();
        musicAudio = musicObj.GetComponent<AudioSource>();

        if (musicAudio.volume != null)
            musicAudio.volume = musicVolume;
        for (int i = 0; i < soundAudios.Length;i++)
        {
            if (soundAudios[i] != null)
                soundAudios[i].volume = allSoundVolume;
        }
    }

    /// <summary>
    /// 播放背景音乐，如果已经在播放，替换原背景
    /// </summary>
    /// <param name="path">音频文件在Resource目录下的路径</param>
    /// <param name="isLoop">是否循环</param>
    public bool PlayMusic(string path, bool isLoop = true)
    {
        if (musicAudio == null)
            return false;

        musicAudio.clip = Resources.Load<AudioClip>(path);
        if (musicAudio.clip == null)
            return false;
        else
        {
            musicAudio.loop = isLoop;
            musicAudio.Play();
            return true;
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public bool StopMusic()
    {
        if (musicAudio == null)
            return false;

        musicAudio.Stop();
        return true;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path">音频文件在Resource目录下的路径</param>
    public bool PlaySound(string path)
    {
        if(StaticData.isMusic == false){
            return false;
        }
        if (StaticData.selectScene == 2 && StaticData.isMusic == false)
            return false;
        if (StaticData.selectScene == 1 && StaticData.isinform == false)
            return false;
        AudioSource source = GetFreeAudioSource();
        if (source == null)
            return false;

        AudioClip audio = Resources.Load<AudioClip>(path);
        if (audio == null)
            return false;
        source.clip = audio;
        source.Play();
        return true;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path">音频文件在Resource目录下的路径</param>
    /// <param name="hasBgMusic">播放音频时是否关闭背景音乐</param>
    public bool PlaySound(string path, bool closeMusic)
    {
        AudioSource source = GetFreeAudioSource();
        if (source == null)
            return false;

        AudioClip audio = Resources.Load<AudioClip>(path);
        if (audio == null)
            return false;
        if (closeMusic)
            musicAudio.volume = 0f;
        source.volume = allSoundVolume;
        source.clip = audio;
        source.Play();
        CancelInvoke("ResetMusicVolume");
        Invoke("ResetMusicVolume", source.clip.length);
        return true;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path">>音频文件在Resource目录下的路径</param>
    /// <param name="hasBgMusic">播放音频时是否关闭背景音乐</param>
    /// <param name="volume">自定义音量</param>
    public bool PlaySound(string path, bool closeMusic, float volume)
    {
        AudioSource source = GetFreeAudioSource();
        if (source == null)
            return false;

        AudioClip audio = Resources.Load<AudioClip>(path);
        if (audio == null)
            return false;
        if (closeMusic)
            musicAudio.volume = 0f;
        source.volume = volume;
        source.clip = audio;
        source.Play();
        CancelInvoke("ResetMusicVolume");
        Invoke("ResetMusicVolume", source.clip.length);
        return true;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path">音频文件在Resource目录下的路径</param>
    /// <param name="hasBgMusic">播放音频时是否关闭背景音乐</param>
    /// <param name="volume">自定义音量</param>
    /// <param name="stopOtherSound">是否停止其他音效</param>
    public bool PlaySound(string path, bool closeMusic, float volume, bool stopOtherSound)
    {
        AudioSource source = GetFreeAudioSource();
        if (source == null)
            return false;

        AudioClip audio = Resources.Load<AudioClip>(path);
        if (audio == null)
            return false;
        if (closeMusic)
            musicAudio.volume = 0f;
        if (stopOtherSound)
        {
            for (int i = 0; i < soundAudios.Length; i++)
            {
                soundAudios[i].Stop();
            }
        }
        source.volume = volume;
        source.clip = audio;
        source.Play();
        CancelInvoke("ResetMusicVolume");
        Invoke("ResetMusicVolume", source.clip.length);
        return true;
    }

    /// <summary>
    /// 设置音乐音量
    /// </summary>
    /// <param name="volume">音量</param>
    public bool SetMusicVolume(float volume)
    {
        if (musicAudio == null || volume < 0 || volume > 1)
            return false;

        musicVolume = volume;
        musicAudio.volume = volume;
        return true;
    }

    /// <summary>
    /// 设置所有音频音量
    /// </summary>
    /// <param name="volume">音量</param>
    public bool SetAllSoundVolume(float volume)
    {
        if (soundAudios == null || soundAudios.Length == 0)
            return false;

        for (int i = 0; i < soundAudios.Length; i++)
        {
            soundAudios[i].volume = volume;
        }
        return true;
    }

    //获取可以播放的AudioSource
    private AudioSource GetFreeAudioSource()
    {
        if (soundAudios == null || soundAudios.Length == 0)
            return null;

        AudioSource source = null;
        for (int i = 0; i < soundAudios.Length; i++)
        {
            if (!soundAudios[i].isPlaying)
            {
                source = soundAudios[i];
                break;
            }
        }
        return source;
    }

    //重置音乐音效
    private void ResetMusicVolume()
    {
        musicAudio.volume = musicVolume;
    }
}
