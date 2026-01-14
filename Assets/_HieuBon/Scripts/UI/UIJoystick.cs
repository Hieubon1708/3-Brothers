using UnityEngine;

public class UIJoystick : MonoBehaviour
{
    [HideInInspector]
    public Vector3 dir;

    public Camera uICamera;

    Vector3 startPosition;

    bool isDrag;

    Transform circle;

    float radius = 0.625f;

    Vector3 pos;

    float distance;

    public Vector3 Dir
    {
        get
        {
            return new Vector3(dir.x, 0, dir.y);
        }
    }

    public float SpeedPercent
    {
        get
        {
            return distance / radius;
        }
    }

    private void Awake()
    {
        circle = transform.GetChild(0);

        startPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pos = uICamera.ScreenToWorldPoint(Input.mousePosition);

            pos.z = transform.position.z;

            transform.position = pos;

            isDrag = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.position = startPosition;

            circle.position = startPosition;

            isDrag = false;

            dir = Vector3.zero;

            distance = 0f;
        }

        if (isDrag)
        {
            Vector3 currentPos = uICamera.ScreenToWorldPoint(Input.mousePosition);

            currentPos.z = circle.position.z;

            dir = (currentPos - pos).normalized;

            distance = Mathf.Clamp(Vector2.Distance(currentPos, pos), 0, radius);

            circle.position = pos + dir * distance;
        }
    }
}
