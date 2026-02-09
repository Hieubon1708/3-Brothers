using System;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class UIHome : MonoBehaviour
{
    public GameObject shopChestKeyNotice;
    public GameObject shopChestFreeNotice;
    public GameObject inventoryUpgradeNotice;
    public GameObject inventoryMergeNotice;

    public GameObject shop;

    UIChestButton[] uIChestButtons;

    private void Awake()
    {
        uIChestButtons = GetComponentsInChildren<UIChestButton>(true);

        for (int i = 0; i < uIChestButtons.Length; i++)
        {
            uIChestButtons[i].LoadData();
        }
    }

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        bool isUpgrade = IsUpgrade();
        bool canMerge = CanMerge();

        inventoryUpgradeNotice.SetActive(isUpgrade);
        inventoryMergeNotice.SetActive(canMerge && !isUpgrade);
    }

    private void Update()
    {
        bool isFree = false;
        bool haveKey = false;

        for (int i = 0; i < uIChestButtons.Length; i++)
        {
            TimeSpan timeRemaining = uIChestButtons[i].FreeTime - DateTime.Now;

            if (timeRemaining.TotalSeconds <= 0) isFree = true;
            if (uIChestButtons[i].ChestKey > 0) haveKey = true;
        }

        shopChestFreeNotice.SetActive(isFree);
        shopChestKeyNotice.SetActive(haveKey && !isFree);
    }

    public void ShowShop()
    {
        shop.SetActive(true);
    }

    public void ShowInventory()
    {
        UIInventory.instance.Show();
    }

    public bool IsUpgrade()
    {
        List<EquipData> equipDatas = GameManager.instance.Equipments;

        for (int i = 0; i < equipDatas.Count; i++)
        {
            if (equipDatas[i].isEquip)
            {
                int gold = GameManager.instance.Gold;
                int goldUpgrade = DataController.instance.GetGoldEquipUpgrade();

                int amountMaterial = equipDatas[i].equipType == EquipType.Weapon ? GameManager.instance.IronAmount : amountMaterial = GameManager.instance.ClothAmount;
                int amountUpgradeMaterial = DataController.instance.GetMaterialEquipUpgrade(equipDatas[i].equipType);

                bool isUpgrade = !(gold < goldUpgrade || amountMaterial < amountUpgradeMaterial || DataController.instance.GetEquipLevel(equipDatas[i]) == 1000);

                if (isUpgrade) return true;
            }
        }
        return false;
    }

    public bool CanMerge()
    {
        List<EquipData> equipDatas = GameManager.instance.Equipments;

        for (int i = 0; i < equipDatas.Count;)
        {
            List<EquipData> checkCount = new List<EquipData>() { equipDatas[i] };

            for (int j = i + 1; j < equipDatas.Count; j++)
            {
                if (UIInventory.instance.IsSame(equipDatas[i], equipDatas[j])) checkCount.Add(equipDatas[j]);
            }
            if (checkCount.Count >= 3) return true;
        }
        return false;
    }
}
