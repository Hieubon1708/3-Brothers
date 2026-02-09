using TMPro;
using UnityEngine;
using static GameController;

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

    public void CheckUpgradeNotice()
    {
        if (equipData == null) return;

        int gold = GameManager.instance.Gold;
        int goldUpgrade = DataController.instance.GetGoldEquipUpgrade();

        int amountMaterial = equipData.equipType == EquipType.Weapon ? GameManager.instance.IronAmount : amountMaterial = GameManager.instance.ClothAmount;
        int amountUpgradeMaterial = DataController.instance.GetMaterialEquipUpgrade(equipData.equipType);

        bool isUpgrade = !(gold < goldUpgrade || amountMaterial < amountUpgradeMaterial || DataController.instance.GetEquipLevel(equipData) == 1000);

        uIEquip.upgradeNotice.SetActive(isUpgrade);
    }


    public bool IsPriority(EquipData equipData)
    {
        if (emptySlot.activeSelf) return true;

        return equipData.equipQuality > this.equipData.equipQuality;
    }
}
