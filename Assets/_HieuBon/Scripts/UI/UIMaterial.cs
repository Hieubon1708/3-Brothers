using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMaterial : MonoBehaviour
{
    public RectTransform content;

    public GameObject iron;
    public GameObject cloth;

    public TextMeshProUGUI textIronAmount;
    public TextMeshProUGUI textClothAmount;

    public GridLayoutGroup gridContent;

    public RectTransform pivot;
    public RectTransform canvas;

    int originPaddingBottom;
    float clampY;

    Vector3[] corners = new Vector3[4];

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        originPaddingBottom = gridContent.padding.bottom;
        clampY = -5 + 390 * canvas.localScale.x;

        LoadData();
    }

    public void LoadData()
    {
        int ironAmount = GameManager.instance.IronAmount;
        int clothAmount = GameManager.instance.ClothAmount;

        iron.SetActive(ironAmount > 0);
        cloth.SetActive(clothAmount > 0);

        textIronAmount.text = ironAmount.ToString();
        textClothAmount.text = clothAmount.ToString();

        bool isActive = ironAmount > 0 || clothAmount > 0;

        gameObject.SetActive(isActive);

        gridContent.padding = new RectOffset(
            gridContent.padding.left,
            gridContent.padding.right,
            gridContent.padding.top,
            isActive ? originPaddingBottom + 320 : originPaddingBottom);
    }

    private void LateUpdate()
    {
        content.GetWorldCorners(corners);

        Vector3 pos = corners[3];

        pos.x = 0;
        pos.y += 390 * canvas.localScale.x;
        pos.y = Mathf.Clamp(pos.y, pos.y, clampY);

        pivot.position = pos;
    }
}
