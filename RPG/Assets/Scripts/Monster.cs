using UnityEngine;

public class Monster : Character
{
    [Header("EXP")]
    [SerializeField]
    private float dropExp = 50.0f;  // 드랍 경험치

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void TakeDamage(float damage, Transform attacker)
    {
        base.TakeDamage(damage, attacker);

        if ( Hp == 0 )
        {
            attacker.GetComponent<Player>().IncreaseExp(dropExp);
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
    }
}
