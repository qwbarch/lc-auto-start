using AutoStart.Core;
using AutoStart.Patch;
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
    public const string PluginVersion = "1.0.1";

    public static Plugin Instance;

    public new ManualLogSource Logger;
    public new Config Config;
    public NetworkValidator NetworkValidator;

    private void Awake()
    {
      Instance = this;
      Logger = BepInEx.Logging.Logger.CreateLogSource(PluginId);
      Config = new Config(this);
      NetworkValidator = new NetworkValidator(Config);
      new Harmony(PluginId).PatchAll(typeof(StartGame));
    }
  }
}