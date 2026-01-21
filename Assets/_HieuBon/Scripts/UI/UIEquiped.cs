using TMPro;
using UnityEngine;

public class UIEquiped : MonoBehaviour
{
    UIEquip uIEquip;

    public GameObject emptySlot;

    public TextMeshProUGUI textLevel;

    public void LoadData(UIEquip uIEquip, int level)
    {
        if (this.uIEquip == null) this.uIEquip = GetComponent<UIEquip>();

        this.uIEquip.LoadEquip(uIEquip.equipType, uIEquip.equipQuality, uIEquip.equipMaterial);

        gameObject.SetActive(true);
        emptySlot.SetActive(false);

        textLevel.text = "Lv." + level;
    }
}
