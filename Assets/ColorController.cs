using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : ExtendedMB
{
    public void ChangeColor(Renderer rnd)
    {
        rnd.material.color = Random.ColorHSV();
    }
}
