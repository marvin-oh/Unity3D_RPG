using System.Collections;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private void OnEnable() => Invoke("AutoDisable", 0.1f);

    private void OnTriggerEnter(Collider other)
    {
        Character attacker = GetComponentInParent<Character>();
        CharacterType type = attacker.Type;
        if ( ((type == CharacterType.Player) && other.CompareTag("Monster")) ||
             ((type == CharacterType.Monster) && other.CompareTag("Player")) )
        {
            float damage   = attacker.Atk;
            float critical = Random.Range(0f, 1f);
            damage = attacker.Cri < critical ? damage : damage * Random.Range(1f, 2f);
            other.GetComponent<Character>().TakeDamage(damage, attacker.transform);
        }
    }


    private void AutoDisable() => gameObject.SetActive(false);
}
