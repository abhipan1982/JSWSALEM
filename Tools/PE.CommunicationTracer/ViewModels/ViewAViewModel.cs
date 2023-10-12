using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.CommunicationTracer.Core;
using PE.CommunicationTracer.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.CommunicationTracer.ViewModels
{
  public class ViewAViewModel : TabViewModelBase
  {
    private string _moduleName;
    private readonly AggregateService _aggregateService;
    private readonly IEventAggregator _eventAggregator;

    public string ModuleName
    {
      get { return _moduleName; }
      set { SetProperty(ref _moduleName, value); }
    }

    private ModuleStatus _moduleStatus;
    public ModuleStatus ModuleStatus
    {
      get { return _moduleStatus; }
      set { SetProperty(ref _moduleStatus, value); }
    }

    public ObservableCollection<MessageViewModel> Messages { get; set; }
    public ObservableCollection<MethodInfo> Methods { get; set; }

    public DelegateCommand<IList> OnMessageSelected { get; set; }
    public DelegateCommand<IList> OnMethodSelected { get; set; }

    public DelegateCommand Connect { get; set; }
    public DelegateCommand Execute { get; set; }

    private MethodInfo _selectedMethod;
    public MethodInfo SelectedMethod
    {
      get { return _selectedMethod; }
      set { SetProperty(ref _selectedMethod, value); }
    }


    private ModelContainer _requestModelContainer;
    public ModelContainer RequestModelContainer
    {
      get { return _requestModelContainer; }
      set { SetProperty(ref _requestModelContainer, value); }
    }

    private ModelContainer _responseModelContainer;
    public ModelContainer ResponseModelContainer
    {
      get { return _responseModelContainer; }
      set { SetProperty(ref _responseModelContainer, value); }
    }

    private string _errorModel;
    public string ErrorModel
    {
      get { return _errorModel; }
      set { SetProperty(ref _errorModel, value); }
    }

    public ViewAViewModel(AggregateService aggregateService, IEventAggregator eventAggregator)
    {
      _aggregateService = aggregateService;
      _eventAggregator = eventAggregator;

      Messages = new ObservableCollection<MessageViewModel>();
      Methods = new ObservableCollection<MethodInfo>();

      OnMessageSelected = new DelegateCommand<IList>(ExecuteOnMessageSelected);
      OnMethodSelected = new DelegateCommand<IList>(ExecuteOnMethodSelected);
      Connect = new DelegateCommand(OnConnect, () => ModuleStatus == ModuleStatus.Disconnected).ObservesProperty(() => ModuleStatus);
      Execute = new DelegateCommand(OnExecute, () => SelectedMethod != null).ObservesProperty(() => SelectedMethod);


      Messages.Add(new MessageViewModel { CallNumber = 0, TimeStamp = DateTime.Now, MethodName = "ForceRatingValueAsync",
        RequestType = typeof(DCRatingForce), Request = new DCRatingForce { RatingId = 5 } });
      Messages.Add(new MessageViewModel { CallNumber = 1, TimeStamp = DateTime.Now, MethodName = "test 2",
        RequestType = typeof(DcMeasDataSample), Request = new DcMeasDataSample { } });

      ModuleStatusEvent tickerEvent = eventAggregator.GetEvent<ModuleStatusEvent>();
      tickerEvent.Subscribe(ChangeModuleState, ThreadOption.UIThread, false,
                            e => e.Name == Common.Common.Modules[ModuleName].name);
    }

    void ExecuteOnMessageSelected(IList selectedItems)
    {
      if (selectedItems.Count != 1) return;

      var message = selectedItems[0] as MessageViewModel;

      RequestModelContainer = new ModelContainer
      {
        Model = message.Request,
        Type = message.RequestType
      };
      ResponseModelContainer = new ModelContainer
      {
        Model = message.Response,
        Type = message.ResponseType
      };

      var method = Methods.FirstOrDefault(x => x.Name == message.MethodName);
      SelectedMethod = method;
      if (method != null)
      {

      }
    }

    void ExecuteOnMethodSelected(IList selectedItems)
    {
      if (selectedItems.Count != 1) return;

      var methodInfo = selectedItems[0] as MethodInfo;

      if (methodInfo == null) return;
      var type = methodInfo.GetParameters().FirstOrDefault().ParameterType;

      object dc = Activator.CreateInstance(type, true);
      foreach (PropertyInfo nested in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        bool emptyCtor = nested.PropertyType.GetConstructors().Any(c => c.GetParameters().Length == 0);
        if (nested.PropertyType.IsClass && emptyCtor)
          nested.SetValue(dc, Activator.CreateInstance(nested.PropertyType), null);
      }

      RequestModelContainer = new ModelContainer
      {
        Model = dc as DataContractBase,
        Type = type
      };
    }

    private void ChangeModuleState(ModuleDate data)
    {
      ModuleStatus = data.Status;
    }

    private void OnConnect()
    {
      _aggregateService.RegisterListener(Common.Common.Modules[ModuleName].name, AddMessage);
    }

    private void OnExecute()
    {
      try
      {
        ErrorModel = null;

        Type module = Common.Common.Modules[ModuleName].type;

        string targetModuleName = Common.Common.Modules[ModuleName].name;

        MethodInfo factory = typeof(InterfaceHelper).GetMethod("GetFactoryChannel");
        MethodInfo factoryRef = factory.MakeGenericMethod(module);
        object client = factoryRef.Invoke(null, new object[] { targetModuleName/*, 3*/ });

        Task task = (Task)SelectedMethod.Invoke(client, new[] { RequestModelContainer.Model });
        task.GetAwaiter().GetResult();
        PropertyInfo resultProperty = task.GetType().GetProperty("Result");
        object result = resultProperty.GetValue(task);

        ResponseModelContainer = new ModelContainer
        {
          Model = result as DataContractBase,
          Type = result.GetType()
        };
      }
      catch (Exception ex)
      {
        ErrorModel = ex.ToString();
      }
    }

    public override bool IsNavigationTarget(NavigationContext navigationContext)
    {
      var resuestedModuleName = (string)navigationContext.Parameters["moduleName"];
      var isTargetModule = ModuleName == resuestedModuleName;
      return isTargetModule && base.IsNavigationTarget(navigationContext);
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
      ModuleName = (string)navigationContext.Parameters["moduleName"];
      Title = ModuleName.Substring(1);
      GetModuleMethods();

      _aggregateService.RegisterListener(Common.Common.Modules[ModuleName].name, AddMessage);
    }

    public void AddMessage(MessageDump messageDump1)
    {
      dynamic messageDump = messageDump1;
      Application.Current.Dispatcher.Invoke(() =>
      {
        var newMessage = false;
        var message = Messages.SingleOrDefault(x => x.CorrelationId == messageDump.CorrelationId);

        if (message == null)
        {
          newMessage = true;
          message = new MessageViewModel();
        }
        // TODO: add limit
        if (messageDump.RequestJson == null)
        {
          message.ResponseType = LoadType(messageDump.DataType);
          message.ExecutionTime = messageDump.ExecutionTime;

          if (typeof(DataContractBase).IsInstanceOfType(message.ResponseType) || message.ResponseType == typeof(DataContractBase))
            message.Response = JsonSerializer.Deserialize(messageDump.ResponseJson, message.ResponseType) as DataContractBase;
          else if (typeof(Exception).IsInstanceOfType(message.ResponseType))
            message.Exception = JsonSerializer.Deserialize(messageDump.ResponseJson, message.ResponseType) as Exception;
        }
        else
        {
          var type = LoadType(messageDump.DataType);

          message.TimeStamp = messageDump.TimeStamp;
          message.Module = messageDump.Module;
          message.Income = messageDump.Income;
          message.CorrelationId = messageDump.CorrelationId;
          message.MethodName = messageDump.MethodName;
          message.CallNumber = messageDump.CallNumber;
          message.RequestType = type;
          message.Request = JsonSerializer.Deserialize(messageDump.RequestJson, type) as DataContractBase;
        }

        if (newMessage) Messages.Add(message);
      });
    }

    private void GetModuleMethods()
    {
      var module = Common.Common.Modules[ModuleName].type;

      var baseModules = module.GetInterfaces().Where(x => x.Name != "IBaseModule").ToList();

      var methodInfos =
        module.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
        .Concat(baseModules.SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)));
      Methods.Clear();
      Methods.AddRange(methodInfos);
    }

    public override void OnTabClose(NavigationContext navigationContext)
    {
      _aggregateService.UnregisterListener(Common.Common.Modules[ModuleName].name);
      _eventAggregator.GetEvent<ModuleStatusEvent>().Unsubscribe(ChangeModuleState);
    }

    private Type LoadType(string typeName)
    {
      //check for problematic work
      if (string.IsNullOrEmpty(typeName))
        return null;

      Assembly currentAssembly = Assembly.GetExecutingAssembly();

      List<string> assemblyFullnames = new List<string>();

      foreach (AssemblyName assemblyName in currentAssembly.GetReferencedAssemblies())
      {
        //Load method resolve refrenced loaded assembly
        Assembly assembly = Assembly.Load(assemblyName.FullName);

        //Check if type is exists in assembly
        var type = assembly.GetType(typeName, false, true);

        if (type != null && !assemblyFullnames.Contains(assembly.FullName))
        {
          assemblyFullnames.Add(assembly.FullName);
          return type;
        }
      }

      return null;
    }
  }
}
