using UnityEngine;

public class UIDropRateEquip : MonoBehaviour
{
    [HideInInspector]
    public EquipData equipData;

    public void OnClick()
    {
        UIShop.instance.Select(equipData);
    }
}
