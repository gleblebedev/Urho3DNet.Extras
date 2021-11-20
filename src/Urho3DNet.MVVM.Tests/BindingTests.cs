using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework;
using Urho3DNet.MVVM.Data;

namespace Urho3DNet.MVVM.Tests
{
    [TestFixture]
    public class BindingTests
    {
        [Test]
        public void DataContext_SliderBinding()
        {
            TestApp.Launch((context) =>
            {
                //while (!Debugger.IsAttached)
                //{
                //    Thread.Sleep(1000);
                //}
                var slider = new Slider(context);
                var view = new SliderView(slider);
                var viewModel = new ViewModel();
                view.Bind(SliderView.ValueProperty, new Data.Binding(nameof(ViewModel.Value)) { Mode = BindingMode.TwoWay, Source = viewModel });
                view.DataContext = viewModel;
                Assert.AreEqual(viewModel.Value, view.Value);
                viewModel.Value = 0.5f;
                Assert.AreEqual(viewModel.Value, view.Value);
                viewModel.Value = 1.0f;
                Assert.AreEqual(viewModel.Value, view.Value);
                view.Value = 0.5f;
                Assert.AreEqual(viewModel.Value, view.Value);
            });
        }

        [Test]
        public void DataContext_DoesNotThrow()
        {
            TestApp.Launch((context) =>
            {
                while (!Debugger.IsAttached)
                {
                    Thread.Sleep(1000);
                }
                var slider = new Slider(context);
                var view = new SliderView(slider);
                view.DataContext = new object();
            });
        }

        public class ViewModel: INotifyPropertyChanged
        {
            private float _value;
            public event PropertyChangedEventHandler PropertyChanged;

            public float Value
            {
                get => _value;
                set
                {
                    if (_value != value)
                    {
                        _value = value;
                        OnPropertyChanged();
                    }
                }
            }

            //[NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}