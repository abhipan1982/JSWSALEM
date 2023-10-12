using PE.Common;

namespace PE.TRK.Base.Managers
{
  public class AlarmDefsBase
  {
    public const string CobbleDetected = ModuleAlarmDefsBase.AlarmCode_CobbleDetected;
    public const string ProductCreatonForMaterialFailed = ModuleAlarmDefsBase.AlarmCode_ProductCreatonForMaterialFailed;
    public const string ProductExistsForMaterial = ModuleAlarmDefsBase.AlarmCode_ProductExistsForMaterial;
    public const string CannotCreateProductForDummyMaterial = ModuleAlarmDefsBase.AlarmCode_CannotCreateProductForDummyMaterial;
    public const string CannotCreateProductForScrappedMaterial = ModuleAlarmDefsBase.AlarmCode_CannotCreateProductForScrappedMaterial;
    public const string ExistingReferenceViolation = ModuleAlarmDefsBase.AlarmCode_TrackingExistingReferenceViolation;
    public const string RecordNotFound = ModuleAlarmDefsBase.AlarmCode_TrackingRecordNotFound;
    public const string RecordNotUnique = ModuleAlarmDefsBase.AlarmCode_TrackingRecordNotUnique;
    public const string UnexpectedError = ModuleAlarmDefsBase.AlarmCode_TrackingUnexpectedError;
    public const string LayerNotCreatedBecauseAreaIsNotLayerArea = ModuleAlarmDefsBase.AlarmCode_LayerNotCreatedBecauseAreaIsNotLayerArea;
    public const string ParentRawMaterialNotFoundInPoint = ModuleAlarmDefsBase.AlarmCode_ParentRawMaterialNotFoundInPoint;
    public const string RecordNotUniqueDuringChildCreation = ModuleAlarmDefsBase.AlarmCode_RecordNotUniqueDuringChildCreation;
    public const string ExistingReferenceViolationDuringChildCreation = ModuleAlarmDefsBase.AlarmCode_ExistingReferenceViolationDuringChildCreation;
    public const string UnexpectedErrorDuringChildCreation = ModuleAlarmDefsBase.AlarmCode_UnexpectedErrorDuringChildCreation;
    public const string RecordNotUniqueDuringLastDivideVerification = ModuleAlarmDefsBase.AlarmCode_RecordNotUniqueDuringLastDivideVerification;
    public const string ExistingReferenceViolationDuringLastDivideVerification = ModuleAlarmDefsBase.AlarmCode_ExistingReferenceViolationDuringLastDivideVerification;
    public const string UnexpectedErrorDuringLastDivideVerification = ModuleAlarmDefsBase.AlarmCode_UnexpectedErrorDuringLastDivideVerification;
    public const string MaterialNotFoundOnPoint = ModuleAlarmDefsBase.AlarmCode_MaterialNotFoundOnPoint;
    public const string RecodrNotUniqueWhileRejectingRawMaterial = ModuleAlarmDefsBase.AlarmCode_RecodrNotUniqueWhileRejectingRawMaterial;
    public const string ExistingReferenceViolationDuringRawMaterialReject = ModuleAlarmDefsBase.AlarmCode_ExistingReferenceViolationDuringRawMaterialReject;
    public const string UnexpectedErrorDuringRawMaterialReject = ModuleAlarmDefsBase.AlarmCode_UnexpectedErrorDuringRawMaterialReject;
    public const string AssetIsNotLayer = ModuleAlarmDefsBase.AlarmCode_AssetIsNotLayer;
    public const string NothingDischargedFromLayer = ModuleAlarmDefsBase.AlarmCode_NothingDischargedFromLayer;
    public const string MaterialNotRemovedFromArea = ModuleAlarmDefsBase.AlarmCode_MaterialNotRemovedFromArea;
    public const string MaterialInChargingGridNotInCollectionType = ModuleAlarmDefsBase.AlarmCode_MaterialInChargingGridNotInCollectionType;
    public const string MaterialNotUndischargeFromChargingGrid = ModuleAlarmDefsBase.AlarmCode_MaterialNotUndischargeFromChargingGrid;
    public const string MaterialNotChargedToChargingGrid = ModuleAlarmDefsBase.AlarmCode_MaterialNotChargedToChargingGrid;
    public const string MaterialNotUnchargedFromFurnace = ModuleAlarmDefsBase.AlarmCode_MaterialNotUnchargedFromFurnace;
    public const string MaterialNotChargedToFurnace = ModuleAlarmDefsBase.AlarmCode_MaterialNotChargedToFurnace;
    public const string MaterialNotDischargedFromFurnace = ModuleAlarmDefsBase.AlarmCode_MaterialNotDischargedFromFurnace;
    public const string MaterialNotUndischargedFromFurnace = ModuleAlarmDefsBase.AlarmCode_MaterialNotUndischargedFromFurnace;
    public const string CannotFindMaterialToUndischarge = ModuleAlarmDefsBase.AlarmCode_CannotFindMaterialToUndischarge;
    public const string MaterialNotChargedToFurnaceExit = ModuleAlarmDefsBase.AlarmCode_MaterialNotChargedToFurnaceExit;
    public const string MaterialNotHardRemoved = ModuleAlarmDefsBase.AlarmCode_MaterialNotHardRemoved;
    public const string ProductCannotBeUndone = ModuleAlarmDefsBase.AlarmCode_ProductCannotBeUndone;
    public const string ProductUndoFailed = ModuleAlarmDefsBase.AlarmCode_ProductUndoFailed;
    public const string MaterialNotAssigned = ModuleAlarmDefsBase.AlarmCode_MaterialNotAssigned;
    public const string MaterialNotUnassigned = ModuleAlarmDefsBase.AlarmCode_MaterialNotUnassigned;
    public const string AreaNotMoveable = ModuleAlarmDefsBase.AlarmCode_AreaNotMoveable;
    public const string AreaMoveForwardFailed = ModuleAlarmDefsBase.AlarmCode_AreaMoveForwardFailed;
    public const string AreaMoveBackwardFailed = ModuleAlarmDefsBase.AlarmCode_AreaMoveBackwardFailed;
    public const string CannotFindLayerInPosition = ModuleAlarmDefsBase.AlarmCode_CannotFindLayerInPosition;
    public const string LayerStatusPreventsPaste = ModuleAlarmDefsBase.AlarmCode_LayerStatusPreventsPaste;
    public const string CannotReplaceMaterialPositionForArea = ModuleAlarmDefsBase.AlarmCode_CannotReplaceMaterialPositionForArea;
    public const string ReplaceMaterialPositionFailed = ModuleAlarmDefsBase.AlarmCode_ReplaceMaterialPositionFailed;
    public const string RemoveMaterialFromAllAreasFailed = ModuleAlarmDefsBase.AlarmCode_RemoveMaterialFromAllAreasFailed;
    public const string LayerWithMaterialNotFound = ModuleAlarmDefsBase.AlarmCode_LayerWithMaterialNotFound;
    public const string LayerInfoNotInValidType = ModuleAlarmDefsBase.AlarmCode_LayerInfoNotInValidType;
    public const string MaterialCutFailed = ModuleAlarmDefsBase.AlarmCode_MaterialCutFailed;
    public const string MessageToL1NotPrepared = ModuleAlarmDefsBase.AlarmCode_MessageToL1NotPrepared;
    public const string UnexpectedExceptionDuringOCRMessageProcessing = ModuleAlarmDefsBase.AlarmCode_UnexpectedExceptionDuringOCRMessageProcessing;
    public const string ProductForLabelNotFound = ModuleAlarmDefsBase.AlarmCode_ProductForLabelNotFound;
    public const string ShapShearCageClosed = ModuleAlarmDefsBase.AlarmCode_ShapShearCageClosed;
    public const string FailedToSendAccumulatedEquipmentUsageForMaterial = ModuleAlarmDefsBase.AlarmCode_FailedToSendAccumulatedEquipmentUsageForMaterial;
    public const string FailedToSendAccumulatedRollsUsageForMaterial = ModuleAlarmDefsBase.AlarmCode_FailedToSendAccumulatedRollsUsageForMaterial;
    public const string WorkOrderStatusPreventsBundleCreation = ModuleAlarmDefsBase.AlarmCode_WorkOrderStatusPreventsBundleCreation;
    public const string SomethingWentWrongWhileCreatingBundleForWorkOrder = ModuleAlarmDefsBase.AlarmCode_SomethingWentWrongWhileCreatingBundleForWorkOrder;
  }
}
