using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Helpers;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.Models;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Core.HtmlHelpers
{
  public static class ListValuesHelper
  {
    // ENUM
    public static SelectList GetShiftsTs()
    {
      using (PEContext uow = new PEContext())
      {
        IList<EVTShiftCalendar> shifts = uow.EVTShiftCalendars
          .Include(q => q.FKCrew)
          .OrderByDescending(q => q.IsActualShift)
          .Take(3)
          .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (EVTShiftCalendar lm in shifts)
        {
          resultDictionary.Add((int)lm.ShiftCalendarId,
            String.Format("{0} - {1} - {2}",
              lm.PlannedStartTime.ToString("m") + " " + lm.PlannedStartTime.ToString("t"),
              lm.PlannedEndTime.ToString("m") + " " + lm.PlannedEndTime.ToString("t"), lm.FKCrew.CrewName));
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetGrooveConditionEnum()
    {
      return SelectListHelpers.GetSelectList<GrooveCondition, int>();
    }

    public static SelectList GetScheduleStatuses()
    {
      return SelectListHelpers.GetSelectList<WorkOrderStatus, int>();
    }

    public static SelectList GetCorrectStatus()
    {
      return SelectListHelpers.GetSelectList<CorrectType, int>();
    }

    public static SelectList GetTextFromTransferTableDataReadingStatus()
    {
      return SelectListHelpers.GetSelectList<TransferTableDataReadingStatus, int>();
    }

    public static SelectList GetTextFromProcessingMessageStatus()
    {
      return SelectListHelpers.GetSelectList<ProcessingMessageStatus, int>();
    }

    public static SelectList GetAlarmTypes()
    {
      return SelectListHelpers.GetSelectList<AlarmType, int>();
    }

    public static SelectList GetTelegramNames()
    {
      return SelectListHelpers.GetSelectList<TelegramIds, int>();
    }

    public static SelectList GetTextFromYesNoStatus()
    {
      return SelectListHelpers.GetSelectList<YesNo, int>();
    }

    public static SelectList GetDeviceStatuses()
    {
      return SelectListHelpers.GetSelectList<DeviceAvailability, int>();
    }

    public static SelectList GetCrews()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<EVTCrew> crews = ctx.EVTCrews.Where(z => z.CrewName == "A" || z.CrewName == "B" || z.CrewName == "C").ToList();
        return new SelectList(crews, "CrewId", "CrewName");
      }
    }

    public static SelectList GetShiftLayouts()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<EVTShiftLayout> list = ctx.EVTShiftLayouts.ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (EVTShiftLayout item in list)
        {
          resultDictionary.Add((int)item.ShiftLayoutId,
             ResxHelper.GetResxByKey(string.Format("SHIFT_LAYOUT_{0}", item.ShiftLayoutCode)));
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }

    }

    public static SelectList GetSetups()
    {
      using PEContext ctx = new PEContext();
      IList<STPSetup> setups = ctx.STPSetups.ToList();
      return new SelectList(setups, "SetupId", "SetupName");
    }

    public static SelectList GetFeaturesDefinitions()
    {
      throw new NotImplementedException();
      //IList<MVHFeature> features = new List<MVHFeature>();

      //using (PEContext ctx = new PEContext())
      //{

      //  features = ctx.MVHFeatures
      //    .Include(fa => fa.MVHFeaturesEXT).Include(fa => fa.Asset).OrderBy(fa => fa.Asset.OrderSeq).ThenBy(fa => fa.FeatureName)
      //    .ToList();

      //  Dictionary<long, string> outputDictionary = new Dictionary<long, string>();
      //  foreach (MVHFeature f in features)
      //  {
      //    outputDictionary.Add(f.FeatureId, $"{f.Asset.AssetName} - {f.FeatureName}");
      //  }

      //  return new SelectList(outputDictionary, "Key", "Value");
      //}
    }

    public static SelectList GetRollStatus()
    {
      return SelectListHelpers.GetSelectList<RollStatus, int>();
    }

    public static SelectList GetGrooveTemplatesShortList()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSGrooveTemplate> equipmentgroup = ctx.RLSGrooveTemplates.ToList();

        SelectList tmpSelectList = new SelectList(equipmentgroup, "GrooveTemplateId", "GrooveTemplateCode");

        return tmpSelectList;
      }
    }

    public static SelectList GetRollScrapReasons()
    {
      return SelectListHelpers.GetSelectList<RollScrapReason, int>();
    }

    public static SelectList GetRollStatusWithoutUndef()
    {
      return SelectListHelpers.GetSelectList<RollStatus, int>();
    }

    public static SelectList GetShapeList()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<PRMShape> shapelist = ctx.PRMShapes.ToList();

        SelectList tmpshapeList = new SelectList(shapelist, "ShapeId", "ShapeName");

        return tmpshapeList;
      }
    }

    public static SelectList GetUnitsList()
    {
      using (SMFContext ctx = new SMFContext())
      {
        IList<UnitOfMeasure> unitsList = ctx.UnitOfMeasures.ToList();

        SelectList tmpshapeList = new SelectList(unitsList, "UnitId", "Name");

        return tmpshapeList;
      }
    }

    public static SelectList GetRollWithTypes()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSRollType> products = ctx.RLSRollTypes.OrderBy(o => o.MatchingRollsetType).ThenBy(o => o.RollTypeName)
          .ThenBy(o => o.DiameterMin).ToList();
        return new SelectList(products, "RollTypeId", "RollTypeName");
      }
    }

    public static SelectList GetEquipmentStatuses()
    {
      return SelectListHelpers.GetSelectList<EquipmentStatus, int>();
    }

    public static SelectList GetSeverityStatuses()
    {
      return SelectListHelpers.GetSelectList<Severity, int>();
    }

    public static SelectList GetEquipmentServiceTypes()
    {
      return SelectListHelpers.GetSelectList<ServiceType, int>();
    }

    public static SelectList GetRollSetStatus()
    {
      return SelectListHelpers.GetSelectList<RollSetStatus, int>();
    }

    public static SelectList GetRollSetStatusShortList()
    {
      return SelectListHelpers.GetSelectList<RollSetStatus, int>(SelectListHelpers.SelectListMode.Include,
        RollSetStatus.NotAvailable.Value, RollSetStatus.Ready.Value);
    }

    public static SelectList GetRollSetHistoryStatus()
    {
      return SelectListHelpers.GetSelectList<RollSetHistoryStatus, int>();
    }

    public static SelectList GetRollsetEmpty()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSRollSet> rollsets = ctx.RLSRollSets
          .Where(f => f.EnumRollSetStatus == RollSetStatus.Empty)
          .OrderBy(p => p.RollSetName)
          .ToList();

        if (rollsets.Count != 0)
        {
          return new SelectList(rollsets, "RollSetId", "RollSetName");
        }

        return null;
      }
    }

    public static SelectList GetRollsetTypeList()
    {
      return SelectListHelpers.GetSelectList<RollSetType, int>();
    }

    public static SelectList GetCassetteStatusShortList()
    {
      return SelectListHelpers.GetSelectList<CassetteStatus, int>(SelectListHelpers.SelectListMode.Include,
        CassetteStatus.NotAvailable.Value, CassetteStatus.Empty.Value, CassetteStatus.InRegeneration.Value);
    }

    public static SelectList GetCassetteStatus()
    {
      return SelectListHelpers.GetSelectList<CassetteStatus, int>();
    }

    public static SelectList GetRollsReadyWithTypes()
    {
      using (HmiContext ctx = new HmiContext())
      {
        IList<V_Roll> rolls = ctx.V_Rolls
          .Where(f => f.EnumRollStatus == RollStatus.New.Value || f.EnumRollStatus == RollStatus.Unassigned.Value)
          .OrderBy(p => p.RollName).ToList();

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

        foreach (V_Roll rl in rolls)
        {
          if (!String.IsNullOrEmpty(rl.RollTypeName))
          {
            resultDictionary.Add(rl.RollId, String.Format("{0} / {1}", rl.RollName, rl.RollTypeName));
          }
          else
          {
            resultDictionary.Add(rl.RollId, String.Format("{0}", rl.RollName));
          }
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetNumberOfActiveRoll()
    {
      return SelectListHelpers.GetSelectList<int>(typeof(RollSetType), "GLOB_ROLLSETS_Number_of_Active_RollEnum_{0}");
    }

    public static SelectList GetCassetteArrangement()
    {
      return SelectListHelpers.GetSelectList<CassetteArrangement, int>();
    }

    public static SelectList GetCassetteType()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<RLSCassetteType> products = ctx.RLSCassetteTypes.ToList();
        return new SelectList(products, "CassetteTypeId", "CassetteTypeName");
      }
    }

    public static SelectList GetCassetteTypeEnum()
    {
      return SelectListHelpers.GetSelectList<CassetteType, int>();
    }

    public static SelectList GetGrooveSettingEnum()
    {
      return SelectListHelpers.GetSelectList<GrooveSetting, int>();
    }

    public static SelectList GetRollSetCounter()
    {
      using (HmiContext ctx = new HmiContext())
      {
        List<V_CassettesOverview> list = ctx.V_CassettesOverviews.ToList();
        List<IGrouping<long, V_CassettesOverview>> cassette = list.GroupBy(x => x.CassetteId).ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (IGrouping<long, V_CassettesOverview> lm in cassette)
        {
          resultDictionary.Add((int)lm.Key, lm.Count().ToString());
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetRollGrooveStatus()
    {
      return SelectListHelpers.GetSelectList<RollGrooveStatus, int>();
    }

    public static SelectList GetStandStat()
    {
      return SelectListHelpers.GetSelectList<StandStatus, int>();
    }

    public static SelectList GetGrooveTemplatesList()
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<GrooveTemplate> equipmentgroup = uow.Repository<GrooveTemplate>()
        //    .Query()
        //    .Get()
        //    .ToList();

        IList<RLSGrooveTemplate> templateList = ctx.RLSGrooveTemplates.ToList();
        SelectList tmpSelectList = new SelectList(templateList, "GrooveTemplateId", "GrooveTemplateName");

        return tmpSelectList;
      }
    }

    public static SelectList GetRollSetHistoryForId(long rollsetHistoryId)
    {
      using (PEContext ctx = new PEContext())
      {
        SelectList tmpSelectList = null;
        ;
        //RollSetHistory rollsetOveview = uow.Repository<RollSetHistory>().Query(z => z.RollSetHistoryId == rollsetHistoryId).GetSingle();
        RLSRollSetHistory rollSetOverview =
          ctx.RLSRollSetHistories.Where(z => z.RollSetHistoryId == rollsetHistoryId).FirstOrDefault();
        if (rollSetOverview != null)
        {
          //IList<RollSetHistory> rollsethistory = uow.Repository<RollSetHistory>().Query(x => (x.FkRollSetId == rollsetOveview.FkRollSetId)).OrderBy(o => o.OrderByDescending(g => g.RollSetHistoryId)).Get().ToList();
          IList<RLSRollSetHistory> rollsethistory = ctx.RLSRollSetHistories
            .Where(x => x.FKRollSetId == rollSetOverview.FKRollSetId).OrderBy(g => g.RollSetHistoryId).ToList();

          Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

          foreach (RLSRollSetHistory lm in rollsethistory)
          {
            if (lm.EnumRollSetHistoryStatus != RollSetHistoryStatus.Actual.Value)
            {
              resultDictionary.Add((int)lm.RollSetHistoryId, String.Format("{0}", lm.CreatedTs));
            }
            else
            {
              resultDictionary.Add((int)lm.RollSetHistoryId, ResxHelper.GetResxByKey(RollSetHistoryStatus.Actual));
            }
          }

          tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        }

        return tmpSelectList;
        //return new SelectList(rollsethistory, "RollSetHistoryId", "Mounted");
      }
    }

    public static SelectList GetRollSetHistory(long rollsetId)
    {
      using (PEContext ctx = new PEContext())
      {
        //IList<RollSetHistory> rollsethistory = uow.Repository<RollSetHistory>().Query(x => (x.FkRollSetId == rollsetId)).OrderBy(o => o.OrderByDescending(g => g.RollSetHistoryId)).Get().ToList();

        IList<RLSRollSetHistory> rollSetHistory = ctx.RLSRollSetHistories.Where(z => z.FKRollSetId == rollsetId)
          .OrderBy(g => g.RollSetHistoryId).ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        foreach (RLSRollSetHistory lm in rollSetHistory)
        {
          if (lm.EnumRollSetHistoryStatus != RollSetHistoryStatus.Actual.Value)
          {
            resultDictionary.Add((int)lm.RollSetHistoryId, String.Format("{0}", lm.CreatedTs));
          }
          else
          {
            resultDictionary.Add((int)lm.RollSetHistoryId, ResxHelper.GetResxByKey(RollSetHistoryStatus.Actual));
          }
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
        //return new SelectList(rollsethistory, "RollSetHistoryId", "Mounted");
      }
    }

    public static SelectList GetCassetteArrangementNoUndefined()
    {
      return SelectListHelpers.GetSelectList<CassetteArrangement, int>(SelectListHelpers.SelectListMode.Exclude,
        CassetteArrangement.Undefined.Value);
    }

    public static SelectList GetUnmountedCassettes()
    {
      using (HmiContext ctx = new HmiContext())
      {
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.New))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverviews
          .Where(x => x.EnumCassetteStatus == CassetteStatus.RollSetInside.Value || x.EnumCassetteStatus == CassetteStatus.New.Value)
          .OrderBy(p => p.CassetteName)
          .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
          if (!resultDictionary.ContainsKey((int)lm.CassetteId))
          {
            resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
          }
        }

        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetMountedCassettes()
    {
      using (HmiContext ctx = new HmiContext())
      {
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                            .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.MountedInStand))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverviews
          .Where(x => x.EnumCassetteStatus == CassetteStatus.MountedInStand.Value)
          .OrderBy(p => p.CassetteName)
          .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }

        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }
    public static SelectList GetCassettesReadyForMount()
    {
      using (HmiContext ctx = new HmiContext())
      {
        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverviews
          .Where(x => x.EnumCassetteStatus == CassetteStatus.RollSetInside.Value || x.EnumCassetteStatus == CassetteStatus.Empty.Value)
          .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
          .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
          if (!resultDictionary.ContainsKey((int)lm.CassetteId))
          {
            resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
          }
        }

        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }
    public static SelectList GetRollsetsReadyOnlyWithTypes(short? type = null)
    {
      using (HmiContext ctx = new HmiContext())
      {
        //IList<VRollsetOverviewNewest> rollsets = uow.Repository<VRollsetOverviewNewest>()
        //                                                                            .Query(f => f.RollSetStatus == (short)PE.Core.Constants.RollSetStatus.Ready)
        //                                                                            .OrderBy(o => o.OrderBy(p => p.RollSetName))
        //                                                                            .Get()
        //                                                                            .ToList();

        IList<V_RollSetOverview> rollsets = ctx.V_RollSetOverviews
          .Where(f => f.EnumRollSetStatus == RollSetStatus.Ready.Value && f.IsLastOne && f.IsLastOne)
          .OrderBy(p => p.RollSetName)
          .ToList();

        if (type != null)
        {
          rollsets = rollsets.Where(x => x.RollSetType == type).ToList();
        }

        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

        foreach (V_RollSetOverview rl in rollsets)
        {
          if (!String.IsNullOrEmpty(rl.UpperRollTypeName))
          {
            resultDictionary.Add(Convert.ToInt64(rl.RollSetId),
              String.Format("{0} / {1}", rl.RollSetName, rl.UpperRollTypeName));
          }
          else
          {
            resultDictionary.Add(Convert.ToInt64(rl.RollSetId), String.Format("{0}", rl.RollSetName));
          }
        }

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        //return new SelectList(cassette, "RollId", "RollName");
        return tmpSelectList;
      }
    }

    public static SelectList GetCassettesReadyForMountWith2Rolls( /*long standId*/)
    {
      using (HmiContext ctx = new HmiContext())
      {
        //var stand = uow.Repository<VActualStandConfiguration>().Query(x => x.StandId == standId).Get().FirstOrDefault();
        //if (stand == null)
        //{
        //    throw new NullReferenceException();
        //}
        //IList<VCassettesOverview> cassette = uow.Repository<VCassettesOverview>()
        //                                                                           // .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty) && (x.NumberOfRolls == stand.NumberOfRolls))
        //                                                                           .Query(x => ((x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty)) && (x.NumberOfRolls == (short)PE.Core.Constants.RollSetType.twoActiveRollsRM))
        //                                                                            .Get()
        //                                                                            .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
        //                                                                            .ToList();

        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverviews
          // .Query(x => (x.Status == (short)PE.Core.Constants.CassetteStatus.RollSetInside) || (x.Status == (short)PE.Core.Constants.CassetteStatus.Empty) && (x.NumberOfRolls == stand.NumberOfRolls))
          .Where(x => x.EnumCassetteStatus == CassetteStatus.RollSetInside.Value || x.EnumCassetteStatus == CassetteStatus.Empty.Value)
          .OrderBy(p => p.CassetteName).ThenBy(b => b.CassetteName)
          .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }

        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetStandActivity()
    {
      return SelectListHelpers.GetSelectList<StandActivity, int>();
    }

    public static SelectList GetStandStatNoUndefined()
    {
      return SelectListHelpers.GetSelectList<StandStatus, int>(SelectListHelpers.SelectListMode.Exclude,
        StandStatus.Undefined.Value);
    }

    public static SelectList GetCassetteReadyNewWithInitValue()
    {
      using (HmiContext ctx = new HmiContext())
      {
        IList<V_CassettesOverview> cassette = ctx.V_CassettesOverviews
          .Where(x => x.EnumCassetteStatus == CassetteStatus.Empty.Value || x.EnumCassetteStatus == CassetteStatus.New.Value)
          .OrderBy(p => p.CassetteName)
          .ToList();

        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose cassette");

        foreach (V_CassettesOverview lm in cassette)
        {
          resultDictionary.Add((int)lm.CassetteId, lm.CassetteName);
        }

        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetAvailableRollsets()
    {
      using (HmiContext ctx = new HmiContext())
      {
        IList<V_RollSetOverview> rollSets = ctx.V_RollSetOverviews
          .Where(x => x.EnumRollSetStatus == RollSetStatus.Ready.Value && x.IsLastOne && x.IsLastOne)
          .OrderBy(p => p.CassetteName)
          .ToList();
        Dictionary<int, string> resultDictionary = new Dictionary<int, string>();

        resultDictionary.Add(0, /*Resources.Framework.Global.GLOB_Name_CassetteChoose*/"Choose rollset");

        foreach (V_RollSetOverview rollset in rollSets)
        {
          resultDictionary.Add((int)rollset.RollSetId, rollset.RollSetName);
        }

        resultDictionary.ToList();

        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

        return tmpSelectList;
      }
    }

    public static SelectList GetDefectCatalogCategoriesList()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<QTYDefectCatalogueCategory> list = ctx.QTYDefectCatalogueCategories
          .OrderBy(o => o.DefectCatalogueCategoryCode).ToList();
        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        if (list != null)
        {
          foreach (QTYDefectCatalogueCategory el in list)
          {
            resultDictionary.Add(el.DefectCatalogueCategoryId, el.DefectCatalogueCategoryCode);
          }
        }

        resultDictionary.ToList();
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
      }
    }

    public static SelectList GetEventTypesList()
    {
      List<EventTypeSelectList> tupleList = new List<EventTypeSelectList>();

      using (PEContext ctx = new PEContext())
      {
        tupleList = ctx.EVTEventTypes
          .OrderBy(o => o.EventTypeCode)
          .Select(x => new EventTypeSelectList
          {
            EventTypeId = x.EventTypeId,
            EventTypeCode = x.EventTypeCode,
            ParentEventTypeId = x.FKParentEvenTypeId
          })
          .ToList();
      }

      if (tupleList.Any())
      {
        var childsHash = tupleList.ToLookup(cat => cat.ParentEventTypeId);

        foreach (var cat in tupleList)
        {
          cat.ChildEventTypes = childsHash[cat.EventTypeId].ToList();
        }

        var result = TraverseEventTypes(tupleList.Where(x => !x.ParentEventTypeId.HasValue))
          .ToDictionary(x => x.EventTypeCode, y => y.EventTypeName);

        SelectList tmpSelectList = new SelectList(result, "Key", "Value");

        return tmpSelectList;
      }

      return new SelectList(new Dictionary<long, string>(), "Key", "Value");
    }

    public static MultiSelectList GetDefectsMulitSelect()
    {
      using (PEContext ctx = new PEContext())
      {
        IList<QTYDefectCatalogue> list = ctx.QTYDefectCatalogues.ToList();
        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
        if (list != null)
        {
          foreach (QTYDefectCatalogue item in list)
          {
            resultDictionary.Add(item.DefectCatalogueId, item.DefectCatalogueCode);
          }
        }

        resultDictionary.ToList();
        MultiSelectList tmpSelectList = new MultiSelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
      }
    }

    public static SelectList GetProductQualityList()
    {
      return SelectListHelpers.GetSelectList<ProductQuality, int>();
    }

    public static SelectList GetWorkOrderStatusesList()
    {
      return SelectListHelpers.GetSelectList<WorkOrderStatus, int>();
    }

    public static SelectList GetRawMaterialStatusesList()
    {
      return SelectListHelpers.GetSelectList<RawMaterialStatus, int>();
    }

    public static SelectList GetLayerStatusesList()
    {
      return SelectListHelpers.GetSelectList<LayerStatus, int>();
    }

    public static SelectList GetCrashTestList()
    {
      return SelectListHelpers.GetSelectList<CrashTest, int>();
    }

    public static SelectList GetInspectionResultList()
    {
      return SelectListHelpers.GetSelectList<InspectionResult, int>();
    }

    public static SelectList GetRejectLocationList()
    {
      return SelectListHelpers.GetSelectList<RejectLocation, int>();
    }

    public static SelectList GetTypeOfScrapList()
    {
      return SelectListHelpers.GetSelectList<TypeOfScrap, int>();
    }

    public static SelectList GetFeatureTypeList()
    {
      return SelectListHelpers.GetSelectList<FeatureType, int>();
    }

    public static SelectList GetCommChannelTypeList()
    {
      return SelectListHelpers.GetSelectList<CommChannelType, int>();
    }

    public static SelectList GetAggregationStrategyList()
    {
      return SelectListHelpers.GetSelectList<AggregationStrategy, int>();
    }

    public static SelectList GetYardTypeList()
    {
      return SelectListHelpers.GetSelectList<YardType, int>();
    }

    public static SelectList GetTrackingAreaTypeList()
    {
      return SelectListHelpers.GetSelectList<TrackingAreaType, int>();
    }

    public static SelectList GetFeatureProviderList()
    {
      return SelectListHelpers.GetSelectList<FeatureProvider, int>();
    }

    public static SelectList GetTagValidationResultList()
    {
      return SelectListHelpers.GetSelectList<TagValidationResult, int>();
    }

    public static SelectList GetTrackingInstructionTypeList()
    {
      return SelectListHelpers.GetSelectList<TrackingInstructionType, int>();
    }

    public static SelectList GetCommStatusList()
    {
      return SelectListHelpers.GetSelectList<CommStatus, int>();
    }

    private static IEnumerable<EventTypeSelectList> TraverseEventTypes(IEnumerable<EventTypeSelectList> items,
      int level = 0)
    {
      foreach (var item in items)
      {
        item.EventTypeName = new StringBuilder().Insert(0, "&nbsp ", level * 2).ToString() +
                     ResxHelper.GetResxByKey(string.Format("EVENT_TYPE_{0}", item.EventTypeCode));

        yield return item;

        foreach (var c in TraverseEventTypes(item.ChildEventTypes, level + 1))
        {
          yield return c;
        }
      }
    }

    public static SelectList GetEquipmentGroupsList()
    {

      using var ctx = new PEContext();
      IList<MNTEquipmentGroup> equipmentgroup = ctx.MNTEquipmentGroups.OrderBy(o => o.EquipmentGroupCode).ToList();
      SelectList tmpSelectList = new SelectList(equipmentgroup, "EquipmentGroupId", "EquipmentGroupCode");
      return tmpSelectList;
    }

    //TODO Excluded new maintenance concept
    //public static SelectList GetComponentGroupsList()
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    SelectList tmpSelectList = null;
    //    IList<MNTComponentGroup> componentGroups = ctx.MNTComponentGroups.OrderBy(o => o.ComponentGroupCode).ToList();
    //    tmpSelectList = new SelectList(componentGroups, "ComponentGroupId", "ComponentGroupName");
    //    return tmpSelectList;
    //  }
    //}

    //public static SelectList GetDeviceGroupsList()
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    SelectList tmpSelectList = null;
    //    IList<MNTDeviceGroup> deviceGroups = ctx.MNTDeviceGroups.OrderBy(o => o.DeviceGroupName).ToList();
    //    tmpSelectList = new SelectList(deviceGroups, "DeviceGroupId", "DeviceGroupName");
    //    return tmpSelectList;
    //  }
    //}

    //public static SelectList GetSupplierList()
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    SelectList tmpSelectList = null;
    //    IList<MNTSupplier> supplierGroups = ctx.MNTSuppliers.OrderBy(o => o.SupplierName).ToList();
    //    tmpSelectList = new SelectList(supplierGroups, "SupplierId", "SupplierName");
    //    return tmpSelectList;
    //  }
    //}

    //public static SelectList GetIncidentTypesList()
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    IList<MNTIncidentType> incidentTypes =
    //      ctx.MNTIncidentTypes.OrderBy(o => o.AUDCreatedTs).ThenBy(o => o.IncidentTypeName).ToList();
    //    return new SelectList(incidentTypes, "IncidentTypeId", "IncidentTypeName");
    //  }
    //}

    //public static SelectList GetActionTypesList()
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    IList<MNTActionType> incidentTypes =
    //      ctx.MNTActionTypes.OrderBy(o => o.AUDCreatedTs).ThenBy(o => o.ActionTypeName).ToList();
    //    return new SelectList(incidentTypes, "ActionTypeId", "ActionTypeName");
    //  }
    //}

    //public static SelectList GetRecommendedActionsList()
    //{
    //  using (PEContext ctx = new PEContext())
    //  {
    //    IList<MNTRecomendedAction> recommendedActions =
    //      ctx.MNTRecomendedActions.OrderBy(o => o.RecomenedActionId).ToList();
    //    return new SelectList(recommendedActions, "RecommendedActionId", "RecommendedActionName");
    //  }
    //}
  }

  public class EventTypeSelectList
  {
    public EventTypeSelectList()
    {
      ChildEventTypes = new List<EventTypeSelectList>();
    }

    public long EventTypeId { get; set; }

    public short EventTypeCode { get; set; }

    public string EventTypeName { get; set; }

    public long? ParentEventTypeId { get; set; }

    public List<EventTypeSelectList> ChildEventTypes { get; set; }
  }
}
