using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowController : ExtendedMB
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("Second", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reload()
    {
        StartCoroutine(CoReload());        
        IEnumerator CoReload()
        {
            yield return SceneManager.UnloadSceneAsync("Second");
            SceneManager.LoadSceneAsync("Second", LoadSceneMode.Additive);
        }
    }
}
