using System;
using System.Linq;

namespace PE.Helpers
{
  public static class UnitTestDetector
  {
    static UnitTestDetector()
    {
      string testAssemblyName = "Microsoft.TestPlatform";
      UnitTestDetector.IsInUnitTest = AppDomain.CurrentDomain.GetAssemblies()
          .Any(a => a.FullName.StartsWith(testAssemblyName));
    }

    public static bool IsInUnitTest { get; private set; }
  }
}
