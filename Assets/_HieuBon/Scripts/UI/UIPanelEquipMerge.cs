using TMPro;
using UnityEngine;
using static GameController;

public class UIPanelEquipMerge : MonoBehaviour
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

    public RectTransform rectPopup;

    public void Show()
    {
        if (ani == null) ani = GetComponent<Animation>();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadData(EquipData equipData)
    {
        if (uIEquip == null) uIEquip = GetComponentInChildren<UIEquip>(true);

        indexType[0].SetActive(equipData.equipType == EquipType.Weapon);
        indexType[1].SetActive(equipData.equipType != EquipType.Weapon);

        int qualityIndex = (int)equipData.equipQuality;

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

        rectPopup.sizeDelta = new Vector2(rectPopup.sizeDelta.x, 600 + totalSize);
        border.sizeDelta = new Vector2(border.sizeDelta.x, totalSize);
    }
}
