namespace Urho3DNet.Config
{
    public interface IConfigContainer<T>
    {
        T Load();

        void Save(T value);
    }
}
