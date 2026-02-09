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

    public void Show(bool isAnimation = false)
    {
        inventory.SetActive(true);
        if (isAnimation) inventoryAni.Play();
    }

    public void Hide()
    {
        inventory.SetActive(false);
    }

    public void Select(UIEquipBottom uIEquipSelect)
    {
        uIEquipInfo.Show(uIEquipSelect);
    }

    public void EquipedSelect(UIEquipTop uIEquipedSelect)
    {
        uIEquipedInfo.Show(uIEquipedSelect);
    }

    public bool IsSame(EquipData equipData1, EquipData equipData2)
    {
        return equipData1.equipType == equipData2.equipType
                && equipData1.equipQuality == equipData2.equipQuality
                && equipData1.equipMaterial == equipData2.equipMaterial;
    }
}
