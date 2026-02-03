using UnityEngine;
using static GameController;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [HideInInspector]
    public UIPanelEquipBottom uIEquipInfo;
    [HideInInspector]
    public UIPanelEquipTop uIEquipedInfo;
    [HideInInspector]
    public UIMaterial uIMaterial;

    private void Awake()
    {
        instance = this;

        uIEquipInfo = GetComponentInChildren<UIPanelEquipBottom>(true);
        uIEquipedInfo = GetComponentInChildren<UIPanelEquipTop>(true);
        uIMaterial = GetComponentInChildren<UIMaterial>(true);
    }

    public void Select(UIEquipBottom uIEquipSelect)
    {
        uIEquipInfo.Show(uIEquipSelect);
    }

    public void EquipedSelect(UIEquipTop uIEquipedSelect)
    {
        uIEquipedInfo.Show(uIEquipedSelect);
    }

    public int GetGoldUpgrade()
    {
        return 10;
    }

    public int GetAmountMaterialUpgrade(EquipType equipType)
    {
        if (equipType == EquipType.Weapon) return 10;
        else return 10;
    }

    public int GetLevel(EquipData equipData)
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

    public int GetValue(int level, EquipType equipType)
    {
        return 999;
    }
}
