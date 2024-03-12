using AutoStart.Core;
using AutoStart.Core.Lobby;
using AutoStart.Patch;
using BepInEx;
using Cysharp.Threading.Tasks;
using FastStartup.Config;
using HarmonyLib;

namespace AutoStart
{
    [BepInPlugin(PluginId, PluginName, PluginVersion)]
    [BepInDependency(FastStartup.Plugin.ModId)]
    class Plugin : BaseUnityPlugin
    {
        internal const string PluginId = "qwbarch.AutoStart";
        internal const string PluginName = "AutoStart";
        internal const string PluginVersion = "1.1.1";

        void Awake()
        {
            Core.Logger.LogInfo("Loading AutoStart.");
            LockFile.WithLock(() =>
            {
                FastStartup.Plugin.Config.AutoLaunchMode.SetSerializedValue(LaunchMode.Lan.ToString());
                Core.Config.InitConfig(this);
            });
            if (Core.Config.AutoStart.Enabled.Value)
            {
                LobbyHost.Start().Forget();
                var harmony = new Harmony(PluginId);
                harmony.PatchAll(typeof(StartGame));
            }
            else
            {
                Core.Logger.LogInfo("AutoStart is disabled.");
            }
        }
    }
}