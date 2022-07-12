using UnityEngine.UI;

public class LogInScreenController : ConnectionScreenController
{
    public InputField IDInput;
    public InputField PasswordInput;

    public override void OnEnable()
    {
        base.OnEnable();
        ResetScreen();
    }

    protected override void ResetScreen()
    {
        foreach (InputField inputField in new InputField[] { IDInput, PasswordInput })
            inputField.text = string.Empty;
    }

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
