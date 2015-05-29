using NetFramework.AsyncSocketClient;
using NetFramework.AsyncSocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static  class IClientScoketHelper
    {
        public static void Send(this IClientScoket client,string command, string message)
        {
            var buffer = System.Text.UTF8Encoding.Default.GetBytes(command + message);

            client.Send(buffer);
        }

        public static void Send(this IServerSocket server,string connectionId, string command, string message)
        {
            var buffer = System.Text.UTF8Encoding.Default.GetBytes(command + message);

            server.Send(connectionId, buffer);
        }

    }
}
