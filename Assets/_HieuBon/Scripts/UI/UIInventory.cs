using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [HideInInspector]
    public UIEquipInfo uIEquipInfo;
    [HideInInspector]
    public UIEquipedInfo uIEquipedInfo;

    private void Awake()
    {
        instance = this;

        uIEquipInfo = GetComponentInChildren<UIEquipInfo>(true);
        uIEquipedInfo = GetComponentInChildren<UIEquipedInfo>(true);
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
