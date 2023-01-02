using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServiceLocator.Instance.Get<SceneFlowController>().Reload();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(ServiceLocator.Instance.Get<Cube>(SerLocID.cube1).gameObject);
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Destroy(ServiceLocator.Instance.Get<Cube>(SerLocID.cube2).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(ServiceLocator.Instance.Get<Transform>(SerLocID.sphere).gameObject);
        }
    }
}
