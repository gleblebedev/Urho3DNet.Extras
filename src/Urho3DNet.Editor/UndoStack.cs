using System.Collections.Generic;

namespace Urho3DNet.Editor
{
    public class UndoStack
    {
        private Stack<IEditorCommand> _stack = new Stack<IEditorCommand>(16);

        public void Push(IEditorCommand command) => _stack.Push(command);

        public IEditorCommand Pop() => (_stack.Count > 0)?_stack.Pop():null;

        public int Count => _stack.Count;

        public IEditorCommand Peek() => _stack.Peek();
    }
}