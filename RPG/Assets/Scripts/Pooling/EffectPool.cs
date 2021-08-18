using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public static EffectPool Instance { private set; get; }
    private void Awake() => Instance = this;

    [SerializeField] private List<GameObject> hitEffects;

    public GameObject HitEffect() => hitEffects.Find(x => !x.activeSelf);
}
