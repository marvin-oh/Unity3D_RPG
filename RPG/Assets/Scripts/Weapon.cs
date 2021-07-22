using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { None=0, Sword, }

public class Weapon
{
    public WeaponType WeaponType  { protected set; get; }   // 무기 타입
    public float      Damage      { protected set; get; }   // 공격력
    public float      AttackRange { protected set; get; }   // 공격 범위
    public float      AttackSpeed { protected set; get; }   // 공격 속도

}
