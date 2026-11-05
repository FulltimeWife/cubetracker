using System.Text.Json.Serialization;

namespace CubeTracker.Config;

public enum ViewMode
{
  All = 0,
  GreedsOnly = 1,
  LootOnly = 2
}

public sealed class DisplaySettings
{
  [JsonPropertyName("view_mode")]
  public ViewMode ViewMode { get; set; } = ViewMode.All;

  [JsonPropertyName("show_rsr")]
  public bool ShowRSr { get; set; } = true;

  [JsonPropertyName("show_ssr_ur")]
  public bool ShowSsrUr { get; set; } = true;

  [JsonPropertyName("show_cravings")]
  public bool ShowCravings { get; set; } = true;

  [JsonPropertyName("show_special")]
  public bool ShowSpecial { get; set; } = true;

  [JsonPropertyName("show_stats")]
  public bool ShowStats { get; set; } = true;

  [JsonIgnore]
  public bool ShowGreeds => ViewMode is ViewMode.All or ViewMode.GreedsOnly;

  [JsonIgnore]
  public bool ShowLoot => ViewMode is ViewMode.All or ViewMode.LootOnly;
}