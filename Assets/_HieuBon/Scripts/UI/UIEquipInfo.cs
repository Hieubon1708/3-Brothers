using TMPro;
using UnityEngine;

public class UIEquipInfo : MonoBehaviour
{
    public Color lockColor;
    public Color[] colors;
    public RectTransform[] barIndexes;
    public TextMeshProUGUI[] textIndexes;

    public DataEquipUpgrade dataEquipUpgrade;

    public RectTransform border;

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        for (int i = 0; i < textIndexes.Length; i++)
        {
            textIndexes[i].text = dataEquipUpgrade.dataEquipUpgradeChildren[i].texts[0];
            textIndexes[i].color = colors[i];
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
}
