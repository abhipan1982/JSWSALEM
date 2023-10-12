using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal._Base
{
  [DataContract]
  [Serializable]
  public abstract class IdBase<T> : DataContractBase
  {
    public IdBase() { }

    public IdBase(T id)
    {
      Id = id;
    }

    [DataMember]
    public T Id { get; set; }
  }
}
