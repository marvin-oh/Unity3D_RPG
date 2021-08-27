using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PlayableCharacter
{
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;

    [Header("EXP")]
    [SerializeField] private GameObject expSliderPrefab;    // 경험치 Slider UI 프리팹
    private Slider expSlider;   // 경험치 Slider UI

    public override float Hp
    {
        set
        {
            base.Hp = value;

            // Data 저장
            SaveData();
        }
        get
        {
            return base.Hp;
        }
    }
    public override float Mp
    {
        set
        {
            base.Mp = value;

            // Data 저장
            SaveData();
        }
        get
        {
            return base.Mp;
        }
    }

    public Inventory Inventory { get => inventory; }
    public float Exp    { private set; get; } = 0;
    public float MaxExp { private set; get; } = 300;


    protected override void OnEnable()
    {
        base.OnEnable();

        // UI 세팅
        expSlider = Instantiate(expSliderPrefab, canvas.transform).GetComponent<Slider>();
        expSlider.value = Exp / MaxExp;

        // Data 저장
        SaveData();
    }


    /// <summary>
    /// 캐릭터 사망 (Animation에서 호출)
    /// </summary>
    protected override void Die()
    {
        GetComponent<PlayerController>().enabled = false;

        base.Die();
    }

    /// <summary>
    /// 데미지 피격시 호출
    /// </summary>
    public override void TakeDamage(float damage, Transform attacker)
    {
        base.TakeDamage(damage, attacker);

        // Data 동기화
        PlayerController controller = GetComponent<PlayerController>();
        if ( (controller != null) && controller.enabled ) { controller.SaveData(); }
    }

    /// <summary>
    /// Weapon 교체 메소드
    /// </summary>
    public override void ChangeWeapon(string _name)
    {
        base.ChangeWeapon(_name);

        // Data 저장
        SaveData();
    }

    /// <summary>
    /// Shoes 교체 메소드
    /// </summary>
    public override void ChangeShoes(string _name)
    {
        base.ChangeShoes(_name);

        // Data 저장
        SaveData();
    }

    /// <summary>
    /// Halmet 교체 메소드
    /// </summary>
    public override void ChangeHalmet(string _name)
    {
        base.ChangeHalmet(_name);

        // Data 저장
        SaveData();
    }

    /// <summary>
    /// Armor 교체 메소드
    /// </summary>
    public override void ChangeArmor(string _name)
    {
        base.ChangeArmor(_name);

        // Data 저장
        SaveData();
    }


    /// <summary>
    /// 레벨업
    /// </summary>
    private void LevelUp()
    {
        Level++;
        Hp  = MaxHp;
        Exp = 0;

        // Data 저장
        SaveData();
    }

    /// <summary>
    /// 경험치 획득
    /// </summary>
    public void IncreaseExp(float exp)
    {
        Exp += exp;

        // 최대 경험치에 도달시 레벨업
        if ( Exp >= MaxExp ) { LevelUp(); }

        // UI 갱신
        if ( expSlider != null ) { expSlider.value = Exp / MaxExp; }

        // Data 저장
        SaveData();
    }

    /// <summary>
    /// 골드 획득
    /// </summary>
    public void IncreaseGold(int gold)
    {
        Inventory.IncreaseGold(gold);
        GameManager.Instance.Notice(characterName + " get " + gold + " G");
    }

    /// <summary>
    /// 소모품 사용
    /// </summary>
    public void UseConsumable(Consumable consumable)
    {
        Hp += consumable.hpGain;
        //Mp += consumable.mpGain;
        IncreaseExp(consumable.expGain);
    }

    /// <summary>
    /// 데이터 불러오기
    /// </summary>
    public void LoadData(PlayerData playerData)
    {
        Level = playerData.Level;
        Hp    = playerData.Hp;
        IncreaseExp(playerData.Exp);
        ChangeWeapon(ItemManager.Instance.GetWeapon(playerData.WeaponID).EquipName);
        ChangeShoes(ItemManager.Instance.GetShoes(playerData.ShoesID).EquipName);
        ChangeHalmet(ItemManager.Instance.GetHalmet(playerData.HalmetID).EquipName);
        ChangeArmor(ItemManager.Instance.GetArmor(playerData.ArmorID).EquipName);
    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    private void SaveData()
    {
        PlayerController controller = GetComponent<PlayerController>();
        if ( (controller != null) && controller.enabled ) { controller.SaveData(); }
    }
}
