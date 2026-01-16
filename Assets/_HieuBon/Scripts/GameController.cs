using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
        None, AssaultRifleGun, BazookaGun, ChemicalGun, ElectricGun, FlameThrowerGun, IceGun, SMGGun
    }

    public enum GameState
    {
        Pause, Playing
    }
}
