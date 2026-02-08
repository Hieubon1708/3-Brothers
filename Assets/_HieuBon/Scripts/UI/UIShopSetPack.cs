using TMPro;
using UnityEngine;

public class UIShopSetPack : MonoBehaviour
{
    public TextMeshProUGUI textGold;
    public TextMeshProUGUI textDiamond;
    public TextMeshProUGUI textPrice;

    public void Initial(SetPackData setPackData)
    {
        if (textGold != null) textGold.text = setPackData.gold + " Gold";
        if (textDiamond != null) textDiamond.text = setPackData.diamond + " Diamond";
        textPrice.text = "$ " + setPackData.price.ToString();
    }
}
