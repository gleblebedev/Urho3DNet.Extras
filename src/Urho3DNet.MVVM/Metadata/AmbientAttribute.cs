using System;

namespace Urho.Metadata
{
    /// <summary>
    /// Defines the ambient class/property 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true)]
    public class AmbientAttribute : Attribute
    {
    }
}
