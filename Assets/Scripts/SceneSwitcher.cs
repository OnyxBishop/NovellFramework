using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public event Action GameOver;

    public void SetNext()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int totalScenes = SceneManager.sceneCountInBuildSettings; 

        if (currentSceneIndex >= totalScenes)
        {
            GameOver?.Invoke();
            return;
        }

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
