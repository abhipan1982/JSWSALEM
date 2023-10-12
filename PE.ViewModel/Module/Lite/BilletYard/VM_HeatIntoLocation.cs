using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.BilletYard
{
  public class VM_HeatIntoLocation : VM_Base
  {
    public VM_HeatIntoLocation()
    {
    }

    public VM_HeatIntoLocation(long heatId, long locationId, int maxMatNumber, long sourceYardId,
      long? sourceLocationId)
    {
      HeatId = heatId;
      LocationId = locationId;
      MaxMatNumber = maxMatNumber;
      MaterialsNumber = maxMatNumber;
      SourceYardId = sourceYardId;
      SourceLocationId = sourceLocationId;
    }

    public long HeatId { get; set; }
    public long? SourceLocationId { get; set; }
    public long SourceYardId { get; set; }

    [SmfDisplay(typeof(VM_HeatIntoLocation), "MaterialNumber", "NAME_MaterialsNumber")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public int MaterialsNumber { get; set; }

    public long LocationId { get; set; }
    public int MaxMatNumber { get; set; }
  }
}
