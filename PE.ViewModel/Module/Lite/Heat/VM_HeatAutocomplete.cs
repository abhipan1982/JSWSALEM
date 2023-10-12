using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
  public class VM_HeatAutocomplete : VM_Base
  {
    public long HeatId { get; set; }
    public string HeatName { get; set; }
    public string SteelgradeCode { get; set; }
    public string SteelgradeName { get; set; }
    public bool IsDummy { get; set; }
    public int MaterialsCount { get; set; }
    public int MaterialsAssigned { get; set; }

  }
}
