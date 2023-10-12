using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Areas.MillConfigurator.ViewModels.MillConfiguration
{
  public class VM_FeatureValidator : VM_Base
  {
    #region ctor

    public VM_FeatureValidator() { }

    #endregion

    #region props

    public string ServerAddress { get; set; }
    public List<VM_FeatureInstance> AssignedFeatures { get; set; }
    public List<VM_FeatureInstance> UnassignedFeatures { get; set; }

    #endregion
  }
}
