namespace CubeTracker.UI.Components;

public static class UiFactory
{
  public static TableLayoutPanel CreateBaseLayout()
  {
    var layout = new TableLayoutPanel
    {
      Dock = DockStyle.Fill,
      AutoSize = true,
      AutoSizeMode = AutoSizeMode.GrowAndShrink,
      ColumnCount = 2,
      Padding = new Padding(12, 8, 12, 8),
      BackColor = Theme.BgColor,
    };
    layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
    layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    return layout;
  }

  public static Label CreateHeader(string text) => new()
  {
    Text = text,
    ForeColor = Theme.GoldColor,
    Font = Theme.HeaderFont,
    AutoSize = true,
    Margin = new Padding(0, 4, 0, 2),
  };

  public static (Label name, Label value) CreateRow(string displayName, Color textColor)
  {
    var nameLbl = new Label
    {
      Text = displayName + ":",
      ForeColor = textColor,
      Font = Theme.LabelFont,
      AutoSize = true,
      Anchor = AnchorStyles.Left,
      Margin = new Padding(0, 1, 0, 1),
    };
    var valueLbl = new Label
    {
      Text = "0",
      ForeColor = Theme.ValueColor,
      Font = Theme.ValueFont,
      AutoSize = false,
      Width = 48,
      TextAlign = ContentAlignment.MiddleRight,
      Anchor = AnchorStyles.Right,
      Margin = new Padding(0, 1, 0, 1),
    };
    return (nameLbl, valueLbl);
  }

  public static Label CreateStatusLabel() => new()
  {
    Text = "",
    ForeColor = Theme.ErrorColor,
    Font = Theme.StatusFont,
    AutoSize = true,
    Margin = new Padding(0, 2, 0, 0),
  };
}
