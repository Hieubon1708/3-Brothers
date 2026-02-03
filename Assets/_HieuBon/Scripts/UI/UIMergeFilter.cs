using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMergeFilter : MonoBehaviour
{
    bool isShowing;

    public GameObject dropdownMenu;

    public GameObject[] iconsFilter;

    public void Init()
    {
        ChangeIconFilter(4);
    }

    public void OnClick()
    {
        if (!UIMergeController.instance.uIMergeSlots[0].isEmpty) return;

        isShowing = !isShowing;

        dropdownMenu.SetActive(isShowing);
    }

    public void Hide()
    {
        isShowing = false;
        dropdownMenu.SetActive(isShowing);
    }

    public void Filter(int index)
    {
        isShowing = false;

        ChangeIconFilter(index);

        GameController.EquipType equipType = GameController.EquipType.None;

        switch (index)
        {
            case 0: equipType = GameController.EquipType.Weapon; break;
            case 1: equipType = GameController.EquipType.Hat; break;
            case 2: equipType = GameController.EquipType.Armor; break;
            case 3: equipType = GameController.EquipType.Shoes; break;
        }

        ActiveEquipByType(equipType);

        dropdownMenu.SetActive(isShowing);
    }

    void ActiveEquipByType(GameController.EquipType equipType)
    {
        List<UIEquip> equips = UIMergeController.instance.equips;

        for (int i = 0; i < equips.Count; i++)
        {
            equips[i].gameObject.SetActive(equips[i].equipData.equipType == equipType || equipType == GameController.EquipType.None);
        }
    }

    void ChangeIconFilter(int index)
    {
        for (int i = 0; i < iconsFilter.Length; i++)
        {
            iconsFilter[i].SetActive(i == index);
        }
    }
}
