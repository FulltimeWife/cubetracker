using CubeTracker.Config;
using CubeTracker.Core;
using CubeTracker.UI.Stats;

namespace CubeTracker.UI.Components;

public sealed class StatPanel
{
  private readonly List<(StatRow Row, Label Value)> _values = new();
  private Label? _status;

  public TableLayoutPanel Build(DisplaySettings display)
  {
    _values.Clear();
    var layout = UiFactory.CreateBaseLayout();

    foreach (var section in StatCatalog.Sections.Where(s => s.IsVisible(display)))
    {
      AddSpanning(layout, UiFactory.CreateHeader(section.Header));

      foreach (var row in section.Rows.Where(r => r.IsVisible?.Invoke(display) ?? true))
        AddRow(layout, row);
    }

    _status = UiFactory.CreateStatusLabel();
    AddSpanning(layout, _status);

    return layout;
  }

  public void Update(CubeState state, string statusMessage)
  {
    foreach (var (row, label) in _values) label.Text = row.Format(state);
    if (_status != null) _status.Text = statusMessage;
  }

  private void AddRow(TableLayoutPanel layout, StatRow row)
  {
    var (nameLbl, valueLbl) = UiFactory.CreateRow(row.Label, row.LabelColor ?? Theme.TextColor);
    layout.Controls.Add(nameLbl);
    layout.Controls.Add(valueLbl);
    _values.Add((row, valueLbl));
  }

  private static void AddSpanning(TableLayoutPanel layout, Control control)
  {
    layout.Controls.Add(control);
    layout.SetColumnSpan(control, 2);
  }


}