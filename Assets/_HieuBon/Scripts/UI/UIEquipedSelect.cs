using TMPro;
using UnityEngine;

public class UIEquipedSelect : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;

    public GameObject emptySlot;

    public TextMeshProUGUI textLevel;

    public void LoadData(EquipData equipData, int level)
    {
        if (uIEquip == null) uIEquip = GetComponent<UIEquip>();

        uIEquip.LoadEquip(equipData);

        gameObject.SetActive(true);
        emptySlot.SetActive(false);

        textLevel.text = "Lv." + level;
    }

    public void OnClick()
    {
        UIInventory.instance.EquipedSelect(this);
    }
}
