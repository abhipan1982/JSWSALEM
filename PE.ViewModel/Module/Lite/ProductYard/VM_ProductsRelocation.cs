using System.Collections.Generic;

namespace PE.HMIWWW.ViewModel.Module.Lite.ProductYard
{
  public class VM_ProductsRelocation
  {
    public long TargetLocationId { get; set; }
    public List<VM_Products> Products { get; set; }
  }

  public class VM_Products
  {
    public long SourceLocationId { get; set; }
    public List<long> ProductsIds { get; set; }
  }
}
