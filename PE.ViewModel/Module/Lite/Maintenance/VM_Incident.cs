using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_Incident : VM_Base
  {
    public VM_Incident()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_Incident(MNTIncident p)
    //{
    //  IncidentId = p.IncidentId;
    //  StartTime = p.StartTime;
    //  EndTime = p.EndTime;
    //  FKIncidentTypeId = p.FKIncidentTypeId;
    //  FKDeviceComponentId = p.FKDeviceComponentId;
    //  IncidentDescription = p.IncidentDescription;

    //  if (p.FKIncidentType != null)
    //  {
    //    IncidentCode = p.FKIncidentType.IncidentTypeCode;
    //    IncidentName = p.FKIncidentType.IncidentTypeName;
    //    IncidentTypeDescription = p.FKIncidentType.IncidentTypeDescription;
    //  }

    //  if (p.FKDeviceComponent != null)
    //  {
    //    DeviceId = p.FKDeviceComponent.FKDeviceId;
    //  }

    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? IncidentId { get; set; }
    public long DeviceId { get; set; }

    [SmfDisplay(typeof(VM_Incident), "IncidentName", "NAME_IncidentName")]
    public string IncidentName { get; set; }

    [SmfDisplay(typeof(VM_Incident), "IncidentCode", "NAME_IncidentCode")]
    public string IncidentCode { get; set; }

    [SmfDisplay(typeof(VM_Incident), "StartTime", "NAME_StartTime")]
    public DateTime? StartTime { get; set; }

    [SmfDisplay(typeof(VM_Incident), "EndTime", "NAME_EndTime")]
    public DateTime? EndTime { get; set; }

    [SmfDisplay(typeof(VM_Incident), "IsPlanned", "NAME_IsPlanned")]
    public short IsPlanned { get; set; }

    [SmfDisplay(typeof(VM_Incident), "FKIncidentTypeId", "NAME_IncidentType")]
    public long? FKIncidentTypeId { get; set; }

    [SmfDisplay(typeof(VM_Incident), "FKDeviceComponentId", "NAME_DeviceComponent")]
    public long? FKDeviceComponentId { get; set; }

    [SmfDisplay(typeof(VM_Incident), "IncidentDescription", "NAME_Description")]
    public string IncidentDescription { get; set; }

    [SmfDisplay(typeof(VM_Incident), "IncidentTypeDescription", "NAME_IncidentTypeDescription")]
    public string IncidentTypeDescription { get; set; }

    [SmfDisplay(typeof(VM_Incident), "FkIncidentTypeId", "NAME_FkIncidentTypeId")]
    public long? FkIncidentTypeId { get; set; }
  }
}
