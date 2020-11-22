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
    Vector3 toThief = Vector3.zero;

    bool hasSeen = false;

    [System.Serializable]
    public class SurveyEvent : UnityEvent<bool>
    {
    }

    public SurveyEvent SeeChange;

    /// <summary>
    /// Convert values from Inspector to more Computer readable info
    /// </summary>
    void Start()
    {
        // convert from FoV in degrees to the equivalent to fit the dot product.
        minimumDotProduct = Mathf.Cos(Mathf.Deg2Rad * (FOV / 2));
    }

    // Update is called once per frame
    void Update()
    {
        toThief = (Thief.transform.position - transform.position).normalized;
        float currentDotProduct = Vector3.Dot(transform.forward, toThief);
        bool isSeeing = currentDotProduct >= minimumDotProduct;

        if (isSeeing && !hasSeen)
        {
            SeeChange.Invoke(true);
            hasSeen = true;
        }
        else if (!isSeeing && hasSeen)
        {
            SeeChange.Invoke(false);
            hasSeen = false;
        }
    }

    /// <summary>
    /// Draw a Unit vector from this to the GameObject to watch.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, toThief);
    }
}
