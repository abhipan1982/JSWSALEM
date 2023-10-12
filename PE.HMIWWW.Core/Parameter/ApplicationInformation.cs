using System;
using System.Diagnostics;
using System.Reflection;

namespace PE.HMIWWW.Core.Parameter
{
  public static class ApplicationInformation
  {
    private static DateTime? _compileDate;

    /// <summary>
    ///   Gets the compile date of the currently executing assembly.
    /// </summary>
    public static DateTime CompileDate
    {
      get
      {
        if (!_compileDate.HasValue)
        {
          DateTime parsedCompileDate = DateTime.Now;

          _compileDate =
            DateTime.TryParse(FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).LegalCopyright,
              out parsedCompileDate)
              ? parsedCompileDate
              : (DateTime?)null;
        }

        return _compileDate ?? new DateTime();
      }
    }
  }
}
