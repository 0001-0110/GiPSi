using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

public class PositionScreenController : ScreenController
{
    public NavMesh navMesh;

    public InputField PositionInput;
    public Button StartNavigationButton;
    private TextController startNavigationButtonTextController;
    [Tooltip("The localization string displayed if the input is invalid")]
    public string InvalidLocalizationString;
    [Tooltip("The delay for the button to reset after an invalid input")]
    public int Delay;

    public GameObject NavigationScreen;
    private NavigationScreenController navigationScreenController;

    private Destination destination;

    public override void Start()
    {
        navigationScreenController = NavigationScreen.GetComponent<NavigationScreenController>();
        startNavigationButtonTextController = StartNavigationButton.GetComponentInChildren<TextController>();
    }

    public void SetDestination(Destination destination)
    {
        this.destination = destination;
    }

    private async Task InvalidInput()
    {
        StartNavigationButton.interactable = false;
        string previousLocalizationString = startNavigationButtonTextController.localizationString;
        startNavigationButtonTextController.SetText(InvalidLocalizationString);
        await Task.Delay(Delay);
        startNavigationButtonTextController.SetText(previousLocalizationString);
        StartNavigationButton.interactable = true;
    }

    public async void StartNavigation()
    {
        Node startingNode = navMesh.GetNode(PositionInput.text);
        Node destinationNode = navMesh.GetNode(startingNode, destination);
        if (startingNode != null && destinationNode != null)
        {
            // Navigation started correctly
            navigationScreenController.StartNavigation(startingNode, destinationNode);
            OpenScreen(NavigationScreen);
            // reset the input field
            PositionInput.text = string.Empty;
        }
        else
        {
            // Starting point or destination cannot be found
            await InvalidInput();
        }
    }
}
