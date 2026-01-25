using UnityEngine;
using static GameController;

public class UIEquipedController : MonoBehaviour
{
    public static UIEquipedController instance;

    [HideInInspector]
    public UIEquipedSelect[] uIEquipeds;

    private void Awake()
    {
        instance = this;

        uIEquipeds = GetComponentsInChildren<UIEquipedSelect>(true);
    }

    public void Equip(EquipData equipData)
    {
        int index = (int)equipData.equipType - 1;

        int level = 1;

        switch (equipData.equipType)
        {
            case EquipType.Weapon: level = GameManager.instance.WeaponLevel; break;
            case EquipType.Hat: level = GameManager.instance.WeaponLevel; break;
            case EquipType.Armor: level = GameManager.instance.WeaponLevel; break;
            case EquipType.Shoes: level = GameManager.instance.WeaponLevel; break;
        }

        uIEquipeds[index].LoadData(equipData, level);
    }

    public void Unequip(UIEquipSelect uIEquipSelect)
    {
        UIEquip uIEquip = uIEquipSelect.uIEquip;
    }
}
