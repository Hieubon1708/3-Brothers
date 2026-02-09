using System;
using UnityEngine;

public class UIHome : MonoBehaviour
{
    public GameObject shopChestKeyNotice;
    public GameObject shopChestFreeNotice;
    public GameObject inventoryUpgradeNotice;
    public GameObject inventoryMergeNotice;

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        bool isUpgrade = UIEquipeTopController.instance.IsUpgrade();
        bool canMerge = UIMergeController.instance.CanMerge();

        inventoryUpgradeNotice.SetActive(isUpgrade);
        inventoryMergeNotice.SetActive(canMerge && !isUpgrade);
    }

    private void Update()
    {
        bool isFree = UIChestController.instance.isFree;
        bool haveKey = UIChestController.instance.haveKey;

        shopChestFreeNotice.SetActive(isFree);
        shopChestKeyNotice.SetActive(haveKey && !isFree);
    }

    public void ShowShop()
    {
        UIShop.instance.shop.SetActive(true);
    }

    public void HideShop()
    {
        UIShop.instance.shop.SetActive(false);
    }

    public void ShowInventory(bool isAnimation)
    {
        UIInventory.instance.inventory.SetActive(true);
        if (isAnimation) UIInventory.instance.inventoryAni.Play();
    }

    public void HideInventory()
    {
        UIInventory.instance.inventory.SetActive(false);
    }
}
