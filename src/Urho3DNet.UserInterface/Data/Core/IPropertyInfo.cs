using System;

namespace Urho3DNet.UserInterface.Data.Core
{
    public interface IPropertyInfo
    {
        string Name { get; }
        object Get(object target);
        void Set(object target, object value);
        bool CanSet { get; }
        bool CanGet { get; }
        Type PropertyType { get; }
    }
}
