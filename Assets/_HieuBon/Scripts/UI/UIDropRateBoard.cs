using System;
using TMPro;
using UnityEngine;
using static GameController;

public class UIDropRateBoard : MonoBehaviour
{
    public GameObject pre;

    public EquipQuality equipQuality;

    public Transform container;

    public TextMeshProUGUI textRate;

    private void Awake()
    {
        EquipType[] t = (EquipType[])Enum.GetValues(typeof(EquipType));
        EquipMaterial[] m = (EquipMaterial[])Enum.GetValues(typeof(EquipMaterial));

        for (int i = 1; i < t.Length; i++)
        {
            int mLength = t[i] == EquipType.Weapon ? m.Length : m.Length - 2;
            for (int j = 0; j < mLength; j++)
            {
                GameObject e = Instantiate(pre, container);
                EquipData equipData = new EquipData(t[i], equipQuality, m[j]);
                e.GetComponent<UIEquip>().LoadEquip(equipData);
                e.GetComponent<UIDropRateEquip>().equipData = equipData;
            }
        }
    }
}
