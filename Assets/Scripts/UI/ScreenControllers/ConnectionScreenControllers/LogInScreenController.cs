using UnityEngine.UI;

public class LogInScreenController : ConnectionScreenController
{
    /// <summary>
    /// Are the given inputs valid ?
    /// </summary>
    /// <returns></returns>
    protected override bool IsInputValid()
    {
        // TODO this needs to be connected to the database
        // TODO remove temp value
        return true;
    }
}
