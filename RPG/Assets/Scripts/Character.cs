using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    private Movement3D movement3D;

    [Header("Info")]
    [SerializeField]
    private int          level = 1;       // 레벨
    [SerializeField]
    private int          maxLevel = 99;   // 최대 레벨
    [SerializeField]
    protected float      hp = 100;        // 체력

    [Header("Attack")]
    protected Weapon     weapon;          // 무기
    [SerializeField]
    private   GameObject attackCollision; // 공격시 충돌감지를 위한 GameObject

    [Header("UI")]
    [SerializeField]
    private GameObject       hpSliderPrefab;   // 체력 Slider UI 프리팹
    private Slider           hpSlider;         // 체력 Slider UI
    [SerializeField]
    private GameObject       levelTextPrefab;  // 레벨 Text UI 프리팹
    private TextMeshProUGUI  levelText;        // 레벨 Text UI

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

    public float MaxHp { protected set; get; }

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

    protected virtual void Awake()
    {
        movement3D = GetComponent<Movement3D>();

        // UI 세팅 - 체력 Slider UI
        GameObject hpSliderClone = Instantiate(hpSliderPrefab);
        Canvas canvas = FindObjectOfType<Canvas>();
        hpSliderClone.transform.SetParent(canvas.transform);
        hpSliderClone.transform.localScale = Vector3.one;
        hpSliderClone.GetComponent<SliderAutoPosition>().Setup(gameObject.transform);
        hpSlider = hpSliderClone.GetComponent<Slider>();

        // UI 세팅 - 레벨 Text UI
        GameObject levelTextClone = Instantiate(levelTextPrefab);
        levelTextClone.transform.SetParent(canvas.transform);
        levelTextClone.transform.localScale = Vector3.one;
        levelTextClone.GetComponent<SliderAutoPosition>().Setup(gameObject.transform);
        levelText = levelTextClone.GetComponent<TextMeshProUGUI>();

        // 캐릭터 정보 설정
        MaxHp = hp;
        Hp    = hp;
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

        // 색상 변경
        StartCoroutine("OnHitColor");
    }

    private IEnumerator OnHitColor()
    {
        // 색을 빨간 색으로 변경한 후 0.1초 후에 원래 색상으로 변경
        Color originColor = gameObject.GetComponentInChildren<MeshRenderer>().material.color;

        foreach ( MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>() )
        {
            renderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        foreach ( MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>() )
        {
            renderer.material.color = originColor;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "사망");

        gameObject.tag = "Untagged";

        StartCoroutine("Death");
    }

    private IEnumerator Death()
    {
        // 충돌 해제
        foreach ( Collider collider in GetComponentsInChildren<Collider>() ) { collider.enabled = false; }

        // 이동 중지
        GetComponent<Movement3D>().enabled = false;

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }
}
