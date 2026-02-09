using UnityEngine;
using static GameController;

public class UIEquipeTopController : MonoBehaviour
{
    public static UIEquipeTopController instance;

    [HideInInspector]
    public UIEquipTop[] uIEquipeds;

    private void Awake()
    {
        instance = this;

        uIEquipeds = GetComponentsInChildren<UIEquipTop>(true);
    }

    public void Equip(EquipData equipData)
    {
        int index = (int)equipData.equipType - 1;

        int level = 1;

        switch (equipData.equipType)
        {
            case EquipType.Weapon: level = GameManager.instance.WeaponLevel; break;
            case EquipType.Hat: level = GameManager.instance.HatLevel; break;
            case EquipType.Armor: level = GameManager.instance.ArmorLevel; break;
            case EquipType.Shoes: level = GameManager.instance.ShoesLevel; break;
        }

        uIEquipeds[index].LoadData(equipData, level);
    }

    public void Unequip(UIEquipBottom uIEquipSelect)
    {
        UIEquip uIEquip = uIEquipSelect.uIEquip;
    }

    public void CheckUpgradeNotice(EquipData equipData)
    {
        for (int i = 0; i < uIEquipeds.Length; i++)
        {
            uIEquipeds[i].CheckUpgradeNotice();
        }
    }

    public bool IsUpgrade()
    {
        for (int i = 0; i < uIEquipeds.Length; i++)
        {
            if (uIEquipeds[i].IsUpgrade()) return true;
        }
        return false;
    }

    public bool IsPriority(EquipData equipData)
    {
        for (int i = 0; i < uIEquipeds.Length; i++)
        {
            if (uIEquipeds[i].equipData.equipType == equipData.equipType) return uIEquipeds[i].IsPriority(equipData);
        }
        return true;
    }
}
