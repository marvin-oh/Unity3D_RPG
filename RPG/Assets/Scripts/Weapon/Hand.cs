using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Weapon
{
    public Hand()
    {
        WeaponType  = WeaponType.None;
        Damage      = 10.0f;
        AttackRange = 1.0f;
        AttackSpeed = 3.0f;
    }
}
