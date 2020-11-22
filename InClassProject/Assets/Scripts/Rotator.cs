using Unity.RemoteConfig;
using UnityEngine;

// Remote code based on: https://docs.unity3d.com/Packages/com.unity.remote-config@1.4/manual/CodeIntegration.html

public struct UserAttributes
{
    //any attributes you might want to use for segmentation, empty if nothing
}
public struct AppAttributes
{
    //any attributes you might want to use for segmentation, empty if nothing
}

public class Rotator : MonoBehaviour
{
    public float RotationSpeed = 0;

    void Awake()
    {
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs<UserAttributes, AppAttributes>(new UserAttributes(), new AppAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log(configResponse.requestOrigin);
        RotationSpeed = ConfigManager.appConfig.GetFloat("CameraRotationSpeed", RotationSpeed);
        Debug.Log(RotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime, Space.Self);
    }
}
