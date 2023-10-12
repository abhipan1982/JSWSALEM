using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PE.CommunicationTracer.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PE.CommunicationTracer.ViewModels
{
  public class MainWindowViewModel : BindableBase
  {
    private string _title = "Communication Tracer";
    private readonly IRegionManager _regionManager;

    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }

    public ObservableCollection<ModuleMenuItem> _moduleItems = new ObservableCollection<ModuleMenuItem>();

    public ObservableCollection<ModuleMenuItem> ModuleItems
    {
      get { return _moduleItems; }
      set { _moduleItems = value; }
    }


    public DelegateCommand<string> NavigateCommand { get; set; }

    public MainWindowViewModel(IRegionManager regionManager)
    {
      NavigateCommand = new DelegateCommand<string>(Navigate);
      _regionManager = regionManager;

      foreach (var module in Common.Common.Modules.OrderBy(x => x.Value.code))
      {
        var menuItem = new ModuleMenuItem { Title = module.Value.code, ModuleName= module.Key, Command = new DelegateCommand<string>(Navigate) };
        ModuleItems.Add(menuItem);
      }
    }

    void Navigate(string navigationPath)
    {
      var p = new NavigationParameters();
      p.Add("moduleName", navigationPath);
      _regionManager.RequestNavigate(RegionNames.TabRegion, "ViewA", p);
    }
  }
  public class ModuleMenuItem
  {
    public string Title { get; set; }
    public string ModuleName { get; set; }
    public ICommand Command { get; set; }
  }
}
