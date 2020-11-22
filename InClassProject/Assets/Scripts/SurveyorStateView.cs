using UnityEngine;

public class SurveyorStateView : MonoBehaviour
{
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

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
