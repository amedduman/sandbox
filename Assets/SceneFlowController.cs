using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowController : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("Second", LoadSceneMode.Additive);
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
