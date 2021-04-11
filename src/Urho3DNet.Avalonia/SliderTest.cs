namespace Urho3DNet
{
    public class SliderTest : BorderImage
    {
        public SliderTest(Context context) : base(context)
        {
            IsEnabled = true;
        }

        public override void OnHover(IntVector2 position, IntVector2 screenPosition, MouseButton buttons, Qualifier qualifiers, Cursor cursor)
        {
            base.OnHover(position, screenPosition, buttons, qualifiers, cursor);
        }

        public override void OnClickBegin(IntVector2 position, IntVector2 screenPosition, MouseButton button, MouseButton buttons,
            Qualifier qualifiers, Cursor cursor)
        {
            base.OnClickBegin(position, screenPosition, button, buttons, qualifiers, cursor);
        }

        public override void OnClickEnd(IntVector2 position, IntVector2 screenPosition, MouseButton button, MouseButton buttons,
            Qualifier qualifiers, Cursor cursor, UIElement beginElement)
        {
            base.OnClickEnd(position, screenPosition, button, buttons, qualifiers, cursor, beginElement);
        }

        public override void OnDragMove(IntVector2 position, IntVector2 screenPosition, IntVector2 deltaPos, MouseButton buttons,
            Qualifier qualifiers, Cursor cursor)
        {
            base.OnDragMove(position, screenPosition, deltaPos, buttons, qualifiers, cursor);
        }
    }
}