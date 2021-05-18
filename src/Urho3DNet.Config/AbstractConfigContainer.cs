using System.Text.Json;

namespace Urho3DNet.Config
{
    public abstract class AbstractConfigContainer<T>: IConfigContainer<T>
    {
        protected string Serialize(T value)
        {
            return JsonSerializer.Serialize(value);
        }

        protected T Deserialize(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public abstract T Load();
        
        public abstract void Save(T value);
    }
}