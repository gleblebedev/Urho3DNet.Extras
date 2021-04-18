﻿using Urho3DNet.SampleApp.View;

namespace Urho3DNet.Samples
{
    public class AvaloniaSample : Sample
    {
        private readonly SharedPtr<AvaloniaElement> _sprite;
        private SampleAvaloniaWindow _window;
        private Slider _slider;
        private SliderTest _sliderTest;

        public AvaloniaSample(Context context) : base(context)
        {
            _window = new SampleAvaloniaWindow();
            _window.Width = 200;
            _window.Height = 100;
            _window.Show();
            //_window.InvalidateVisual();
            _sprite = UIRoot.CreateChild<AvaloniaElement>();
            _sprite.Value.Canvas = _window;
            DefaultFogColor = new Color(0.1f, 0.2f, 0.4f, 1.0f);
            IsMouseVisible = true;
            MouseMode = MouseMode.MmFree;

            _slider = UIRoot.CreateChild<Slider>();
            _slider.SetDefaultStyle(ResourceCache.GetResource<XMLFile>("UI/DefaultStyle.xml"));
            _slider.Height = 16;
            _slider.Width = 200;
            _slider.Orientation = Orientation.OHorizontal;
            _slider.Range = 1;
            _slider.Value = 0.5f;
            _slider.SetStyleAuto();

            _sliderTest = UIRoot.CreateChild<SliderTest>();
            _sliderTest.SetDefaultStyle(ResourceCache.GetResource<XMLFile>("UI/DefaultStyle.xml"));
            _sliderTest.Height = 16;
            _sliderTest.Width = 200;
            _sliderTest.SetStyleAuto();
            _sliderTest.Position = new IntVector2(100, 100);
        }


        public override void Dispose()
        {
            base.Dispose();
            _sprite.Dispose();
        }

        public override void OnUpdate(float timeStep)
        {
            _slider.IsEnabled = true;
            base.OnUpdate(timeStep);

            _sprite.Value.Position = new Vector2(0.5f * (Graphics.Width - _sprite.Value.Width),
                0.5f * (Graphics.Height - _sprite.Value.Height));
            _sprite.Value.Size = new IntVector2(220, 110);

            _slider.Position = new IntVector2(100,20);
            _slider.Width = 200;
        }
    }
}