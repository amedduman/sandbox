using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ServiceLocator.Instance.Get<ColorController>().ChangeColor(GetComponent<Renderer>());            
        }
    }
}
