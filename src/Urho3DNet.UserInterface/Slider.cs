using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Data;

namespace Urho3DNet.MVVM
{
    public class Slider : UrhoControl
    {
        /// <summary>
        ///     Defines the <see cref="Range" /> property.
        /// </summary>
        public static readonly DirectProperty<Slider, float> RangeProperty =
            UrhoProperty.RegisterDirect<Slider, float>(
                nameof(Range),
                o => o.Range,
                (o, v) => o.Range = v);

        /// <summary>
        ///     Defines the <see cref="Value" /> property.
        /// </summary>
        public static readonly DirectProperty<Slider, float> ValueProperty =
            UrhoProperty.RegisterDirect<Slider, float>(
                nameof(Value),
                o => o.Value,
                (o, v) => o.Value = v,
                defaultBindingMode: BindingMode.TwoWay);

        private readonly Urho3DNet.Slider _slider;

        public Slider(Context context)
        {
            _slider = context.CreateObject<Urho3DNet.Slider>();
            _slider.SubscribeToEvent(E.SliderChanged, HanddleSliderChanged);
        }

        /// <summary>
        ///     Gets or sets the maximum value.
        /// </summary>
        public float Range
        {
            get => _slider.Range;

            set { SetAndRaise(RangeProperty, _slider.Range, value, _ => _slider.Range = value); }
        }

        /// <summary>
        ///     Gets or sets the current value.
        /// </summary>
        public float Value
        {
            get => _slider.Value;

            set { SetAndRaise(ValueProperty, Value, value, _ => _slider.Value = _); }
        }

        public UIElement VisualTreeElement => _slider;

        private void HanddleSliderChanged(VariantMap args)
        {
            var f = args[E.SliderChanged.Value].Float;
            RaisePropertyChanged(ValueProperty, Optional<float>.Empty, f);
        }
    }
}