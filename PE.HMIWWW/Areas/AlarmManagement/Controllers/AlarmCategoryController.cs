using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE.HMIWWW.Core.Controllers;
using SMF.DbEntity.Models;

namespace PE.HMIWWW.Areas.AlarmManagement.Controllers
{
  [Area("AlarmManagement")]
  [Authorize]
  public class AlarmCategoryController : BaseController
  {
    public ActionResult Index()
    {
      return View();
    }
  }

  [Area("AlarmManagement")]
  [Authorize]
  public class AlarmCategoryApiController : ControllerBase
  {
    private readonly SMFContext _context;

    public AlarmCategoryApiController(SMFContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get([DataSourceRequest] DataSourceRequest request)
    {
      using var context = new SMFContext();
      return Ok(await context.AlarmCategories.ToDataSourceResultAsync(request));
    }
    [HttpPost]
    public async Task<ActionResult> Post(AlarmCategory product)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(error => error.ErrorMessage));
      }
      using var context = new SMFContext();
      context.AlarmCategories.Add(product);
      await context.SaveChangesAsync();
      return new ObjectResult(new DataSourceResult { Data = new[] { product }, Total = 1 });
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, AlarmCategory product)
    {
      if (ModelState.IsValid && id == product.AlarmCategoryId)
      {
        try
        {
          using var context = new SMFContext();
          context.AlarmCategories.Update(product);
          await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          return new NotFoundResult();
        }
        return new StatusCodeResult(200);
      }
      else
      {
        return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(error => error.ErrorMessage));
      }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        using var context = new SMFContext();
        var entity = new AlarmCategory();
        entity.AlarmCategoryId = id;
        context.AlarmCategories.Attach(entity);
        context.AlarmCategories.Remove(entity);
        await context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return new NotFoundResult();
      }
      return new StatusCodeResult(200);
    }
  }
}
