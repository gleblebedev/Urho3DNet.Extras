using System;
using Avalonia.Input;
using Avalonia.Platform;

namespace Urho3DNet.AvaliniaAdapter
{
    public class CursorFactory : IStandardCursorFactory
    {
        public IPlatformHandle GetCursor(StandardCursorType cursorType)
        {
            return new PlatformHandle(IntPtr.Zero, "ZeroCursor");
        }
    }
}