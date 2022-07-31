using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConnectionScreenController : ScreenController
{
    public ScheduleController ScheduleController;

    public InputField FirstNameInput;
    public InputField LastNameInput;
    public InputField PasswordInput;

    public TextController TextController;
    protected InputField[] inputFields;
    protected Dropdown[] dropdowns;

    public string InvalidText;

    /// <summary>
    /// the sreen to go to if the connection is succesful
    /// </summary>
    public GameObject NextScreen;

    public override void Awake()
    {
        base.Awake();
        inputFields = GetComponentsInChildren<InputField>();
        dropdowns = GetComponentsInChildren<Dropdown>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        ResetScreen();
    }

    protected virtual void ResetScreen()
    {
        // Clear all input fields
        foreach (InputField inputField in inputFields)
            inputField.text = string.Empty;
        // Reset all Dropdowns
        foreach (Dropdown dropdown in dropdowns)
            dropdown.value = 0;
    }

    protected async Task InvalidInput(int delay = 1000)
    {
        string previousLocalizationString = TextController.localizationString;
        TextController.SetText(InvalidText);
        await Task.Delay(delay);
        TextController.SetText(previousLocalizationString);
    }

    protected abstract bool IsInputValid();

    /// <summary>
    /// TODO need to be renamed
    /// </summary>
    public async void Validate()
    {
        if (!IsInputValid())
            await InvalidInput();
        else
        {
            // TODO save the profile
            // TODO this is going to throw an null reference exception
            ScheduleController.LoadTimeTable($"{LastNameInput.text}_{FirstNameInput.text}");
            // Go to the main menu screen
            OpenScreen(NextScreen);
            NextScreen.GetComponent<ModularScreenController>().SetMode("SignUp");
        }
    }
}
