using System;
using UnityEngine;
using UnityEngine.UI;

public class UIChestController : MonoBehaviour
{
    public static UIChestController instance;

    public GameObject[] boards;

    public GameObject panelDropRate;

    public DropRateData[] dropRateDatas;

    ScrollRect[] scrollRects;

    UIChestButton[] uIChestButtons;

    public enum ChestType
    {
        Silver, Epic
    }

    private void Awake()
    {
        instance = this;

        UIDropRateBoard[] uIDropRateBoards = GetComponentsInChildren<UIDropRateBoard>(true);
        uIChestButtons = GetComponentsInChildren<UIChestButton>(true);

        int k = 0;

        for (int i = 0; i < dropRateDatas.Length; i++)
        {
            for (int j = 0; j < dropRateDatas[i].rates.Length; j++)
            {
                uIDropRateBoards[k].textRate.text = dropRateDatas[i].rates[j] + "%";
                k++;
            }
        }

        scrollRects = new ScrollRect[boards.Length];

        for (int i = 0; i < scrollRects.Length; i++)
        {
            scrollRects[i] = boards[i].GetComponent<ScrollRect>();
        }
    }

    public void Roll(ChestType type, int amount)
    {
        if (type == ChestType.Epic) EpicRoll(amount);
        else SilverRoll(amount);
    }

    public void SilverRoll(int amount)
    {
        float greatRate = dropRateDatas[0].rates[0];

        for (int i = 0; i < amount; i++)
        {
            float random = UnityEngine.Random.Range(0f, 100f);

            if (random <= greatRate)
            {

            }
        }
    }

    public void EpicRoll(int amount)
    {
        float epicRate = dropRateDatas[0].rates[0];
        float greatRate = dropRateDatas[0].rates[1];

        for (int i = 0; i < amount; i++)
        {
            float random = UnityEngine.Random.Range(0f, 100f);

            if (random <= epicRate)
            {

            }
            else if (random <= greatRate)
            {

            }
            else
            {

            }
        }
    }

    public void ShowDropRate(int index)
    {
        for (int i = 0; i < boards.Length; i++)
        {
            boards[i].SetActive(i == index);
            scrollRects[i].normalizedPosition = new Vector2(0, 1);
        }

        panelDropRate.SetActive(true);
    }

    public void HideDropRate()
    {
        panelDropRate.SetActive(false);
    }

    public bool HaveKey()
    {
        for (int i = 0; i < uIChestButtons.Length; i++)
        {
            if (uIChestButtons[i].ChestKey > 0) return true;
        }
        return false;
    }

    public bool IsFree()
    {
        for (int i = 0; i < uIChestButtons.Length; i++)
        {
            if (uIChestButtons[i].freeButton.activeSelf) return true;
        }
        return false;
    }
}


[System.Serializable]
public class DropRateData
{
    public float[] rates;
}
