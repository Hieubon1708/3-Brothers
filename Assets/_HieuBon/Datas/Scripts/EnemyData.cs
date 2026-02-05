using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyIndexData[] enemyIndexDatas;
}

[System.Serializable]
public class EnemyIndexData
{
    public GameController.EnemyType type;
    public int hp;
    public int attack;
    public float attackRange;
    public float attackSpeed;
    public float speed;

    public EnemyIndexData(EnemyIndexData enemyIndexData)
    {
        this.type = enemyIndexData.type;
        this.hp = enemyIndexData.hp;
        this.attack = enemyIndexData.attack;
        this.attackRange = enemyIndexData.attackRange;
        this.attackSpeed = enemyIndexData.attackSpeed;
        this.speed = enemyIndexData.speed;
    }
}
