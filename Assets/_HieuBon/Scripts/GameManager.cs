using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public int WeaponLevel
    {
        get
        {
            return PlayerPrefs.GetInt("WeaponLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("WeaponLevel", value);
        }
    }

    public int HatLevel
    {
        get
        {
            return PlayerPrefs.GetInt("HatLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("HatLevel", value);
        }
    }

    public int ArmorLevel
    {
        get
        {
            return PlayerPrefs.GetInt("ArmorLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("ArmorLevel", value);
        }
    }

    public int ShoesLevel
    {
        get
        {
            return PlayerPrefs.GetInt("ShoesLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("ShoesLevel", value);
        }
    }
}
