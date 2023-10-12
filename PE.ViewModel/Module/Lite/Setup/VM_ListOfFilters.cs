using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_ListOfFilters : VM_Base
  {
    public List<VM_Filters> ListOfFilters { get; set; }
    [SmfDisplay(typeof(VM_ListOfFilters), "SetupName", "NAME_SetupName")]
    public string SetupName { get; set; }
    [SmfDisplay(typeof(VM_ListOfFilters), "SetupCode", "NAME_SetupCode")]
    public string SetupCode { get; set; }
    [SmfDisplay(typeof(VM_ListOfFilters), "SetupTypeCode", "NAME_SetupTypeCode")]
    public string SetupTypeCode { get; set; }
    public long SetupId { get; set; }
    public long SetupType { get; set; }
    public VM_ListOfFilters()
    {
      ListOfFilters = new List<VM_Filters>();
    }
    public VM_ListOfFilters(List<VM_Filters> listOfFilters)
    {
      ListOfFilters = listOfFilters;
    }
  }
}
