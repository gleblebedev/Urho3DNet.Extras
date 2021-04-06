namespace Urho3DNet
{
    public class SkiaElement : Sprite
    {
        private SkiaCanvas _canvas;

        public SkiaElement(Context context): base(context)
        {
            IsEnabled = true;
        }

        public SkiaCanvas Canvas
        {
            get
            {
                return _canvas;
            }
            set
            {
                if (_canvas != value)
                {
                    _canvas = value;
                    var size = _canvas.Size;
                    ImageRect = new IntRect(0, 0, size.X, size.Y);
                    Size = size;
                    Texture = _canvas.Texture;
                }
            }
        }

        public override void OnClickBegin(IntVector2 position, IntVector2 screenPosition, MouseButton button, MouseButton buttons,
            Qualifier qualifiers, Cursor cursor)
        {
            base.OnClickBegin(position, screenPosition, button, buttons, qualifiers, cursor);
        }
    }
}