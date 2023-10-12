using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_WidgetConfigurations : VM_Base
  {
    #region ctor

    public VM_WidgetConfigurations() { }

    public VM_WidgetConfigurations(HMIWidgetConfiguration data, bool isCurrentUserAssigned = false)
    {
      WidgetConfigurationId = data.WidgetConfigurationId;
      WidgetId = data.FKWidgetId;
      OrderSeq = data.OrderSeq;
      WidgetName = data.FKWidget.WidgetName;
      WidgetFileName = data.FKWidget.WidgetFileName;
      IsCurrentUserAssigned = isCurrentUserAssigned;
      IsActive = data.IsActive ?? false;
    }

    public VM_WidgetConfigurations(HMIWidget data, bool isCurrentUserAssigned = false)
    {
      WidgetId = data.WidgetId;
      WidgetName = data.WidgetName;
      WidgetFileName = data.WidgetFileName;
      IsCurrentUserAssigned = isCurrentUserAssigned;
    }

    public VM_WidgetConfigurations(V_WidgetConfiguration data)
    {
      Widget = data.WidgetId;
      IsActive = data.IsActive ?? false;
      WidgetName = data.WidgetName;
      WidgetFileName = data.WidgetFileName;
      WidgetConfigurationId = data.WidgetConfigurationId;
      OrderSeq = data.OrderSeq ?? 1;
    }

    #endregion

    #region properties

    public long? WidgetConfigurationId { get; set; }

    [SmfDisplay(typeof(VM_WidgetConfigurations), "WidgetName", "NAME_WidgetName")]
    public string WidgetName { get; set; }

    [SmfDisplay(typeof(VM_WidgetConfigurations), "WidgetFileName", "NAME_WidgetFileName")]
    public string WidgetFileName { get; set; }

    public long WidgetId { get; set; }

    public long? Widget { get; set; }

    [SmfDisplay(typeof(VM_WidgetConfigurations), "IsActive", "NAME_Active")]
    public bool IsActive { get; set; }

    [SmfDisplay(typeof(VM_WidgetConfigurations), "IsCurrentUserAssigned", "NAME_Active")]
    public bool IsCurrentUserAssigned { get; set; }

    [SmfDisplay(typeof(VM_WorkOrderOverview), "OrderSeq", "NAME_OrderSeq")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    [SmfRequired]
    public short OrderSeq { get; set; }

    #endregion
  }
}
