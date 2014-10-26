using System;
using System.Configuration;
using System.Net;
using Clockwork;

namespace DrunkCheck.APIs
{
    public static class ClockWorkSms
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings.Get("ClockWorkSMSAPIKey");

        public static bool SendTestMessage()
        {
            API api = new API(ApiKey);
            try
            {
                SMSResult result = api.Send(new SMS {To = "441234567890", Message = "Hello World"});

                return result.Success;
            }
            catch (APIException ex)
            {
                // You'll get an API exception for errors
                // such as wrong username or password
                Console.WriteLine("API Exception: " + ex.Message);
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn't reach the Clockwork server
                Console.WriteLine("Web Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                Console.WriteLine("Argument Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                Console.WriteLine("Unknown Exception: " + ex.Message);
            }
            return false;
        }
    }
}
