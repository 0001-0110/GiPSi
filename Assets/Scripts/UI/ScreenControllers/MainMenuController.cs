using UnityEngine;

using Pathfinding;

public class MainMenuController : ScreenController
{
    public GameObject PositionScreen;
    public NavigationScreenController NavigationScreenController;

    private ScheduleController scheduleController;
    
    public ScanningScreenController ScanningScreenController;

    public override void Start()
    {
        scheduleController = ScheduleController.Instance;
    }

    public void StartNavigation(int destinationIndex)
    {
        Debug.Log($"DEBUG: Navigation - The destination is {(Destination)destinationIndex}");
        NavigationScreenController.Destination = (Destination)destinationIndex;
        OpenScreen(PositionScreen);
    }

    public void StartNavigation(Destination destination)
    {
        Debug.Log($"DEBUG: Navigation - The destination is {destination}");
        NavigationScreenController.Destination = destination;
        OpenScreen(PositionScreen);
    }

    public void StartNavigation()
    {
        StartNavigation(scheduleController.GetActiveRoom());
    }

    public void InitScan()
    {
        ScanningScreenController.InitScan(gameObject, QRCodeReader.Mode.Position);
    }
}
