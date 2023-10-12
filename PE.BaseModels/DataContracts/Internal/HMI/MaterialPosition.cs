using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.HMI
{
  [DataContract]
  public class DCMaterialPosition : DataContractBase
  {
    [DataMember] public List<AreaMaterialPosition> Areas { get; set; }
    [DataMember] public bool IsLaneStopped { get; set; }
    [DataMember] public bool IsSlowProduction { get; set; }
  }

  [DataContract]
  public class AreaSignals
  {
    [DataMember] public bool ModeProduction { get; set; }

    [DataMember] public bool ModeAdjustion { get; set; }

    [DataMember] public bool Simulation { get; set; }

    [DataMember] public bool AutomaticRelease { get; set; }

    [DataMember] public bool Empty { get; set; }

    [DataMember] public bool CobbleDetected { get; set; }

    [DataMember] public bool ModeLocal { get; set; }

    [DataMember] public bool CobbleDetectionSelected { get; set; }

    [DataMember] public double? Grading { get; set; }
  }

  [DataContract]
  public class AreaMaterialPosition
  {
    public AreaMaterialPosition()
    {
      Materials = new List<MaterialPosition>();
      Layers = new List<LayerElement>();
    }
    private int materialsHashCode => Materials.Any() ?
      $"{string.Join("-", Materials.Select(x => $"{x.RawMaterialId}-{x.Order}-{x.PositionOrder}-{x.IsVirtual}"))}".GetHashCode() :
      0;

    private int layersHashCode => Layers.Any() ?
      $"{string.Join("-", Layers.Select(x => $"{x.Id}-{x.PositionOrder}-{x.HasChanged}"))}".GetHashCode() :
      0;

    private int signalsHashCode => Signals != null ?
      $"{Signals.ModeProduction}-{Signals.ModeAdjustion}-{Signals.Simulation}-{Signals.AutomaticRelease}-{Signals.Empty}-{Signals.CobbleDetected}-{Signals.ModeLocal}-{Signals.CobbleDetectionSelected}".GetHashCode() :
      0;
    public int HashCode => $"{AreaId}-{materialsHashCode}-{signalsHashCode}-{layersHashCode}".GetHashCode();

    [DataMember] public int AreaId { get; set; }

    [DataMember] public List<MaterialPosition> Materials { get; set; }

    [DataMember] public AreaSignals Signals { get; set; }

    [DataMember] public List<LayerElement> Layers { get; set; }
  }

  [DataContract]
  public class MaterialPosition
  {
    [DataMember] public long RawMaterialId { get; set; }
    [DataMember] public string MaterialName { get; set; }

    [DataMember] public bool IsScrap { get; set; }

    [DataMember] public bool IsPartialScrap { get; set; }

    [DataMember] public double ScrapPercent { get; set; }

    [DataMember] public int Order { get; set; }

    [DataMember] public int PositionOrder { get; set; }

    [DataMember] public bool IsVirtual { get; set; }

    [DataMember] public short? Grading { get; set; }

    [DataMember] public short MaterialsSum { get; set; }
  }

  [DataContract]
  public class LayerElement
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public string Name { get; set; }

    [DataMember] public int PositionOrder { get; set; }

    [DataMember] public int MaterialsSum { get; set; }
    [DataMember] public bool HasChanged { get; set; }

    [DataMember] public bool IsEmpty { get; set; }

    [DataMember] public bool IsForming { get; set; }

    [DataMember] public bool IsFormed { get; set; }
  }
}
