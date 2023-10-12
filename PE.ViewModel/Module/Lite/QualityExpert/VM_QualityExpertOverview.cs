using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.QualityExpert
{
  public class VM_QualityExpertOverview : VM_Base
  {
    public VM_QualityExpertSlimRawMaterial HeaderRawMaterial { get; set; }

    public VM_QualityExpertSlimRawMaterial CurrentRawMaterial { get; set; }

    public List<VM_QualityExpertSlimRawMaterial> RawMaterials { get; set; }

    public int ChildMaterials { get; set; }

    public bool EnableNavigation { get; set; }
  }

  public class VM_QualityExpertSlimRawMaterial
  {
    public VM_QualityExpertSlimRawMaterial(TRKRawMaterial data)
    {
      RawMaterialId = data.RawMaterialId;
      RawMaterialName = data.RawMaterialName;
      LastGrading = data.LastGrading;
      CuttingSeqNo = data.CuttingSeqNo;
    }

    public VM_QualityExpertSlimRawMaterial() { }

    public long RawMaterialId { get; set; }

    public short CuttingSeqNo { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "RawMaterialName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "LastGrading", "NAME_CurrentGradingValue")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? LastGrading { get; set; }

    [SmfDisplay(typeof(VM_QualityExpertRawMaterial), "HeatName", "NAME_HeatName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string HeatName { get; set; }
  }
}
