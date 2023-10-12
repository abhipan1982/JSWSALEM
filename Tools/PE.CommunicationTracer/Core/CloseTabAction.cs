using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using Prism.Regions;

namespace PE.CommunicationTracer.Core
{
  public interface ITabClosing
  {
    void OnTabClose(NavigationContext navigationContext);
  }
  public class CloseTabAction : TriggerAction<Button>
  {
    protected override void Invoke(object parameter)
    {
      var args = parameter as RoutedEventArgs;
      if (args == null)
        return;

      var tabItem = FindParent<TabItem>(args.OriginalSource as DependencyObject);
      if (tabItem == null)
        return;

      var tabControl = FindParent<TabControl>(tabItem);
      if (tabControl == null)
        return;

      IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
      if (region == null) 
        return;

      RemoveItemFromRegion(tabItem, region);
    }

    void RemoveItemFromRegion(TabItem item, IRegion region)
    {
      var navigationContext = new NavigationContext(region.NavigationService, null);
      if (CanRemove(item.Content, navigationContext))
      {
        InvokeOnClose(item.Content, navigationContext);
        item.Template = null;
        region.Remove(item.Content);
      }
    }

    void InvokeOnClose(object item, NavigationContext navigationContext)
    {
      var navigationAwareItem = item as ITabClosing;
      if (navigationAwareItem != null)
        navigationAwareItem.OnTabClose(navigationContext);

      var frameworkElement = item as FrameworkElement;
      if (frameworkElement != null)
      {
        var navigationAwareDataContext = frameworkElement.DataContext as ITabClosing;
        if (navigationAwareDataContext != null)
          navigationAwareDataContext.OnTabClose(navigationContext);
      }
    }

    bool CanRemove(object item, NavigationContext navigationContext)
    {
      bool canRemove = true;

      var confirmRequestItem = item as IConfirmNavigationRequest;
      if (confirmRequestItem != null)
      {
        confirmRequestItem.ConfirmNavigationRequest(navigationContext, result => canRemove = result);
      }

      var frameworkElement  = item as FrameworkElement;
      if (frameworkElement != null && canRemove)
      {
        IConfirmNavigationRequest confirmRequestDataContext = frameworkElement.DataContext as IConfirmNavigationRequest;
        if (confirmRequestDataContext != null)
        {
          confirmRequestDataContext.ConfirmNavigationRequest(navigationContext, result => canRemove = result);
        }
      }

      return canRemove;
    }

    static T FindParent<T>(DependencyObject child) where T : DependencyObject
    {
      DependencyObject parentObject = VisualTreeHelper.GetParent(child);

      if (parentObject == null)
        return null;

      var parent = parentObject as T;
      if (parent != null)
        return parent;

      return FindParent<T>(parentObject);
    }
  }
}
