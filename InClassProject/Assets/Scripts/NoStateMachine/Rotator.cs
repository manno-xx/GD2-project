using Unity.RemoteConfig;
using UnityEngine;

// Remote Config code based on: https://docs.unity3d.com/Packages/com.unity.remote-config@1.4/manual/CodeIntegration.html

public struct UserAttributes
{
    //any attributes you might want to use for segmentation, empty if nothing
}
public struct AppAttributes
{
    //any attributes you might want to use for segmentation, empty if nothing
}

/// <summary>
/// Rotates the camera.
///
/// The Remote Config code makes the class rather bloated.
/// Can no doubt be optimised... (Single Responsibility and all that jazz)
/// </summary>
public class Rotator : MonoBehaviour
{
    public float RotationSpeed = 0;
    
    /// <summary>
    /// Actually rotate the camera
    /// </summary>
    void Update()
    {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime, Space.Self);
    }
}
