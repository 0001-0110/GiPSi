using UnityEngine;
using Services;

public class SOSScreenController : ScreenController
{
    [Tooltip("The list of all numbers to send the message to")]
    public string[] MobileNumbers;
    [Tooltip("The message sent")]
    public string SOSMessage;

    public void SendSOS()
    {
        foreach (string mobileNumber in MobileNumbers)
            PhoneService.SendSMS(mobileNumber, SOSMessage);
    }
}
