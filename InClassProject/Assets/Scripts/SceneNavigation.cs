using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that implements basic navigation methods
/// </summary>
public class SceneNavigation : MonoBehaviour
{

    /// <summary>
    /// Let the scene manager load a new scene
    /// </summary>
    /// <param name="sceneName">The name of the scene to load</param>
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
