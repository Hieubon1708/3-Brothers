using Newtonsoft.Json;
using System.Collections.Generic;
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

    public int IronAmount
    {
        get
        {
            return PlayerPrefs.GetInt("IronAmount", 1);
        }
        set
        {
            PlayerPrefs.SetInt("IronAmount", value);
        }
    }

    public int ClothAmount
    {
        get
        {
            return PlayerPrefs.GetInt("ClothAmount");
        }
        set
        {
            PlayerPrefs.SetInt("ClothAmount", value);
        }
    }

    public List<EquipData> Equipments
    {
        get
        {
            string txt = PlayerPrefs.GetString("Equipments", string.Empty);

            if (!string.IsNullOrEmpty(txt))
            {
                return JsonConvert.DeserializeObject<List<EquipData>>(txt);
            }

            return new List<EquipData>();
        }
        set
        {
            string txt = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString("Equipments", txt);
        }
    }
}
