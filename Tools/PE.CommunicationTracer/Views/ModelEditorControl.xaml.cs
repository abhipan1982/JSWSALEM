using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;
using Newtonsoft.Json;
using PE.CommunicationTracer.Core;
using PE.CommunicationTracer.Core.Factories;
using SMF.Core.DC;

namespace PE.CommunicationTracer.Views
{
  /// <summary>
  /// Interaction logic for ModelEditorControl.xaml
  /// </summary>
  public partial class ModelEditorControl : UserControl, INotifyPropertyChanged
  {
    FoldingManager _foldingManager;
    BraceFoldingStrategy _foldingStrategy;

    public ModelContainer ModelContainer
    {
      get { return (ModelContainer)GetValue(ModelContainerProperty); }
      set { SetValue(ModelContainerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ModelContainer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ModelContainerProperty =
        DependencyProperty.Register("ModelContainer", typeof(ModelContainer), typeof(ModelEditorControl), new PropertyMetadata(null, new PropertyChangedCallback(ModelContainerChanged)));

    private static void ModelContainerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue is ModelContainer container && container != null)
      {
        ((TabItem)((d as ModelEditorControl).tabs.Items[0])).IsEnabled = true;
        (d as ModelEditorControl).tabs.SelectedIndex = 0;
        (d as ModelEditorControl).pgModel.SelectedObject = container.Model;
      }
    }

    public string ErrorMessage
    {
      get { return (string)GetValue(ErrorMessageProperty); }
      set { SetValue(ErrorMessageProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Error.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ErrorMessageProperty =
        DependencyProperty.Register("ErrorMessage", typeof(string), typeof(ModelEditorControl), new PropertyMetadata(null, new PropertyChangedCallback(ErrorChanged)));

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    private static void ErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue == null) return;

      ((TabItem)(d as ModelEditorControl).tabs.Items[0]).IsEnabled = false;
      (d as ModelEditorControl).tabs.SelectedIndex = 1;

      (d as ModelEditorControl).tbJson.Text = e.NewValue.ToString();
    }

    public ModelEditorControl()
    {
      InitializeComponent();

      _foldingManager = FoldingManager.Install(tbJson.TextArea);
      _foldingStrategy = new BraceFoldingStrategy();

      var foldingUpdateTimer = new DispatcherTimer();
      foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
      foldingUpdateTimer.Tick += delegate { _foldingStrategy.UpdateFoldings(_foldingManager, tbJson.Document); };
      foldingUpdateTimer.Start();

      pgModel.ControlFactory = new CustomPropertyGridControlFactory();
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (ModelContainer?.Type == null) return;
      if (!(e.Source is TabControl item)) return;

      e.Handled = true;

      var selected = item.SelectedItem as TabItem;
      var tab = selected.Header.ToString();

      switch (tab)
      {
        case "Properties":
          if (!string.IsNullOrEmpty(tbJson.Text))
          {
            try
            {
              //var model = JsonSerializer.Deserialize(tbJson.Text, Type);
              var model = JsonConvert.DeserializeObject(tbJson.Text, ModelContainer.Type, new JsonSerializerSettings
              {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
              });
              pgModel.SelectedObject = model;
              var oldValue = ModelContainer.Model;
              ModelContainer.Model = model as DataContractBase;
              OnPropertyChanged(new DependencyPropertyChangedEventArgs(ModelContainerProperty, oldValue, ModelContainer));
              RaisePropertyChanged("Model");
            }
            catch (Exception ex)
            {
              //tbResponse.Text = ex.ToString();
            }
          }
          break;
        case "Json":
          //var options = new JsonSerializerOptions { WriteIndented = true };
          //tbJson.Text = JsonSerializer.Serialize(pgModel.SelectedObject, options);
          tbJson.Text = JsonConvert.SerializeObject(pgModel.SelectedObject, Formatting.Indented, new JsonSerializerSettings
          {
            DateTimeZoneHandling = DateTimeZoneHandling.Local
          });
          _foldingStrategy.UpdateFoldings(_foldingManager, tbJson.Document);
          break;
      }
    }
  }
}
