using UnityEngine;

using Pathfinding;

public class MainMenuController : ScreenController
{
    public GameObject PositionScreen;
    private PositionScreenController positionScreenController;
    
    public ScanningScreenController ScanningScreenController;

    public override void Start()
    {
        positionScreenController = PositionScreen.GetComponent<PositionScreenController>();
    }

    public void StartNavigation(int destinationIndex)
    {
        positionScreenController.SetDestination((Destination)destinationIndex);
        OpenScreen(PositionScreen);
    }

    public void InitScan()
    {
        ScanningScreenController.InitScan(gameObject, QRCodeReader.Mode.Position);
    }
}
