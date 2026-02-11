using System;
using UnityEngine;
using static UITasks;

public class UITasks : MonoBehaviour
{
    public static UITasks instance;

    public MissionData[] missionDatas;

    public enum MissionType
    {
        Login, KillEnemy, KillBoss, OpenChest, WatchAds
    }

    public enum MissionState
    {
        Incomple, Complete, Received
    }

    private void Awake()
    {
        instance = this;
    }

    public void SaveMission(string key, int amount)
    {
        PlayerPrefs.SetInt(key, GetMission(key) + amount);
    }

    public int GetMission(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public MissionData GetMissionData(MissionType missionType)
    {
        for (int i = 0; i < missionDatas.Length; i++)
        {
            if (missionDatas[i].missionType == missionType) return missionDatas[i];
        }
        return null;
    }
}

[System.Serializable]
public class MissionData
{
    public UITasks.MissionType missionType;
    public string title;
    public int[] amountOfLevel;
    public int[] amountTrophyOfLevel;

    public int GetLevel(string type)
    {
        int amount = UITasks.instance.GetMission(type + missionType);

        if (amount == -1) return amountOfLevel.Length;

        for (int i = 0; i < amountOfLevel.Length; i++)
        {
            if (amount <= amountOfLevel[i]) return i + 1;
        }
        return 1;
    }

    public int GetAmount(string type)
    {
        return UITasks.instance.GetMission(type + missionType);
    }

    public MissionState GetState(string type, int level)
    {
        int amount = UITasks.instance.GetMission(type + missionType);

        if (amount == -1) return MissionState.Received;
        if (amount == amountOfLevel[level - 1]) return MissionState.Complete;
        else return MissionState.Incomple;
    }
}

public class ChestData
{
    public bool[] isReceiveds = new bool[5];
}
