using System.Windows.Input;
using Urho3DNet.MVVM.Binding;

namespace Urho3DNet.MVVM
{
    public partial class ButtonView
    {
        #region Property Command

        public static readonly DirectProperty<ButtonView, ICommand> CommandProperty =
            UrhoProperty.RegisterDirect< ButtonView, ICommand>(
                nameof(Command),
                view => view.Command,
                (view, command) => view.Command = command);

        private ICommand _command;

        public ICommand Command
        {
            get
            {
                return _command;
            }

            set
            {
                SetAndRaise(CommandProperty, _command, value, _ =>
                {
                    _command = value;
                });
            }
        }

        #endregion Property Command


        #region Property CommandParameter

        public static readonly DirectProperty<ButtonView, object> CommandParameterProperty =
            UrhoProperty.RegisterDirect<ButtonView, object>(
                nameof(CommandParameter),
                view => view.CommandParameter,
                (view, command) => view.CommandParameter = command);

        private object _commandParameter;

        public object CommandParameter
        {
            get
            {
                return _commandParameter;
            }

            set
            {
                SetAndRaise(CommandParameterProperty, _commandParameter, value, _ =>
                {
                    _commandParameter = value;
                });
            }
        }

        #endregion Property CommandParameter

        protected override void SubscribeToEvents(Object target)
        {
            target.SubscribeToEvent(E.Click, HandleClickEvent);
            base.SubscribeToEvents(target);
        }

        private void HandleClickEvent(VariantMap obj)
        {
            _command?.Execute(_commandParameter);
        }

        protected override void UnsubscribeFromEvents(Object target)
        {
            target.UnsubscribeFromEvent(E.Click);
            base.UnsubscribeFromEvents(target);
        }
    }
}