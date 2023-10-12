namespace PE.DbEntity.SPModels
{
  public class SPUserMenu
  {
    public string UserName { get; set; }

    public string AccessUnitName { get; set; }

    public long HmiClientMenuId { get; set; }

    public long? ParentHmiClientMenuId { get; set; }

    public string HmiClientMenuName { get; set; }

    public string ControllerName { get; set; }

    public string Method { get; set; }
    
    public string Area { get; set; }

    public string MethodParameter { get; set; }

    public short? DisplayOrder { get; set; }

    public string IconName { get; set; }

    public bool? IsActive { get; set; }

    public long? AccessUnitId { get; set; }
  }
}
