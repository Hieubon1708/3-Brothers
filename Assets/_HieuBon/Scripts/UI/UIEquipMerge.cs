using UnityEngine;

public class UIEquipMerge : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;

    private void Awake()
    {
        uIEquip = GetComponent<UIEquip>();
    }

    public void OnClick()
    {
        UIMergeController.instance.AddSlot(this);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}
