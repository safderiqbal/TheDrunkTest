using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DrunkCheckClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ClientWebSocketTest().Wait();
            Console.ReadKey();
        }

        public async static Task ClientWebSocketTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            ClientWebSocket clientWebSocket = new ClientWebSocket();
            await clientWebSocket.ConnectAsync(new Uri(@"http://localhost:10000"), cts.Token);

            ArraySegment<byte> incommingBytes = new ArraySegment<byte>();

            await clientWebSocket.ReceiveAsync(incommingBytes, new CancellationToken());

            Console.WriteLine(incommingBytes);

            await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, new CancellationToken());
        }
    }
}
