using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : Equipment
{
    [SerializeField] protected int speed;

    public int Spd => speed;
}
