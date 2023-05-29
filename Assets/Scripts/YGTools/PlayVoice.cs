using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayVoice : MonoBehaviour
{
    public Image[] r;
    
    public Text tvSeconds;

    private int _showIndex;
    
    public void Show(Vector3 pos, int sec = 0)
    {
        gameObject.SetActive(true);
        transform.localPosition = pos;
        tvSeconds.text = sec + "\'";
        for (int i = 0; i < r.Length; i++)
        {
            r[i].gameObject.SetActive(i == r.Length -1);
        }
        _showIndex = 0;
        InvokeRepeating("showR", 0.5f, 0.3f);
    }

    public void Hide()
    {
        CancelInvoke("showR");
        gameObject.SetActive(false);
    }

    private void showR()
    {
        if (_showIndex >= r.Length)
        {
            _showIndex = 0;
        }
        for(int i = 0; i < r.Length; i++){
            r[i].gameObject.SetActive(i == _showIndex);    
        }
        _showIndex++;
    }
 
}