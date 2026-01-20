using UnityEngine;

public class UIEquipSelect : MonoBehaviour
{
    UIEquip uIEquip;
    UIButtonScale uIButtonScale;

    private void Awake()
    {
        uIEquip = GetComponent<UIEquip>();
        uIButtonScale = GetComponentInChildren<UIButtonScale>();
        uIButtonScale.onClick += OnClick;
    }

    void OnClick()
    {
        UIInventory.instance.Select(uIEquip);
    }

    private void OnDestroy()
    {
        uIButtonScale.onClick -= OnClick;
    }
}
