using UnityEngine;

public class Monster : Character
{
    [SerializeField]
    private float dropExp = 50.0f;  // 드랍 경험치

    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();
    }
}
