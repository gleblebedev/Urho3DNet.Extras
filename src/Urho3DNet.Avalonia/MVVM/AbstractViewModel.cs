using System.ComponentModel;
using Avalonia.Input;

namespace Urho3DNet.MVVM
{
    public abstract class AbstractViewModel: INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Changed property name.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var eventHandler = PropertyChanged;

            eventHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
