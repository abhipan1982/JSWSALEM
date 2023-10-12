using System;
using System.IO;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace PE.HMIWWW.Core.Helpers
{
  public static class PdfHelper
  {
    public static Byte[] GetPdfA4ByteArray(string html)
    {
      var document = new PdfDocument();
      PdfGenerator.AddPdfPages(document, html, PageSize.A4);

      Byte[] res = null;
      using (MemoryStream ms = new MemoryStream())
      {
        document.Save(ms);
        res = ms.ToArray();
      }
      return res;
    }
  }
}
