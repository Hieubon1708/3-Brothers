using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum WeaponType
    {
        None, Knife, AssaultRifleGun, BazookaGun, ChemicalGun, ElectricGun, FlameThrowerGun, IceGun, SMGGun
    }

    public enum GameState
    {
        Pause, Playing
    }
}
