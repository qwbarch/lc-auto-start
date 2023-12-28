using System;
using BepInEx;
using BepInEx.Configuration;

namespace AutoStart.Core
{
  class Config
  {
    public ConfigEntry<string> ValidatorServerHost;
    public ConfigEntry<int> ValidatorServerPort;

    public Config(BaseUnityPlugin plugin)
    {
      const string ServerSection = "Network Validator";
      ValidatorServerHost = plugin.Config.Bind(
        ServerSection,
        "Host",
        "127.0.0.1",
        "Server address to host the network validator on."
      );
      ValidatorServerPort = plugin.Config.Bind(
        ServerSection,
        "Port",
        7777,
        "Server port to host the network validator on."
      );
    }
  }
}