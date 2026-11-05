using CubeTracker.Config;

namespace CubeTracker.UI.Menus;

public sealed record OverlayMenuActions(
  Action DisplayChanged,
  Action<bool> AlwaysOnTopChanged,
  Action ResetCounts,
  Action ChooseLog,
  Action Quit
  );

public static class OverlayContextMenu
{
  public static ContextMenuStrip Create(DisplaySettings display, bool AlwaysOnTop, OverlayMenuActions actions)
  {
    var menu = new ContextMenuStrip();

    menu.Items.Add(CreateViewModeMenu(display, actions.DisplayChanged));
    menu.Items.Add(CreateLootMenu(display, actions.DisplayChanged));
    menu.Items.Add(CreateStatsMenu(display, actions.DisplayChanged));

    menu.Items.Add(new ToolStripSeparator());

    var top = new ToolStripMenuItem("Always on top") { CheckOnClick = true, Checked = AlwaysOnTop };
    top.CheckedChanged += (_, _) => actions.AlwaysOnTopChanged(top.Checked);
    menu.Items.Add(top);
    //menu.Items.Add("Reset Counts", null, (_, _) => actions.ResetCounts());
    menu.Items.Add("Choose log file", null, (_, _) => actions.ChooseLog());
    menu.Items.Add(new ToolStripSeparator());
    menu.Items.Add("Quit", null, (_, _) => actions.Quit());

    return menu;
  }

  private static ToolStripMenuItem CreateViewModeMenu(DisplaySettings display, Action changed)
  {
    var root = new ToolStripMenuItem("View Layout");
    var items = new List<ToolStripMenuItem>();

    foreach (var (text, mode) in new[]
    {
      ("Show All Stats", ViewMode.All),
      ("Greeds Only", ViewMode.GreedsOnly),
      ("Loot Only", ViewMode.LootOnly)
    })
    {
      var item = new ToolStripMenuItem(text) { Checked = display.ViewMode == mode };
      item.Click += (_, _) =>
      {
        display.ViewMode = mode;
        foreach (var other in items) other.Checked = ReferenceEquals(other, item);
        changed();
      };
      items.Add(item);
      root.DropDownItems.Add(item);
    }

    return root;
  }

  private static ToolStripMenuItem CreateLootMenu(DisplaySettings display, Action changed)
  {
    var root = new ToolStripMenuItem("Loot Visibility");
    root.DropDownItems.Add(CreateToggle("Show R & SR", () => display.ShowRSr, v => display.ShowRSr = v, changed));
    root.DropDownItems.Add(CreateToggle("Show SSR & UR", () => display.ShowSsrUr, v => display.ShowSsrUr = v, changed));
    root.DropDownItems.Add(CreateToggle("Show Special Event", () => display.ShowSpecial, v => display.ShowSpecial = v, changed));
    root.DropDownItems.Add(CreateToggle("Show Cravings", () => display.ShowCravings, v => display.ShowCravings = v, changed));
    return root;
  }

  private static ToolStripMenuItem CreateStatsMenu(DisplaySettings display, Action changed)
  {
    var root = new ToolStripMenuItem("Statistics Visibility");
    root.DropDownItems.Add(CreateToggle("Show Averages", () => display.ShowStats, v => display.ShowStats = v, changed));
    return root;
  }

  private static ToolStripMenuItem CreateToggle(string text, Func<bool> get, Action<bool> set, Action changed)
  {
    var item = new ToolStripMenuItem(text) { CheckOnClick = true, Checked = get() };
    item.CheckedChanged += (_, _) =>
    {
      set(item.Checked);
      changed();
    };
    return item;
  }
}