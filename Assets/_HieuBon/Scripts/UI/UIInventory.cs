using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    [HideInInspector]
    public UIEquipInfo uIEquipInfo;

    private void Awake()
    {
        instance = this;

        uIEquipInfo = GetComponentInChildren<UIEquipInfo>(true);
    }

    public void Select(UIEquipSelect uIEquipSelect)
    {
        uIEquipInfo.Show(uIEquipSelect);
    }
}
