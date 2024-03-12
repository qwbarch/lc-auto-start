using System.Net.Sockets;
using Cysharp.Threading.Tasks;

namespace AutoStart.Core.Lobby
{
    static class LobbyParticipant
    {
        /// <summary>
        /// Wait until the lobby host is ready, and then join the lobby.
        /// </summary>
        public static async UniTask JoinLobby(MenuManager menuManager)
        {
            var tcpClient = new TcpClient(LobbyHost.Host, LobbyHost.Port);
            var stream = tcpClient.GetStream();
            await stream.ReadAsync(new byte[0], 0, 0);
            Logger.LogInfo("Lobby is ready! Attempting to join the game.");
            menuManager.StartAClient();
        }
    }
}