using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField]
    private GameObject expSliderPrefab;  // 경험치 Slider UI 프리팹
    private Slider     expSlider;        // 경험치 Slider UI

    private float  currentExp;  // 현재 경험치
    private float  maxExp;      // 최대 경험치

    protected override void Awake()
    {
        base.Awake();

        // 초기 경험치 설정
        currentExp = 0;
        maxExp = 300;

        // UI 세팅
        GameObject expSliderClone = Instantiate(expSliderPrefab);
        Canvas canvas = FindObjectOfType<Canvas>();
        expSliderClone.transform.SetParent(canvas.transform);
        expSliderClone.transform.localScale = Vector3.one;
        expSliderClone.GetComponent<SliderAutoPosition>().Setup(gameObject.transform);
        expSlider = expSliderClone.GetComponent<Slider>();
        expSlider.value = currentExp / maxExp;
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void TakeDamage(float damage, Transform attacker)
    {
        base.TakeDamage(damage, attacker);
    }

    protected override void Die()
    {
        base.Die();
    }

    public void IncreaseExp(float exp)
    {
        currentExp += exp;

        // 최대 경험치에 도달시 레벨업
        if ( currentExp >= maxExp )
        {
            LevelUp();
        }

        // UI 갱신
        expSlider.value = currentExp / maxExp;

        Debug.Log("EXP: " + currentExp);
    }

    private void LevelUp()
    {
        Level++;

        currentExp -= maxExp;
    }
}
