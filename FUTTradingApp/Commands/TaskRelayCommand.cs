using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FUTTradingApp.Commands
{
    public class TaskRelayCommand : ICommand, INotifyPropertyChanged
    {
        #region Fields

        private readonly Func<Task> _task = null;
        private readonly Func<bool> _canExecute = null;
        private bool _isExecuting;

        #endregion Fields

        #region Constructors

        public TaskRelayCommand(Func<Task> task)
            : this(task, null)
        {
        }

        public TaskRelayCommand(Func<Task> task, Func<bool> canExecute)
        {
            #region MEMORY_LEAK_DEBUG

#if MEMORY_LEAK_DEBUG && DEBUG
            this.RegisterNewRef(typeof(TaskRelayCommand));
#endif

            #endregion MEMORY_LEAK_DEBUG

            if (task == null)
                throw new ArgumentNullException("task");

            this._task = task;
            this._canExecute = canExecute;
        }

        public bool IsExecuting => this._isExecuting;

        #region MEMORY_LEAK_DEBUG

#if MEMORY_LEAK_DEBUG && DEBUG

        ~TaskRelayCommand()
        {
            this.UnregisterRef(typeof(TaskRelayCommand));
        }

#endif

        #endregion MEMORY_LEAK_DEBUG

        #endregion Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return (this._canExecute == null || this._canExecute());
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            if (!this._isExecuting)
            {
                this._isExecuting = true;
                this.OnPropertyChanged(nameof(this.IsExecuting));

                await this._task();

                this._isExecuting = false;
                this.OnPropertyChanged(nameof(this.IsExecuting));
            }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        #endregion ICommand Members

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }

    public class TaskRelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Func<T, Task> _task = null;
        private readonly Func<T, bool> _canExecute = null;
        private bool _isExecuting;

        #endregion Fields

        #region Constructors

        public TaskRelayCommand(Func<T, Task> task)
            : this(task, null)
        {
        }

        public TaskRelayCommand(Func<T, Task> task, Func<T, bool> canExecute)
        {
            #region MEMORY_LEAK_DEBUG

#if MEMORY_LEAK_DEBUG && DEBUG
            this.RegisterNewRef(typeof(TaskRelayCommand<T>));
#endif

            #endregion MEMORY_LEAK_DEBUG

            if (task == null)
                throw new ArgumentNullException("task");

            this._task = task;
            this._canExecute = canExecute;
        }

        #region MEMORY_LEAK_DEBUG

#if MEMORY_LEAK_DEBUG && DEBUG

        ~TaskRelayCommand()
        {
            this.UnregisterRef(typeof(TaskRelayCommand<T>));
        }

#endif

        #endregion MEMORY_LEAK_DEBUG

        #endregion Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return (this._canExecute == null || this._canExecute((T)parameter));
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            if (!this._isExecuting)
            {
                this._isExecuting = true;
                await this._task((T)parameter);
                this._isExecuting = false;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion ICommand Members
    }
}
