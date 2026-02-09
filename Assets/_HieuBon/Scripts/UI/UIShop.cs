using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public static UIShop instance;

    public PackData[] goldPackDatas;
    public PackData[] diamondPackDatas;
    public SetPackData[] setPackDatas;
    public DropRateData[] dropRateDatas;

    public Sprite[] bgsPack;

    public UIPanelEquip uIPanelEquip;

    public GameObject shop;

    public enum PackType
    {
        Diamond, Gold
    }

    private void Awake()
    {
        instance = this;

        UIShopPack[] packs = GetComponentsInChildren<UIShopPack>(true);
        UIShopSetPack[] setPacks = GetComponentsInChildren<UIShopSetPack>(true);

        int d = 0;
        int g = 0;

        for (int i = 0; i < packs.Length; i++)
        {
            if (packs[i].type == PackType.Gold)
            {
                packs[i].Initial(goldPackDatas[g], bgsPack[1]);
                packs[i].GetComponent<Button>().onClick.AddListener(() => BuyGoldPack(g));
                g++;
            }
            else if (packs[i].type == PackType.Diamond)
            {
                packs[i].Initial(diamondPackDatas[d], bgsPack[0]);
                packs[i].GetComponent<Button>().onClick.AddListener(() => BuyDiamondPack(d));
                d++;
            }
        }

        for (int i = 0; i < setPacks.Length; i++)
        {
            setPacks[i].Initial(setPackDatas[i]);
            setPacks[i].GetComponent<Button>().onClick.AddListener(() => BuySetPack(i));
        }
    }

    public void BuySetPack(int index)
    {

    }

    public void BuyDiamondPack(int index)
    {

    }

    public void BuyGoldPack(int index)
    {

    }

    public void Select(EquipData equipData)
    {
        uIPanelEquip.Show(equipData);
    }
}

[System.Serializable]
public class PackData
{
    public UIShop.PackType type;

    public string packName;
    public int amount;
    public float price;
    public Sprite icon;
    public float y;
}

[System.Serializable]
public class SetPackData
{
    public int gold;
    public int diamond;
    public EquipData equipData;
    public float price;
}


