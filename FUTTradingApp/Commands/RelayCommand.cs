using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FUTTradingApp.Commands
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        #endregion Fields

        #region Ctor

        public RelayCommand(Action action)
            : this(action, (Func<bool>)null)
        {
        }

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        #endregion Ctor

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion Events

        #region Methods

        public bool CanExecute(object parameter)
        {
            return this._canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            this._action?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Methods
    }
}
