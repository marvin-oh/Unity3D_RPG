using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] private GameObject expSliderPrefab;  // 경험치 Slider UI 프리팹
    private Slider expSlider;                             // 경험치 Slider UI

    private float  currentExp = 0;  // 현재 경험치
    private float  maxExp = 300;    // 최대 경험치

    protected override void OnEnable()
    {
        base.OnEnable();

        // UI 세팅
        Canvas canvas = GetComponentInChildren<Canvas>();
        GameObject expSliderClone = Instantiate(expSliderPrefab, canvas.transform);
        expSlider = expSliderClone.GetComponent<Slider>();
        expSlider.value = currentExp / maxExp;
    }

    public void IncreaseExp(float exp)
    {
        currentExp += exp;

        // 최대 경험치에 도달시 레벨업
        if ( currentExp >= maxExp ) { LevelUp(); }

        // UI 갱신
        expSlider.value = currentExp / maxExp;
    }

    private void LevelUp()
    {
        Level++;
        Hp = MaxHp;
        currentExp = 0;
    }
}
