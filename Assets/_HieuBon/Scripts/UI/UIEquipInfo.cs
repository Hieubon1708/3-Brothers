using TMPro;
using UnityEngine;
using static GameController;

public class UIEquipInfo : MonoBehaviour
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

    Animation ani;

    [HideInInspector]
    public UIEquipSelect uIEquipSelect;

    public void Show(UIEquipSelect uIEquipSelect)
    {
        this.uIEquipSelect = uIEquipSelect;

        UIEquip uIEquip = uIEquipSelect.uIEquip;

        if (this.uIEquip == null) this.uIEquip = GetComponentInChildren<UIEquip>(true);
        if (ani == null) ani = GetComponent<Animation>();

        LoadData(uIEquip);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadData(UIEquip uIEquip)
    {
        indexType[0].SetActive(uIEquip.equipType == EquipType.Weapon);
        indexType[1].SetActive(uIEquip.equipType != EquipType.Weapon);

        int qualityIndex = (int)uIEquip.equipQuality;

        for (int i = 0; i < labels.Length; i++)
        {
            labels[i].SetActive(i == qualityIndex);
        }

        for (int i = 0; i < locks.Length; i++)
        {
            locks[i].SetActive(i > qualityIndex);
        }

        DataEquipUpgrade dataEquipUpgrade = dataEquipUpgrades[(int)uIEquip.equipType - 1];

        equipName.text = dataEquipUpgrade.names[(int)uIEquip.equipMaterial];

        this.uIEquip.LoadEquip(uIEquip.equipType, uIEquip.equipQuality, uIEquip.equipMaterial);

        for (int i = 0; i < textIndexes.Length; i++)
        {
            textIndexes[i].text = dataEquipUpgrade.dataEquipUpgradeChildren[i].texts[(int)uIEquip.equipMaterial];

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

        border.sizeDelta = new Vector2(border.sizeDelta.x, totalSize);
    }

    public void Equip()
    {
        UIEquipedController.instance.Equip(uIEquipSelect);

        Hide();
    }
}
