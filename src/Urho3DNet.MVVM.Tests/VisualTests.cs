using System.Linq;
using NUnit.Framework;
using Urho.VisualTree;

namespace Urho3DNet.MVVM.Tests
{
    [TestFixture]
    public class VisualTests
    {
        [Test]
        public void AddAndRemoveElement()
        {
            TestApp.Launch((context) =>
            {
                var parent = new UIElement(context);
                var child = new UIElement(context);
                var parentView = new UIElementView(parent);
                child.SetParent(parent);
                var childView = parentView.GetVisualChildren().First() as UIElementView;
                Assert.AreEqual(child, childView.Target);
                Assert.AreEqual(parentView, childView.GetVisualParent());

                child.SetParent(null);
                Assert.AreEqual(0, parentView.GetVisualChildren().Count());
                Assert.IsNull(childView.GetVisualParent());
            });
        }
    }
}