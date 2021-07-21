using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private float  currentExp;  // 현재 경험치

    private Weapon weapon;

    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();

        // 기본 무기
        gameObject.AddComponent<Weapon>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        
    }
}
