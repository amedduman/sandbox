using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour/*ExtendedMB*/
{
    public void ChangeColor(Renderer rnd)
    {
        rnd.material.color = Random.ColorHSV();
    }
}
