using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
  public class VM_Filters : VM_Base
  {
    public VM_Filters() { }

    public VM_Filters(STPSetupParameter setup)
    {
      SetupParameterId = setup.SetupParameterId;
      ParameterName = setup.FKParameter.ParameterName;
      TableName = setup.FKParameter.TableName;
      ColumnId = setup.FKParameter.ColumnId;
      ColumnValue = setup.FKParameter.ColumnValue;
      ParameterValue = setup.ParameterValueId;
      IsRequierd = setup.IsRequired ?? false;
    }

    public VM_Filters(STPSetupTypeParameter setupType, long id, long value)
    {
      SetupParameterId = id;
      ParameterName = setupType.FKParameter.ParameterName;
      ParameterNameTranslated = ResxHelper.GetResxByKey("NAME_" + ParameterName.Replace(" ", ""));
      TableName = setupType.FKParameter.TableName ?? ParameterName;
      ColumnId = setupType.FKParameter.ColumnId;
      ColumnValue = setupType.FKParameter.ColumnValue;
      ParameterValue = value;
      IsRequierd = setupType.DefaultIsRequired;
      SetupType = setupType.FKSetupTypeId;
      ParameterType = setupType.FKParameterId;
      ParameterCode = setupType.FKParameter.ParameterCode;
    }

    public VM_Filters(STPSetupTypeParameter setupType)
    {
      ParameterName = setupType.FKParameter.ParameterName;
      TableName = setupType.FKParameter.TableName ?? ParameterName;
      ParameterNameTranslated = ResxHelper.GetResxByKey("NAME_" + ParameterName.Replace(" ", ""));
      ColumnId = setupType.FKParameter.ColumnId;
      ColumnValue = setupType.FKParameter.ColumnValue;
      IsRequierd = setupType.DefaultIsRequired;
      SetupType = setupType.FKSetupTypeId;
      ParameterType = setupType.FKParameterId;
    }

    public long? SetupParameterId { get; set; }
    [SmfDisplay(typeof(VM_Filters), "ParameterName", "NAME_ParameterName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string ParameterName { get; set; }
    public string ParameterNameTranslated { get; set; }
    [SmfDisplay(typeof(VM_Filters), "TableName", "NAME_TableName")]
    public string TableName { get; set; }
    public string ColumnId { get; set; }
    [SmfDisplay(typeof(VM_Filters), "ColumnValue", "NAME_ColumnValue")]
    public string ColumnValue { get; set; }
    [SmfDisplay(typeof(VM_Filters), "ParameterValueName", "NAME_ParameterValue")]
    public string ParameterValueName { get; set; }
    public long? ParameterValue { get; set; }
    public bool IsRequierd { get; set; }
    public long SetupType { get; set; }
    public long ParameterType { get; set; }
    public string ParameterCode { get; set; }
  }
}
