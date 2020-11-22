using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// Keeps track of the stats needed for analytics
/// </summary>
public class SceneStatsTracker : MonoBehaviour
{
    int TimesSeen = 0;

    float duration = 0;

    /// <summary>
    /// Tell Unity we're starting Level One!
    /// </summary>
    void Start()
    {
        AnalyticsEvent.LevelStart("level_one");
    }

    /// <summary>
    /// Keep track of how long we remain in this scene.
    /// </summary>
    private void Update()
    {
        duration += Time.deltaTime;
    }

    /// <summary>
    /// This handler is subcribed to the UnityEvent dispatcher of the camera (which does the 'seeing')
    /// </summary>
    /// <param name="IsSeen"></param>
    public void CountTimesSeen(bool IsSeen)
    {
        if (IsSeen)
        {
            TimesSeen++;
        }
    }

    /// <summary>
    /// When this object is destroyed (assuming that only happens when the scene is 'closed') send an analytics event
    /// </summary>
    void OnDestroy()
    {
        AnalyticsEvent.LevelComplete("level_one", new Dictionary<string, object> {
            {"times_seen", TimesSeen },
            {"duration", duration }
        });

        Debug.Log($"duration, {duration}, times seen {TimesSeen}");
    }
}
