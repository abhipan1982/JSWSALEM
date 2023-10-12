using Microsoft.Extensions.Hosting;

namespace PE.HMIWWW.Core.Extensions
{
  public static class WebHostExtensions
  {
    public static IHost Initialize(this IHost webHost)
    {
      CoreInitializer.Initialize();

      return webHost;
    }

    public static string BuildImageSrc(this string iconName, string basePath = "/css/Functions/Small/", string extention = ".png")
    {
      return basePath + iconName + extention;
    }
  }
}
