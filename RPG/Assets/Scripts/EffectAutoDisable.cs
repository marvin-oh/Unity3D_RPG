using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAutoDisable : MonoBehaviour
{
    private void Update()
    {
        if ( !GetComponent<ParticleSystem>().isPlaying )
        { gameObject.SetActive(false); }
    }
}
