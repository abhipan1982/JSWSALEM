namespace PE.BaseModels.Structures.RLS
{
  public struct SingleGrooveSetup
  {
    private short _grooveIdx; //starts with 1
    private long _grooveTemplate; //PK from groove templates table
    private short _grooveStatus; //referemce to SRC.Core.Constants.RollGrooveStatus 	PlannedAndNoChange = 4, PlannedAndTurning = 5, PlannedAndNotAvailable = 6
    private short _grooveConfirmed;
    private short _groveCondition;
    private double? _accWeightwithCoeff;
    private double? _accWeight;
    private long? _accBilletCnt;
    private string _grooveRemark;

    public short GrooveIdx
    {
      get { return _grooveIdx; }
      set { _grooveIdx = value; }
    }

    public long GrooveTemplate
    {
      get { return _grooveTemplate; }
      set { _grooveTemplate = value; }
    }

    public short GrooveStatus
    {
      get { return _grooveStatus; }
      set { _grooveStatus = value; }
    }

    public short GrooveConfirmed
    {
      get { return _grooveConfirmed; }
      set { _grooveConfirmed = value; }
    }

    public short GrooveCondition
    {
      get { return _groveCondition; }
      set { _groveCondition = value; }
    }

    public double? AccWeightWithCoeff
    {
      get { return _accWeightwithCoeff; }
      set { _accWeightwithCoeff = value; }
    }

    public double? AccWeight
    {
      get { return _accWeight; }
      set { _accWeight = value; }
    }

    public long? AccBilletCnt
    {
      get { return _accBilletCnt; }
      set { _accBilletCnt = value; }
    }

    public string GrooveRemark
    {
      get { return _grooveRemark; }
      set { _grooveRemark = value; }
    }
  }
}
