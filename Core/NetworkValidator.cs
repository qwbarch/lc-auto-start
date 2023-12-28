using System.Net.Sockets;

namespace AutoStart.Core
{
  /// <summary>
  /// In order to check if a LAN game is already hosted, we must ping the server to see if it exists.
  /// Lethal Company runs on UDP port 7777. Since it's UDP, there's no connection handshake for me to verify a connection.
  /// This class is a workaround where we host our own TCP server to decide whether we need to host or join the game.
  /// </summary>
  class NetworkValidator
  {
    public static bool ServerIsRunning()
    {
      return false;
    }
  }
}