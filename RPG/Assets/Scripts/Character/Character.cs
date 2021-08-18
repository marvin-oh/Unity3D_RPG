using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    private Movement movement;
    private Animator animator;
    private Canvas   canvas;

    [Header("Info")]
    [SerializeField] private   int   level    = 1;    // 레벨
    [SerializeField] private   int   maxLevel = 99;   // 최대 레벨
    [SerializeField] protected float hp;              // 현재 체력
    [SerializeField] protected float maxHp = 100;     // 최대 체력

    [Header("Attack")]
    [SerializeField] private Transform  hand;            // 무기 장착 위치
    [SerializeField] private GameObject attackCollision; // 공격시 충돌감지를 위한 GameObject
    protected Weapon weapon;    // 현재 착용중인 무기
    private   bool   canMove;   // 이동 가능 여부

    [Header("UI")]
    [SerializeField] private GameObject hpSliderPrefab;   // 체력 Slider UI 프리팹
    [SerializeField] private GameObject levelTextPrefab;  // 레벨 Text UI 프리팹
    private Slider           hpSlider;  // 체력 Slider UI
    private TextMeshProUGUI  levelText; // 레벨 Text UI

    protected int Level
    {
        set
        {
            level = Mathf.Clamp(value, 0, maxLevel);
            levelText.text = "Lv." + level.ToString();
        }
        get
        {
            return level;
        }
    }

    public float Hp
    {
        set
        {
            if ( value < hp )
            {
                GameObject hitEffect = EffectPool.Instance.HitEffect();
                hitEffect.transform.position = transform.position;
                hitEffect.SetActive(true);
            }

            hp = Mathf.Clamp(value, 0, MaxHp);
            hpSlider.value = Hp / MaxHp;

            if  ( Hp == 0 )
            {
                canMove = false;

                animator.Play("CharacterDeath");
            }
            else
            {
                animator.Play("CharacterHit");
            }
        }
        get
        {
            return hp;       
        }
    }

    public float MaxHp { protected set => maxHp = value; get => maxHp; }

    public Weapon Weapon
    {
        set
        {
            if ( weapon ) { Destroy(weapon.gameObject); }
            weapon = value;

            // 무기 정보에 따라 충돌범위 설정
            attackCollision.transform.localPosition = new Vector3(0, 0, Weapon.AttackRange);
            attackCollision.transform.localScale    = new Vector3(attackCollision.transform.localScale.x, Weapon.AttackRange, attackCollision.transform.localScale.z);
        }
        get
        {
            return weapon;
        }
    }

    protected virtual void OnEnable()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        canvas   = GetComponentInChildren<Canvas>();

        // UI 세팅 - 체력 Slider UI
        GameObject hpSliderClone = Instantiate(hpSliderPrefab, canvas.transform);
        hpSlider = hpSliderClone.GetComponent<Slider>();

        // UI 세팅 - 레벨 Text UI
        GameObject levelTextClone = Instantiate(levelTextPrefab, canvas.transform);
        levelText = levelTextClone.GetComponent<TextMeshProUGUI>();

        // 캐릭터 정보 설정
        Hp    = MaxHp;
        Level = level;

        // 무기 설정
        ChangeWeapon("Hand");

        // 이동 가능
        canMove = true;
    }

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

    public void JumpTo()
    {
        if ( movement.JumpTo() ) { GetComponent<Animator>().SetTrigger("Jump"); }
    }

    public virtual void Attack()
    {
        if ( !canMove ) { return; }

        // Attack 애니메이션
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 공격시 충돌영역 활성화 메소드 (Aniamtion에서 호출)
    /// </summary>
    private void OnAttackCollision()
    {
        // 충돌감지를 위한 GameObject 활성화
        attackCollision.SetActive(true);
    }

    public virtual void TakeDamage(float damage, Transform attacker) => Hp -= damage;

    /// <summary>
    /// 캐릭터 사망 (Animation에서 호출)
    /// </summary>
    protected virtual void Die() => gameObject.SetActive(false);

    /// <summary>
    /// 무기 교체 메소드
    /// </summary>
    public void ChangeWeapon(string _name)
    {
        Weapon = Instantiate(WeaponManager.Instance.GetWeapon(_name), hand);
    }

    /// <summary>
    /// 공격/사망시 이동 불가 (Animation에서 호출)
    /// </summary>
    public void EnableMove()  => canMove = true;

    public void DisableMove() => canMove = false;
}
