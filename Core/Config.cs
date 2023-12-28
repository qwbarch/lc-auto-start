using System;
using BepInEx;
using BepInEx.Configuration;

namespace AutoStart.Core
{
  public class Config
  {
    public ConfigEntry<bool> AutoPullLever;
    public ConfigEntry<string> ValidatorServerHost;
    public ConfigEntry<int> ValidatorServerPort;

    public Config(BaseUnityPlugin plugin)
    {
      AutoPullLever = plugin.Config.Bind(
        "AutoStart",
        "AutoPullLever",
        false,
        "Automatically pull the lever when hosting games."
      );

      const string ValidatorSection = "NetworkValidator";
      ValidatorServerHost = plugin.Config.Bind(
        ValidatorSection,
        "Host",
        "127.0.0.1",
        "Server address to host the network validator on."
      );
      ValidatorServerPort = plugin.Config.Bind(
        ValidatorSection,
        "Port",
        7777,
        "Server port to host the network validator on."
      );
    }
  }
}