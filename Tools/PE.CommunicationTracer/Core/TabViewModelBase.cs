using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PE.CommunicationTracer.Core
{
  public class TabViewModelBase : BindableBase, INavigationAware, IActiveAware, ITabClosing
  {
    protected bool _tabPinned = false;

    private string _title;
    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }

    private bool _isActive;
    public bool IsActive
    {
      get { return _isActive; }
      set { SetProperty(ref _isActive, value); }
    }

    public event EventHandler IsActiveChanged; 

    public DelegateCommand PinTabCommand { get; set; }
    public DelegateCommand UnpinTabCommand { get; set; }

    public TabViewModelBase()
    {
      PinTabCommand = new DelegateCommand(ExecutePinTab);
      UnpinTabCommand = new DelegateCommand(ExecuteUnpinTab);
    }

    protected virtual void ExecuteUnpinTab()
    {
      _tabPinned = false;
    }

    protected virtual void ExecutePinTab()
    {
      _tabPinned = true;
    }

    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
      return !_tabPinned;
    }

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {

    }

    public virtual void OnTabClose(NavigationContext navigationContext)
    {
    }
  }
}
