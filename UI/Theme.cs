namespace CubeTracker.UI;

public static class Theme
{
  // Colours
  public static readonly Color BgColor = ColorTranslator.FromHtml("#14141c");
  public static readonly Color GoldColor = ColorTranslator.FromHtml("#e8b84a");
  public static readonly Color FailColor = ColorTranslator.FromHtml("#950606");
  public static readonly Color TextColor = ColorTranslator.FromHtml("#d8d8e0");
  public static readonly Color ValueColor = Color.White;
  public static readonly Color ErrorColor = ColorTranslator.FromHtml("#ff7a7a");
  public static readonly Color DividerColor = ColorTranslator.FromHtml("#3a3a4a");

  private const string FontFamily = "Segoe UI";
  public static readonly Font HeaderFont = new(FontFamily, 12, FontStyle.Bold);
  public static readonly Font LabelFont = new(FontFamily, 10);
  public static readonly Font ValueFont = new(FontFamily, 11, FontStyle.Bold);
  public static readonly Font StatusFont = new(FontFamily, 8);
}
