using CubeTracker.Utils;

namespace CubeTracker.Config;

public static class ConfigManager
{
  public static string AppDir { get; } = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
  public static string ConfigPath { get; } = Path.Combine(AppDir, "config.json");

  public static AppConfig Load()
  {
    var config = new AppConfig();
    var saved = JsonHelper.Load<AppConfig>(ConfigPath);
    if (saved != null)
    {
      if (!string.IsNullOrEmpty(saved.LogPath)) config.LogPath = saved.LogPath;
      if (saved.Opacity > 0) config.Opacity = saved.Opacity;
      if (saved.PollMs > 0) config.PollMs = saved.PollMs;
      if (saved.X != 0 || saved.Y != 0) { config.X = saved.X; config.Y = saved.Y; }
      if (saved.Display != null) config.Display = saved.Display;
      config.AlwaysOnTop = saved.AlwaysOnTop;
    }
    Save(config);
    return config;
  }

  public static void Save(AppConfig config) => JsonHelper.Save(ConfigPath, config);
}
