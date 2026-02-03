using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIButtonMerge : MonoBehaviour
{
    public GameObject disabled;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Check(int count, int selectCount)
    {
        text.text = selectCount > 0 ? "Confirm" : "Quick Merge";

        disabled.SetActive(!(count > 0 && selectCount == 0 || selectCount == 3));
    }

    public void Merge()
    {
        int selectCount = UIMergeController.instance.SelectCount();

        List<EquipData> mergeEquipDatas = UIMergeController.instance.mergeEquips;

        if (mergeEquipDatas.Count > 0 && selectCount == 0)
        {
            for (int j = 0; j < mergeEquipDatas.Count; j++)
            {
                HandleMerge(mergeEquipDatas[j]);
            }

            UIMergeController.instance.LoadData();
        }
        else if (selectCount == 3)
        {
            HandleMerge(UIMergeController.instance.uIMergeSlots[0].uIEquipMerge.uIEquip.equipData);

            UIMergeController.instance.HideAll();
            UIMergeController.instance.LoadData();
        }
    }

    void HandleMerge(EquipData equipData)
    {
        EquipData upgradeEquip = new EquipData(equipData.equipType, equipData.equipQuality + 1, equipData.equipMaterial);

        List<EquipData> equipDatas = UIEquipBottomController.instance.equipDatas;
        equipDatas.Add(upgradeEquip);

        int count = 3;

        for (int i = 0; i < equipDatas.Count; i++)
        {
            if (UIMergeController.instance.IsSame(equipData, equipDatas[i]))
            {
                equipDatas.RemoveAt(i);
                i--;

                count--;

                if (count == 0) break;
            }
        }
    }
}
