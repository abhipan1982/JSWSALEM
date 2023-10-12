using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PE.Helpers
{
  /// <summary>
  /// Greatest Common Divisor calculator
  /// </summary>
  public class GcdCalculator
  {
    /// <summary>
    /// Get Greatest Common Divisor
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static int GetGcd(int[] numbers)
    {
      return numbers.Aggregate(GetGcd);
    }

    /// <summary>
    /// Get Greatest Common Divisor
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GetGcd(int a, int b)
    {
      return (a == 0 || b == 0) ? a | b : GetGcd(Math.Min(a, b), Math.Max(a, b) % Math.Min(a, b));
    }
  }
}
