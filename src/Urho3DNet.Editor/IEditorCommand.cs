using System;

namespace Urho3DNet.Editor
{
    public interface IEditorCommand: IDisposable
    {
        void Undo();
    }
}