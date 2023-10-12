using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EFCoreExtensions;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.STP;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.DbEntity.PEContext;
using Kendo.Mvc.Extensions;
using PE.DbEntity.EFCoreExtensions;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class SetupConfigurationService : BaseService, ISetupConfigurationService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public SetupConfigurationService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public async Task<VM_SetupConfiguration> FindConfigurationAsync(long configurationId)
    {
      VM_SetupConfiguration result = new VM_SetupConfiguration();

      V_SetupConfigurationSearchGrid data = await _hmiContext.V_SetupConfigurationSearchGrids
        .FirstAsync(x => x.ConfigurationId == configurationId);
      result = new VM_SetupConfiguration(data);

      return result;
    }

    public DataSourceResult GetConfigurationSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_SetupConfigurationSearchGrids.ToDataSourceLocalResult(request, modelState, data => new VM_SetupConfiguration(data));

      return result;
    }

    public DataSourceResult GetSetupConfigurationDetails(ModelStateDictionary modelState, DataSourceRequest request, long configurationId)
    {
      DataSourceResult result = null;

      if (configurationId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      return _hmiContext.V_SetupConfigurationSearchGrids
        .Where(x => x.ConfigurationId == configurationId)
        .ToDataSourceLocalResult(request, modelState, data => new VM_SetupConfiguration(data));
    }

    public DataSourceResult GetSetupConfigurationsSearchGridData(ModelStateDictionary modelState, DataSourceRequest request, long configurationId)
    {
      IQueryable<V_SetupConfiguration> result = _hmiContext.V_SetupConfigurations.Where(x => x.ConfigurationId == configurationId);

      return result.ToDataSourceLocalResult(request, modelState, data => new VM_SetupConfigurations(data));
    }

    public async Task<long> GetSetupType(long setupId)
    {
      return await _peContext.STPSetups.Where(x => x.SetupId == setupId).Select(x => x.FKSetupTypeId).FirstOrDefaultAsync();
    }

    public List<string> GetListOfFiltersNameForSetup(long setupTypeId)
    {
      List<string> result = new List<string>();

      IEnumerable<STPSetupTypeParameter> listOfFilters = FindSetupParameters(setupTypeId);
      foreach (STPSetupTypeParameter filter in listOfFilters)
      {
        result.Add(filter.FKParameter.ParameterName);
      }

      return result;
    }

    public DataSourceResult GetSetupParametersGridData(ModelStateDictionary modelState, DataSourceRequest request, long setupId, long setupConfigurationId)
    {
      STPSetupConfiguration setupConfiguration = _peContext.STPSetupConfigurations.FirstOrDefault(x => x.SetupConfigurationId == setupConfigurationId);
      IEnumerable<VM_Setup> result = _hmiContext.V_SetupParameters.Where(x => x.SetupId == setupId).ToList().Select(x => new VM_Setup(x, setupConfiguration?.SetupConfigurationLastSentTs));

      return result.ToDataSourceLocalResult(request, modelState, (x) => x);
    }

    public DataSourceResult GetSetupInstructions(ModelStateDictionary modelState, DataSourceRequest request, long setupId)
    {
      List<DbEntity.SPModels.SPSetupInstruction> result = FindSetupInstructions(setupId, null, null)
        .OrderBy(x => x.OrderSeq)
        .ToList();

      return result.ToDataSourceLocalResult(request, modelState, data => new VM_SetupValues(data));
    }

    public VM_SetupConfiguration GetEmptyConfiguration()
    {
      List<VM_SetupConnector> connectedSetups = GetSetupTypes(_peContext, null);
      List<VM_Setup> setups = GetSetups(_peContext);

      return new VM_SetupConfiguration(connectedSetups, setups, null);
    }

    public async Task<VM_Base> CreateSetupConfigurationAsync(ModelStateDictionary modelState, VM_SetupConfiguration model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCSetupConfiguration dc = new DCSetupConfiguration
      {
        Setups = model.ConnectedSetups.Where(x => x.SetupId != null).Select(x => x.SetupId.Value).ToList(),
        SetupConfigurationName = model.ConfigurationName,
        SetupConfigurationVersion = model.ConfigurationVersion
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateSetupConfigurationAsync(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_SetupConfiguration GetConfiguration(long configurationId, bool includeSetups)
    {
      if (includeSetups)
      {
        List<STPSetupConfiguration> setupConfigurations = _peContext.STPSetupConfigurations
          .Include(x => x.FKConfiguration)
          .Include(x => x.FKSetup)
          .Where(x => x.FKConfigurationId == configurationId)
          .ToList();
        List<VM_SetupConnector> connectedSetups = GetSetupTypes(_peContext, setupConfigurations);
        List<VM_Setup> setups = GetSetups(_peContext);

        return new VM_SetupConfiguration(connectedSetups, setups, setupConfigurations.Select(x => x.FKConfiguration).FirstOrDefault());
      }
      else
      {
        STPConfiguration configuration = _peContext.STPConfigurations.First(x => x.ConfigurationId == configurationId);
        return new VM_SetupConfiguration(configurationId, configuration.ConfigurationName, configuration.ConfigurationVersion++);
      }
    }

    public async Task<VM_Base> EditSetupConfigurationAsync(ModelStateDictionary modelState, VM_SetupConfiguration model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCSetupConfiguration dc = new DCSetupConfiguration
      {
        SetupConfigurationId = model.ConfigurationId,
        Setups = model.ConnectedSetups.Where(x => x.SetupId != null).Select(x => x.SetupId.Value).ToList(),
        SetupConfigurationName = model.ConfigurationName,
        SetupConfigurationVersion = model.ConfigurationVersion
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.EditSetupConfigurationAsync(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteSetupConfigurationAsync(ModelStateDictionary modelState, long configurationId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCSetupConfiguration dc = new DCSetupConfiguration
      {
        SetupConfigurationId = configurationId
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteSetupConfigurationAsync(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> SendSetupConfigurationAsync(ModelStateDictionary modelState, long configurationId, bool steelgradeRelated)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCSetupConfiguration dc = new DCSetupConfiguration
      {
        SetupConfigurationId = configurationId,
        IsSteelgradeRelated = steelgradeRelated
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendSetupConfigurationAsync(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CloneSetupConfigurationAsync(ModelStateDictionary modelState, VM_SetupConfiguration model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCSetupConfiguration dc = new DCSetupConfiguration
      {
        SetupConfigurationId = model.ConfigurationId,
        SetupConfigurationName = model.ConfigurationName,
        SetupConfigurationVersion = model.ConfigurationVersion
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CloneSetupConfigurationAsync(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CreateSetupConfigurationVersionAsync(ModelStateDictionary modelState, long configurationId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCSetupConfiguration dc = new DCSetupConfiguration
      {
        SetupConfigurationId = configurationId,
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateSetupConfigurationVersionAsync(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    #region private

    private List<STPSetupTypeParameter> FindSetupParameters(long setupTypeId)
    {
      return _peContext.STPSetupTypeParameters
        .AsNoTracking()
        .Where(x => x.FKSetupTypeId == setupTypeId)
        .Include(x => x.FKParameter)
        .Include(x => x.FKSetupType)
        .AsNoTracking()
        .OrderByDescending(x => x.DefaultIsRequired)
        .ThenBy(x => x.OrderSeq)
        .ToList();
    }

    private List<DbEntity.SPModels.SPSetupInstruction> FindSetupInstructions(long? setupId, long? assetId, bool? isSentToL1)
    {

      SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@SetupId",
                            SqlDbType =  SqlDbType.BigInt,
                            IsNullable = true,
                            Direction = ParameterDirection.Input,
                            Value = (object)setupId ?? DBNull.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@AssetId",
                            SqlDbType =  SqlDbType.BigInt,
                            IsNullable = true,
                            Direction = ParameterDirection.Input,
                            Value = (object)assetId ?? DBNull.Value
                        },
                        new SqlParameter() {
                            ParameterName = "@IsSentToL1",
                            SqlDbType =  SqlDbType.Bit,
                            IsNullable = true,
                            Direction = ParameterDirection.Input,
                            Value = (object)isSentToL1 ?? DBNull.Value
                        }};

      return _hmiContext.ExecuteSetupInstruction(parameters);
    }

    private List<VM_SetupConnector> GetSetupTypes(PEContext ctx, List<STPSetupConfiguration> setupConfigurations)
    {
      List<VM_SetupConnector> result = new List<VM_SetupConnector>();

      foreach (STPSetupType item in ctx.STPSetupTypes.AsQueryable())
      {
        result.Add(new VM_SetupConnector(item.SetupTypeId, 0, item.SetupTypeName));
      }

      if (setupConfigurations != null)
      {
        List<long> setupIds = setupConfigurations.Select(x => x.FKSetupId).ToList();
        List<STPSetup> connectedSetups = ctx.STPSetups.Include(x => x.FKSetupType).Where(x => setupIds.Contains(x.SetupId)).ToList();

        foreach (STPSetup item in connectedSetups)
        {
          foreach (VM_SetupConnector element in result)
          {
            if (element.SetupTypeId == item.FKSetupType.SetupTypeId)
            {
              element.SetupId = item.SetupId;
            }
          }
        }
      }

      //else
      //{
      //  List<long> setupIds = setupConfigurations.Select(x => x.FKSetupId).ToList();
      //  foreach (STPSetup item in ctx.STPSetups.Include(x => x.FKSetupType).GroupBy(p => new { p.FKSetupType }).AsQueryable())
      //  {
      //    result.Add(new VM_SetupConnector(item.FKSetupTypeId, setupIds.Contains((item.SetupId)) ? item.SetupId : 0, item.FKSetupType.SetupTypeName));
      //  }
      //}

      return result;
    }

    private List<VM_Setup> GetSetups(PEContext ctx)
    {
      List<VM_Setup> result = new List<VM_Setup>();

      foreach (STPSetup item in ctx.STPSetups.AsQueryable())
      {
        result.Add(new VM_Setup(item));
      }

      return result;
    }

    #endregion
  }
}
