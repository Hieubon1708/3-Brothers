using System;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class UIEquipController : MonoBehaviour
{
    public static UIEquipController instance;

    public GameObject preEquip;

    public Transform equipContainer;

    List<UIEquip> equips = new List<UIEquip>();
    public List<EquipData> equipDatas = new List<EquipData>();

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

            EquipData equipData = new EquipData(rt, rq, rm);

            equipDatas.Add(equipData);

            Debug.Log(rt + " " + rq + " " + rm);

            LoadEquips();
            SaveEquips();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < equipDatas.Count; i++)
            {
                equipDatas[i].isEquip = true;
            }
        }
    }

    public void LoadEquips()
    {
        int count = 0;

        //data
        for (int i = 0; i < equipDatas.Count; i++)
        {
            Debug.Log(equipDatas[i].isEquip);

            if (equipDatas[i].isEquip)
            {
                UIEquipedController.instance.Equip(equipDatas[i]);
            }
            else
            {
                //equips obj grid
                if (count >= equips.Count)
                {
                    UIEquip uIEquip = Instantiate(preEquip, equipContainer).GetComponent<UIEquip>();

                    equips.Add(uIEquip);
                }

                equips[count].LoadEquip(equipDatas[i]);

                count++;
            }
        }
    }

    public void SaveEquips()
    {
        GameManager.instance.Equipments = equipDatas;
    }
}

[System.Serializable]
public class EquipData
{
    public bool isEquip;
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
