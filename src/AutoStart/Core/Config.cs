using BepInEx;
using BepInEx.Configuration;

namespace AutoStart.Core
{
    static class Config
    {
        static readonly string[] SaveFiles = new string[]
        {
            "LCSaveFile1",
            "LCSaveFile2",
            "LCSaveFile3",
            "LCChallengeFile"
        };

        internal static class AutoStart
        {
            internal const string Section = "AutoStart";
            internal static ConfigEntry<bool> Enabled;
            internal static ConfigEntry<string> SaveFile;
        }

        internal static class AutoPullLever
        {
            internal const string Section = "AutoPullLever";
            internal static ConfigEntry<bool> Enabled;
            internal static ConfigEntry<int> PlayersRequired;
        }

        internal static void InitConfig(BaseUnityPlugin plugin)
        {
            AutoStart.Enabled = plugin.Config.Bind(
                AutoStart.Section,
                "Enabled",
                true,
                "Whether or not the plugin should be enabled."
            );
            AutoStart.SaveFile = plugin.Config.Bind(
                AutoStart.Section,
                "SaveFile",
                "LCSaveFile1",
                $"Possible vanilla values: {string.Join(", ", SaveFiles)}"
            );
            AutoPullLever.Enabled = plugin.Config.Bind(
                AutoPullLever.Section,
                "Enabled",
                true,
                "Whether or not the start lever should be automatically pulled."
            );
            AutoPullLever.PlayersRequired = plugin.Config.Bind(
                AutoPullLever.Section,
                "PlayersRequired",
                0,
                "Required number of players in the lobby for the lever to be pulled (excluding the host)."
            );
        }
    }
}