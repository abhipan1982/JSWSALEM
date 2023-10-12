using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PE.HMIWWW.Core.HtmlHelpers
{
  public static class ImageButtons
  {
    public static HtmlString DefaultButtons(this IHtmlHelper helper, bool bigIcons = false)
    {
      string tmpRet = null;
      tmpRet = ImageButton(helper, "back", "Back()", bigIcons).ToString();
      tmpRet += ImageButton(helper, "refresh", "Refresh()", bigIcons).ToString();
      tmpRet += ImageButton(helper, "print", "Print()", bigIcons).ToString();
      //tmpRet += ImageButton(helper, "help", "Help()", bigIcons).ToString();

      return new HtmlString(tmpRet);
    }


    //public static HtmlString ImageButton(this IHtmlHelper helper, string controller, string actionName, string imageName, bool bigIcons = false, string resxKey = null)
    //{
    //  return ImageButton(helper, imageName, controller, actionName, null, bigIcons, resxKey);
    //}

    //public static HtmlString ImageButton(this IHtmlHelper helper, string imageName, string controller, string actionName, object routeValues, bool bigIcons = false, string resxKey = null)
    //{
    //  UrlHelper urlHelper = new UrlHelper(helper.ViewContext);
    //  urlHelper.Action(actionName, controller, routeValues);
    //  //Gets the action name and routes
    //  UrlHelper urlHelper = new UrlHelper(helper.ViewContext.HttpContext);
    //  string url = urlHelper.Action(actionName,controller, routeValues);

    //  return PrepareButton(imageName, url, bigIcons, resxKey);
    //}
    public static HtmlString ImageButton(this IHtmlHelper helper, string imageName, string javascriptMethod,
      bool bigIcons = false, string resxKey = null)
    {
      //Gets the action name and routes
      string url = String.Format("javascript:{0};", javascriptMethod);

      return PrepareButton(imageName, url, bigIcons, resxKey);
    }

    public static HtmlString ImageButtonId(this IHtmlHelper helper, string imageName, string javascriptMethod,
      bool visible, string id, bool bigIcons = false, string resxKey = null)
    {
      //Gets the action name and routes
      string url = String.Format("javascript:{0};", javascriptMethod);

      return PrepareButton(imageName, url, bigIcons, resxKey, visible, id);
    }

    private static HtmlString PrepareButton(string imageName, string url, bool bigIcons, string resxKey,
      bool visible = true, string id = null)
    {
      //Creates the image tag
      TagBuilder imgTag = new TagBuilder("img");
      //string imageUrl = string.Format("/Content/functions/{0}/{1}.png", (bigIcons?"big": "small"), imageName);
      string imageUrl = string.Format("/css/functions/{0}/{1}.png", bigIcons ? "big" : "small", imageName);
      string text = string.Empty;

      if (resxKey == null)
      {
        //get resource text
        text = ResxHelper.GetResxButtonText(imageName);
      }
      else
      {
        text = ResxHelper.GetResxByKey(resxKey);
      }

      imgTag.Attributes.Add("src", imageUrl);
      imgTag.Attributes.Add("onmouseover", "this.src='" + imageUrl.Replace(".png", "_hover.png") + "'");
      imgTag.Attributes.Add("onmouseout", "this.src='" + imageUrl + "'");
      imgTag.Attributes.Add("border", "0");

      if (visible)
      {
        imgTag.AddCssClass("image-button");
      }
      else
      {
        imgTag.AddCssClass("image-button-hidden");
      }

      if (id != null)
      {
        imgTag.Attributes.Add("id", id);
      }

      imgTag.MergeAttribute("alt", text);

      //Creates the link tag
      TagBuilder imglink = new TagBuilder("a");
      imglink.MergeAttribute("href", url);
      imglink.MergeAttribute("title", text);


      imglink.InnerHtml.AppendHtml(imgTag);


      using (StringWriter writer = new StringWriter())
      {
        imglink.WriteTo(writer, HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
      }
    }
  }
}
