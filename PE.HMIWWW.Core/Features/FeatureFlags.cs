namespace PE.HMIWWW.Core.Features
{
  public static class FeatureFlags
  {
    // These flags are safe to access in any context
    public const string AlternativeColours = "AlternativeColours";

    // These flags are only safe to access from an HttpContext-safe request
    public static class Ui
    {
      private const string Prefix = "UI_";
      public const string Profiler = Prefix + "Profiler";
    }
  }
}
