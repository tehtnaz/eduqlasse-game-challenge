using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Scene we are going to change to
    [SerializeField] private string sceneToLoad;

    // Swaps to the scene we have this script set to
    public void SwapScene()
    {
        if (sceneToLoad != null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
