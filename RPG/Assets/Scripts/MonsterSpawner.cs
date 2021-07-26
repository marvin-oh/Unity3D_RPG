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
    private float       deliveryTime = 2.0f;
    
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
        // 랜덤 위치 지정
        Vector3 randPos = Random.insideUnitSphere.normalized;
        randPos.y = target.transform.position.y;
        Vector3 goalPos = target.transform.position + randPos;
        goalPos.y = target.transform.position.y;

        // 해당 위치 방향으로 deliveryTime동안 이동
        Vector3 direction = (goalPos - target.transform.position).normalized;
        target.MoveTo(direction);
        target.transform.LookAt(goalPos);

        yield return new WaitForSeconds(deliveryTime);

        target.MoveTo(Vector3.zero);

        // MonsterState 변경
        target.GetComponent<Monster>().ChangeState(MonsterState.SearchTarget);
        target.GetComponent<Monster>().StartCoroutine("Idle");
    }
}
