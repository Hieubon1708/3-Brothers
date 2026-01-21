using DG.Tweening;
using UnityEngine;

public class UIButtonScale : MonoBehaviour
{
    float originScale;

    Transform equipSelect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (equipSelect == null) originScale = hit.collider.transform.localScale.x;

                equipSelect = hit.collider.transform;
                equipSelect.DOKill();
                equipSelect.DOScale(originScale * 0.95f, 0.1f);
            }
        }

        if (equipSelect != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (equipSelect != hit.collider.transform) Release();
            }
            else Release();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (equipSelect != null) Release();
        }
    }

    void Release()
    {
        equipSelect.DOKill();
        equipSelect.DOScale(originScale, 0.1f);
        equipSelect = null;
    }
}
