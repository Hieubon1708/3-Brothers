using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class UIResizeScroll : MonoBehaviour
{
    public RectTransform pivot;

    public RectTransform canvas;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        GridLayoutGroup gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();

        int count = 5;

        while (true)
        {
            float spacing = (count - 1) * gridLayoutGroup.spacing.x;
            float size = count * gridLayoutGroup.cellSize.x;

            if (spacing + size + 35 > canvas.sizeDelta.x) break;

            count++;
        }

        count--;

        float spa = (count - 1) * gridLayoutGroup.spacing.x;
        float si = count * gridLayoutGroup.cellSize.x;
        int left = (int)(canvas.sizeDelta.x - (spa + si)) / 2;

        gridLayoutGroup.padding = new RectOffset(
            left,
            gridLayoutGroup.padding.right,
            gridLayoutGroup.padding.top,
            gridLayoutGroup.padding.bottom);

        transform.position = pivot.position;

        Vector3 pos = UIController.instance.uICamera.WorldToScreenPoint(pivot.position);

        GetComponent<RectTransform>().sizeDelta = new Vector2(0, canvas.sizeDelta.y / Screen.height * pos.y);
    }
}
