using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CharacterType { NPC, Player, Monster, }

public class Character : MonoBehaviour
{
    protected Movement movement;
    protected Animator animator;
    protected Canvas   canvas;

    [Header("Info")]
    [SerializeField] private   CharacterType type;      // 타입
    [SerializeField] protected string characterName;    // 캐릭터명
    [SerializeField] protected Text   nameText;         // 캐릭터 이름 Text

    protected bool canMove;   // 이동 가능 여부

    public CharacterType Type { get => type; }
    public string Name => characterName;

 
    private void Update()
    {
        if ( transform.position.y < 0 ) { Die(); }
    }

    protected virtual void OnEnable()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        canvas   = GetComponentInChildren<Canvas>();

        // 캐릭터 정보 설정
        nameText.text = characterName;

        // 이동 가능
        canMove = true;
    }


    /// <summary>
    /// 진행 방향 설정
    /// </summary>
    public void MoveTo(Vector3 direction)
    {
        if ( !canMove )
        {
            movement.MoveTo(Vector3.zero);
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().ResetTrigger("Jump");
            return;
        }

        movement.MoveTo(direction);
        if ( direction != Vector3.zero )
        {
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<Animator>().ResetTrigger("Jump");
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
    }

    /// <summary>
    /// 캐릭터 점프
    /// </summary>
    public void JumpTo()
    {
        if ( movement.JumpTo() ) { GetComponent<Animator>().SetTrigger("Jump"); }
    }

    /// <summary>
    /// 캐릭터 사망 (Animation에서 호출)
    /// </summary>
    protected virtual void Die() => gameObject.SetActive(false);

    /// <summary>
    /// 공격/사망시 이동 불가 (Animation에서 호출)
    /// </summary>
    public void EnableMove()  => canMove = true;
    public void DisableMove() => canMove = false;
}
