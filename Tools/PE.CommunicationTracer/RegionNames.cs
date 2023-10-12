using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;

namespace PE.CommunicationTracer
{
  public class UIModule : IModule
  {
    public void OnInitialized(IContainerProvider containerProvider)
    {

    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }
  }
  public static class RegionNames
  {
    public const string TabRegion = "TabRegion";
    public const string ContentRegion = "ContentRegion";
  }
}
