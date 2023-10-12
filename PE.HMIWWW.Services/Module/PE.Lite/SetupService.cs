using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using LinqKit;
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
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Notification;
using PE.DbEntity.PEContext;
using PE.DbEntity.EFCoreExtensions;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class SetupService : BaseService, ISetupService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public SetupService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    #region Setups


    public DataSourceResult GetSetupSearchGridData(ModelStateDictionary modelState, DataSourceRequest request, long setupType)
    {
      IQueryable<V_SetupParameter> result = _hmiContext.V_SetupParameters.Where(x => x.SetupTypeId == setupType);

      return result.ToDataSourceLocalResult(request, modelState, data => new VM_Setup(data));
    }

    public async Task<long> GetSetupType(long setupId)
    {
      return await _peContext.STPSetups.Where(x => x.SetupId == setupId).Select(x => x.FKSetupTypeId).FirstOrDefaultAsync();
    }

    public VM_ListOfFilters GetFiltersListForSetupWithValues(ModelStateDictionary modelState, long setupId)
    {
      VM_ListOfFilters result = new VM_ListOfFilters();
      STPSetup setup = FindSetup(setupId);
      if (setup == null) return result;
      List<STPSetupTypeParameter> parameterListForSetup = FindSetupParameters(setup.FKSetupTypeId);
      result.SetupName = setup.SetupName;
      result.SetupCode = setup.SetupCode;
      result.SetupId = setup.SetupId;
      result.SetupType = setup.FKSetupTypeId;
      result.SetupTypeCode = setup.FKSetupType.SetupTypeCode;
      foreach (STPSetupTypeParameter parameter in parameterListForSetup)
      {
        STPSetupParameter setupParameter = FindSetupParameterById(setupId, parameter.FKParameterId);
        if (setupParameter != null)
        {
          VM_Filters filter = new VM_Filters(parameter, parameter.FKParameterId, setupParameter.ParameterValueId);
          filter.ParameterValueName = TranslateParameterValueName(filter.ParameterCode, setupParameter.ParameterValueId);
          result.ListOfFilters.Add(filter);
        }
      }

      return result;
    }

    public List<string> GetListOfFiltersNameForSetup(long setupType)
    {
      List<string> result = new List<string>();
      IEnumerable<STPSetupTypeParameter> listOfFilters = FindSetupParameters(setupType);
      foreach (STPSetupTypeParameter filter in listOfFilters)
      {
        result.Add(filter.FKParameter.ParameterName);
      }

      return result;
    }

    public List<VM_Filters> GetValueOfFiltersForSetup(long setupId)
    {
      List<VM_Filters> listOfFilters = new List<VM_Filters>();

      STPSetup setup = FindSetup(setupId);
      if (setup == null) return listOfFilters;
      List<STPSetupTypeParameter> parameterListForSetup = FindSetupParameters(setup.FKSetupTypeId);
      foreach (STPSetupTypeParameter parameter in parameterListForSetup)
      {
        STPSetupParameter setupParameter = FindSetupParameterById(setupId, parameter.FKParameterId);
        if (setupParameter != null)
        {
          VM_Filters filter = new VM_Filters(parameter, parameter.FKParameterId, setupParameter.ParameterValueId);
          filter.ParameterValueName = TranslateParameterValueName(filter.ParameterCode, setupParameter.ParameterValueId);
          listOfFilters.Add(filter);
        }
      }

      return listOfFilters;
    }

    public VM_ListOfFilters GetFiltersListForSetupType(ModelStateDictionary modelState, long setupType)
    {
      VM_ListOfFilters result = new VM_ListOfFilters();

      result.SetupType = setupType;
      List<STPSetupTypeParameter> parameterListForSetup = FindSetupParameters(setupType);
      if (!parameterListForSetup.Any())
        result.SetupTypeCode = _peContext.STPSetupTypes.First(x => x.SetupTypeId == setupType).SetupTypeCode;
      else
        result.SetupTypeCode = parameterListForSetup.First().FKSetupType.SetupTypeCode;

      foreach (STPSetupTypeParameter parameter in parameterListForSetup)
      {
        VM_Filters filter = new VM_Filters(parameter);
        result.ListOfFilters.Add(filter);
      }

      return result;
    }

    public Dictionary<long, string> GetFilteringData(string tableName, string columnId, string columnName)
    {
      string query = $"SELECT CAST({columnId} as bigint) as [Key],CAST({columnName} as varchar) as Value FROM {tableName}";

      return _hmiContext.ExecuteFilteringData(query);
    }

    #endregion

    #region module communication

    public async Task<VM_Base> UpdateSetupParameters(ModelStateDictionary modelState, VM_ListOfFilters model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values
      if (CheckIfParametersAreUniqe(model))
      {
        DCSetupListOfParameres dCParameterList = new DCSetupListOfParameres();
        dCParameterList.ListOfParametres = new Dictionary<long, long>();
        dCParameterList.SetupName = model.SetupName;
        dCParameterList.SetupId = model.SetupId;
        foreach (VM_Filters parameter in model.ListOfFilters)
        {
          dCParameterList.ListOfParametres.Add((long)parameter.SetupParameterId, parameter.ParameterValue.Value);
        }
        SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateSetupParametersAsync(dCParameterList);

        HandleWarnings(sendOfficeResult, ref modelState);

      }
      else
      {
        throw new ModuleMessageException("Setup", "STP005", "");
      }

      return result;
    }

    public async Task<VM_Base> CreateSetup(ModelStateDictionary modelState, VM_ListOfFilters model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values
      if (CheckIfParametersAreUniqe(model) && CheckIfSetupNameIsUniqe(model))
      {
        DCSetupListOfParameres dCParameterList = new DCSetupListOfParameres();
        dCParameterList.ListOfParametres = new Dictionary<long, long>();
        dCParameterList.SetupType = model.SetupType;
        dCParameterList.SetupName = model.SetupName;
        foreach (VM_Filters parameter in model.ListOfFilters)
        {
          if (parameter.ParameterValue.HasValue)
          {
            dCParameterList.ListOfParametres.Add((long)parameter.ParameterType, parameter.ParameterValue.Value);
          }
        }
        SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateSetupAsync(dCParameterList);

        HandleWarnings(sendOfficeResult, ref modelState);
      }
      else
      {
        throw new ModuleMessageException("Setup", "STP005", "");
      }

      return result;
    }

    public async Task<VM_Base> UpdateSetupValue(ModelStateDictionary modelState, VM_SetupValues model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values
      if (!ValidateValueType(model.Value, model.DataType))
        throw new ModuleMessageException("Setup", "STP011", "");
      if (!ValidateValueRange(model.Value, model.DataType, model.RangeFrom, model.RangeTo))
        throw new ModuleMessageException("Setup", "STP012", "");

      DCSetupValue dCTelegramSetup = new DCSetupValue()
      {
        SetupInstructionId = model.SetupInstructionId,
        Value = model.Value
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateSetupValueAsync(dCTelegramSetup);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CopySetup(ModelStateDictionary modelState, VM_ListOfFilters model)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      //validate values
      if (CheckIfParametersAreUniqe(model) && CheckIfSetupNameIsUniqe(model))
      {
        DCSetupListOfParameres dCParameterList = new DCSetupListOfParameres();
        dCParameterList.ListOfParametres = new Dictionary<long, long>();
        dCParameterList.SetupName = model.SetupName;
        dCParameterList.SetupId = model.SetupId;
        foreach (VM_Filters parameter in model.ListOfFilters)
        {
          dCParameterList.ListOfParametres.Add((long)parameter.SetupParameterId, parameter.ParameterValue.Value);
        }
        SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CopySetupAsync(dCParameterList);

        HandleWarnings(sendOfficeResult, ref modelState);
      }
      else
      {
        throw new ModuleMessageException("Setup", "STP005", "");
      }

      return result;
    }

    public async Task<VM_Base> DeleteSetup(ModelStateDictionary modelState, long setupId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      DCSetupListOfParameres dCParameterList = new DCSetupListOfParameres();
      dCParameterList.SetupId = setupId;
      dCParameterList.ListOfParametres = new Dictionary<long, long>();
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteSetupAsync(dCParameterList);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> SendSetupsToL1(ModelStateDictionary modelState, long telegramId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      DCCommonSetupStructure dCParameterList = new DCCommonSetupStructure
      {
        TelegramId = telegramId
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendSetupsToL1Async(dCParameterList);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CalculateSetup(ModelStateDictionary modelState, long telegramId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }
      DCCommonSetupStructure dCParameterList = new DCCommonSetupStructure
      {
        TelegramId = telegramId
      };
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CalculateSetupAsync(dCParameterList);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public DataSourceResult GetSetupInstructions(ModelStateDictionary modelState, DataSourceRequest request, long setupId)
    {
      List<DbEntity.SPModels.SPSetupInstruction> result = FindSetupInstructions(setupId, null, null)
        .OrderBy(x => x.OrderSeq)
        .ToList();

      return result.ToDataSourceLocalResult(request, modelState, data => new VM_SetupValues(data));
    }

    #endregion

    #region Helpers

    public string GetSetupNameByType(long setupType)
    {
      return _peContext.STPSetupTypes
        .FirstOrDefault(x => x.SetupTypeId == setupType)
        .SetupTypeName.Replace(" ", "");
    }

    private STPSetup FindSetup(long setupId)
    {
      return _peContext.STPSetups
        .Include(x => x.FKSetupType)
        .FirstOrDefault(x => x.SetupId == setupId);
    }

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

    private STPSetupParameter FindSetupParameterById(long setupId, long parameterId)
    {
      return _peContext.STPSetupParameters
        .AsNoTracking()
        .FirstOrDefault(x => x.FKSetupId == setupId && x.FKParameterId == parameterId);
    }

    private bool CheckIfSetupNameIsUniqe(VM_ListOfFilters listOfFilters)
    {
      return !_peContext.STPSetups
        .Where(x => x.FKSetupTypeId == listOfFilters.SetupType && x.SetupName == listOfFilters.SetupName)
        .Any();
    }

    private bool CheckIfParametersAreUniqe(VM_ListOfFilters listOfFilters)
    {
      bool result = false;

      if (listOfFilters.ListOfFilters.Count == 0)
        return true;
      long setupTypeId = listOfFilters.ListOfFilters.First().SetupType;
      Expression<Func<STPSetupParameter, bool>> predicate = PredicateBuilder.New<STPSetupParameter>(false);
       
      foreach (VM_Filters parameter in listOfFilters.ListOfFilters)
      {
        Expression<Func<STPSetupParameter, bool>> inner = PredicateBuilder.New<STPSetupParameter>(true);

        inner = inner.And(i => i.FKParameterId == parameter.ParameterType);
        inner = inner.And(i => i.ParameterValueId == parameter.ParameterValue);

        predicate = predicate.Or(inner);

      }

      result = !(from setup in _peContext.STPSetups.Where(x => x.FKSetupTypeId == setupTypeId)
                 let parametersCount = _peContext.STPSetupParameters.Where(p => p.FKSetupId == setup.SetupId).Where(predicate).Count()
                 where parametersCount == listOfFilters.ListOfFilters.Count
                 select setup).Any();

      return result;
    }

    private bool ValidateValueType(string value, string dataType)
    {
      try
      {
        switch (dataType)
        {
          case "BOOL":
            if (value != "0" && value != "1")
              return false;
            break;
          case "BYTE":
            if (value != "0" && value != "1")
              return false;
            break;
          case "INT":
            Int16.Parse(value);
            break;
          case "DINT":
            Int32.Parse(value);
            break;
          case "REAL":
            float.Parse(value.Replace('.', ','));
            break;
        }
      }
      catch (Exception)
      {
        NotificationController.Error($"Operation failed - Value conversion failed!, method: {MethodBase.GetCurrentMethod().Name}");
        return false;
      }

      return true;
    }

    private bool ValidateValueRange(string value, string dataType, double? rangeFrom, double? rangeTo)
    {
      try
      {
        switch (dataType)
        {
          case "BOOL":
            if (Int16.Parse(value) < rangeFrom || Int16.Parse(value) > rangeTo)
              return false;
            break;
          case "BYTE":
            if (Int16.Parse(value) < rangeFrom || Int16.Parse(value) > rangeTo)
              return false;
            break;
          case "INT":
            if (Int16.Parse(value) < rangeFrom || Int16.Parse(value) > rangeTo)
              return false;
            break;
          case "DINT":
            if (Int32.Parse(value) < rangeFrom || Int32.Parse(value) > rangeTo)
              return false;
            break;
          case "REAL":
            if (float.Parse(value.Replace('.', ',')) < rangeFrom || float.Parse(value.Replace('.', ',')) > rangeTo)
              return false;
            break;
        }
      }
      catch (Exception)
      {
        NotificationController.Error($"Operation failed - Value conversion failed!, method: {MethodBase.GetCurrentMethod().Name}");
        return false;
      }

      return true;
    }

    private string TranslateParameterValueName(string setupCode, long valueId)
    {
      if (valueId > 0)
      {

        SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@ParameterCode",
                            SqlDbType =  SqlDbType.VarChar,
                            Size = 100,
                            Direction = ParameterDirection.Input,
                            Value = setupCode
                        },
                        new SqlParameter() {
                            ParameterName = "@ParameterValueId",
                            SqlDbType =  SqlDbType.Int,
                            Direction = ParameterDirection.Input,
                            Value = valueId
                        },
                        new SqlParameter() {
                            ParameterName = "@ResultValue",
                            SqlDbType =  SqlDbType.NVarChar,
                            Size = 4000,
                            Direction = ParameterDirection.Output,
                            Value =  DBNull.Value
                        },
        };

        return _hmiContext.ExecuteParameterLookup(parameters);
      }

      return "";
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

    #endregion
  }
}
