using UnityEngine;

public class UIMergeSlot : MonoBehaviour
{
    public UIMergeController.SlotType slotType;

    [HideInInspector]
    public UIEquipMerge uIEquipMerge;
    UIEquip uIEquip;

    CanvasGroup canvasGroup;

    public bool isEmpty = true;

    private void Awake()
    {
        uIEquip = GetComponentInChildren<UIEquip>(true);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void LoadData(UIEquipMerge uIEquipMerge)
    {
        uIEquip.LoadEquip(uIEquipMerge.uIEquip.equipData);

        if (slotType == UIMergeController.SlotType.Top)
        {
            EquipData equipData = uIEquipMerge.uIEquip.equipData;
            EquipData upgradeEquip = new EquipData(equipData.equipType, equipData.equipQuality + 1, equipData.equipMaterial);

            uIEquip.LoadEquip(upgradeEquip);
        }
        else if (slotType == UIMergeController.SlotType.Bottom)
        {
            this.uIEquipMerge = uIEquipMerge;

            isEmpty = false;
        }
        else transform.SetAsFirstSibling();

        canvasGroup.alpha = 1;
    }

    public void Show(UIEquipMerge uIEquipMerge)
    {
        if (slotType == UIMergeController.SlotType.Left
            || slotType == UIMergeController.SlotType.Right)
        {
            this.uIEquipMerge = uIEquipMerge;

            isEmpty = false;

            transform.SetAsLastSibling();
        }
    }

    public void Hide(bool isAll = true)
    {
        transform.SetAsFirstSibling();

        isEmpty = true;

        if (isAll) canvasGroup.alpha = 0;

        if (uIEquipMerge != null) uIEquipMerge.Active();
    }

    public void OnClick()
    {
        if (slotType == UIMergeController.SlotType.Top)
        {
        }
        else
        {
            if (isEmpty) return;

            if (slotType == UIMergeController.SlotType.Bottom) UIMergeController.instance.HideAll();
            else
            {
                Hide(false);
                UIMergeController.instance.Hide();
            }
        }
    }
}
