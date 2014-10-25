using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrunkCheck.APIs
{
    public class ClockWorkSMS
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings.Get("ClockWorkSMSAPIKey");
    }
}
