using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithLevel : MonoBehaviour
{
    private void Update()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    
}