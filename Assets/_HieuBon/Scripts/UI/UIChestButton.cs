using System;
using TMPro;
using UnityEngine;

public class UIChestButton : MonoBehaviour
{
    public UIChestController.ChestType type;

    public string keyKey;
    public string timeKey;

    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textPrice;

    public GameObject freeButton;

    public int price;

    DateTime nextTime;

    public DateTime FreeTime
    {
        get
        {
            return DateTime.Parse(PlayerPrefs.GetString(timeKey,
                type == UIChestController.ChestType.Silver ? DateTime.Now.ToString() : DateTime.Today.ToString()));
        }
        set
        {
            PlayerPrefs.SetString(timeKey, value.ToString());
        }
    }

    public int ChestKey
    {
        get
        {
            return PlayerPrefs.GetInt(keyKey);
        }
        set
        {
            PlayerPrefs.GetInt(keyKey, value);
        }
    }

    private void Awake()
    {
        keyKey = type + "key";
        timeKey = type + "time";

        nextTime = FreeTime;
    }

    private void Update()
    {
        TimeSpan timeRemaining = nextTime - DateTime.Now;

        freeButton.SetActive(timeRemaining.TotalSeconds <= 0);
        textTime.gameObject.SetActive(timeRemaining.TotalSeconds > 0);

        if (timeRemaining.TotalSeconds > 0)
        {
            string timeString = string.Format("FREE {0:D2}H {1:D2}M",
                timeRemaining.Hours,
                timeRemaining.Minutes);

            textTime.text = timeString;
        }
    }

    public void Roll()
    {
        if (freeButton.activeSelf)
        {
            FreeTime = type == UIChestController.ChestType.Silver ? DateTime.Now.AddHours(3) : DateTime.Today.AddDays(1);
            
            nextTime = FreeTime;

            UIChestController.instance.Roll(type, 1);
        }
        else if (ChestKey > 0)
        {
            UIChestController.instance.Roll(type, ChestKey);

            ChestKey = 0;

            LoadButton();
        }
        else
        {
            int diamond = GameManager.instance.Diamond;

            if (diamond < price)
            {

                return;
            }

            GameManager.instance.Diamond -= price;

            UIChestController.instance.Roll(type, ChestKey);
        }
    }

    void LoadButton()
    {
        if (ChestKey > 0) textPrice.text = "<sprite=2>" + ChestKey;
        else textPrice.text = "<sprite=0>" + price;
    }
}
