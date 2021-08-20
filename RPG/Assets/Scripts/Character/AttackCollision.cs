using System.Collections;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        Character attacker = GetComponentInParent<Character>();
        CharacterType type = attacker.Type;
        if ( ((type == CharacterType.Player) && other.CompareTag("Monster")) ||
             ((type == CharacterType.Monster) && other.CompareTag("Player")) )
        {
            float damage = attacker.Weapon.Damage;
            other.GetComponent<Character>().TakeDamage(damage, attacker.transform);
        }
    }

    private IEnumerator AutoDisable()
    {
        // 0.1초 후에 오브젝트 비활성화
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
