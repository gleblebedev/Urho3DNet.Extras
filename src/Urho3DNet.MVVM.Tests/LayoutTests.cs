using System.Linq;
using NUnit.Framework;
using Urho.VisualTree;
using Urho3DNet.MVVM.Layout;

namespace Urho3DNet.MVVM.Tests
{
    [TestFixture]
    public class LayoutTests
    {
        [Test]
        public void TestAlignment()
        {
            TestApp.Launch((context) =>
            {
                var parent = new UIElement(context);
                var button = new Button(context);
                button.SetParent(parent);
                var parentView = new UIElementView(parent);
                var buttonView = (ButtonView)parentView.GetVisualChildren().First();
                var parentLayout = (Layoutable) parentView;
                var buttonLayout = (Layoutable) buttonView;

                System.Console.WriteLine($"Parent: {parentLayout.Bounds}");
                System.Console.WriteLine($"Button Bounds: {buttonLayout.Bounds}");
                System.Console.WriteLine($"Button Size: {buttonLayout.Width} {buttonLayout.Height}");

                buttonLayout.HorizontalAlignment = Layout.HorizontalAlignment.Right;
                buttonLayout.VerticalAlignment = Layout.VerticalAlignment.Bottom;
                buttonLayout.MinWidth = 100;
                buttonLayout.MinHeight = 50;
                parentLayout.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
                parentLayout.VerticalAlignment = Layout.VerticalAlignment.Stretch;
                parentLayout.Margin = new Thickness(10);
                parentLayout.Arrange(new Rect(0, 0, 800, 600));

                System.Console.WriteLine($"Parent: {parentLayout.Bounds}");
                System.Console.WriteLine($"Button: {buttonLayout.Bounds}");
                System.Console.WriteLine($"Button Size: {buttonLayout.Width} {buttonLayout.Height}");
            });
        }
    }
}