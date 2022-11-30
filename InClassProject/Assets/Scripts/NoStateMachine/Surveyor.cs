using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sort of security camera script
/// checks to see if 'the thief' is within field of view (using dot product)
/// Invokes a UnityEvent if it does.
///
/// Does not take obstacles into account (yet?)
/// </summary>
public class Surveyor : MonoBehaviour
{
    [Tooltip("Horizontal field of view in degrees")]
    [Range(0, 180)]
    public float FOV;

    float minimumDotProduct = 0;

    [Tooltip("The gameobject that represents the thief to watch out for")]
    public GameObject Thief;

    bool hasSeen = false;

    public delegate void SurveyDelegate(bool doSee);
    public static SurveyDelegate SeeChange;

    /// <summary>
    /// Convert values from Inspector to more Computer readable info
    /// </summary>
    void Start()
    {
        // convert from FoV in degrees to the equivalent to fit the dot product.
        minimumDotProduct = Mathf.Cos(Mathf.Deg2Rad * (FOV / 2));
    }

    /// <summary>
    /// Checks if the 'thief' is in Field of View or not.
    /// When that state has changed, the SeeCHange event is invoked
    /// </summary>
    void Update()
    {
        Vector3 toThief = (Thief.transform.position - transform.position).normalized;
        float currentDotProduct = Vector3.Dot(transform.forward, toThief);
        bool isSeeing = currentDotProduct >= minimumDotProduct;

        if (isSeeing && !hasSeen)
        {
            SeeChange?.Invoke(true);
            hasSeen = true;
        }
        else if (!isSeeing && hasSeen)
        {
            SeeChange?.Invoke(false);
            hasSeen = false;
        }
    }

    /// <summary>
    /// Draw a Unit vector from this to the GameObject to watch out for.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Thief.transform.position - transform.position);
    }
}
