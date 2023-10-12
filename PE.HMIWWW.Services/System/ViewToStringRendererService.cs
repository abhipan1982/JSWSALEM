using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace PE.HMIWWW.Services.System
{
  public interface IViewToStringRendererService
  {
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
  }

  public class ViewToStringRendererService : ViewExecutor, IViewToStringRendererService
  {
    private ITempDataProvider _tempDataProvider;
    private IServiceProvider _serviceProvider;
    private readonly IActionContextAccessor _actionContextAccessor;

    public ViewToStringRendererService(
        IOptions<MvcViewOptions> viewOptions,
        IHttpResponseStreamWriterFactory writerFactory,
        ICompositeViewEngine viewEngine,
        ITempDataDictionaryFactory tempDataFactory,
        DiagnosticListener diagnosticListener,
        IModelMetadataProvider modelMetadataProvider,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider, 
        IActionContextAccessor actionContextAccessor)
        : base(viewOptions, writerFactory, viewEngine, tempDataFactory, diagnosticListener, modelMetadataProvider)
    {
      _tempDataProvider = tempDataProvider;
      _serviceProvider = serviceProvider;
      _actionContextAccessor = actionContextAccessor;
    }

    public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
    {
      var result = new ViewResult()
      {
        ViewData = new ViewDataDictionary<TModel>(
                  metadataProvider: new EmptyModelMetadataProvider(),
                  modelState: new ModelStateDictionary())
        {
          Model = model
        },
        TempData = new TempDataDictionary(
                  _actionContextAccessor.ActionContext.HttpContext,
                  _tempDataProvider),
        ViewName = viewName,
      };

      var viewEngineResult = FindView(_actionContextAccessor.ActionContext, result);
      viewEngineResult.EnsureSuccessful(originalLocations: null);

      var view = viewEngineResult.View;

      using (var output = new StringWriter())
      {
        var viewContext = new ViewContext(
            _actionContextAccessor.ActionContext,
            view,
            new ViewDataDictionary<TModel>(
                metadataProvider: new EmptyModelMetadataProvider(),
                modelState: new ModelStateDictionary())
            {
              Model = model
            },
            new TempDataDictionary(
                _actionContextAccessor.ActionContext.HttpContext,
                _tempDataProvider),
            output,
            new HtmlHelperOptions());

        await view.RenderAsync(viewContext);

        return output.ToString();
      }
    }

    /// <summary>
    /// Attempts to find the <see cref="IView"/> associated with <paramref name="viewResult"/>.
    /// </summary>
    /// <param name="actionContext">The <see cref="ActionContext"/> associated with the current request.</param>
    /// <param name="viewResult">The <see cref="ViewResult"/>.</param>
    /// <returns>A <see cref="ViewEngineResult"/>.</returns>
    ViewEngineResult FindView(ActionContext actionContext, ViewResult viewResult)
    {
      if (actionContext == null)
      {
        throw new ArgumentNullException(nameof(actionContext));
      }

      if (viewResult == null)
      {
        throw new ArgumentNullException(nameof(viewResult));
      }

      var viewEngine = viewResult.ViewEngine ?? ViewEngine;

      var viewName = viewResult.ViewName ?? GetActionName(actionContext);

      var result = viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
      var originalResult = result;
      if (!result.Success)
      {
        result = viewEngine.FindView(actionContext, viewName, isMainPage: true);
      }

      if (!result.Success)
      {
        if (originalResult.SearchedLocations.Any())
        {
          if (result.SearchedLocations.Any())
          {
            // Return a new ViewEngineResult listing all searched locations.
            var locations = new List<string>(originalResult.SearchedLocations);
            locations.AddRange(result.SearchedLocations);
            result = ViewEngineResult.NotFound(viewName, locations);
          }
          else
          {
            // GetView() searched locations but FindView() did not. Use first ViewEngineResult.
            result = originalResult;
          }
        }
      }

      if (!result.Success)
        throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", viewName));

      return result;
    }


    private const string ActionNameKey = "action";
    private static string GetActionName(ActionContext context)
    {
      if (context == null)
      {
        throw new ArgumentNullException(nameof(context));
      }

      if (!context.RouteData.Values.TryGetValue(ActionNameKey, out var routeValue))
      {
        return null;
      }

      var actionDescriptor = context.ActionDescriptor;
      string normalizedValue = null;
      if (actionDescriptor.RouteValues.TryGetValue(ActionNameKey, out var value) &&
          !string.IsNullOrEmpty(value))
      {
        normalizedValue = value;
      }

      var stringRouteValue = routeValue?.ToString();
      if (string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase))
      {
        return normalizedValue;
      }

      return stringRouteValue;
    }
  }
}
