using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> monsterList;
    [SerializeField] private int              spawnCount   = 5;
    [SerializeField] private float            spawnTime    = 5.0f;
    [SerializeField] private float            deliveryTime = 2.0f;
    [SerializeField] private Transform        spawnArea;

    public List<GameObject> MonsterList { get => monsterList; }


    private void Start() => StartCoroutine("Spawn");


    private IEnumerator Spawn()
    {
        while ( true )
        {
            // 일정 수에 도달하면 생성x
            if ( monsterList.FindAll(x => x.activeSelf).Count < spawnCount )
            {
                // monsterList에서 비활성화된 player중 하나 활성화
                GameObject monster = monsterList.Find(x => !x.activeSelf);
                monster.transform.position = spawnArea.position;
                monster.SetActive(true);
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
