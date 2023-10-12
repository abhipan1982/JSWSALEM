using System;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public class EventRaisingPoint<T>
  {
    public EventRaisingPoint()
    {
      Value = default;
    }

    public T Value { get; private set; }

    public void SetValue(T value, Action eventTrigger)
    {
      if (value != null && !value.Equals(Value))
      {
        Value = value;
        eventTrigger();
      }
    }

    public void SetValue(T value)
    {
      Value = value;
    }

    public override bool Equals(object obj)
    {
      if (obj is EventRaisingPoint<T>)
      {
        EventRaisingPoint<T> point = obj as EventRaisingPoint<T>;

        if (point != null && point.Value != null)
        {
          return point.Value.Equals(Value);
        }
      }

      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
