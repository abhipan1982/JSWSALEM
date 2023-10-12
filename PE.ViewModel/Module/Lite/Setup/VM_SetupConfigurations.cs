using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupConfigurations : VM_Base
  {
    #region ctor

    public VM_SetupConfigurations() { }

    public VM_SetupConfigurations(V_SetupConfiguration data)
    {
      ConfigurationId = data.ConfigurationId;
      SetupId = data.SetupId;
      SetupTypeId = data.SetupTypeId;
      ConfigurationName = data.ConfigurationName;
      ConfigurationCreatedTs = data.ConfigurationCreatedTs;
      ConfigurationVersion = data.ConfigurationVersion;
      SetupTypeName = data.SetupTypeName;
      IsRequired = data.IsRequired;
      IsActive = data.IsActive;
      IsSteelgradeRelated = data.IsSteelgradeRelated;
      SetupName = data.SetupName;
      SetupCreatedTs = data.SetupCreatedTs;
      SetupUpdatedTs = data.SetupUpdatedTs;
      SetupConfigurationId = data.SetupConfigurationId;
      SetupConfigurationLastSentTs = data.SetupConfigurationLastSentTs;
      ConfigurationLastSentTs = data.ConfigurationLastSentTs;
      IsActive = data.IsActive;
      IsSteelgradeRelated = data.IsSteelgradeRelated;
    }

    public VM_SetupConfigurations(STPSetupType data)
    {
      SetupTypeId = data.SetupTypeId;
      SetupTypeName = data.SetupTypeName;
      SetupTypeCode = data.SetupTypeCode;
    }

    #endregion

    #region props

    [SmfDisplay(typeof(VM_SetupConfigurations), "ConfigurationName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public string ConfigurationName { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "ConfigurationCreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ConfigurationCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "ConfigurationVersion", "NAME_SetupConfigurationVersion")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public short ConfigurationVersion { get; set; }


    public DateTime? ConfigurationLastSentTs { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "SetupTypeName", "NAME_SetupTypeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupTypeName { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "SetupTypeCode", "NAME_SetupTypeCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupTypeCode { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "IsRequired", "NAME_Required")]
    public bool IsRequired { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "IsActive", "NAME_IsActive")]
    public bool IsActive { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "IsSteelgradeRelated", "NAME_IsSteelgradeRelated")]
    public bool IsSteelgradeRelated { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "SetupName", "NAME_SetupName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string SetupName { get; set; }

    public DateTime SetupCreatedTs { get; set; }

    public DateTime SetupUpdatedTs { get; set; }

    public DateTime? SetupConfigurationLastSentTs { get; set; }

    [SmfDisplay(typeof(VM_SetupConfigurations), "SetupTypeId", "NAME_SetupName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long SetupTypeId { get; set; }

    public long ConfigurationId { get; set; }

    public long SetupId { get; set; }

    public long SetupConfigurationId { get; set; }

    public List<VM_Setup> Setups { get; set; }

    #endregion
  }
}
