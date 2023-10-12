using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.LabelPrinter
{
  public class VM_LabelRequest : VM_Base
  {
    public int ImageHeight { get; set; } = 350;
    public int ImageWidth { get; set; } = 250;
    public long? ProductId { get; set; }
    public long? RawMaterialId { get; set; }
  }
}
