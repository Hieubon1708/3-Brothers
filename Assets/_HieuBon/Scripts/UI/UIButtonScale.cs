using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float originScale;

    Button button;

    private void Awake()
    {
        originScale = transform.localScale.x;
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;

        transform.DOKill();
        transform.DOScale(originScale * 0.95f, 0.15f).SetEase(Ease.Linear);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) return;

        transform.DOKill();
        transform.DOScale(originScale, 0.15f).SetEase(Ease.Linear);
    }
}
