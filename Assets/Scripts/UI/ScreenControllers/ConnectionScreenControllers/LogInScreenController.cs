using UnityEngine;

public class LogInScreenController : ConnectionScreenController
{
    private const string userNamePreference = "UserName";
    public static string UserNamePreference { get { return userNamePreference; } }

    public override void OnEnable()
    {
        base.OnEnable();
        if (PlayerPrefs.HasKey(userNamePreference))
            UserNameInput.text = PlayerPrefs.GetString(userNamePreference);
    }

    private bool IsPasswordCorrect()
    {
        // TODO
        return true;
        //return SecurityService.HashString(PasswordInput.text) == 
    }

    /// <summary>
    /// Test if the connection informations are correct
    /// </summary>
    /// <returns></returns>
    protected override bool IsInputValid()
    {
        // TODO this needs to be connected to the database
        return base.IsInputValid() && IsPasswordCorrect();
    }

    /// <summary>
    /// Load the profile of the corresponding user
    /// </summary>
    protected override void Connect()
    {
        base.Connect();
        scheduleController.LoadTimeTable();
        // TODO
    }
}
