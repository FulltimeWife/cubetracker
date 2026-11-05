using CubeTracker.Config;
using CubeTracker.UI;

namespace CubeTracker;

internal static class Program
{
  [STAThread]
  private static void Main()
  {
    ApplicationConfiguration.Initialize();
    var config = ConfigManager.Load();
    Application.Run(new OverlayForm(config));
  }
}