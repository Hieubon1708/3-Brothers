using TMPro;
using UnityEngine;

public class UIEquipTop : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;

    public GameObject emptySlot;

    public TextMeshProUGUI textLevel;

    public EquipData equipData;

    public void LoadData(EquipData equipData, int level)
    {
        if (this.equipData != null && this.equipData != equipData) this.equipData.isEquip = false;

        this.equipData = equipData;

        if (uIEquip == null) uIEquip = GetComponent<UIEquip>();

        uIEquip.LoadEquip(equipData);

        gameObject.SetActive(true);
        emptySlot.SetActive(false);

        UpdateLevel(level);
    }

    public void OnClick()
    {
        UIInventory.instance.EquipedSelect(this);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
        emptySlot.SetActive(true);
    }

    public void UpdateLevel(int level)
    {
        textLevel.text = "Lv." + level;
    }
}
