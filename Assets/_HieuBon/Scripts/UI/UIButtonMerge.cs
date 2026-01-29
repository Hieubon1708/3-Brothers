using TMPro;
using UnityEngine;

public class UIButtonMerge : MonoBehaviour
{
    public GameObject disabled;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Check(int count, int selectCount)
    {
        text.text = selectCount > 0 ? "Confirm" : "Quick Merge";

        disabled.SetActive(!(count > 0 && selectCount == 0 || selectCount == 3));
    }
}
