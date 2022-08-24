using System;
using UnityEngine.UI;

using Services;

public class SignUpScreenController : ConnectionScreenController
{
    public Dropdown BirthDayInput;
    public Dropdown BirthMonthInput;
    public Dropdown BirthYearInput;

    public InputField ConfirmPasswordInput;

    public override void Start()
    {
        base.Start();
        // Initialise the dropdown to display the correct amount of days
        BirthDayInput.AddOptions(Services.ListService.StringRange(1, 31));
        // Initialise the dropdown to display the correct amount of month
        BirthMonthInput.AddOptions(Services.ListService.StringRange(1, 12));
        // Initialise the dropdown to display the possible years.
        // Might cause issue if someone over 150 year old tries to use the app
        BirthYearInput.AddOptions(Services.ListService.StringRange(DateTime.Now.Year - 125, DateTime.Now.Year));
    }

    protected override bool IsInputValid()
    {
        return base.IsInputValid() && PasswordInput.text == ConfirmPasswordInput.text;
    }

    /// <summary>
    /// Create the new user profile
    /// </summary>
    protected override void Connect()
    {
        base.Connect();
        // TODO create new profile
        string passwordHash = SecurityService.HashString(PasswordInput.text);
    }
}
