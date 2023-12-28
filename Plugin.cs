using AutoStart.Core;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AutoStart
{
  [BepInPlugin(PluginId, PluginName, PluginVersion)]
  public class Plugin : BaseUnityPlugin
  {
    public const string PluginId = "qwbarch.AutoStart";
    public const string PluginName = "AutoStart";
    public const string PluginVersion = "1.0.0";

    internal static new ManualLogSource Logger;

    private void Awake()
    {
      Logger = BepInEx.Logging.Logger.CreateLogSource(PluginId);
      var config = new Config(this);
      //new Harmony(PluginId).PatchAll(typeof());
    }
  }
}