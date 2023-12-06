namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_ComponentsGroupsTreeListEl
  {
    public VM_ComponentsGroupsTreeListEl(long? parentId, long id, string name, bool isDevice, long? deviceId,
      bool isComponent, bool isGroup, bool isCounterLimit = false, string unit = "", double? value = null)
    {
      this.Id = id;
      this.ParentId = parentId;
      this.Name = name;
      this.IsDevice = isDevice;
      this.DeviceId = deviceId;
      this.IsComponent = isComponent;
      this.IsGroup = isGroup;
      this.IsCounterLimit = isCounterLimit;
      this.Unit = unit;
      this.Value = value;
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
