using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Urho3DNet.Samples.UWP
{
    internal class Program
    {
        [MTAThread]
        private static void Main()
        {
            ReferenceAllTypes();

            Launcher.SdlHandleBackButton = true;
            Launcher.Run(_ => { return new SamplesManager(_); });
        }

        private static void ReferenceAllTypes()
        {
#if DEBUG
            
            var a = Assembly.Load("Avalonia.Themes.Default");
            foreach (var type in a.Modules.SelectMany(_ => _.GetTypes()))
            {
                foreach (var constructor in type.GetConstructors())
                {
                    Debug.WriteLine($"new {type.Namespace}.{type.Name}({string.Join(", ", constructor.GetParameters().Select(_=>"default("+_.ParameterType.Namespace+"."+ _.ParameterType.Name+")"))});");
                }
            }
#else
            BuildXaml();
#endif
        }

        private static void BuildXaml()
        {
            new Avalonia.Themes.Default.DefaultTheme();
            //new Avalonia.Themes.Default.InverseBooleanValueConverter();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Accents.BaseDark.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Accents.BaseLight.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.AutoCompleteBox.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Button.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ButtonSpinner.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Calendar.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.CalendarButton.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.CalendarDatePicker.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.CalendarDayButton.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.CalendarItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.CaptionButtons.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Carousel.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.CheckBox.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ComboBox.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ComboBoxItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ContentControl.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ContextMenu.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.DataValidationErrors.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.DatePicker.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.DefaultTheme.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.EmbeddableControlRoot.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Expander.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.FlyoutPresenter.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.FocusAdorner.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.GridSplitter.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ItemsControl.xaml();
            new ItemsControl();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Label.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ListBox.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ListBoxItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Menu.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.MenuFlyoutPresenter.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.MenuItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.NativeMenuBar.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.NotificationCard.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.NumericUpDown.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.OverlayPopupHost.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.PathIcon.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.PopupRoot.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ProgressBar.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.RadioButton.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.RepeatButton.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ScrollBar.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ScrollViewer.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Separator.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Slider.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.SplitView.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TabControl.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TabItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TabStrip.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TabStripItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TextBox.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TimePicker.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TitleBar.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ToggleButton.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ToggleSwitch.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.ToolTip.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TreeView.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.TreeViewItem.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.UserControl.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.Window.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:Avalonia.Themes.Default.WindowNotificationManager.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:/ Accents / BaseDark.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:/ Accents / BaseLight.xaml();
            //new CompiledAvaloniaXaml.NamespaceInfo:/ DefaultTheme.xaml();
        }
    }
}
