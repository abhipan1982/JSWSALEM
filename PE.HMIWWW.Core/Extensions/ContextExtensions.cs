using System;
using Microsoft.AspNetCore.Http;

namespace PE.HMIWWW.Core.Extensions
{
  public static class ContextExtensions
  {
    public static Uri AbsoluteUri(this HttpRequest request)
    {
      return GetAbsoluteUri(request);
    }


    public static string Url(this HttpRequest request)
    {
      return GetAbsoluteUri(request).ToString();
    }

    public static string AbsolutePath(this HttpRequest request)
    {
      return GetAbsoluteUri(request).AbsolutePath;
    }

    private static Uri GetAbsoluteUri(HttpRequest request)
    {
      UriBuilder uriBuilder = new UriBuilder();
      uriBuilder.Scheme = request.Scheme;
      uriBuilder.Host = request.Host.Host;
      uriBuilder.Path = request.Path.ToString();
      uriBuilder.Query = request.QueryString.ToString();
      return uriBuilder.Uri;
    }
  }
}
