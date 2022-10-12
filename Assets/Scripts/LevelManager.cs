using UnityEngine;
using UnityEngine.SceneManagement;
using Services;


public class LevelManager : MonoBehaviour
{
    InputManager _inputManager;

    void OnEnable()
    {
        _inputManager = Services.ServiceLocator.Instance.Get<InputManager>();

        _inputManager.OnFirstLevelLoadButtonPressed += LoadFirstLevel;
        _inputManager.OnSecondLevelLoadButtonPressed += LoadSecondLevel;
    }

    void OnDisable()
    {
        _inputManager.OnFirstLevelLoadButtonPressed -= LoadFirstLevel;
        _inputManager.OnSecondLevelLoadButtonPressed -= LoadSecondLevel;
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    void LoadSecondLevel()
    {
        SceneManager.LoadScene(1);
    }
}
