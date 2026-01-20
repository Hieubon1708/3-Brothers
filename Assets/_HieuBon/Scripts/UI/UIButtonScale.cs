using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool isClick;

    public Action onClick;

    private void Awake()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;

        transform.DOKill();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isClick || eventData.dragging) return;


        //if (onClick != null) onClick.Invoke();
    }
}
