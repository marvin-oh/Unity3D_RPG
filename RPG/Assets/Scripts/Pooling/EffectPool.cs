using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public static EffectPool Instance { private set; get; }
    private void Awake() => Instance = this;

    [SerializeField] private List<GameObject> hitEffects;

    public void HitEffect(Vector3 position)
    {
        foreach ( GameObject hitEffect in hitEffects )
        {
            if ( !hitEffect.activeSelf )
            {
                hitEffect.transform.position = position;
                hitEffect.SetActive(true);
                return;
            }
        }
    }
}
