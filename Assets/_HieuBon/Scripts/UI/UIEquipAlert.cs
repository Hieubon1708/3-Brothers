using UnityEngine;

public class UIEquipAlert : MonoBehaviour
{
    public GameObject[] icons;

    public void IsNew(EquipData equipData)
    {
        if (equipData.isNew) equipData.isNew = false;

        icons[0].SetActive(equipData.isNew);
    }

    public void IsPriority(bool isPriority)
    {
        icons[1].SetActive(isPriority);
    }

    public void IsUpgrade(bool isUpgrade)
    {
        icons[2].SetActive(isUpgrade);
    }

    public void IsMerge(bool isMerge)
    {
        icons[4].SetActive(isMerge);
    }
}
