using System.Text.RegularExpressions;
using CubeTracker.Utils;
using CubeTracker.Config;

namespace CubeTracker.Core;

public sealed class CubertTracker
{
  private readonly CubeState _state;
  private long _pos;
  private readonly string _statePath;
  private int _currentGreedCount = 0;

  private static readonly Regex GreedSuccessRegex = new(@"^\[(\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}:\d{3})\].*GreedAnim Start", RegexOptions.Compiled);
  private static readonly Regex GreedFailRegex = new(@"^\[(\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}:\d{3})\].*BrokenAnim Start", RegexOptions.Compiled);
  private static readonly Regex AudioRegex = new(@"^\[(\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}:\d{3})\].*AudioEnum\s+==\s+(\w+)", RegexOptions.Compiled);
  private static readonly Regex CubeDestroyRegex = new(@"^\[(\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}:\d{3})\].*S3GamePlayMagicCube Destory", RegexOptions.Compiled);

  public CubeState State => _state;

  public CubertTracker()
  {
    _statePath = Path.Combine(ConfigManager.AppDir, "cube_counts.json");
    _state = JsonHelper.Load<CubeState>(_statePath) ?? new CubeState();
    _pos = _state.LastLogPosition;
  }

  public void Save() => JsonHelper.Save(_statePath, _state);

  public void Reset()
  {
    _state.SingleGreed = 0;
    _state.DoubleGreed = 0;
    _state.GreedFail = 0;
    _state.SpecialEvent = 0;
    _state.RSr = 0;
    _state.Ssr = 0;
    _state.Craving = 0;
    _state.PetProc = 0;
    _state.CubesDone = 0;
    _currentGreedCount = 0;
    Save();
  }

  public void ResetPosition()
  {
    _pos = 0;
    _state.LastLogPosition = 0;
    Save();
  }

  public bool Poll(string logPath)
  {
    try
    {
      long size = new FileInfo(logPath).Length;

      if (size < _pos)
      {
        _pos = 0;
      }

      if (size == _pos) return false;

      using var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
      fs.Seek(_pos, SeekOrigin.Begin);
      using var ms = new MemoryStream();
      fs.CopyTo(ms);
      var data = ms.ToArray();

      var lastNl = Array.LastIndexOf(data, (byte)'\n');
      if (lastNl < 0) return false;

      var consumed = data.AsSpan(0, lastNl + 1);
      _pos += consumed.Length;
      _state.LastLogPosition = _pos;

      var text = System.Text.Encoding.UTF8.GetString(consumed);
      bool changed = false;

      foreach (var rawLine in text.Split('\n'))
      {
        var line = rawLine.TrimEnd('\r');
        if (line.Length == 0) continue;

        var successM = GreedSuccessRegex.Match(line);
        if (successM.Success)
        {
          _state.GreedSuccess++;
          _currentGreedCount++;
          _state.LastGreedSuccessTs = successM.Groups[1].Value;
          changed = true;
          continue;
        }

        var failM = GreedFailRegex.Match(line);
        if (failM.Success)
        {
          _state.GreedFail++;
          changed = true;
          continue;
        }

        var audioM = AudioRegex.Match(line);
        if (audioM.Success)
        {
          var ts = audioM.Groups[1].Value;
          var enumName = audioM.Groups[2].Value;

          switch (enumName)
          {
            case "S3_RandomCube_Eye":
              _state.SpecialEvent++;
              changed = true;
              break;
            case "S3_Award_Ordinary":
              _state.RSr++;
              changed = true;
              break;
            case "S3_Award_SSR":
              _state.Ssr++;
              _state.LastSsrTs = ts;
              changed = true;
              break;
            case "S3_Award_GoldCube":
              _state.Craving++;
              _state.LastCravingTs = ts;
              changed = true;
              break;
            case "S3_Pet_Light":
              _state.PetProc++;
              changed = true;
              break;
          }
        }

        var destroyM = CubeDestroyRegex.Match(line);
        if (destroyM.Success)
        {
          _state.CubesDone++;
          _state.LastCubeDoneTs = destroyM.Groups[1].Value;

          if (_currentGreedCount == 1) _state.SingleGreed++;
          if (_currentGreedCount == 2)
          {
            _state.SingleGreed++;
            _state.DoubleGreed++;
          }
          _currentGreedCount = 0;
          changed = true;
          continue;
        }
      }
      Save();

      return changed;
    }
    catch
    {
      return false;
    }
  }
}