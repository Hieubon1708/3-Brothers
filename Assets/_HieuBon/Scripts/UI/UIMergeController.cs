using UnityEngine;

public class UIMergeController : MonoBehaviour
{
    public static UIMergeController instance;

    private void Awake()
    {
        instance = this;
    }
}
