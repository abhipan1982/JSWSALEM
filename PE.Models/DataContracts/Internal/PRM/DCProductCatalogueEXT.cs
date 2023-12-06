using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.Models;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.PRM
{
  [DataContract]
  [Serializable]
  public class DCProductCatalogueEXT:DataContractBase
  {
    [DataMember]
    public long FKProductCatalogueId { get; set; }
    [DataMember]
    public double? MinOvality { get; set; }
    [DataMember]
    public double? MinDiameter { get; set; }
    [DataMember]
    public double? MaxDiameter { get; set; }
    [DataMember]
    public double? Diameter { get; set; }
    [DataMember]
    public double? NegRcsSide { get; set; }
    [DataMember]
    public double? PosRcsSide { get; set; }
    [DataMember]
    public double? MinSquareness { get; set; }
    [DataMember]
    public double? MaxSquareness { get; set; }
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string ProductExternalCatalogueName { get; set; }

    [DataMember]
    public long Id { get; set; }

    [DataMember]
    public double? Length { get; set; }

    [DataMember]
    public double? LengthMax { get; set; }

    [DataMember]
    public double? LengthMin { get; set; }

    [DataMember]
    public double? Width { get; set; }

    [DataMember]
    public double? WidthMax { get; set; }

    [DataMember]
    public double? WidthMin { get; set; }

    [DataMember]
    public double Thickness { get; set; }

    [DataMember]
    public double ThicknessMax { get; set; }

    [DataMember]
    public double ThicknessMin { get; set; }

    [DataMember]
    public double? WeightMax { get; set; }

    [DataMember]
    public double? WeightMin { get; set; }

    [DataMember]
    public double? OvalityMax { get; set; }

    [DataMember]
    public string ProductCatalogueDescription { get; set; }

    [DataMember]
    public string Shape { get; set; }

    [DataMember]
    public string Type { get; set; }

    [DataMember]
    public double StdProductivity { get; set; }

    [DataMember]
    public double StdMetallicYield { get; set; }

    [DataMember]
    public DateTime LastUpdateTs { get; set; }

    [DataMember]
    public long ShapeId { get; set; }

    [DataMember]
    public long TypeId { get; set; }

    [DataMember]
    public double? Weight { get; set; }

  }
}
