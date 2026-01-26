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
}
