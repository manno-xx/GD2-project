using UnityEngine;

/// <summary>
/// Changes the color of the renderer based on the info recieved
/// </summary>
public class SurveyorStateView : MonoBehaviour
{
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// If parameter is true, renderer's color is set to red
    /// Otherwise it is set to white.
    /// </summary>
    /// <param name="alert">The parameter indicating the State of Alert</param>
    public void ChangeColor(bool alert)
    {
        if (alert)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }
}
