using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UIShop;

public class UIShopPack : MonoBehaviour
{
    public PackType type;

    public TextMeshProUGUI textAmount;
    public TextMeshProUGUI textPackName;
    public TextMeshProUGUI textPrice;

    public Image icon;
    public Image bg;

    public void Initial(PackData packData, Sprite bg)
    {
        textAmount.text = packData.amount.ToString();
        textPackName.text = packData.packName;
        textPrice.text = "$ " + packData.price.ToString();
        icon.sprite = packData.icon;

        this.bg.sprite = bg;

        icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, packData.y);
    }
}
