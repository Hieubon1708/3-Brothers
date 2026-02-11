using TMPro;
using UnityEngine;

public class UIMission : MonoBehaviour
{
    public UITasks.MissionType missionType;

    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textAmount;
    public TextMeshProUGUI textTrophyAmount;

    public GameObject defaultButton;
    public GameObject claimButton;
    public GameObject v;

    public RectTransform fill;

    public GameObject fade;

    float originWidth;

    private void Awake()
    {
        originWidth = fill.sizeDelta.x;
    }

    public void LoadData(MissionData missionData, string type)
    {
        int level = missionData.GetLevel(type);
        int amount = missionData.GetAmount(type);

        UITasks.MissionState missionState = missionData.GetState(type, level);

        textTitle.text = missionData.title;

        int totalAmount = missionData.amountOfLevel[level - 1];

        textTrophyAmount.text = missionData.amountTrophyOfLevel[level - 1].ToString();
        textAmount.text = amount + "/" + totalAmount;

        float percent = (float)amount / totalAmount;
        fill.sizeDelta = new Vector2(originWidth * percent, fill.sizeDelta.y);

        defaultButton.SetActive(missionState == UITasks.MissionState.Incomple);
        claimButton.SetActive(missionState == UITasks.MissionState.Complete);
        v.SetActive(missionState == UITasks.MissionState.Received);
        fade.SetActive(missionState == UITasks.MissionState.Received);
    }
}
