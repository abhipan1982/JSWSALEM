using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_ProductYard : VM_Base
  {
    public long? ProductYardId { get; set; }

    [SmfDisplay(typeof(VM_ProductYard), "YardName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string YardName { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int NumberOfProducts { get; set; }

    public int WeightOfProducts { get; set; }

    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public int FreeSpace { get; set; }
  }
}
