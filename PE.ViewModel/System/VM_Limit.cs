using System;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.EnumClasses;
using SMF.DbEntity.Models;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Limit : VM_Base
  {
    public VM_Limit()
    {
    }

    public VM_Limit(Limit limit)
    {
      Id = limit.LimitId;
      Description = limit.Description;
      Name = limit.Name;
      GroupId = limit.ParameterGroupId;
      GroupName = limit.ParameterGroup.Name;
      switch (limit.EnumParameterValueType)
      {
        case var value when value == LimitValueType.ValueDate.Value:
          LowerValue = limit.LowerValueDate.ToString();
          UpperValue = limit.UpperValueDate.ToString();
          break;
        case var value when value == LimitValueType.ValueFloat.Value:
          LowerValue = limit.LowerValueFloat.ToString();
          UpperValue = limit.UpperValueFloat.ToString();
          break;
        case var value when value == LimitValueType.ValueInt.Value:
        default:
          LowerValue = limit.LowerValueInt.ToString();
          UpperValue = limit.UpperValueInt.ToString();
          break;
      }

      if (limit.Unit != null)
      {
        Unit = limit.Unit.UnitSymbol;
      }

      Type = limit.EnumParameterValueType.ToString();
      ConvertToLocal(this);
    }

    [SmfDisplay(typeof(VM_Limit), "Id", "NAME_Id")]
    public virtual Int64 Id { get; set; }

    [SmfDisplay(typeof(VM_Limit), "Description", "NAME_Description")]
    public virtual String Description { get; set; }

    [SmfDisplay(typeof(VM_Limit), "Name", "NAME_Name")]
    public virtual String Name { get; set; }

    [SmfDisplay(typeof(VM_Limit), "GroupName", "NAME_GroupName")]
    public virtual String GroupName { get; set; }

    [SmfDisplay(typeof(VM_Limit), "GroupId", "NAME_Id")]
    public virtual Int64 GroupId { get; set; }

    [SmfDisplay(typeof(VM_Limit), "LowerValue", "NAME_LowerValue")]
    public virtual String LowerValue { get; set; }

    [SmfDisplay(typeof(VM_Limit), "UpperValue", "NAME_UpperValue")]
    public virtual String UpperValue { get; set; }

    [SmfDisplay(typeof(VM_Limit), "Type", "NAME_LimitType")]
    public virtual String Type { get; set; }

    [SmfDisplay(typeof(VM_Limit), "Unit", "NAME_Unit")]
    public virtual String Unit { get; set; }
  }
  //public class VM_LimitList : VM_BaseList<VM_Limit>
  //{
  //	public VM_LimitList()
  //	{
  //	}
  //	public VM_LimitList(IList<Limit> dbClass)
  //	{
  //		foreach (Limit item in dbClass)
  //		{
  //			this.Add(new VM_Limit(item));
  //		}
  //	}
  //}
}
