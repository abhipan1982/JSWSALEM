using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace PE.HMIWWW.Core.Extensions
{
  public static class HtmlContentExtensions
  {
    public static string ToHtmlString(this IHtmlContent htmlContent)
    {
      if (htmlContent is HtmlString htmlString)
      {
        return htmlString.Value;
      }

      using StringWriter writer = new StringWriter();
      htmlContent.WriteTo(writer, HtmlEncoder.Default);
      return writer.ToString();
    }
  }
}
