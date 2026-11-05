namespace CubeTracker.Core;

public static class StatsCalculator
{
  public static string GreedSuccessRate(CubeState s)
  {
    int totalAttempts = s.GreedSuccess + s.GreedFail;
    if (totalAttempts == 0) return "0%";
    double rate = (double)s.GreedSuccess / totalAttempts * 100;
    return $"{rate:F0}%";
  }
}