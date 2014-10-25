using System;
using System.IO.Ports;

namespace DrunkCheck.Models
{
    public class ArduinoInterface : IDisposable
    {
        private readonly SerialPort Port;
        
        internal ArduinoInterface(string portName, int timeout = 30000)
        {
            Port = new SerialPort(portName)
            {
                ReadTimeout = timeout
            };
            Port.Open();
        }

        public int Read()
        {
            Port.WriteLine(string.Empty);
            Port.ReadLine();
            return int.Parse(Port.ReadLine());
        }

        public void Dispose()
        {
            Port.Close();
        }
    }
}