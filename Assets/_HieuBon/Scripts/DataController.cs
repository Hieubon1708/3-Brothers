using UnityEngine;
using static GameController;

public class DataController : MonoBehaviour
{
    public static DataController instance;

    private void Awake()
    {
        instance = this;
    }

    public int GetGoldEquipUpgrade()
    {
        return 10;
    }

    public int GetMaterialEquipUpgrade(EquipType equipType)
    {
        if (equipType == EquipType.Weapon) return 10;
        else return 10;
    }

    public int GetEquipLevel(EquipData equipData)
    {
        int level = 1;

        switch (equipData.equipType)
        {
            case EquipType.Weapon: level = GameManager.instance.WeaponLevel; break;
            case EquipType.Hat: level = GameManager.instance.HatLevel; break;
            case EquipType.Armor: level = GameManager.instance.ArmorLevel; break;
            case EquipType.Shoes: level = GameManager.instance.ShoesLevel; break;
        }

        return level;
    }

    public int GetEquipValue(int level, EquipType equipType)
    {
        return 999;
    }
}
