using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameController;

public class UIEquipBottomController : MonoBehaviour
{
    public static UIEquipBottomController instance;

    public GameObject preEquip;

    public Transform equipContainer;

    List<UIEquip> equips = new List<UIEquip>();
    [HideInInspector]
    public List<EquipData> equipDatas = new List<EquipData>();
    List<UIEquipBottom> equipsBottom = new List<UIEquipBottom>();

    bool isSortByType;

    public TextMeshProUGUI textSort;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        equipDatas = GameManager.instance.Equipments;

        LoadEquips();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EquipType[] t = (EquipType[])Enum.GetValues(typeof(EquipType));
            EquipQuality[] q = (EquipQuality[])Enum.GetValues(typeof(EquipQuality));
            EquipMaterial[] m = (EquipMaterial[])Enum.GetValues(typeof(EquipMaterial));

            EquipType rt = t[UnityEngine.Random.Range(1, t.Length)];
            EquipQuality rq = q[UnityEngine.Random.Range(0, q.Length)];
            EquipMaterial rm = m[UnityEngine.Random.Range(0, rt == EquipType.Weapon ? m.Length - 1 : m.Length - 3)];

            // EquipData equipData = new EquipData(EquipType.Weapon, EquipQuality.Q1, EquipMaterial.M1);
            EquipData equipData = new EquipData(rt, rq, rm);

            equipDatas.Add(equipData);

            Debug.Log(rt + " " + rq + " " + rm);

            LoadEquips();
            SaveEquips();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIMergeController.instance.LoadData();
        }
    }

    public void LoadEquips()
    {
        if (isSortByType) SortByType();
        else SortByQuality();

        int count = 0;

        for (int i = 0; i < equipDatas.Count; i++)
        {
            if (equipDatas[i].isEquip)
            {
                UIEquipeTopController.instance.Equip(equipDatas[i]);
            }
            else
            {
                if (count >= equips.Count)
                {
                    GameObject e = Instantiate(preEquip, equipContainer);
                    UIEquip uIEquip = e.GetComponent<UIEquip>();
                    UIEquipBottom uIEquipBottom = e.GetComponent<UIEquipBottom>();

                    equips.Add(uIEquip);
                    equipsBottom.Add(uIEquipBottom);
                }

                equips[count].LoadEquip(equipDatas[i]);
                equipsBottom[count].uIEquipAlert.IsNew(equipDatas[i]);

                count++;
            }
        }

        for (int i = count; i < equips.Count; i++)
        {
            equips[i].gameObject.SetActive(false);
        }

        UIEquipeTopController.instance.IsAnyUpgrade();
    }

    public void SaveEquips()
    {
        GameManager.instance.Equipments = equipDatas;
    }

    public void Sort()
    {
        textSort.text = "Sort By " + (isSortByType ? "Type" : "Quality");

        isSortByType = !isSortByType;

        LoadEquips();
    }

    public void SortByQuality()
    {
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
    }

    public void SortByType()
    {
        for (int i = 0; i < equipDatas.Count - 1; i++)
        {
            for (int j = i + 1; j < equipDatas.Count; j++)
            {
                if (equipDatas[i].equipType > equipDatas[j].equipType)
                {
                    EquipData temp = equipDatas[i];
                    equipDatas[i] = equipDatas[j];
                    equipDatas[j] = temp;
                }
                else if (equipDatas[i].equipType == equipDatas[j].equipType && equipDatas[i].equipQuality < equipDatas[j].equipQuality)
                {
                    EquipData temp = equipDatas[i];
                    equipDatas[i] = equipDatas[j];
                    equipDatas[j] = temp;
                }
            }
        }
    }
}

[System.Serializable]
public class EquipData
{
    public bool isEquip;
    public bool isNew = true;

    public EquipType equipType;
    public EquipQuality equipQuality;
    public EquipMaterial equipMaterial;

    public EquipData(EquipType equipType, EquipQuality equipQuality, EquipMaterial equipMaterial)
    {
        this.equipType = equipType;
        this.equipQuality = equipQuality;
        this.equipMaterial = equipMaterial;
    }
}
