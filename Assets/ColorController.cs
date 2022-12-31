using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public void ChangeColor(Renderer rnd)
    {
        rnd.material.color = Random.ColorHSV();
    }
}
