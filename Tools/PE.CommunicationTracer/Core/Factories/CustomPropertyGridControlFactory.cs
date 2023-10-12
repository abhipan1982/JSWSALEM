using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PropertyTools.Wpf;
using SMF.Core.DC;

using ColumnDefinition = System.Windows.Controls.ColumnDefinition;

namespace PE.CommunicationTracer.Core.Factories
{
  public class CustomPropertyGridControlFactory : PropertyGridControlFactory
  {
    public override FrameworkElement CreateControl(PropertyItem pi, PropertyControlFactoryOptions options)
    {
      // Check if the property is of type HmiInitiator
      if (pi.Is(typeof(HmiInitiator)))
      {
        // Create a control to edit the HmiInitiator
        return this.CreateHmiInitiatorControl(pi, options);
      }
      else if (pi.Is(typeof(ModuleWarningMessage)))
      {
        return this.CreateModuleWarningMessageControl(pi, options);
      }
      else if (pi.Is(typeof(DateTime)))
      {
        return this.CreateDateTimeControl(pi, options);
      }

      return base.CreateControl(pi, options);
    }

    private FrameworkElement CreateDateTimeControl(PropertyItem pi, PropertyControlFactoryOptions options)
    {
      var grid = new Grid();
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
      var tbDate = new TextBox();
      pi.FormatString = "yyyy/MM/dd HH:mm:ss";
      tbDate.SetBinding(TextBox.TextProperty, pi.CreateBinding());
      grid.Children.Add(tbDate);

      var button = new Button
      {
        Content = "Now",
        Width = 35,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      button.Click += (s, e) =>
      {
        tbDate.Text = DateTimeOffset.Now.ToString("O");
        BindingExpression be = tbDate.GetBindingExpression(TextBox.TextProperty);
        be.UpdateSource();
      };
      Grid.SetColumn(button, 1);
      grid.Children.Add(button);

      return grid;
    }

    protected virtual FrameworkElement CreateHmiInitiatorControl(PropertyItem pi, PropertyControlFactoryOptions options)
    {
      var grid = new Grid();
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
      //grid.ColumnDefinitions.Add(new ColumnDefinition());
      var userId = new TextBox();
      userId.SetBinding(TextBox.TextProperty, new Binding(pi.Descriptor.Name + ".UserId"));
      //var label = new Label { Content = "-" };
      //Grid.SetColumn(label, 1);
      //var ipAddressBox = new TextBox();
      //Grid.SetColumn(ipAddressBox, 2);
      //ipAddressBox.SetBinding(TextBox.TextProperty, new Binding(pi.Descriptor.Name + ".IpAddress"));
      grid.Children.Add(userId);
      //grid.Children.Add(label);
      //grid.Children.Add(ipAddressBox);

      return grid;
    }

    protected virtual FrameworkElement CreateModuleWarningMessageControl(PropertyItem pi, PropertyControlFactoryOptions options)
    {
      var grid = new Grid();
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      var message = new TextBox();
      //message.IsReadOnly = true;
      //message.Text = (pi.Val as ModuleWarningMessage).ToString();
      //message.SetBinding(TextBox.TextProperty, new Binding(pi.Descriptor.Name + ".UserId") { Mode = BindingMode.OneWay });
      //grid.Children.Add(message);

      return grid;
    }
  }
}
