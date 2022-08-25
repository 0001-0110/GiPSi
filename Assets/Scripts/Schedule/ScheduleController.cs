using System;
using System.IO;
using UnityEngine;

using Pathfinding;
using Schedule;
using Services;

public class ScheduleController : MonoBehaviour
{
    public static ScheduleController Instance { get; private set; }

    public string UserName { get; set; }
    public TimeTable TimeTable { get; private set; }

    /// <summary>
    /// Return the name of the file containing the schedule for this user
    /// </summary>
    private static Func<string, string> ScheduleFileName = userName => $"Schedule_{userName}.json";
    /// <summary>
    /// Return the entire path of the file containing the schedule for this user
    /// </summary>
    private static Func<string, string> FilePath = userName => Path.Combine(Application.persistentDataPath, ScheduleFileName(userName));

    public void Awake()
    {
        Instance = this;
    }

    public Destination GetRoom(DateTime time)
    {
        if (TimeTable == null)
            return Destination.None;
        return TimeTable.GetRoom(time);
    }

    public Destination GetActiveRoom()
    {
        return GetRoom(DateTime.Now.AddMinutes(5));
    }

    /// <summary>
    /// Load the TimeTable for the current user
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>
    /// <para>Null if the file doesn't exists</para>
    /// </returns>
    public void LoadTimeTable()
    {
        TimeTable = JsonService.Deserialize<TimeTable>(FilePath(UserName));
        if (TimeTable == null)
            // No file corresponding to the current user was found
            TimeTable = new TimeTable();
    }

    public void SaveTimeTable()
    {
        JsonService.Serialize(FilePath(UserName), TimeTable);
    }
}
