using UnityEngine;

public class UIEquipBottom : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;
    [HideInInspector]
    public UIEquipAlert uIEquipAlert;

    private void Awake()
    {
        uIEquip = GetComponent<UIEquip>();
        uIEquipAlert = GetComponent<UIEquipAlert>();
    }

    public void OnClick()
    {
        UIInventory.instance.Select(this);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    public void IsPriority()
    {
        uIEquipAlert.IsPriority(!UIEquipeTopController.instance.IsEquiped(uIEquip.equipData.equipType) 
            || UIEquipeTopController.instance.IsGreaterQuality(uIEquip.equipData.equipType, uIEquip.equipData.equipQuality));
    }
}
