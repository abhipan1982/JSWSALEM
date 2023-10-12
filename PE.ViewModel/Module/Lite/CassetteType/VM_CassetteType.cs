using System;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.CassetteType
{
  public class VM_CassetteType : VM_Base
  {
    #region ctor

    public VM_CassetteType()
    {
      NumberOfRolls = 1;
    }

    public VM_CassetteType(RLSCassetteType cassetType)
    {
      CassetteTypeId = cassetType.CassetteTypeId;
      CassetteTypeName = cassetType.CassetteTypeName;
      CassetteTypeDescription = cassetType.CassetteTypeDescription;
      NumberOfRolls = cassetType.NumberOfRolls;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion

    #region properties

    public virtual long? CassetteTypeId { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_CassetteType), "CassetteTypeName", "NAME_Name")]
    public virtual String CassetteTypeName { get; set; }

    [SmfDisplay(typeof(VM_CassetteType), "CassetteTypeDescription", "NAME_Description")]
    public virtual String CassetteTypeDescription { get; set; }

    [SmfRequired]
    [SmfDisplay(typeof(VM_CassetteType), "NumberOfRolls", "NAME_NumberOfRolls")]
    [SmfFormat("FORMAT_Integer", NullDisplayText = "-", HtmlEncode = false)]
    public virtual short? NumberOfRolls { get; set; }

    #endregion
  }
}
