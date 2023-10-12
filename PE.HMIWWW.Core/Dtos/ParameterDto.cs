using System;

namespace PE.HMIWWW.Core.Dtos
{
  public class ParameterDto
  {
    public long ParameterId { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public long ParameterGroupId { get; set; }
    public string ParameterGroupName { get; set; }
    public int ValueType { get; set; }
    public string ValueText { get; set; }
    public DateTime? ValueDate { get; set; }
    public double? ValueFloat { get; set; }
    public int? ValueInt { get; set; }
    public string UnitSymbol { get; set; }
  }
}
