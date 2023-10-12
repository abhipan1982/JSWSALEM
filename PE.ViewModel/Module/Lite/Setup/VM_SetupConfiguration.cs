using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_SetupConfiguration : VM_Base
  {
    #region ctor

    public VM_SetupConfiguration() { }

    public VM_SetupConfiguration(V_SetupConfigurationSearchGrid data)
    {
      ConfigurationId = data.ConfigurationId;
      ConfigurationName = data.ConfigurationName;
      ConfigurationCreatedTs = data.ConfigurationCreatedTs;
      ConfigurationVersion = data.ConfigurationVersion;
      ConfigurationLastSentTs = data.ConfigurationLastSentTs;
    }

    public VM_SetupConfiguration(List<VM_SetupConnector> connectedSetups, List<VM_Setup> setups, STPConfiguration configuration)
    {
      ConnectedSetups = connectedSetups;
      Setups = setups;
      ConfigurationVersion = configuration?.ConfigurationVersion ?? 1;
      ConfigurationId = configuration?.ConfigurationId ?? 0;
      ConfigurationName = configuration?.ConfigurationName ?? null;
    }

    public VM_SetupConfiguration(long configurationId, string configurationName, short configurationVersion)
    {
      ConfigurationId = configurationId;
      ConfigurationName = configurationName;
      ConfigurationVersion = configurationVersion;
    }

    #endregion

    #region props

    [SmfDisplay(typeof(VM_SetupConfiguration), "ConfigurationName", "NAME_Name")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public string ConfigurationName { get; set; }

    [SmfDisplay(typeof(VM_SetupConfiguration), "ConfigurationCreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime ConfigurationCreatedTs { get; set; }

    [SmfDisplay(typeof(VM_SetupConfiguration), "ConfigurationLastSentTs", "NAME_LastSent")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? ConfigurationLastSentTs { get; set; }

    [SmfDisplay(typeof(VM_SetupConfiguration), "ConfigurationVersion", "NAME_SetupConfigurationVersion")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short ConfigurationVersion { get; set; }

    public long ConfigurationId { get; set; }

    public List<VM_SetupConnector> ConnectedSetups { get; set; }
    public List<VM_Setup> Setups { get; set; }

    #endregion
  }
}
