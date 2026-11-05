using CubeTracker.Config;
using CubeTracker.Core;
using CubeTracker.UI.Behaviours;
using CubeTracker.UI.Components;
using CubeTracker.UI.Menus;

namespace CubeTracker.UI;

public sealed class OverlayForm : Form
{
  private const string LogMissingMessage = "Log not found - right-click to set";

  private readonly AppConfig _config;
  private readonly CubertTracker _tracker = new();
  private readonly StatPanel _statPanel = new();
  private readonly DragHandler _drag;
  private readonly System.Windows.Forms.Timer _timer;

  private Control? _content;
  private string _logPath;

  public OverlayForm(AppConfig config)
  {
    _config = config;
    _logPath = LogLocator.Resolve(config.LogPath);

    ConfigureWindow();

    _drag = new DragHandler(this);
    _drag.DragCompleted += OnDragCompleted;

    ContextMenuStrip = OverlayContextMenu.Create(config.Display, config.AlwaysOnTop, new OverlayMenuActions(
      DisplayChanged: OnDisplayChanged,
      AlwaysOnTopChanged: OnTopMostChanged,
      ResetCounts: ResetCounts,
      ChooseLog: ChooseLogFile,
      Quit: Close));

    RebuildContent();

    _timer = new System.Windows.Forms.Timer { Interval = Math.Max(50, config.PollMs) };
    _timer.Tick += (_, _) => Poll();
    _timer.Start();
  }

  private void ConfigureWindow()
  {
    AutoScaleMode = AutoScaleMode.Dpi;
    AutoSize = true;
    AutoSizeMode = AutoSizeMode.GrowAndShrink;
    FormBorderStyle = FormBorderStyle.None;
    BackColor = Theme.BgColor;
    StartPosition = FormStartPosition.Manual;
    Location = new Point(_config.X, _config.Y);
    ShowInTaskbar = true;
    TopMost = _config.AlwaysOnTop;
    Text = "Cube Tracker";
  }

  protected override void OnFormClosed(FormClosedEventArgs e)
  {
    _timer.Stop();
    _timer.Dispose();
    base.OnFormClosed(e);
  }

  // --- UI ---

  private void RebuildContent()
  {
    SuspendLayout();

    if (_content != null)
    {
      Controls.Remove(_content);
      _content.Dispose();
    }

    _content = _statPanel.Build(_config.Display);
    _drag.Attach(_content);
    Controls.Add(_content);

    ResumeLayout(true);
    RefreshValues();
  }

  private void RefreshValues() =>
    _statPanel.Update(_tracker.State, string.IsNullOrEmpty(_logPath) ? LogMissingMessage : "");

  // --- Events from child components ---

  private void OnDragCompleted(Point location)
  {
    _config.X = location.X;
    _config.Y = location.Y;
    ConfigManager.Save(_config);
  }

  private void OnDisplayChanged()
  {
    ConfigManager.Save(_config);
    RebuildContent();
  }

  private void OnTopMostChanged(bool value)
  {
    _config.AlwaysOnTop = value;
    TopMost = value;
    ConfigManager.Save(_config);
  }

  private void ResetCounts()
  {
    _tracker.Reset();
    RefreshValues();
  }

  // --- Log handling ---

  private void ChooseLogFile()
  {
    using var dlg = new OpenFileDialog { Filter = "Log files (*.log)|*.log|All files (*.*)|*.*" };
    if (dlg.ShowDialog(this) != DialogResult.OK) return;

    _config.LogPath = dlg.FileName;
    ConfigManager.Save(_config);
    _logPath = dlg.FileName;
    _tracker.ResetPosition();
    RefreshValues();
  }

  private void Poll()
  {
    if (string.IsNullOrEmpty(_logPath)) _logPath = LogLocator.Resolve(_config.LogPath);
    if (!string.IsNullOrEmpty(_logPath) && _tracker.Poll(_logPath))
      RefreshValues();
  }
}
