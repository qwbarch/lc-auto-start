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
      var scene = SceneManager.LoadScene("MainMenu", new LoadSceneParameters(LoadSceneMode.Single));
      await scene.WaitUntilReady();
      var menuManager = UnityEngine.Object.FindObjectOfType<MenuManager>();
      if (Plugin.Instance.NetworkValidator.ServerIsRunning())
      {
        Plugin.Instance.Logger.LogInfo("Server already exists. Joining the game...");
        menuManager.StartAClient();
      }
      else
      {
        Plugin.Instance.Logger.LogInfo("Server not found. Hosting the game...");
        new Thread(Plugin.Instance.NetworkValidator.StartServer).Start();
        menuManager.ConfirmHostButton();
      }
    }
  }
}