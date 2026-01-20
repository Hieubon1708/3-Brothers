using UnityEngine;

[CreateAssetMenu(fileName = "DataEquipUpgrade", menuName = "Scriptable Objects/DataEquipUpgrade")]
public class DataEquipUpgrade : ScriptableObject
{
    public string[] names;
    public DataEquipUpgradeChild[] dataEquipUpgradeChildren;
}

[System.Serializable]
public class DataEquipUpgradeChild
{
    public string[] texts;
}
