using System;

namespace PE.DbEntity.SPModels
{
  public class SPL3L1MaterialAssignment
  {
    public long OrderSeq { get; set; }
    public long MaterialId { get; set; }
    public string MaterialName { get; set; }
    public short MaterialSeqNo { get; set; }
    public DateTime MaterialCreatedTs { get; set; }
    public long HeatId { get; set; }
    public long? WorkOrderId { get; set; }
    public double? MaterialWeight { get; set; }
    public bool? IsAssigned { get; set; }
    public bool? IsDummy { get; set; }
    public string DisplayedMaterialName { get; set; }
    public long? RawMaterialId { get; set; }
    public string RawMaterialName { get; set; }
    public DateTime? RawMaterialCreatedTs { get; set; }
    public short? EnumRawMaterialStatus { get; set; }
    public double? LastWeight { get; set; }
    public long? ParentRawMaterialId { get; set; }
    public short? ChildsNo { get; set; }
    public short? CuttingSeqNo { get; set; }
    public short? EnumRejectLocation { get; set; }
    public short? EnumTypeOfScrap { get; set; }
    public short? OutputPieces { get; set; }
    public double? ScrapPercent { get; set; }
    public long? ProductId { get; set; }
    public string RawMaterialStatus { get; set; }
    public int DefectsNumber { get; set; }
    public string ProductCatalogueTypeCode { get; set; }
  }
}
