using Services;

public class SOSScreenController : ScreenController
{
    public string MobileNumber;

    public void SendSOS(string message)
    {
        PhoneService.SendSMS(MobileNumber, message);
    }
}
