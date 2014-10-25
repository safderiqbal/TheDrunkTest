using System;
using System.Configuration;

namespace DrunkCheckClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ArduinoInterface arduinoInterface = 
                new ArduinoInterface(ConfigurationManager.AppSettings["ComPort"]);

            using (new ServiceWebSocketInterface(arduinoInterface))
            {
                Console.ReadKey();
            }
        }
    }
}
