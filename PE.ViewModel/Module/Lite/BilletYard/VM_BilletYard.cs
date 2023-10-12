using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_BilletYard : VM_Base
  {
    public long? BilletYardId { get; set; }

    [SmfDisplay(typeof(VM_BilletYard), "YardName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string YardName { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int NumberOfMaterials { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FreeSpace { get; set; }

    public bool IsReception { get; set; }
    public bool IsChargingGrid { get; set; }
    public bool IsScrapped { get; set; }
    public bool IsYard { get; set; }
  }
}
