using UnityEngine;

public class Detectable : MonoBehaviour
{
    //Detects manually if obj is being seen by the main camera

    Collider objCollider;

    Camera cam;
    Plane[] planes;

    void Start()
    {
        cam = Camera.main;
        objCollider =  GetComponent<Collider>();
    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            Debug.Log("has been detected!");
        }
        else
        {
            Debug.Log("Nothing has been detected");
        }
    }

    
}
