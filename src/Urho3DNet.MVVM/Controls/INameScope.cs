using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Urho3DNet.MVVM.Collections;
using Urho3DNet.MVVM.Utilities;

namespace Urho3DNet.MVVM.Controls
{
    /// <summary>
    /// Defines a name scope.
    /// </summary>
    public interface INameScope
    {
        /// <summary>
        /// Registers an element in the name scope.
        /// </summary>
        /// <param name="name">The element name.</param>
        /// <param name="element">The element.</param>
        void Register(string name, object element);

        /// <summary>
        /// Finds a named element in the name scope, waits for the scope to be completely populated before returning null
        /// Returned task is configured to run any continuations synchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The element, or null if the name was not found.</returns>
        SynchronousCompletionAsyncResult<object> FindAsync(string name);
        
        /// <summary>
        /// Finds a named element in the name scope, returns immediately, doesn't traverse the name scope stack
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The element, or null if the name was not found.</returns>
        object Find(string name);

        /// <summary>
        /// Marks the name scope as completed, no further registrations will be allowed
        /// </summary>
        void Complete();
        
        /// <summary>
        /// Returns whether further registrations are allowed on the scope
        /// </summary>
        bool IsCompleted { get; }


    }

    /// <summary>
    /// An indexed dictionary of resources.
    /// </summary>
    public class ResourceDictionary : UrhoDictionary<object, object?>, IResourceDictionary
    {
        private IResourceHost? _owner;
        private UrhoList<IResourceProvider>? _mergedDictionaries;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceDictionary"/> class.
        /// </summary>
        public ResourceDictionary()
        {
            CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceDictionary"/> class.
        /// </summary>
        public ResourceDictionary(IResourceHost owner)
            : this()
        {
            Owner = owner;
        }

        public IResourceHost? Owner
        {
            get => _owner;
            private set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OwnerChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public IList<IResourceProvider> MergedDictionaries
        {
            get
            {
                if (_mergedDictionaries == null)
                {
                    _mergedDictionaries = new UrhoList<IResourceProvider>();
                    _mergedDictionaries.ResetBehavior = ResetBehavior.Remove;
                    _mergedDictionaries.ForEachItem(
                        x =>
                        {
                            if (Owner is object)
                            {
                                x.AddOwner(Owner);
                            }
                        },
                        x =>
                        {
                            if (Owner is object)
                            {
                                x.RemoveOwner(Owner);
                            }
                        }, null);
                }

                return _mergedDictionaries;
            }
        }

        bool IResourceNode.HasResources
        {
            get
            {
                if (Count > 0)
                {
                    return true;
                }

                if (_mergedDictionaries?.Count > 0)
                {
                    foreach (var i in _mergedDictionaries)
                    {
                        if (i.HasResources)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public event EventHandler? OwnerChanged;

        public bool TryGetResource(object key, out object? value)
        {
            if (TryGetValue(key, out value))
            {
                return true;
            }

            if (_mergedDictionaries != null)
            {
                for (var i = _mergedDictionaries.Count - 1; i >= 0; --i)
                {
                    if (_mergedDictionaries[i].TryGetResource(key, out value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void IResourceProvider.AddOwner(IResourceHost owner)
        {
            owner = owner ?? throw new ArgumentNullException(nameof(owner));

            if (Owner != null)
            {
                throw new InvalidOperationException("The ResourceDictionary already has a parent.");
            }

            Owner = owner;

            var hasResources = Count > 0;

            if (_mergedDictionaries is object)
            {
                foreach (var i in _mergedDictionaries)
                {
                    i.AddOwner(owner);
                    hasResources |= i.HasResources;
                }
            }

            if (hasResources)
            {
                owner.NotifyHostedResourcesChanged(ResourcesChangedEventArgs.Empty);
            }
        }

        void IResourceProvider.RemoveOwner(IResourceHost owner)
        {
            owner = owner ?? throw new ArgumentNullException(nameof(owner));

            if (Owner == owner)
            {
                Owner = null;

                var hasResources = Count > 0;

                if (_mergedDictionaries is object)
                {
                    foreach (var i in _mergedDictionaries)
                    {
                        i.RemoveOwner(owner);
                        hasResources |= i.HasResources;
                    }
                }

                if (hasResources)
                {
                    owner.NotifyHostedResourcesChanged(ResourcesChangedEventArgs.Empty);
                }
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Owner?.NotifyHostedResourcesChanged(ResourcesChangedEventArgs.Empty);
        }
    }
}
