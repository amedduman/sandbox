using UnityEngine;
using Zenject;

public class Sphere : MonoBehaviour
{
    [Inject(Id = "cube")] Transform cube;
    //[Inject] InputManager inputManager;


    private void Start()
    {
        Destroy(cube.gameObject);
        //inputManager.Log();
    }
}
