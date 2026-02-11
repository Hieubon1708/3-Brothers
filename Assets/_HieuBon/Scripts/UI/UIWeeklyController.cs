using Newtonsoft.Json;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UITasks;

public class UIWeeklyController : MonoBehaviour
{
    public static UIWeeklyController instance;

    public TextMeshProUGUI textTrophy;

    UIMission[] uIMissions;
    UITaskChest[] uITaskChests;

    public MissionType[] missionTypes;

    public GameObject preUIMission;

    public Transform container;

    public Image fill;

    public TextMeshProUGUI textTime;

    public DateTime WeeklyRemainingTime
    {
        get
        {
            return DateTime.Parse(PlayerPrefs.GetString("WeeklyRemainingTime", DateTime.Today.ToString()));
        }
        set
        {
            PlayerPrefs.SetString("WeeklyRemainingTime", value.ToString());
        }
    }

    public int TrophyWeekly
    {
        get
        {
            return PlayerPrefs.GetInt("TrophyWeekly");
        }
        set
        {
            PlayerPrefs.SetInt("TrophyWeekly", value);
        }
    }

    public ChestData ChestWeekly
    {
        get
        {
            string txt = PlayerPrefs.GetString("ChestWeekly", string.Empty);
            if (!string.IsNullOrEmpty(txt))
            {
                return JsonConvert.DeserializeObject<ChestData>(txt);
            }
            return new ChestData();
        }
        set
        {
            string txt = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString("ChestWeekly", txt);
        }
    }

    private void Awake()
    {
        uITaskChests = GetComponentsInChildren<UITaskChest>();
        uIMissions = new UIMission[missionTypes.Length];

        for (int i = 0; i < missionTypes.Length; i++)
        {
            MissionData missionData = UITasks.instance.GetMissionData(missionTypes[i]);

            if (missionData != null)
            {
                uIMissions[i] = Instantiate(preUIMission, container).GetComponent<UIMission>();
                uIMissions[i].LoadData(missionData, "Weekly");
            }
            else
            {
                Debug.LogError("!");
            }
        }

        LoadData();
    }

    private void Update()
    {
        TimeSpan timeRemaining = WeeklyRemainingTime - DateTime.Now;

        if (timeRemaining.TotalSeconds <= 0) WeeklyRemainingTime = WeeklyRemainingTime.AddDays(7);

        string timeString = string.Format("Time Remaining: {0:D2}H {1:D2}M",
                timeRemaining.Hours,
                timeRemaining.Minutes);

        textTime.text = timeString;
    }

    public void ClearMission()
    {
        PlayerPrefs.DeleteKey("ChestWeekly");

        for (int i = 0; i < missionTypes.Length; i++)
        {
            PlayerPrefs.DeleteKey("WeeklyMission" + missionTypes[i]);
        }
    }

    public void LoadData()
    {
        textTrophy.text = TrophyWeekly.ToString();

        fill.fillAmount = (float)TrophyWeekly / 100;

        int milestone = 0;
        bool[] isReceiveds = ChestWeekly.isReceiveds;

        for (int i = 0; i < uITaskChests.Length; i++)
        {
            milestone += 20;

            MissionState missionState = TrophyWeekly >= milestone ? MissionState.Complete : MissionState.Incomple;

            if (isReceiveds[i]) missionState = MissionState.Received;

            uITaskChests[i].LoadData(missionState, milestone);
        }
    }

    public void CheckWeeklyMission(MissionType missionType, int amount)
    {

    }
}
