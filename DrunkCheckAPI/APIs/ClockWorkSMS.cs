using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Clockwork;

namespace DrunkCheck.APIs
{
    public static class ClockWorkSms
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings.Get("ClockWorkSMSAPIKey");

        public static bool SendMessage(int receiverNumber, string message)
        {
            API api = new API(ApiKey);
            try
            {
                SMSResult result = api.Send(
                    new SMS {
                        To = receiverNumber.ToString(CultureInfo.InvariantCulture),
                        Message = message
                    }
                );

                if (result.Success)
                {
                    Debug.WriteLine("SMS Sent to {0}, Clockwork ID: {1}", result.SMS.To, result.ID);
                    return true;
                }
                Debug.WriteLine("SMS to {0} failed, Clockwork Error: {1} {2}", result.SMS.To, result.ErrorCode, result.ErrorMessage);
                return false;
            }
            catch (APIException ex)
            {
                // You'll get an API exception for errors
                // such as wrong username or password
                Debug.WriteLine("API Exception: " + ex.Message);
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn't reach the Clockwork server
                Debug.WriteLine("Web Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                Debug.WriteLine("Argument Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                Debug.WriteLine("Unknown Exception: " + ex.Message);
            }
            return false;
        }
    }
}
