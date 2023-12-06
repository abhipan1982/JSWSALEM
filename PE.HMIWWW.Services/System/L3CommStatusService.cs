using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.TransferModels;
using PE.DbEntity.TransferModels;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Models.DataContracts.Internal.DBA;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.System;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.System
{
  public interface IL3CommStatusService
  {
    DataSourceResult GetL3TransferTableWorkOrderList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);



    //Av@071123--

    DataSourceResult GetL3L2BatchDataDefinitionList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);





    DataSourceResult GetL3TransferTableGeneralList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    VM_Base GetWorkOrderDefinition(ModelStateDictionary modelState, long counterId);

    Task<VM_Base> CreateWorkOrderDefinition(ModelStateDictionary modelState,
      VM_L3L2WorkOrderDefinition workOrderDefinition);

    Task<VM_Base> UpdateWorkOrderDefinition(ModelStateDictionary modelState,
      VM_L3L2WorkOrderDefinition workOrderDefinition);

    Task<VM_Base> DeleteWorkOrderDefinition(ModelStateDictionary modelState,
      VM_L3L2WorkOrderDefinition workOrderDefinition);

    DataSourceResult GetL3TransferTableWorkOrderReports(ModelStateDictionary modelState, DataSourceRequest request);

    VM_Base GetWorkOrderReport(ModelStateDictionary modelState, long counterId);

    Task<VM_Base> ResetWorkOrderReport(ModelStateDictionary modelState, VM_L2L3WorkOrderReport workOrderDefinition);

    DataSourceResult GetL3TransferTableProductReports(ModelStateDictionary modelState, DataSourceRequest request);

    //Av@021123

    DataSourceResult GetL2L3BatchReport(ModelStateDictionary modelState, DataSourceRequest request);


    //DataSourceResult GetL3L2BatchDataDefinition(ModelStateDictionary modelState, long counterId);


    VM_Base GetProductReport(ModelStateDictionary modelState, long counterId);

    Task<VM_Base> ResetProductReport(ModelStateDictionary modelState, VM_L2L3ProductReport productReport);

    Task<VM_Base> ImportWorkOrderDefinition(ModelStateDictionary modelState, Stream fileStream);
  }

  public class L3CommStatusService : BaseService, IL3CommStatusService
  {
    private readonly PE.DbEntity.PEContext.TransferContext _transferContext;

    private readonly TransferCustomContext _transferCustomContext;//AddAv
    private const int MaxNumberOfImportRecords = 100;

    public L3CommStatusService(IHttpContextAccessor httpContextAccessor, DbEntity.PEContext.TransferContext transferContext, TransferCustomContext transferContext1) : base(httpContextAccessor)//AddAv
    //public L3CommStatusService(IHttpContextAccessor httpContextAccessor, DbEntity.PEContext.TransferContext transferContext) : base(httpContextAccessor)//AddAv
    {
      _transferContext = transferContext;

     _transferCustomContext = transferContext1;//AddAv
    }

    public DataSourceResult GetL3TransferTableWorkOrderList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _transferContext.L3L2WorkOrderDefinitions
        .ToDataSourceLocalResult(request, modelState, x => new VM_L3L2WorkOrderDefinition(x));

      return result;
    }


    //Av@071123--

    public DataSourceResult GetL3L2BatchDataDefinitionList(ModelStateDictionary modelState,
       DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _transferCustomContext.L3L2BatchDataDefinitions
        .ToDataSourceLocalResult(request, modelState, x => new VM_L3L2BatchDataDefinition(x));

      return result;
    }




    public DataSourceResult GetL3TransferTableGeneralList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _transferContext.V_L3L2TransferTablesSummaries
        .ToDataSourceLocalResult(request, modelState, x => new VM_L3TransferTableGeneral(x));

      return result;
    }

    public VM_Base GetWorkOrderDefinition(ModelStateDictionary modelState, long counterId)
    {
      VM_L3L2WorkOrderDefinition result = null;

      //VALIDATE ENTRY PARAMETERS
      if (counterId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }
            //END OF VALIDATION

            DbEntity.TransferModels.L3L2WorkOrderDefinition wod = _transferContext.L3L2WorkOrderDefinitions
        .SingleOrDefault(x => x.CounterId == counterId);
      result = wod != null ? new VM_L3L2WorkOrderDefinition(wod) : null;

      return result;
    }

    public async Task<VM_Base> CreateWorkOrderDefinition(ModelStateDictionary modelState,
      VM_L3L2WorkOrderDefinition workOrderDefinition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref workOrderDefinition);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.CreateWorkOrderDefinitionAsync(workOrderDefinition.GetL3L2WorkOrderDefinitionDataContract());

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateWorkOrderDefinition(ModelStateDictionary modelState,
      VM_L3L2WorkOrderDefinition workOrderDefinition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref workOrderDefinition);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.UpdateWorkOrderDefinitionAsync(workOrderDefinition.GetL3L2WorkOrderDefinitionDataContract());

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteWorkOrderDefinition(ModelStateDictionary modelState,
      VM_L3L2WorkOrderDefinition workOrderDefinition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition = new DCL3L2WorkOrderDefinitionMOD
      {
        Counter = workOrderDefinition.CounterId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendDeleteWorkOrderDefinitionAsync(dcWorkOrderDefinition);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public DataSourceResult GetL3TransferTableWorkOrderReports(ModelStateDictionary modelState,
      DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _transferContext.L2L3WorkOrderReports
        .ToDataSourceLocalResult(request, modelState, x => new VM_L2L3WorkOrderReport(x));

      return result;
    }

    public VM_Base GetWorkOrderReport(ModelStateDictionary modelState, long counterId)
    {
      VM_L2L3WorkOrderReport result = null;

      //VALIDATE ENTRY PARAMETERS
      if (counterId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }
            //END OF VALIDATION

            DbEntity.TransferModels.L2L3WorkOrderReport wod = _transferContext.L2L3WorkOrderReports
        .SingleOrDefault(x => x.Counter == counterId);
      result = wod != null ? new VM_L2L3WorkOrderReport(wod) : null;

      return result;
    }
    public async Task<VM_Base> ResetWorkOrderReport(ModelStateDictionary modelState,
      VM_L2L3WorkOrderReport workOrderDefinition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCL2L3WorkOrderReport dcWorkOrderReport = new DCL2L3WorkOrderReport { Counter = workOrderDefinition.CounterId };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendResetWorkOrderReportAsync(dcWorkOrderReport);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public DataSourceResult GetL3TransferTableProductReports(ModelStateDictionary modelState,
      DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _transferContext.L2L3ProductReports
        .ToDataSourceLocalResult(request, modelState, x => new VM_L2L3ProductReport(x));

      return result;
    }


    //Av@231123

    public DataSourceResult GetL2L3BatchReport(ModelStateDictionary modelState,
    DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _transferCustomContext.L2L3BatchReports
        .ToDataSourceLocalResult(request, modelState, x => new VM_L2L3BatchReport(x));

      return result;
    }





    public VM_Base GetProductReport(ModelStateDictionary modelState, long counterId)
    {
      VM_L2L3ProductReport result = null;

      //VALIDATE ENTRY PARAMETERS
      if (counterId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }
            //END OF VALIDATION

            DbEntity.TransferModels.L2L3ProductReport cr = _transferContext.L2L3ProductReports
        .SingleOrDefault(x => x.Counter == counterId);
      result = cr != null ? new VM_L2L3ProductReport(cr) : null;

      return result;
    }

    public async Task<VM_Base> ResetProductReport(ModelStateDictionary modelState,
      VM_L2L3ProductReport workOrderDefinition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCL2L3ProductReport dcProductReport = new DCL2L3ProductReport { Counter = workOrderDefinition.Counter };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendResetProductReportAsync(dcProductReport);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> ImportWorkOrderDefinition(ModelStateDictionary modelState, Stream fileStream)
    {
      VM_Base result = new VM_Base();

      var records = new List<DbEntity.TransferModels.L3L2WorkOrderDefinition>();
      var reader = ExcelReaderFactory.CreateReader(fileStream);

      int row = 0;
      reader.Read(); // skip header

      while (reader.Read())
      {
        row++;

        if (reader.FieldCount < 22)
        {
          AddModelStateError(modelState, string.Format(ResourceController.GetErrorText("IncorrectWOImportFieldCount"), row, reader.FieldCount, 22));
        }
        var record = new DbEntity.TransferModels.L3L2WorkOrderDefinition();

        int col = 0;
        record.WorkOrderName = reader.GetValue(col++)?.ToString();

        if (string.IsNullOrWhiteSpace(record.WorkOrderName))
        {
          continue;
        }

        if (records.Count >= MaxNumberOfImportRecords)
        {
          AddModelStateError(modelState, string.Format(ResourceController.GetErrorText("WOImportRecordLimit"), MaxNumberOfImportRecords));
          break;
        }

        record.PreviousWorkOrderName = reader.GetValue(col++)?.ToString();
        record.OrderDeadline = reader.GetDateTime(col++).ToString("ddMMyyyy");
        record.CustomerName = reader.GetValue(col++)?.ToString();
        col++; // BilletSize
        record.NumberOfBillets = reader.GetValue(col++)?.ToString();
        record.BilletWeight = reader.GetValue(col++)?.ToString();
        record.BilletLength = reader.GetValue(col++)?.ToString();
        record.TargetWorkOrderWeight = reader.GetValue(col++)?.ToString();
        record.TargetWorkOrderWeightMin = reader.GetValue(col++)?.ToString();
        record.TargetWorkOrderWeightMax = reader.GetValue(col++)?.ToString();
        record.SteelgradeCode = reader.GetValue(col++)?.ToString();
        record.HeatName = reader.GetValue(col++)?.ToString();
        record.InputThickness = reader.GetValue(col++)?.ToString();
        record.InputWidth = reader.GetValue(col++)?.ToString();
        record.InputShapeSymbol = reader.GetValue(col++)?.ToString();
        record.OutputShapeSymbol = reader.GetValue(col++)?.ToString();
        record.OutputThickness = reader.GetValue(col++)?.ToString();
        record.OutputWidth = reader.GetValue(col++)?.ToString();
        record.BundleWeightMin = reader.GetValue(col++)?.ToString();
        record.BundleWeightMax = reader.GetValue(col++)?.ToString();
        // ExternalWorkOrderName
        // MaterialCatalogueName
        // ProductCatalogueName

        var check = reader.GetValue(col++)?.ToString();

        if (check != "1")
        {
          AddModelStateError(modelState, string.Format(ResourceController.GetErrorText("InvalidWOImport"), row));
        }

        records.Add(record);
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      using var transaction = _transferContext.Database.BeginTransaction();

      try
      {
        foreach (var record in records)
        {
          await _transferContext.L3L2WorkOrderDefinitions.AddAsync(record);
          await _transferContext.SaveChangesAsync();
        }
        transaction.Commit();
      }
      catch (Exception ex)
      {
        AddModelStateError(modelState, ex.Message);
      }

      return result;
    }
    
    //public Task<VM_Base> CreateWorkOrderDefinition(ModelStateDictionary modelState, VM_L3L2WorkOrderDefinition workOrderDefinition)
    //{
    //  throw new NotImplementedException();
    //}

    //public Task<VM_Base> UpdateWorkOrderDefinition(ModelStateDictionary modelState, VM_L3L2WorkOrderDefinition workOrderDefinition)
    //{
    //  throw new NotImplementedException();
    //}

    //public Task<VM_Base> ResetWorkOrderReport(ModelStateDictionary modelState, VM_L2L3WorkOrderReport workOrderDefinition)
    //{
    //  throw new NotImplementedException();
    //}
  }

  public class WorkOrderDefinitionExcelRecord
  {
    public string WorkOrderName { get; set; }
    public string PreviousWorkOrderName { get; set; }
    public string OrderDeadline { get; set; }
    public string CustomerName { get; set; }
    public string BilletSize { get; set; }
    public string NumberOfBillets { get; set; }
    public string BilletWeight { get; set; }
    public string BilletLength { get; set; }
    public string TargetWorkOrderWeight { get; set; }
    public string TargetWorkOrderWeightMin { get; set; }
    public string TargetWorkOrderWeightMax { get; set; }
    public string SteelGradeCode { get; set; }
    public string HeatName { get; set; }
    public string InputThickness { get; set; }
    public string InputWidth { get; set; }
    public string InputShapeSymbol { get; set; }
    public string OutputShapeSymbol { get; set; }
    public string OutputThickness { get; set; }
    public string OutputWidth { get; set; }
    public string BundleWeightMin { get; set; }
    public string BundleWeightMax { get; set; }
    public string Check { get; set; }
  }
}
