namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_AssetTreeListEl
  {
    public VM_AssetTreeListEl(long? parentId, long id, string name, bool isDevice, long? deviceId, bool isArea)
    {
      this.Id = id;
      this.ParentId = parentId;
      this.Name = name;
      this.IsDevice = isDevice;
      this.DeviceId = deviceId;
      this.IsArea = isArea;
    }

    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long? DeviceId { get; set; }
    public string Name { get; set; }
    public bool IsDevice { get; set; }
    public bool IsArea { get; set; }
  }
}
