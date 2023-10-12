using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Product
{
  public class VM_Shape : VM_Base
  {
    public VM_Shape()
    {
    }

    public VM_Shape(PRMShape s)
    {
      Id = s.ShapeId;
      Name = s.ShapeName;
      Description = s.ShapeDescription;
      ShapeCode = s.ShapeCode;
      IsDefault = s.IsDefault;

      UnitConverterHelper.ConvertToLocal(this);
    }

    public long Id { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyName", "NAME_Name")]
    public string Name { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyDescription", "NAME_Description")]
    public string Description { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeySymbol", "NAME_Symbol")]
    public string ShapeCode { get; set; }

    [SmfDisplay(typeof(VM_Shape), "KeyName", "NAME_IsDefault")]
    public bool IsDefault { get; set; }
  }
}
