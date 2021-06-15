using System.Collections.Specialized;
using System.ComponentModel;

namespace Urho3DNet.MVVM.Collections
{
    internal static class EventArgsCache
    {
        internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs(nameof(UrhoList<object>.Count));
        internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
    }
}