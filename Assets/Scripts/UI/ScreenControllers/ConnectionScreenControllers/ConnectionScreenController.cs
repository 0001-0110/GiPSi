using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConnectionScreenController : ScreenController
{
    protected ScheduleController scheduleController;

    public InputField UserNameInput;
    //public InputField LastNameInput;
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

        scheduleController = ScheduleController.Instance;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delay"></param>
    /// <remarks>async void because we do not want to wait foir it to end</remarks>
    protected async void InvalidInput(int delay = 1000)
    {
        string previousLocalizationString = TextController.LocalizationString;
        TextController.SetText(InvalidText);
        await Task.Delay(delay);
        TextController.SetText(previousLocalizationString);
    }

    protected virtual bool IsInputValid()
    {
        return UserNameInput.text != string.Empty && PasswordInput.text != string.Empty;
    }

    protected virtual void Connect()
    {
        scheduleController.UserName = UserNameInput.text;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <return>
    /// A boolean representing if the Input is valid
    /// </return>
    public virtual void Validate()
    {
        if (!IsInputValid())
            InvalidInput();
        else
        {
            Connect();
            // Go to the main menu screen
            OpenScreen(NextScreen);
        }
    }
}
