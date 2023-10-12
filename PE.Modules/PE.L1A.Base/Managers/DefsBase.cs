using PE.Common;

namespace PE.L1A.Base.Managers
{
  public class AlarmDefsBase
  {
    public const string RecordNotUnique = ModuleAlarmDefsBase.AlarmCode_L1AdapterRecordNotUnique;
    public const string ExistingReferenceViolation = ModuleAlarmDefsBase.AlarmCode_L1AdapterExistingReferenceViolation;
    public const string RecordNotFound = ModuleAlarmDefsBase.AlarmCode_L1AdapterRecordNotFound;
    public const string UnexpectedError = ModuleAlarmDefsBase.AlarmCode_L1AdapterUnexpectedError;
    public const string MessageToL1NotSent = ModuleAlarmDefsBase.AlarmCode_MessageToL1NotSent;
    public const string CannotInitConnectionWithOPCServer = ModuleAlarmDefsBase.AlarmCode_CannotInitConnectionWithOPCServer;
    public const string ConnectionWithOPCServerInitialized = ModuleAlarmDefsBase.AlarmCode_ConnectionWithOPCServerInitialized;
    public const string ConnectionWithOPCServerStarted = ModuleAlarmDefsBase.AlarmCode_ConnectionWithOPCServerStarted;
    public const string CannotStartConnectionWithOPCServer = ModuleAlarmDefsBase.AlarmCode_CannotStartConnectionWithOPCServer;
    public const string CannotGetSubscriptionForOPCServer = ModuleAlarmDefsBase.AlarmCode_CannotGetSubscriptionForOPCServer;
    public const string CannotReadL1TagsBecauseModuleIsNotInitialized = ModuleAlarmDefsBase.AlarmCode_CannotReadL1TagsBecauseModuleIsNotInitialized;
    public const string ErrorDuringProcessingMeasurement = ModuleAlarmDefsBase.AlarmCode_ErrorDuringProcessingMeasurement;
    public const string MeasurementsNotFoundForPoint = ModuleAlarmDefsBase.AlarmCode_MeasurementsNotFoundForPoint;
  }
}
