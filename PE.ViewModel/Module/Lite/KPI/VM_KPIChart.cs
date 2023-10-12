using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.KPI
{
  public class VM_KPIChart : VM_Base
  {
    #region ctor

    public VM_KPIChart() { }

    #endregion

    #region props

    public List<VM_KPIOverview> KPIOverviews { get; set; } = new List<VM_KPIOverview>();

    #endregion
  }
}
