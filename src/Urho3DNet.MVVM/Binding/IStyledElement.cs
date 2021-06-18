using System.ComponentModel;
using Urho3DNet.MVVM.Controls;
using Urho3DNet.MVVM.LogicalTree;
using Urho3DNet.MVVM.Styling;

namespace Urho3DNet.MVVM.Binding
{
    public interface IStyledElement:
        IStyleable,
        IStyleHost,
        ILogical,
        IResourceHost,
        IDataContextProvider,
        ISupportInitialize
    {
    }
}