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

    public int Gold
    {
        get
        {
            return PlayerPrefs.GetInt("Gold");
        }
        set
        {
            PlayerPrefs.SetInt("Gold", value);

            //UIController.instance.uICurrency.UpdateGold();
        }
    }

    public int Diamond
    {
        get
        {
            return PlayerPrefs.GetInt("Diamond");
        }
        set
        {
            PlayerPrefs.SetInt("Diamond", value);

            //UIController.instance.uICurrency.UpdateDiamond();
        }
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

            UIEquipeTopController.instance.uIEquipeds[0].UpdateLevel(value);
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

            UIEquipeTopController.instance.uIEquipeds[1].UpdateLevel(value);
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

            UIEquipeTopController.instance.uIEquipeds[2].UpdateLevel(value);
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

            UIEquipeTopController.instance.uIEquipeds[3].UpdateLevel(value);
        }
    }

    public int IronAmount
    {
        get
        {
            return PlayerPrefs.GetInt("IronAmount");
        }
        set
        {
            PlayerPrefs.SetInt("IronAmount", value);

            UIInventory.instance.uIMaterial.LoadData();
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

            UIInventory.instance.uIMaterial.LoadData();
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
