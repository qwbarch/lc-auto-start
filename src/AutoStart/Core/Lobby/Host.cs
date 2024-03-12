using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace AutoStart.Core.Lobby
{
    static class LobbyHost
    {
        internal const string Host = "127.0.0.1";
        internal const int Port = 7777;

        static readonly LinkedList<TcpClient> Clients = new LinkedList<TcpClient>();

        static bool _IsHosting = false;
        public static bool IsHosting => _IsHosting;

        static bool IsReady = false;

        static async UniTask SendReady(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                await stream.WriteAsync(new byte[0], 0, 0);
            }
            finally
            {
                client.Close();
            }
        }

        /// <summary>
        /// Allow participants to begin joining the lobby.
        /// </summary>
        public static async UniTask SetLobbyReady()
        {
            if (_IsHosting)
            {
                IsReady = true;
                foreach (var client in Clients)
                {
                    await SendReady(client);
                }
                Clients.Clear();
            }
        }

        /// <summary>
        /// Start hosting the lobby. This throws a <b>SocketException</b> if the server is already being hosted.
        /// </summary>
        public static async UniTask Start()
        {
            var listener = new TcpListener(IPAddress.Parse(Host), Port);
            try
            {
                listener.Start();
                _IsHosting = true;
                Logger.LogInfo("LobbyHost is enabled. Waiting for participants.");
            }
            catch (SocketException)
            {
                Logger.LogInfo("LobbyHost is disabled. Waiting for host to be ready before joining.");
            }
            if (_IsHosting)
            {
                while (true)
                {
                    try
                    {
                        var client = await listener.AcceptTcpClientAsync();
                        if (IsReady) await SendReady(client);
                        else Clients.AddLast(client);
                    }
                    catch (SocketException) { }
                }
            }
        }
    }
}