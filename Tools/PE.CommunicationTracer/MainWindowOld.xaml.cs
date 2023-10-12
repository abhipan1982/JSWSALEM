using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PE.Interfaces;
using PE.Core;
using SMF.Core.Communication;
using SMF.Core.Infrastructure;
using PE.Interfaces.Modules;
using System.Text.Json;
using ICSharpCode.AvalonEdit.Folding;
using System.Windows.Threading;
using SMF.Core.Interfaces;

namespace PE.CommunicationTracer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindowOld : Window
  {
    private Dictionary<ModuleDescription, Type> _modules;
    private Type _currentType;
    private string _tab;

    FoldingManager _foldingManager;
    BraceFoldingStrategy _foldingStrategy;

    public MainWindowOld()
    {
      InitializeComponent();
      InitModules();

      foreach (var module in _modules)
      {
        cbMolules.Items.Add(module.Key.Name);
      }

      _foldingManager = FoldingManager.Install(tbJson.TextArea);
      _foldingStrategy = new BraceFoldingStrategy();

      var foldingUpdateTimer = new DispatcherTimer();
      foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
      foldingUpdateTimer.Tick += delegate { _foldingStrategy.UpdateFoldings(_foldingManager, tbJson.Document); };
      foldingUpdateTimer.Start();
    }

    private void InitModules()
    {
      _modules = new Dictionary<ModuleDescription, Type>
      {
        { Modules.Hmiexe, typeof(IHmi) },
        { Modules.DBAdapter, typeof(IDBAdapter) },
        { Modules.ProdManager, typeof(IProdManager) },
        { Modules.ProdPlaning, typeof(IProdPlaning) },
        { Modules.MvHistory, typeof(IMVHistory) },
        { Modules.Events, typeof(IEvents) },
        { Modules.L1Adapter, typeof(IL1Adapter) },
        { Modules.Tracking, typeof(ITracking) },
        { Modules.WalkingBeamFurnace, typeof(IWalkingBeamFurnace) },
        { Modules.TcpProxy, typeof(ITcpProxy) },
        { Modules.Setup, typeof(ISetup) },
        { Modules.RollShop, typeof(IRollShop) },
        { Modules.Maintenance, typeof(IMaintenance) },
        { Modules.Quality, typeof(IQuality) },
        { Modules.QualityExpert, typeof(IQualityExpert) },
        { Modules.Yards, typeof(IYards) },
      };
    }

    private async void btnRefreshStatus_Click(object sender, RoutedEventArgs e)
    {
      await RefreshStatus();
    }

    private async void cbMolules_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      btnSend.IsEnabled = false;
      cbMethods.IsEnabled = true;
      btnRefreshStatus.IsEnabled = true;

      Type module = _modules.Where(x => x.Key.Name == cbMolules.SelectedItem.ToString()).Select(x => x.Value).FirstOrDefault();

      Type baseModule = module.GetInterfaces().Where(x => x.Name != "IBaseModule").Single();

      var methodInfos = 
        module.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
        .Concat(baseModule.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance));
      cbMethods.Items.Clear();

      foreach (MethodInfo methodInfo in methodInfos)
      {
        cbMethods.Items.Add(methodInfo);
      }

      await RefreshStatus();
    }

    private async Task RefreshStatus()
    {
      btnRefreshStatus.IsEnabled = false;

      bool pipeOpened = await PipeSearch(cbMolules.SelectedItem.ToString());
      SetPipeStatus(pipeOpened);

      btnRefreshStatus.IsEnabled = true;
    }

    private void SetPipeStatus(bool pipeOpened)
    {
      if (pipeOpened)
        tbPipeStatus.Background = new SolidColorBrush(Color.FromArgb(0xFF, 128, 237, 95));
      else
        tbPipeStatus.Background = new SolidColorBrush(Color.FromArgb(0xFF, 237, 114, 95));
    }

    private async Task<bool> PipeSearch(string pipeName)
    {
      Guid pipeGuid;

      string s = string.Format(@"net.pipe://+/MODULES/{0}/", pipeName.ToUpper());

      byte[] bytes = Encoding.UTF8.GetBytes(s);
      string base64 = Convert.ToBase64String(bytes);

      string namedPipeMMFName = string.Format(@"Global\net.pipe:E{0}", base64);

      try
      {
        using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(namedPipeMMFName, MemoryMappedFileRights.Read))
        {
          using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(4, 45, MemoryMappedFileAccess.Read))
          {
            accessor.Read<Guid>(0, out pipeGuid);
          }
        }

        using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeGuid.ToString(), PipeDirection.InOut,
                                PipeOptions.None, TokenImpersonationLevel.Impersonation))
        {
          await pipeClient.ConnectAsync(200);
        }

        return true;
      }
      catch
      {
        return false;
      }
    }

    private void cbMethods_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (cbMethods.SelectedItem == null) return;

      MethodInfo methodInfo = cbMethods.SelectedItem as MethodInfo;
      _currentType = methodInfo.GetParameters().FirstOrDefault().ParameterType;

      object dc = Activator.CreateInstance(_currentType, true);
      foreach (PropertyInfo nested in _currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        bool emptyCtor = nested.PropertyType.GetConstructors().Any(c => c.GetParameters().Length == 0);
        if (nested.PropertyType.IsClass && emptyCtor)
          nested.SetValue(dc, Activator.CreateInstance(nested.PropertyType), null);
      }

      pgModel.SelectedObject = dc;

      btnSend.IsEnabled = true;
    }

    private async void btnSend_ClickAsync(object sender, RoutedEventArgs e)
    {
      try
      {
        var options = new JsonSerializerOptions { WriteIndented = true };

        if (_tab == "Json")
        {
          try
          {
            var model = JsonSerializer.Deserialize(tbJson.Text, _currentType);
            pgModel.SelectedObject = model;
          }
          catch (Exception ex)
          {
            tbResponse.Text = ex.ToString();
            return;
          }
        }

        var result = await InvokeMethod(cbMolules.SelectedItem.ToString(), cbMethods.SelectedItem as MethodInfo, pgModel.SelectedObject);
        tbResponse.Text = JsonSerializer.Serialize(result, options);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString(), ex.Message);
      }
    }

    private async Task<object> InvokeMethod(string moduleName, MethodInfo methodInfo, object dc)
    {

      Type module = _modules.Where(x => x.Key.Name == moduleName).Select(x => x.Value).FirstOrDefault();

      string targetModuleName = cbMolules.SelectedItem.ToString();

      MethodInfo factory = typeof(InterfaceHelper).GetMethod("GetFactoryChannel");
      MethodInfo factoryRef = factory.MakeGenericMethod(module);
      object client = factoryRef.Invoke(null, new object[] { targetModuleName/*, 3*/ });

      Task task = (Task)methodInfo.Invoke(client, new[] { dc });
      await task;
      PropertyInfo resultProperty = task.GetType().GetProperty("Result");
      object result = resultProperty.GetValue(task);

      return result;
    }

    private void pgModel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (_currentType == null) return;
      if (!(e.Source is TabControl item)) return;

      e.Handled = true;

      var selected = item.SelectedItem as TabItem;
      _tab = selected.Header.ToString();

      switch (_tab)
      {
        case "Properties":
          if (!string.IsNullOrEmpty(tbJson.Text))
          {
            try
            {
              var model = JsonSerializer.Deserialize(tbJson.Text, _currentType);
              pgModel.SelectedObject = model;
            }
            catch (Exception ex)
            {
              tbResponse.Text = ex.ToString();
            }
          }
          break;
        case "Json":
          var options = new JsonSerializerOptions { WriteIndented = true };
          tbJson.Text = JsonSerializer.Serialize(pgModel.SelectedObject, options);
          _foldingStrategy.UpdateFoldings(_foldingManager, tbJson.Document);
          break;
      }
    }
  }
}
