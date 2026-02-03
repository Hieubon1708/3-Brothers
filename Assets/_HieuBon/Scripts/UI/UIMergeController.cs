using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMergeController : MonoBehaviour
{
    public static UIMergeController instance;

    [HideInInspector]
    public List<UIEquip> equips = new List<UIEquip>();
    List<UIEquipMerge> equipsMerge = new List<UIEquipMerge>();

    public GameObject preEquip;

    public Transform equipContainer;

    [HideInInspector]
    public List<EquipData> mergeEquips;
    List<EquipData> equipDatas;

    UIButtonMerge uIButtonMerge;
    [HideInInspector]
    public UIMergeSlot[] uIMergeSlots;
    [HideInInspector]
    public UIPanelEquipMerge uIPanelEquipMerge;
    public UIMergeFilter uIMergeFilter;

    public GameObject fade;

    ScrollRect scrollRect;

    public enum SlotType
    {
        Bottom, Left, Right, Top
    }

    private void Awake()
    {
        instance = this;

        uIButtonMerge = GetComponentInChildren<UIButtonMerge>(true);
        scrollRect = GetComponentInChildren<ScrollRect>(true);
        uIPanelEquipMerge = GetComponentInChildren<UIPanelEquipMerge>(true);
        uIMergeFilter = GetComponentInChildren<UIMergeFilter>(true);
        uIMergeSlots = GetComponentsInChildren<UIMergeSlot>(true);
    }

    public void HideAll()
    {
        for (int i = 0; i < uIMergeSlots.Length; i++)
        {
            uIMergeSlots[i].Hide();
        }

        uIButtonMerge.Check(mergeEquips.Count, 0);

        fade.SetActive(false);

        for (int h = 0; h < equipsMerge.Count; h++)
        {
            equipsMerge[h].IsCanMerge(false);
        }

        scrollRect.vertical = true;

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

        GenerateEquip();
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
                    for (int h = 0; h < equipDatas.Count; h++)
                    {
                        bool isSame = IsSame(uIEquipMerge.uIEquip.equipData, equipDatas[h]);

                        if (isSame)
                        {
                            EquipData equipData = equipDatas[h];
                            equipDatas.RemoveAt(h);
                            equipDatas.Insert(0, equipData);
                        }

                        equipsMerge[h].IsCanMerge(isSame);
                    }

                    for (int h = 0; h < equipsMerge.Count; h++)
                    {
                        bool isSame = IsSame(uIEquipMerge.uIEquip.equipData, equipDatas[h]);

                        equipsMerge[h].IsCanMerge(isSame);
                        equipsMerge[h].uIEquipAlert.IsMerge(isSame);
                    }

                    GenerateEquip();

                    scrollRect.normalizedPosition = new Vector2(0, 1);
                    scrollRect.vertical = false;

                    uIMergeFilter.Hide();

                    fade.SetActive(true);

                    for (int j = 0; j < uIMergeSlots.Length; j++)
                    {
                        uIMergeSlots[j].LoadData(equipsMerge[0]);
                    }

                    equipsMerge[0].Deactive();

                    break;
                }

                EquipData equipData1 = uIMergeSlots[0].uIEquipMerge.uIEquip.equipData;
                EquipData equipData2 = uIEquipMerge.uIEquip.equipData;

                if (IsSame(equipData1, equipData2))
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

    public void LoadData()
    {
        equipDatas = new List<EquipData>(UIEquipBottomController.instance.equipDatas);
        mergeEquips = new List<EquipData>();

        for (int i = 0; i < equipDatas.Count;)
        {
            List<EquipData> checkCount = new List<EquipData>() { equipDatas[i] };

            for (int j = i + 1; j < equipDatas.Count; j++)
            {
                if (IsSame(equipDatas[i], equipDatas[j])) checkCount.Add(equipDatas[j]);

                if (checkCount.Count == 3)
                {
                    mergeEquips.Add(equipDatas[i]);
                    break;
                }
            }

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

        for (int i = equipDatas.Count; i < equips.Count; i++)
        {
            equips[i].gameObject.SetActive(false);
        }

        GenerateEquip();
    }

    void GenerateEquip()
    {
        for (int i = 0; i < equipDatas.Count; i++)
        {
            if (i >= equips.Count)
            {
                GameObject e = Instantiate(preEquip, equipContainer);

                UIEquip uIEquip = e.GetComponent<UIEquip>();
                UIEquipMerge uIEquipMerge = e.GetComponent<UIEquipMerge>();

                equipsMerge.Add(uIEquipMerge);
                equips.Add(uIEquip);
            }

            equips[i].LoadEquip(equipDatas[i]);
        }
    }

    public bool IsSame(EquipData equipData1, EquipData equipData2)
    {
        return equipData1.equipType == equipData2.equipType
                && equipData1.equipQuality == equipData2.equipQuality
                && equipData1.equipMaterial == equipData2.equipMaterial;
    }
}
