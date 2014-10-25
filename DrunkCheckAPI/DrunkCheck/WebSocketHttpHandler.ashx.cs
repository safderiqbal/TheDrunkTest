using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;
using DrunkCheck.Models;

namespace DrunkCheck
{
    public class WebSocketHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(InitialiseWebSocket);
            else
                context.Response.StatusCode = 400;
        }

        private async Task InitialiseWebSocket(AspNetWebSocketContext context)
        {
            DrunkCheckInterface.WebSocket = context.WebSocket;

            await DrunkCheckInterface.TestConnection();

            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));

                if (!DrunkCheckInterface.IsActive())
                    break;
            }
        }

        public bool IsReusable { get { return true; } }
    }
}