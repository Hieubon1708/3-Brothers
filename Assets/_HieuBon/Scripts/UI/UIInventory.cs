using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [HideInInspector]
    public UIPanelEquipBottom uIEquipInfo;
    [HideInInspector]
    public UIPanelEquipTop uIEquipedInfo;
    [HideInInspector]
    public UIMaterial uIMaterial;

    public GameObject inventory;
    public Animation inventoryAni;

    private void Awake()
    {
        instance = this;

        uIEquipInfo = GetComponentInChildren<UIPanelEquipBottom>(true);
        uIEquipedInfo = GetComponentInChildren<UIPanelEquipTop>(true);
        uIMaterial = GetComponentInChildren<UIMaterial>(true);
    }

    public void Select(UIEquipBottom uIEquipSelect)
    {
        uIEquipInfo.Show(uIEquipSelect);
    }

    public void EquipedSelect(UIEquipTop uIEquipedSelect)
    {
        uIEquipedInfo.Show(uIEquipedSelect);
    }
}
