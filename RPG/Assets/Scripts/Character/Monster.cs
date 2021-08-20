using System.Collections;
using UnityEngine;

public enum MonsterState { SearchTarget=0, Chase, TryAttack, }

public class Monster : Character
{
    [Header("Attack")]
    [SerializeField] private float detectRange = 10.0f;

    [Header("EXP")]
    [SerializeField] private float dropExp = 50.0f;      // 드랍 경험치

    [Header("Drop Item")]
    [SerializeField] private DroppedItem[] droppedItems;

    private GameObject   attackTarget = null;  // 공격 대상
    private MonsterState monsterState;         // Monster FSM


    private void Update()
    {
        if ( attackTarget != null )
        {
            // Player를 바라보도록 설정
            Vector3 position = new Vector3(attackTarget.transform.position.x, transform.position.y, attackTarget.transform.position.z);
            transform.LookAt(position);
        }
    }


    public override void TakeDamage(float damage, Transform attacker)
    {
        base.TakeDamage(damage, attacker);

        if ( Hp == 0 )
        {
            attacker.GetComponent<Player>()?.IncreaseExp(dropExp);

            StopAllCoroutines();
            MoveTo(Vector3.zero);
            attackTarget = null;

            // 아이템 드롭
            ItemPool.Instance.DropItem(droppedItems[Random.Range(0, droppedItems.Length)], transform.position);
        }
    }

    public void ChangeState(MonsterState newState)
    {
        // 이전에 재생중이던 상태 종료
        StopCoroutine(monsterState.ToString());
        // 상태 변경
        monsterState = newState;
        // 새로운 상태 재생
        StartCoroutine(monsterState.ToString());
    }

    private IEnumerator Idle()
    {
        while ( true )
        {
            if ( monsterState == MonsterState.SearchTarget )
            {
                Vector3 randPos = Random.insideUnitSphere;
                randPos.y = transform.position.y;
                Vector3 direction = (randPos - transform.position).normalized;
                MoveTo(direction);
                transform.LookAt(randPos);

                yield return new WaitForSeconds(1.0f);

                MoveTo(Vector3.zero);

                yield return new WaitForSeconds(1.0f);
            }

            yield return null;
        }
    }

    private IEnumerator SearchTarget()
    {
        while ( true )
        {
            // 가장 가까이 있는 공격 대상(Player) 탐색
            attackTarget = FindClosestAttackTarget();

            if ( attackTarget != null )
            {
                // Player와의 거리가 가까우면 공격, 거리가 멀다면 추적
                float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
                if ( distance <= Weapon.AttackRange*2 ){ ChangeState(MonsterState.TryAttack); }
                else { ChangeState(MonsterState.Chase); }
            }
            
            yield return null;
        }
    }

    private IEnumerator Chase()
    {
        while ( attackTarget != null )
        {
            // 거리가 너무 멀어지거나, 충분히 가까이 접근했을 경우 중지
            float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
            if ( (distance > detectRange) || (distance <= Weapon.AttackRange*1.5) ) { break; }

            // Target 방향으로 이동
            Vector3 direction = (attackTarget.transform.position - transform.position).normalized;
            MoveTo(direction);

            yield return null;
        }

        MoveTo(Vector3.zero);
        ChangeState(MonsterState.SearchTarget);
    }

    private IEnumerator TryAttack()
    {
        Attack();

        yield return new WaitForSeconds(10.0f / Weapon.AttackSpeed);

        ChangeState(MonsterState.SearchTarget);
    }

    private GameObject FindClosestAttackTarget()
    {
        // 제일 가까이 있는 Player를 찾기 위해 최초 거리를 최대한 크게 설정
        float closestDistSqr = Mathf.Infinity;

        // 현재 맵에 존재하는 모든 Player 검사
        Player[] players = FindObjectsOfType<Player>();
        for ( int i=0; i< players.Length; ++i )
        {
            float distance = Vector3.Distance(players[i].transform.position, transform.position);
            // 현재까지 검사한 적보다 거리가 가까우면
            if ( (distance <= detectRange) && (distance <= closestDistSqr) )
            {
                closestDistSqr = distance;
                attackTarget   = players[i].gameObject;
            }
        }

        if ( closestDistSqr != Mathf.Infinity ) { return attackTarget; }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.1f);
        Gizmos.DrawSphere(transform.position, detectRange);
    }
}
