/**
 * 用于在物体上增加EventTrigger事件
 * 使用方法：
 *     EventTriggerListener.Get(游戏物体).事件 = 你的方法;
 */

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void PointerDelegate(PointerEventData eventdata);
    public delegate void VoidDel(GameObject go);
    public VoidDel onClick;
    public VoidDel onDown;
    public VoidDel onEnter;
    public VoidDel onExit;
    public VoidDel onUp;
    public VoidDel onSelect;
    public VoidDel onUpdateSelect;
    public VoidDel onDragEnd;
    public VoidDel onDrop;
    public PointerDelegate onDragPoint;
    public VoidDel onDrag;
    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if (onDrop != null) onDrop(gameObject);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if (onDrag != null) onDrag(gameObject);
        if (onDragPoint != null) onDragPoint(eventData);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (onDragEnd != null) {
            onDragEnd(gameObject);
        }
    }
}
