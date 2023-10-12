using PE.BaseDbEntity.EnumClasses;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCSignalDefinition
  {
    /// <summary>
    /// Unique identifier of a signal. If categories are required the name should be separated by dots (e.g. CCM.Mold.CastingSpeed)
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Signal-description which will be displayed in MSS
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Just for displaying in MSS, no automatic unit-conversion
    /// </summary>
    public string Unit { get; set; }
    /// <summary>
    /// Type to filter rules-parameters for signals (e.g. only Numeric/TimeSeries/NumericSeries can be mapped as TSignalType.NUMERIC)
    /// </summary>
    public QESignalType Type { get; set; }
  }

  public enum SignalType
  {
    NUMERIC = 1,
    TEXT = 2,
    BOOLEAN = 3,
    TIMESTAMP = 4,
    JSON_OBJECT = 5,
    RATING = 6,
  }
}
