using System.Linq.Expressions;
using System;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System.Linq;

namespace Kendo.Mvc.UI
{
  public static class HtmlHelperExtension
  {
    public static WidgetFactory<TModel> PeKendo<TModel>(this IHtmlHelper<TModel> helper)
    {
      return new PeWidgetFactory<TModel>(helper);
    }
  }
}

namespace Kendo.Mvc.UI.Fluent
{
  public class PeWidgetFactory<TModel> : WidgetFactory<TModel>
  {
    private static readonly Regex _stringFormatExpression = new Regex("(?<=\\{\\d:)(.)*(?=\\})", RegexOptions.Compiled);
    public PeWidgetFactory(IHtmlHelper<TModel> htmlHelper) : base(htmlHelper)
    {
    }

    public override NumericTextBoxBuilder<TValue> NumericTextBoxFor<TValue>(Expression<Func<TModel, TValue?>> expression)
    {
      
      ModelExplorer modelExplorer = GetModelExplorer(expression);   
      var numericTextBox = base.NumericTextBoxFor(expression);
      var originalFormat = modelExplorer.Metadata.EditFormatString;

      if(!string.IsNullOrWhiteSpace(originalFormat))
      {
        var format = _stringFormatExpression.Match(originalFormat).ToString();

        if (format.Contains("N"))
        {
          numericTextBox.Decimals(Convert.ToInt32(format.Split('N').LastOrDefault()));
        }
      }

      return numericTextBox;

    }
  }
}
