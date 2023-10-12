using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using PE.CommunicationTracer.Services;
using PE.CommunicationTracer.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace PE.CommunicationTracer
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    public App()
    {
      DispatcherUnhandledException += App_DispatcherUnhandledException;
      TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      var msg = "[ UnhandledException ] UI Dispatcher, " + Common.Common.GetExceptionStringFormat(e.Exception);
      var time = " [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "]";

      MessageBox.Show(msg, "Exception" + time, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
      var msg = "[ UnhandledException ] Task, " + e.Exception.Message;
      var time = " [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "]";

      MessageBox.Show(msg, "Exception" + time, MessageBoxButton.OK, MessageBoxImage.Error);

      e.SetObserved();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      var msg = "[ UnhandledException ] Current Domain, " + Common.Common.GetExceptionStringFormat(e.ExceptionObject as Exception);
      var time = " [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "]";

      MessageBox.Show(msg, "Exception" + time, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.Register<object, ViewA>("ViewA");
      containerRegistry.RegisterSingleton<AggregateService>();
      //throw new NotImplementedException();
    }
  }
}
