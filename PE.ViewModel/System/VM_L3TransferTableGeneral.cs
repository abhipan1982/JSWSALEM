using System.ComponentModel.DataAnnotations;
using PE.DbEntity.TransferModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L3TransferTableGeneral : VM_Base
  {
    #region properties

    public long OrderSeq { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3TransferTableGeneral), "TransferTableName", "NAME_TableName")]
    public virtual string TransferTableName { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3TransferTableGeneral), "StatusNew", "ENUM_CommStatus_New")]
    public virtual long? StatusNew { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3TransferTableGeneral), "StatusInProc", "ENUM_CommStatus_BeingProcessed")]
    public virtual long? StatusInProc { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3TransferTableGeneral), "StatusOK", "ENUM_CommStatus_OK")]
    public virtual long? StatusOK { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3TransferTableGeneral), "StatusValErr", "ENUM_CommStatus_ValidationError")]
    public virtual long? StatusValErr { get; set; }

    [Editable(false)]
    [SmfDisplay(typeof(VM_L3TransferTableGeneral), "StatusProcErr", "ENUM_CommStatus_ProcessingError")]
    public virtual long? StatusProcErr { get; set; }

    #endregion

    #region ctor

    public VM_L3TransferTableGeneral() { }

    public VM_L3TransferTableGeneral(V_L3L2TransferTablesSummary l3L2TransferTablesSummary)
    {
      OrderSeq = l3L2TransferTablesSummary.OrderSeq;
      TransferTableName = l3L2TransferTablesSummary.TransferTableName;
      StatusNew = l3L2TransferTablesSummary.StatusNew;
      StatusInProc = l3L2TransferTablesSummary.StatusInProc;
      StatusOK = l3L2TransferTablesSummary.StatusOK;
      StatusValErr = l3L2TransferTablesSummary.StatusValErr;
      StatusProcErr = l3L2TransferTablesSummary.StatusProcErr;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion
  }
}
