using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ConnectionScreenController : ScreenController
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

    protected async Task InvalidInput(int delay = 1000)
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
    public async void Validate()
    {
        if (!IsInputValid())
            await InvalidInput();
        else
        {
            // TODO save the profile
            // Go to the main menu screen
            OpenScreen(NextScreen);
            NextScreen.GetComponent<ModularScreenController>().SetMode("SignUp");
        }
    }
}
