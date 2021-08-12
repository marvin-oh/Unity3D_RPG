using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAutoRotate : MonoBehaviour
{
    private void Update()
    {
        Player player = FindObjectOfType<Player>();
        if ( player != null )
        {
            transform.rotation = player.transform.rotation;
        }
    }
}
