using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Keeps track of the stats needed for analytics
/// </summary>
public class SceneStatsTracker : MonoBehaviour
{
    int TimesSeen = 0;

    float duration = 0;

    private Sessions sessions;

    /// <summary>
    /// Tell Unity we're starting Level One!
    /// </summary>
    void Start()
    {
        Debug.Log(Application.dataPath);
        ReadJSON();
    }

    private void OnEnable()
    {
        FollowState.SeeChange += CountTimesSeen;
    }

    private void OnDisable()
    {
        FollowState.SeeChange -= CountTimesSeen;
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
        WriteJSON();
        AppendCSV();
    }

    private void ReadJSON()
    {
        string path = Application.dataPath + @"/Resources/stats.txt";

        // This text is added only once to the file.
        if (File.Exists(path))
        {
            string contents = File.ReadAllText(path);
            sessions = JsonUtility.FromJson<Sessions>(contents);
        }
    }
    
    /// <summary>
    /// Write JSON to disk
    /// </summary>
    private void WriteJSON()
    {
        Level stats = new Level()
        {
            Duration = duration,
            TimesSeen = TimesSeen
        };
        
        sessions.sessions.Add(stats);

        string path = Application.dataPath + @"/Resources/stats.txt";
        string data = JsonUtility.ToJson(sessions);
        
        // Create a file to write to.
        File.WriteAllText(path, data);
    }

    /// <summary>
    /// append data to JSON
    /// </summary>
    private void AppendCSV()
    {
        Level stats = new Level()
        {
            Duration = duration,
            TimesSeen = TimesSeen
        };
        string path = Application.dataPath + @"/Resources/stats.csv";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, stats.ToString() + Environment.NewLine);
        }
        else
        {
            File.AppendAllText(path, stats.ToString() + Environment.NewLine);
        }
    }
}

[Serializable]
class Sessions
{
    public List<Level> sessions;
}

[Serializable]
class Level
{
    public string when = DateTime.Now.ToString();
    public int TimesSeen = 0;
    public float Duration = 0;

    public override string ToString()
    {
        return $"{when}, {TimesSeen}, {Duration}";
    }
}