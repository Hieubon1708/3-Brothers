using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public bool flip;

    public float time = 0.5f;

    private void Start()
    {
        leftDoor.localRotation = Quaternion.Euler(0, -90, 90);
        rightDoor.localRotation = Quaternion.Euler(0, -90, -90);
    }

    public void Open()
    {
        leftDoor.DOLocalRotateQuaternion(Quaternion.Euler(flip ? 90 : -90, -90, 90), time);
        rightDoor.DOLocalRotateQuaternion(Quaternion.Euler(flip ? -90 : 90, -90, -90), time);
    }

    public void Close()
    {
        leftDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, -90, 90), time);
        rightDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, -90, -90), time);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Open();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Close();
        }
    }
}
