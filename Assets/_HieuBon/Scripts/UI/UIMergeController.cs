using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMergeController : MonoBehaviour
{
    public static UIMergeController instance;

    [HideInInspector]
    public List<UIEquip> equips = new List<UIEquip>();

    public GameObject preEquip;

    public Transform equipContainer;

    List<EquipData> mergeEquips;

    public UIButtonMerge uIButtonMerge;

    public UIMergeSlot[] uIMergeSlots;

    public enum SlotType
    {
        Bottom, Left, Right, Top
    }

    private void Awake()
    {
        instance = this;
    }

    public void HideAll()
    {
        for (int i = 0; i < uIMergeSlots.Length; i++)
        {
            uIMergeSlots[i].Hide();
        }

        uIButtonMerge.Check(mergeEquips.Count, 0);
    }

    public void Hide()
    {
        uIButtonMerge.Check(mergeEquips.Count, SelectCount());
    }

    public void AddSlot(UIEquipMerge uIEquipMerge)
    {
        for (int i = 0; i < uIMergeSlots.Length - 1; i++)
        {
            if (uIMergeSlots[i].isEmpty)
            {
                if (i == 0)
                {
                    for (int j = 0; j < uIMergeSlots.Length; j++)
                    {
                        uIMergeSlots[j].LoadData(uIEquipMerge);
                    }

                    uIEquipMerge.Deactive();

                    break;
                }

                EquipData equipData1 = uIMergeSlots[0].uIEquipMerge.uIEquip.equipData;
                EquipData equipData2 = uIEquipMerge.uIEquip.equipData;

                if (equipData1.equipType == equipData2.equipType
                && equipData1.equipQuality == equipData2.equipQuality
                && equipData1.equipMaterial == equipData2.equipMaterial)
                {
                    uIMergeSlots[i].Show(uIEquipMerge);

                    uIEquipMerge.Deactive();

                    break;
                }
            }
        }

        uIButtonMerge.Check(mergeEquips.Count, SelectCount());
    }

    public int SelectCount()
    {
        int selectCount = 0;

        for (int i = 0; i < uIMergeSlots.Length - 1; i++)
        {
            if (!uIMergeSlots[i].isEmpty) selectCount++;
        }

        return selectCount;
    }

    public bool CanMerge()
    {
        List<EquipData> equipDatas = new List<EquipData>(GameManager.instance.Equipments);
        mergeEquips = new List<EquipData>();

        for (int i = 0; i < equipDatas.Count;)
        {
            List<EquipData> checkCount = new List<EquipData>() { equipDatas[i] };

            for (int j = i + 1; j < equipDatas.Count; j++)
            {
                if (IsSame(equipDatas[i], equipDatas[j])) checkCount.Add(equipDatas[j]);
            }
            if (checkCount.Count >= 3) return true;
        }
        return false;
    }

    public void LoadData()
    {
        List<EquipData> equipDatas = new List<EquipData>(UIEquipBottomController.instance.equipDatas);
        mergeEquips = new List<EquipData>();

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

        uIButtonMerge.Check(mergeEquips.Count, 0);

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

        for (int i = 0; i < equips.Count; i++)
        {
            equips[i].mergeNotice.SetActive(mergeEquips.Contains(equipDatas[i]));
        }
    }

    public bool IsSame(EquipData equipData1, EquipData equipData2)
    {
        return equipData1.equipType == equipData2.equipType
                && equipData1.equipQuality == equipData2.equipQuality
                && equipData1.equipMaterial == equipData2.equipMaterial;
    }
}
