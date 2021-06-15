namespace Urho3DNet.MVVM
{
    public partial class SliderView
    {
        protected override void SubscribeToEvents(Object target)
        {
            target.SubscribeToEvent(E.SliderChanged, HandleSliderChanged);
            base.SubscribeToEvents(target);
        }

        protected override void UnsubscribeFromEvents(Object target)
        {
            target.UnsubscribeFromEvent(E.SliderChanged);
            base.UnsubscribeFromEvents(target);
        }

        private void HandleSliderChanged(VariantMap args)
        {
            SetValue(ValueProperty, _target.Value);
        }
    }
}