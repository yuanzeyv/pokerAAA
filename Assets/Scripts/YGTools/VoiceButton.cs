using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class VoiceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image img;
    public Image imgCancel;
    public RecorderShowView showView;

    private Vector2 _beginPos, _curPos;
    private bool _isCancel;

    private Queue<Action> _queue;

    private byte[] voiceData;
    private string voicePath;
    private int voiceTime;

    private bool isClick = false;
    private long last_ts = 0;

    private void Awake()
    {
        _beginPos = Vector2.zero;
        _curPos = Vector2.zero;
        _queue = new Queue<Action>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.GetSingleton().myNetID < 0)
        {
            Tip.Instance.showMsg("请入坐再聊天！");
            return;
        }
        if(TimeUtil.Now() - last_ts < 10){
            Tip.Instance.showMsg("发送太频繁，请稍等！");
            return;
        }
        isClick = true;
        showView.Show();
        VoiceRecorder.Instance.StartRecord();
        _beginPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _curPos = eventData.position;
        if(_curPos.y > _beginPos.y && (_curPos - _beginPos).sqrMagnitude > 4000)
        {
            _isCancel = true;
            showView.ShowCancel(true);
            img.gameObject.SetActive(false);
            imgCancel.gameObject.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isClick) return;
        Stop();
    }

    public void Stop(){ 
        isClick = false;  
        voiceTime = showView.GetSecond();
        showView.Hide();
        VoiceRecorder.Instance.StopRecord();
            
        if (!_isCancel)
        {
            if (voiceTime < 3){
                Tip.Instance.showMsg("声音不能少于3秒");
                return;
            }
            if (voiceTime > 10){
                Tip.Instance.showMsg("声音不能大于10秒");
                return;
            }
            
            last_ts = TimeUtil.Now();
            
            voiceData = VoiceRecorder.Instance.Save();
            voicePath = string.Format("audio/{0}/{1}_{2}.wav", StaticData.ID, StaticData.ID, TimeUtil.DateToMills(DateTime.Now));

            NetMngr.GetSingleton().Send(InterfaceGame.sendChat, new object[] { 2, voicePath});
        }
        else
        {
            img.gameObject.SetActive(true);
            imgCancel.gameObject.SetActive(false);
        }
        _isCancel = false;
    }

    // sendChat（语音）发送成功，上传语音
    public void UploadVoice(){
        if (voicePath == null || voiceData == null){
            return;
        }
        AliyunOssService.Instance.AsyncUploadObject(voicePath, voiceData, (bool isOk) =>
        {
            if (!isOk){
                Tip.Instance.showMsg("发送失败");
                return;
            }
            Tip.Instance.showMsg("发送成功");
            
            NetMngr.GetSingleton().Send(InterfaceGame.playVoice, new object[] { voicePath, voiceTime });
            // test
            // for (int i = 0; i < 1; i++){
            //     Hashtable h = new Hashtable();
            //     h.Add("userID", StaticData.ID);
            //     h.Add("voicePath", voicePath);
            //     h.Add("voiceTime", voiceTime);
            //     GameUIManager.GetSingleton().notifyPlayVoice(h);
            // }
        });
    }

}
