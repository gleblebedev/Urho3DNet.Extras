namespace Urho3DNet.MVVM.PropertyStore
{
    internal interface IBatchUpdate
    {
        void BeginBatchUpdate();
        void EndBatchUpdate();
    }
}
