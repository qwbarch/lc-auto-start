using HarmonyLib;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using GameNetcodeStuff;
using System.Threading.Tasks;
using AutoStart.Core;
using AutoStart.Core.Lobby;

namespace AutoStart.Patch
{
    static class StartGame
    {
        static int ConnectedPlayers = 0;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.Start))]
        static async void StartGameWhenReady()
        {
            var scene = SceneManager.GetSceneByName("MainMenu");
            while (!SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                await Task.Yield();
            }
            var menuManager = UnityEngine.Object.FindObjectOfType<MenuManager>();
            if (LobbyHost.IsHosting)
            {
                LockFile.WithLock(() => GameNetworkManager.Instance.currentSaveFileName = Config.AutoStart.SaveFile.Value);
                menuManager.ConfirmHostButton();
            }
            else
            {
                LobbyParticipant.JoinLobby(menuManager).Forget();
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.ConnectClientToPlayerObject))]
        static void SetLobbyHostReady(PlayerControllerB __instance)
        {
            if (__instance.IsHost && Config.AutoPullLever.Enabled.Value)
            {
                if (Config.AutoPullLever.PlayersRequired.Value == 0) StartOfRound.Instance.StartGame();
                else LobbyHost.SetLobbyReady().Forget();
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.OnClientConnect))]
        static void IncrementPlayerCount(StartOfRound __instance)
        {
            if (__instance.IsHost && Config.AutoPullLever.Enabled.Value)
            {
                ConnectedPlayers++;
                if (ConnectedPlayers == Config.AutoPullLever.PlayersRequired.Value)
                {
                    __instance.StartGame();
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.OnClientDisconnect))]
        static void DecrementPlayerCount(StartOfRound __instance)
        {
            if (__instance.IsHost && Config.AutoPullLever.Enabled.Value) ConnectedPlayers--;
        }
    }
}