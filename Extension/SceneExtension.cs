using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace AutoStart.Extension
{
  public static class SceneExtension
  {
    public static async Task WaitUntilReady(this Scene scene)
    {
      while (!scene.isLoaded)
      {
        await Task.Yield();
      }
    }
  }
}