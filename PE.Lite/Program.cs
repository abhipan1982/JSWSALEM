using System;
using SMF.Core.Helpers;

namespace PE.Lite
{
  public class Program
  {
    public static void Main(string[] args)
    {
      int port = PortHelper.GetPort("lite");
      Console.WriteLine(port);
    }
  }
}
