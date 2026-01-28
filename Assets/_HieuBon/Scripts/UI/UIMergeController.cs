using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UIMergeController : MonoBehaviour
{
    public static UIMergeController instance;

    List<UIEquip> equips = new List<UIEquip>();

    public GameObject preEquip;

    public Transform equipContainer;

    private void Awake()
    {
        instance = this;
    }

    public void LoadData()
    {
        List<EquipData> equipDatas = new List<EquipData>(UIEquipBottomController.instance.equipDatas);
        List<EquipData> mergeEquips = new List<EquipData>();

        for (int i = 0; i < equipDatas.Count;)
        {
            List<EquipData> checkCount = new List<EquipData>() { equipDatas[i] };

            for (int j = i + 1; j < equipDatas.Count; j++)
            {
                if (IsSame(equipDatas[i], equipDatas[j])) checkCount.Add(equipDatas[j]);
            }

            if (checkCount.Count >= 3) mergeEquips.Add(equipDatas[i]);

            for (int j = 0; j < checkCount.Count; j++)
            {
                equipDatas.Remove(checkCount[j]);
            }
        }

        equipDatas = UIEquipBottomController.instance.equipDatas;

        for (int i = 0; i < equipDatas.Count - 1; i++)
        {
            for (int j = i + 1; j < equipDatas.Count; j++)
            {
                if (equipDatas[i].equipQuality < equipDatas[j].equipQuality)
                {
                    EquipData temp = equipDatas[i];
                    equipDatas[i] = equipDatas[j];
                    equipDatas[j] = temp;
                }
                else if (equipDatas[i].equipQuality == equipDatas[j].equipQuality
                    && equipDatas[i].equipType > equipDatas[j].equipType)
                {
                    EquipData temp = equipDatas[i];
                    equipDatas[i] = equipDatas[j];
                    equipDatas[j] = temp;
                }
            }
        }

        for (int i = 0; i < mergeEquips.Count - 1; i++)
        {
            for (int j = i + 1; j < mergeEquips.Count; j++)
            {
                if (mergeEquips[i].equipQuality > mergeEquips[j].equipQuality)
                {
                    EquipData temp = mergeEquips[i];
                    mergeEquips[i] = mergeEquips[j];
                    mergeEquips[j] = temp;
                }
                else if (mergeEquips[i].equipQuality == mergeEquips[j].equipQuality
                    && mergeEquips[i].equipType < mergeEquips[j].equipType)
                {
                    EquipData temp = mergeEquips[i];
                    mergeEquips[i] = mergeEquips[j];
                    mergeEquips[j] = temp;
                }
            }
        }

        for (int k = 0; k < mergeEquips.Count; k++)
        {
            for (int i = 0; i < equipDatas.Count; i++)
            {
                if (IsSame(mergeEquips[k], equipDatas[i]))
                {
                    EquipData equipData = equipDatas[i];
                    equipDatas.RemoveAt(i);
                    equipDatas.Insert(0, equipData);
                }
            }
        }

        for (int i = 0; i < equipDatas.Count; i++)
        {
            if (i >= equips.Count)
            {
                UIEquip uIEquip = Instantiate(preEquip, equipContainer).GetComponent<UIEquip>();

                equips.Add(uIEquip);
            }

            equips[i].LoadEquip(equipDatas[i]);
        }

        for (int i = equipDatas.Count; i < equips.Count; i++)
        {
            equips[i].gameObject.SetActive(false);
        }
    }

    bool IsSame(EquipData equipData1, EquipData equipData2)
    {
        return equipData1.equipType == equipData2.equipType
                && equipData1.equipQuality == equipData2.equipQuality
                && equipData1.equipMaterial == equipData2.equipMaterial;
    }
}
