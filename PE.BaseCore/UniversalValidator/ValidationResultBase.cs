namespace PE.Core.UniversalValidator
{
  public abstract class ValidationResultBase
  {
    public bool IsValid { get; private set; }

    protected ValidationResultBase()
    {
      IsValid = true;
    }

    public void SetValid(bool isValid)
    {
      IsValid = isValid;
    }
  }
}
