using System.Threading;
using AutoStart.Extension;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace AutoStart.Patch
{
  class StartGame
  {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PreInitSceneScript), nameof(PreInitSceneScript.Start))]
    private static void StartLANMode()
    {
      SceneManager.LoadScene("InitSceneLANMode");
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(InitializeGame), nameof(InitializeGame.Start))]
    private static async void StartWhenReady()
    {
      await
        SceneManager
          .LoadScene("MainMenu", new LoadSceneParameters(LoadSceneMode.Single))
          .WaitUntilReady();
      var menuManager = UnityEngine.Object.FindObjectOfType<MenuManager>();
      if (Plugin.Instance.NetworkValidator.ServerIsRunning())
      {
        Plugin.Instance.Logger.LogInfo("Server already exists. Joining the existing game.");
        menuManager.StartAClient();
      }
      else
      {
        Plugin.Instance.Logger.LogInfo("Server not found. Hosting a LAN game.");
        new Thread(Plugin.Instance.NetworkValidator.StartServer).Start();
        menuManager.ConfirmHostButton();
      }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Start))]
    private static void AutoPullLever()
    {
      if (Plugin.Instance.NetworkValidator.IsServer && Plugin.Instance.Config.AutoPullLever.Value)
      {
        Plugin.Instance.Logger.LogInfo("AutoPullLever is enabled. Pulling the lever.");
        StartOfRound.Instance.StartGame();
      }
    }
  }
}