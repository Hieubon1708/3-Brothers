using UnityEngine;

public class UIEquipedController : MonoBehaviour
{
    public static UIEquipedController instance;

    [HideInInspector]
    public UIEquiped[] uIEquipeds;

    private void Awake()
    {
        instance = this;

        uIEquipeds = GetComponentsInChildren<UIEquiped>(true);
    }

    public void Equip(UIEquipSelect uIEquipSelect)
    {
        uIEquipSelect.Deactive();

        UIEquip uIEquip = uIEquipSelect.uIEquip;

        int index = (int)uIEquip.equipType - 1;

        int level = 1;

        switch (uIEquip.equipType)
        {
            case GameController.EquipType.Weapon: level = GameManager.instance.WeaponLevel; break;
            case GameController.EquipType.Hat: level = GameManager.instance.WeaponLevel; break;
            case GameController.EquipType.Armor: level = GameManager.instance.WeaponLevel; break;
            case GameController.EquipType.Shoes: level = GameManager.instance.WeaponLevel; break;
        }

        uIEquipeds[index].LoadData(uIEquip, level);
    }

    public void Unequip(UIEquipSelect uIEquipSelect)
    {
        UIEquip uIEquip = uIEquipSelect.uIEquip;
    }
}
