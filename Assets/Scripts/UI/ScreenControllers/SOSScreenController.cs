using UnityEngine;
using UnityEngine.Networking;

public class SOSScreenController : ScreenController
{
    public string MobileNumber;

    public void SendSOS(string message)
    {
        Services.PhoneService.SendSMS(MobileNumber, message);
    }
}
