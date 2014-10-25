using System;
using System.Configuration;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrunkCheckClient
{
    public class ServiceWebSocketInterface : IDisposable
    {
        private ClientWebSocket ClientWebSocket;
        private readonly ArduinoInterface ArduinoInterface;

        public ServiceWebSocketInterface(ArduinoInterface arduinoInterface)
        {
            ArduinoInterface = arduinoInterface;
            WebSocketStart();
            Console.WriteLine("Connected");
        }

        private void WebSocketStart()
        {
            ClientWebSocket = new ClientWebSocket();

            ClientWebSocket.ConnectAsync(
                new Uri(ConfigurationManager.AppSettings["ServerURI"]),
                CancellationToken.None).Wait();

            byte[] bytes = new byte[2];
            ArraySegment<byte> incommingBytes = new ArraySegment<byte>(bytes);
            ClientWebSocket.ReceiveAsync(incommingBytes, CancellationToken.None).Wait();
            
            Task.Run(() => ClientWebSocketTest());
        }

        internal async Task ClientWebSocketTest()
        {
            while (true)
            {
                byte[] bytes = new byte[2];

                ArraySegment<byte> incommingBytes = new ArraySegment<byte>(bytes);

                await ClientWebSocket.ReceiveAsync(incommingBytes, CancellationToken.None);

                Console.WriteLine(Encoding.UTF8.GetChars(incommingBytes.Array));

                int result = ArduinoInterface.Read();

                ArraySegment<byte> sendBytes = new ArraySegment<byte>(BitConverter.GetBytes(result));

                await ClientWebSocket.SendAsync(
                    sendBytes,
                    WebSocketMessageType.Binary,
                    true,
                    CancellationToken.None);
            }
        }

        public void Dispose()
        {
            ClientWebSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        }
    }
}
