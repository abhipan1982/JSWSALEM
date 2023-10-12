using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_DeviceIncident : VM_Base
  {
    public VM_DeviceIncident()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_DeviceIncident(MNTIncident p)
    //{
    //  IncidentId = p.IncidentId;
    //  DeviceId = p.FKDeviceComponentId ?? 0;
    //  StartTime = p.StartTime;
    //  EndTime = p.EndTime;
    //  EnumSeverity = p.EnumSeverity;
    //  FKIncidentTypeId = p.FKIncidentTypeId;
    //  FKDeviceComponentId = p.FKDeviceComponentId;
    //  IncidentDescription = p.IncidentDescription;
    //  if (p.FKIncidentTypeId % 4 == 0)
    //  {
    //    CategoryColor = "#009000";
    //  }

    //  if (p.FKIncidentTypeId % 4 == 1)
    //  {
    //    CategoryColor = "#900048";
    //  }

    //  if (p.FKIncidentTypeId % 4 == 2)
    //  {
    //    CategoryColor = "#480090";
    //  }

    //  if (p.FKIncidentTypeId % 4 == 3)
    //  {
    //    CategoryColor = "#909000";
    //  }

    //  if (p.FKIncidentType != null)
    //  {
    //    IncidentCode = p.FKIncidentType.IncidentTypeCode;
    //    IncidentName = p.FKIncidentType.IncidentTypeName;
    //    IncidentTypeDescription = p.FKIncidentType.IncidentTypeDescription;
    //  }

    //  if (p.FKDeviceComponent != null)
    //  {
    //    if (p.FKDeviceComponent.FKComponent != null)
    //    {
    //      ComponentName = p.FKDeviceComponent.FKComponent.ComponentName;
    //    }
    //  }

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long DeviceId { get; set; }
    public long IncidentId { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "IncidentName", "NAME_IncidentName")]
    public string IncidentName { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "ComponentName", "NAME_ComponentName")]
    public string ComponentName { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "IncidentCode", "NAME_IncidentCode")]
    public string IncidentCode { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "StartTime", "NAME_StartTime")]
    public DateTime? StartTime { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "EndTime", "NAME_EndTime")]
    public DateTime? EndTime { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "IsPlanned", "NAME_IsPlanned")]
    public short IsPlanned { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "EnumSeverity", "NAME_EnumSeverity")]
    public short EnumSeverity { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "FKIncidentTypeId", "NAME_IncidentType")]
    public long? FKIncidentTypeId { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "FKDeviceComponentId", "NAME_DeviceComponent")]
    public long? FKDeviceComponentId { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "IncidentDescription", "NAME_Description")]
    public string IncidentDescription { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "IncidentTypeDescription", "NAME_IncidentTypeDescription")]
    public string IncidentTypeDescription { get; set; }

    [SmfDisplay(typeof(VM_DeviceIncident), "CategoryColor", "NAME_Color")]
    public string CategoryColor { get; set; }
  }
}
