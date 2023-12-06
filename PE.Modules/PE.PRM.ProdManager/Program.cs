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
using PE.PRM.ProdManager.Handlers;
using PE.PRM.Base.Managers;
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
      
      
      services.AddSingleton<IProdManager, ExternalAdapter>();
      //services.AddSingleton<ProductHandlerBase, ProductHandlerBase>();

      services.AddSingleton<SteelgradeHandler, SteelgradeHandler>();
      services.AddSingleton<MaterialCatalogueHandler, MaterialCatalogueHandler>();
      services.AddSingleton<ProductCatalogueHandler, ProductCatalogueHandler>();
      services.AddSingleton<WorkOrderHandler, WorkOrderHandler>();
      services.AddSingleton<HeatHandler, HeatHandler>();
      services.AddSingleton<MaterialHandler, MaterialHandler>();
      //services.AddSingleton<ScrapGroupHandlerBase, ScrapGroupHandlerBase>();
      services.AddSingleton<SteelFamilyHandler, SteelFamilyHandler>();
      services.AddSingleton<BilletYardHandler, BilletYardHandler>();

      //services.AddSingleton<ModuleBaseExternalAdapterHandler>();

      services.AddSingleton<ICatalogueManager, CatalogueManager>();//@av
      services.AddSingleton<IProductManager, ProductManager>();
      services.AddSingleton<IWorkOrderManager, BatchDataManager>();

      services.AddSingleton<ProductCatalogueHandler>();
      //services.AddSingleton<MaterialCatalogueHandler>();
      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
