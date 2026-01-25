using UnityEngine;
using static GameController;

public class UIEquip : MonoBehaviour
{
    public bool isUseTypeIcon;

    public GameObject[] frames;
    public GameObject[] iconFrames;
    public GameObject[] icons;
    public GameObject[] weaponIcons;
    public GameObject[] hatIcons;
    public GameObject[] armorIcons;
    public GameObject[] shoesIcons;

    public EquipData equipData;

    public void LoadEquip(EquipData equipData)
    {
        this.equipData = equipData;

        int iconIndex = (int)equipData.equipMaterial;
        int frameIndex = (int)equipData.equipQuality;

        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].SetActive(i == frameIndex);
        }

        if (isUseTypeIcon)
        {
            for (int i = 0; i < iconFrames.Length; i++)
            {
                iconFrames[i].SetActive(i == frameIndex);
            }

            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].SetActive(i == (int)equipData.equipType - 1);
            }
        }

        for (int i = 0; i < weaponIcons.Length; i++)
        {
            weaponIcons[i].SetActive(i == iconIndex && equipData.equipType == EquipType.Weapon);
        }
        for (int i = 0; i < hatIcons.Length; i++)
        {
            hatIcons[i].SetActive(i == iconIndex && equipData.equipType == EquipType.Hat);
        }
        for (int i = 0; i < armorIcons.Length; i++)
        {
            armorIcons[i].SetActive(i == iconIndex && equipData.equipType == EquipType.Armor);
        }
        for (int i = 0; i < shoesIcons.Length; i++)
        {
            shoesIcons[i].SetActive(i == iconIndex && equipData.equipType == EquipType.Shoes);
        }
    }
}
