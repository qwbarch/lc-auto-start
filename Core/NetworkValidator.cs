using System.Net;
using System.Net.Sockets;

namespace AutoStart.Core
{
  /// <summary>
  /// In order to check if a LAN game is already hosted, we must ping the server to see if it exists.
  /// Lethal Company runs on UDP port 7777. Since it's UDP, there's no connection handshake for me to verify a connection.
  /// This class is a workaround where we host our own TCP server to decide whether we need to host or join the game.
  /// </summary>
  public class NetworkValidator
  {
    private readonly Config Config;

    public NetworkValidator(Config config)
    {
      Config = config;
    }

    /// <summary>
    /// Check whether the network validator server is running.
    /// If it's on, it indicates that we should join the LAN game instead of hosting one.
    /// </summary>
    public bool ServerIsRunning()
    {
      using (var client = new TcpClient())
      {
        try
        {
          client.Connect(
            Config.ValidatorServerHost.Value,
            Config.ValidatorServerPort.Value
          );
          client.Close();
          return true;
        }
        catch (SocketException)
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Hosts the network validator server.
    /// Note: This is a blocking function, and needs to be run on a separate thread.
    /// </summary>
    public void StartServer()
    {
      var server = new TcpListener(IPAddress.Parse(Config.ValidatorServerHost.Value), Config.ValidatorServerPort.Value);
      server.Start();
      while (true)
      {
        try
        {
          server.AcceptTcpClient().Close();
        }
        catch (SocketException) { }
      }
    }
  }
}