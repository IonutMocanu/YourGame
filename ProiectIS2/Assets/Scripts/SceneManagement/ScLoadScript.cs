using UnityEngine;
using UnityEngine.SceneManagement;

public class ScLoadScript : MonoBehaviour
{
    public void NextScene(int index) // e 1 pt urmatoarea scena si -1 pt cea anterioara
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + index;
        Debug.Log($"{nextSceneIndex}");
        if (SceneManager.sceneCount >= nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
