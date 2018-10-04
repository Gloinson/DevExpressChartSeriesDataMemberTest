using System;
using System.Windows.Input;

namespace TestSeriesDataMemberId.Common
{
    public class UICommand : ICommand
    {
        private readonly Action<object> method;
        private readonly Func<object, bool> canExecute;

        public void Execute(object parameter)
        {
            method(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public UICommand(Action<object> method, Func<object, bool> canExecute)
        {
            this.method = method;
            this.canExecute = canExecute;
        }

        public UICommand(Action method, Func<bool> canExecute)
            : this(x => method(), x => canExecute())
        {
        }

        public UICommand(Action<object> method)
            : this(method, x => true)
        {
        }

        public UICommand(Action method)
            : this(x => method(), x => true)
        {
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
