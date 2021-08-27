using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    [SerializeField] protected int    attackDamage; // 공격력
    [SerializeField] protected float  attackRange;  // 공격 범위
    [SerializeField] protected float  attackSpeed;  // 공격 속도
    [SerializeField] protected float  critical;     // 치명타 확률

    public int   Atk         => attackDamage;
    public float AttackRange => attackRange;
    public float AttackSpeed => attackSpeed;
    public float Cri         => critical;
}
