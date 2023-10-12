﻿using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollTypeData : DataContractBase
  {
    [DataMember]
    public long? Id
    {
      get;
      set;
    }

    [DataMember]
    public String RollTypeName
    {
      get;
      set;
    }

    [DataMember]
    public String RollTypeDescription
    {
      get;
      set;
    }

    [DataMember]
    public double? DiameterMin
    {
      get;
      set;
    }

    [DataMember]
    public double? DiameterMax
    {
      get;
      set;
    }

    [DataMember]
    public double? RoughnessMin
    {
      get;
      set;
    }

    [DataMember]
    public double? RoughnessMax
    {
      get;
      set;
    }

    [DataMember]
    public double? YieldStrengthRef
    {
      get;
      set;
    }

    [DataMember]
    public virtual String SteelgradeRoll
    {
      get;
      set;
    }

    [DataMember]
    public virtual double? Length
    {
      get;
      set;
    }

    [DataMember]
    public virtual String DrawingName
    {
      get;
      set;
    }

    [DataMember]
    public virtual String ChokeType
    {
      get;
      set;
    }

    [DataMember]
    public virtual long? AccBilletCntLimit
    {
      get;
      set;
    }

    [DataMember]
    public virtual double? AccWeightLimit
    {
      get;
      set;
    }

    [DataMember]
    public virtual short? MatchingRollSetType
    {
      get;
      set;
    }
  }
}
