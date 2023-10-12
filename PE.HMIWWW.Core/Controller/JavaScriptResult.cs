using Microsoft.AspNetCore.Mvc;

namespace PE.HMIWWW.Core.Controllers
{
  public class JavaScriptResult : ContentResult
  {
    public JavaScriptResult(string script)
    {
      Content = script;
      ContentType = "application/javascript";
    }
  }
}
