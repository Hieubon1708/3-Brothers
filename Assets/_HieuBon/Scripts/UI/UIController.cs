using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Camera uICamera;

    [HideInInspector]
    public UIInGame uIInGame;
    [HideInInspector]
    public UICurrency uICurrency;

    private void Awake()
    {
        instance = this;

        uIInGame = GetComponent<UIInGame>();
        uICurrency = GetComponentInChildren<UICurrency>(true);
    }
}
