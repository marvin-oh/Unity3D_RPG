using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableCharacter : Character
{
    [Header("Stat")]
    [SerializeField] private   int   level    = 1;    // 레벨
    [SerializeField] private   int   maxLevel = 99;   // 최대 레벨
    [SerializeField] protected float hp;              // 현재 HP
    [SerializeField] protected float maxHp    = 100;  // 최대 HP
    [SerializeField] protected float mp;              // 현재 MP
    [SerializeField] protected float maxMp    = 50;   // 최대 MP
    [SerializeField] protected int   atk;             // 공격력
    [SerializeField] protected int   def;             // 방어력
    [SerializeField] protected int   spd;             // 이동속도
    [SerializeField] protected float cri;             // 치명타 확률
    protected Weapon weapon;    // 현재 착용중인 Weapon
    protected Shoes  shoes;     // 현재 착용중인 Shoes
    protected Shoes  shoes0;
    protected Halmet halmet;    // 현재 착용중인 Helmet
    protected Armor  armor;     // 현재 착용중인 Armor

    [Header("Equip")]
    [SerializeField] private Transform  hand;       // Weapon 장착 위치
    [SerializeField] private Transform  leftFoot;   // Shoes  장착 위치 (왼발)
    [SerializeField] private Transform  rightFoot;  // Shoes  장착 위치 (오른발)
    [SerializeField] private Transform  body;       // Armor  장착 위치
    [SerializeField] private Transform  head;       // Halmet 장착 위치

    [Header("Attack")]
    [SerializeField] private GameObject attackCollision; // 공격시 충돌감지를 위한 GameObject
    
    public int   Level
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
    public virtual float Hp
    {
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            hpSlider.value = hp / maxHp;

            if ( hp == 0 )
            {
                canMove = false;

                animator.Play("CharacterDeath");
            }
        }
        get
        {
            return hp;       
        }
    }
    public float MaxHp { protected set => maxHp = value; get => maxHp; }
    public virtual float Mp
    {
        set
        {
            mp = Mathf.Clamp(value, 0, maxMp);
            mpSlider.value = mp / maxMp;
        }
        get
        {
            return mp;
        }
    }
    public float MaxMp { protected set => maxMp = value; get => maxMp; }

    public int   Atk { get => atk + weapon.Atk; }
    public int   Def { get => def + halmet.Def + armor.Def; }
    public int   Spd { get => spd + shoes.Spd; }
    public float Cri { get => cri + weapon.Cri; }

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
    public Shoes  Shoes
    {
        set
        {
            if ( shoes )  { Destroy(shoes.gameObject); }
            if ( shoes0 ) { Destroy(shoes0.gameObject); }
            shoes = value;

            // 이동속도
            movement.AddSpeed = Spd;
        }
        get
        {
            return shoes;
        }
    }
    public Halmet Halmet
    {
        set
        {
            if ( halmet ) { Destroy(halmet.gameObject); }
            halmet = value;
        }
        get
        {
            return halmet;
        }
    }
    public Armor  Armor
    {
        set
        {
            if ( armor ) { Destroy(armor.gameObject); }
            armor = value;
        }
        get
        {
            return armor;
        }
    }

    [Header("UI")]
    [SerializeField] private   GameObject hpSliderPrefab;   // 체력 Slider UI 프리팹
    [SerializeField] private   GameObject mpSliderPrefab;   // 체력 Slider UI 프리팹
    [SerializeField] private   GameObject levelTextPrefab;  // 레벨 Text UI 프리팹
    [SerializeField] private   Text       DmgText;          // 데미지 이펙트 Text
    private Slider hpSlider;  // HP Slider UI
    private Slider mpSlider;  // MP Slider UI
    private Text   levelText; // 레벨 Text UI


    protected override void OnEnable()
    {
        base.OnEnable();

        // UI 세팅
        if ( hpSlider == null )
        {
            // HP Slider UI
            hpSlider = Instantiate(hpSliderPrefab, canvas.transform).GetComponent<Slider>();
        }
        if ( mpSlider == null )
        {
            // MP Slider UI
            mpSlider = Instantiate(mpSliderPrefab, canvas.transform).GetComponent<Slider>();
        }
        if ( levelText == null )
        {
            // 레벨 Text UI
            levelText = Instantiate(levelTextPrefab, canvas.transform).GetComponent<Text>();
        }

        // 캐릭터 정보 설정
        Level = level;
        Hp    = MaxHp;
        Mp    = MaxMp;

        // 장비 설정
        ChangeWeapon("Hand");
        ChangeShoes("Foot");
        ChangeHalmet("Head");
        ChangeArmor("Body");
    }


    /// <summary>
    /// 캐릭터 사망 (Animation에서 호출)
    /// </summary>
    protected override void Die()
    {
        base.Die();

        hp = maxHp;
    }


    public virtual void Attack()
    {
        if (!canMove) { return; }

        // Attack 애니메이션
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 데미지 피격시 호출
    /// </summary>
    public virtual void TakeDamage(float damage, Transform attacker)
    {
        // 사망 상태시 피격 무시
        if ( hp == 0 ) { return; }

        damage = damage * 100 / (100 + Def);

        // HitEffect
        EffectPool.Instance.HitEffect(transform.position);
        DmgText.text = string.Format("{0:##,##0}", damage);
        DmgText.gameObject.SetActive(true);

        animator.Play("CharacterHit");

        Hp -= damage;
    }

    /// <summary>
    /// 공격시 충돌영역 활성화 메소드 (Aniamtion에서 호출)
    /// </summary>
    private void OnAttackCollision()
    {
        // 충돌감지를 위한 GameObject 활성화
        attackCollision.SetActive(true);
    }

    /// <summary>
    /// Weapon 교체 메소드
    /// </summary>
    public virtual void ChangeWeapon(string _name) => Weapon = Instantiate(ItemManager.Instance.GetWeapon(_name), hand);

    /// <summary>
    /// Shoes 교체 메소드
    /// </summary>
    public virtual void ChangeShoes(string _name)
    {
        Shoes  = Instantiate(ItemManager.Instance.GetShoes(_name), leftFoot);
        shoes0 = Instantiate(ItemManager.Instance.GetShoes(_name), rightFoot);
    }

    /// <summary>
    /// Halmet 교체 메소드
    /// </summary>
    public virtual void ChangeHalmet(string _name) => Halmet = Instantiate(ItemManager.Instance.GetHalmet(_name), head);

    /// <summary>
    /// Armor 교체 메소드
    /// </summary>
    public virtual void ChangeArmor(string _name) => Armor = Instantiate(ItemManager.Instance.GetArmor(_name), body);
}
