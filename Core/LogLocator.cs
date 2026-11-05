namespace CubeTracker.Core;

public static class LogLocator
{
  public static string Resolve(string? configuredPath) => !string.IsNullOrEmpty(configuredPath) && File.Exists(configuredPath) ? configuredPath : "";
}