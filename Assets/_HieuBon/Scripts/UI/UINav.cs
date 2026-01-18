using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UINav : MonoBehaviour
{
    public RectTransform[] btns;
    public GameObject[] ligths;

    public RectTransform canvas;

    float max = 460;


    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        OnClick(1);
    }

    public void OnClick(int index)
    {
        float x = canvas.sizeDelta.x;

        float min = (x - max) / 2;

        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].sizeDelta = new Vector2(i == index ? max : min, btns[i].sizeDelta.y);
            ligths[i].SetActive(i == index);
        }
    }
}
