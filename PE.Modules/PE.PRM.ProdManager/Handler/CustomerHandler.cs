using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.External.DBA;
using PE.Models.DataContracts.Internal.DBA;

namespace PE.PRM.ProdManager.Handlers
{
  public class CustomerHandler
  {
    /// <summary>
    ///   Return customer or default customer if customer with param-name does't exist
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="customerName"></param>
    public async Task<PRMCustomer> GetCustomerByNameAsync(PEContext ctx, string customerName, bool getDefault = false)
    {
      PRMCustomer customer = await ctx.PRMCustomers
        .Where(x => x.CustomerName.ToLower().Equals(customerName.ToLower()))
        .SingleOrDefaultAsync();

      if (customer == null && getDefault)
      {
        customer = await ctx.PRMCustomers.Where(x => x.IsDefault).SingleAsync();
      }

      return customer;
    }

    /// <summary>
    ///   Return customer or create customer if customer with param-name does't exist
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="customerName"></param>
    /// <param name="backMsg">Optional to fill return information to L3</param>
    public async Task<PRMCustomer> GetCustomerByNameOrCreateNewAsync(PEContext ctx, string customerName,
      DCBatchDataStatus backMsg = null)
    {
      PRMCustomer customer = string.IsNullOrWhiteSpace(customerName) ? 
        null :
        await GetCustomerByNameAsync(ctx, customerName);

      if (customer == null)
      {
        customer = new PRMCustomer
        {
          CustomerName = customerName,
          CustomerCode = customerName,
          IsActive = true
        };

        if (backMsg != null)
        {
          backMsg.BackMessage += " PRMCustomer created;";
        }

        ctx.PRMCustomers.Add(customer);
      }

      return customer;
    }
  }
}
