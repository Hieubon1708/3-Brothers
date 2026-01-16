using UnityEngine;
using static GameController;

public class UIEquip : MonoBehaviour
{
    public EquipType equipType;
    public EquipQuality equipQuality;
    public EquipMaterial equipMaterial;

    public GameObject[] frames;
    public GameObject[] weaponIcons;
    public GameObject[] hatIcons;
    public GameObject[] armorIcons;
    public GameObject[] shoesIcons;

    public void LoadEquip(EquipType equipType, EquipQuality equipQuality, EquipMaterial equipMaterial)
    {
        this.equipType = equipType;
        this.equipQuality = equipQuality;
        this.equipMaterial = equipMaterial;

        int iconIndex = (int)equipMaterial;
        int frameIndex = (int)equipMaterial;

        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].SetActive(i == frameIndex);
        }

        if (equipType == EquipType.Weapon)
        {
            for (int i = 0; i < weaponIcons.Length; i++)
            {
                weaponIcons[i].SetActive(i == iconIndex);
            }
        }
        else if (equipType == EquipType.Hat)
        {
            for (int i = 0; i < hatIcons.Length; i++)
            {
                hatIcons[i].SetActive(i == iconIndex);
            }
        }
        else if (equipType == EquipType.Armor)
        {
            for (int i = 0; i < armorIcons.Length; i++)
            {
                armorIcons[i].SetActive(i == iconIndex);
            }
        }
        else if (equipType == EquipType.Shoes)
        {
            for (int i = 0; i < shoesIcons.Length; i++)
            {
                shoesIcons[i].SetActive(i == iconIndex);
            }
        }
    }
}
