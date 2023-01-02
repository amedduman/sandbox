using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServiceLocator.Get<SceneFlowController>().Reload();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(ServiceLocator.Get<Cube>(SerLocID.cube1).gameObject);
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Destroy(ServiceLocator.Get<Cube>(SerLocID.cube2).gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(ServiceLocator.Get<Transform>(SerLocID.sphere).gameObject);
        }
    }
}
