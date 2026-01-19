using TMPro;
using UnityEngine;

public class TestText : MonoBehaviour
{
    public TextMeshProUGUI a;

    private void Update()
    {
        Debug.Log(a.preferredWidth);
        RectTransform r = a.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(r.sizeDelta.x, a.preferredHeight);
    }
}
