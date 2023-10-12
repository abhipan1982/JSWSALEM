using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using PE.BaseDbEntity.EnumClasses;
using PE.Common;
using PE.DbEntity.EnumClasses;

namespace HMIRefreshKeysHelper
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      EnumInitializator.Init();
      string refreshKeys = "var HmiRefreshKeys = " + GetRefreshKeyClasses() + ";\r\n";
      string trackingAreaKeys = "var TrackingAreaKeys = " + GetTrackingAreaClasses() + ";\r\n";
      OverwriteRefreshKeysFile(refreshKeys + trackingAreaKeys);
    }

    public static string TryGetSolutionDirectoryInfo(string currentPath = null)
    {
      DirectoryInfo directory = new DirectoryInfo(
        currentPath ?? Directory.GetCurrentDirectory());
      while (directory != null && !directory.GetFiles("*.sln").Any())
      {
        directory = directory.Parent;
      }

      FileInfo fileInfo = directory.GetFiles("*pe_lite_dev.sln").FirstOrDefault();
      return fileInfo != null ? fileInfo.FullName : null;
    }

    private static string GetRefreshKeyClasses()
    {
      Assembly assembly = Assembly.GetAssembly(typeof(HMIRefreshKeys));
      Dictionary<string, object> constants = typeof(HMIRefreshKeys).GetFields().OrderBy(x => x.Name)
        .ToDictionary(x => x.Name, x => x.GetValue(null));
      var options = new JsonSerializerOptions { WriteIndented = true };
      return JsonSerializer.Serialize(constants, options);
    }

    private static string GetTrackingAreaClasses() 
    {
      Dictionary<string, int> constants = new Dictionary<string, int>();
      foreach (var propertyItem in typeof(TrackingArea).GetFields(BindingFlags.Public |
        BindingFlags.Static))
      {
        dynamic item = propertyItem.GetValue(null);

        constants.Add(item.Name, (int)item.Value);
      }
      var options = new JsonSerializerOptions { WriteIndented = true };
      return JsonSerializer.Serialize(constants, options);
    }

    private static void OverwriteRefreshKeysFile(string line)
    {
      DirectoryInfo di = new DirectoryInfo(Environment.GetCommandLineArgs()[0]);
      string path = di.Parent.Parent.Parent.Parent.FullName;
      File.WriteAllText(path + @"\PE.HMIWWW\wwwroot\js\contents.js", line);
    }
  }
}
