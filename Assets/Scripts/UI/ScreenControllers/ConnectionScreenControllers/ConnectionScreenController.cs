using System;
using System.Threading.Tasks;
using UnityEngine;

public class ConnectionScreenController : ScreenController
{
    public TextController TextController;

    public string InvalidText;

    /// <summary>
    /// the sreen to go to if the connection is succesful
    /// </summary>
    public GameObject NextScreen;

    protected virtual void ResetScreen()
    {
        throw new NotImplementedException();
    }

    protected async void InvalidInput(int delay = 1000)
    {
        string previousLocalizationString = TextController.localizationString;
        TextController.SetText(InvalidText);
        await Task.Delay(delay);
        TextController.SetText(previousLocalizationString);
    }

    protected virtual bool IsInputValid()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO need to be renamed
    /// </summary>
    public void Validate()
    {
        if (!IsInputValid())
            InvalidInput();
        else
        {
            // TODO save the profile
            // Go to the main menu screen
            OpenScreen(NextScreen);
            NextScreen.GetComponent<ModularScreenController>().SetMode("SignUp");
        }
    }
}
