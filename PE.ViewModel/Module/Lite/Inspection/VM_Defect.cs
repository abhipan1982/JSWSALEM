using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Inspection
{
  public class VM_Defect : VM_Base
  {
    public VM_Defect()
    {
    }

    public VM_Defect(V_DefectsSummary defect)
    {
      DefectId = defect.DefectId;
      DefectName = defect.DefectName;
      DefectCatalogueId = defect.DefectCatalogueId;
      DefectCatalogueName = defect.DefectCatalogueName;
      AssetId = defect.AssetId;
      AssetName = defect.AssetName;
      RawMaterialId = defect.RawMaterialId;
      RawMaterialName = defect.RawMaterialName;
      DefectPosition = defect.DefectPosition;
      DefectDescription = defect.DefectDescription;
    }

    public VM_Defect(QTYDefect defect)
    {
      DefectId = defect.DefectId;
      DefectName = defect.DefectName;
      DefectCatalogueId = defect.FKDefectCatalogueId;
      DefectCatalogueName = defect.FKDefectCatalogue?.DefectCatalogueName;
      AssetId = defect.FKAsset?.AssetId;
      AssetName = defect.FKAsset?.AssetName;
      RawMaterialId = defect.FKRawMaterialId;
      RawMaterialName = defect.FKRawMaterial?.RawMaterialName;
      DefectPosition = defect.DefectPosition;
      DefectDescription = defect.DefectDescription;
    }

    public long? DefectId { get; set; }
    public string DefectName { get; set; }

    [SmfDisplay(typeof(VM_Defect), "DefectCatalogueId", "NAME_DefectCatalogue")]
    public long? DefectCatalogueId { get; set; }

    public string DefectCatalogueName { get; set; }
    public string AssetName { get; set; }

    [SmfDisplay(typeof(VM_Defect), "AssetId", "NAME_Asset")]
    public long? AssetId { get; set; }

    public long? RawMaterialId { get; set; }
    public string RawMaterialName { get; set; }

    [SmfDisplay(typeof(VM_Defect), "DefectPosition", "NAME_DefectPosition")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public short? DefectPosition { get; set; }

    [SmfDisplay(typeof(VM_Defect), "DefectDescription", "NAME_DefectDescription")]
    public string DefectDescription { get; set; }
  }
}
