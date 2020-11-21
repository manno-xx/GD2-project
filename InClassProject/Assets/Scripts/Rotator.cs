using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotationSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime, Space.Self);
    }
}
