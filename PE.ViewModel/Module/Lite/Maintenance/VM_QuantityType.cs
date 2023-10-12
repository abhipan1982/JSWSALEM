using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Maintenance
{
  public class VM_QuantityType : VM_Base
  {
    public VM_QuantityType()
    {
      UnitConverterHelper.ConvertToLocal(this);
    }

    //public VM_QuantityType(MNTQuantityType p)
    //{
    //  QuantityTypeId = p.QuantityTypeId;
    //  //TODOMN - refactor this
    //  //this.CreatedTs = p.CreatedTs;
    //  //this.LastUpdateTs = p.LastUpdateTs;
    //  QuantityTypeCode = p.QuantityTypeCode;
    //  QuantityTypeName = p.QuantityTypeName;
    //  FkUnitId = p.FKUnitId;
    //  FkExtUnitCategoryId = p.FKExtUnitCategoryId;
    //  UnitConverterHelper.ConvertToLocal(this);
    //}

    public long? QuantityTypeId { get; set; }

    [SmfDisplay(typeof(VM_QuantityType), "CreatedTs", "NAME_CreatedTs")]
    public DateTime? CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_QuantityType), "LastUpdateTs", "NAME_LastUpdateTs")]
    public DateTime? LastUpdateTs { get; set; }

    [SmfDisplay(typeof(VM_QuantityType), "QuantityTypeName", "NAME_QuantityTypeName")]
    public string QuantityTypeName { get; set; }

    [SmfDisplay(typeof(VM_QuantityType), "QuantityTypeCode", "NAME_QuantityTypeCode")]
    public string QuantityTypeCode { get; set; }

    [SmfDisplay(typeof(VM_QuantityType), "FkUnitId", "NAME_UnitName")]
    public long? FkUnitId { get; set; }

    [SmfDisplay(typeof(VM_QuantityType), "FkExtUnitCategoryId", "NAME_ExtUnitCategory")]
    public long? FkExtUnitCategoryId { get; set; }
  }
}
