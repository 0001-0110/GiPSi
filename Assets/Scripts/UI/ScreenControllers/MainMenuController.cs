using UnityEngine;

using Pathfinding;
using Schedule;

public class MainMenuController : ScreenController
{
    public GameObject PositionScreen;
    private PositionScreenController positionScreenController;

    private ScheduleController scheduleController;
    
    public ScanningScreenController ScanningScreenController;

    public override void Start()
    {
        positionScreenController = PositionScreen.GetComponent<PositionScreenController>();

        scheduleController = ScheduleController.Instance;
    }

    public void StartNavigation(int destinationIndex)
    {
        Debug.Log($"DEBUG - 22 | {(Destination)destinationIndex}");
        positionScreenController.SetDestination((Destination)destinationIndex);
        OpenScreen(PositionScreen);
    }

    public void StartNavigation(Destination destination)
    {
        Debug.Log($"DEBUG - 22 | {destination}");
        positionScreenController.SetDestination(destination);
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
