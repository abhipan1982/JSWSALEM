using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.Common;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using PE.HMIWWW.ViewModel.Module.Lite.Quality;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.Notification;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductQualityMgmService : BaseService, IProductQualityMgmService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public ProductQualityMgmService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetProductHistoryList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (HmiContext ctx = new HmiContext())
      {
        //result = ctx.V_ProductionHistories
        //  .ToDataSourceLocalResult(request, modelState, x => new VM_ProductHistory(x));
      }

      return result;
    }

    public async Task<VM_Base> AssignQualityAsync(ModelStateDictionary modelState, long productId, short quality,
      List<long> defectIds)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      //UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCQualityAssignment dcQualityAssignment = new DCQualityAssignment();
      dcQualityAssignment.DefectCatalogueElementIds = defectIds;
      if (dcQualityAssignment.ProductIds is null)
      {
        dcQualityAssignment.ProductIds = new List<long>();
      }

      dcQualityAssignment.ProductIds.Add(productId);
      dcQualityAssignment.QualityFlag = ProductQuality.GetValue(quality);


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssignQualityAsync(dcQualityAssignment);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AssignRawMaterialQualityAsync(ModelStateDictionary modelState, long rawMaterialId,
      short quality, List<long> defectIds)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      //UnitConverterHelper.ConvertToSi(ref defectCatalogue);

      DCQualityAssignment dcQualityAssignment = new DCQualityAssignment();
      dcQualityAssignment.DefectCatalogueElementIds = defectIds;
      if (dcQualityAssignment.RawMaterialIds is null)
      {
        dcQualityAssignment.RawMaterialIds = new List<long>();
      }

      dcQualityAssignment.RawMaterialIds.Add(rawMaterialId);
      dcQualityAssignment.QualityFlag = ProductQuality.GetValue(quality);


      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssignQualityAsync(dcQualityAssignment);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public DataSourceResult GetDefectsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.QTYDefectCatalogues.Include(x => x.FKParentDefectCatalogue)
        .Include(x => x.FKDefectCatalogueCategory)
        .ToDataSourceLocalResult(request, modelState, x => new VM_DefectCatalogue(x));

      return result;
    }

    public Dictionary<int, string> GetProductQualityList()
    {
      Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

      try
      {
        foreach (var propertyItem in typeof(ProductQuality).GetFields(BindingFlags.Public |
          BindingFlags.Static))
        {
          dynamic item = propertyItem.GetValue(null);

          resultDictionary.Add((int)item.Value, ResxHelper.GetResxByKey((ProductQuality)item.Value));
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(CommonAlarmDefs.AlarmCode_ExceptionInViewBagMethod,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message), MethodBase.GetCurrentMethod().Name, ex.Message);
        NotificationController.LogException(ex,
          String.Format("Exception in view bag method: {0}. Exception: {1}", MethodBase.GetCurrentMethod().Name,
            ex.Message));
      }

      return resultDictionary;
    }

    public DataSourceResult GetProductDefectsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long productId)
    {
      IQueryable<V_DefectsSummary> productDefectsList =
        _hmiContext.V_DefectsSummaries.Where(x => x.ProductId == productId).AsQueryable();

      return productDefectsList.ToDataSourceLocalResult(request, modelState, data => new VM_ProductDefect(data));
    }
  }
}
