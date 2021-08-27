using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : Character
{
    [Header("NPC Talk")]
    [SerializeField] private bool talk;
    [TextArea] [SerializeField] private List<string> dialogs;

    [Header("NPC Shop")]
    [SerializeField] private bool shop;
    [SerializeField] private List<string> items;

    [Header("NPC Quest")]
    [SerializeField] private bool quest;

    [Header("Location")]
    [SerializeField] private Transform area;
    [SerializeField] private float     areaRange;

    public bool Shop  => shop;
    public bool Quest => quest;
    public List<string> Dialogs => dialogs;
    public List<string> Items   => items;


    private void Update()
    {
        // 지정된 위치에서 일정반경 이상 벗어나면 복귀
        float distance = Vector3.Distance(transform.position, area.position);
        if ( distance > areaRange ) { StartCoroutine("ReturnArea"); }
    }

    protected override void OnEnable()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();

        // 캐릭터 정보 설정
        nameText.text = characterName;

        // 이동 가능
        canMove = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player") )
        {
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            GameManager.Instance.NpcTalkBtn.gameObject.SetActive(talk);
            GameManager.Instance.NpcTalkPanel.GetComponent<NpcTalkPanel>().Setup(this);
            GameManager.Instance.NpcShopPanel.GetComponent<NpcShopPanel>().Setup(this);

            GameManager.Instance.NpcQuestPanel.GetComponentInChildren<Text>().text = characterName;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.CompareTag("Player") )
        {
            GameManager.Instance.NpcTalkBtn.gameObject.SetActive(false);
            GameManager.Instance.NpcTalkPanel.SetActive(false);
            GameManager.Instance.NpcShopPanel.SetActive(false);
            GameManager.Instance.NpcQuestPanel.SetActive(false);
        }
    }


    /// <summary>
    /// 지정된 위치로 복귀 코루틴
    /// </summary>
    private IEnumerator ReturnArea()
    {
        while ( true )
        {
            // 거리가 너무 멀어지거나, 충분히 가까이 접근했을 경우 중지
            float distance = Vector3.Distance(transform.position, area.position);
            if ( distance <= 3f ) { break; }

            // Target 방향으로 이동
            Vector3 direction = (area.position - transform.position).normalized;
            transform.LookAt(new Vector3(area.position.x, transform.position.y, area.position.z));
            MoveTo(direction);

            yield return null;
        }

        MoveTo(Vector3.zero);
    }
}
