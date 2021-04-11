using Avalonia.Input;

namespace Urho3DNet.AvaliniaAdapter
{
    public class InputModifiersContainer
    {
        public RawInputModifiers Modifiers { get; private set; } = RawInputModifiers.None;

        public void Set(RawInputModifiers flag)
        {
            Modifiers |= flag;
        }

        public void Drop(RawInputModifiers flag)
        {
            Modifiers &= ~flag;
        }
    }
}