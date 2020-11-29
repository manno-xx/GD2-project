using UnityEngine;

/// <summary>
/// Crude way to move the player for now.
/// No Rigid body involved etc.
/// </summary>
public class Player : MonoBehaviour
{

    [SerializeField]
    float speed;

    void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            0,
            Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}
