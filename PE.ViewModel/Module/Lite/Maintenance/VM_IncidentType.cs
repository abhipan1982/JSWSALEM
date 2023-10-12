using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_IncidentType : VM_Base
  {
    public VM_IncidentType()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_IncidentType(MNTIncidentType p)
    //{
    //  IncidentTypeId = p.IncidentTypeId;
    //  IncidentTypeName = p.IncidentTypeName;
    //  IncidentTypeCode = p.IncidentTypeCode;
    //  IncidentTypeDescription = p.IncidentTypeDescription;
    //  //TODOMN - exclude this
    //  //this.CreatedTs = p.CreatedTs;
    //  //this.LastUpdateTs = p.LastUpdateTs;
    //  EnumSeverity = p.EnumSeverity;


    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? IncidentTypeId { get; set; }

    [SmfDisplay(typeof(VM_IncidentType), "IncidentName", "NAME_IncidentName")]
    public string IncidentTypeName { get; set; }

    [SmfDisplay(typeof(VM_IncidentType), "IncidentCode", "NAME_IncidentCode")]
    public string IncidentTypeCode { get; set; }

    [SmfDisplay(typeof(VM_Incident), "IncidentTypeDescription", "NAME_IncidentTypeDescription")]
    public string IncidentTypeDescription { get; set; }

    [SmfDisplay(typeof(VM_Incident), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Incident), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_Incident), "DefaultEnumSeverity", "NAME_DefaultEnumSeverity")]
    public short? EnumSeverity { get; set; }
  }
}
