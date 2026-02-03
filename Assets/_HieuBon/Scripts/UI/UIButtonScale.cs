using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float originScale;

    public GameObject disable;

    private void Awake()
    {
        originScale = transform.localScale.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (disable != null && disable.activeSelf) return;

        transform.DOKill();
        transform.DOScale(originScale * 0.95f, 0.15f).SetEase(Ease.Linear);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (disable != null && disable.activeSelf) return;

        transform.DOKill();
        transform.DOScale(originScale, 0.15f).SetEase(Ease.Linear);
    }
}
