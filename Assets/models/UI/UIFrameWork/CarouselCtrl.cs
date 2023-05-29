using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CarouselCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Carousel carousel;
    private Scrollbar scrollbar;

    private int selectIndex = 0;
    private float pointDownTime;
    private float pointDownValue;
    private float autoPlayTimer;
    private bool canPlay;
    private bool isForRight = true;
    private Coroutine moveCoroutine;

    public void Init(Carousel carousel, Scrollbar scrollbar)
    {
        this.carousel = carousel;
        this.scrollbar = scrollbar;
        autoPlayTimer = 0f;
        canPlay = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointDownValue = scrollbar.value;
        pointDownTime = Time.time;
        canPlay = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float changeValue = scrollbar.value - pointDownValue;
        float dragTime = Time.time - pointDownTime;
        autoPlayTimer = 0f;
        canPlay = true;
        if (dragTime > carousel.canMoveDragTimespan)
        {
            if (Mathf.Abs(changeValue) < (1f/(carousel.carouselList.Count - 1))/2 || carousel.carouselList.Count == 1)
            {
                Debug.Log("selectIndex"+ selectIndex);
                MoveToIndex(selectIndex);
                if (changeValue == 0f || carousel.carouselList.Count == 1)
                {
                    Call(selectIndex);
                }
            }
            else
            {
                float index_ = Mathf.Abs(changeValue) / ((1f / (carousel.carouselList.Count - 1)) / 2);
                float temp = Mathf.Floor(index_);
                int index = 1;
                if (temp % 2 == 0)
                {
                    index = (int)temp / 2;
                }
                else
                {
                    index =(int) (temp + 1) / 2;
                }
                if (changeValue > 0)
                    MoveToIndex(Mathf.Clamp(selectIndex + index, 0, carousel.carouselList.Count - 1));
                else
                    MoveToIndex(Mathf.Clamp(selectIndex - index, 0, carousel.carouselList.Count - 1));
            }
        }
        else
        {
            if (changeValue > 0)
                MoveToIndex(Mathf.Clamp(selectIndex + 1, 0, carousel.carouselList.Count - 1));
            else if(changeValue < 0)
                MoveToIndex(Mathf.Clamp(selectIndex - 1, 0, carousel.carouselList.Count - 1));
        }
    }

    //触发回调
    private void Call(int index)
    {
        if (carousel.carouselList[index].callback != null)
            carousel.carouselList[index].callback(carousel.carouselList[index].texture);
    }

    public void MoveToIndex(int index)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        StartCoroutine(MoveToNewIndex(index));
    }

    private IEnumerator MoveToNewIndex(int index)
    {
        float timer = 0f;
        float startValue = scrollbar.value;
        float speed = carousel.moveSpeed;
        selectIndex = index;
        ChangePoint(selectIndex);
        while (timer <= 1f / speed)
        {
            timer += Time.deltaTime;
            scrollbar.value = Mathf.Lerp(startValue, 1f / (carousel.carouselList.Count - 1) * index, timer * speed);
            yield return null;
        }
    }

    private void ChangePoint(int index)
    {
        if(carousel.pointList.Count > index && carousel.pointList[index] != null)
            carousel.pointList[index].isOn = true;
    }

    private void Update()
    {
        if (canPlay && carousel.isAutoPlay && carousel.carouselList.Count > 1)
        {
            autoPlayTimer += Time.deltaTime;
            if (autoPlayTimer >= carousel.playTimespan)
            {
                if (selectIndex >= carousel.carouselList.Count - 1)
                    isForRight = false;
                else if (selectIndex <= 0)
                    isForRight = true;
                int nextIndex = isForRight ? selectIndex + 1 : selectIndex - 1;
                MoveToIndex(nextIndex);
                autoPlayTimer = 0f;
            }
        }
    }
}
