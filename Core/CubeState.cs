using System.Text.Json.Serialization;

namespace CubeTracker.Core;

public sealed class CubeState
{
  [JsonPropertyName("greed_success")]
  public int GreedSuccess { get; set; }

  [JsonPropertyName("single_greed")]
  public int SingleGreed { get; set; }

  [JsonPropertyName("double_greed")]
  public int DoubleGreed { get; set; }

  [JsonPropertyName("greed_fail")]
  public int GreedFail { get; set; }

  [JsonPropertyName("special_event")]
  public int SpecialEvent { get; set; }

  [JsonPropertyName("r_sr")]
  public int RSr { get; set; }

  [JsonPropertyName("ssr")]
  public int Ssr { get; set; }

  [JsonPropertyName("craving")]
  public int Craving { get; set; }

  [JsonPropertyName("pet_proc")]
  public int PetProc { get; set; }

  [JsonPropertyName("cubes_done")]
  public int CubesDone { get; set; }

  [JsonPropertyName("last_log_position")]
  public long LastLogPosition { get; set; }

  // Background timestamps for future stats
  [JsonPropertyName("last_greed_success_ts")]
  public string LastGreedSuccessTs { get; set; } = "";

  [JsonPropertyName("last_ssr_ts")]
  public string LastSsrTs { get; set; } = "";

  [JsonPropertyName("last_craving_ts")]
  public string LastCravingTs { get; set; } = "";

  [JsonPropertyName("last_cube_done_ts")]
  public string LastCubeDoneTs { get; set; } = "";



}