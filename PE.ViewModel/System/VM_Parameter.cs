using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Parameter : VM_Base
  {
    public VM_Parameter()
    {
    }

    public VM_Parameter(Parameter parameter)
    {
      Id = parameter.ParameterId;
      Description = parameter.Description;
      Name = parameter.Name;
      GroupId = parameter.ParameterGroup.ParameterGroupId;
      GroupName = parameter.ParameterGroup.Name;
      if (parameter.EnumParameterValueType == ParameterValueType.ValueText.Value)
      {
        Value = parameter.ValueText;
      }
      else if (parameter.EnumParameterValueType == ParameterValueType.ValueDate.Value)
      {
        Value = parameter.ValueDate.ToString();
      }
      else if (parameter.EnumParameterValueType == ParameterValueType.ValueFloat.Value)
      {
        Value = parameter.ValueFloat.ToString();
      }
      else if (parameter.EnumParameterValueType == ParameterValueType.ValueInt.Value)
      {
        Value = parameter.ValueInt.ToString();
      }

      if (parameter.Unit != null)
      {
        Unit = parameter.Unit.UnitSymbol;
      }

      Type = parameter.EnumParameterValueType.ToString();
      ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_Parameter), "Id", "NAME_Id")]
    public virtual Int64 Id { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "Description", "NAME_Description")]
    public virtual String Description { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "Name", "NAME_Name")]
    public virtual String Name { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "GroupName", "NAME_GroupName")]
    public virtual String GroupName { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "GroupId", "NAME_GroupId")]
    public virtual Int64 GroupId { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "Value", "NAME_Value")]
    public virtual String Value { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "Type", "NAME_Type")]
    public virtual String Type { get; set; }

    [SmfDisplay(typeof(VM_Parameter), "Unit", "NAME_Unit")]
    public virtual String Unit { get; set; }
  }

  //public class VM_ParameterList : VM_BaseList<VM_Parameter>
  //{
  //	public VM_ParameterList()
  //	{
  //	}
  //	public VM_ParameterList(IList<Parameter> dbClass)
  //	{
  //		foreach (Parameter item in dbClass)
  //		{
  //			this.Add(new VM_Parameter(item));
  //		}
  //	}
  //}
}
