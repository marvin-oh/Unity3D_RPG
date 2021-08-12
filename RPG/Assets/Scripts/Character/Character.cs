using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    private Movement movement3D;

    [Header("Info")]
    [SerializeField] private   int   level    = 1;    // 레벨
    [SerializeField] private   int   maxLevel = 99;   // 최대 레벨
    [SerializeField] protected float hp;              // 현재 체력
    [SerializeField] protected float maxHp = 100;     // 최대 체력

    [Header("Attack")]
    protected Weapon         weapon;    // 무기

    [SerializeField] private GameObject attackCollision; // 공격시 충돌감지를 위한 GameObject

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
            hp = Mathf.Clamp(value, 0, MaxHp);
            hpSlider.value = Hp / MaxHp;
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
            weapon = value;

            // 무기 정보에 따라 충돌범위 설정
            attackCollision.transform.localPosition = new Vector3(0, 0, Weapon.AttackRange);
            attackCollision.transform.localScale = new Vector3(attackCollision.transform.localScale.x, Weapon.AttackRange, attackCollision.transform.localScale.z);
        }
        get
        {
            return weapon;
        }
    }

    protected virtual void OnEnable()
    {
        movement3D    = GetComponent<Movement>();
        Canvas canvas = GetComponentInChildren<Canvas>();

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
        Weapon = new Hand();
    }

    public void MoveTo(Vector3 direction)
    {
        if ( Hp == 0 )
        {
            movement3D.MoveTo(Vector3.zero);
            return;
        }

        movement3D.MoveTo(direction);
    }

    public void JumpTo()
    {
        movement3D.JumpTo();
    }

    public virtual void Attack()
    {
        OnAttackCollision();

        Debug.Log(gameObject.name + " Attack");
    }

    private void OnAttackCollision()
    {
        // 충돌감지를 위한 GameObject 활성화
        attackCollision.SetActive(true);
    }

    public virtual void TakeDamage(float damage, Transform attacker)
    {
        Debug.Log(gameObject.name + " TakeDamage (" + damage + ") by " + attacker.name);

        Hp -= damage;

        if ( Hp == 0 ) { Die(); }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "사망");

        gameObject.SetActive(false);
    }
}
