namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_ComponentsGroupsTreeListEl
  {
    public VM_ComponentsGroupsTreeListEl(long? ParentId, long Id, string Name, bool IsDevice, long? DeviceId,
      bool IsComponent, bool IsGroup, bool IsCounterLimit = false, string Unit = "", double? Value = null)
    {
      this.Id = Id;
      this.ParentId = ParentId;
      this.Name = Name;
      this.IsDevice = IsDevice;
      this.DeviceId = DeviceId;
      this.IsComponent = IsComponent;
      this.IsGroup = IsGroup;
      this.IsCounterLimit = IsCounterLimit;
      this.Unit = Unit;
      this.Value = Value;
    }

    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long? DeviceId { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public double? Value { get; set; }
    public double? ValueAlarm { get; set; }
    public double? ValueWarning { get; set; }
    public double? ValueMax { get; set; }
    public bool IsDevice { get; set; }
    public bool IsComponent { get; set; }
    public bool IsGroup { get; set; }
    public bool IsCounterLimit { get; set; }
  }
}
