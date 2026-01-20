using DG.Tweening;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [HideInInspector]
    public UIEquipInfo uIEquipInfo;

    float originScale;

    GameObject equipSelect;

    bool isDrag;

    private void Awake()
    {
        instance = this;

        uIEquipInfo = GetComponentInChildren<UIEquipInfo>(true);
    }

    public void Select(UIEquip uIEquip)
    {
        uIEquipInfo.Show(uIEquip);
    }

    void Update()
    {
        isDrag = true;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (equipSelect == null) originScale = transform.localScale.x;

                hit.collider.transform.DOKill();
                hit.collider.transform.DOScale(originScale * 0.95f, 0.1f);

                Debug.Log("Hit: " + hit.collider.name);
            }
        }

        if (isDrag)
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (equipSelect != hit.collider.gameObject)
                {
                    isDrag = false;
                    equipSelect = null;
                }
            }
            else
            {
                isDrag = false;
                equipSelect = null;
            }
        }
    }
}
