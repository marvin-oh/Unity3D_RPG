using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { private set; get; }
    private void Awake() => Instance = this;

    [SerializeField] private List<Weapon> weapons;

    public Weapon GetWeapon(string _name) => weapons.Find(x => x.name == _name);
}
