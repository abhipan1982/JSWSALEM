using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration
{
  public class VM_FeaturesToPdf : VM_Base
  {
    public VM_FeaturesToPdf() { }

    public VM_FeaturesToPdf(List<VM_FeatureInstance> features, List<VM_AssetTree> assets, string projectName)
    {
      Features = features;
      ProjectName = projectName;
      FileName = $"{projectName}-PE-T41-L1InterfaceDetails.pdf";
      Assets = assets;
    }

    public List<VM_FeatureInstance> Features { get; set; }

    public List<VM_AssetTree> Assets { get; set; }

    public string ProjectName { get; set; }

    public string FileName { get; set; }
  }
}
