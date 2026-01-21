using UnityEngine;

public class UIEquipSelect : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;

    private void Awake()
    {
        uIEquip = GetComponent<UIEquip>();
    }

    private void Start()
    {
        uIEquip.LoadEquip(uIEquip.equipType, uIEquip.equipQuality, uIEquip.equipMaterial);
    }

    public void OnClick()
    {
        UIInventory.instance.Select(this);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
