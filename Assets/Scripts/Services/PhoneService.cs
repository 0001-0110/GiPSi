using System;
using UnityEngine;

namespace Services
{
    public static class PhoneService
    {
        public static void SendSMS(string mobileNumber, string message)
        {
#if UNITY_ANDROID
            Application.OpenURL($"sms:{mobileNumber}?body={message}");
            return;
#endif
#if UNITY_IOS
            Application.OpenURL($"sms:{mobileNumber}?&body={UnityWebRequest.EscapeURL(message)}");
            return;
#endif
            // Not a phone
            throw new Exception($"What are you trying to achieve exaclty ?");
        }
    }
}
