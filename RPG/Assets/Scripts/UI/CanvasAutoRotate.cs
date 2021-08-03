using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAutoRotate : MonoBehaviour
{
    private void Update() => transform.rotation = GameObject.FindObjectOfType<Player>().transform.rotation;
}
