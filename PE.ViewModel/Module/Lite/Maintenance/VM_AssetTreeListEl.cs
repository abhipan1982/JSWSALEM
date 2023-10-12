namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_AssetTreeListEl
  {
    public VM_AssetTreeListEl(long? ParentId, long Id, string Name, bool IsDevice, long? DeviceId, bool IsArea)
    {
      this.Id = Id;
      this.ParentId = ParentId;
      this.Name = Name;
      this.IsDevice = IsDevice;
      this.DeviceId = DeviceId;
      this.IsArea = IsArea;
    }

    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long? DeviceId { get; set; }
    public string Name { get; set; }
    public bool IsDevice { get; set; }
    public bool IsArea { get; set; }
  }
}
