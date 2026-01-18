using UnityEngine;

public class UIResizeScroll : MonoBehaviour
{
    public Transform pivot;

    public Camera uICamera;

    private void Awake()
    {
        transform.position = pivot.position;

        Vector3 pos = uICamera.ScreenToWorldPoint(Vector3.zero);
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, pivot.position.y - pos.y - 200);
    }
}
