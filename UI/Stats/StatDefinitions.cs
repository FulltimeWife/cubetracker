using CubeTracker.Config;
using CubeTracker.Core;

namespace CubeTracker.UI.Stats;

public sealed record StatRow(
  string Label,
  Func<CubeState, string> Format,
  Func<DisplaySettings, bool>? IsVisible = null,
  Color? LabelColor = null);

public sealed record StatSection(
  string Header,
  Func<DisplaySettings, bool> IsVisible,
  IReadOnlyList<StatRow> Rows);