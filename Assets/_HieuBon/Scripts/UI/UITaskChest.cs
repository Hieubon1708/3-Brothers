using TMPro;
using UnityEngine;

public class UITaskChest : MonoBehaviour
{
    public GameObject defaultChest;
    public GameObject claimChest;
    public GameObject receivedChest;

    public TextMeshProUGUI textAmount;

    public void LoadData(UITasks.MissionState missionState, int amount)
    {
        textAmount.text = amount.ToString();

        defaultChest.SetActive(missionState == UITasks.MissionState.Incomple);
        claimChest.SetActive(missionState == UITasks.MissionState.Complete);
        receivedChest.SetActive(missionState == UITasks.MissionState.Received);
    }
}
