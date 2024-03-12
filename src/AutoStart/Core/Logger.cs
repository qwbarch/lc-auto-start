using BepInEx.Logging;

namespace AutoStart.Core
{
    static class Logger
    {
        private static readonly ManualLogSource Source = BepInEx.Logging.Logger.CreateLogSource(Plugin.PluginId);

        internal static void LogInfo(string message)
        {
            Source.LogInfo(message);
        }

        internal static void LogDebug(string message)
        {
            Source.LogDebug(message);
        }

        internal static void LogWarning(string message)
        {
            Source.LogWarning(message);
        }

        internal static void LogError(string message)
        {
            Source.LogError(message);
        }
    }
}