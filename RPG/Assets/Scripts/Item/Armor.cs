using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
    [SerializeField] protected int defense;

    public int Def => defense;
}
