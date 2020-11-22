using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// Keeps track of the stats needed for analytics
/// </summary>
public class SceneStatsTracker : MonoBehaviour
{
    int TimesSeen = 0;

    float startTime = 0;
    float duration = 0;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        startTime = Time.time;

        AnalyticsEvent.LevelStart("level_one");
    }

    private void Update()
    {
        duration = Time.time - startTime;
    }

    public void CountTimesSeen(bool IsSeen)
    {
        if (IsSeen)
        {
            TimesSeen++;
        }
    }

    void OnDestroy()
    {
        AnalyticsEvent.LevelComplete("level_one", new Dictionary<string, object> {
            {"times_seen", TimesSeen },
            {"duration", duration }
        });

        Debug.Log($"duration, {duration}, times seen {TimesSeen}");
    }
}
