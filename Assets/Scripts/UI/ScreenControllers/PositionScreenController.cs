using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

public class PositionScreenController : ScreenController
{
    public InputField PositionInput;

    public GameObject NavigationScreen;
    private NavigationScreenController navigationScreenController;

    private Destination destination;

    public override void Start()
    {
        navigationScreenController = NavigationScreen.GetComponent<NavigationScreenController>();
    }

    public void SetDestination(Destination destination)
    {
        this.destination = destination;
    }

    public void StartNavigation()
    {
        if (navigationScreenController.StartNavigation(PositionInput.text, destination))
            // Navigation started correctly
            OpenScreen(NavigationScreen);
        else
            // Starting point or destination cannot be found
            // TODO
            return;
    }
}
