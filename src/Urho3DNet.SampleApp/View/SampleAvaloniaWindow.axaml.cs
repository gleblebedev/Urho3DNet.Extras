using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Urho3DNet.SampleApp.View
{
    public class SampleAvaloniaWindow : Avalonia.Controls.Window
    {
        public SampleAvaloniaWindow()
        {
            InitializeComponent();
#if DEBUG
            //this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
