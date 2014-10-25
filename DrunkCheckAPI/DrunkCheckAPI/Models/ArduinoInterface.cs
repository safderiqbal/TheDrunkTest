using System;
using System.IO.Ports;

namespace DrunkCheckAPI.Models
{
    public class ArduinoInterface : IDisposable
    {
        private readonly SerialPort Port;
        
        internal ArduinoInterface(string portName, int timeout = 5000)
        {
            Port = new SerialPort(portName)
            {
                ReadTimeout = timeout
            };
            Port.Open();
        }

        public string Read()
        {
            Port.Write(string.Empty);
            return Port.ReadLine();
        }

        public void Dispose()
        {
            Port.Close();
        }
    }
}