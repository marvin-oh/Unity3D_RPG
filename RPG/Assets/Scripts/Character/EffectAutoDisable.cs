using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAutoDisable : MonoBehaviour
{
    [SerializeField] private float time = 0.5f;

    private void OnEnable() => Invoke("Disable", time);

    private void Disable() => gameObject.SetActive(false);
}
