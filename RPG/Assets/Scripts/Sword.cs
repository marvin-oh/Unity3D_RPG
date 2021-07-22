using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Sword()
    {
        WeaponType  = WeaponType.Sword;
        Damage      = 50.0f;
        AttackRange = 3.0f;
        AttackSpeed = 5.0f;
    }
}
