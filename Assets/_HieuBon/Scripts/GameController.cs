using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public EnemyData enemyData;

    public float enemyVision = 3f;

    public enum EnemyType
    {
        E1, E2, E3, E4, E5
    }

    public enum EquipQuality
    {
        Q1, Q2, Q3, Q4, Q5, Q6
    }

    public enum EquipType
    {
        None, Weapon, Hat, Armor, Shoes
    }

    public enum EquipMaterial
    {
        M1, M2, M3, M4, M5, M6, M7, M8
    }

    public enum WeaponType
    {
        Knife, AssaultRifleGun, BazookaGun, ChemicalGun, ElectricGun, FlameThrowerGun, IceGun, SMGGun
    }

    public enum GameState
    {
        Playing, Pause
    }

    private void Awake()
    {
        instance = this;
    }

    public EnemyIndexData GetEnemyIndexData(EnemyType type)
    {
        for (int i = 0; i < enemyData.enemyIndexDatas.Length; i++)
        {
            if (enemyData.enemyIndexDatas[i].type == type) return enemyData.enemyIndexDatas[i];
        }
        return null;
    }
}
