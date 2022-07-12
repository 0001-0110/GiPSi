using System;
using UnityEngine;

namespace Services
{
    public static class PhoneService
    {
        public static void SendSMS(string mobileNumber, string message)
        {
#if UNITY_ANDROID
            string URL = $"sms:{mobileNumber}?body={message}";
#else

#if UNITY_IOS
            string URL = $"sms:{mobileNumber}?&body={UnityWebRequest.EscapeURL(message)}";
#else
            throw new Exception($"What are you trying to achieve exaclty ?");
#endif
#endif
            //Execute Text Message
            Application.OpenURL(URL);
        }
    }
}
