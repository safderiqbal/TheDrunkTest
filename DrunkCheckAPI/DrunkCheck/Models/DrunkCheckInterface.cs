using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrunkCheck.Models
{
    public static class DrunkCheckInterface
    {
        public static WebSocket WebSocket;
        private static readonly object LockObject = new object();

        public async static Task TestConnection()
        {
            if (WebSocket.State == WebSocketState.Open)
            {
                await WebSocket.SendAsync(new ArraySegment<byte>(
                    Encoding.UTF8.GetBytes(string.Empty)),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
            else
            {
                throw new Exception("Web socket was not open.");
            }
        }

        public static bool IsActive()
        {
            return WebSocket.State == WebSocketState.Open;
        }

        public static DrunkCheckResponse Read(User user = null)
        {
            lock (LockObject)
            {
                if (WebSocket == null || WebSocket.State != WebSocketState.Open)
                    throw new Exception("No connection to DrunkCheckClient");

                WebSocket.SendAsync(new ArraySegment<byte>(
                    Encoding.UTF8.GetBytes(string.Empty)),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None).Wait();

                byte[] bytes = new byte[4];
                ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes);
                WebSocket.ReceiveAsync(arraySegment, CancellationToken.None).Wait();

                return new DrunkCheckResponse(user, BitConverter.ToInt32(bytes, 0));
            }
        }
    }
}