using System.Collections;
using System.Collections.Generic;
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

    public Enemy GetEnemy(GameObject e)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject == e) return enemies[i];
        }
        return null;
    }
}
