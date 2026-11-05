using CubeTracker.Core;

namespace CubeTracker.UI.Stats;

public static class StatCatalog
{
  public static readonly IReadOnlyList<StatSection> Sections = new[]
  {
    new StatSection("Greed Stats", d => d.ShowGreeds, new[]
    {
      new StatRow("Single Greed", s => s.SingleGreed.ToString()),
      new StatRow("Double Greed", s => s.DoubleGreed.ToString()),
      new StatRow("Total Greeds", s => s.GreedSuccess.ToString()),
      new StatRow("Greeds Failed", s => s.GreedFail.ToString())
    }),

    new StatSection("Loot Stats", d => d.ShowLoot, new[]
    {
      new StatRow("R, SR", s => s.RSr.ToString(), d => d.ShowRSr),
      new StatRow("SSR, UR", s => s.Ssr.ToString(), d => d.ShowSsrUr),
      new StatRow("Cravings", s => s.Craving.ToString(), d => d.ShowCravings),
      new StatRow("Special Event", s => s.SpecialEvent.ToString(), d => d.ShowSpecial)
    }),

    new StatSection("Misc", _ => true, new[]
    {
      new StatRow("Cubes Done", s => s.CubesDone.ToString()),
      new StatRow("Pet Procs", s => s.PetProc.ToString())
    }),

    new StatSection("Averages", d => d.ShowStats, new[]
    {
      new StatRow("Greed Success Rate", StatsCalculator.GreedSuccessRate, LabelColor: Theme.GoldColor)
    })
  };
}