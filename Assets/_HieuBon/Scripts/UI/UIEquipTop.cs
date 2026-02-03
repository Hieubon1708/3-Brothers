using TMPro;
using UnityEngine;
using static GameController;

public class UIEquipTop : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;
    [HideInInspector]
    UIEquipAlert uIEquipAlert;

    public GameObject emptySlot;

    public TextMeshProUGUI textLevel;

    public EquipData equipData;

    private void Awake()
    {
        uIEquipAlert = GetComponent<UIEquipAlert>();
    }

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

    public void CanUpgrade()
    {
        int gold = GameManager.instance.Gold;
        int goldUpgrade = UIInventory.instance.GetGoldUpgrade();

        int amountMaterial = equipData.equipType == EquipType.Weapon ? GameManager.instance.IronAmount : amountMaterial = GameManager.instance.ClothAmount;
        int amountUpgradeMaterial = UIInventory.instance.GetAmountMaterialUpgrade(equipData.equipType);

        uIEquipAlert.IsUpgrade(!(gold < goldUpgrade || amountMaterial < amountUpgradeMaterial || UIInventory.instance.GetLevel(equipData) == 1000));
    }
}
