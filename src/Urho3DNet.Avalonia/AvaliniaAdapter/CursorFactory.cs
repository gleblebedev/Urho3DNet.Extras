using Avalonia;
using Avalonia.Input;
using Avalonia.Platform;

namespace Urho3DNet.AvaliniaAdapter
{
    class CursorFactory : ICursorFactory
    {
        public ICursorImpl GetCursor(StandardCursorType cursorType) => new CursorStub();
        public ICursorImpl CreateCursor(IBitmapImpl cursor, PixelPoint hotSpot) => new CursorStub();

        private class CursorStub : ICursorImpl
        {
            public void Dispose() { }
        }
    }
}