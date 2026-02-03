using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class UIPanelEquipTop : MonoBehaviour
{
    public Color lockColor;
    public Color[] colors;
    public RectTransform[] barIndexes;
    public TextMeshProUGUI[] textIndexes;
    public GameObject[] labels;
    public GameObject[] locks;

    public DataEquipUpgrade[] dataEquipUpgrades;

    public RectTransform border;

    UIEquip uIEquip;

    public GameObject[] indexType;

    public TextMeshProUGUI equipName;
    public TextMeshProUGUI textAmountMaterial;
    public TextMeshProUGUI textAmountGold;
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textValue;

    Animation ani;

    [HideInInspector]
    public UIEquipTop uIEquipedSelect;

    public RectTransform rectPopup;

    public GameObject[] mats;
    public GameObject[] btnsDisable;
    public Button[] btnsUpgrade;

    public void Show(UIEquipTop uIEquipedSelect)
    {
        this.uIEquipedSelect = uIEquipedSelect;

        UIEquip uIEquip = uIEquipedSelect.uIEquip;

        if (this.uIEquip == null) this.uIEquip = GetComponentInChildren<UIEquip>(true);
        if (ani == null) ani = GetComponent<Animation>();

        LoadData(uIEquip.equipData);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadData(EquipData equipData)
    {
        indexType[0].SetActive(equipData.equipType == EquipType.Weapon);
        indexType[1].SetActive(equipData.equipType != EquipType.Weapon);

        mats[0].SetActive(equipData.equipType == EquipType.Weapon);
        mats[1].SetActive(equipData.equipType != EquipType.Weapon);

        int qualityIndex = (int)equipData.equipQuality;

        UpdateIndex(equipData);

        for (int i = 0; i < labels.Length; i++)
        {
            labels[i].SetActive(i == qualityIndex);
        }

        for (int i = 0; i < locks.Length; i++)
        {
            locks[i].SetActive(i > qualityIndex);
        }

        DataEquipUpgrade dataEquipUpgrade = dataEquipUpgrades[(int)equipData.equipType - 1];

        equipName.text = dataEquipUpgrade.names[(int)equipData.equipMaterial];

        uIEquip.LoadEquip(equipData);

        for (int i = 0; i < textIndexes.Length; i++)
        {
            textIndexes[i].text = dataEquipUpgrade.dataEquipUpgradeChildren[i].texts[(int)equipData.equipMaterial];

            if (i > qualityIndex) textIndexes[i].color = lockColor;
            else textIndexes[i].color = colors[i];
        }

        float totalSize = 0f;

        for (int i = 0; i < barIndexes.Length; i++)
        {
            float y = textIndexes[i].preferredHeight + 30;

            barIndexes[i].sizeDelta = new Vector2(barIndexes[i].sizeDelta.x, y);

            totalSize += y;
        }

        rectPopup.sizeDelta = new Vector2(rectPopup.sizeDelta.x, 750 + totalSize);
        border.sizeDelta = new Vector2(border.sizeDelta.x, totalSize);
    }

    void UpdateIndex(EquipData equipData)
    {
        int level = UIInventory.instance.GetLevel(equipData);

        int value = UIInventory.instance.GetValue(level, equipData.equipType);

        textValue.text = value.ToString();

        int gold = GameManager.instance.Gold;
        int goldUpgrade = UIInventory.instance.GetGoldUpgrade();

        int amountMaterial = equipData.equipType == EquipType.Weapon ? GameManager.instance.IronAmount : amountMaterial = GameManager.instance.ClothAmount;
        int amountUpgradeMaterial = UIInventory.instance.GetAmountMaterialUpgrade(equipData.equipType);

        string textGold = "<color=" + (gold < goldUpgrade ? "red" : "white") + ">" + gold + "</color>/" + goldUpgrade;
        string textMaterial = "<color=" + (amountMaterial < amountUpgradeMaterial ? "red" : "white") + ">" + amountMaterial + "</color>/" + amountUpgradeMaterial;

        textAmountGold.text = textGold;
        textAmountMaterial.text = textMaterial;

        bool isUpgrade = gold >= goldUpgrade && amountMaterial >= amountUpgradeMaterial && level < 1000;

        for (int i = 0; i < btnsUpgrade.Length; i++)
        {
            btnsDisable[i].SetActive(!isUpgrade);
            btnsUpgrade[i].interactable = isUpgrade;
        }

        textLevel.text = "Level: " + level + "/1000";
    }

    public void Upgrade()
    {
        EquipData equipData = uIEquipedSelect.equipData;

        int gold = GameManager.instance.Gold;
        int goldUpgrade = UIInventory.instance.GetGoldUpgrade();

        int amountMaterial = equipData.equipType == EquipType.Weapon ? GameManager.instance.IronAmount : amountMaterial = GameManager.instance.ClothAmount;
        int amountUpgradeMaterial = UIInventory.instance.GetAmountMaterialUpgrade(equipData.equipType);

        if (gold < goldUpgrade || amountMaterial < amountUpgradeMaterial || UIInventory.instance.GetLevel(equipData) == 1000) return;

        GameManager.instance.Gold -= goldUpgrade;

        switch (equipData.equipType)
        {
            case EquipType.Weapon: GameManager.instance.WeaponLevel++; break;
            case EquipType.Hat: GameManager.instance.HatLevel++; break;
            case EquipType.Armor: GameManager.instance.ArmorLevel++; break;
            case EquipType.Shoes: GameManager.instance.ShoesLevel++; break;
        }

        if (equipData.equipType == EquipType.Weapon) GameManager.instance.IronAmount -= amountUpgradeMaterial;
        else GameManager.instance.ClothAmount -= amountUpgradeMaterial;

        UpdateIndex(equipData);
    }

    public void UpgradeMax()
    {
        EquipData equipData = uIEquipedSelect.equipData;

        while (true)
        {
            int gold = GameManager.instance.Gold;
            int goldUpgrade = UIInventory.instance.GetGoldUpgrade();

            int amountMaterial = equipData.equipType == EquipType.Weapon ? GameManager.instance.IronAmount : amountMaterial = GameManager.instance.ClothAmount;
            int amountUpgradeMaterial = UIInventory.instance.GetAmountMaterialUpgrade(equipData.equipType);

            if (gold < goldUpgrade || amountMaterial < amountUpgradeMaterial || UIInventory.instance.GetLevel(equipData) == 1000) break;

            GameManager.instance.Gold -= goldUpgrade;

            switch (equipData.equipType)
            {
                case EquipType.Weapon: GameManager.instance.WeaponLevel++; break;
                case EquipType.Hat: GameManager.instance.HatLevel++; break;
                case EquipType.Armor: GameManager.instance.ArmorLevel++; break;
                case EquipType.Shoes: GameManager.instance.ShoesLevel++; break;
            }

            if (equipData.equipType == EquipType.Weapon) GameManager.instance.IronAmount -= amountUpgradeMaterial;
            else GameManager.instance.ClothAmount -= amountUpgradeMaterial;
        }

        UpdateIndex(equipData);
    }

    public void UnEquip()
    {
        uIEquip.equipData.isEquip = false;

        UIEquipBottomController.instance.SaveEquips();
        UIEquipBottomController.instance.LoadEquips();

        uIEquipedSelect.Deactive();

        Hide();
    }
}
