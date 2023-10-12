using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.Interfaces.Managers.PRM;
using PE.DbEntity.EnumClasses;
using PE.PRM.Base.Module;
using PE.PRM.ProdManager.Communication;
using SMF.Module.Core;


using PE.DbEntity.Providers;
using PE.PRM.ProdManager.Handler;
using SMF.Core.Interfaces;
using SMF.Module.Core.Interfaces;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.PEContext;
using PE.BaseInterfaces.SendOffices.PRM;
using PE.PRM.Base.Handlers;
using PE.PRM.Base.Managers;
using PE.PRM.Managers;
using Org.BouncyCastle.Asn1.X509.Qualified;
using PE.Interfaces.Modules;

namespace PE.PRM.ProdManager
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>(10000);//Av add 10000
    }

    public override void RegisterServices(ServiceCollection services)
    {
     // EnumInitializator.Init();
      base.RegisterServices(services);
      services.AddSingleton(typeof(ICustomContextProvider<>), typeof(DefaultCustomContextProvider<>));
      services.AddSingleton<IModuleInfo, ModuleInfo>();

      //services.AddSingleton<IProdManagerCatalogueBaseSendOffice, ModuleBaseSendOffice>();
      //services.AddSingleton<IProdManagerProductSendOffice, ModuleBaseSendOffice>();
      //services.AddSingleton<IProdManagerWorkOrderBaseSendOffice, ModuleBaseSendOffice>();

      //services.AddSingleton<IWorkOrderBaseManager, WorkOrderBaseManager>();
      //services.AddSingleton<ICatalogueBaseManager,d CatalogueBaseManager>();
       services.AddSingleton<IProductManager, ProductManager>();
      services.AddSingleton<IWorkOrderManager, WorkOrderManager>();
      services.AddSingleton<IProdManager, ExternalAdapter>();
      //services.AddSingleton<ProductHandlerBase, ProductHandlerBase>();

      //services.AddSingleton<SteelgradeHandlerBase, SteelgradeHandlerBase>();
      //services.AddSingleton<MaterialCatalogueHandlerBase, MaterialCatalogueHandlerBase>();
      //services.AddSingleton<ProductCatalogueHandlerBase, ProductCatalogueHandlerBase>();
      //services.AddSingleton<WorkOrderHandlerBase, WorkOrderHandlerBase>();
      //services.AddSingleton<HeatHandlerBase, HeatHandlerBase>();
      //services.AddSingleton<MaterialHandlerBase, MaterialHandlerBase>();
      //services.AddSingleton<ScrapGroupHandlerBase, ScrapGroupHandlerBase>();
      //services.AddSingleton<SteelFamilyHandlerBase, SteelFamilyHandlerBase>();
      //services.AddSingleton<BilletYardHandlerBase, BilletYardHandlerBase>();

      //services.AddSingleton<ModuleBaseExternalAdapterHandler>();

      services.AddSingleton<ICatalogueManager, CatalogueManager>();//@av      
      services.AddSingleton<ProductCatalogueHandler>();
      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
