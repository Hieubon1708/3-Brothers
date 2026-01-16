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
    List<EquipData> equipDatas = new List<EquipData>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EquipType[] t = ((EquipType[])Enum.GetValues(typeof(EquipType)));
            EquipQuality[] q = ((EquipQuality[])Enum.GetValues(typeof(EquipQuality)));
            EquipMaterial[] m = ((EquipMaterial[])Enum.GetValues(typeof(EquipMaterial)));

            EquipType rt = t[UnityEngine.Random.Range(1, t.Length)];
            EquipQuality rq = q[UnityEngine.Random.Range(0, q.Length)];
            EquipMaterial rm = m[UnityEngine.Random.Range(0, m.Length)];

            EquipData equipData = new EquipData(rt, rq, rm);

            equipDatas.Add(equipData);

            Debug.Log(rt + " " + rq + " " + rm);

            LoadEquips();
        }
    }

    void LoadEquips()
    {
        for (int i = 0; i < equipDatas.Count; i++)
        {
            if (i == equips.Count)
            {
                UIEquip uIEquip = Instantiate(preEquip, equipContainer).GetComponent<UIEquip>();

                equips.Add(uIEquip);
            }

            equips[i].LoadEquip(equipDatas[i].equipType, equipDatas[i].equipQuality, equipDatas[i].equipMaterial);
        }
    }
}

[System.Serializable]
public class EquipData
{
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
