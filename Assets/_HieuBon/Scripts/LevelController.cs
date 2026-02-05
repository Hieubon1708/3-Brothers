using UnityEngine;
using static GameController;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    Enemy[] enemies;

    public GameState gameState;

    private void Awake()
    {
        instance = this;

        enemies = GetComponentsInChildren<Enemy>();
    }

    public Enemy GetEnemy(Transform e)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].transform == e) return enemies[i];
        }
        return null;
    }
}
