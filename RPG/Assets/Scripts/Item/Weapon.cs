using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ItemSO ItemSO;

    [SerializeField] protected string     weaponName;   // 무기 이름
    [SerializeField] protected int        attackDamage; // 공격력
    [SerializeField] protected float      attackRange;  // 공격 범위
    [SerializeField] protected float      attackSpeed;  // 공격 속도
    [SerializeField] protected float      critical;     // 치명타 확률

    public int ID => ItemSO.Items.Find(x => x.name == weaponName).id;

    public string     WeaponName  => weaponName;
    public int        Atk         => attackDamage;
    public float      AttackRange => attackRange;
    public float      AttackSpeed => attackSpeed;
    public float      Cri         => critical;
}
