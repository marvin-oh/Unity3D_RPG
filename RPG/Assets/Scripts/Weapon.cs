using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Hand=0, Sword, }

public class Weapon : MonoBehaviour
{
    [SerializeField] protected string     weaponName;   // 무기 이름
    [SerializeField] protected WeaponType weaponType;   // 무기 타입
    [SerializeField] protected float      damage;       // 공격력
    [SerializeField] protected float      attackRange;  // 공격 범위
    [SerializeField] protected float      attackSpeed;  // 공격 속도

    public string     WeaponName  { get => weaponName; }
    public WeaponType WeaponType  { get => weaponType; }
    public float      Damage      { get => damage; }
    public float      AttackRange { get => attackRange; }
    public float      AttackSpeed { get => attackSpeed; }

}
