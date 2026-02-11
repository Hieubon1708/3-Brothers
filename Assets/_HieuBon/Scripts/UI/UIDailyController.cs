using System;
using TMPro;
using UnityEngine;
using static UITasks;
using Newtonsoft.Json;
using UnityEngine.UI;

public class UIDailyController : MonoBehaviour
{
    public static UIDailyController instance;

    public TextMeshProUGUI textTrophy;

    UIMission[] uIMissions;
    UITaskChest[] uITaskChests;

    public MissionType[] missionTypes;

    public GameObject preUIMission;

    public Transform container;

    public Image fill;

    public TextMeshProUGUI textTime;

    public DateTime DailyRemainingTime
    {
        get
        {
            return DateTime.Parse(PlayerPrefs.GetString("DailyRemainingTime", DateTime.Today.ToString()));
        }
        set
        {
            PlayerPrefs.SetString("DailyRemainingTime", value.ToString());
        }
    }

    public int TrophyDaily
    {
        get
        {
            return PlayerPrefs.GetInt("TrophyDaily");
        }
        set
        {
            PlayerPrefs.SetInt("TrophyDaily", value);
        }
    }

    public ChestData ChestDaily
    {
        get
        {
            string txt = PlayerPrefs.GetString("ChestDaily", string.Empty);
            if (!string.IsNullOrEmpty(txt))
            {
                return JsonConvert.DeserializeObject<ChestData>(txt);
            }
            return new ChestData();
        }
        set
        {
            string txt = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString("ChestDaily", txt);
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
                uIMissions[i].LoadData(missionData, "Daily");
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
        TimeSpan timeRemaining = DailyRemainingTime - DateTime.Now;

        if (timeRemaining.TotalSeconds <= 0) DailyRemainingTime = DailyRemainingTime.AddDays(1);

        string timeString = string.Format("Time Remaining: {0:D2}H {1:D2}M",
                timeRemaining.Hours,
                timeRemaining.Minutes);

        textTime.text = timeString;
    }

    public void ClearMission()
    {
        PlayerPrefs.DeleteKey("ChestDaily");

        for (int i = 0; i < missionTypes.Length; i++)
        {
            PlayerPrefs.DeleteKey("DailyMission" + missionTypes[i]);
        }
    }

    public void LoadData()
    {
        textTrophy.text = TrophyDaily.ToString();

        fill.fillAmount = (float)TrophyDaily / 100;

        int milestone = 0;
        bool[] isReceiveds = ChestDaily.isReceiveds;

        for (int i = 0; i < uITaskChests.Length; i++)
        {
            milestone += 20;

            MissionState missionState = TrophyDaily >= milestone ? MissionState.Complete : MissionState.Incomple;

            if (isReceiveds[i]) missionState = MissionState.Received;

            uITaskChests[i].LoadData(missionState, milestone);
        }
    }

    public void CheckDailyMission(MissionType missionType, int amount)
    {

    }
}
