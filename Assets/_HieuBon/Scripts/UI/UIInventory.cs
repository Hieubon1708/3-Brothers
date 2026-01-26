using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [HideInInspector]
    public UIEquipInfo uIEquipInfo;
    [HideInInspector]
    public UIEquipedInfo uIEquipedInfo;
    [HideInInspector]
    public UIMaterial uIMaterial;

    private void Awake()
    {
        instance = this;

        uIEquipInfo = GetComponentInChildren<UIEquipInfo>(true);
        uIEquipedInfo = GetComponentInChildren<UIEquipedInfo>(true);
        uIMaterial = GetComponentInChildren<UIMaterial>(true);
    }

    public void Select(UIEquipSelect uIEquipSelect)
    {
        uIEquipInfo.Show(uIEquipSelect);
    }

    public void EquipedSelect(UIEquipedSelect uIEquipedSelect)
    {
        uIEquipedInfo.Show(uIEquipedSelect);
    }
}
