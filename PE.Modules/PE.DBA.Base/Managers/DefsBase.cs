using PE.Common;

namespace PE.DBA.Base.Managers
{
  public class AlarmDefsBase
  {
    public const string RecordNotUnique = ModuleAlarmDefsBase.AlarmCode_DataBaseAdapterRecordNotUnique;
    public const string ExistingReferenceViolation = ModuleAlarmDefsBase.AlarmCode_DataBaseAdapterExistingReferenceViolation;
    public const string RecordNotFound = ModuleAlarmDefsBase.AlarmCode_DataBaseAdapterRecordNotFound;
    public const string UnexpectedError = ModuleAlarmDefsBase.AlarmCode_DataBaseAdapterUnexpectedError;
    public const string TransferDataFromTransferTableToAdapter = ModuleAlarmDefsBase.AlarmCode_TransferDataFromTransferTableToAdapter;
    public const string TimeoutDuringProcessingWorkOrderTransferTable = ModuleAlarmDefsBase.AlarmCode_TimeoutDuringProcessingWorkOrderTransferTable;
    public const string WorkOrderDefinitionUpdateError = ModuleAlarmDefsBase.AlarmCode_WorkOrderDefinitionUpdateError;
    public const string WorkOrderDefinitionNotUpdatable = ModuleAlarmDefsBase.AlarmCode_WorkOrderDefinitionNotUpdatable;
    public const string WorkOrderDefinitionAlreadyProcessed = ModuleAlarmDefsBase.AlarmCode_WorkOrderDefinitionAlreadyProcessed;
    public const string WorkOrderDefinitionDeleteError = ModuleAlarmDefsBase.AlarmCode_WorkOrderDefinitionDeleteError;
    public const string WorkOrderReportNotResetted = ModuleAlarmDefsBase.AlarmCode_WorkOrderReportNotResetted;
    public const string ProductReportNotResetted = ModuleAlarmDefsBase.AlarmCode_ProductReportNotResetted;
    public const string WorkOrderReportNotSent = ModuleAlarmDefsBase.AlarmCode_WorkOrderReportNotSent;
    public const string ProductReportNotSent = ModuleAlarmDefsBase.AlarmCode_ProductReportNotSent;
  }
}
