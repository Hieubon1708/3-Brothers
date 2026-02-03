using UnityEngine;

public class UIEquipMerge : MonoBehaviour
{
    [HideInInspector]
    public UIEquip uIEquip;
    [HideInInspector]
    public UIEquipAlert uIEquipAlert;

    Canvas canvas;

    private void Awake()
    {
        uIEquip = GetComponent<UIEquip>();
        uIEquipAlert = GetComponent<UIEquipAlert>();
        canvas = GetComponent<Canvas>();
    }

    public void OnClick()
    {
        if (uIEquip.equipData.equipQuality == GameController.EquipQuality.Q6)
        {
            return;
        }

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

    public void IsCanMerge(bool isCanMerge)
    {
        canvas.overrideSorting = isCanMerge;
        canvas.sortingOrder = isCanMerge ? 2 : 0;
    }
}
