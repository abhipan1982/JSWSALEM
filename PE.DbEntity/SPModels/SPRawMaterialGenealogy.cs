namespace PE.DbEntity.SPModels
{
  public class SPRawMaterialGenealogy
  {
    public long RawMaterialId { get; set; }
    public long? ParentRawMaterialId { get; set; }
    public long? MaterialId { get; set; }
    public long? ProductId { get; set; }
    public string RawMaterialName { get; set; }
    public short EnumRawMaterialStatus { get; set; }
    public short EnumTypeOfScrap { get; set; }
    public short EnumRejectLocation { get; set; }
    public double? LastLength { get; set; }
    public long? ProductCatalogueTypeId { get; set; }
    public string ProductCatalogueTypeCode { get; set; }
    public short CuttingSeqNo { get; set; }
    public short ChildsNo { get; set; }
  }
}
