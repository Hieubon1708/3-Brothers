using UnityEngine;

public class UIEquipSelect : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;

    private void Awake()
    {
        uIEquip = GetComponent<UIEquip>();
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
