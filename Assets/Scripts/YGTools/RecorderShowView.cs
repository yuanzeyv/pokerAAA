using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecorderShowView : MonoBehaviour
{
    public Image[] r;
    public Text tvSeconds, tvTip;

    private int _showIndex;
    private const float _maxLength = 10.0f;  // 10s
    private float _length = 0;

    public void Show()
    {
        gameObject.SetActive(true);
        ShowCancel(false);
        for (int i = 0; i < r.Length; i++)
        {
            r[i].gameObject.SetActive(true);
        }
        _length = _maxLength;
        _showIndex = 2;
        StartCoroutine(countDown());
        InvokeRepeating("showR", 0.5f, 0.3f);
    }

    public void Hide()
    {
        StopAllCoroutines();
        CancelInvoke("showR");
        gameObject.SetActive(false);
        ShowCancel(false);
        _showIndex = 2;
        _length = _maxLength;
    }

    public void ShowCancel(bool isCancel)
    {
        if(isCancel)
        {
            tvTip.color = Color.red;
        }
        else
        {
            tvTip.color = Color.white;
        }
    }

    private void showR()
    {
        if (_showIndex > r.Length)
        {
            _showIndex = 2;
        }
        for (int i = 2; i < r.Length; i++)
        {
            if(i < _showIndex)
            {
                r[i].gameObject.SetActive(true);
            }
            else
            {
                r[i].gameObject.SetActive(false);
            }
        }
        _showIndex++;
    }

    public int GetSecond(){
        return (int)(_maxLength - _length);
    }

    IEnumerator countDown()
    {
        tvSeconds.text = _length + "\"";
        while (_length > 0)
        {
            yield return new WaitForSeconds(1);
            _length--;
            tvSeconds.text = _length + "\"";
        }
        CancelInvoke("showR");
        for (int i = 0; i < r.Length; i++)
        {
            r[i].gameObject.SetActive(true);
        }
        GameUIManager.GetSingleton().controlbtns.voiceButton.Stop();
    } 
}