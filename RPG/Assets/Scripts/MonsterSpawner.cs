using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject  MonsterPrefab;
    [SerializeField]
    private int         spawnCount = 5;
    [SerializeField]
    private float       spawnTime = 5.0f;
    [SerializeField]
    private float       deliveryTime = 1.0f;
    
    private void Awake()
    {
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while ( true )
        {
            // 일정 수에 도달하면 생성x
            if ( FindObjectsOfType<Monster>().Length < spawnCount )
            {
                GameObject monster = Instantiate(MonsterPrefab, transform);
                
                StartCoroutine(Delivery(monster.GetComponent<Monster>()));
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator Delivery(Character target)
    {
        Vector3 randPos = Random.insideUnitSphere.normalized;
        randPos.y = 0;
        Vector3 goalPos = transform.position + randPos;
        print("goalPos:" + goalPos);

        Vector3 direction = (goalPos - transform.position).normalized;
        target.MoveTo(direction);

        yield return new WaitForSeconds(deliveryTime);

        target.MoveTo(Vector3.zero);
    }
}
