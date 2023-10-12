using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupValidationResult : VM_Base
  {
    public bool IsCalculationValid { get; set; }
    public bool IsRollDiametersValid { get; set; }
    public List<string> MissedRollDiameters { get; set; }
  }
}
