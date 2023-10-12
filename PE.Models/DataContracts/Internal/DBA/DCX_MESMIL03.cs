using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.Adapter
{
  public class DCX_MESMIL03 : DataContractBase
  {
    /// <summary>
    /// Primary Key in transfer table
    /// </summary>
    [DataMember]
    public long Counter { get; set; }

    /// <summary>
    /// Time stamp when record has been inserted to transfer table
    /// </summary>
    [DataMember]
    public DateTime Reg_Datetime { get; set; }

    /// <summary>
    /// 0 - new record, 1 - processed and OK, 2 - processed and Error, 3 - processed and rejected
    /// </summary>
    //[DataMember]
    //public PE.DbEntity.Enums.CommStatus Reg_Status { get; set; }

    /// <summary>
    /// Unique heat ID
    /// </summary>
    [DataMember]
    public string Heat_Id { get; set; }

    /// <summary>
    /// Standard billet length
    /// </summary>
    [DataMember]
    public int? Billet_Len { get; set; }

    /// <summary>
    /// Standard billet width
    /// </summary>
    [DataMember]
    public short? Billet_Wid { get; set; }

    /// <summary>
    /// Billet thickness
    /// </summary>
    [DataMember]
    public short? Billet_Thk { get; set; }

    /// <summary>
    /// Billet weight
    /// </summary>
    [DataMember]
    public double? Billet_Wgt { get; set; }

    /// <summary>
    /// steel grade code
    /// </summary>
    [DataMember]
    public string Steelgrade { get; set; }

    /// <summary>
    /// Target total number of the products
    /// </summary>
    [DataMember]
    public short? Num_planned { get; set; }

    /// <summary>
    /// Target total weight of the products
    /// </summary>
    [DataMember]
    public double? Wgt_Planned { get; set; }

    /// <summary>
    /// Billet ID defined by customer, free text
    /// </summary>
    [DataMember]
    public string Billet_id { get; set; }

  }
}
