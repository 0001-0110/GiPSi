using Services;

public class LogInScreenController : ConnectionScreenController
{
    private bool IsPasswordCorrect()
    {
        throw new System.NotImplementedException();
        //return SecurityService.HashString(PasswordInput.text) == 
    }

    /// <summary>
    /// Test if the connection informations are correct
    /// </summary>
    /// <returns></returns>
    protected override bool IsInputValid()
    {
        // TODO this needs to be connected to the database
        // TODO remove temp value
        return base.IsInputValid() && IsPasswordCorrect();
    }

    /// <summary>
    /// Load the profile of the corresponding user
    /// </summary>
    protected override void Connect()
    {
        base.Connect();
        scheduleController.LoadTimeTable();
    }
}
