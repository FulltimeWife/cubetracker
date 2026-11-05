using System.Text.Json.Serialization;

namespace CubeTracker.Config;

public sealed class AppConfig
{
  public DisplaySettings Display { get; set; } = new();

  [JsonPropertyName("log_path")]
  public string LogPath { get; set; } = @"C:\Program Files (x86)\Steam\steamapps\common\Torchlight Infinite\UE_game\TorchLight\Saved\Logs\UE_game.log";

  [JsonPropertyName("opacity")]
  public double Opacity { get; set; } = 0.85;

  [JsonPropertyName("poll_ms")]
  public int PollMs { get; set; } = 500;

  [JsonPropertyName("always_on_top")]
  public bool AlwaysOnTop { get; set; } = true;

  [JsonPropertyName("x")]
  public int X { get; set; } = 60;

  [JsonPropertyName("y")]
  public int Y { get; set; } = 60;
}
